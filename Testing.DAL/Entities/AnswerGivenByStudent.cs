using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class AnswerGivenByStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        [ForeignKey("StudentResult")]
        public Guid StudentResultId { get; set; }
        public StudentTestResult StudentResult { get; set; }

        public Guid AnswerId { get; set; }

    }
}
