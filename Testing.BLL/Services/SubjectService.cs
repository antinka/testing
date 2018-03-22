using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.BLL.Services
{
    //GRUD subject.
    public class SubjectService : ISubjectService
    {
        IUnitOfWork Database { get; set; }
        public SubjectService(IUnitOfWork uow)
        {
            Database = uow;
        }
        //Get all subject in db.
        public IEnumerable<SubjectDTO> GetSubjects()
        {
            IEnumerable<SubjectDTO> subjectDTO = new List<SubjectDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>());
                IMapper mapper = config.CreateMapper();
                subjectDTO= mapper.Map<IEnumerable<Subject>, List<SubjectDTO>>(Database.Subjects.GetList());
             }
            catch (Exception ex)
            {
             Logger.Log.Error(ex.Message);
            }
            return subjectDTO;
        }

        //Return subject choosen by id.
        public SubjectDTO GetSubjectById(Guid id)
        {
            SubjectDTO subjectDTO = new SubjectDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Subject, SubjectDTO>());
                IMapper mapper = config.CreateMapper();
                subjectDTO = mapper.Map<Subject, SubjectDTO>(Database.Subjects.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return subjectDTO;
        }

        // Add new subject.
        public void AddNewSubject(SubjectDTO subjectDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, Subject>());
                IMapper mapper = config.CreateMapper();
                Database.Subjects.Create(mapper.Map<SubjectDTO, Subject>(subjectDTO));
                Database.Subjects.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        // Delete choosen subject by id.
        public void DeleteSubject(Guid id)
        {
            try
            {
                Subject subject = Database.Subjects.GetById(id);
                if (subject != null)
                {
                    Database.Subjects.Delete(id);
                    Database.Subjects.Save();
                    Database.SubjectTests.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        // Update subject.
        public void UpdateSubject(SubjectDTO subjectDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<SubjectDTO, Subject>());
                IMapper mapper = config.CreateMapper();
                Database.Subjects.Update(mapper.Map<SubjectDTO, Subject>(subjectDTO));
                Database.Subjects.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }
    }
}
