using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Testing.BLL.DTO;
using Testing.BLL.Services;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;
using Testing.DAL.Repositories;
using System.Linq;
namespace Testing.BLL.Tests.Services
{
    [TestClass]
    public class SubjectServiceTest
    {

        Mock<IUnitOfWork> subjectRepo;
        SubjectService subjectService;
        public Guid id = Guid.NewGuid();
        List<Subject> subjects = new List<Subject>();
        Subject subjectTest = new Subject();
        bool boolDelete = false;
        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            subjectRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            subjectRepo.Setup(x => x.Subjects.GetList())
                .Returns(new List<Subject>
                {
                new Subject { Id = Guid.NewGuid(), Name = "math" },
                new Subject { Id = Guid.NewGuid(), Name = "phys" },
                new Subject { Id = Guid.NewGuid(), Name = "chorus" }
                });

            subjectRepo.Setup(x => x.Subjects.GetById(id))
              .Returns( new Subject { Id = id, Name = "math" });

            subjectRepo.Setup(x => x.Subjects.Create(It.IsAny<Subject>())).Callback(() => subjects.Add(It.IsAny<Subject>()));

            subjectRepo.Setup(x => x.Subjects.Update(It.IsAny<Subject>())).Callback(() =>
                subjectTest.Name = "My name is test" );

            subjectRepo.Setup(x => x.Subjects.Delete(It.IsAny<Guid>())).Callback(() => boolDelete=true);

            // Create the service and inject the repository into the service
            subjectService = new SubjectService(subjectRepo.Object);
        }

        [TestMethod]
        public void TestGetSubjects()
        {
            // Act
           var subjects = subjectService.GetSubjects();
            // Assert
            Assert.AreEqual(3, subjects.Count());
        }

        [TestMethod]
        public void TestGetSubjectById()
        {
            // Act
            var subject = subjectService.GetSubjectById(id);
            // Assert
            Assert.AreEqual("math", subject.Name);
        }

        [TestMethod]
        public void TestAddNewSubject()
        {
            SubjectDTO subjectTestDTO = new SubjectDTO();
            // Act
            subjectService.AddNewSubject(subjectTestDTO);
            subjectService.AddNewSubject(subjectTestDTO);
            // Assert
            Assert.AreEqual(2, subjects.Count());
        }

        [TestMethod]
        public void TestUpdateSubject()
        {
            SubjectDTO subjectTestDTO = new SubjectDTO();
            // Act
             subjectService.UpdateSubject(subjectTestDTO);
            // Assert
            Assert.AreEqual("My name is test", subjectTest.Name);
        }

        [TestMethod]
        public void TestDeleteSubject()
        {
            // Act
            subjectService.DeleteSubject(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }

    }
}
