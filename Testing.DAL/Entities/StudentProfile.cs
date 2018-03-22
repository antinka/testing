using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testing.DAL.Entities
{
    public class StudentProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
