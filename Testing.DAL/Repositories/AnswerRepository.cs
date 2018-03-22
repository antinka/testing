using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories
{
    class AnswerRepository : IRepository<Answer>
    {
        private TestingContext db;

        public AnswerRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(Answer item)
        {
            db.Answers.Add(item);
        }

        public void Delete(Guid id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer != null)
                db.Answers.Remove(answer);
        }

        public Answer GetById(Guid id)
        {
            return db.Answers.Find(id);
        }

        public IEnumerable<Answer> GetList()
        {
            return db.Answers;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Answer item)
        {
           // db.Entry(item).State = EntityState.Modified;
            db.Set<Answer>().AddOrUpdate(item);
        }
    }
}