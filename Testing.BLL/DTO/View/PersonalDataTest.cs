using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
   public class PersonalDataTest
    {
        [Display(Name = "Предмет")]
        public string SubjectName { get; set; }
        [Display(Name = "Тест")]
        public string TestName { get; set; }
        [Display(Name = "Сложность")]
        public string TestDifficult { get; set; }
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
        [Display(Name = "Начало прохождения теста")]
        public DateTime StartTest { get; set; }
        [Display(Name = "Конец прохождения теста")]
        public DateTime EndtTest { get; set; }
        [Display(Name = "Процент правильных ответов")]
        public double Mark { get; set; }
    }
}
