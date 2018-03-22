using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities.Connection
{
    public class SubjectTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey ("Subject")]
        public Guid SubjectId { get; set; }
        public Test Test { get; set; }
        [ForeignKey("Test")]
        public Guid TestId { get; set; }
    }
}
