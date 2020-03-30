using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie4.Models;

namespace Zadanie4.DAL
{
    public interface IDBService
    {
        public ICollection<Student> GetStudents();
    }
}
