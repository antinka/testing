using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class StudentTestResult
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime StartTest { get; set; }
        public DateTime EndtTest { get; set; }
        public double PercentOfRightAnswers { get; set; }
        [ForeignKey("StudentProfile")]
        public string StudentProfileId { get; set; }
        public StudentProfile StudentProfile { get; set; }
        [ForeignKey("Test")]
        public Guid TestId { get; set; }
        public Test Test { get; set; }

    }
}
