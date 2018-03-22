using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;

namespace Testing.BLL.Interfaces
{
    public interface IQuestionService
    {
       
       
        void AddNewCOnnectionTestQuestion(Guid testId, Guid questionId);
        int CountQuestionForTest(Guid id);
        IEnumerable<ViewQuestionTestDTO> ReturnQuestionTest(Guid id);
        IEnumerable<ViewAnswers> ReturnAllAnswerToQuestion(Guid id);
        IEnumerable<ViewQuestionAnswers> ReturnQuestionWithAnswers(Guid id);


        QuestionDTO GetQuestionById(Guid id);
        IEnumerable<QuestionDTO> GetQuestions();
        void AddNewQuestion(QuestionDTO questionDTO);
        void DeleteQuestion(Guid id);
        void UpdateQuestion(QuestionDTO questionDTO);


        void AddNewCOnnectionExamQuestion(Guid examId, Guid questionId);
        int CountQuestionForExam(Guid id);
        IEnumerable<ViewQuestionExamDTO> ReturnQuestionExam(Guid id);
        IEnumerable<QuestionDTO> GetQuestionsNotInCurrentExam(Guid id);
    }
}
