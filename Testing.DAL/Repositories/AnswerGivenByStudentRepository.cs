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
    public class AnswerGivenByStudentRepository : IRepository<AnswerGivenByStudent>
    {
        private TestingContext db;

        public AnswerGivenByStudentRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(AnswerGivenByStudent item)
        {
            db.AnswerGivenByStudents.Add(item);
        }

        public void Delete(Guid id)
        {
            AnswerGivenByStudent answerGivenByStudent = db.AnswerGivenByStudents.Find(id);
            if (answerGivenByStudent != null)
                db.AnswerGivenByStudents.Remove(answerGivenByStudent);
        }

        public AnswerGivenByStudent GetById(Guid id)
        {
            return db.AnswerGivenByStudents.Find(id);
        }

        public IEnumerable<AnswerGivenByStudent> GetList()
        {
            return db.AnswerGivenByStudents;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(AnswerGivenByStudent item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<AnswerGivenByStudent>().AddOrUpdate(item);
        }
    }
}