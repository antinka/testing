using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.BLL;
using Testing.BLL.Interfaces;

namespace Testing.WEB.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public ActionResult Index(Guid? Id)
        {
            ViewBag.Roles = new SelectList(UserService.GetRoles(), "Id", "Name");
            Guid id = Id ?? Guid.Empty;
            if (id == Guid.Empty)
            {
                return View(UserService.GetUsers()); 
            }
            else
            {
                ViewBag.Id = Id;
                if (UserService.GetUsersByIdRole(id).Count() < 1)
                    return View("ViewNoUserInCurrentRole");
                return View(UserService.GetUsersByIdRole(id));
            }
        }
        public ActionResult LockUser(Guid id)
        {
            UserService.LockUser(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "lock user " + id);
            return RedirectToAction("Index");
        }
        public ActionResult UnLockUser(Guid id)
        {
            UserService.UnLockUser(id);
            Logger.Log.Info("User " + User.Identity.GetUserId() + "unlock user " + id);
            return RedirectToAction("Index");
        }
    }
}