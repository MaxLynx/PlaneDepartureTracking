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
    public partial class HistoryForm : Form
    {
        List<String> history;
        public HistoryForm(List<String> history)
        {
            this.history = history;
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            StringBuilder textBoxInfo = new StringBuilder();
            foreach(String line in history)
            {
                textBoxInfo.Append(line);
                textBoxInfo.Append("\r\n");
            }
            textBox1.Text = textBoxInfo.ToString();
        }
    }
}
