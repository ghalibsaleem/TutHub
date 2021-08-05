using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutHub.Models;
using TutHub.Models.Authentication;

namespace TutHub.Interfaces.Authentication
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
