using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<Course>>> Get(int id,[FromServices] IConfiguration config)
        {
            
            CourseHandler courseHandler = new CourseHandler(config);
            APIResponse<Course> aPIResponse = new APIResponse<Course>
            {
                Data = await courseHandler.GetCourse(id)
            };
            
            return aPIResponse;
        }

        [Authorize]
        [HttpGet("/Course/{id}/Video")]
        public async Task<ActionResult<APIResponse<List<Video>>>> GetList(int id, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<List<Video>> aPIResponse = new APIResponse<List<Video>>
            {
                Data = await videoHandler.GetVideoList(id)
            };
            return aPIResponse;
        }

        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<APIResponse<Course>>> Post(Course course ,[FromServices] IConfiguration config)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                course.OwnerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            CourseHandler courseHandler = new CourseHandler(config);
            
            APIResponse<Course> aPIResponse = new APIResponse<Course>
            {
                Data = await courseHandler.InsertCourse(course)
            };
            return aPIResponse;
        }

        
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<APIResponse<Course>>> Update(Course course, [FromServices] IConfiguration config)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                course.OwnerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            CourseHandler courseHandler = new CourseHandler(config);
            APIResponse<Course> aPIResponse = new APIResponse<Course>
            {
                Data = await courseHandler.UpdateCourse(course)
            };
            return aPIResponse;
        }

        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse<bool>>> Delete(int id, [FromServices] IConfiguration config)
        {
            string ownerId = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                ownerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            CourseHandler courseHandler = new CourseHandler(config);
            
            APIResponse<bool> aPIResponse = new APIResponse<bool>
            {
                Data = await courseHandler.DeleteCourse(id, ownerId)
            };
            return aPIResponse;
        }
    }
}
