using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;

namespace Testing.BLL.Interfaces
{
    public interface IOpenAnswerGivenByStutenService
    {
        Guid AddNewOpenAnswer(string openAnswerGivenByStutentDTO, string studId);
        void AddNewConnectionExamAnswer(Guid examId, Guid answersId, Guid studExamRes);
        void AddNewConnectionExamEmptyAnswer(Guid examId, Guid answersId, Guid studExamRes);
    }
}
