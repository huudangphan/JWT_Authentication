using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JWT.Controllers;

namespace JWT
{
    public class User
    {
        
        public  int userID { get; set; }
        public  string username { get; set; }
        public  string password { get; set; }       
        public static string token { get; set; }
        public User() { }
        
    }
}
