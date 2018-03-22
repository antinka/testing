using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.DTO.View;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //GRUD question, add connection exam question.
    public class QuestionService : IQuestionService
    {
        IUnitOfWork Database { get; set; }
        public QuestionService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddNewCOnnectionTestQuestion(Guid testId, Guid questionId)
        {
            try
            {
                Test test = Database.Tests.GetById(testId);
                Question question = Database.Questions.GetById(questionId);
                TestQuestion testQuestion = new TestQuestion();
                testQuestion.Test = test;
                testQuestion.Question = question;
                test.CountQuestion += 1;
                Database.Tests.Update(test);
                Database.Tests.Save();
                Database.TestQuestions.Create(testQuestion);
                Database.TestQuestions.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void AddNewQuestion(QuestionDTO questionDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, Question>());
                IMapper mapper = config.CreateMapper();
                Database.Questions.Create(mapper.Map<QuestionDTO, Question>(questionDTO));
                Database.Questions.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void DeleteQuestion(Guid id)
        {
            try
            {
                Question question = Database.Questions.GetById(id);
                if (question != null)
                {
                    Database.Questions.Delete(id);
                    Database.Questions.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public QuestionDTO GetQuestionById(Guid id)
        {
            QuestionDTO questionDTO = new QuestionDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>());
                IMapper mapper = config.CreateMapper();
                questionDTO = mapper.Map<Question, QuestionDTO>(Database.Questions.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return questionDTO;
        }

        public IEnumerable<QuestionDTO> GetQuestions()
        {
            List<QuestionDTO> list = new List<QuestionDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>());
                IMapper mapper = config.CreateMapper();
                list= mapper.Map<IEnumerable<Question>, List<QuestionDTO>>(Database.Questions.GetList());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return list;
        }

        public IEnumerable<ViewQuestionTestDTO> ReturnQuestionTest(Guid id)
        {
            IEnumerable<ViewQuestionTestDTO> viewQuestionTestDTO = new List<ViewQuestionTestDTO>();
            try
            {
                var viewQuestionTest = (from t in Database.Tests.GetList()
                                    where
                                      t.Id == id
                                    join tq in Database.TestQuestions.GetList() on t.Id equals tq.TestId into tq_join
                                    from tq in tq_join.DefaultIfEmpty()
                                    join q in Database.Questions.GetList() on tq.QuestionId equals q.Id into q_join
                                    from q in q_join.DefaultIfEmpty()
                                    join qa in Database.QuestionAnswers.GetList() on q.Id equals qa.QuestionId into qa_join
                                    from qa in qa_join.DefaultIfEmpty()
                                    join a in Database.Answers.GetList() on qa.AnswerId equals a.Id into a_join
                                    from a in a_join.DefaultIfEmpty()
                                    select new
                                    {
                                        IdQuestion = q.Id,
                                        TestName = t.Name,
                                        QuestionTitle = q.QuestionTitle,
                                        AnswerTitle = a == null ?" " : a.AnswerTitle,
                                        IsRight = qa.IsRight,
                                        AnswerId = a == null ? Guid.Empty : a.Id,
                                    });

            viewQuestionTestDTO = viewQuestionTest.GroupBy(t => t.QuestionTitle).ToList().Select(qw => new ViewQuestionTestDTO
            {
                IdQuestion = qw.First().IdQuestion,
                TestName = qw.First().TestName,
                QuestionTitle = qw.First().QuestionTitle,
                IsRight = string.Join(",", qw.Select(e => e.IsRight)).Split(new Char[] { ',' }),
                AnswerTitle = string.Join(",", qw.Select(e => e.AnswerTitle)).Split(new Char[] { ',' }),
                AnswerId = string.Join(",", qw.Select(e => e.AnswerId)).Split(new Char[] { ',' })
            });
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewQuestionTestDTO;
        }
        public int CountQuestionForTest(Guid id)
        {
            int tests = 0;
            try
            {
                tests = Database.TestQuestions.GetList().Where(st => st.TestId == id).Count();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return tests;
        }
        public IEnumerable<ViewAnswers> ReturnAllAnswerToQuestion(Guid id)
        {
            IEnumerable<ViewAnswers> ViewAnswers = new List<ViewAnswers>();
            try
            {
                ViewAnswers = from qa in Database.QuestionAnswers.GetList()
                                                   where
                                                     qa.QuestionId == id
                                                   join a in Database.Answers.GetList() on qa.AnswerId equals a.Id into a_join
                                                   from a in a_join.DefaultIfEmpty()
                                                   select new ViewAnswers
                                                   {
                                                       Id = a.Id,
                                                       AnswerTitle = a.AnswerTitle,
                                                       IsRight = qa.IsRight,
                                                       IdConnectionWithRightAnswer = qa.Id

                                                   };
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return ViewAnswers;
        }
        public void UpdateQuestion(QuestionDTO questionDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, Question>());
                IMapper mapper = config.CreateMapper();
                Database.Questions.Update(mapper.Map<QuestionDTO, Question>(questionDTO));
                Database.Questions.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
}
        public IEnumerable<ViewQuestionAnswers> ReturnQuestionWithAnswers(Guid id)
        {
            IEnumerable<ViewQuestionAnswers> viewQuestionAnswers = new List<ViewQuestionAnswers>();
            try
            {
                viewQuestionAnswers =(from t in Database.Tests.GetList()
                                                                   where
                                                                     t.Id == id
                                                                   join tq in Database.TestQuestions.GetList() on t.Id equals tq.TestId into tq_join
                                                                   from tq in tq_join.DefaultIfEmpty()
                                                                   join q in Database.Questions.GetList() on tq.QuestionId equals q.Id into q_join
                                                                   from q in q_join.DefaultIfEmpty()
                                                                   select new ViewQuestionAnswers
                                                                   {
                                                                       QuestionId = q.Id,
                                                                       Question = q.QuestionTitle,
                                                                       Answers = (from qa in Database.QuestionAnswers.GetList()
                                                                                  where qa.QuestionId== q.Id
                                                                                  join a in Database.Answers.GetList() on qa.AnswerId equals a.Id into a_join
                                                                                  from a in a_join.DefaultIfEmpty()
                                                                                 
                                                                                  select new AnswerDTO
                                                                                  {
                                                                                      Id = a.Id,
                                                                                      AnswerTitle = a.AnswerTitle
                                                                                  }).AsEnumerable()     
                                                                   }).AsEnumerable();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewQuestionAnswers;
        }

        public void AddNewCOnnectionExamQuestion(Guid examId, Guid questionId)
        {
            try
            {
                Exam exam = Database.Exams.GetById(examId);
                Question question = Database.Questions.GetById(questionId);
                ExamQuestion examQuestion = new ExamQuestion();
                examQuestion.Exam = exam;
                examQuestion.Question = question;
                Database.ExamQuestions.Create(examQuestion);
                Database.ExamQuestions.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public int CountQuestionForExam(Guid id)
        {
            int examsCount = 0;
            try
            {
                examsCount = Database.ExamQuestions.GetList().Where(st => st.ExamId == id).Count();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return examsCount;
        }

        public IEnumerable<ViewQuestionExamDTO> ReturnQuestionExam(Guid id)
        {
            IEnumerable<ViewQuestionExamDTO> viewQuestionExamDTO = new List<ViewQuestionExamDTO>();
            try
            {
                var viewQuestionTest = (from e in Database.Exams.GetList()
                                        where
                                          e.Id == id
                                        join tq in Database.ExamQuestions.GetList() on e.Id equals tq.ExamId into tq_join
                                        from tq in tq_join.DefaultIfEmpty()
                                        join q in Database.Questions.GetList() on tq.QuestionId equals q.Id into q_join
                                        from q in q_join.DefaultIfEmpty()
                                        select new
                                        {
                                            IdQuestion = q.Id,
                                            ExamName = e.Name,
                                            QuestionTitle = q.QuestionTitle,
                                
                                        });

                viewQuestionExamDTO = viewQuestionTest.GroupBy(t => t.QuestionTitle).ToList().Select(qw => new ViewQuestionExamDTO
                {
                    IdQuestion = qw.First().IdQuestion,
                    ExamName = qw.First().ExamName,
                    QuestionTitle = qw.First().QuestionTitle,
                   
                });
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewQuestionExamDTO;
        }

        public IEnumerable<QuestionDTO> GetQuestionsNotInCurrentExam(Guid id)
        {
            IEnumerable<QuestionDTO> questionList = new List<QuestionDTO>();
            try
            {
                      var  questionListExam = (from q in Database.Questions.GetList()
                        join eq in Database.ExamQuestions.GetList() on q.Id equals eq.QuestionId into eq_join
                                        from eq in eq_join.DefaultIfEmpty()
                        select new 
                        {
                            Id = q.Id,
                            QuestionTitle = q.QuestionTitle,
                            ExamId=eq==null?Guid.Empty : eq.ExamId
                        });

                questionList = ( from qle in questionListExam
                                 where 
                                 qle.ExamId == Guid.Empty ||
                                  qle.ExamId !=id
                                  select new QuestionDTO
                                  {
                                      Id = qle.Id,
                                      QuestionTitle = qle.QuestionTitle,
                                  }
                                 );

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return questionList;
        }
    }
}
