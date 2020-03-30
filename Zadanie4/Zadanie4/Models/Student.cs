using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie4.Models
{
    public class Student : IComparable
    {
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public int IdEnrollment { get; set; }
        public String BirthDate { get; set; }

        public Student() { }

        public Student(int idStudent, string firstName, string lastName)
        {
            this.IdStudent = idStudent;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int CompareTo(object obj)
        {
            int index = (int)obj;
            return this.IdStudent - index;
        }
    }
}
