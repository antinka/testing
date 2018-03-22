using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewPersonalData
    {
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Заблокирован")]
        public String Lock { get; set; }
        public bool LockoutEnabled { get; set; }
        public List<PersonalDataTest> PersonalDataTest { get; set; }
        public List<PersonalDataExam> PersonalDataExam { get; set; }

    }
}
