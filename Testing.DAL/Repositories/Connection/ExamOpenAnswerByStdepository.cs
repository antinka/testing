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
    public class ExamOpenAnswerByStdepository : IRepository<ExamOpenAnswerByStd>
    {
        private TestingContext db;

        public ExamOpenAnswerByStdepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(ExamOpenAnswerByStd item)
        {
            db.ExamOpenAnswerByStds.Add(item);
        }

        public void Delete(Guid id)
        {
            ExamOpenAnswerByStd examOpenAnswer = db.ExamOpenAnswerByStds.Find(id);
            if (examOpenAnswer != null)
                db.ExamOpenAnswerByStds.Remove(examOpenAnswer);
        }

        public ExamOpenAnswerByStd GetById(Guid id)
        {
            return db.ExamOpenAnswerByStds.Find(id);
        }

        public IEnumerable<ExamOpenAnswerByStd> GetList()
        {
            return db.ExamOpenAnswerByStds;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(ExamOpenAnswerByStd item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}