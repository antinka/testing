using System;
using System.ComponentModel.DataAnnotations;

namespace Testing.DAL.Entities
{
    public class Test
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Runtime { get; set; }
        public TestDifficult TestDifficult { get; set; }
        public Guid? TestDifficultId { get; set; }
        public int CountQuestion { get; set; }
    }
}
