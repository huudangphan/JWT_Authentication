using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Login(string username,string password)
        {
           
            Session s = new Session();
            try
            {
                string Urlbase = "https://localhost:44390/Authentication";
                UserModel user = new UserModel();
                user.username = username;
                user.password = password;
                string postData = JsonConvert.SerializeObject(user);
                string url = string.Format(Urlbase);
                WebRequest request = WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "POST";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                    streamWriter.Flush();
                    streamWriter.Close();
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var data = (JObject)JsonConvert.DeserializeObject(result);
                        s.id= data["userID"].Value<int>();
                        s.username = data["username"].Value<string>();
                        s.password = data["password"].Value<string>();
                        s.token= data["token"].Value<string>();
                        //MessageBox.Show(s.token);
                        if (s.token != null)
                        {
                            this.Hide();
                            TKB f = new TKB(s);
                            f.ShowDialog();
                            this.Show();
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Username or Password imvalid");
            }
            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            Login(username, password);

        }
    }
}
