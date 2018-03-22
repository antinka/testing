using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //Class for counting test result, take student result by Id,update it, add.
    public class TestResultService : ITestResultService
    {
        IUnitOfWork Database { get; set; }
        public TestResultService(IUnitOfWork uow)
        {
            Database = uow;
        }

        int CountQuestionInTest(Guid idTest)
        {
            return (from qa in Database.QuestionAnswers.GetList()
                    join tq in Database.TestQuestions.GetList() on qa.QuestionId equals tq.QuestionId into tq_join
                    from tq in tq_join.DefaultIfEmpty()
                    where
                        qa.IsRight == true &&
                        tq.TestId == idTest
                    select new
                    {
                        qa.QuestionId
                    }).Distinct().Count();
        }

        int CountRightAnswersGivenByStudentsToTest(Guid idTest, Guid idStudResult)
        {
            var resultR = (from q in ((from QuestionAnswers in Database.QuestionAnswers.GetList()
                                       where
  (from TestQuestions in Database.TestQuestions.GetList()
   where
      TestQuestions.TestId == idTest
   select new
   {
       TestQuestions.QuestionId
   }).Contains(new { QuestionId = QuestionAnswers.QuestionId }) &&
QuestionAnswers.IsRight == true
                                       select new
                                       {
                                           QuestionAnswers.QuestionId,
                                           QuestionAnswers.AnswerId,
                                           QuestionAnswers.IsRight
                                       }).Distinct())
                           join w in (
           ((from AnswerGivenByStudents in Database.AnswerGivenByStudents.GetList()
             where
           AnswerGivenByStudents.StudentResultId == idStudResult
             select new
             {
                 AnswerGivenByStudents.QuestionId,
                 AnswerGivenByStudents.AnswerId,
                 IsRight = "s"
             }).Distinct()))
           on new { q.QuestionId, q.AnswerId }
                       equals new { w.QuestionId, w.AnswerId } into w_join
                           from w in w_join.DefaultIfEmpty()
                           select new
                           {
                               q.QuestionId,
                               Result = w == null ? Guid.Empty : w.QuestionId
                           });
            var resultL = (from q in (
             ((from AnswerGivenByStudents in Database.AnswerGivenByStudents.GetList()
               where
             AnswerGivenByStudents.StudentResultId == idStudResult
               select new
               {
                   AnswerGivenByStudents.QuestionId,
                   AnswerGivenByStudents.AnswerId,
                   IsRight = "s"
               }).Distinct()))
                           join w in (
                               ((from QuestionAnswers in Database.QuestionAnswers.GetList()
                                 where
                         (from TestQuestions in Database.TestQuestions.GetList()
                          where
                             TestQuestions.TestId == idTest
                          select new
                          {
                              TestQuestions.QuestionId
                          }).Contains(new { QuestionId = QuestionAnswers.QuestionId }) &&
                       QuestionAnswers.IsRight == true
                                 select new
                                 {
                                     QuestionAnswers.QuestionId,
                                     QuestionAnswers.AnswerId,
                                     QuestionAnswers.IsRight
                                 }).Distinct()))
                                 on new { q.QuestionId, q.AnswerId }
                             equals new { w.QuestionId, w.AnswerId } into w_join
                           from w in w_join.DefaultIfEmpty()
                           select new
                           {
                               q.QuestionId,
                               Result = w == null ? Guid.Empty : w.QuestionId
                           });

            var result = resultL.Union(resultR);

            var WrongAnsw = (from r in result
                             where r.Result == Guid.Empty
                             select new
                             {
                                 r.QuestionId
                             });


            return (from QuestionAnswers in Database.QuestionAnswers.GetList()
                    where
                          (from TestQuestions in Database.TestQuestions.GetList()
                           where
                 TestQuestions.TestId == idTest
                           select new
                           {
                               TestQuestions.QuestionId
                           }).Contains(new { QuestionId = QuestionAnswers.QuestionId }) &&
                      QuestionAnswers.IsRight == true
                    group QuestionAnswers by new
                    {
                        QuestionAnswers.QuestionId
                    } into g
                    where !WrongAnsw.Contains(new { g.Key.QuestionId })
                    select new
                    {
                        g.Key.QuestionId
                    }).Count();
        }

        public double CountMarkForTest(Guid idTest, Guid idStudResult)
        {
            double mark = 0;
            try
            {
                int countQuestionInTest = CountQuestionInTest(idTest);
                int countRightAnsw = CountRightAnswersGivenByStudentsToTest(idTest, idStudResult);

                mark = (double)(100 / countQuestionInTest) * countRightAnsw;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return mark;
        }

        public Guid AddStudResultReturnId(string idStud, Guid idTest, DateTime timeStert, DateTime timeStop)
        {
            try
            {
                StudentTestResult studentResult = new StudentTestResult();
                studentResult.Id = Guid.NewGuid();
                studentResult.StudentProfileId = idStud;
                studentResult.TestId = idTest;
                studentResult.StartTest = timeStert;
                studentResult.EndtTest = timeStop;

                Database.StudentTestResults.Create(studentResult);
                Database.StudentTestResults.Save();
                return studentResult.Id;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
                return Guid.Empty;
            }
        }

        public void UpdateStudResult(double mark, Guid idStudResult)
        {
            try
            {
                StudentTestResult studentResult = GetStudentResultById(idStudResult);
                studentResult.PercentOfRightAnswers = mark;
                Database.StudentTestResults.Update(studentResult);
                Database.StudentTestResults.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void AddAnswerGivenByStud(Guid questionId, Guid answerId, Guid studResult)
        {
            try
            {
                if (answerId != Guid.Empty)
                {
                    AnswerGivenByStudent answerGivenByStudent = new AnswerGivenByStudent();
                    answerGivenByStudent.QuestionId = questionId;
                    answerGivenByStudent.AnswerId = answerId;
                    answerGivenByStudent.StudentResultId = studResult;
                    Database.AnswerGivenByStudents.Create(answerGivenByStudent);
                    Database.AnswerGivenByStudents.Save();
                }
                else
                {

                    AnswerGivenByStudent answerGivenByStudent = new AnswerGivenByStudent();
                    answerGivenByStudent.QuestionId = questionId;
                    answerGivenByStudent.StudentResultId = studResult;
                    answerGivenByStudent.AnswerId = Guid.Empty;
                    Database.AnswerGivenByStudents.Create(answerGivenByStudent);
                    Database.AnswerGivenByStudents.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public StudentTestResult GetStudentResultById(Guid id)
        {
            StudentTestResult studentResult = new StudentTestResult();
            try
            {
                studentResult = Database.StudentTestResults.GetById(id);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return studentResult;
        }
    }
}
