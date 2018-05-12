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

        public StudentsController(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<StudentsController>();
        }

        [HttpGet]
        [Route("GetStudents")]
        public IActionResult GetStudents()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){Id=1,Name = "NahiYan1",Phone = "8801622595291"},
                new Student(){Id=2,Name = "NahiYan2",Phone = "8801622595292"},
                new Student(){Id=3,Name = "NahiYan3",Phone = "8801622595293"},
            };
            return Ok(students);
        }

        [HttpPost]
        [Route("SaveStudent")]
        public IActionResult SaveStudent([FromBody] Student student)
        {
            student.Id = 4;
            return Ok(student);

        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent(string id)
        {
            return Ok(id);
        }
    }
}