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
    public partial class NotifyPlaneDepartureForm : Form
    {
        Airport model;
        public NotifyPlaneDepartureForm(Airport model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (model.NotifyDeparture(textBox4.Text))
            {
                MessageBox.Show("Plane with id " + textBox4.Text + " successfully departured");
            }
            else
            {
                MessageBox.Show("Plane with id " + textBox4.Text + " was not found on any track");
            }
        }

        private void NotifyPlaneDepartureForm_Load(object sender, EventArgs e)
        {

        }
    }
}
