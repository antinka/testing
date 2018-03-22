using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //Class for get stud result exam, update this and add new, plus add comment by teacher.
   public class ExamResultService: IExamResultService
    {
        IUnitOfWork Database { get; set; }
        public ExamResultService(IUnitOfWork uow)
        {
            Database = uow;
        }
       

        public void UpdateStudResult(int mark, Guid idStudResult, Guid idConnectionExamAnsw)
        {
            try
            {
                StudentExamResult studentResult = GetStudentResultById(idStudResult);
                studentResult.Mark = mark;
                Database.StudentExamResults.Update(studentResult);
                Database.StudentExamResults.Save();
                ExamOpenAnswerByStd examOpenAnswerByStd = Database.ExamOpenAnswerByStds.GetById(idConnectionExamAnsw);
                examOpenAnswerByStd.IsChecked = true;
                Database.ExamOpenAnswerByStds.Update(examOpenAnswerByStd);
                Database.ExamOpenAnswerByStds.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        public StudentExamResult GetStudentResultById(Guid id)
        {
            StudentExamResult studentResult = new StudentExamResult();
            try
            {
                studentResult = Database.StudentExamResults.GetById(id);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return studentResult;
        }

        public Guid AddStudExamRes(string idUser, DateTime StartExam, DateTime EndtExam)
        {
            StudentExamResult studentResult = new StudentExamResult();
            studentResult.EndtExam = EndtExam;
            studentResult.StartExam = StartExam;
            studentResult.StudentProfileId = idUser;
            studentResult.Mark = 0;
            studentResult.Id = Guid.NewGuid();
            Database.StudentExamResults.Create(studentResult);
            Database.StudentExamResults.Save();
            return studentResult.Id;
        }
        public void AddCommentToCheckExam(Guid idStudExamRes, string comment)
        {
            CommentToExamResult commentToExam = new CommentToExamResult();
            commentToExam.Comment = comment;
            commentToExam.StudentExamResultId = idStudExamRes;
            Database.CommentToExamResults.Create(commentToExam);
            Database.CommentToExamResults.Save();
        }
    }
}
