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
    public partial class FrmTestFlash : Form
    {
        bool bInit = false;
        public int iModule = 1;
        MotionCard mc = null;
        public FrmTestFlash(int _iModule = 1)
        {
            mc = MotionCard.getMotionCard();
            iModule = _iModule;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Run.runMode = RunMode.飞拍测试;
            Run.testFlash.IStep = 0;
            TestFlash.bFlash = false;
            TestFlash.bNotTestFlash = cbCam.Checked;
            TestFlash.iPicNum = 1;//拍照顺序
            TestFlash.iPicSum = 9;//拍照总数,飞拍之前赋值确认
            TestFlash.lstPos.Clear();
            TestFlash.lstPoint.Clear();
            TestFlash.lstPoint1.Clear();
            for (int i = 0; i < 9; i++)
            {
                TestFlash.lstPoint.Add(new Point());
                TestFlash.lstPoint1.Add(new Point());
            }
            TestFlash.iCurrentPos = 0;
            TestFlash.dMakeup = (double)nudDelay.Value;
            if (rbtn1.Checked)
            {
                if (AssembleSuction1.pSafeXYZ.X-10 > mc.dic_Axis[AXIS.组装X1轴].dPos)
                {
                    MessageBox.Show("组装X1超过安全位");
                    return;
                }

                 TestFlash.axis=AXIS.取料X1轴;//测试轴
                 TestFlash.axisZ = AXIS.取料Z1轴;
                 TestFlash.dSafeZ = OptSution1.pSafeXYZ.Z;
              
                TestFlash.dStartPos = OptSution1.dFlashStartX;//飞拍起始点位
                TestFlash.dEndPos = OptSution1.pPutPos.X;//飞拍结束点位
       
               
                TestFlash.doCam=DO.取料下相机1;
                TestFlash.channel = 0;//触发通道
                
                TestFlash.bFlash = false;
                for (int i = 0; i < 9; i++)
                {
                    TestFlash.lstPos.Add(CommonSet.dic_OptSuction1[i + 1].DPosCameraDownX);
                }

            }
            else if (rbtn2.Checked)
            {
                if (AssembleSuction2.pSafeXYZ.X - 10 > mc.dic_Axis[AXIS.组装X2轴].dPos)
                {
                    MessageBox.Show("组装X2超过安全位");
                    return;
                }
                TestFlash.axis = AXIS.取料X2轴;//测试轴
                TestFlash.axisZ = AXIS.取料Z2轴;
                TestFlash.dSafeZ = OptSution2.pSafeXYZ.Z;

                TestFlash.dStartPos = OptSution2.dFlashStartX;//飞拍起始点位
                TestFlash.dEndPos = OptSution2.pPutPos.X;//飞拍结束点位


                TestFlash.doCam = DO.取料下相机2;
                TestFlash.channel = 1;//触发通道

                TestFlash.bFlash = false;
                for (int i = 0; i < 9; i++)
                {
                    TestFlash.lstPos.Add(CommonSet.dic_OptSuction2[i + 10].DPosCameraDownX);
                }

            }
            else if (rbtn3.Checked)
            {
                if (OptSution1.pSafeXYZ.X + 10 < mc.dic_Axis[AXIS.取料X1轴].dPos)
                {
                    MessageBox.Show("取料X1超过安全位");
                    return;
                }
                TestFlash.axis = AXIS.组装X1轴;//测试轴
                TestFlash.axisZ = AXIS.组装Z1轴;
                TestFlash.dSafeZ = AssembleSuction1.pSafeXYZ.Z;

                TestFlash.dStartPos = AssembleSuction1.dFlashStartX;//飞拍起始点位
                TestFlash.dEndPos = AssembleSuction1.dFlashEndX;//飞拍结束点位


                TestFlash.doCam = DO.组装下相机1;
                TestFlash.channel = 0;//触发通道

                TestFlash.bFlash = false;
                for (int i = 0; i < 9; i++)
                {
                    TestFlash.lstPos.Add(CommonSet.dic_Assemble1[i + 1].DPosCameraDownX );
                }

            }
            else if (rbtn4.Checked)
            {
                if (OptSution2.pSafeXYZ.X + 10 < mc.dic_Axis[AXIS.取料X2轴].dPos)
                {
                    MessageBox.Show("取料X2超过安全位");
                    return;
                }
                TestFlash.axis = AXIS.组装X2轴;//测试轴
                TestFlash.axisZ = AXIS.组装Z2轴;
                TestFlash.dSafeZ = AssembleSuction2.pSafeXYZ.Z;
                TestFlash.dStartPos = AssembleSuction2.dFlashStartX;//飞拍起始点位
                TestFlash.dEndPos = AssembleSuction2.dFlashEndX;//飞拍结束点位


                TestFlash.doCam = DO.组装下相机2;
                TestFlash.channel = 2;//触发通道

                TestFlash.bFlash = false;
                for (int i = 0; i < 9; i++)
                {
                    TestFlash.lstPos.Add(CommonSet.dic_Assemble2[i + 10].DPosCameraDownX );
                }

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Run.runMode = RunMode.手动;
        }

        private void FrmTestFlash_FormClosing(object sender, FormClosingEventArgs e)
        {
            Run.runMode = RunMode.手动;
            CommonSet.lstFlashWin.Clear();
            // this.Visible = false;
            //e.Cancel = true;

        }

        private void FrmTestFlash_Load(object sender, EventArgs e)
        {

            //if (iModule == 1)
            //{
            nudDelay.Value = (decimal)OptSution1.dFalshMakeUpX;
            //}
            //else{
            //    nudDelay.Value = (decimal)ProductSuction.dCamMakeup2;
            //}
            CommonSet.lstFlashWin.Clear();
            CommonSet.lstFlashWin.Add(hWindowControl1.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl2.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl3.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl4.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl5.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl6.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl7.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl8.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl9.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl10.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl11.HalconWindow);
            CommonSet.lstFlashWin.Add(hWindowControl12.HalconWindow);
            bInit = true;   

        }

        private void nudDelay_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (iModule == 1)
            {
                OptSution1.dFalshMakeUpX = (double)nudDelay.Value;
            }
            else if(iModule == 2)
            {
                OptSution2.dFalshMakeUpX = (double)nudDelay.Value;
            }
            else if (iModule == 3)
            {
                AssembleSuction1.dFalshMakeUpX = (double)nudDelay.Value;
            }
            else if (iModule == 4)
            {
                AssembleSuction2.dFalshMakeUpX = (double)nudDelay.Value;
            }
            TestFlash.dMakeup = (double)nudDelay.Value;
           
        }

        private void rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (rbtn1.Checked)
            {
                iModule = 1;
                nudDelay.Value = (decimal)OptSution1.dFalshMakeUpX;
            }
        }

        private void rbtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (rbtn2.Checked)
            {
                iModule = 2;
                nudDelay.Value = (decimal)OptSution2.dFalshMakeUpX;
            }
        }

        private void rbtn3_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (rbtn3.Checked)
            {
                iModule = 3;
                nudDelay.Value = (decimal)AssembleSuction1.dFalshMakeUpX;
            }
        }

        private void rbtn4_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (rbtn4.Checked)
            {
                iModule = 4;
                nudDelay.Value = (decimal)AssembleSuction2.dFalshMakeUpX;
            }
        }

        private void cbCam_Click(object sender, EventArgs e)
        {
            TestFlash.bNotTestFlash = cbCam.Checked;
        }
    }
}
