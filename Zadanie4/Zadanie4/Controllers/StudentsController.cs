using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Zadanie4.Models;
using Zadanie4.DAL;

namespace Zadanie4.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDBService _dbService;

        public StudentsController(IDBService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Metoda do zwracania listy studentów z bazy danych - s19234
        /// </summary>
        /// <param name="orderBy">string do sortowania rosnąca lub malejąco</param>
        /// <returns></returns>
        /// <exception cref="FormatException">Kiedy parametr orderBy jest źle sforłumowany</exception>
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            List<Student> list = new List<Student>();
            if (!orderBy.Equals("desc") || !orderBy.Equals("asc"))
                throw new FormatException("Query string built wrong");

            string querry = "SELECT * FROM Student ORDER BY Student.IndexNumber " + orderBy;
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19234;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = querry;

                connection.Open();

                var dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    var st = new Student();
                    st.IdEnrollment = Int32.Parse(dataReader["IdEnrollment"].ToString());
                    st.IndexNumber = (dataReader["IndexNumber"].ToString());
                    st.FirstName = dataReader["FirstName"].ToString();
                    st.LastName = dataReader["LastName"].ToString();
                    st.BirthDate = dataReader["BirthDate"].ToString();

                    list.Add(st);
                }
            }

            return Ok(list);
        }

        /// <summary>
        /// Metoda do zwracania studenta
        /// </summary>
        /// <param name="id">Id studenta</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
                return Ok("Kowalski");
            else if (id == 2)
                return Ok("Malewski");
            else if (id == 3)
                return Ok("Andrzejewski");
            return NotFound("Student not found");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            _dbService.GetStudents().Add(student);
            return Ok(student);
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int index)
        {
            foreach(Student student in _dbService.GetStudents())
            {
                if (student.CompareTo(index) == 0)
                {
                    _dbService.GetStudents().Remove(student);
                    return Ok("Student removed");
                }
            }
            return NotFound("Student not found");
        }

        [HttpPut]
        public IActionResult ActStudent(int index)
        {
            foreach(Student student in _dbService.GetStudents())
            {
                if(student.CompareTo(index) == 0)
                {
                    return Ok("Student act");
                }
            }
            return NotFound("Student not found");
        }
    }
}