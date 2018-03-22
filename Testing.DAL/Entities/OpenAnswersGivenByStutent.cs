using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
   public class OpenAnswersGivenByStutent
    {
        [Key]
        public Guid Id { get; set; }
        public string Answers { get; set; }
    }
}
