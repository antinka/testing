using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Services;
using Testing.DAL;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Tests.Services
{
    [TestClass]
    public class TestDifficultServiceTest
    {
        Mock<IUnitOfWork> testDifRepo;
        TestDifficultService testDifService;
        public Guid id = Guid.NewGuid();
        List<TestDifficult> testDifficults = new List<TestDifficult>();
        TestDifficult testDifficult = new TestDifficult();
        bool boolDelete = false;
        [TestInitialize]
        public void SetUp()
        {
            // Create a new mock of the repository
            testDifRepo = new Mock<IUnitOfWork>();

            // Set up the mock for the repository
            testDifRepo.Setup(x => x.TestDifficults.GetList())
                .Returns(new List<TestDifficult>
                {
                new TestDifficult { Id = Guid.NewGuid(), Difficult = "1" },
                new TestDifficult { Id = Guid.NewGuid(), Difficult = "2" },
                new TestDifficult { Id = Guid.NewGuid(), Difficult = "3" }
                });

            testDifRepo.Setup(x => x.TestDifficults.GetById(id))
              .Returns(new TestDifficult { Id = id, Difficult = "4" });

            testDifRepo.Setup(x => x.TestDifficults.Create(It.IsAny<TestDifficult>())).Callback(() => testDifficults.Add(It.IsAny<TestDifficult>()));

            testDifRepo.Setup(x => x.TestDifficults.Update(It.IsAny<TestDifficult>())).Callback(() =>
                testDifficult.Difficult = "My name is test");

            testDifRepo.Setup(x => x.TestDifficults.Delete(It.IsAny<Guid>())).Callback(() => boolDelete = true);

            // Create the service and inject the repository into the service
            testDifService = new TestDifficultService(testDifRepo.Object);
        }

        [TestMethod]
        public void TestGetTestDifficults()
        {
            // Act
            var item = testDifService.GetTestDifficult();
            // Assert
            Assert.AreEqual(3, item.Count());
        }

        [TestMethod]
        public void TestGetTestDifficultById()
        {
            // Act
            var exam = testDifService.GetTestDifficultById(id);
            // Assert
            Assert.AreEqual("4", exam.Difficult);
        }

        [TestMethod]
        public void TestAddNewTestDifficult()
        {
            TestDifficultDTO item = new TestDifficultDTO();
            // Act
            testDifService.AddNewTestDifficult(item);
            testDifService.AddNewTestDifficult(item);
            // Assert
            Assert.AreEqual(2, testDifficults.Count());
        }

        [TestMethod]
        public void TestUpdateTestDifficult()
        {
            TestDifficultDTO item = new TestDifficultDTO();
            // Act
            testDifService.UpdateTestDifficult(item);
            // Assert
            Assert.AreEqual("My name is test", testDifficult.Difficult);
        }

        [TestMethod]
        public void TestDeleteTestDifficult()
        {
            // Act
            testDifService.DeleteTestDifficult(id);
            // Assert
            Assert.AreEqual(true, boolDelete);
        }
    }
}
