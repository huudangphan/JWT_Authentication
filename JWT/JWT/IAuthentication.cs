using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT
{
    public interface IAuthenticateService
    {
        User Authenticate(string username, string password);
    }
}
