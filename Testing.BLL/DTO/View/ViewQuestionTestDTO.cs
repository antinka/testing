using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class ViewQuestionTestDTO
    {
        public Guid IdQuestion { get; set; }
        [Display(Name = "Название теста")]
        public string TestName { get; set; }
        [Display(Name = "Вопрос")]
        public string QuestionTitle { get; set; }
        public string[] AnswerId { get; set; }
        [Display(Name = "Ответы")]
        public string []AnswerTitle { get; set; }
        [Display(Name = "Правильный")]
        public string [] IsRight { get; set; }
    }
}
