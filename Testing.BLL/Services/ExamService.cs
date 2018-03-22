using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.BLL.Interfaces;
using Testing.DAL.Entities;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;
using Testing.BLL.DTO.View;

namespace Testing.BLL.Services
{
    // Class for GRUD emax return view ViewExamSubjDTO and add new connection subject exam.
    public class ExamService: IExamService
    {
        IUnitOfWork Database { get; set; }
        public ExamService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public int CountExamForSubject(Guid id)
        {
            int examDTO = 0;
            try
            {
                examDTO = Database.SubjectExams.GetList().Where(st => st.SubjectId == id).Count();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return examDTO;
        }
        public void AddNewCOnnectionSubjectExam(Guid examId, Guid subjectId)
        {
            try
            {
                Exam exam = Database.Exams.GetById(examId);
                Subject subject = Database.Subjects.GetById(subjectId);
                SubjectExam subjectExam = new SubjectExam();
                subjectExam.Exam = exam;
                subjectExam.Subject = subject;
                Database.SubjectExams.Create(subjectExam);
                Database.SubjectExams.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public IEnumerable<ExamDTO> GetExams()
        {
            IEnumerable<ExamDTO> examDTO = new List<ExamDTO>();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Exam, ExamDTO>());
                IMapper mapper = config.CreateMapper();
                examDTO = mapper.Map<IEnumerable<Exam>, List<ExamDTO>>(Database.Exams.GetList());
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return examDTO;
        }

        public void AddNewTExam(ExamDTO examDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ExamDTO, Exam>());
                IMapper mapper = config.CreateMapper();
                Database.Exams.Create(mapper.Map<ExamDTO, Exam>(examDTO));
                Database.Exams.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void UpdateExam(ExamDTO examDTO)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ExamDTO, Exam>());
                IMapper mapper = config.CreateMapper();
                Database.Exams.Update(mapper.Map<ExamDTO, Exam>(examDTO));
                Database.Exams.Save();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public void DeleteExam(Guid id)
        {
            try
            {
                Exam exam = Database.Exams.GetById(id);
                if (exam != null)
                {
                    Database.Exams.Delete(id);
                    Database.Exams.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
        }

        public IEnumerable<ViewExamSubjDTO> ReturnViewExamSub(Guid id)
        {
            IEnumerable<ViewExamSubjDTO> viewTestSubDTO2 = new List<ViewExamSubjDTO>();
            try
            {
                if (id != Guid.Empty)
                {
                    IEnumerable<ViewExamSubjDTO> viewTestSubDTO = (from su in Database.Subjects.GetList()
                                                                  where
                                                                    su.Id == id
                                                                  join st in Database.SubjectExams.GetList() on su.Id equals st.SubjectId
                                                                  join t in Database.Exams.GetList() on st.ExamId equals t.Id into t_join
                                                                  from t in t_join.DefaultIfEmpty()
                                                                  select new ViewExamSubjDTO
                                                                  {
                                                                      IdExam= t == null ? Guid.Empty : t.Id,
                                                                      SubjectName = su.Name,
                                                                      ExamName = t == null ? String.Empty : t.Name,
                                                                      Runtime = t == null ? TimeSpan.MinValue : t.Runtime
                                                                  });
                    return viewTestSubDTO;
                }
                else
                {
                    viewTestSubDTO2 = (from t in Database.Exams.GetList()
                                       join st in Database.SubjectExams.GetList() on t.Id equals st.ExamId into st_join
                                       from st in st_join.DefaultIfEmpty()
                                       join su in Database.Subjects.GetList() on st.SubjectId equals su.Id into t_join
                                       from su in t_join.DefaultIfEmpty()
                                       select new ViewExamSubjDTO
                                       {
                                           IdExam = t == null ? Guid.Empty : t.Id,
                                           SubjectName = su.Name,
                                           ExamName = t == null ? String.Empty : t.Name,
                                           Runtime = t == null ? TimeSpan.MinValue : t.Runtime
                                       });
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return viewTestSubDTO2;
        }

        //Return test choosen by id
        public ExamDTO GetExamById(Guid id)
        {
            ExamDTO examDTO = new ExamDTO();
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Exam, ExamDTO>());
                IMapper mapper = config.CreateMapper();
                examDTO = mapper.Map<Exam, ExamDTO>(Database.Exams.GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return examDTO;
        }
    }
}
