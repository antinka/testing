using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //GRUD, add connection subject test,count test in subject,return view ViewTestSubDTO.
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }
        public TestService(IUnitOfWork uow)
        {
            Database = uow;
        }
        //Get all subject in db.
        public IEnumerable<TestDTO> GetTest()
        {
            IEnumerable<TestDTO> testDTO = new List<TestDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Test, TestDTO>());
                IMapper mapper = config.CreateMapper();
                testDTO= mapper.Map<IEnumerable<Test>, List<TestDTO>>(Database.Tests.GetList());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return testDTO;
        }
        public void AddNewCOnnectionSubjectTest(Guid testId, Guid subjectId)
        {
            try
            {
                Test test = Database.Tests.GetById(testId);
                Subject subject = Database.Subjects.GetById(subjectId);
                SubjectTest subjectTest = new SubjectTest();
                subjectTest.Test = test;
                subjectTest.Subject = subject;
                Database.SubjectTests.Create(subjectTest);
                Database.SubjectTests.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        // Add new test.
        public void AddNewTest(TestDTO testDTO, Guid testDifficultId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDifficult, TestDifficultDTO>());
                IMapper mapper = config.CreateMapper();
                testDTO.TestDifficultId = testDifficultId;
                config = new MapperConfiguration(cfg => cfg.CreateMap<TestDTO, Test>());
                mapper = config.CreateMapper();
                Database.Tests.Create(mapper.Map<TestDTO, Test>(testDTO));
                Database.Tests.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        // Delete choosen test by id.
        public void DeleteTest(Guid id)
        {
            try
            {
                Test test = Database.Tests.GetById(id);
                if (test != null)
                {
                    Database.Tests.Delete(id);
                    Database.Tests.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        //Return test choosen by id
        public TestDTO GetTestById(Guid id)
        {
            TestDTO testDTO = new TestDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Test, TestDTO>());
                IMapper mapper = config.CreateMapper();
                testDTO = mapper.Map<Test, TestDTO>(Database.Tests.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return testDTO;
        }

        public IEnumerable<ViewTestSubDTO> ReturnViewTestSub(Guid id)
        {
            IEnumerable<ViewTestSubDTO> viewTestSubDTO2 = new List<ViewTestSubDTO>();
            try
            {
                if (id != Guid.Empty)
                {
                    IEnumerable<ViewTestSubDTO> viewTestSubDTO = (from su in Database.Subjects.GetList()
                                                                  where
                                                                    su.Id == id
                                                                  join st in Database.SubjectTests.GetList() on su.Id equals st.SubjectId
                                                                  join t in Database.Tests.GetList() on st.TestId equals t.Id into t_join
                                                                  from t in t_join.DefaultIfEmpty()
                                                                  join td in Database.TestDifficults.GetList() on t.TestDifficultId equals td.Id
                                                                  select new ViewTestSubDTO
                                                                  {
                                                                      IdTest = t == null ? Guid.Empty : t.Id,
                                                                      SubjectName = su.Name,
                                                                      TestName = t == null ? String.Empty : t.Name,
                                                                      Runtime = t == null ? TimeSpan.MinValue : t.Runtime,
                                                                      Difficult = td.Difficult,
                                                                      CountQuestion = t.CountQuestion
                                                                  });
                    return viewTestSubDTO;
                }
                else
                {
                    viewTestSubDTO2 = (from t in Database.Tests.GetList()
                                       join st in Database.SubjectTests.GetList() on t.Id equals st.TestId into st_join
                                       from st in st_join.DefaultIfEmpty()
                                       join su in Database.Subjects.GetList() on st.SubjectId equals su.Id into t_join
                                       from su in t_join.DefaultIfEmpty()
                                       join td in Database.TestDifficults.GetList() on t.TestDifficultId equals td.Id into td_join
                                       from td in td_join.DefaultIfEmpty()
                                       select new ViewTestSubDTO
                                       {
                                           IdTest = t == null ? Guid.Empty : t.Id,
                                           SubjectName = su.Name,
                                           TestName = t == null ? String.Empty : t.Name,
                                           Runtime = t == null ? TimeSpan.MinValue : t.Runtime,
                                           Difficult = td.Difficult,
                                           CountQuestion = t.CountQuestion
                                       });
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewTestSubDTO2;
        }

        public void UpdateTest(TestDTO testDTO)
        {
            try
            {
                int countQuestionInTest = (from tq in Database.TestQuestions.GetList()
                                           where
                                               tq.TestId == testDTO.Id
                                           select tq.Id).Count();
                testDTO.CountQuestion = countQuestionInTest;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDTO, Test>());
                  IMapper  mapper = config.CreateMapper();
               
                 Database.Tests.Update(mapper.Map<TestDTO, Test>(testDTO));
                 Database.Tests.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
        public int CountTestForSubject(Guid id)
        {
            int testDTO = 0;
            try
            {
                testDTO = Database.SubjectTests.GetList().Where(st => st.SubjectId == id).Count();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return testDTO;
        }
    }
}
