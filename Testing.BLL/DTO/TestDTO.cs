using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class TestDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название теста")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
        [Required]
        [Display(Name = "Сложность")]
        public Guid? TestDifficultId { get; set; }
        public TestDifficultDTO TestDifficult { get; set; }
        public int CountQuestion { get; set; }
    }
}
