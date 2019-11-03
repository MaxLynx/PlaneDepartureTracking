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
    public partial class NotifyPlaneArrivalForm : Form
    {
        Airport model;
        public NotifyPlaneArrivalForm(Airport model, String id)
        {
            this.model = model;
            InitializeComponent();
            textBox1.Text = id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.AddNewPlane(textBox2.Text, textBox3.Text, textBox1.Text, Double.Parse(textBox4.Text));
            MessageBox.Show("New plane with ID " + textBox1.Text + " was added");
            Close();
        }

        private void NotifyPlaneArrivalForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
