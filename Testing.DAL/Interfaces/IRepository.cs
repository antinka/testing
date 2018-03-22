using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //Main methods for work with db.
        IEnumerable<T> GetList();
        T GetById(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}