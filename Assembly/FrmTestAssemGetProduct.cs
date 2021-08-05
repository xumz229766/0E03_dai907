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
    public partial class FrmTestAssemGetProduct : Form
    {
        public FrmTestAssemGetProduct()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.组装取料测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            Run.bAssemGetFlag = true;
            Run.assemGetProduct.IStep = 0;
            //ResultTestModule.bUseAssembleParam = cbUseAssem.Checked;
            Run.runMode = RunMode.组装取料测试;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bAssemGetFlag = !Run.bAssemGetFlag;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.组装验证)
            {
                btnStart.Text = "运 行 中。。";
                if (Run.bCheckFlag)
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开 始";
                btnStop.Text = "暂 停";
            }

        }

        private void FrmTestAssemGetProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Run.runMode = RunMode.手动;
            timer1 = null;
        }

        private void FrmTestAssemGetProduct_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }



    }
}
