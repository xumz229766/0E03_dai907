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
    public partial class FrmGlueTest : Form
    {
        bool bInit = false;
        int iStationNum = 1;
        public FrmGlueTest()
        {
            InitializeComponent();
        }
        public FrmGlueTest(int _station)
        {
            iStationNum = _station;
            InitializeComponent();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
           // PrepareModule.state1 = ActionState.上墨;
            //if (rbtn1.Checked)
            //{
                
            //    //    Run.rotateModule = new RotateSelfShotTest1();

               
            //    //Run.rotateModule.IStep = 0;
            //    //Run.runMode = RunMode.自旋转;
            //}
            //else
            //{
                
            //    //Run.calibrationModule = new CalibShotTest1();
               
            //    //Run.calibrationModule.IStep = 0;
            //    //Run.runMode = RunMode.标定;
            //}
            //Run.runMode = RunMode.标定;
            Run.glueTest.IStep = 0;
            Run.runMode = RunMode.点胶测试;
           
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.runMode = RunMode.手动;
        }

        private void FrmCalibTest_Load(object sender, EventArgs e)
        {
            bInit = true;
            InitControl();
            bInit = false;

            timer1.Start();
        }

        public void InitControl()
        {
            //SetNumericValue(nudMakeUpR, (decimal)BarrelSuction.dMakeUpR);
            SetNumericValue(nudAngle, (decimal)BarrelSuction.dShotAngle);
            SetNumericValue(nudStartAngle, (decimal)BarrelSuction.dStartAngle);
            SetNumericValue(nudStopAngle, (decimal)BarrelSuction.dStopAngle);
            SetNumericValue(nudAngleZStop, (decimal)BarrelSuction.dAngleZStop);
            SetNumericValue(nudDistZStop, (decimal)BarrelSuction.dDstZStop);
            SetNumericValue(nudShotNum, (decimal)BarrelSuction.iDirect);
        }
        public void UpdateUI()
        {
            try
            {
                InitControl();
            }
            catch (Exception)
            {
                
                
            }  
        }

        public void SetNumericValue(NumericUpDown nud, decimal value)
        {
            if (!nud.Focused)
            {
                if (value > nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                else if (value < nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                nud.Value = value;
               
            }
        }
        private void nudMakeUpR_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
           // BarrelSuction.dMakeUpR = (double)nudMakeUpR.Value;
        }

        private void nudStartAngle_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.dStartAngle = (double)nudStartAngle.Value;
        }

        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.dShotAngle = (double)nudAngle.Value;
        }

        private void nudStopAngle_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.dStopAngle = (double)nudStopAngle.Value;
        }

        private void nudAngleZStop_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.dAngleZStop = (double)nudAngleZStop.Value;
        }

        private void nudDistZStop_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.dDstZStop = (double)nudDistZStop.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void FrmCalibTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1 = null;
            }
            catch (Exception)
            {
                
               
            }
        }

        private void nudShotNum_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;
            BarrelSuction.iDirect = (int)nudShotNum.Value;
        }
    }
}
