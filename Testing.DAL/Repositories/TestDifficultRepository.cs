using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.EF;
using Testing.DAL.Interfaces;

namespace Testing.DAL.Repositories
{
    public class TestDifficultRepository : IRepository<TestDifficult>
    {
        private TestingContext db;

        public TestDifficultRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(TestDifficult item)
        {
            db.TestDifficults.Add(item);
        }

        public void Delete(Guid id)
        {
            TestDifficult testDifficult = db.TestDifficults.Find(id);
            if (testDifficult != null)
                db.TestDifficults.Remove(testDifficult);
        }

        public TestDifficult GetById(Guid id)
        {
            return db.TestDifficults.Find(id);
        }

        public IEnumerable<TestDifficult> GetList()
        {
            return db.TestDifficults;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(TestDifficult item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<TestDifficult>().AddOrUpdate(item);
        }
    }
}