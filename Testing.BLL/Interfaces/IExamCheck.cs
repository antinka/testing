using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO.View;

namespace Testing.BLL.Interfaces
{
    public interface IExamCheck
    {
        IEnumerable<ViewCheckExam> GetNotCheckExams(Guid idSubj);
        ViewExamQuestionAnswer CheckExam(Guid idCheckExam);
        ViewForExamPdf GenerateExamResultPdf(Guid idExam, Guid IdOpenAnswer);
    }
}
