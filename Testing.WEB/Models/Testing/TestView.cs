using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Testing.WEB.Models.Testing
{
    public class TestView
    {
        [Required]
        [Display(Name = "Название теста")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Длительность")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan Runtime { get; set; }
    }
}