using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models.Authentication
{
    public class UserToken
    {
        public string username { get; set; }

        public User user { get; set; }

        public string token { get; set; }
    }
}
