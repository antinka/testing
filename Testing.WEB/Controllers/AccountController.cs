using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.DTO;
using Testing.BLL.Infrastructure;
using Testing.BLL.Interfaces;
using Testing.WEB.Models;
using Testing.WEB.Models.ForFileStream;

namespace Testing.WEB.Controllers
{
    public class AccountController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { UserName = model.UserName, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                    Logger.Log.Info("User enter wrong login or password");
                }
                else
                {
                    if (UserService.IsLockedUser(userDto))
                    {
                        //  ModelState.AddModelError("", "К сожалению вы заблокированы.");
                        return View("BlockedUser");
                    }
                    else
                    {
                        if (await UserService.IsEmailConfirmed(userDto))
                        {
                            AuthenticationManager.SignOut();
                            AuthenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = true
                            }, claim);
                            Logger.Log.Info("User enter to system " + User.Identity.GetUserId());
                        }
                        //  else ModelState.AddModelError("", "Email не подтвержден.");
                        else return View("ConfirnEmail");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Logger.Log.Info("User logout from system");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    LockoutEnabled = false,
                    Role = "student"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    Logger.Log.Info("New user add system");
                    UserDTO userDtoWithId = await UserService.FindAsync(userDto.UserName);
                    string code = await UserService.GenerateEmailConfirmationTokenAsync(userDtoWithId.Id);
                    string callBackURL = Url.Action("ConfirmEmail", "Account", new { userId = userDtoWithId.Id, code = code },
                                                   protocol: Request.Url.Scheme);

                    StringBuilder confirmingMessage = new StringBuilder(ConfigurationManager.AppSettings["EmailConfirmingMessageStart"]);
                    confirmingMessage.Append("<a href=\"");
                    confirmingMessage.Append(callBackURL);
                    confirmingMessage.Append("\">Press to confirm email. <a/>");
                    confirmingMessage.Append(ConfigurationManager.AppSettings["EmailConfirmingMessageEnd"]);

                    
                    await UserService.SendEmailAsync(userDtoWithId.Id, confirmingMessage.ToString());
                   return View("DisplayEmail");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                    Logger.Log.Info(operationDetails.Property + operationDetails.Message);
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            IdentityResult operationResult = await UserService.ConfirmEmail(userId, code);
            if (operationResult.Succeeded)
            {
                return View("SuccessRegistration");
            }
            return View("Error");
        }


        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "admin@admin.admin",
                UserName = "admin",
                Password = "adminadmin",
                Role = "admin",
                LockoutEnabled = false
            },new List<string> { "student", "admin","teacher"});

            await UserService.SetInitialData(new UserDTO
            {
                Email = "teacher@teacher.teacher",
                UserName = "teacher",
                Password = "teacher",
                Role = "teacher",
                LockoutEnabled = false
            });
        }

        //Return registration information and result of test user.
        [Authorize]
        public ActionResult PersonalArea()
        {
            string userId = User.Identity.GetUserId();
            string userImg = User.Identity.GetUserId() + ".png";
            string path = Path.Combine(Server.MapPath("~/UploadedImages/"), userImg);
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                ViewBag.ImageData = imageDataURL;
            }
            else
            {
                path = Path.Combine(Server.MapPath("~/UploadedImages/"), "avatarDefault.png");
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                ViewBag.ImageData = imageDataURL;
            }
            return View(UserService.GetUser(userId));
        }

        //Save photo on server.
        [Authorize]
        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                var path = "";
                if (uploadImage.ContentLength > 0)
                {
                    if(Path.GetExtension(uploadImage.FileName).ToLower()== ".png")
                    { string userImg = User.Identity.GetUserId() + ".png";
                        path = Path.Combine(Server.MapPath("~/UploadedImages/"), userImg);
                        uploadImage.SaveAs(path);
                        Logger.Log.Info("User " + User.Identity.GetUserId() + "add photo");
                    }
                }
            }
            Logger.Log.Info("User " + User.Identity.GetUserId() + "try to add photo");
            return RedirectToAction("PersonalArea");
        }


    }
}