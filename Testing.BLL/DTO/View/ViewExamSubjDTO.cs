using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewExamSubjDTO
    {
        public Guid IdExam { get; set; }
        [Required]
        [Display(Name = "Название предмета")]
        public string SubjectName { get; set; }
        [Required]
        [Display(Name = "Название экзамена")]
        public string ExamName { get; set; }
        [Required]
        [Display(Name = "Длитеьность")]
        public TimeSpan Runtime { get; set; }
    }
}
