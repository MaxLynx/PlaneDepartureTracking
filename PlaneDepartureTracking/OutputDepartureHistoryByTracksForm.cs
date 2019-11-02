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
    public partial class OutputDepartureHistoryByTracksForm : Form
    {
        Airport model;
        public OutputDepartureHistoryByTracksForm(Airport model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void OutputDepartureHistoryByTracksForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "TRACK NAME";
            dataGridView1.Columns[1].Name = "ID";
            dataGridView1.Columns[2].Name = "PRODUCER";
            dataGridView1.Columns[3].Name = "TYPE";
            dataGridView1.Columns[4].Name = "DEPARTURE TIME";

            foreach(String[] row in model.OutputTrackDepartureHistories())
            {
                dataGridView1.Rows.Add(row);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
