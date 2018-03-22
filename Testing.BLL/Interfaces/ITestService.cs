using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;

namespace Testing.BLL.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDTO> GetTest();
        int CountTestForSubject(Guid id);
        IEnumerable<ViewTestSubDTO> ReturnViewTestSub(Guid id);
        TestDTO GetTestById(Guid id);
        void AddNewTest(TestDTO testDTO, Guid testDifficultId);
        void DeleteTest(Guid id);
        void AddNewCOnnectionSubjectTest(Guid testId, Guid subjectId);
        void UpdateTest(TestDTO testDTO);

    }
}
