using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly
{
    public partial class FrmTestAutoCalib : Form
    {
        public static string strTitle = "";
        
        public FrmTestAutoCalib()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.自动标定)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            Run.bAutoCalibFlag = true;
            Run.autoCalib.IStep = 0;
            Run.runMode = RunMode.自动标定;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bAutoCalibFlag = !Run.bAutoCalibFlag;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.自动标定)
            {
                btnStart.Text = "验 证 中。。";
                if (Run.bAutoCalibFlag)
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开 始 ";
                btnStop.Text = "暂 停";
            }
        }

        private void FrmTestAutoCalib_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Run.runMode = RunMode.手动;
            timer1 = null;
        }

        private void FrmTestAutoCalib_Load(object sender, EventArgs e)
        {
            this.Text = strTitle;
        }
    }
}
