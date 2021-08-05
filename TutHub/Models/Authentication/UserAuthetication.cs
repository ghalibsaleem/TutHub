using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models.Authentication
{
    public class UserAuthetication
    {
        public string usr_id { get; set; }

        public byte[] password { get; set; }

        public byte[] passwordSalt { get; set; }

    }
}
