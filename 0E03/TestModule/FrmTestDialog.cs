using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0E03
{
    public partial class FrmTestDialog : Form
    {
        ActionModule testModule = null;
        RunMode testMode;
        int iIndex = 0;
        string strTitle = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">测试模块</param>
        /// <param name="_mode">运行模式</param>
        /// <param name="iFlagIndex">bool标志索引</param>
        public FrmTestDialog(ActionModule m,RunMode _mode,int iFlagIndex,string str="")
        {
            testModule = m;
            testMode = _mode;
            iIndex = iFlagIndex;
            strTitle = str;
            InitializeComponent();
        }
        private void FrmTestSingleAxis_Load(object sender, EventArgs e)
        {
            this.Text = testMode.ToString()+"--"+strTitle;
            timer1.Start();
        }

        private void FrmTestSingleAxis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Run.bTestFlag = new bool[10];
            Run.runMode = RunMode.手动;
            timer1.Stop();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bTestFlag[iIndex] = !Run.bTestFlag[iIndex];
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.手动)
            {
                Run.runMode = testMode;
                Run.bTestFlag[iIndex] = true;
                testModule.IStep = 0;
            }
            else
            {
                Run.runMode = RunMode.手动;
                Run.bTestFlag[iIndex] = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Run.runMode == testMode)
            {
                btnStart.Text = "测试中。。";
                if (Run.bTestFlag[iIndex])
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开始测试";
                btnStop.Text = "暂 停";
            }
        }
    }
}
