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
    public partial class FrmAutoCenter : Form
    {
        public FrmAutoCenter()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.取料中心对位测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            Run.bTestGetFlag = true;
            Run.centerTestModule.IStep = 0;
           
            Run.runMode = RunMode.取料中心对位测试;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bTestGetFlag = !Run.bTestGetFlag;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           

            if (Run.runMode == RunMode.取料中心对位测试)
            {
                btnStart.Text = "中心对位测试中。。";
                if (Run.bTestGetFlag)
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开 始 对 位";
                btnStop.Text = "暂 停";
            }
        }

        private void FrmAutoCenter_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FrmAutoCenter_FormClosing(object sender, FormClosingEventArgs e)
        {
            Run.runMode = RunMode.手动;
            timer1.Stop();
            timer1 = null;
        }
    }
}
