using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Testing.BLL.DTO;

namespace Testing.WEB.Models.Testing
{
    public class QuestionView
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public AnswerDTO[] Answer { get; set; }
    }
}