using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.BLL.Tests.Calculation
{
    [TestClass]
    public class TestResultTest
    {
        [TestMethod]
        public void TestMark10Question5RightAnswers()
        {
            CalculateTestsResult calculateTestResult = new CalculateTestsResult();
            // Act
            var result = calculateTestResult.CalculateMark(10, 5);
            // Assert
            Assert.AreEqual(50, result);
        }

    }
}
