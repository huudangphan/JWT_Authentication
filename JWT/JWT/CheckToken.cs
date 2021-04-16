using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace JWT
{
    public static class CheckToken
    {
        
        public static bool check()
        {
            
            if (User.token.Equals(JwtBearerDefaults.AuthenticationScheme))
                return true;
            return false;
            

        }
    }
}
