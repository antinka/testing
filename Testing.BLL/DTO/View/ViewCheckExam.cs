using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewCheckExam
    {
        public Guid IdExamOpenAnsw { get; set; }
        [Display(Name = "Название предмета")]
        public string SubjectName { get; set; }
        [Display(Name = "Название экзамена")]
        public string ExamName { get; set; }
        [Display(Name = "Длитеьность")]
        public TimeSpan Runtime { get; set; }
    }
}
