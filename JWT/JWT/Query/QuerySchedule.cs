using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Query
{
    public class QuerySchedule
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
            using (NpgsqlConnection conn = new NpgsqlConnection(conStr))
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
        public string GetSchedule(string username,string password)
        {
            string query = "select * from schedule , Account  where username ='" + username + "'and password = '" + password + "'";
            return ExcuteQuery(query);
        }
        public void AddSchedule(int userid, string day, string time, string job)
        {
            string query = "insert into schedule(userID,day,time,job) values(" + userid + ",'" + day + "','" + time + "','" + job + "')";
            ExcuteNonquery(query);
        }
        public void UpdateSchedule(int id, int userID, string day, string time, string job)
        {
            string query = "update Schedule set day = '" + day + "', time = '" + time + "', job = '" + job + "' where userID = " + userID + " and id = " + id + "";
            ExcuteNonquery(query);
        }
        public void DeleteSchedule(int id)
        {
            string query = "delete from Schedule where id = " + id + "";
            ExcuteNonquery(query);
        }
    }
}
