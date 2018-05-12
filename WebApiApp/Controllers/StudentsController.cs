using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private  ILogger _logger;
        private AppDbContext _context;


        public StudentsController(ILoggerFactory factory,AppDbContext db)
        {
            _logger = factory.CreateLogger<StudentsController>();
            _context = db;
        }

        [HttpGet]
        [Route("GetStudents")]
        public IActionResult GetStudents(int skip=0,int take=10,string orderByname = "Name",bool isAscending=true)
        {
            IQueryable<Student> dbstudent = _context.Students.AsQueryable();
            if (orderByname == "Name")
            {
                dbstudent = isAscending ? dbstudent.OrderBy(x => x.Name).AsQueryable() : dbstudent.OrderByDescending(x => x.Name).AsQueryable();
            }
            if (orderByname == "Phone")
            {
                dbstudent = isAscending ? dbstudent.OrderBy(x => x.Phone).AsQueryable() : dbstudent.OrderByDescending(x => x.Phone).AsQueryable();
            }
            dbstudent = dbstudent.Skip(skip).Take(take).AsQueryable();
           // var students = _context.Students.ToList();
            return Ok(dbstudent);
        } 

        [HttpPost]
        [Route("SaveStudent")]
        public IActionResult SaveStudent([FromBody] Student student)
        {
            //model validation
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);

        }

        [HttpPut]
        [Route("EditStudent")]
        public IActionResult EditStudent([FromBody] Student student)
        {
            var update = _context.Set<Student>().Update(student);
            _context.SaveChanges();
            //var find = _context.Students.Find(student.Id);
            //if (find == null)
            //{
            //    return NotFound("Student Not Found");
            //}

            //find.Name = student.Name;
            //find.Phone = student.Phone;
            _context.SaveChanges();
            return Ok(true);
        }
        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            Student student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound("Object not found");
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
      
            return Ok(id);
        }
    }
}