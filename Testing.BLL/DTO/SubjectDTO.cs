using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO
{
    public class SubjectDTO
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }
    }
}
