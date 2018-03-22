using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories.Connection
{
    public class SubjectExamRepository : IRepository<SubjectExam>
    {
        private TestingContext db;

        public SubjectExamRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(SubjectExam item)
        {
            db.SubjectExams.Add(item);
        }

        public void Delete(Guid id)
        {
            SubjectExam subjectExam = db.SubjectExams.Find(id);
            if (subjectExam != null)
                db.SubjectExams.Remove(subjectExam);
        }

        public SubjectExam GetById(Guid id)
        {
            return db.SubjectExams.Find(id);
        }

        public IEnumerable<SubjectExam> GetList()
        {
            return db.SubjectExams;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(SubjectExam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}