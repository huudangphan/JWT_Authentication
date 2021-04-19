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
    
    public class APIAuthorization: Attribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {      
            

            if(Startup.listToken.Count!=0&&check())
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
        private bool check()
        {
            for (int i = 0; i < User.token.Count; i++)
            {
                for (int j = 0; j < Startup.listToken.Count; j++)
                {
                    if(User.token[i]==Startup.listToken[j])
                    {
                        return true;
                        break;
                    }    
                }
            }
            return false;
        }
      
    
    }
}
