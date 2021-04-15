using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;

namespace JWT.Query
{
    
    public class QueryAccount
    {
        string conStr = @"Server=172.16.8.20;Port=5432;User Id=POSMAN;Password=apzon@123;Database=Schedule";
        DataTable dt;
        private string ExcuteQuery(string query)
        {
            NpgsqlConnection conn = new NpgsqlConnection(conStr);
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            dt = new DataTable();
            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
             return JsonConvert.SerializeObject(dt);
        
        }
        public void ExcuteNonquery(string querry)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(querry);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(querry, conStr);
            using(NpgsqlConnection conn=new NpgsqlConnection(conStr))
            {
                try
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public string getAccount()
        {           
            string querry = "select * from Account";
            return ExcuteQuery(querry);
        }
        public void UpdateAccount(string username,string password)
        {
            string query= "update Account set password = '" + password + "' where username = '" + username + "'";
            ExcuteNonquery(query);
        }
               
    }
}
