using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models.Authentication
{
    public class UserReg
    {
        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string f_name { get; set; }

        public string l_name { get; set; }
    }
}
