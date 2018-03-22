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
    public class TestServiceTest
    {
        Mock<IUnitOfWork> testSRepo;
        TestService testService;
        public Guid id = Guid.NewGuid();
        List<Test> tests = new List<Test>();
        Test testTest = new Test();
        bool boolDelete = false;
        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            testSRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            testSRepo.Setup(x => x.Tests.GetList())
                .Returns(new List<Test>
                {
                new Test { Id = Guid.NewGuid(), Name = "math" },
                new Test { Id = Guid.NewGuid(), Name = "phys" },
                new Test { Id = Guid.NewGuid(), Name = "chorus" }
                });

            testSRepo.Setup(x => x.Tests.GetById(id))
              .Returns(new Test { Id = id, Name = "math" });

            testSRepo.Setup(x => x.Tests.Create(It.IsAny<Test>())).Callback(() => tests.Add(It.IsAny<Test>()));

            testSRepo.Setup(x => x.Tests.Update(It.IsAny<Test>())).Callback<Test>(p =>
            testTest.CountQuestion = 5);

            testSRepo.Setup(x => x.Tests.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);

            // Create the service and inject the repository into the service
            testService = new TestService(testSRepo.Object);
        }

        [TestMethod]
        public void TestGetTests()
        {
            // Act
            var subjects = testService.GetTest();
            // Assert
            Assert.AreEqual(3, subjects.Count());
        }

        [TestMethod]
        public void TestGetTestById()
        {
            // Act
            var subject = testService.GetTestById(id);
            // Assert
            Assert.AreEqual("math", subject.Name);
        }

        [TestMethod]
        public void TestAddNewTest()
        {
            TestDTO testDTO = new TestDTO();
            TestDifficultDTO testDifficultDTO = new TestDifficultDTO();
            // Act
            testService.AddNewTest(testDTO, testDifficultDTO.Id);
            testService.AddNewTest(testDTO, testDifficultDTO.Id);
            testService.AddNewTest(testDTO, testDifficultDTO.Id);
            // Assert
            Assert.AreEqual(3, tests.Count());
        }

        [TestMethod]
        public void TestUpdateTest()
        {
            TestDTO testDTO = new TestDTO();
            // Act
            testService.UpdateTest(testDTO);
            // Assert
            Assert.AreEqual(5, testTest.CountQuestion);
        }

        [TestMethod]
        public void TestDeleteTest()
        {
            // Act
            testService.DeleteTest(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }
    }
}
