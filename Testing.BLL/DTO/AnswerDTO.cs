using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class AnswerDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Ответ")]
        public string AnswerTitle { get; set; }
    }
}
