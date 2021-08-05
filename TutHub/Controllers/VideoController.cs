using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TutHub.DataHandlers;
using TutHub.Models;

namespace TutHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        // GET api/<CourseController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<Video>>> Get(int id, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<Video> aPIResponse = new APIResponse<Video>
            {
                Data = await videoHandler.GetVideo(id)
            };

            return aPIResponse;
        }

        // POST api/<CourseController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<APIResponse<Video>>> Post(Video video, [FromServices] IConfiguration config)
        {
            string ownerId = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                ownerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<Video> aPIResponse = new APIResponse<Video>
            {
                Data = await videoHandler.InsertVideo(video, ownerId)
            };

            return aPIResponse;
        }

        [Authorize]
        [HttpPost("insertlist")]
        public async Task<ActionResult<APIResponse<List<bool>>>> PostList(List<Video> lstvideo, [FromServices] IConfiguration config)
        {
            string ownerId = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                ownerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<List<bool>> aPIResponse = new APIResponse<List<bool>>
            {
                Data = await videoHandler.InsertListVideo(lstvideo, ownerId)
            };

            return aPIResponse;
        }


        // PUT api/<CourseController>/5
        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<APIResponse<Video>>> update(Video video, [FromServices] IConfiguration config)
        {

            string ownerId = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                ownerId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<Video> aPIResponse = new APIResponse<Video>
            {
                Data = await videoHandler.UpdateVideo(video, ownerId)
            };

            return aPIResponse;
        }

        // DELETE api/<CourseController>/5
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
            VideoHandler videoHandler = new VideoHandler(config);
            
            APIResponse<bool> aPIResponse = new APIResponse<bool>
            {
                Data = await videoHandler.DeleteVideo(id, ownerId)
            };
            return aPIResponse;
        }
    }
}
