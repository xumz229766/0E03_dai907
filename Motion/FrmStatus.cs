using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Motion0
{
    public partial class FrmStatus : Form
    {
        MotionCard mc = null;
        public FrmStatus()
        {
            InitializeComponent();
        }

        private void FrmStatus_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            timer1.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // ioControl1.setStatus(checkBox1.Checked);
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ioControl1.updateIOStatus();
            axisControl1.updateAxis(mc.dic_Axis);
            lblElapsedTime.Text = mc.lRuntime.ToString()+" ms";
        }

        private void FrmStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void ioControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
