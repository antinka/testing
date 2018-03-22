using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
   public class ViewTestSubDTO
    {
        public Guid IdTest { get; set; }
        [Required]
        [Display(Name = "Название предмета")]
        public string SubjectName { get; set; }
        [Required]
        [Display(Name = "Название теста")]
        public string TestName { get; set; }
        [Required]
        [Display(Name = "Длитеьность")]
        public TimeSpan Runtime { get; set; }
        [Required]
        [Display(Name = "Сложность")]
        public string Difficult { get; set; }
        [Display(Name = "Количество вопросов")]
        public int CountQuestion { get; set; }
    }
}
