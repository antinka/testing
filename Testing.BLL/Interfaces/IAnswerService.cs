using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;

namespace Testing.BLL.Interfaces
{
    public interface IAnswerService
    {
        IEnumerable<AnswerDTO> GetAnswers();
        AnswerDTO GetAnswerById(Guid id);
        void AddNewAnswer(AnswerDTO answerDTO);
        void DeleteAnswer(Guid id);
        void AddNewConnectionQuestionAnswer(Guid questionId, Guid answerId,bool isRight);
    
        IEnumerable<AnswerDTO> GetAnswersForQuestion(Guid questionId);
        void UpdateAnswer(AnswerDTO answerDTO);
        void MarkAsRight(Guid id);
        void MarkAsWrong(Guid id);
    }
}
