using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewQuestionExamDTO
    {
        public Guid IdQuestion { get; set; }
        [Display(Name = "Название экзамена")]
        public string ExamName { get; set; }
        [Display(Name = "Вопрос")]
        public string QuestionTitle { get; set; }
    }
}
