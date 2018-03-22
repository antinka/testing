﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string QuestionTitle { get; set; }
    }
}
