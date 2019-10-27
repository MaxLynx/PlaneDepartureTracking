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
    public partial class ListAllWaitingPlanesByID : Form
    {
        Airport model;
        public ListAllWaitingPlanesByID(Airport model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void ListAllWaitingPlanesByID_Load(object sender, EventArgs e)
        {
            List<String> output = model.OutputWaitingPlanes();
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 7;
            /*
            dataGridView1.Columns[1].Width = dataGridView1.Columns[1].Width + 80;
            dataGridView1.Columns[3].Width = dataGridView1.Columns[3].Width + 200;
            dataGridView1.Columns[4].Width = dataGridView1.Columns[4].Width * 2;*/
            dataGridView1.Columns[0].Name = "PRODUCER";
            dataGridView1.Columns[1].Name = "TYPE";
            dataGridView1.Columns[2].Name = "ID";
            dataGridView1.Columns[3].Name = "MIN TRACK LENGTH";
            dataGridView1.Columns[4].Name = "ARRIVAL TIME";
            dataGridView1.Columns[5].Name = "TRACK REQUIREMENT TIME";
            dataGridView1.Columns[6].Name = "PRIORITY";
                foreach (String line in output)
                {
                    String[] row = line.Split(',');
                    
                    dataGridView1.Rows.Add(row);
                }
        }
    }
}
