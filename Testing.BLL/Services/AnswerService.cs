using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    // Class for  work with answer to tests, include GRUD and add connection beetween answer and question, mark answer as wrong adn right.
    public class AnswerService : IAnswerService
    {
        IUnitOfWork Database { get; set; }
        public AnswerService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddNewAnswer(AnswerDTO answerDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, Answer>());
                IMapper mapper = config.CreateMapper();
                Database.Answers.Create(mapper.Map<AnswerDTO, Answer>(answerDTO));
                Database.Answers.Save();

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void AddNewConnectionQuestionAnswer(Guid questionId, Guid answerId, bool isRight)
        {
            try
            {
                Answer answer = Database.Answers.GetById(answerId);
                if (answer != null)
                {
                    Question question = Database.Questions.GetById(questionId);
                    if (question != null)
                    {
                    QuestionAnswer questionAnswer = new QuestionAnswer();
                    questionAnswer.Answer = answer;
                    questionAnswer.Question = question;
                    questionAnswer.IsRight = isRight;
                    Database.QuestionAnswers.Create(questionAnswer);
                    Database.QuestionAnswers.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
       

        public void DeleteAnswer(Guid id)
        {
            try
            {
                Answer answer = Database.Answers.GetById(id);
                if (answer != null)
                {
                    Database.Answers.Delete(id);
                    Database.Answers.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public AnswerDTO GetAnswerById(Guid id)
        {
            AnswerDTO answerDTO = new AnswerDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>());
                IMapper mapper = config.CreateMapper();
                answerDTO = mapper.Map<Answer, AnswerDTO>(Database.Answers.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return answerDTO;
        }

        public IEnumerable<AnswerDTO> GetAnswers()
        {
            List<AnswerDTO> answer = new List<AnswerDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>());
                IMapper mapper = config.CreateMapper();
                answer = mapper.Map<IEnumerable<Answer>, List<AnswerDTO>>(Database.Answers.GetList());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return answer;
        }

        public IEnumerable<AnswerDTO> GetAnswersForQuestion(Guid questionId)
        {
            List<AnswerDTO> answerDTO = new List<AnswerDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>());
                IMapper mapper = config.CreateMapper();
           
                foreach ( var answers in Database.QuestionAnswers.GetList().Where(q => q.Question.Id == questionId))
                {
                   answerDTO.Add(mapper.Map<Answer,AnswerDTO>(Database.Answers.GetById(answers.Id)));
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return answerDTO;
        }

        public void UpdateAnswer(AnswerDTO answerDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, Answer>());
                IMapper mapper = config.CreateMapper();
                Database.Answers.Update(mapper.Map<AnswerDTO, Answer>(answerDTO));
                Database.Answers.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        public void MarkAsRight(Guid id)
        {
            try
            {
                QuestionAnswer qa = Database.QuestionAnswers.GetById(id);
                if (qa != null)
                {
                    qa.IsRight = true;
                    Database.QuestionAnswers.Update(qa);
                    Database.QuestionAnswers.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        public void MarkAsWrong(Guid id)
        {
            try
            {
                QuestionAnswer qa = Database.QuestionAnswers.GetById(id);
                if (qa != null)
                {
                    qa.IsRight = false;
                    Database.QuestionAnswers.Update(qa);
                    Database.QuestionAnswers.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
    }
}
