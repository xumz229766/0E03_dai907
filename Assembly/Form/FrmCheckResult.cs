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
    public partial class FrmCheckResult : Form
    {
        public FrmCheckResult()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.组装验证)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            Run.bCheckFlag = true;
            Run.resultTestModule.IStep = 0;
            ResultTestModule.bUseAssembleParam = cbUseAssem.Checked;
            Run.runMode = RunMode.组装验证;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bCheckFlag = !Run.bCheckFlag;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.组装验证)
            {
                btnStart.Text = "验 证 中。。";
                if (Run.bCheckFlag)
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开 始 验 证";
                btnStop.Text = "暂 停";
            }
            if (ResultTestModule.bUseOptOnly)
            {
                this.Text = "取料补偿验证";
                cbUseAssem.Visible = false;
            }
            else
            {
                this.Text = "组装验证";
            }
        }

        private void FrmCheckResult_Load(object sender, EventArgs e)
        {

            timer1.Start();
            cbUseAssem.Checked = ResultTestModule.bUseAssembleParam;
        }

        private void FrmCheckResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();

            
            Run.runMode = RunMode.手动;
            timer1 = null;
        }

        private void cbUseAssem_Click(object sender, EventArgs e)
        {
            ResultTestModule.bUseAssembleParam = cbUseAssem.Checked;
        }
    }
}
