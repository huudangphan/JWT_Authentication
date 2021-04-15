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
        public TKB()
        {
            InitializeComponent();
            loadData();
        }
        void loadData()
        {
            string baseURL = "https://localhost:44390/api/Schedule?username=huudang&password=dang123";
            using (WebClient wc = new WebClient())
            {

                try
                {
                    wc.Headers.Add("Authorization", "Bearer Token eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMSIsInJvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvdmVyc2lvbiI6IlYzLjEiLCJuYmYiOjE2MTg0NjE2OTMsImV4cCI6MTYxODQ2MTc1MywiaWF0IjoxNjE4NDYxNjkzfQ.c3mFIGGf7GiBQVzt4FXRuQu0GtiaruE3Cz2cFswWliU");
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
