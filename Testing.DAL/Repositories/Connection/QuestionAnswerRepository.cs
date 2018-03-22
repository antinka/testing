using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Entities.Connection;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories.Connection
{
    public class QuestionAnswerRepository : IRepository<QuestionAnswer>
    {
        private TestingContext db;

        public QuestionAnswerRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(QuestionAnswer item)
        {
            db.QuestionAnswers.Add(item);
        }

        public void Delete(Guid id)
        {
            QuestionAnswer questionAnswer = db.QuestionAnswers.Find(id);
            if (questionAnswer != null)
                db.QuestionAnswers.Remove(questionAnswer);
        }

        public QuestionAnswer GetById(Guid id)
        {
            return db.QuestionAnswers.Find(id);
        }

        public IEnumerable<QuestionAnswer> GetList()
        {
            return db.QuestionAnswers;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(QuestionAnswer item)
        {
           // db.Entry(item).State = EntityState.Modified;
            db.Set<QuestionAnswer>().AddOrUpdate(item);
        }
    }
}
