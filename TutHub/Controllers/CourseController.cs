using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutHub.DataHandlers;
using TutHub.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {  
        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<Course> Get(int id,[FromServices] IConfiguration config)
        {
            CourseHandler courseHandler = new CourseHandler(config);
            var course = await courseHandler.GetCourse(id);
            return course;
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<Course> Post(Course course ,[FromServices] IConfiguration config)
        {
            CourseHandler courseHandler = new CourseHandler(config);
            var newCourse =  await courseHandler.InsertCourse(course);
            return newCourse;
        }

        // PUT api/<CourseController>/5
        [HttpPost("/update")]
        public async Task<Course> Put(Course course, [FromServices] IConfiguration config)
        {
            CourseHandler courseHandler = new CourseHandler(config);
            var result = await courseHandler.UpdateCourse(course);

            return result;
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id, [FromServices] IConfiguration config)
        {
            CourseHandler courseHandler = new CourseHandler(config);
            var result = await courseHandler.DeleteCourse(id);
            return result;
        }
    }
}
