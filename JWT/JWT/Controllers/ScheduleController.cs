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
    [APIAuthorization]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        QuerySchedule sche = new QuerySchedule();
        [HttpGet]
        public string GetSchedule(string username,string password)
        {
            return sche.GetSchedule(username,password);
        }
        [HttpPost]
        public void AddSchedule(int userid,string day,string time,string job)
        {
            sche.AddSchedule(userid, day, time, job);
        }
        [HttpPut]
        public void UpdateSchedule(int id,int userId,string day,string time,string job)
        {
            sche.UpdateSchedule(id, userId, day, time, job);
        }
        [HttpDelete]
        public void DeleteSchedule(int id)
        {
            sche.DeleteSchedule(id);
        }
    }
}
