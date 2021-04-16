using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace JWT
{
    public  class CheckToken
    {
        private  Startup _startup;
        public  CheckToken(Startup startup)

        {
            _startup = startup;
        }
        public static  bool check()
        { 
            if (User.token == JwtBearerDefaults.AuthenticationScheme)
                return false;
            else
                return true;
            
        }
    }
}
 