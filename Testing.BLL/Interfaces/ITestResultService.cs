using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.Entities;

namespace Testing.BLL.Interfaces
{
    public interface ITestResultService
    {
        Guid AddStudResultReturnId(string idStud, Guid idTest, DateTime timeStert, DateTime timeStop);
        StudentTestResult GetStudentResultById(Guid id);
        void UpdateStudResult(double mark, Guid idStudResult);
        double CountMarkForTest(Guid idTest, Guid idStudResult);
        void AddAnswerGivenByStud(Guid questionId, Guid answerId, Guid studResult);
    }
}
