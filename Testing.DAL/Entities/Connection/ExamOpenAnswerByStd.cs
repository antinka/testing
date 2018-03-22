using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities.Connection
{
    public class ExamOpenAnswerByStd
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Exam")]
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey("OpenAnswerGivenByStutent")]
        public Guid OpenAnswerGivenByStutentId { get; set; }
        public OpenAnswersGivenByStutent OpenAnswerGivenByStutent { get; set; }
        [ForeignKey("StudentExamResult")]
        public Guid StudentExamResultId { get; set; }
        public StudentExamResult StudentExamResult { get; set; }
        public bool IsChecked { get; set; }
    }
}
