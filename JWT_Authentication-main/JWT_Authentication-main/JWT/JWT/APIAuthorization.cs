using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JWT
{
    [AttributeUsage(AttributeTargets.Class)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class APIAuthorization: Attribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {           

            if(User.token!=null)
            {
                return;
            }
            else
            {
                
                filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                filterContext.Result = new JsonResult("NotAuthorized")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = "Invalid Token"
                    },
                };
            }
           

        }
      
    
    }
}
