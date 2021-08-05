using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TutHub.DataHandlers;
using TutHub.Interfaces.Authentication;
using TutHub.Models;
using TutHub.Models.Authentication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutHub.Controllers.Authentication
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost("/login")]
        public async Task<ActionResult<UserToken>> login(UserAuth userAuth, [FromServices] IConfiguration config)
        {
            var s = HttpContext.Request.Path;
            UserHandler userHandler = new UserHandler(config);
            var user = await userHandler.LogIn(userAuth);
            return new UserToken
            {
                username = user.usr_id,
                user = user,
                token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("/signup")]
        public async Task<ActionResult<User>> signup(UserReg user, [FromServices] IConfiguration config)
        {
            UserAuthetication authetication = new UserAuthetication();
            using (var hmac = new HMACSHA512())
            {
               
                authetication.usr_id = user.username;
                authetication.password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.password));
                authetication.passwordSalt = hmac.Key;
            }
            User user1 = new User()
            {
                usr_id = user.username,
                f_name = user.f_name,
                l_name = user.l_name,
                email = user.email,
                dateJoined = DateTime.Now.Date
            };
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.SignUp(authetication,user1);

            return validUser;
        }
    }
}
