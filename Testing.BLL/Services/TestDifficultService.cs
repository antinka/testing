using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //GRUD.
    public class TestDifficultService : ITestDifficultService
    {
        IUnitOfWork Database { get; set; }
        public TestDifficultService(IUnitOfWork uow)
        {
            Database = uow;
        }

        //Get all TestDifficult in db.
        public IEnumerable<TestDifficultDTO> GetTestDifficult()
        {
            IEnumerable<TestDifficultDTO> testDifficultDTO = new List<TestDifficultDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDifficult, TestDifficultDTO>());
                IMapper mapper = config.CreateMapper();
                testDifficultDTO= mapper.Map<IEnumerable<TestDifficult>, List<TestDifficultDTO>>(Database.TestDifficults.GetList());

            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return testDifficultDTO;
        }

        public void AddNewTestDifficult(TestDifficultDTO testDifficultDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDifficultDTO, TestDifficult>());
                IMapper mapper = config.CreateMapper();
                Database.TestDifficults.Create(mapper.Map<TestDifficultDTO, TestDifficult>(testDifficultDTO));
                Database.TestDifficults.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void DeleteTestDifficult(Guid id)
        {
            try
            {
                TestDifficult testDifficult = Database.TestDifficults.GetById(id);
                if (testDifficult != null)
                {
                    Database.TestDifficults.Delete(id);
                    Database.TestDifficults.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public TestDifficultDTO GetTestDifficultById(Guid id)
        {
            TestDifficultDTO testDifficultDTO = new TestDifficultDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDifficult, TestDifficultDTO>());
                IMapper mapper = config.CreateMapper();
                testDifficultDTO = mapper.Map<TestDifficult, TestDifficultDTO>(Database.TestDifficults.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return testDifficultDTO;
        }
       
        public void UpdateTestDifficult(TestDifficultDTO testDifficultDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<TestDifficultDTO, TestDifficult>());
                IMapper mapper = config.CreateMapper();
                Database.TestDifficults.Update(mapper.Map<TestDifficultDTO, TestDifficult>(testDifficultDTO));
                Database.TestDifficults.Save();
             }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
    }
}
