using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Testing.WEB.Models.Testing
{
    public class AddQuestionAnswerView
    {
        [Required]
        [Display(Name = "Вопрос")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "Ответы")]
        public string[] Answers { get; set; }
    }
}