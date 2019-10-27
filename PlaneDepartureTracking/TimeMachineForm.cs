using PlaneDepartureTracking.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneDepartureTracking
{
    public partial class TimeMachineForm : Form
    {
        Airport model;
        public TimeMachineForm(Airport model)
        {
            this.model = model;
            InitializeComponent();
            textBox4.Text = model.SystemTime.ToString();
            textBox1.Text = model.SystemTime.ToString();
        }

        private void TimeMachineForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.SystemTime = DateTime.Parse(textBox1.Text);
            MessageBox.Show("Date and time were updated");
            Close();
        }
    }
}
