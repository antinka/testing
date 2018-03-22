using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Роль")]
        public string Role { get; set; }
        public string Email { get; set; }
        [Display(Name = "Заблокирован")]
        public bool LockoutEnabled { get; set; }
    }
}
