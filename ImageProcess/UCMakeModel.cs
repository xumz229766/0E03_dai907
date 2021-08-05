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
    public partial class UCMakeModel : UserControl
    {
        HWindow hwin;
        MakeModel m;
        public UCMakeModel(HWindow _hwin,MakeModel _m)
        {
            hwin = _hwin;
            m = _m;
            InitializeComponent();
        }
        public void setControls()
        {
            if (m != null)
            {
                nudMinScore.Value = (decimal)m.dMinScore;
                if (m.bReduceRegion)
                {
                    rbtnRegion.Checked = true;
                }
                else {
                    rbtnAll.Checked = true;
                }
                cbOutput.Checked = m.bOutputPoint;
                lblOrginRow.Text = m.outputRow.ToString();
                lblOrginCol.Text = m.outputCol.ToString();
                
            }
        }
        public void getModelParam()
        {
            m.dMinScore = (double)nudMinScore.Value;
            m.bReduceRegion = rbtnRegion.Checked;
            m.bOutputPoint = cbOutput.Checked;
            //m.outputRow = (int)nudOutputRow.Value;
            //m.outputCol = (int)nudOutputCol.Value;

        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            m.bRun = true;
            getModelParam();
            m.action(m.hImage);
            HOperatorSet.ClearWindow(hwin);
            HOperatorSet.DispImage(m.hImage, hwin);
            m.showObj(hwin);
            showResult();
            m.bRun = false;
            
        }
        private void showResult() {
            try
            {
                if (m.bTestResult)
                {
                    lblModelResult.Text = "OK";
                    lblModelResult.ForeColor = Color.Green;
                }
                else
                {
                    lblModelResult.Text = "NG";
                    lblModelResult.ForeColor = Color.Red;
                }
                lblModelAngle.Text = m.dic_outResult[OutputResult.Model_Angle].ToString("0.00");
                lblModelCol.Text = m.dic_outResult[OutputResult.Model_Column].ToString("0.000");
                lblModelRow.Text = m.dic_outResult[OutputResult.Model_Row].ToString("0.000");
                lblModelScore.Text = m.dic_outResult[OutputResult.Model_Score].ToString("0.00");
                if (m.bOutputPoint)
                {
                    if (m.dic_outResult.ContainsKey(OutputResult.Model_OutputRow))
                    {
                        lblOutputRow.Text = m.dic_outResult[OutputResult.Model_OutputRow].ToString("0.000");
                        lblOutputCol.Text = m.dic_outResult[OutputResult.Model_OutputCol].ToString("0.000");
                    }
                }
                else
                {
                    lblOutputRow.Text = "0.000";
                    lblOutputCol.Text = "0.000";
                }
            }
            catch (Exception ex) { }
        }

        private void UCMakeModel_Load(object sender, EventArgs e)
        {
            m.createDrawCircleObj(hwin, 100, 100, 50);
            setControls();
        }

        private void btnGetContour_Click(object sender, EventArgs e)
        {
            m.getShapeModelContours((int)nudContrast.Value);
            HOperatorSet.ClearWindow(hwin);
            HOperatorSet.DispImage(m.hImage, hwin);
            m.showObj(hwin);
        }
        public void addErase(HObject hoErase)
        {
            m.addErase(hoErase);
            HOperatorSet.ClearWindow(hwin);
            HOperatorSet.DispImage(m.hImage, hwin);
            m.showObj(hwin);
        }
        public void RemoveErase()
        {
            m.removeErase();
            HOperatorSet.ClearWindow(hwin);
            HOperatorSet.DispImage(m.hImage, hwin);
            m.showObj(hwin);
        }

        private void btnCreateModel_Click(object sender, EventArgs e)
        {
            if (m.createShapeModel((int)nudContrast.Value))
                MessageBox.Show("创建模板成功！");
            else
            {
                MessageBox.Show("创建模板失败!");
            }
        }

        private void cbOutput_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = cbOutput.Checked;

            
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!m.bTestResult) {
                MessageBox.Show("请先查找模板再设定输出点位！");
                return;
            }

            if (m.getOutputPoint((double)nudOutputCol.Value, (double)nudOutputRow.Value))
            {
                lblOrginCol.Text = m.outputCol.ToString("0.000");
                lblOrginRow.Text = m.outputRow.ToString("0.000");
                HOperatorSet.ClearWindow(hwin);
                HOperatorSet.DispImage(m.hImage, hwin);
                m.showObj(hwin);
                MessageBox.Show("设置点位成功!");                

                return;
            }
            else
            {
                MessageBox.Show("设置点位失败!");
                lblOrginCol.Text = "0.000";
                lblOrginRow.Text = "0.000";
                return;
            }
        }
        public void Release()
        {
            m.bRun = true;
            m.clearDrawObj(hwin);
            
        }
    }
}
