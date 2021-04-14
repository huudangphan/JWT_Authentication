using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace JWT
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appsttings  )
        {
            _appSettings = appsttings.Value;
        }

        private List<User> ListUser = new List<User>()
        {
            new User{userID=1, username="dang",password="dang123"}
        };
        public User Authenticate(string username, string password)
        {
            var user = ListUser.SingleOrDefault(x => x.username == username && x.password == password);
            if (user == null)
                return null;
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                 {
                     new Claim(ClaimTypes.Name,user.userID.ToString()),
                     new Claim(ClaimTypes.Role,"Admin"),
                     new Claim(ClaimTypes.Version,"V3.1")
                 }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandle.CreateToken(tokenDescriptor);
            user.token = tokenHandle.WriteToken(token);
            return user;
        }
    }
}
