using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simulation.Forms
{
    public partial class Controller : Form
    {
        public Controller()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form dispaly = new Forms.Map((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            dispaly.Show();
        }
    }
}
