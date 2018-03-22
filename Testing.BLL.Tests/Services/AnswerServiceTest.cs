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
    public class AnswerServiceTest
    {
        Mock<IUnitOfWork> answerRepo;
        AnswerService answerService;
        public Guid id = Guid.NewGuid();
        List<Answer> answers = new List<Answer>();
        Answer answerTest = new Answer();
        bool boolDelete = false;

        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            answerRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            answerRepo.Setup(x => x.Answers.GetList())
                .Returns(new List<Answer>
                {
                new Answer { Id = Guid.NewGuid(), AnswerTitle = "1" },
                new Answer { Id = Guid.NewGuid(), AnswerTitle = "2" },
                new Answer { Id = Guid.NewGuid(), AnswerTitle = "3" }
                });

            answerRepo.Setup(x => x.Answers.GetById(id))
              .Returns(new Answer { Id = id, AnswerTitle = "5" });

            answerRepo.Setup(x => x.Answers.Create(It.IsAny<Answer>())).Callback(() => answers.Add(It.IsAny<Answer>()));

            answerRepo.Setup(x => x.Answers.Update(It.IsAny<Answer>())).Callback(() =>
                answerTest.AnswerTitle = "My name is test");

            answerRepo.Setup(x => x.Answers.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);

            // Create the service and inject the repository into the service
            answerService = new AnswerService(answerRepo.Object);
        }

        [TestMethod]
        public void TestGetAnswers()
        {
            // Act
            var subjects = answerService.GetAnswers();
            // Assert
            Assert.AreEqual(3, subjects.Count());
        }

        [TestMethod]
        public void TestGeAnswerById()
        {
            // Act
            var subject = answerService.GetAnswerById(id);
            // Assert
            Assert.AreEqual("5", subject.AnswerTitle);
        }

        [TestMethod]
        public void TestAddNewAnswer()
        {
            AnswerDTO item = new AnswerDTO();
            // Act
            answerService.AddNewAnswer(item);
            answerService.AddNewAnswer(item);
            // Assert
            Assert.AreEqual(2, answers.Count());
        }

        [TestMethod]
        public void TestUpdateAnswer()
        {
            AnswerDTO item = new AnswerDTO();
            // Act
            answerService.UpdateAnswer(item);
            // Assert
            Assert.AreEqual("My name is test", answerTest.AnswerTitle);
        }

        [TestMethod]
        public void TestDeleteAnswer()
        {
            // Act
            answerService.DeleteAnswer(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }
    }
}
