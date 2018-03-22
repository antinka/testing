using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.BLL.DTO;
using Testing.DAL.Entities;

namespace Testing.BLL.Interfaces
{
    public interface ISubjectService
    {
        IEnumerable<SubjectDTO> GetSubjects();
        SubjectDTO GetSubjectById(Guid id);
        void AddNewSubject(SubjectDTO subjectDTO);
        void DeleteSubject(Guid id);
        void UpdateSubject(SubjectDTO subjectDTO);
    }
}
