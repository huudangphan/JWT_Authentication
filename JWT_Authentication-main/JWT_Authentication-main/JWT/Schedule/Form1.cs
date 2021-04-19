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
        Session s = new Session();
        public void Login(string username,string password)
        {
           
          
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
                        s.id = data["userID"].Value<int>();
                        s.username = data["username"].Value<string>();
                        s.password = data["password"].Value<string>();

                        //MessageBox.Show(s.token);                  

                    }
                }


                //string Urlbase2 = "https://localhost:44390/Authentication";
                //string url2 = string.Format(Urlbase2);
                //WebRequest request2 = WebRequest.Create(url2);
                ////request2.ContentType = "application/json";
                //request2.Method = "GET";
                //using (var streamWriter2 = new StreamWriter(request2.GetRequestStream()))
                //{

                //    var response2 = request2.GetResponse();
                //    using (var streamReader2 = new StreamReader(response2.GetResponseStream()))
                //    {
                //        var result2 = streamReader2.ReadToEnd();
                //        var data2 = (JObject)JsonConvert.DeserializeObject(result2);
                //        s.token = data2.ToString();

                //        if (data2 != null)
                //        {
                //            this.Hide();
                //            TKB f = new TKB(s);
                //            f.ShowDialog();
                //            this.Show();
                //        }
                //    }
                //}


            }
            catch (Exception ex)
            {

                MessageBox.Show("Username or Password invalid");
            }
            
            
        }
        private void getToken()
        {
            try
            {
                
                string Urlbase = "https://localhost:44390/Authentication";
                               
                string url = string.Format(Urlbase);
                WebRequest request = WebRequest.Create(url);
                //request.Method = "GET";


                WebResponse response = request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(dataStream);

                    string responseFromServer = reader.ReadToEnd();
                    if (responseFromServer != "[]")
                    {

                        TKB f = new TKB(s);
                        this.Hide();
                        f.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("username or password invalid");
                    }

                }
                response.Close();


                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;
            //Login(username, password);
            getToken();

        }
    }
}
