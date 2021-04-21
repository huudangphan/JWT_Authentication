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
using Npgsql;
using System.Data;
using Newtonsoft.Json;

namespace JWT
{
    public  class AuthenticateService : IAuthenticateService
    {
        string conStr = @"Server=172.16.8.20;Port=5432;User Id=POSMAN;Password=apzon@123;Database=Schedule";
        private static string tokenStr;
        DataTable dt;
        // get username
        private  string ExcuteQuery(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(conStr);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            dt = new DataTable();
            string result = "";
            try
            {
                conn.Open();
                adapter.Fill(dt);
                NpgsqlDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {
                    result = dRead["username"].ToString();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }
        // get password
        private  string ExcuteQueryp(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(conStr);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            dt = new DataTable();
            string result = "";
            try
            {
                conn.Open();
                adapter.Fill(dt);
                NpgsqlDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {
                    result = dRead["password"].ToString();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }
        private  int ExcuteQueryI(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(conStr);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            dt = new DataTable();
            int result = -1;
            try
            {
                conn.Open();
                adapter.Fill(dt);
                NpgsqlDataReader dRead = cmd.ExecuteReader();
                while (dRead.Read())
                {
                    result = Int32.Parse(dRead["id"].ToString());
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return result;

        }

        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appsttings)
        {
            _appSettings = appsttings.Value;
        }        
        public  User Authenticate(string username, string password)
        {
            // lay username va password tu database
            string queryu = "select username from Account acc where acc.username='" + username + "'and password='" + password + "'";
            string queryp = "select password from Account acc where acc.username='" + username + "'and password='" + password + "'";
            string queryi = "select id from Account acc where acc.username='" + username + "'and password='" + password + "'";
            int id = ExcuteQueryI(queryi);
            string us = ExcuteQuery(queryu);
            string ps = ExcuteQueryp(queryp);
            // add user vao listuser
             List<User>  ListUser = new List<User>() 
            { 
                new User{userID=id,username=us,password=ps}
            };
            // tao token
            var user = ListUser.SingleOrDefault(x => x.username == username && x.password == password);
            if (user == null)
                return null;
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF32.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                 {   new Claim("username",user.username.ToString()),                  
                     new Claim(ClaimTypes.Role,"Admin"),
                     new Claim(ClaimTypes.Version,"V3.1")
                 }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            user.password = "";
            var token = tokenHandle.CreateToken(tokenDescriptor);
            string tokenRes = tokenHandle.WriteToken(token);
            User.token = tokenRes;
            Startup.listToken.Add(tokenRes);
            return user;
        }

        public List<string> Token()
        {
            return Startup.listToken;
        }
    }
}
