using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;
using Testing.BLL.Infrastructure;
using Testing.DAL.Entities;

namespace Testing.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        IIdentityMessageService EmailService { get; set; }

        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task SetInitialData(UserDTO adminDto);
        Task UpdateUser(UserDTO userDto);
        IEnumerable<UserDTO> GetUsers();
        ViewPersonalData GetUser(string id);
        void UnLockUser(Guid id);
        void LockUser(Guid id);
        IEnumerable<ApplicationRole> GetRoles();
        IEnumerable<UserDTO> GetUsersByIdRole(Guid id);
        Task<UserDTO> FindAsync(string userName);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task SendEmailAsync(string userId,  string message);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<bool> IsEmailConfirmed(UserDTO userDto);
        void SetDefaultTokenProvider();
        bool IsLockedUser(UserDTO userDto);
        
    }
}
