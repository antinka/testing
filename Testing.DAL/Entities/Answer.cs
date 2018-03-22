using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class Answer
    {
        [Key]
        public Guid Id { get;  set; }
        public string AnswerTitle { get; set; }
    }
}
