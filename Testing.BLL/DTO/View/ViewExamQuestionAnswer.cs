using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewExamQuestionAnswer
    {
        public Guid Id { get; set; }
        public Guid StudentExamResultId { get; set; }
        [Display(Name = "Вопросы")]
        public String Questions { get; set; }
        [Display(Name = "Ответы")]
        public string Answers { get; set; }
    }
}
