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
    public class TestQuestionRepository : IRepository<TestQuestion>
    {
        private TestingContext db;

        public TestQuestionRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(TestQuestion item)
        {
            db.TestQuestions.Add(item);
        }

        public void Delete(Guid id)
        {
            TestQuestion testQuestion= db.TestQuestions.Find(id);
            if (testQuestion != null)
                db.TestQuestions.Remove(testQuestion);
        }

        public TestQuestion GetById(Guid id)
        {
            return db.TestQuestions.Find(id);
        }

        public IEnumerable<TestQuestion> GetList()
        {
            return db.TestQuestions;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(TestQuestion item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}