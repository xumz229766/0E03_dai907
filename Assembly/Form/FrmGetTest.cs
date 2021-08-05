using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;
namespace Assembly
{
    public partial class FrmGetTest : Form
    {
        public static Point pTest = new Point();//测试点
        public static DO dSuction, dGet;//吸笔气缸和真空
        public static DI dIn;//吸笔真空检测
        public static int min, max;//取料的上下限
        public FrmGetTest()
        {
            InitializeComponent();
        }

        private void FrmGetTest_Load(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.取料测试)
            {
                btnStart.Text = "取 料 测 试 中。。";
                if (Run.bTestGetFlag)
                    btnStop.Text = "暂 停";
                else
                    btnStop.Text = "继 续";
            }
            else
            {
                btnStart.Text = "开 始 测 试";
                btnStop.Text = "暂 停";
            }

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.取料测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            Run.bTestGetFlag = true;
            Run.getTestModule.IStep = 0;
            Run.runMode = RunMode.取料测试;
            

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bTestGetFlag = !Run.bTestGetFlag;

        }

        private void FrmGetTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Run.runMode = RunMode.手动;
            timer1.Stop();
            timer1 = null;
           
        }
    }
}
