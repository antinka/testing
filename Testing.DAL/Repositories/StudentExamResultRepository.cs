using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories
{
      public class StudentExamResultRepository : IRepository<StudentExamResult>
    {
        private TestingContext db;

        public StudentExamResultRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(StudentExamResult item)
        {
            db.StudentExamResults.Add(item);
        }

        public void Delete(Guid id)
        {
            StudentExamResult studentResult = db.StudentExamResults.Find(id);
            if (studentResult != null)
                db.StudentExamResults.Remove(studentResult);
        }

        public StudentExamResult GetById(Guid id)
        {
            return db.StudentExamResults.Find(id);
        }

        public IEnumerable<StudentExamResult> GetList()
        {
            return db.StudentExamResults;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(StudentExamResult item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<StudentExamResult>().AddOrUpdate(item);
        }
    }
}