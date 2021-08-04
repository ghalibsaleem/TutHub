using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("{id}")]
        public async Task<Video> Get(int id, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            var video = await videoHandler.GetVideo(id);
            return video;
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<Video> Post(Video video, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            var newVideo = await videoHandler.InsertVideo(video);
            return newVideo;
        }

        [HttpPost("insertlist")]
        public async Task<List<bool>> PostList(List<Video> lstvideo, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            var newVideo = await videoHandler.InsertListVideo(lstvideo);
            return newVideo;
        }


        // PUT api/<CourseController>/5
        [HttpPost("update")]
        public async Task<Video> update(Video video, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            var newVideo = await videoHandler.UpdateVideo(video);
            return newVideo;
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id, [FromServices] IConfiguration config)
        {
            VideoHandler videoHandler = new VideoHandler(config);
            var newVideo = await videoHandler.DeleteVideo(id);
            return newVideo;
        }
    }
}
