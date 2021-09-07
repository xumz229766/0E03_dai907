using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Motion0;
using Tray;
using HalconDotNet;
using ImageProcess0;
using System.Diagnostics;
namespace _0E030
{
    public partial class FrmSetRight : UIPage
    {

        double velXY = 100;
        double velZ = 50;
        int iCurrentTrayIndex = 1;
        bool bInit = false;
        MotionCard mc = null;
        Tray.Tray tray = null;
        public FrmSetRight()
        {
            InitializeComponent();
        }

        private void FrmSetRight_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            //CommonSet.WriteInfo("加载设置参数界面1");
            InitTray();
            UpdateControl();
            bInit = true;
        }

        public void InitTray()
        {
            bInit = false;
            //当前吸笔无盘时,不加载盘
            if (iCurrentTrayIndex == 0)
                return;
            tray = null;
            Tray.Tray tempTray = TrayFactory.getTrayFactory((iCurrentTrayIndex+2).ToString());
            tempTray.RemoveDelegate();
            tray = tempTray.Clone();
            tempTray.updateColor += FrmMain.trayPR.UpdateColor;


            //tray = tempTray.Clone();
            //trayPanel = new TrayPanel();
            trayPanel1.BShowModel = true;
            trayPanel1.setTrayObj(tray, CommonSet.ColorInit);
            // trayPanel1.Dock = DockStyle.Fill;

            tray.updateColor += trayPanel1.UpdateColor;

            int count = tray.dic_Index.Count;
            nudIndex1.Maximum = count;
            nudIndex2.Maximum = count;
            nudIndex3.Maximum = count;
            // ProductSuction suction = CommonSet.dic_ProductSuction1[iSuctionNum];



            if (ModelManager.SuctionRParam.lstIndex1[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.SuctionRParam.lstIndex1[iCurrentTrayIndex - 1] = count;
                nudIndex1.Value = ModelManager.SuctionRParam.lstIndex1[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex1.Value = ModelManager.SuctionRParam.lstIndex1[iCurrentTrayIndex - 1];
            }

            if (ModelManager.SuctionRParam.lstIndex2[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.SuctionRParam.lstIndex2[iCurrentTrayIndex - 1] = count;
                nudIndex2.Value = ModelManager.SuctionRParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex2.Value = ModelManager.SuctionRParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            if (ModelManager.SuctionRParam.lstIndex3[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.SuctionRParam.lstIndex3[iCurrentTrayIndex - 1] = count;
                nudIndex3.Value = ModelManager.SuctionRParam.lstIndex3[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex3.Value = ModelManager.SuctionRParam.lstIndex3[iCurrentTrayIndex - 1];
            }


            tray.setNumColor(ModelManager.SuctionRParam.lstIndex1[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.SuctionRParam.lstIndex2[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.SuctionRParam.lstIndex3[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.updateColor();


            UpdateControl();
            bInit = true;

        }
        public void UpdateControl()
        {
            #region"标定"
            setNumerialControl(nudCalibUpX1, CalibrationR.lstCalibCameraUpXY[0].X);
            setNumerialControl(nudCalibUpY1, CalibrationR.lstCalibCameraUpXY[0].Y);
            setNumerialControl(nudCamUpPosR1, CalibrationR.lstCalibCameraUpRC[0].X);
            setNumerialControl(nudCamUpPosC1, CalibrationR.lstCalibCameraUpRC[0].Y);

            setNumerialControl(nudCalibUpX2, CalibrationR.lstCalibCameraUpXY[1].X);
            setNumerialControl(nudCalibUpY2, CalibrationR.lstCalibCameraUpXY[1].Y);
            setNumerialControl(nudCamUpPosR2, CalibrationR.lstCalibCameraUpRC[1].X);
            setNumerialControl(nudCamUpPosC2, CalibrationR.lstCalibCameraUpRC[1].Y);

            setNumerialControl(nudCalibDownX1, CalibrationR.lstCalibCameraDownXY[0].X);
            setNumerialControl(nudCamDownPosR1, CalibrationR.lstCalibCameraDownRC[0].X);
            setNumerialControl(nudCamDownPosC1, CalibrationR.lstCalibCameraDownRC[0].Y);

            setNumerialControl(nudCalibDownX2, CalibrationR.lstCalibCameraDownXY[1].X);
            setNumerialControl(nudCamDownPosR2, CalibrationR.lstCalibCameraDownRC[1].X);
            setNumerialControl(nudCamDownPosC2, CalibrationR.lstCalibCameraDownRC[1].Y);

            setNumerialControl(nudGetCalibPosX, ModelManager.SuctionRParam.DGetPosX);
            setNumerialControl(nudCamUpCalibPosX, CalibrationR.pCalibUpPosXY.X);
            setNumerialControl(nudCamUpCalibPosY, CalibrationR.pCalibUpPosXY.Y);
            setNumerialControl(nudCamUpCalibExposure, CalibrationR.ICalibUpExposure);

            setNumerialControl(nudCamDownCalibPosX, ModelManager.SuctionRParam.DPosCameraDownX);
            setNumerialControl(nudCamDownCalibPosZ, CalibrationR.DCalibDownPosZ);
            setNumerialControl(nudCamDownCalibExposure, CalibrationR.ICalibDownExposure);
            setNumerialControl(nudGetCalibPosZ, CalibrationR.DGetCalibPosZ);
            #endregion
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            setNumerialControl(nudIndex1, ModelManager.SuctionRParam.lstIndex1[index]);
            setNumerialControl(nudIndex2, ModelManager.SuctionRParam.lstIndex2[index]);
            setNumerialControl(nudIndex3, ModelManager.SuctionRParam.lstIndex3[index]);
            setNumerialControl(nudPX1, ModelManager.SuctionRParam.lstP1[index].X);
            setNumerialControl(nudPX2, ModelManager.SuctionRParam.lstP2[index].X);
            setNumerialControl(nudPX3, ModelManager.SuctionRParam.lstP3[index].X);

            setNumerialControl(nudPY1, ModelManager.SuctionRParam.lstP1[index].Y);
            setNumerialControl(nudPY2, ModelManager.SuctionRParam.lstP2[index].Y);
            setNumerialControl(nudPY3, ModelManager.SuctionRParam.lstP3[index].Y);

            setNumerialControl(nudSafeX, ModelManager.SuctionRParam.pSafeXYZ.X);
            setNumerialControl(nudSafeY, ModelManager.SuctionRParam.pSafeXYZ.Y);
            setNumerialControl(nudSafeZ, ModelManager.SuctionRParam.pSafeXYZ.Z);

            setNumerialControl(nudGetTime, ModelManager.SuctionRParam.DGetTime);
            setNumerialControl(nudPosGetX, ModelManager.SuctionRParam.DGetPosX);
            setNumerialControl(nudPosGetZ, ModelManager.SuctionRParam.DGetPosZ);

            setNumerialControl(nudPutTime, ModelManager.SuctionRParam.DPutTime);
            setNumerialControl(nudPosPutX, ModelManager.SuctionRParam.DPutPosX);
            setNumerialControl(nudPosPutZ, ModelManager.SuctionRParam.DPutPosZ);

             setNumerialControl(nudVelPut2, ModelManager.SuctionRParam.VelZ2.Vel);
            setNumerialControl(nudPosPutZ2, ModelManager.SuctionRParam.DPutPosZ2);
            setNumerialControl(nudPosCamDown, ModelManager.SuctionRParam.DPosCameraDownX);

            setNumerialControl(nudExpCamUp, ModelManager.SuctionRParam.IProductUpExposure);
            setNumerialControl(nudExpCamDown, ModelManager.SuctionRParam.IProductDownExposure);
            setNumerialControl(nudPosDiscardX, ModelManager.SuctionRParam.DDiscardX);
            setNumerialControl(nudPosDiscardY, ModelManager.SuctionRParam.DDiscardY);

            cbUseTwo.Checked = ModelManager.SuctionRParam.BTwo;






        }
        public void SaveUI()
        {
            CalibrationR.lstCalibCameraUpXY[0].X = (double)nudCalibUpX1.Value;
            CalibrationR.lstCalibCameraUpXY[0].Y = (double)nudCalibUpY1.Value;
            CalibrationR.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
            CalibrationR.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

            CalibrationR.lstCalibCameraUpXY[1].X = (double)nudCalibUpX2.Value;
            CalibrationR.lstCalibCameraUpXY[1].Y = (double)nudCalibUpY2.Value;
            CalibrationR.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
            CalibrationR.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

            CalibrationR.lstCalibCameraDownXY[0].X = (double)nudCalibDownX1.Value;
            CalibrationR.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
            CalibrationR.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

            CalibrationR.lstCalibCameraDownXY[1].X = (double)nudCalibDownX2.Value;
            CalibrationR.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
            CalibrationR.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;


            ModelManager.SuctionRParam.DGetPosX = (double)nudGetCalibPosX.Value;
            CalibrationR.pCalibUpPosXY.X = (double)nudCamUpCalibPosX.Value;
            CalibrationR.pCalibUpPosXY.Y = (double)nudCamUpCalibPosY.Value;
            CalibrationR.ICalibUpExposure = nudCamUpCalibExposure.Value;

           // CalibrationR.DCalibDownPosX = (double)nudCamDownCalibPosX.Value;
            CalibrationR.DCalibDownPosZ = (double)nudCamDownCalibPosZ.Value;
            CalibrationR.ICalibDownExposure = nudCamDownCalibExposure.Value;
            CalibrationR.DGetCalibPosZ = (double)nudGetCalibPosZ.Value;

            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            ModelManager.SuctionRParam.lstIndex1[index] = (int)nudIndex1.Value;
            ModelManager.SuctionRParam.lstIndex2[index] = (int)nudIndex2.Value;
            ModelManager.SuctionRParam.lstIndex3[index] = (int)nudIndex3.Value;

            ModelManager.SuctionRParam.lstP1[index].X = (double)nudPX1.Value;
            ModelManager.SuctionRParam.lstP2[index].X = (double)nudPX2.Value;
            ModelManager.SuctionRParam.lstP3[index].X = (double)nudPX3.Value;

            ModelManager.SuctionRParam.lstP1[index].Y = (double)nudPY1.Value;
            ModelManager.SuctionRParam.lstP2[index].Y = (double)nudPY2.Value;
            ModelManager.SuctionRParam.lstP3[index].Y = (double)nudPY3.Value;

            ModelManager.SuctionRParam.pSafeXYZ.X = (double)nudSafeX.Value;
            ModelManager.SuctionRParam.pSafeXYZ.Y = (double)nudSafeY.Value;
            ModelManager.SuctionRParam.pSafeXYZ.Z = (double)nudSafeZ.Value;

            ModelManager.SuctionRParam.DGetTime = (double)nudGetTime.Value;
            ModelManager.SuctionRParam.DGetPosX = (double)nudPosGetX.Value;
            ModelManager.SuctionRParam.DGetPosZ = (double)nudPosGetZ.Value;

            ModelManager.SuctionRParam.DPutTime = (double)nudPutTime.Value;
            ModelManager.SuctionRParam.DPutPosX = (double)nudPosPutX.Value;
            ModelManager.SuctionRParam.DPutPosZ = (double)nudPosPutZ.Value;

            ModelManager.SuctionRParam.VelZ2.Vel=(double)nudVelPut2.Value;
            ModelManager.SuctionRParam.DPutPosZ2 = (double)nudPosPutZ2.Value;
            ModelManager.SuctionRParam.DPosCameraDownX = (double)nudPosCamDown.Value;

            ModelManager.SuctionRParam.IProductUpExposure = nudExpCamUp.Value;
            ModelManager.SuctionRParam.IProductDownExposure = nudExpCamDown.Value;
            ModelManager.SuctionRParam.DDiscardX = (double)nudPosDiscardX.Value;
            ModelManager.SuctionRParam.DDiscardY = (double)nudPosDiscardY.Value;


            ModelManager.SuctionRParam.BTwo = cbUseTwo.Checked;
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
        private void setNumerialControl(NumericUpDown nud, int value)
        {
            if (!nud.Focused)
            {
                if (value < (int)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (int)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        private void setNumerialControl(UIDoubleUpDown nud, double value)
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
                nud.Value = value;
            }

        }
        private void setNumerialControl(UIIntegerUpDown nud, int value)
        {
            if (!nud.Focused)
            {
                if (value < (int)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (int)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = value;
            }

        }
        public HWindow GetUpWindow()
        {
            return hWindowControl1.HalconWindow;
        }
        public HWindow GetDownWindow()
        {
            return hWindowControl2.HalconWindow;
        }
        Stopwatch sw = new Stopwatch();
        public void UpdateUI()
        {
            try
            {
                UpdateControl();
                if (frmRotate != null)
                    frmRotate.UpdateUI();

                lbl取料C2轴Pos.Text = mc.dic_Axis[AXIS.C2轴].dPos.ToString("0.00");
                lbl取料X2轴Pos.Text = mc.dic_Axis[AXIS.取料X2轴].dPos.ToString("0.000");
                lbl取料Y2轴Pos.Text = mc.dic_Axis[AXIS.取料Y2轴].dPos.ToString("0.000");
                lbl组装X2轴Pos.Text = mc.dic_Axis[AXIS.组装X2轴].dPos.ToString("0.000");
                lbl组装Z2轴Pos.Text = mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString("0.000");
                lblSuctionGet.On = mc.dic_DO[DO.右吸嘴吸气];
                lblSuctionPut.On = mc.dic_DO[DO.右吸嘴吹气];
                if (Run.runMode == RunMode.取料标定)
                {
                    if(Run.bTestFlag[4])
                        lblTestState.Text = CalibOptRModule.strType;
                }
                else
                {
                    lblTestState.Text = "空闲";
                }

                ShowImageClass.bGrabDataR = cbGrabData.Checked;
                if (cbGrabData.Checked)
                {
                    sw.Start();
                    if (sw.ElapsedMilliseconds > 2000)
                    {
                        ModelManager.CamDownR.SoftTrigger();
                        sw.Restart();
                    }
                }
                lblResolutionUp.Text = "像素比例:" + CalibrationR.GetUpResulotion().ToString("0.0000") + "mm/p 相机角度:" + CalibrationR.GetUpCameraAngle().ToString("0.000");
                lblResolutionDown.Text = "像素比例:" + CalibrationR.GetDownResulotion().ToString("0.0000") + "mm/p 相机角度:" + CalibrationR.GetDownCameraAngle().ToString("0.000");
                cbUseCalibUp.Checked = CalibrationR.bUseUpAngle;
                cbUseCalibDown.Checked = CalibrationR.bUseDownAngle;
                cbContinousUp.Checked = !ModelManager.CamUpR.bTrigger;
                cbContinousDown.Checked = !ModelManager.CamDownR.bTrigger;
            }
            catch (Exception)
            {


            }
        }
        private void 取料X1轴P_Click(object sender, EventArgs e)
        {
            if (rbtnContinue.Checked)
                return;

            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = nudVelHand.Value;
                double newPos = ((posNow + (double)nudDist.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                vel = nudVelHand.Value;
                double newPos = ((posNow - (double)nudDist.Value));
                mc.AbsMove(axis, newPos, vel);
            }
        }

        private void 取料X1轴P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtnHand.Checked)
                return;
            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = nudVelHand.Value;
                double newPos = ((posNow + (double)nudDist.Value));
                mc.VelMove(axis, vel);
            }
            else
            {
                vel = nudVelHand.Value;
                double newPos = ((posNow - (double)nudDist.Value));
                mc.VelMove(axis, -vel);
            }
        }

        private void 取料X1轴P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtnHand.Checked)
                return;

            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.StopAxis(axis);
        }





        private void btnSetTray_Click(object sender, EventArgs e)
        {
            List<int> lst = new List<int>();
            lst.Add(iCurrentTrayIndex+2);


            TestTray test = new TestTray(lst, CommonSet.strProductParamPath + ModelManager.strProductName + "\\Tray.ini");
            test.ShowDialog();
            InitTray();
            SuctionR.InitTray();
            if (iCurrentTrayIndex == ModelManager.SuctionRParam.iCurrentTrayIndex)
            {
                SuctionR.InitTrayPanel(FrmMain.trayPR, iCurrentTrayIndex, CommonSet.ColorInit);
                FrmMain.trayPR.ShowTray(SuctionR.lstTray[iCurrentTrayIndex - 1]);
            }
        }

        private void rbtnLen1_Click(object sender, EventArgs e)
        {
            iCurrentTrayIndex = 1;
            InitTray();

        }

        private void rbtnLen2_Click(object sender, EventArgs e)
        {
            iCurrentTrayIndex = 2;
            InitTray();

        }

        private void nudIndex1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (tray == null)
                return;

            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
            SaveUI();
        }

        private void nudIndex2_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (tray == null)
                return;

            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
            SaveUI();
        }

        private void nudIndex3_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (tray == null)
                return;

            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
            SaveUI();
        }
        private void btnGetCalibUp1_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionRParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX1.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudCalibUpY1.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;

            nudCamUpPosR1.Value = (decimal)ModelManager.SuctionRParam.imgResultUp.CenterRow;
            nudCamUpPosC1.Value = (decimal)ModelManager.SuctionRParam.imgResultUp.CenterColumn;

        }
        private void btnGetCalibUp2_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionRParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX2.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudCalibUpY2.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;

            nudCamUpPosR2.Value = (decimal)ModelManager.SuctionRParam.imgResultUp.CenterRow;
            nudCamUpPosC2.Value = (decimal)ModelManager.SuctionRParam.imgResultUp.CenterColumn;
        }

        private void btnGetCalibDown1_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionRParam.imgResultDown.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibDownX1.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;


            nudCamDownPosR1.Value = (decimal)ModelManager.SuctionRParam.imgResultDown.CenterRow;
            nudCamDownPosC1.Value = (decimal)ModelManager.SuctionRParam.imgResultDown.CenterColumn;
        }

        private void btnGetCalibDown2_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionRParam.imgResultDown.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibDownX2.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;


            nudCamDownPosR2.Value = (decimal)ModelManager.SuctionRParam.imgResultDown.CenterRow;
            nudCamDownPosC2.Value = (decimal)ModelManager.SuctionRParam.imgResultDown.CenterColumn;
        }

        private void btnGetP1_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX1.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudPY1.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;

        }

        private void btnGetP2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX2.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudPY2.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;
        }

