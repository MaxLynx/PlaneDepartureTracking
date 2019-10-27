using PlaneDepartureTracking.Model;
using PlaneDepartureTracking.Utils;
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
    public partial class Form1 : Form
    {
        Airport model;
        public Form1()
        {
            model = new Airport();
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new NotifyPlaneArrivalIDForm(model).Show();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            new NotifyTrackRequirementIDForm(model).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new ListAllWaitingPlanesByID(model).Show();
        }
    }
}
