using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace NASA_Astronomy_Picture_Of_The_Day
{
    public partial class NASAAPOD : Form
    {
        public NASAAPOD()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadYesterdayAPOD();
            LoadTodayAPOD();
        }

        private async void LoadPreviousAPOD(DateTime date)
        {
            using(HttpClient client = new HttpClient())
            {
                using(HttpResponseMessage response = await client.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&date="+date.ToString("yyyy-MM-dd")))
                {
                    using(HttpContent content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        var imageUrl = (string)json["url"];
                        pictureBoxToday.Load(imageUrl);
                        dateToolStripMenuItem.Text = date.ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        private async void LoadTodayAPOD()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY"))
                {
                    using (HttpContent content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();
                        JObject json = JObject.Parse(result);
                        var imageUrl = (string)json["url"];
                        pictureBoxToday.Load(imageUrl);
                        pictureBoxToday.Height = pictureBoxToday.Image.Height;
                        pictureBoxToday.Width = pictureBoxToday.Image.Width;
                        dateToolStripMenuItem.Text = DateTime.Today.ToString("yyyy-MM-dd");
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void todayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTodayAPOD();
        }

        private void yesterdayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadPreviousAPOD(DateTime.Today.AddDays(-1.0));
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatePicker date = new DatePicker();
            date.ShowDialog();
            var newDate = date.newDate;
            LoadPreviousAPOD(newDate);
        }
    }
}