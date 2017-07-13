using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NASA_Astronomy_Picture_Of_The_Day
{

    public partial class DatePicker : Form
    {
        public DateTime newDate = new DateTime();

        public DatePicker()
        {
            InitializeComponent();
        }

        private void DatePicker_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Width = 260;
            dateTimePicker1.Height = 22;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            newDate = dateTimePicker1.Value;
            if(dateTimePicker1.Value != DateTime.Today)
            {
                this.Close();
            }
        }
    }
}
