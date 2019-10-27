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
    public partial class NotifyPlaneArrivalIDForm : Form
    {
        Airport model;
        public NotifyPlaneArrivalIDForm(Airport model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (model.NotifyArrival(textBox4.Text))
            {
                MessageBox.Show("Successfull notification!");
            }
            else
            {
                new NotifyPlaneArrivalForm(model, textBox4.Text).Show();
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
