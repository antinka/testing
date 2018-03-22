using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class StudentExamResult
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime StartExam { get; set; }
        public DateTime EndtExam { get; set; }
        public int Mark { get; set; }
        [ForeignKey("StudentProfile")]
        public string StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; }
 

    }
}
