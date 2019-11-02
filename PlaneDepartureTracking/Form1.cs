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
            if (listBox1.SelectedIndex > 0)
            {
                new ListAllWaitingPlanesByID(model, null, listBox1.SelectedItem.ToString()).Show();
            }
            else
            {
                new ListAllWaitingPlanesByID(model, null, null).Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new TimeMachineForm(model).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                new ListAllWaitingPlanesByID(model, textBox1.Text, listBox1.SelectedItem.ToString()).Show();
            }
            else
            {
                new ListAllWaitingPlanesByID(model, textBox1.Text, null).Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new HistoryForm(model.PlaneArrivals).Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("All");
            listBox1.SelectedIndex = 0;
            foreach (String name in model.GetTrackNames()) {
                listBox1.Items.Add(name);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new HistoryForm(model.TrackAllocations).Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new HistoryForm(model.PlaneDepartures).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (model.RemovePlaneFromWaiting(textBox1.Text))
            {
                MessageBox.Show("Plane with id " + textBox1.Text + " was removed from the waiting queue");
            }
            else
            {
                MessageBox.Show("Plane with id " + textBox1.Text + " was not found in the waiting queue");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (model.ChangePlanePriority(textBox1.Text, Int32.Parse(textBox2.Text)))
            {
                MessageBox.Show("Priority of plane with id " + textBox1.Text + " was changed");
            }
            else
            {
                MessageBox.Show("Plane with id " + textBox1.Text + " was not found in the waiting queue");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new NotifyPlaneDepartureForm(model).Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new OutputDepartureHistoryByTracksForm(model).Show();
        }
    }
}
