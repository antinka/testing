using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories
{
    public class ExamRepository : IRepository<Exam>
    {
        private TestingContext db;

        public ExamRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(Exam item)
        {
            db.Exams.Add(item);
        }

        public void Delete(Guid id)
        {
            Exam exam = db.Exams.Find(id);
            if (exam != null)
                db.Exams.Remove(exam);
        }

        public Exam GetById(Guid id)
        {
            return db.Exams.Find(id);
        }

        public IEnumerable<Exam> GetList()
        {
            return db.Exams;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Exam item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
