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
   public class StudentTestResultRepository : IRepository<StudentTestResult>
    {
        private TestingContext db;

        public StudentTestResultRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(StudentTestResult item)
        {
            db.StudentTestResults.Add(item);
        }

        public void Delete(Guid id)
        {
            StudentTestResult studentResult = db.StudentTestResults.Find(id);
            if (studentResult != null)
                db.StudentTestResults.Remove(studentResult);
        }

        public StudentTestResult GetById(Guid id)
        {
            return db.StudentTestResults.Find(id);
        }

        public IEnumerable<StudentTestResult> GetList()
        {
            return db.StudentTestResults;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(StudentTestResult item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<StudentTestResult>().AddOrUpdate(item);
        }
    }
}