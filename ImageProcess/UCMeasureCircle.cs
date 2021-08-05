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

    public partial class UCMeasureCircle : UserControl
    {
        HWindow hwin;
        MeasureCircle mc = null;
        bool bInit = false;
        

        public UCMeasureCircle(MeasureCircle _mc)
        {
            bInit = false;
            hwin = _mc.hwin;
            mc = _mc;
            InitializeComponent();
        }
        private void setControls()
        {
           
            if (mc != null)
            {
                nudRow.Value = (decimal)mc.dCircleRow;
                nudCol.Value = (decimal)mc.dCircleColumn;
                nudRadius.Value = (decimal)mc.dCircleRadius;
                nudScore.Value = (decimal)mc.dMinScore;
                nudDist.Value = (decimal)mc.dMinDist;
                nudLen1.Value = (decimal)mc.dLen1;
                nudLen2.Value = (decimal)mc.dLen2;
                nudThresh.Value = (decimal)mc.iThresh;
                cmbSelect.Text = mc.strSelect;
                cmbTrans.Text = mc.strMTrans;
                cbCheckAngle.Checked = mc.bCheckAngle;
                cbModelCenter.Checked = mc.bModelCenter;
                nudMaxR.Value = (decimal)mc.dMaxR;
                nudMinR.Value = (decimal)mc.dMinR;
            }
        }
        private void setMeasureValue()
        {
            mc.dMinScore = (double)nudScore.Value;
            mc.dMinDist = (double)nudDist.Value;
            mc.dLen1 = (double)nudLen1.Value;
            mc.dLen2 = (double)nudLen2.Value;
            mc.iThresh = (int)nudThresh.Value;
            mc.strSelect = cmbSelect.Text;
            mc.strMTrans = cmbTrans.Text;
            mc.bCheckAngle = cbCheckAngle.Checked;
            mc.bModelCenter = cbModelCenter.Checked;
            mc.dMinR = (double)nudMinR.Value;
            mc.dMaxR = (double)nudMaxR.Value;
        }

        private void UCMeasureCircle_Load(object sender, EventArgs e)
        {
          
            setControls();
            mc.createDrawCircleObj(hwin, mc.dCircleRow, mc.dCircleColumn, mc.dCircleRadius);
            timer1.Start();
            bInit = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {          
            test();
            
        }
        private void test()
        {
            

            setMeasureValue();
           

            mc.getDrawCircleParam();
            mc.setMeasureParam();
             if (mc.bModelCenter)
            {
                if (!mc.bModelResult)
                {
                    MessageBox.Show("未找到输出位置，请检查模板设置或去掉模板输出位置！");
                    return;
                }
                else {
                    mc.createDrawCircleObj(hwin, mc.dCircleRow, mc.dCircleColumn, mc.dCircleRadius);
                }
            }
            mc.action(mc.hImage);
            HOperatorSet.ClearWindow(hwin);
            IProcess.DispImage(mc.hImage, hwin);
            mc.showObj(hwin);

            if (mc.bTestResult)
            {
                lblResult.ForeColor = Color.Green;
                lblResult.Text = "OK";
            }
            else
            {
                lblResult.ForeColor = Color.Red;
                lblResult.Text = "NG";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //mc.getDrawCircleParam();
                nudRow.Value = (decimal)mc.dCircleRow;
                nudCol.Value = (decimal)mc.dCircleColumn;
                nudRadius.Value = (decimal)mc.dCircleRadius;
                if (mc.dic_outResult.ContainsKey(OutputResult.MC_Col))
                {
                    lblResultCol.Text = mc.dic_outResult[OutputResult.MC_Col].ToString("0.000");
                    lblResultRow.Text = mc.dic_outResult[OutputResult.MC_Row].ToString("0.000");
                    lblResultR.Text = mc.dic_outResult[OutputResult.MC_Radius].ToString("0.000");

                }
                if ( mc.dic_outResult.ContainsKey(OutputResult.MC_Angle))
                    lblResultAngle.Text = mc.dic_outResult[OutputResult.MC_Angle].ToString("0.000");
            }
            catch (Exception ex) { }
        }

        private void nudScore_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                test();
        }
        public void Release()
        {
            timer1.Stop();
            timer1 = null;
            mc.bRun = true;
            mc.clearDrawObj(hwin);
        }
        
    }
}
