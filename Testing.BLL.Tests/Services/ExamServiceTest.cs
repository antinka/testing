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
    public class ExamServiceTest
    {
        Mock<IUnitOfWork> examRepo;
        ExamService examService;
        public Guid id = Guid.NewGuid();
        List<Exam> subjects = new List<Exam>();
        Exam subjectTest = new Exam();
        bool boolDelete = false;
        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            examRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            examRepo.Setup(x => x.Exams.GetList())
                .Returns(new List<Exam>
                {
                new Exam { Id = Guid.NewGuid(), Name = "math" },
                new Exam { Id = Guid.NewGuid(), Name = "phys" },
                new Exam { Id = Guid.NewGuid(), Name = "chorus" }
                });

            examRepo.Setup(x => x.Exams.GetById(id))
              .Returns(new Exam { Id = id, Name = "math" });

            examRepo.Setup(x => x.Exams.Create(It.IsAny<Exam>())).Callback(() => subjects.Add(It.IsAny<Exam>()));

            examRepo.Setup(x => x.Exams.Update(It.IsAny<Exam>())).Callback(() =>
                subjectTest.Name = "My name is test");

            examRepo.Setup(x => x.Exams.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);

            // Create the service and inject the repository into the service
            examService = new ExamService(examRepo.Object);
        }

        [TestMethod]
        public void TestGetExams()
        {
            // Act
            var subjects = examService.GetExams();
            // Assert
            Assert.AreEqual(3, subjects.Count());
        }

        [TestMethod]
        public void TestGetExamById()
        {
            // Act
            var exam = examService.GetExamById(id);
            // Assert
            Assert.AreEqual("math", exam.Name);
        }

        [TestMethod]
        public void TestAddNewExam()
        {
            ExamDTO item = new ExamDTO();
            // Act
            examService.AddNewTExam(item);
            examService.AddNewTExam(item);
            // Assert
            Assert.AreEqual(2, subjects.Count());
        }

        [TestMethod]
        public void TestUpdateExam()
        {
            ExamDTO item = new ExamDTO();
            // Act
            examService.UpdateExam(item);
            // Assert
            Assert.AreEqual("My name is test", subjectTest.Name);
        }

        [TestMethod]
        public void TestDeleteExam()
        {
            // Act
            examService.DeleteExam(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }
    }
}
