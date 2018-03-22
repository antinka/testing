using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;

namespace Testing.BLL.Interfaces
{
    public interface ITestDifficultService
    {
        IEnumerable<TestDifficultDTO> GetTestDifficult();
        TestDifficultDTO GetTestDifficultById(Guid id);
        void AddNewTestDifficult(TestDifficultDTO testDifficultDTO);
        void DeleteTestDifficult(Guid id);
        void UpdateTestDifficult(TestDifficultDTO testDifficultDTO);
    }
}
