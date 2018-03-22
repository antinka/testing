using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Services;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Tests.Services
{
    [TestClass]
    public class QuestionServiceTest
    {
        Mock<IUnitOfWork> questRepo;
        QuestionService questService;
        public Guid id = Guid.NewGuid();
        List<Question> questions = new List<Question>();
        Question questTest = new Question();
        bool boolDelete = false;
        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            questRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            questRepo.Setup(x => x.Questions.GetList())
                .Returns(new List<Question>
                {
                new Question { Id = Guid.NewGuid(), QuestionTitle = "1+1" },
                new Question { Id = Guid.NewGuid(),  QuestionTitle = "1+2" },
                new Question { Id = Guid.NewGuid(),  QuestionTitle = "1+3" }
                });

            questRepo.Setup(x => x.Questions.GetById(id))
              .Returns(new Question { Id = id, QuestionTitle = "1+0" });

            questRepo.Setup(x => x.Questions.Create(It.IsAny<Question>())).Callback(() => questions.Add(It.IsAny<Question>()));

            questRepo.Setup(x => x.Questions.Update(It.IsAny<Question>())).Callback(() =>
                questTest.QuestionTitle = "My name is test");

            questRepo.Setup(x => x.Questions.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);

            // Create the service and inject the repository into the service
            questService = new QuestionService(questRepo.Object);
        }

        [TestMethod]
        public void TestGetQuestions()
        {
            // Act
            var items = questService.GetQuestions();
            // Assert
            Assert.AreEqual(3, items.Count());
        }

        [TestMethod]
        public void TestGetQuestionyId()
        {
            // Act
            var exam = questService.GetQuestionById(id);
            // Assert
            Assert.AreEqual("1+0", exam.QuestionTitle);
        }

        [TestMethod]
        public void TestAddNewQuestion()
        {
            QuestionDTO item = new QuestionDTO();
            // Act
            questService.AddNewQuestion(item);
            questService.AddNewQuestion(item);
            // Assert
            Assert.AreEqual(2, questions.Count());
        }

        [TestMethod]
        public void TestUpdateQuestion()
        {
            QuestionDTO item = new QuestionDTO();
            // Act
            questService.UpdateQuestion(item);
            // Assert
            Assert.AreEqual("My name is test", questTest.QuestionTitle);
        }

        [TestMethod]
        public void TestDeleteQuestion()
        {
            // Act
            questService.DeleteQuestion(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }
    }
}
