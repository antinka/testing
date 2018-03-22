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
    public class CommentToExamResultRepository : IRepository<CommentToExamResult>
    {
        private TestingContext db;

        public CommentToExamResultRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(CommentToExamResult item)
        {
            db.CommentToExamResults.Add(item);
        }

        public void Delete(Guid id)
        {
            CommentToExamResult commentToExamResult = db.CommentToExamResults.Find(id);
            if (commentToExamResult != null)
                db.CommentToExamResults.Remove(commentToExamResult);
        }

        public CommentToExamResult GetById(Guid id)
        {
            return db.CommentToExamResults.Find(id);
        }

        public IEnumerable<CommentToExamResult> GetList()
        {
            return db.CommentToExamResults;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(CommentToExamResult item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<CommentToExamResult>().AddOrUpdate(item);
        }
    }
}