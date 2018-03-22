using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities.Connection
{
    public class QuestionAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public  Question Question { get; set; }
        [ForeignKey("Answer")]
        public Guid AnswerId { get; set; }
        public  Answer Answer { get; set; }
        public bool IsRight { get; set; }
    }
}
