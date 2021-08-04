﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutHub.DataHandlers;
using TutHub.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutHub.Controllers.Authentication
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("/login")]
        public async Task<User> login(string username, string password, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var user = await userHandler.LogIn(username, password);
            return user;
        }

        [HttpPost("/signup")]
        public async Task<User> signup(User user, [FromServices] IConfiguration config)
        {
            UserHandler userHandler = new UserHandler(config);
            var validUser = await userHandler.SignUp(user);

            return validUser;
        }
    }
}