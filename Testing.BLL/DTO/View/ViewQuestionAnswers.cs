using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.DTO.View
{
    public class ViewQuestionAnswers
    {
        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
    }
}
