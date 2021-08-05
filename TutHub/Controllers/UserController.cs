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
using TutHub.Models.Authentication;

namespace TutHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        //TODO
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse<bool>>> Get([FromServices] IConfiguration config)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var user = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            return null;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<APIResponse<User>>> update(User user, [FromServices] IConfiguration config)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var username = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (username != user.usr_id)
                {
                    return new APIResponse<User>
                    {
                        Data = null,
                        Message = "Not Authorized to update other Users"
                    };
                }
            }
            UserHandler userHandler = new UserHandler(config);
            
            APIResponse<User> aPIResponse = new APIResponse<User>
            {
                Data =  await userHandler.Update(user)
            };
            return aPIResponse;
        }

        [Authorize]
        [HttpPut("updatepassword")]
        public async Task<ActionResult<APIResponse<bool>>> UpdatePassword(UserAuth userAuth, [FromServices] IConfiguration config)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var username = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (username != userAuth.username)
                {
                    return new APIResponse<bool>
                    {
                        Data = false,
                        Message = "Not Authorized to change password of other Users"
                    };
                }
            }
            UserHandler userHandler = new UserHandler(config);

            APIResponse<bool> aPIResponse = new APIResponse<bool>
            {
                Data = await userHandler.UpdatePassword(userAuth.username, userAuth.password, userAuth.newPassword)
            };
            return aPIResponse;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse<bool>>> Delete(string id, [FromServices] IConfiguration config)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var username = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (username != id)
                {
                    return new APIResponse<bool>
                    {
                        Data = false,
                        Message = "Not Authorized to delete other Users"
                    };
                }
            }
            UserHandler userHandler = new UserHandler(config);
            
            APIResponse<bool> aPIResponse = new APIResponse<bool>
            {
                Data = await userHandler.Delete(id)
            };
            return aPIResponse;
        }
    }
}
