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
    public class SubjectRepositories : IRepository<Subject>
    {
        private TestingContext db;

        public SubjectRepositories(TestingContext context)
        {
            this.db = context;
        }

        public void Create(Subject item)
        {
            db.Subjects.Add(item);
        }

        public void Delete(Guid id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject != null)
                db.Subjects.Remove(subject);
        }

        public Subject GetById(Guid id)
        {
            return db.Subjects.Find(id);
        }

        public IEnumerable<Subject> GetList()
        {
            return db.Subjects;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Subject item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}