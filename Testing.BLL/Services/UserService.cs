using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;
using Testing.BLL.Infrastructure;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;
namespace Testing.BLL.Services
{
    //GRUD,2FA,Locked/unLoced.
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
       
        public IIdentityMessageService EmailService
        {
            get {return Database.UserManager.EmailService;  }
            set{ Database.UserManager.EmailService = value;}
        }

        public IUserTokenProvider<ApplicationUser, string> UserTokenProvider
        {
            get { return Database.UserManager.UserTokenProvider; }
            set {  Database.UserManager.UserTokenProvider = value;  }
        }
        public UserService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        
        public async Task<bool> IsEmailConfirmed(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
                return user.EmailConfirmed;
            else
                return false;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var provider = new DpapiDataProtectionProvider("Testing");
            Database.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("EmailConfirmation"));
            var token = await Database.UserManager.GenerateEmailConfirmationTokenAsync(userId);
            return token;
        }

        public async Task SendEmailAsync(string userId, string message)
        {
            await Database.UserManager.SendEmailAsync(userId, "Подтверждение электронной почты", message);

        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            return await Database.UserManager.ConfirmEmailAsync(userId, code);
        }

        public void SetDefaultTokenProvider()
        {
            var provider = new DpapiDataProtectionProvider("Testing");
            Database.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("EmailConfirmation"));
        }


        public async Task<UserDTO> FindAsync(string userName)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                UserDTO resultUser = new UserDTO();
                resultUser.Email = user.Email;
                resultUser.Id = user.Id;
                resultUser.FirstName = user.StudentProfile.FirstName;
                resultUser.SecondName = user.StudentProfile.SecondName;
                return resultUser;
            }
            return null;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByNameAsync(userDto.UserName);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.UserName, LockoutEnabled = userDto.LockoutEnabled };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                //Add new user.
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // Add role to current user.
                StudentProfile clientProfile = new StudentProfile { Id = user.Id, FirstName = userDto.FirstName, SecondName = userDto.SecondName };
                Database.StudentProfiles.Create(clientProfile);
                await Database.SaveAsync();


                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.UserName, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }


        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }


        public async Task SetInitialData(UserDTO teacherDto)
        {
            await Create(teacherDto);
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, ApplicationUser>());
            IMapper mapper = config.CreateMapper();
            await Database.UserManager.UpdateAsync(mapper.Map<UserDTO, ApplicationUser>(userDto));
            await Database.SaveAsync();
        }

        public void LockUser(Guid id)
        {
            try
            {
                ApplicationUser user = Database.UserManager.FindById(id.ToString());
                if (user != null)
                {
                    user.LockoutEnabled = true;
                    Database.UserManager.Update(user);
                    Database.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void UnLockUser(Guid id)
        {
            try
            {
                ApplicationUser user = Database.UserManager.FindById(id.ToString());
                if (user != null)
                {
                    user.LockoutEnabled = false;
                    Database.UserManager.Update(user);
                    Database.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        public bool IsLockedUser(UserDTO userDto)
        {
            ApplicationUser user =  Database.UserManager.Find(userDto.UserName, userDto.Password);
            if (user != null)
                return user.LockoutEnabled;
            else
                return false;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<UserDTO> userDTO = new List<UserDTO>();
            try
            {
                userDTO = from user in Database.UserManager.Users.ToList()
                          join s in Database.StudentProfiles.GetList() on user.Id equals s.Id
                          select new UserDTO
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              FirstName = s.FirstName,
                              SecondName = s.SecondName,
                              Email = user.Email,
                              LockoutEnabled = user.LockoutEnabled
                          };
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return userDTO;
        }

        public IEnumerable<ApplicationRole> GetRoles()
        {
            IEnumerable<ApplicationRole> role = new List<ApplicationRole>();
            try
            {
                role = Database.RoleManager.Roles.ToList();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return role;
        }
        public IEnumerable<UserDTO> GetUsersByIdRole(Guid id)
        {
            string ID = id.ToString();
            IEnumerable<UserDTO> userDTO = new List<UserDTO>();
            try
            {
                userDTO = from user in Database.UserManager.Users.ToList()
                          join s in Database.StudentProfiles.GetList() on user.Id equals s.Id
                          where user.Roles.Any(r => r.RoleId == ID)
                          select new UserDTO
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              FirstName = s.FirstName,
                              SecondName = s.SecondName,
                              Email = user.Email,
                              LockoutEnabled = user.LockoutEnabled
                          };
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return userDTO;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ViewPersonalData GetUser(string id)
        {
            ViewPersonalData viewPersonalData = new ViewPersonalData();
            try
            {
                viewPersonalData = (from user in Database.UserManager.Users.ToList()
                                    join s in Database.StudentProfiles.GetList() on user.Id equals s.Id
                                    where user.Id == id
                                    select new ViewPersonalData
                                    {
                                        UserName = user.UserName,
                                        FirstName = s.FirstName,
                                        SecondName = s.SecondName,
                                        Email = user.Email,
                                        LockoutEnabled = user.LockoutEnabled,
                                        PersonalDataTest = (from s in Database.StudentProfiles.GetList()
                                                            join sr in Database.StudentTestResults.GetList() on s.Id equals sr.StudentProfileId
                                                            join t in Database.Tests.GetList() on sr.TestId equals t.Id into t_join
                                                            from t in t_join.DefaultIfEmpty()
                                                            join td in Database.TestDifficults.GetList() on t.TestDifficultId equals td.Id
                                                            join st in Database.SubjectTests.GetList() on new { Id = t.Id } equals new { Id = st.TestId }
                                                            join sb in Database.Subjects.GetList() on st.SubjectId equals sb.Id
                                                            where
                                                              s.Id == id
                                                            select new PersonalDataTest
                                                            {
                                                                SubjectName = sb.Name,
                                                                TestName = t.Name,
                                                                TestDifficult = td.Difficult,
                                                                Runtime = t.Runtime,
                                                                StartTest = sr.StartTest,
                                                                EndtTest = sr.EndtTest,
                                                                Mark = sr.PercentOfRightAnswers
                                                            }).ToList(),
                                        PersonalDataExam = (from sp in Database.StudentProfiles.GetList()
                                                            where sp.Id == id
                                                            join ser in Database.StudentExamResults.GetList() on sp.Id equals ser.StudentProfileId
                                                            join eo in Database.ExamOpenAnswerByStds.GetList() on ser.Id equals eo.StudentExamResultId
                                                            join e in Database.Exams.GetList() on eo.ExamId equals e.Id
                                                            join se in Database.SubjectExams.GetList() on e.Id equals se.ExamId
                                                            join s in Database.Subjects.GetList() on se.SubjectId equals s.Id
                                                            join cm in Database.CommentToExamResults.GetList() on ser.Id equals cm.StudentExamResultId into cm_join
                                                            from cm in cm_join.DefaultIfEmpty()
                                                            select new PersonalDataExam
                                                            {
                                                                IdOpenAnswer = eo.OpenAnswerGivenByStutentId,
                                                                IdExam = e.Id,
                                                                IdStud = sp.Id,
                                                                Mark = ser.Mark,
                                                                StartExam = ser.StartExam,
                                                                EndtExam = ser.EndtExam,
                                                                Runtime = e.Runtime,
                                                                ExamName = e.Name,
                                                                SubjectName = s.Name,
                                                                Comment = cm == null ? "нет" : cm.Comment
                                                            }).ToList()


                                    }).FirstOrDefault();
                if (viewPersonalData.LockoutEnabled == false)
                {
                    viewPersonalData.Lock = "нет";
                }
                else
                {
                    viewPersonalData.Lock = "Да";
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewPersonalData;
        }
    }
}
