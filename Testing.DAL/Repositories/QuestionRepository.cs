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
    public class QuestionRepository :  IRepository<Question>
    {
        private TestingContext db;

        public QuestionRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(Question item)
        {
            db.Questions.Add(item);
        }

        public void Delete(Guid id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
                db.Questions.Remove(question);
        }

        public Question GetById(Guid id)
        {
            return db.Questions.Find(id);
        }

        public IEnumerable<Question> GetList()
        {
            return db.Questions;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Question item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}