using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.DAL.Entities;

namespace Testing.DAL.Interfaces
{
    public interface IStudenRepository : IDisposable
    {
        void Create(StudentProfile item);
    }
}
