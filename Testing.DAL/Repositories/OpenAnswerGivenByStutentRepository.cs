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
    public class OpenAnswerGivenByStutentRepository : IRepository<OpenAnswersGivenByStutent>
    {
        private TestingContext db;

        public OpenAnswerGivenByStutentRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(OpenAnswersGivenByStutent item)
        {
            db.OpenAnswerGivenByStutents.Add(item);
        }

        public void Delete(Guid id)
        {
            OpenAnswersGivenByStutent examOpenAnswer = db.OpenAnswerGivenByStutents.Find(id);
            if (examOpenAnswer != null)
                db.OpenAnswerGivenByStutents.Remove(examOpenAnswer);
        }

        public OpenAnswersGivenByStutent GetById(Guid id)
        {
            return db.OpenAnswerGivenByStutents.Find(id);
        }

        public IEnumerable<OpenAnswersGivenByStutent> GetList()
        {
            return db.OpenAnswerGivenByStutents;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(OpenAnswersGivenByStutent item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}