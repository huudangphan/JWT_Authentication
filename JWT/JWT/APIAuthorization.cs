using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JWT
{
    [AttributeUsage(AttributeTargets.Class)]
    
    public class APIAuthorization: Attribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {      
            

            if(/*Startup.listToken.Count!=0&&*/ValidateJwtToken(User.token)!=null)
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
        public string ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes("MyAPIKey");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                
                var username = jwtToken.Claims.First(a => a.Type == "username").Value;

                // return account id from JWT token if validation successful
                return username;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }


    }
}
