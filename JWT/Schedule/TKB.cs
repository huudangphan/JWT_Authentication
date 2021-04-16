using Newtonsoft.Json;
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
    public partial class TKB : Form
    {
        private Session sess;
        Session Sess
        {
            get { return sess; }
            set { sess = value; }
        }
        public TKB(Session sess)
        {
            InitializeComponent();
            this.sess = sess;
            loadData();
        }
        void loadData()
        {
            string baseURL = "https://localhost:44390/api/Schedule?username=huudang&password=dang123";
            using (WebClient wc = new WebClient())
            {
                try
                {
                   
                   wc.Headers.Add("Authorization", "Bearer " + sess.token);
                    var json = wc.DownloadString(baseURL);
                    
                    var data = JsonConvert.DeserializeObject<List<ScheduleModel>>(json);
                    
                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    
                }
            }

        }
        //public void Them(string userid, string day, string thoigian, string viec)
        //{
        //    try
        //    {
        //        string strUrl = String.Format("https://localhost:44390/api/Schedule?userid=" + userid + "&day='" + day + "'&time='" + thoigian + "'&job='" + viec + "'");
        //        WebRequest request = WebRequest.Create(strUrl);
        //        request.Method = "POST";
        //        request.ContentType = "application/json";
               
        //        request.Headers.Add("Authorization", "Bearer " + sess.token);
        //        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //        {
        //            var response = request.GetResponse();
        //            using (var streamReader = new StreamReader(response.GetResponseStream()))
        //            {
        //                var result = streamReader.ReadToEnd();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //}
        public void Sua(string id, string userid, string day, string thoigian, string viec)
        {
            try
            {
                string strUrl = String.Format("https://localhost:44390/api/Schedule?id=" + id + "&userid=" + userid + "&day=" + day + "&time=" + thoigian + "&job=" + viec);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + sess.token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var reponse = request.GetResponse();
                    using (var streamReader = new StreamReader(reponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Session expies, please login again");
                this.Close();
            }
        }
        public void ThemDongTrong(string userid)
        {
            try
            {
                string strUrl = String.Format("https://localhost:44390/api/Schedule?userid=" + userid);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + sess.token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var response = request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Session expies, please login again");
                this.Close();
            }
        }
        public void Xoa(string id)
        {
            try
            {
                string strUrl = String.Format("https://localhost:44390/api/Schedule?id=" + id);
                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + sess.token);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var reponse = request.GetResponse();
                    using (var streamReader = new StreamReader(reponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Session expies, please login again");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id, day, time, job;
            string userid;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    id = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    day = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    time = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    job = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    userid = dataGridView1.Rows[i].Cells[1].Value.ToString();                    
                    Sua(id, userid, day, time, job);
                }                
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Session expies, please login again");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string userid = dataGridView1.Rows[1].Cells[1].Value.ToString();
                if (e.KeyData == Keys.Enter)
                {
                    ScheduleModel lich = new ScheduleModel();
                    lich.userId = userid;
                    lich.time = "";
                    lich.job = "";
                    lich.day = "";
                    ThemDongTrong(lich.userId);
                    loadData();
                }
                if (e.KeyData == Keys.Delete)
                {
                    string id = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                    Xoa(id);
                    loadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Session expies, please login again");
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Info f = new Info(sess);
            this.Hide();
            f.ShowDialog();
            this.Show();

        }
    }
}
