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
using Motion;
using Tray;
using HalconDotNet;
using ImageProcess;
using System.Diagnostics;
namespace _0E03
{
    public partial class FrmSetLeft : UIPage
    {
        double velXY = 100;
        double velZ = 50;
        int iCurrentTrayIndex = 1;
        bool bInit = false;
        MotionCard mc = null;
        Tray.Tray tray = null;
        public FrmSetLeft()
        {
            InitializeComponent();
        }

        private void FrmSetLeft_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            //CommonSet.WriteInfo("加载设置参数界面1");
            InitTray();
            UpdateControl();
            bInit = true;
        }
        public HWindow GetUpWindow()
        {
            return hWindowControl1.HalconWindow;
        }
        public HWindow GetDownWindow()
        {
            return hWindowControl2.HalconWindow;
        }
        public void InitTray()
        {
            bInit = false;
            //当前吸笔无盘时,不加载盘
            if (iCurrentTrayIndex == 0)
                return;
            tray = null;
            Tray.Tray tempTray = TrayFactory.getTrayFactory(iCurrentTrayIndex.ToString());
            tempTray.RemoveDelegate();
            tray = tempTray.Clone();
            tempTray.updateColor += FrmMain.trayPL.UpdateColor;


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



            if (ModelManager.SuctionLParam.lstIndex1[iCurrentTrayIndex-1] > count)
            {
                ModelManager.SuctionLParam.lstIndex1[iCurrentTrayIndex - 1] = count;
                nudIndex1.Value = ModelManager.SuctionLParam.lstIndex1[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex1.Value = ModelManager.SuctionLParam.lstIndex1[iCurrentTrayIndex - 1];
            }

            if (ModelManager.SuctionLParam.lstIndex2[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.SuctionLParam.lstIndex2[iCurrentTrayIndex - 1] = count;
                nudIndex2.Value = ModelManager.SuctionLParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex2.Value = ModelManager.SuctionLParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            if (ModelManager.SuctionLParam.lstIndex3[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.SuctionLParam.lstIndex3[iCurrentTrayIndex - 1] = count;
                nudIndex3.Value = ModelManager.SuctionLParam.lstIndex3[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex3.Value = ModelManager.SuctionLParam.lstIndex3[iCurrentTrayIndex - 1];
            }

              
            tray.setNumColor(ModelManager.SuctionLParam.lstIndex1[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.SuctionLParam.lstIndex2[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.SuctionLParam.lstIndex3[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.updateColor();
           
           
            UpdateControl();
            bInit = true;

        }
        public void UpdateControl()
        {
            #region"标定"
            setNumerialControl(nudCalibUpX1, CalibrationL.lstCalibCameraUpXY[0].X);
            setNumerialControl(nudCalibUpY1, CalibrationL.lstCalibCameraUpXY[0].Y);
            setNumerialControl(nudCamUpPosR1, CalibrationL.lstCalibCameraUpRC[0].X);
            setNumerialControl(nudCamUpPosC1, CalibrationL.lstCalibCameraUpRC[0].Y);

            setNumerialControl(nudCalibUpX2, CalibrationL.lstCalibCameraUpXY[1].X);
            setNumerialControl(nudCalibUpY2, CalibrationL.lstCalibCameraUpXY[1].Y);
            setNumerialControl(nudCamUpPosR2, CalibrationL.lstCalibCameraUpRC[1].X);
            setNumerialControl(nudCamUpPosC2, CalibrationL.lstCalibCameraUpRC[1].Y);

            setNumerialControl(nudCalibDownX1, CalibrationL.lstCalibCameraDownXY[0].X);
            setNumerialControl(nudCamDownPosR1, CalibrationL.lstCalibCameraDownRC[0].X);
            setNumerialControl(nudCamDownPosC1, CalibrationL.lstCalibCameraDownRC[0].Y);

            setNumerialControl(nudCalibDownX2, CalibrationL.lstCalibCameraDownXY[1].X);
            setNumerialControl(nudCamDownPosR2, CalibrationL.lstCalibCameraDownRC[1].X);
            setNumerialControl(nudCamDownPosC2, CalibrationL.lstCalibCameraDownRC[1].Y);

            setNumerialControl(nudGetCalibPosX, ModelManager.SuctionLParam.DGetPosX);
            setNumerialControl(nudCamUpCalibPosX, CalibrationL.pCalibUpPosXY.X);
            setNumerialControl(nudCamUpCalibPosY, CalibrationL.pCalibUpPosXY.Y);
            setNumerialControl(nudCamUpCalibExposure, CalibrationL.ICalibUpExposure);

            setNumerialControl(nudCamDownCalibPosX, ModelManager.SuctionLParam.DPosCameraDownX);
            setNumerialControl(nudCamDownCalibPosZ, CalibrationL.DCalibDownPosZ);
            setNumerialControl(nudCamDownCalibExposure, CalibrationL.ICalibDownExposure);
            setNumerialControl(nudGetCalibPosZ, CalibrationL.DGetCalibPosZ);
            #endregion
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            setNumerialControl(nudIndex1, ModelManager.SuctionLParam.lstIndex1[index]);
            setNumerialControl(nudIndex2, ModelManager.SuctionLParam.lstIndex2[index]);
            setNumerialControl(nudIndex3, ModelManager.SuctionLParam.lstIndex3[index]);
            setNumerialControl(nudPX1, ModelManager.SuctionLParam.lstP1[index].X);
            setNumerialControl(nudPX2, ModelManager.SuctionLParam.lstP2[index].X);
            setNumerialControl(nudPX3, ModelManager.SuctionLParam.lstP3[index].X);

            setNumerialControl(nudPY1, ModelManager.SuctionLParam.lstP1[index].Y);
            setNumerialControl(nudPY2, ModelManager.SuctionLParam.lstP2[index].Y);
            setNumerialControl(nudPY3, ModelManager.SuctionLParam.lstP3[index].Y);

            setNumerialControl(nudSafeX, ModelManager.SuctionLParam.pSafeXYZ.X);
            setNumerialControl(nudSafeY, ModelManager.SuctionLParam.pSafeXYZ.Y);
            setNumerialControl(nudSafeZ, ModelManager.SuctionLParam.pSafeXYZ.Z);

            setNumerialControl(nudGetTime, ModelManager.SuctionLParam.DGetTime);
            setNumerialControl(nudPosGetX, ModelManager.SuctionLParam.DGetPosX);
            setNumerialControl(nudPosGetZ, ModelManager.SuctionLParam.DGetPosZ);

            setNumerialControl(nudPutTime, ModelManager.SuctionLParam.DPutTime);
            setNumerialControl(nudPosPutX, ModelManager.SuctionLParam.DPutPosX);
            setNumerialControl(nudPosPutZ, ModelManager.SuctionLParam.DPutPosZ);

            setNumerialControl(nudVelPut2, ModelManager.SuctionLParam.VelZ2.Vel);
            setNumerialControl(nudPosPutZ2, ModelManager.SuctionLParam.DPutPosZ2);
            setNumerialControl(nudPosCamDown, ModelManager.SuctionLParam.DPosCameraDownX);

            setNumerialControl(nudExpCamUp, ModelManager.SuctionLParam.IProductUpExposure);
            setNumerialControl(nudExpCamDown, ModelManager.SuctionLParam.IProductDownExposure);
            setNumerialControl(nudPosDiscardX, ModelManager.SuctionLParam.DDiscardX);
            setNumerialControl(nudPosDiscardY, ModelManager.SuctionLParam.DDiscardY);

            cbUseTwo.Checked = ModelManager.SuctionLParam.BTwo;


           
            


        }
        public void SaveUI()
        {
            CalibrationL.lstCalibCameraUpXY[0].X = (double)nudCalibUpX1.Value;
            CalibrationL.lstCalibCameraUpXY[0].Y = (double)nudCalibUpY1.Value;
            CalibrationL.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
            CalibrationL.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

            CalibrationL.lstCalibCameraUpXY[1].X = (double)nudCalibUpX2.Value;
            CalibrationL.lstCalibCameraUpXY[1].Y = (double)nudCalibUpY2.Value;
            CalibrationL.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
            CalibrationL.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

            CalibrationL.lstCalibCameraDownXY[0].X = (double)nudCalibDownX1.Value;
            CalibrationL.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
            CalibrationL.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

            CalibrationL.lstCalibCameraDownXY[1].X = (double)nudCalibDownX2.Value;
            CalibrationL.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
            CalibrationL.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;


            ModelManager.SuctionLParam.DGetPosX = (double)nudGetCalibPosX.Value;
            CalibrationL.pCalibUpPosXY.X = (double)nudCamUpCalibPosX.Value;
            CalibrationL.pCalibUpPosXY.Y = (double)nudCamUpCalibPosY.Value;
            CalibrationL.ICalibUpExposure = nudCamUpCalibExposure.Value;

            CalibrationL.DCalibDownPosX=(double)nudCamDownCalibPosX.Value;
            CalibrationL.DCalibDownPosZ = (double)nudCamDownCalibPosZ.Value;
            CalibrationL.ICalibDownExposure = nudCamDownCalibExposure.Value;
            CalibrationL.DGetCalibPosZ = (double)nudGetCalibPosZ.Value;
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            ModelManager.SuctionLParam.lstIndex1[index] = (int)nudIndex1.Value;
            ModelManager.SuctionLParam.lstIndex2[index] = (int)nudIndex2.Value;
            ModelManager.SuctionLParam.lstIndex3[index] = (int)nudIndex3.Value;

            ModelManager.SuctionLParam.lstP1[index].X = (double)nudPX1.Value;
            ModelManager.SuctionLParam.lstP2[index].X = (double)nudPX2.Value;
            ModelManager.SuctionLParam.lstP3[index].X = (double)nudPX3.Value;

            ModelManager.SuctionLParam.lstP1[index].Y = (double)nudPY1.Value;
            ModelManager.SuctionLParam.lstP2[index].Y = (double)nudPY2.Value;
            ModelManager.SuctionLParam.lstP3[index].Y = (double)nudPY3.Value;

           ModelManager.SuctionLParam.pSafeXYZ.X = (double)nudSafeX.Value;
           ModelManager.SuctionLParam.pSafeXYZ.Y = (double)nudSafeY.Value;
           ModelManager.SuctionLParam.pSafeXYZ.Z = (double)nudSafeZ.Value;

            ModelManager.SuctionLParam.DGetTime=(double)nudGetTime.Value;
           ModelManager.SuctionLParam.DGetPosX=(double)nudPosGetX.Value;
           ModelManager.SuctionLParam.DGetPosZ = (double)nudPosGetZ.Value;

           ModelManager.SuctionLParam.DPutTime = (double)nudPutTime.Value;
           ModelManager.SuctionLParam.DPutPosX = (double)nudPosPutX.Value;
           ModelManager.SuctionLParam.DPutPosZ = (double)nudPosPutZ.Value;

           ModelManager.SuctionLParam.VelZ2.Vel=(double)nudVelPut2.Value;
           ModelManager.SuctionLParam.DPutPosZ2=(double)nudPosPutZ2.Value;
           ModelManager.SuctionLParam.DPosCameraDownX=(double)nudPosCamDown.Value;

           ModelManager.SuctionLParam.IProductUpExposure=nudExpCamUp.Value;
           ModelManager.SuctionLParam.IProductDownExposure=nudExpCamDown.Value;
           ModelManager.SuctionLParam.DDiscardX = (double)nudPosDiscardX.Value;
           ModelManager.SuctionLParam.DDiscardY = (double)nudPosDiscardY.Value;

            ModelManager.SuctionLParam.BTwo=cbUseTwo.Checked;
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
        Stopwatch sw = new Stopwatch();
        public void UpdateUI()
        {
            try
            {
                UpdateControl();
                if (frmRotate != null)
                {
                    frmRotate.UpdateUI();
                }
                lbl取料C1轴Pos.Text = mc.dic_Axis[AXIS.C1轴].dPos.ToString("0.00");
                lbl取料X1轴Pos.Text = mc.dic_Axis[AXIS.取料X1轴].dPos.ToString("0.000");
                lbl取料Y1轴Pos.Text = mc.dic_Axis[AXIS.取料Y1轴].dPos.ToString("0.000");
                lbl组装X1轴Pos.Text = mc.dic_Axis[AXIS.组装X1轴].dPos.ToString("0.000");
                lbl取料Z1轴Pos.Text = mc.dic_Axis[AXIS.组装Z1轴].dPos.ToString("0.000");
                lblSuctionGet.On = mc.dic_DO[DO.左吸嘴吸气];
                lblSuctionPut.On = mc.dic_DO[DO.左吸嘴吹气];
                //InitDataBindings();
                if (Run.runMode == RunMode.取料标定)
                {
                    if (Run.bTestFlag[3])
                        lblTestState.Text = CalibOptLModule.strType;
                }
                else
                {
                    lblTestState.Text = "空闲";
                }
                ShowImageClass.bGrabDataL = cbGrabData.Checked;
                if (cbGrabData.Checked)
                {
                    sw.Start();
                    if (sw.ElapsedMilliseconds > 2000)
                    {
                        ModelManager.CamDownL.SoftTrigger();
                        sw.Restart();
                    }
                }
                cbUseCalibDown.Checked = CalibrationL.bUseDownAngle;
                cbUseCalibUp.Checked = CalibrationL.bUseUpAngle;
                lblResolutionUp.Text = "像素比例:" + CalibrationL.GetUpResulotion().ToString("0.0000")+"mm/p 相机角度:"+CalibrationL.GetUpCameraAngle().ToString("0.000");
                lblResolutionDown.Text = "像素比例:" + CalibrationL.GetDownResulotion().ToString("0.0000") + "mm/p 相机角度:" + CalibrationL.GetDownCameraAngle().ToString("0.000");
                cbContinousUp.Checked = !ModelManager.CamUpL.bTrigger;
                cbContinousDown.Checked = !ModelManager.CamDownL.bTrigger;

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
               
                mc.VelMove(axis, vel);
            }
            else
            {
                vel = nudVelHand.Value;

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
            lst.Add(iCurrentTrayIndex);


            TestTray test = new TestTray(lst, CommonSet.strProductParamPath + ModelManager.strProductName + "\\Tray.ini");
            test.ShowDialog();
            InitTray();
            SuctionL.InitTray();
            if (iCurrentTrayIndex == ModelManager.SuctionLParam.iCurrentTrayIndex)
            {
                SuctionL.InitTrayPanel(FrmMain.trayPL, iCurrentTrayIndex, CommonSet.ColorInit);
                FrmMain.trayPL.ShowTray(SuctionL.lstTray[iCurrentTrayIndex - 1]);
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
            if (!ModelManager.SuctionLParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX1.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudCalibUpY1.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;

            nudCamUpPosR1.Value = (decimal)ModelManager.SuctionLParam.imgResultUp.CenterRow;
            nudCamUpPosC1.Value = (decimal)ModelManager.SuctionLParam.imgResultUp.CenterColumn;

        }
        private void btnGetCalibUp2_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionLParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX2.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudCalibUpY2.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;

            nudCamUpPosR2.Value = (decimal)ModelManager.SuctionLParam.imgResultUp.CenterRow;
            nudCamUpPosC2.Value = (decimal)ModelManager.SuctionLParam.imgResultUp.CenterColumn;
        }

        private void btnGetCalibDown1_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionLParam.imgResultDown.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibDownX1.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;


            nudCamDownPosR1.Value = (decimal)ModelManager.SuctionLParam.imgResultDown.CenterRow;
            nudCamDownPosC1.Value = (decimal)ModelManager.SuctionLParam.imgResultDown.CenterColumn;
        }

        private void btnGetCalibDown2_Click(object sender, EventArgs e)
        {
            if (!ModelManager.SuctionLParam.imgResultDown.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibDownX2.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;


            nudCamDownPosR2.Value = (decimal)ModelManager.SuctionLParam.imgResultDown.CenterRow;
            nudCamDownPosC2.Value = (decimal)ModelManager.SuctionLParam.imgResultDown.CenterColumn;
        }

        private void btnGetP1_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？","提示",MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX1.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudPY1.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;

        }

        private void btnGetP2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX2.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudPY2.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
        }

        private void btnGetP3_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX3.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudPY3.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
        }

        private void btnGoP1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.lstP1[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.lstP1[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGoP2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.lstP2[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.lstP2[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGoP3_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.lstP3[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.lstP3[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGetSafeX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeX.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;

        }

        private void btnGetSafeY_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeY.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
        }

        private void btnGetSafeZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudSafeZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

      

        private void btnGetPosX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
        }

        private void btnGetPosZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

        private void btnGetPutX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosPutX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
        }

        private void btnGetPutZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosPutZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

        private void btnGetPutZ2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosPutZ2.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

        private void btnGetPosCamDown_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }

            nudPosCamDown.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
        }

        private void cbUseTwo_Click(object sender, EventArgs e)
        {
            ModelManager.SuctionLParam.BTwo = !ModelManager.SuctionLParam.BTwo;
        }

        private void btnGoSafeX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.pSafeXYZ.X, velXY, 0.1, 0.1);
          
        }

        private void btnGoSafeY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnGoSafeZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }
        private void btnGoPosX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DGetPosX, velXY, 0.1, 0.1);
        }

        private void btnGoPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在取料位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.DGetPosZ, velZ, 0.1, 0.1);
        }

        private void btnGoPutX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            if (mc.dic_Axis[AXIS.组装X2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.X + 0.1)
            {
                MessageBox.Show("组装X2轴超出安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DPutPosX, velXY, 0.1, 0.1);
        }

        private void btnGoPutZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.DPutPosZ, velZ, 0.1, 0.1);
        }

        private void btnGoPutZ2_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.DPutPosZ2, velZ, 0.1, 0.1);
        }

