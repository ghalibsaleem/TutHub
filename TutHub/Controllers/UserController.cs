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
        [HttpGet("{id}")]
        public async Task<bool> Get(string id, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var result = await userHandler.Delete(id);
            return result;
        }

        [HttpPut("update")]
        public async Task<User> update(User user, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.Update(user);

            return validUser;
        }


        [HttpPut("updatepassword")]
        public async Task<bool> UpdatePassword(string username, string oldpassword, string newpassword, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.UpdatePassword(username, oldpassword, newpassword);

            return validUser;
        }


        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var result = await userHandler.Delete(id);
            return result;
        }
    }
}
