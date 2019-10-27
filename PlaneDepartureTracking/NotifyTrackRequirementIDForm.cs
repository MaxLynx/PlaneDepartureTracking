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
    public partial class NotifyTrackRequirementIDForm : Form
    {
        Airport model;
        public NotifyTrackRequirementIDForm(Airport model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (model.NotifyTrackRequirement(textBox4.Text))
            {
                MessageBox.Show("Successfull notification!");
            }
            else
            {
                MessageBox.Show("Plane with this ID has not arrived yet");
            }
        }
    }
}
