using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities.Connection
{
    public class SubjectExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey("Exam")]
        public Guid ExamId { get; set; }
    }
}
