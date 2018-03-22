using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;

namespace Testing.BLL.Interfaces
{
    public interface IExamService
    {
        void AddNewCOnnectionSubjectExam(Guid examId, Guid subjectId);
        IEnumerable<ExamDTO> GetExams();
        void AddNewTExam(ExamDTO examDTO);
        void UpdateExam(ExamDTO examDTO);
        void DeleteExam(Guid id);
        int CountExamForSubject(Guid id);
        IEnumerable<ViewExamSubjDTO> ReturnViewExamSub(Guid id);
        ExamDTO GetExamById(Guid id);
    }
}
