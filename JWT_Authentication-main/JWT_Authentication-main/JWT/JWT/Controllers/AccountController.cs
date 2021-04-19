using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JWT.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        QueryAccount acc = new QueryAccount();
        [HttpGet]
        public string getAccount()
        {
            return acc.getAccount();
        }
        [HttpPut]
        public void UpdateAccount(string username,string password)
        {
            acc.UpdateAccount(username, password);
        }
       
    }
}
