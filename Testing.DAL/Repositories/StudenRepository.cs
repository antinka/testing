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
    public class StudenRepository : IRepository<StudentProfile>
    {
        private TestingContext db;

        public StudenRepository(TestingContext context)
        {
            this.db = context;
        }
        public void Create(StudentProfile item)
        {
            db.StudentProfiles.Add(item);
        }

        public void Delete(Guid id)
        {
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile != null)
                db.StudentProfiles.Remove(studentProfile);
        }

        public StudentProfile GetById(Guid id)
        {
            return db.StudentProfiles.Find(id);
        }

        public IEnumerable<StudentProfile> GetList()
        {
            return db.StudentProfiles;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(StudentProfile item)
        {
            // db.Entry(item).State = EntityState.Modified;
            db.Set<StudentProfile>().AddOrUpdate(item);
        }
    }

    //    : IStudenRepository
    //{
    //    public TestingContext Database { get; set; }
    //    public StudenRepository(TestingContext db)
    //    {
    //        Database = db;
    //    }

    //    public void Create(StudentProfile item)
    //    {
    //        Database.StudentProfiles.Add(item);
    //        Database.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        Database.Dispose();
    //    }
    //}
}
