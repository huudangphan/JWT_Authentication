using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    wc.Headers.Add("Authorization", "Bearer Token "+sess.token);
                    var json = wc.DownloadString(baseURL);
                    var data = JsonConvert.DeserializeObject<List<ScheduleModel>>(json);
                    dataGridView1.DataSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
    }
}
