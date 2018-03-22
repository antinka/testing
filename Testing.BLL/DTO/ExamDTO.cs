using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class ExamDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название экзамена")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
    }
}
