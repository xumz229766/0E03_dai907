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
using HalconDotNet;
using ImageProcess;

namespace Assembly
{
    public partial class FrmPixelToAxis : Form
    {
        int iCamPos = 1;
        MotionCard mc = null;
        bool bInit = false;
        public FrmPixelToAxis()
        {
            mc = MotionCard.getMotionCard();
            InitializeComponent();
        }

        private void FrmPixelToAxis_Load(object sender, EventArgs e)
        {
            CommonSet.hPixelToAxisCalibWin = hWindowControl1.HalconWindow;
            bInit = false;
            updateControl();
            bInit = true;
        }
        private string strHead1 = "已标定值\r\n序号  X     Y       Row     Column \r\n";
        private string strHead2 = "当前标定值\r\n序号  X     Y       Row     Column \r\n";
        public void updateUI()
        {
            try
            {
                if (!bInit)
                    return;
                updateControl();
                txtCalibNow.Text = strHead2 + GetString(CalibModule.lstPointXY,CalibModule.lstPointRC);
              
            }
            catch (Exception)
            {
                
                
            }
        }
        public void updateControl()
        {
            if (radioButton1.Checked)
            {
                iCamPos = 1;
                setNumerialControl(nudCalibX, OptSution1.pCalibCameraUpXY.X);
                setNumerialControl(nudCalibY, OptSution1.pCalibCameraUpXY.Y);
                txtCalib.Text = "取料1" + strHead1 + GetString(OptSution1.lstCalibCameraUpXY, OptSution1.lstCalibCameraUpRC);
                lblCurrentAxisPos.Text = "X:"+mc.dic_Axis[AXIS.取料X1轴].dPos.ToString("0.000")+"Y:"+mc.dic_Axis[AXIS.取料Y1轴].dPos.ToString("0.000") ;
                // nudCalibY.Value = (decimal)OptSution1.pCalibCameraUpXY.Y;
                setNumerialControl(nudGain, OptSution1.dCalibGain);
            }
            else if (radioButton2.Checked)
            {
                iCamPos = 2;
                setNumerialControl(nudCalibX, OptSution2.pCalibCameraUpXY.X);
                setNumerialControl(nudCalibY, OptSution2.pCalibCameraUpXY.Y);
                txtCalib.Text = "取料2" + strHead1 + GetString(OptSution2.lstCalibCameraUpXY, OptSution2.lstCalibCameraUpRC);
                lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.取料X2轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.取料Y2轴].dPos.ToString("0.000");
                //nudCalibX.Value = (decimal)OptSution2.pCalibCameraUpXY.X;
                //nudCalibY.Value = (decimal)OptSution2.pCalibCameraUpXY.Y;
                setNumerialControl(nudGain, OptSution2.dCalibGain);
            }
            else if (radioButton3.Checked)
            {
                iCamPos = 3;
                setNumerialControl(nudCalibX, AssembleSuction1.pCalibCameraUpXY.X);
                setNumerialControl(nudCalibY, AssembleSuction1.pCalibCameraUpXY.Y);
                setNumerialControl(nudGain, AssembleSuction1.dCalibGain);

                txtCalib.Text = "组装1工位1" + strHead1 + GetString(AssembleSuction1.lstCalibCameraUpXY1, AssembleSuction1.lstCalibCameraUpRC1);
                lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.组装X1轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.组装Y1轴].dPos.ToString("0.000");
            }
            else if (radioButton4.Checked)
            {
                iCamPos = 4;
                setNumerialControl(nudCalibX, AssembleSuction1.pCalibCameraUpXY2.X);
                setNumerialControl(nudCalibY, AssembleSuction1.pCalibCameraUpXY2.Y);
                setNumerialControl(nudGain, AssembleSuction1.dCalibGain);

                txtCalib.Text = "组装1工位2" + strHead1 + GetString(AssembleSuction1.lstCalibCameraUpXY2, AssembleSuction1.lstCalibCameraUpRC2);
                lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.组装X1轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.组装Y2轴].dPos.ToString("0.000");
                //nudCalibX.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.X;
                //nudCalibY.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.Y;
            }
            else if (radioButton5.Checked)
            {
                iCamPos = 5;
                setNumerialControl(nudCalibX, AssembleSuction2.pCalibCameraUpXY.X);
                setNumerialControl(nudCalibY, AssembleSuction2.pCalibCameraUpXY.Y);
                setNumerialControl(nudGain, AssembleSuction2.dCalibGain);

                txtCalib.Text = "组装2工位1" + strHead1 + GetString(AssembleSuction2.lstCalibCameraUpXY1, AssembleSuction2.lstCalibCameraUpRC1);
                lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.组装X2轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.组装Y1轴].dPos.ToString("0.000");
                
                //nudCalibX.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.X;
                //nudCalibY.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.Y;
            }
            else if (radioButton6.Checked)
            {
                iCamPos = 6;
                setNumerialControl(nudCalibX, AssembleSuction2.pCalibCameraUpXY2.X);
                setNumerialControl(nudCalibY, AssembleSuction2.pCalibCameraUpXY2.Y);
                setNumerialControl(nudGain, AssembleSuction2.dCalibGain);

                txtCalib.Text = "组装2工位2" + strHead1 + GetString(AssembleSuction2.lstCalibCameraUpXY2, AssembleSuction2.lstCalibCameraUpRC2);
                lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.组装X2轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.组装Y2轴].dPos.ToString("0.000");
                
                
                //nudCalibX.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.X;
                //nudCalibY.Value = (decimal)AssembleSuction2.pCalibCameraUpXY.Y;
            }
            else if (radioButton7.Checked)
            {
                iCamPos = 7;
                setNumerialControl(nudCalibX, BarrelSuction.pPicGlueCalib.X);
                setNumerialControl(nudCalibY, BarrelSuction.pPicGlueCalib.Y);
                setNumerialControl(nudGain, BarrelSuction.dCalibGain);
                txtCalib.Text = "点胶" + strHead1 + GetString(BarrelSuction.lstCalibCameraUpXY, BarrelSuction.lstCalibCameraUpRC);
                //nudCalibX.Value = (decimal)BarrelSuction.pPicGlueCalib.X;
                //nudCalibY.Value = (decimal)BarrelSuction.pPicGlueCalib.Y;
            }
        }
        private void setNumerialControl(NumericUpDown nud, double value)
        {
            if (!nud.Focused)
            {
                if (value < (double)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (double)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        private string GetString(List<Point> lstXY,List<Point> lstRC)
        {
            StringBuilder sb = new StringBuilder();
            if (lstXY.Count == lstRC.Count)
            {
                int count = lstRC.Count;
                for (int i = 0; i < count; i++)
                { 
                    sb.Append(i.ToString("0")+"    "+lstXY[i].X.ToString("0.000")+"  "+lstXY[i].Y.ToString("0.000")+"   "+lstRC[i].X.ToString("0.000")+"   "+lstRC[i].Y.ToString("0.000"));
                    sb.Append("\r\n");
                }
            }
            return sb.ToString();
        }
        public void SaveUI()
        {
            if (radioButton1.Checked)
            {
                iCamPos = 1;
                OptSution1.pCalibCameraUpXY.X = (double)nudCalibX.Value;
                OptSution1.pCalibCameraUpXY.Y = (double)nudCalibY.Value;
                OptSution1.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton2.Checked)
            {
                iCamPos = 2;
                OptSution2.pCalibCameraUpXY.X = (double)nudCalibX.Value;
                OptSution2.pCalibCameraUpXY.Y = (double)nudCalibY.Value;
                OptSution2.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton3.Checked)
            {
                iCamPos = 3;
               
                AssembleSuction1.pCalibCameraUpXY.X = (double)nudCalibX.Value;
                AssembleSuction1.pCalibCameraUpXY.Y = (double)nudCalibY.Value;
                AssembleSuction1.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton4.Checked)
            {
                iCamPos = 4;
               
                AssembleSuction1.pCalibCameraUpXY2.X = (double)nudCalibX.Value;
                AssembleSuction1.pCalibCameraUpXY2.Y = (double)nudCalibY.Value;
                AssembleSuction1.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton5.Checked)
            {
                iCamPos = 5;

                AssembleSuction2.pCalibCameraUpXY.X = (double)nudCalibX.Value;
                AssembleSuction2.pCalibCameraUpXY.Y = (double)nudCalibY.Value;
                AssembleSuction2.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton6.Checked)
            {
                iCamPos = 6;

                AssembleSuction2.pCalibCameraUpXY2.X = (double)nudCalibX.Value;
                AssembleSuction2.pCalibCameraUpXY2.Y = (double)nudCalibY.Value;
                AssembleSuction2.dCalibGain = (double)nudGain.Value;
            }
            else if (radioButton7.Checked)
            {
                iCamPos = 7;

                BarrelSuction.pPicGlueCalib.X = (double)nudCalibX.Value;
                BarrelSuction.pPicGlueCalib.Y = (double)nudCalibY.Value;
                BarrelSuction.dCalibGain = (double)nudGain.Value;
            }
            
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iCamPos == 1)
                {
                    CheckUpA1();
                }
                else if (iCamPos == 2)
                {
                    CheckUpA2();
                }
                else if (iCamPos == 3)
                {
                    CheckUpC1();
                }
                else if (iCamPos == 4)
                {
                    CheckUpC1();
                }
                else if (iCamPos == 5)
                {
                    CheckUpC2();
                }
                else if (iCamPos == 6)
                {
                    CheckUpC2();
                }
                else if (iCamPos == 7)
                {
                    CheckUpD();
                }
            }
            catch (Exception)
            {


            }
        }
        private double dRow, dColumn;
        private void CheckUpA1()
        {
            var suction = CommonSet.dic_OptSuction1[1];
            HObject hImage = suction.hImageUP;
            suction.InitImageUp(OptSution1.strPicUpCalibName);
            ProcessFatory pf = suction.pfUp;
            if (pf == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            pf.SetRun(true);
            pf.hwin = hWindowControl1.HalconWindow;
            suction.actionUp(hImage);
            pf.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultUp.CenterColumn.ToString("0.000");
            nudRow.Value = (decimal)suction.imgResultUp.CenterRow;
            nudCol.Value = (decimal)suction.imgResultUp.CenterColumn;
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (suction.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckUpA2()
        {
            var suction = CommonSet.dic_OptSuction2[10];
           
            suction.InitImageUp(OptSution2.strPicUpCalibName);
            HObject hImage = suction.hImageUP;
            ProcessFatory pf = suction.pfUp;
            if (pf == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            pf.SetRun(true);
            pf.hwin = hWindowControl1.HalconWindow;
            suction.actionUp(hImage);
            pf.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultUp.CenterColumn.ToString("0.000");
            //dRow = suction.imgResultUp.CenterRow;
            //dColumn = suction.imgResultUp.CenterColumn;
            nudRow.Value = (decimal)suction.imgResultUp.CenterRow;
            nudCol.Value = (decimal)suction.imgResultUp.CenterColumn;
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (suction.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckUpC1()
        {
          
            HObject hImage = AssembleSuction1.hImageUP;
            AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
            ProcessFatory pf = AssembleSuction1.pfUp;
            if (pf == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            pf.SetRun(true);
            pf.hwin = hWindowControl1.HalconWindow;
            AssembleSuction1.actionUp(hImage);
            pf.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
            //dRow = AssembleSuction1.imgResultUp.CenterRow;
            //dColumn = AssembleSuction1.imgResultUp.CenterColumn;
            nudRow.Value = (decimal)AssembleSuction1.imgResultUp.CenterRow;
            nudCol.Value = (decimal)AssembleSuction1.imgResultUp.CenterColumn;
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (AssembleSuction1.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckUpC2()
        {
           
            HObject hImage = AssembleSuction2.hImageUP;
            AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCalibName);
            ProcessFatory pf = AssembleSuction2.pfUp;
            if (pf == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            pf.SetRun(true);
            pf.hwin = hWindowControl1.HalconWindow;
            AssembleSuction2.actionUp(hImage);
            pf.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
            //dRow = AssembleSuction2.imgResultUp.CenterRow;
            //dColumn = AssembleSuction2.imgResultUp.CenterColumn;
            nudRow.Value = (decimal)AssembleSuction2.imgResultUp.CenterRow;
            nudCol.Value = (decimal)AssembleSuction2.imgResultUp.CenterColumn;
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (AssembleSuction2.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckUpD()
        {
           
            HObject hImage = BarrelSuction.hImageUP;
            BarrelSuction.InitImageUp(BarrelSuction.strPicUpCalibName);
            ProcessFatory pf = BarrelSuction.pfUp;
            if (pf == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            pf.SetRun(true);
            pf.hwin = hWindowControl1.HalconWindow;
            BarrelSuction.actionUp(hImage);
            pf.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");
            //dRow = BarrelSuction.imgResultUp.CenterRow;
            //dColumn = BarrelSuction.imgResultUp.CenterColumn;
            nudRow.Value = (decimal)BarrelSuction.imgResultUp.CenterRow;
            nudCol.Value = (decimal)BarrelSuction.imgResultUp.CenterColumn;
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (BarrelSuction.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void btnTriggerCam_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (iCamPos == 1)
                {
                    if (CommonSet.camUpA1 != null)
                    {
                      
                        mc.setDO(DO.取料上相机1, false);
                    }
                }
                else if (iCamPos == 2)
                {
                    if (CommonSet.camUpA2 != null)
                    {

                        mc.setDO(DO.取料上相机2, false);
                    }
                }
                else if (iCamPos == 3)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                       
                        mc.setDO(DO.组装上相机1, false);
                    }
                }
                else if (iCamPos == 4)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                      
                        mc.setDO(DO.组装上相机1, false);
                    }
                }
                else if (iCamPos == 5)
                {
                    if (CommonSet.camUpC2 != null)
                    {

                        mc.setDO(DO.组装上相机2, false);
                    }
                }
                else if (iCamPos == 6)
                {
                    if (CommonSet.camUpC2 != null)
                    {

                        mc.setDO(DO.组装上相机2, false);
                    }
                }
                else if (iCamPos == 7)
                {
                    if (CommonSet.camUpD1 != null)
                    {

                        mc.setDO(DO.点胶上相机, false);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCam_MouseDown(object sender, MouseEventArgs e)
        {
           
            try
            {
                if (iCamPos == 1)
                {
                    if (CommonSet.camUpA1 != null)
                    {
                        CommonSet.camUpA1.SetExposure(OptSution1.dExposureTimeUp);
                        CommonSet.camUpA1.SetGain(OptSution1.dCalibGain);
                        mc.setDO(DO.取料上相机1, true);
                    }
                }
                else if (iCamPos == 2)
                {
                    if (CommonSet.camUpA2 != null)
                    {
                        CommonSet.camUpA2.SetExposure(OptSution2.dExposureTimeUp);
                        CommonSet.camUpA2.SetGain(OptSution2.dCalibGain);
                        mc.setDO(DO.取料上相机2, true);
                    }
                }
                else if (iCamPos == 3)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGain);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else if (iCamPos == 4)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGain);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else if (iCamPos == 5)
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGain);
                        mc.setDO(DO.组装上相机2, true);
                    }
                }
                else if (iCamPos == 6)
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGain);
                        mc.setDO(DO.组装上相机2, true);
                    }
                }
                else if (iCamPos == 7)
                {
                    if (CommonSet.camUpD1 != null)
                    {
                        CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                        CommonSet.camUpD1.SetGain(BarrelSuction.dCalibGain);
                        mc.setDO(DO.点胶上相机, true);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iCamPos == 1)
            {
                strName = OptSution1.strPicUpCalibName;
                hoImage = CommonSet.dic_OptSuction1[1].hImageUP;
                if (strName.Equals(""))
                {
                    strName = "取料1标定" ;

                }
            }
            else if(iCamPos == 2)
            {
                strName = OptSution2.strPicUpCalibName;
                hoImage = CommonSet.dic_OptSuction2[10].hImageUP;
                if (strName.Equals(""))
                {
                    strName = "取料2标定";

                }
            }
            else if ((iCamPos == 3)||(iCamPos == 4))
            {
                strName = AssembleSuction1.strPicUpCalibName;
                hoImage = AssembleSuction1.hImageUP;
                if (strName.Equals(""))
                {
                    strName = "组装1标定";

                }
            }
            else if ((iCamPos == 5) || (iCamPos == 6))
            {
                strName = AssembleSuction2.strPicUpCalibName;
                hoImage = AssembleSuction2.hImageUP;
                if (strName.Equals(""))
                {
                    strName = "组装2标定";

                }
            }
            else 
            {
                strName = BarrelSuction.strPicUpCalibName;
                hoImage = BarrelSuction.hImageUP;
                if (strName.Equals(""))
                {
                    strName = "点胶标定";

                }
            }
           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Run.calibModule.IStep = 0;
            CommonSet.hPixelToAxisCalibWin = hWindowControl1.HalconWindow;
            CalibModule.currentPoint.X = (double)nudCalibX.Value;
            CalibModule.currentPoint.Y = (double)nudCalibY.Value;
            CalibModule.dDist = (double)nudDist.Value;
            if (iCamPos == 1)
            {
                CalibModule.axisX = AXIS.取料X1轴;
                CalibModule.axisY = AXIS.取料Y1轴;

            }
            else if (iCamPos == 2)
            {
                CalibModule.axisX = AXIS.取料X2轴;
                CalibModule.axisY = AXIS.取料Y2轴;

            }
            else if (iCamPos == 3)
            {
                CalibModule.axisX = AXIS.组装X1轴;
                CalibModule.axisY = AXIS.组装Y1轴;

            }
            else if (iCamPos == 4)
            {
                CalibModule.axisX = AXIS.组装X1轴;
                CalibModule.axisY = AXIS.组装Y2轴;

            }
            else if (iCamPos == 5)
            {
                CalibModule.axisX = AXIS.组装X2轴;
                CalibModule.axisY = AXIS.组装Y1轴;

            }
            else if (iCamPos == 6)
            {
                CalibModule.axisX = AXIS.组装X2轴;
                CalibModule.axisY = AXIS.组装Y2轴;

            }
            else if (iCamPos == 7)
            {
                CalibModule.axisX = AXIS.点胶X轴;
                CalibModule.axisY = AXIS.点胶Y轴;

            }
            CalibModule.iCamPos = iCamPos;
            Run.runMode = RunMode.像素标定;
        }

        private void nudCalibX_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void nudCalibY_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void btnUpdateCalib_Click(object sender, EventArgs e)
        {
            if (iCamPos == 1)
            {
                OptSution1.lstCalibCameraUpXY.Clear();
                OptSution1.lstCalibCameraUpRC.Clear();
                OptSution1.lstCalibCameraUpXY.AddRange(CalibModule.lstPointXY.ToArray());
                OptSution1.lstCalibCameraUpRC.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 2)
            {
                OptSution2.lstCalibCameraUpXY.Clear();
                OptSution2.lstCalibCameraUpRC.Clear();
                OptSution2.lstCalibCameraUpXY.AddRange(CalibModule.lstPointXY.ToArray());
                OptSution2.lstCalibCameraUpRC.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 3)
            {
                AssembleSuction1.lstCalibCameraUpXY1.Clear();
                AssembleSuction1.lstCalibCameraUpRC1.Clear();
                AssembleSuction1.lstCalibCameraUpXY1.AddRange(CalibModule.lstPointXY.ToArray());
                AssembleSuction1.lstCalibCameraUpRC1.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 4)
            {

                AssembleSuction1.lstCalibCameraUpXY2.Clear();
                AssembleSuction1.lstCalibCameraUpRC2.Clear();
                AssembleSuction1.lstCalibCameraUpXY2.AddRange(CalibModule.lstPointXY.ToArray());
                AssembleSuction1.lstCalibCameraUpRC2.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 5)
            {

                AssembleSuction2.lstCalibCameraUpXY1.Clear();
                AssembleSuction2.lstCalibCameraUpRC1.Clear();
                AssembleSuction2.lstCalibCameraUpXY1.AddRange(CalibModule.lstPointXY.ToArray());
                AssembleSuction2.lstCalibCameraUpRC1.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 6)
            {

                AssembleSuction2.lstCalibCameraUpXY2.Clear();
                AssembleSuction2.lstCalibCameraUpRC2.Clear();
                AssembleSuction2.lstCalibCameraUpXY2.AddRange(CalibModule.lstPointXY.ToArray());
                AssembleSuction2.lstCalibCameraUpRC2.AddRange(CalibModule.lstPointRC.ToArray());

            }
            else if (iCamPos == 7)
            {

                BarrelSuction.lstCalibCameraUpXY.Clear();
                BarrelSuction.lstCalibCameraUpRC.Clear();
                BarrelSuction.lstCalibCameraUpXY.AddRange(CalibModule.lstPointXY.ToArray());
                BarrelSuction.lstCalibCameraUpRC.AddRange(CalibModule.lstPointRC.ToArray());

            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (iCamPos == 1)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;

            }
            else if (iCamPos == 2)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;

            }
            else if (iCamPos == 3)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
               

            }
            else if (iCamPos == 4)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
          

            }
            else if (iCamPos == 5)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;


            }
            else if (iCamPos == 6)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;


            }
            else if (iCamPos == 7)
            {
                nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.点胶X轴].dPos;
                nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;

            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {


            if (iCamPos == 1)
            {
                //nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
                //nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
                mc.AbsMove(AXIS.取料X1轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.取料Y1轴, (double)nudCalibY.Value, 100);


            }
            else if (iCamPos == 2)
            {

               
                mc.AbsMove(AXIS.取料X2轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.取料Y2轴, (double)nudCalibY.Value, 100);
            }
            else if (iCamPos == 3)
            {
               
                mc.AbsMove(AXIS.组装X1轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudCalibY.Value, 100);

            }
            else if (iCamPos == 4)
            {

                mc.AbsMove(AXIS.组装X1轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudCalibY.Value, 100);

            }
            else if (iCamPos == 5)
            {

                mc.AbsMove(AXIS.组装X2轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudCalibY.Value, 100);

            }
            else if (iCamPos == 6)
            {

                mc.AbsMove(AXIS.组装X2轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudCalibY.Value, 100);

            }
            else if (iCamPos == 7)
            {

                mc.AbsMove(AXIS.点胶X轴, (double)nudCalibX.Value, 100);
                mc.AbsMove(AXIS.点胶Y轴, (double)nudCalibY.Value, 30);

            }
        }

        private void btnCheckResult_Click(object sender, EventArgs e)
        {
            Point p = new Point();
            HTuple outX, outY;
            HTuple hom;
            if(CalibModule.lstPointRC.Count<9)
                hom = CommonSet.HomatPixelToAxis(AssembleSuction1.lstCalibCameraUpRC1, AssembleSuction1.lstCalibCameraUpXY1);
            else
              hom = CommonSet.HomatPixelToAxis(CalibModule.lstPointRC, CalibModule.lstPointXY);
            if (hom == null)
            {
                MessageBox.Show("请先标定");
                return;
            }
            
            try
            {
                HOperatorSet.AffineTransPoint2d(hom, (double)nudRow.Value, (double)nudCol.Value,out outX,out outY);
                p.X = outX;
                p.Y = outY;

                lblResult.Text = "X:"+p.X.ToString("0.000")+" Y:"+p.Y.ToString("0.000");
                // p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return ;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            try
            {
                bInit = false;
                updateControl();
                bInit = true;

            }
            catch (Exception)
            {
                
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((nudPosX.Value > (nudCalibX.Value + nudDist.Value)) || (nudPosX.Value < (nudCalibX.Value - nudDist.Value)))
            {
                MessageBox.Show("验证位X超范围！");
                return; 
            }
            if ((nudPosY.Value > (nudCalibY.Value + nudDist.Value)) || (nudPosY.Value < (nudCalibY.Value - nudDist.Value)))
            {
                MessageBox.Show("验证位Y超范围！");
                return;
            }

            if (iCamPos == 1)
            {
                //nudPosX.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
                //nudPosY.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
                mc.AbsMove(AXIS.取料X1轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.取料Y1轴, (double)nudPosY.Value, 100);


            }
            else if (iCamPos == 2)
            {


                mc.AbsMove(AXIS.取料X2轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.取料Y2轴, (double)nudPosY.Value, 100);
            }
            else if (iCamPos == 3)
            {

                mc.AbsMove(AXIS.组装X1轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudPosY.Value, 100);

            }
            else if (iCamPos == 4)
            {

                mc.AbsMove(AXIS.组装X1轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudPosY.Value, 100);

            }
            else if (iCamPos == 5)
            {

                mc.AbsMove(AXIS.组装X2轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudPosY.Value, 100);

            }
            else if (iCamPos == 6)
            {

                mc.AbsMove(AXIS.组装X2轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudPosY.Value, 100);

            }
            else if (iCamPos == 7)
            {

                mc.AbsMove(AXIS.点胶X轴, (double)nudPosX.Value, 100);
                mc.AbsMove(AXIS.点胶Y轴, (double)nudPosY.Value, 30);

            }
        }

        private void nudGain_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }
    }
}
