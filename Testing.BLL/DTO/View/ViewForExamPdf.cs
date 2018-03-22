using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewForExamPdf
    {
        public Guid StudentExamResultId { get; set; }
        [Display(Name = "Вопросы")]
        public String Questions { get; set; }
        [Display(Name = "Ответы")]
        public string Answers { get; set; }
        [Display(Name = "Предмет")]
        public string SubjectName { get; set; }
        [Display(Name = "Название экзамена")]
        public string ExamName { get; set; }
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
        [Display(Name = "Начало прохождения экзамена")]
        public DateTime StartExam { get; set; }
        [Display(Name = "Конец прохождения экзамена")]
        public DateTime EndtExam { get; set; }
        [Display(Name = "Оценка")]
        public double Mark { get; set; }
        [Display(Name = "Коментарий")]
        public string Comment { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
    }
}
