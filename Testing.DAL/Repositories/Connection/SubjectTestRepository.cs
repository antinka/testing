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
    public class SubjectTestRepository : IRepository<SubjectTest>
    {
        private TestingContext db;

        public SubjectTestRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(SubjectTest item)
        {
            db.SubjectTests.Add(item);
        }

        public void Delete(Guid id)
        {
            SubjectTest subjectTest= db.SubjectTests.Find(id);
            if (subjectTest != null)
                db.SubjectTests.Remove(subjectTest);
        }

        public SubjectTest GetById(Guid id)
        {
            return db.SubjectTests.Find(id);
        }

        public IEnumerable<SubjectTest> GetList()
        {
            return db.SubjectTests;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(SubjectTest item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}