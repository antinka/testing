using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.Entities;

namespace Testing.BLL.Interfaces
{
    public interface IExamResultService
    {

        StudentExamResult GetStudentResultById(Guid id);
        void UpdateStudResult(int mark, Guid idStudResult, Guid idConnectionExamAnsw);
        Guid AddStudExamRes(string idUser, DateTime StartExam, DateTime EndtExam);
        void AddCommentToCheckExam(Guid idStudExamRes, string comment);
    }
}
