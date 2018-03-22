using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class CommentToExamResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Comment { get; set; }
        [ForeignKey("StudentExamResult")]
        public Guid StudentExamResultId { get; set; }
        public StudentExamResult StudentExamResult { get; set; }
    }
}
