using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class PersonalDataExam
    {
        public Guid IdOpenAnswer { get; set; }
        public Guid IdExam { get; set; }
        public string IdStud { get; set; }
        [Display(Name = "Предмет")]
        public string SubjectName { get; set; }
        [Display(Name = "Название экзамена")]
        public string ExamName { get; set; }
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
        [Display(Name = "Начало прохождения теста")]
        public DateTime StartExam { get; set; }
        [Display(Name = "Конец прохождения теста")]
        public DateTime EndtExam { get; set; }
        [Display(Name = "Оценка")]
        public double Mark { get; set; }
        [Display(Name = "Коментарий")]
        public string Comment { get; set; }
    }
}
