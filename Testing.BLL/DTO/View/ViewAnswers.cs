using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewAnswers
    {
        public Guid Id { get; set; }
        [Display(Name = "Ответ")]
        public string AnswerTitle { get; set; }
        [Display(Name = "Правильный")]
        public bool IsRight { get; set; }
        public Guid IdConnectionWithRightAnswer { get; set; }
    }
}
