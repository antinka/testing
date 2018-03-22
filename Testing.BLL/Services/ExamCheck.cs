using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO.View;
using Testing.BLL.Interfaces;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //Class for Generate Exam Result in Pdf, taking all not checked  and checked exam.
    public class ExamCheck : IExamCheck
    {
        IUnitOfWork Database { get; set; }
        public ExamCheck(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ViewCheckExam> GetNotCheckExams( Guid idSubj)
        {
            IEnumerable<ViewCheckExam> viewCheckExam = new List<ViewCheckExam>();
            try
            {
                if (idSubj != Guid.Empty)
                {
                    viewCheckExam = (from eo in Database.ExamOpenAnswerByStds.GetList()
                                     join e in Database.Exams.GetList() on eo.ExamId equals e.Id
                                     join se in Database.SubjectExams.GetList() on e.Id equals se.ExamId
                                     join s in Database.Subjects.GetList() on se.SubjectId equals s.Id
                                     where
                                       eo.IsChecked == false &&
                                       s.Id == idSubj
                                     select new ViewCheckExam
                                     {
                                         IdExamOpenAnsw = eo.Id,
                                         SubjectName = s.Name,
                                         ExamName = e.Name,
                                         Runtime = e.Runtime
                                     }
                    );
                }
                else
                {
                    viewCheckExam = (from eo in Database.ExamOpenAnswerByStds.GetList()
                                     join e in Database.Exams.GetList() on eo.ExamId equals e.Id
                                     join se in Database.SubjectExams.GetList() on e.Id equals se.ExamId
                                     join s in Database.Subjects.GetList() on se.SubjectId equals s.Id
                                     where
                                       eo.IsChecked == false
                                     select new ViewCheckExam
                                     {
                                         IdExamOpenAnsw = eo.Id,
                                         SubjectName = s.Name,
                                         ExamName = e.Name,
                                         Runtime = e.Runtime
                                     });
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewCheckExam;
        }


        public ViewExamQuestionAnswer CheckExam(Guid idCheckExam)
        {
            ViewExamQuestionAnswer viewExamQuestionAnswer = new ViewExamQuestionAnswer();
            try
            {
                var examQuestionAnswer = from eo in Database.ExamOpenAnswerByStds.GetList()
                                         join e in Database.Exams.GetList() on eo.ExamId equals e.Id
                                         join eq in Database.ExamQuestions.GetList() on e.Id equals eq.ExamId
                                         join q in Database.Questions.GetList() on eq.QuestionId equals q.Id
                                         join oa in Database.OpenAnswerGivenByStutents.GetList() on eo.OpenAnswerGivenByStutentId equals oa.Id
                                         where
                                           eo.Id == idCheckExam
                                         select new
                                         {
                                             Id= idCheckExam,
                                             StudentExamResultId = eo.StudentExamResultId,
                                             QuestionTitle = q.QuestionTitle,
                                             Answers = oa.Answers
                                         };

                viewExamQuestionAnswer = examQuestionAnswer.GroupBy(t => t.StudentExamResultId).ToList().Select(qw => new ViewExamQuestionAnswer
                {
                    Id = qw.First().Id,
                    StudentExamResultId = qw.First().StudentExamResultId,
                    Answers = qw.First().Answers,
                    Questions = string.Join("& ", qw.Select(e => e.QuestionTitle))
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewExamQuestionAnswer;
        }


        public ViewForExamPdf GenerateExamResultPdf(Guid idExam, Guid IdOpenAnswer)
        {
            ViewForExamPdf viewForExamPdf = new ViewForExamPdf();
            try
            {
                var viewFullinfAbouExam = from e in Database.Exams.GetList()
                                          join eo in Database.ExamOpenAnswerByStds.GetList() on e.Id equals eo.ExamId 
                                          join eq in Database.ExamQuestions.GetList() on e.Id equals eq.ExamId 
                                          join q in Database.Questions.GetList() on eq.QuestionId equals q.Id
                                          join oa in Database.OpenAnswerGivenByStutents.GetList() on eo.OpenAnswerGivenByStutentId equals oa.Id
                                          join ser in Database.StudentExamResults.GetList() on eo.StudentExamResultId equals ser.Id
                                          join sp in Database.StudentProfiles.GetList() on ser.StudentProfileId equals sp.Id
                                          join se in Database.SubjectExams.GetList() on e.Id equals se.ExamId
                                          join s in Database.Subjects.GetList() on se.SubjectId equals s.Id
                                          join cm in Database.CommentToExamResults.GetList() on ser.Id equals cm.StudentExamResultId into cm_join
                                          from cm in cm_join.DefaultIfEmpty()
                                          where
                                            e.Id == idExam &&
                                            eo.OpenAnswerGivenByStutentId == IdOpenAnswer
                                          select new
                                          {
                                              StudentExamResultId = eo.StudentExamResultId,
                                              Comment =cm == null ?string.Empty : cm.Comment,
                                              SubjectName = s.Name,
                                              ExamName = e.Name,
                                              Runtime = e.Runtime,
                                              Answers = oa == null ? string.Empty : oa.Answers,
                                              QuestionTitle = q.QuestionTitle,
                                              FirstName = sp.FirstName,
                                              SecondName = sp.SecondName,
                                              StartExam = ser.StartExam,
                                              EndtExam = ser.EndtExam,
                                              Mark = ser.Mark
                                          };

            viewForExamPdf = viewFullinfAbouExam.GroupBy(t => t.StudentExamResultId).ToList().Select(qw => new ViewForExamPdf
            {
                StudentExamResultId = qw.First().StudentExamResultId,
                Comment = qw.First().Comment,
                SubjectName = qw.First().SubjectName,
                ExamName = qw.First().ExamName,
                Runtime = qw.First().Runtime,
                Answers = qw.First().Answers,
                Questions = string.Join("& ", qw.Select(e => e.QuestionTitle)),
                FirstName = qw.First().FirstName,
                SecondName = qw.First().SecondName,
                StartExam = qw.First().StartExam,
                EndtExam = qw.First().EndtExam,
                Mark = qw.First().Mark
            }).FirstOrDefault();


        }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewForExamPdf;
        }
    }
}