        private void btnGetP3_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX3.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudPY3.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;
        }

        private void btnGoP1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.lstP1[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.lstP1[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGoP2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.lstP2[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.lstP2[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGoP3_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.lstP3[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.lstP3[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGetSafeX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeX.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;

        }

        private void btnGetSafeY_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeY.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;
        }

        private void btnGetSafeZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }



        private void btnGetPosX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetX.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
        }

        private void btnGetPosZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }

        private void btnGetPutX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosPutX.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
        }

        private void btnGetPutZ_Click(object sender, EventArgs e)
        {

        }

        private void btnGetPutZ2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosPutZ2.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }

        private void btnGetPosCamDown_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }

            nudPosCamDown.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
        }

        private void cbUseTwo_Click(object sender, EventArgs e)
        {
            ModelManager.SuctionRParam.BTwo = !ModelManager.SuctionRParam.BTwo;
        }

        private void btnGoSafeX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.pSafeXYZ.X, velXY, 0.1, 0.1);

        }

        private void btnGoSafeY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }

            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnGoSafeZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }
        private void btnGoPosX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DGetPosX, velXY, 0.1, 0.1);
        }

        private void btnGoPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在取料位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.DGetPosZ, velZ, 0.1, 0.1);
        }

        private void btnGoPutX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPutPosX, velXY, 0.1, 0.1);
        }

        private void btnGoPutZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.DPutPosZ, velZ, 0.1, 0.1);
        }

        private void btnGoPutZ2_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.DPutPosZ2, velZ, 0.1, 0.1);
        }

        private void btnGoPosCamDown_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }
        private void btnGoPosDiscard_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DDiscardX, velXY, 0.1, 0.1);
        }

        private void btnTriggerCamUpProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpR != null)
                {
                    ModelManager.CamUpR.SetExposure(ModelManager.SuctionRParam.IProductUpExposure);
                    ModelManager.CamUpR.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }


        }

        private void btnSetProductCheckUp_Click(object sender, EventArgs e)
        {
            
            HObject hoImage;
            string strName = "";

            strName = ModelManager.SuctionRParam.strPicCameraUpName;
            hoImage = ModelManager.SuctionRParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "右上相机产品检测";
                ModelManager.SuctionRParam.strPicCameraUpName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckUp_Click(object sender, EventArgs e)
        {
            HObject hImage = ModelManager.SuctionRParam.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            // string strProcessName = suction.strPicCameraUpName;
            ModelManager.SuctionRParam.InitImageUp(ModelManager.SuctionRParam.strPicCameraUpName);

            if (ModelManager.SuctionRParam.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            ModelManager.SuctionRParam.pfUp.SetRun(true);
            ModelManager.SuctionRParam.pfUp.hwin = hWindowControl1.HalconWindow;
            ModelManager.SuctionRParam.actionUp(hImage);
            ModelManager.SuctionRParam.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultUp.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (ModelManager.SuctionRParam.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }

        private void btnTriggerCamDownProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure(ModelManager.SuctionRParam.IProductDownExposure);
                    ModelManager.CamDownR.SoftTrigger();
                }
            }
            catch (Exception)
            {
            }

        }

        private void btnSetProductCheckDown_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = ModelManager.SuctionRParam.strPicDownProduct;
            hoImage = ModelManager.SuctionRParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "右下相机产品检测";
                ModelManager.SuctionRParam.strPicDownProduct = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckDown_Click(object sender, EventArgs e)
        {
            HObject hImage = ModelManager.SuctionRParam.hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            // string strProcessName = suction.strPicCameraUpName;
            ModelManager.SuctionRParam.InitImageDown(ModelManager.SuctionRParam.strPicDownProduct);

            if (ModelManager.SuctionRParam.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            HWindow hwin = hWindowControl2.HalconWindow;
            ModelManager.SuctionRParam.pfDown.SetRun(true);
            ModelManager.SuctionRParam.pfDown.hwin = hwin;
            ModelManager.SuctionRParam.actionDown(hImage);
            ModelManager.SuctionRParam.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (ModelManager.SuctionRParam.imgResultDown.bImageResult)
                CommonSet.disp_message(hwin, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hwin, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
        }

        private void btnTriggerCamUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpR != null)
                {
                    ModelManager.CamUpR.SetExposure(nudCamUpExp.Value);
                    ModelManager.CamUpR.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }

        }

        private void btnSaveUp_Click(object sender, EventArgs e)
        {
            if (ModelManager.SuctionRParam.hImageUP == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "PicLUp" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(ModelManager.SuctionRParam.hImageUP, "bmp", 0, fileName);
            }
        }

        private void btnTriggerCamDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure(nudCamDownExp.Value);
                    ModelManager.CamDownR.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnSaveDown_Click(object sender, EventArgs e)
        {
            if (ModelManager.SuctionRParam.hImageDown == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "PicLUp" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(ModelManager.SuctionRParam.hImageDown, "bmp", 0, fileName);
            }
        }

        private void nudCalibUpX1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();

        }

        private void nudSafeX_ValueChanged(object sender, double value)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void nudCamUpCalibExposure_ValueChanged(object sender, int value)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void cbContinousUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpR != null)
                {
                    ModelManager.CamUpR.SetTriggerMode(!cbContinousUp.Checked);
                }
            }
            catch (Exception)
            {


            }
        }

        private void nudCamUpExp_ValueChanged(object sender, int value)
        {
            try
            {
                if (ModelManager.CamUpR != null)
                {
                    ModelManager.CamUpR.SetExposure(value);
                }
            }
            catch (Exception)
            {


            }
        }

        private void nudCamDownExp_ValueChanged(object sender, int value)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure(value);
                }
            }
            catch (Exception)
            {


            }
        }

        private void cbContinousDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetTriggerMode(!cbContinousDown.Checked);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnGetPosDiscard_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosDiscardX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
        }

       

        private void btnSafeAssemX1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnSafeAssemX2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnSafeAssemZ1_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }

        private void btnSafeAssemZ2_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }

        private void btnSafeGetXY1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.pSafeXYZ.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnSafeGetXY2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.pSafeXYZ.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnSafeBarrelXY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.镜筒X轴, ModelManager.BarrelParam.pSafeXY.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.镜筒Y轴, ModelManager.BarrelParam.pSafeXY.Y, velXY, 0.1, 0.1);
        }

        private void btnGetPosDiscardY_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosDiscardY.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;
        }

        private void btnGoPosDiscardY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.DDiscardY, velXY, 0.1, 0.1);
        }

        public static FrmRotate frmRotate = null;
        private void btnRotateTest_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DPosCameraDownX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在下相机拍照位");
                return;
            }
           
            frmRotate = new FrmRotate(AXIS.C2轴);
            frmRotate.ShowDialog();
            frmRotate = null;
        }

        private void lblSuctionGet_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.右吸嘴吸气, !mc.dic_DO[DO.右吸嘴吸气]);
        }

        private void lblSuctionPut_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.右吸嘴吹气, !mc.dic_DO[DO.右吸嘴吹气]);
        }

        private void nudTestGet_Click(object sender, EventArgs e)
        {
            if (rbtnLen1.Checked)
                TestGetOptRModule.iTray = 1;
            else
                TestGetOptRModule.iTray = 2;
            TestGetOptRModule.pos = (int)nudGetNum.Value;
            TestGetOptRModule.strType = "取料";
            Run.testGetRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.testGetRModule, RunMode.取料测试, 2, "取料");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnPutTest_Click(object sender, EventArgs e)
        {
            if (rbtnLen1.Checked)
                TestGetOptRModule.iTray = 1;
            else
                TestGetOptRModule.iTray = 2;
            TestGetOptRModule.pos = (int)nudGetNum.Value;
            TestGetOptRModule.strType = "放料";
            Run.testGetRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.testGetRModule, RunMode.取料测试, 2, "放料");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnGetCalibPosX_Click(object sender, EventArgs e)
        {

            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetX.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
            nudGetCalibPosX.Value = (decimal)nudPosGetX.Value;
        }

        private void btnGoCalibPosX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DGetPosX, velXY, 0.1, 0.1);
        }

        private void btnGetCalibPosZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudGetCalibPosZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }

        private void btnGoCalibPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在取料位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, (double)nudGetCalibPosZ.Value, velZ, 0.1, 0.1);
        }

        private void btnGoCamUpCalibPos_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X2轴, (double)nudCamUpCalibPosX.Value, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, (double)nudCamUpCalibPosY.Value, velXY, 0.1, 0.1);
        }

        private void btnTriggerUpCalib_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpR != null)
                {
                    ModelManager.CamUpR.SetExposure(CalibrationR.ICalibUpExposure);
                    ModelManager.CamUpR.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }

        }

        private void btnCalibUpCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = CalibrationR.strPicUpCalibName;
            hoImage = ModelManager.SuctionRParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "右上相机标定";
                CalibrationR.strPicUpCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnGoCamDownCalibPos_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnGoCamDownCalibPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在下相机拍照位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, (double)nudCamDownCalibPosZ.Value, velZ, 0.1, 0.1);
        }

        private void btnTriggerDownCalib_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure(CalibrationR.ICalibDownExposure);
                    ModelManager.CamDownR.SoftTrigger();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCalibDownCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = CalibrationR.strPicDownCalibName;
            hoImage = ModelManager.SuctionRParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "右下相机标定";
                CalibrationR.strPicDownCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnTestGet_Click(object sender, EventArgs e)
        {
            CalibOptRModule.strType = "取标定块";
            CalibOptRModule.bContinue = cbContinue.Checked;
            CalibOptRModule.iTimes = (int)nudTimes.Value;
            Run.calibRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibRModule, RunMode.取料标定, 4, "取标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnTestPut_Click(object sender, EventArgs e)
        {
            CalibOptRModule.strType = "放标定块";
            CalibOptRModule.bContinue = cbContinue.Checked;
            CalibOptRModule.iTimes = (int)nudTimes.Value;
            Run.calibRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibRModule, RunMode.取料标定, 4, "放标定块");
            ftd.ShowDialog();
            ftd = null;
        }
        #region"缩放显示"
        private int startX, startY, endX, endY;
        public void DisplayPart(HWindow hwin, int iPosX, int iPosY, double multi = 1.01)
        {
            HTuple hWidth, hHeight;
            hWidth = 2448;
            Height = 2048;
            if ((iPosX + (endX - iPosX) / multi) - (iPosX - (iPosX - startX) / multi) > 3 * hWidth)
            {
                return;
            }
            double scale = 4 / 3;
            startX = (int)(iPosX - (iPosX - startX) / multi);
            startY = (int)(iPosY - (iPosY - startY) / (multi * scale));
            endX = (int)(iPosX + (endX - iPosX) / multi);
            endY = (int)(iPosY + (endY - iPosY) / (multi * scale));

        }
        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindow hwin = hWindowControl1.HalconWindow;
                int hv_MouseRow, hv_MouseColumn, hv_Button;

                hwin.GetMposition(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMpositionSubPix(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMposition(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                if (e.Delta > 0)
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 1.3);

                }
                else
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 0.7);
                }
                HOperatorSet.ClearWindow(hwin);
                HOperatorSet.SetPart(hwin, startY, startX, endY, endX);
                HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageUP, hwin);

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}


                HWindow hwin = hWindowControl1.HalconWindow;
                double hv_MouseRow, hv_MouseColumn;
                int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);


                if (hv_Button == 4)
                {
                    HTuple hWidth = 2448;
                    HTuple hHeight = 2048;
                    // HOperatorSet.GetImageSize(CommonSet.dic_BarrelSuction[iSuctionNum].hImageCenter, out hWidth, out hHeight);
                    HOperatorSet.SetPart(hwin, 0, 0, hHeight.I, hWidth.I);
                    startX = 0;
                    startY = 0;
                    endX = hWidth.I;
                    endY = hHeight.I;

                    HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageUP, hwin);

                }

            }
            catch (Exception ex) { }
        }

        private void hWindowControl1_Enter(object sender, EventArgs e)
        {
            hWindowControl1.Focus();
            HTuple hWidth = 2448;
            HTuple hHeight = 2048;
            startX = 0;
            startY = 0;
            endX = hWidth.I;
            endY = hHeight.I;
        }

        private void hWindowControl2_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {


                HWindow hwin = hWindowControl2.HalconWindow;
                double hv_MouseRow, hv_MouseColumn;
                int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);


                if (hv_Button == 4)
                {
                    HTuple hWidth = 2448;
                    HTuple hHeight = 2048;
                    // HOperatorSet.GetImageSize(CommonSet.dic_BarrelSuction[iSuctionNum].hImageCenter, out hWidth, out hHeight);
                    HOperatorSet.SetPart(hwin, 0, 0, hHeight.I, hWidth.I);
                    startX = 0;
                    startY = 0;
                    endX = hWidth.I;
                    endY = hHeight.I;

                    HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);

                }

            }
            catch (Exception ex) { }
        }

        private void hWindowControl2_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindow hwin = hWindowControl2.HalconWindow;
                int hv_MouseRow, hv_MouseColumn, hv_Button;

                hwin.GetMposition(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMpositionSubPix(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMposition(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                if (e.Delta > 0)
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 1.3);

                }
                else
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 0.7);
                }
                HOperatorSet.ClearWindow(hwin);
                HOperatorSet.SetPart(hwin, startY, startX, endY, endX);
                HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        private void hWindowControl2_Enter(object sender, EventArgs e)
        {
            hWindowControl2.Focus();
            HTuple hWidth = 2448;
            HTuple hHeight = 2048;
            startX = 0;
            startY = 0;
            endX = hWidth.I;
            endY = hHeight.I;
        }
        #endregion

        private void cbUseCalibUp_Click(object sender, EventArgs e)
        {
            CalibrationR.bUseUpAngle = !CalibrationR.bUseUpAngle;
        }

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            CalibrationR.bUseDownAngle = !CalibrationR.bUseDownAngle;
        }
    }
}
