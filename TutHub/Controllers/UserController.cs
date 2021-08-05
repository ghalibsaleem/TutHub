using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {

        //TODO
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<bool>> Get(string id, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var result = await userHandler.Delete(id);
            return result;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<User>> update(User user, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.Update(user);

            return validUser;
        }

        [Authorize]
        [HttpPut("updatepassword")]
        public async Task<bool> UpdatePassword(string username, string oldpassword, string newpassword, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.UpdatePassword(username, oldpassword, newpassword);

            return validUser;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var result = await userHandler.Delete(id);
            return result;
        }
    }
}
