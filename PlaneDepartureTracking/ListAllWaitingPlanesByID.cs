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
        String id;
        public ListAllWaitingPlanesByID(Airport model, String id)
        {
            this.model = model;
            this.id = id;
            InitializeComponent();
        }

        private void ListAllWaitingPlanesByID_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "PRODUCER";
            dataGridView1.Columns[1].Name = "TYPE";
            dataGridView1.Columns[2].Name = "ID";
            dataGridView1.Columns[3].Name = "MIN TRACK LENGTH";
            dataGridView1.Columns[4].Name = "ARRIVAL TIME";
            dataGridView1.Columns[5].Name = "TRACK REQUIREMENT TIME";
            dataGridView1.Columns[6].Name = "PRIORITY";
            dataGridView1.Columns[7].Name = "TRACK";

            if (id == null)
            {
                List<String> output = model.OutputWaitingPlanes();

                
                foreach (String line in output)
                {
                    String[] row = line.Split(',');

                    dataGridView1.Rows.Add(row);
                }
            }
            else
            {
                dataGridView1.Rows.Add(model.FindWaitingPlane(id));
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
