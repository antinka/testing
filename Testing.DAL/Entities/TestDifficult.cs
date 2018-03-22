using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.Entities;

namespace Testing.DAL
{
    public class TestDifficult
    {
        [Key]
        public Guid Id { get; set; }
        public string Difficult { get; set; }
        public IList<Test> Tests { get; set; }
    }
}
