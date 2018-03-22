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
    public class TestRepositories : IRepository<Test>
    {
        private TestingContext db;

        public TestRepositories(TestingContext context)
        {
            this.db = context;
        }
        public void Create(Test item)
        {
            db.Tests.Add(item);
        }

        public void Delete(Guid id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
                db.Tests.Remove(test);
        }

        public Test GetById(Guid id)
        {
            return db.Tests.Find(id);
        }

        public IEnumerable<Test> GetList()
        {
            return db.Tests;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Test item)
        {
            //db.Entry(item).State = EntityState.Detached;
            //db.SaveChanges();
             //db.Entry(item).State = EntityState.Modified;
            //db.SaveChanges();
            db.Set<Test>().AddOrUpdate(item);
        }
    }
}