        private void btnGoPosCamDown_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }
        private void btnGoPosDiscard_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DDiscardX, velXY, 0.1, 0.1);
        }
        private void btnTriggerCamUpProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpL != null)
                {
                    ModelManager.CamUpL.SetExposure(ModelManager.SuctionLParam.IProductUpExposure);
                    ModelManager.CamUpL.SoftTrigger();
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

            strName = ModelManager.SuctionLParam.strPicCameraUpName;
             hoImage = ModelManager.SuctionLParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "左上相机产品检测";
                ModelManager.SuctionLParam.strPicCameraUpName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckUp_Click(object sender, EventArgs e)
        {
            HObject hImage = ModelManager.SuctionLParam.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            // string strProcessName = suction.strPicCameraUpName;
            ModelManager.SuctionLParam.InitImageUp(ModelManager.SuctionLParam.strPicCameraUpName);

            if (ModelManager.SuctionLParam.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            ModelManager.SuctionLParam.pfUp.SetRun(true);
            ModelManager.SuctionLParam.pfUp.hwin = hWindowControl1.HalconWindow;
            ModelManager.SuctionLParam.actionUp(hImage);
            ModelManager.SuctionLParam.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultUp.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (ModelManager.SuctionLParam.imgResultUp.bImageResult)
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
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetExposure(ModelManager.SuctionLParam.IProductDownExposure);
                    ModelManager.CamDownL.SoftTrigger();
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

            strName = ModelManager.SuctionLParam.strPicDownProduct;
            hoImage = ModelManager.SuctionLParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "左下相机产品检测";
                ModelManager.SuctionLParam.strPicDownProduct = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckDown_Click(object sender, EventArgs e)
        {
            HObject hImage = ModelManager.SuctionLParam.hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            // string strProcessName = suction.strPicCameraUpName;
            ModelManager.SuctionLParam.InitImageDown(ModelManager.SuctionLParam.strPicDownProduct);

            if (ModelManager.SuctionLParam.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            HWindow hwin = hWindowControl2.HalconWindow;
            ModelManager.SuctionLParam.pfDown.SetRun(true);
            ModelManager.SuctionLParam.pfDown.hwin = hwin ;
            ModelManager.SuctionLParam.actionDown(hImage);
            ModelManager.SuctionLParam.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (ModelManager.SuctionLParam.imgResultDown.bImageResult)
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
                if (ModelManager.CamUpL != null)
                {
                    ModelManager.CamUpL.SetExposure(nudCamUpExp.Value);
                    ModelManager.CamUpL.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }
          
        }

        private void btnSaveUp_Click(object sender, EventArgs e)
        {
            if (ModelManager.SuctionLParam.hImageUP == null)
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
                HOperatorSet.WriteImage(ModelManager.SuctionLParam.hImageUP, "bmp", 0, fileName);
            }
        }

        private void btnTriggerCamDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetExposure(nudCamDownExp.Value);
                    ModelManager.CamDownL.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnSaveDown_Click(object sender, EventArgs e)
        {
            if (ModelManager.SuctionLParam.hImageDown == null)
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
                HOperatorSet.WriteImage(ModelManager.SuctionLParam.hImageDown, "bmp", 0, fileName);
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
                if (ModelManager.CamUpL != null)
                {
                    ModelManager.CamUpL.SetTriggerMode(!cbContinousUp.Checked);
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
                if (ModelManager.CamUpL != null)
                {
                    ModelManager.CamUpL.SetExposure(value);
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
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetTriggerMode(!cbContinousDown.Checked);
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
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetExposure(value);
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

        private void btnGoPosDiscardY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.DDiscardY, velXY, 0.1, 0.1);
        }

        private void btnGetPosDiscardY_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosDiscardY.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
        }
        public static FrmRotate frmRotate = null;
        private void btnRotateTest_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DPosCameraDownX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在下相机拍照位");
                return;
            }
           
            frmRotate = new FrmRotate(AXIS.C1轴);
            frmRotate.ShowDialog();
            frmRotate = null;
        }

        private void lblSuctionGet_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.左吸嘴吸气, !mc.dic_DO[DO.左吸嘴吸气]);
        }

        private void lblSuctionPut_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.左吸嘴吹气, !mc.dic_DO[DO.左吸嘴吹气]);
        }

        private void nudTestGet_Click(object sender, EventArgs e)
        {
            if (rbtnLen1.Checked)
                TestGetOptLModule.iTray = 1;
            else
                TestGetOptLModule.iTray = 2;
            TestGetOptLModule.pos = (int)nudGetNum.Value;
            TestGetOptLModule.strType = "取料";
            Run.testGetLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.testGetLModule, RunMode.取料测试, 1,"取料");
            ftd.ShowDialog();
            ftd = null;
            //Task t = new Task(()=> {
            //    TestGetOptLModule test = new TestGetOptLModule();
            //    test.IStep = 0;
            //    while (test.IStep != (test.lstAction.Count - 1))
            //    {
            //        test.MakeAction();
            //        System.Threading.Thread.Sleep(10);
            //    }

            //});
            //t.Start();
        }

        private void btnPutTest_Click(object sender, EventArgs e)
        {
            if (rbtnLen1.Checked)
                TestGetOptLModule.iTray = 1;
            else
                TestGetOptLModule.iTray = 2;
            TestGetOptLModule.pos = (int)nudGetNum.Value;
            TestGetOptLModule.strType = "放料";
            Run.testGetLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.testGetLModule, RunMode.取料测试, 1, "放料");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnGetCalibPosX_Click(object sender, EventArgs e)
        {
            
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPosGetX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
            nudGetCalibPosX.Value = (decimal)nudPosGetX.Value;
        }

        private void btnGoCalibPosX_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DGetPosX, velXY, 0.1, 0.1);
        }

        private void btnGetCalibPosZ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudGetCalibPosZ.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

        private void btnGoCalibPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在取料位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, (double)nudGetCalibPosZ.Value, velZ, 0.1, 0.1);
        }

        private void btnGoCamUpCalibPos_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X1轴, (double)nudCamUpCalibPosX.Value, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, (double)nudCamUpCalibPosY.Value, velXY, 0.1, 0.1);
        }

        private void btnTriggerUpCalib_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamUpL != null)
                {
                    ModelManager.CamUpL.SetExposure(CalibrationL.ICalibUpExposure);
                    ModelManager.CamUpL.SoftTrigger();
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

            strName = CalibrationL.strPicUpCalibName;
            hoImage = ModelManager.SuctionLParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "左上相机标定";
                CalibrationL.strPicUpCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnGoCamDownCalibPos_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnGoCamDownCalibPosZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在下相机拍照位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, (double)nudCamDownCalibPosZ.Value, velZ, 0.1, 0.1);
        }

        private void btnTriggerDownCalib_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetExposure(CalibrationL.ICalibDownExposure);
                    ModelManager.CamDownL.SoftTrigger();
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

            strName = CalibrationL.strPicDownCalibName;
            hoImage = ModelManager.SuctionLParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "左下相机标定";
               CalibrationL.strPicDownCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnTestGet_Click(object sender, EventArgs e)
        {
            CalibOptLModule.strType = "取标定块";
            CalibOptLModule.bContinue = cbContinue.Checked;
            CalibOptLModule.iTimes = (int)nudTimes.Value;
            Run.calibLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibLModule, RunMode.取料标定, 3, "取标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnTestPut_Click(object sender, EventArgs e)
        {
            CalibOptLModule.strType = "放标定块";
            CalibOptLModule.bContinue = cbContinue.Checked;
            CalibOptLModule.iTimes = (int)nudTimes.Value;
            Run.calibLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibLModule, RunMode.取料标定, 3, "放标定块");
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
                HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageUP, hwin);

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
                   
                        HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageUP, hwin);
                    
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
                    
                    HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                   
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
                HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);

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
            CalibrationL.bUseUpAngle = !CalibrationL.bUseUpAngle;
        }

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            CalibrationL.bUseDownAngle = !CalibrationL.bUseDownAngle;
        }
    }
}
