using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class TestDifficultDTO
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Сложность тестов")]
        public string Difficult { get; set; }
        public IList<TestDTO> Tests { get; set; }
    }
}
