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
    public class ExamQuestionRepository : IRepository<ExamQuestion>
    {
        private TestingContext db;

        public ExamQuestionRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(ExamQuestion item)
        {
            db.ExamQuestions.Add(item);
        }

        public void Delete(Guid id)
        {
            ExamQuestion examQuestion = db.ExamQuestions.Find(id);
            if (examQuestion != null)
                db.ExamQuestions.Remove(examQuestion);
        }

        public ExamQuestion GetById(Guid id)
        {
            return db.ExamQuestions.Find(id);
        }

        public IEnumerable<ExamQuestion> GetList()
        {
            return db.ExamQuestions;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(ExamQuestion item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}