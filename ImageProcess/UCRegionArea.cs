using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;


namespace ImageProcess
{
    public partial class UCRegionArea : UserControl
    {
        HWindow hwin;
        RegionArea ra = null;
        bool bInit = false;
        
        public UCRegionArea(RegionArea _r)
        {
            bInit = false;
            hwin = _r.hwin;
            ra = _r;
            InitializeComponent();
        }
        private void setControls()
        {
            if (ra != null)
            {
                cbModelCenter.Checked = ra.bModelCenter;
                nudRow.Value = (decimal)ra.dCircleRow;
                nudCol.Value = (decimal)ra.dCircleColumn;
                nudRadius.Value = (decimal)ra.dCircleRadius;
                cmbPreProcess.Text = ra.strPreMehtod;
                cmbSize.Text = ra.iSize.ToString();
                nudRingWidth.Value = (decimal)ra.dWidth;
                nudMinThresh.Value = (decimal)ra.iMinThreshold;
                nudMaxThresh.Value = (decimal)ra.iMaxThreshold;                
                nudMinArea.Value = (decimal)ra.dMinArea;
                nudMaxArea.Value = (decimal)ra.dMaxArea;              
                
            }
        }
        private void setRAValue()
        {
            if (ra != null)
            {
                ra.bModelCenter = cbModelCenter.Checked;
                //ra.dCircleColumn = (double)nudCol.Value;
                //ra.dCircleRadius = (double)nudRadius.Value;
                //ra.dCircleRow = (double)nudRow.Value;
                ra.strPreMehtod = cmbPreProcess.Text;
                ra.iSize = Convert.ToInt32(cmbSize.Text);
                ra.dWidth = (double)nudRingWidth.Value;
                ra.iMinThreshold = (int)nudMinThresh.Value;
                ra.iMaxThreshold = (int)nudMaxThresh.Value;
                ra.dMinArea = (double)nudMinArea.Value;
                ra.dMaxArea = (double)nudMaxArea.Value;
            }
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            test();
           
        }

        private void UCRegionAngle_Load(object sender, EventArgs e)
        {
            cmbPreProcess.Text = "无";
            cmbSize.Text = "5";
            setControls();
            ra.createDrawCircleObj(hwin,ra.dCircleRow,ra.dCircleColumn,ra.dCircleRadius);
            timer1.Start();
            bInit = true;
            
        }

        public void Release()
        {
            ra.bRun = true;
            ra.clearDrawObj(hwin);
        }
      
        private void cmbPreProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            setRAValue();
            ra.getPreProcess(ra.hImage, ra.strPreMehtod);
            HOperatorSet.ClearWindow(hwin);
            IProcess.DispImage(ra.hImage, hwin);
            ra.showObj(hwin);
            

        }


        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            setRAValue();
            ra.getPreProcess(ra.hImage, ra.strPreMehtod);
            HOperatorSet.ClearWindow(hwin);
            IProcess.DispImage(ra.hImage, hwin);
            ra.showObj(hwin);
        }

        private void nudRingWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            setRAValue();
            ra.getPreProcess(ra.hImage, ra.strPreMehtod);
            ra.GenRingContours();
            HOperatorSet.ClearWindow(hwin);
            IProcess.DispImage(ra.hImage, hwin);
            ra.showObj(hwin);
        }

        private void nudMinThresh_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            test();
        }

        private void nudMaxThresh_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            test();
        }
        public void test()
        {
            setRAValue();
            ra.GenRingContours();
            //ra.getDrawCircleParam();
            if (ra.bModelCenter)
            {
                if (!ra.bModelResult)
                {
                    MessageBox.Show("未找到输出位置，请检查模板设置或去掉模板输出位置！");
                    return;
                }
                else
                {
                    ra.createDrawCircleObj(hwin, ra.dCircleRow, ra.dCircleColumn, ra.dCircleRadius);
                }
            }
            ra.action(ra.hImage);
            HOperatorSet.ClearWindow(hwin);
            IProcess.DispImage(ra.hImage, hwin);
            ra.showObj(hwin);
            if (ra.bTestResult)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "OK";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "NG";
            }
            lblResultAngle.Text = ra.dResultAngle.ToString();
            lblResultArea.Text = ra.dResultArea.ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (ra == null) {
                return;
            }
            try
            {
                nudRow.Value = (decimal)ra.dCircleRow;
                nudCol.Value = (decimal)ra.dCircleColumn;
                nudRadius.Value = (decimal)ra.dCircleRadius;
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void cbModelCenter_CheckedChanged(object sender, EventArgs e)
        {
            if(!bInit)
                return;
            setRAValue();
            ra.GenRingContours();
            if (ra.bModelCenter)
            {
                if (!ra.bModelResult)
                {
                    MessageBox.Show("未找到输出位置，请检查模板设置或去掉模板输出位置！");
                    return;
                }
                else
                {
                    ra.createDrawCircleObj(hwin, ra.dCircleRow, ra.dCircleColumn, ra.dCircleRadius);
                }
            }
        }
    }
}
