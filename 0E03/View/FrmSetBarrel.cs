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
    public partial class FrmSetBarrel : UIPage
    {
        double velXY = 100;
        double velZ = 50;
        int iCurrentTrayIndex = 1;
        bool bInit = false;
        MotionCard mc = null;
        Tray.Tray tray = null;
        public static HWindow hwinUp;
        public FrmSetBarrel()
        {
            InitializeComponent();
        }

        private void FrmSetBarrel_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            hwinUp = hWindowControl1.HalconWindow;
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
            Tray.Tray tempTray = TrayFactory.getTrayFactory((iCurrentTrayIndex+4).ToString());
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



            if (ModelManager.BarrelParam.lstIndex1[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.BarrelParam.lstIndex1[iCurrentTrayIndex - 1] = count;
                nudIndex1.Value = ModelManager.BarrelParam.lstIndex1[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex1.Value = ModelManager.BarrelParam.lstIndex1[iCurrentTrayIndex - 1];
            }

            if (ModelManager.BarrelParam.lstIndex2[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.BarrelParam.lstIndex2[iCurrentTrayIndex - 1] = count;
                nudIndex2.Value = ModelManager.BarrelParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex2.Value = ModelManager.BarrelParam.lstIndex2[iCurrentTrayIndex - 1];
            }
            if (ModelManager.BarrelParam.lstIndex3[iCurrentTrayIndex - 1] > count)
            {
                ModelManager.BarrelParam.lstIndex3[iCurrentTrayIndex - 1] = count;
                nudIndex3.Value = ModelManager.BarrelParam.lstIndex3[iCurrentTrayIndex - 1];
            }
            else
            {
                nudIndex3.Value = ModelManager.BarrelParam.lstIndex3[iCurrentTrayIndex - 1];
            }


            tray.setNumColor(ModelManager.BarrelParam.lstIndex1[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.BarrelParam.lstIndex2[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.setNumColor(ModelManager.BarrelParam.lstIndex3[iCurrentTrayIndex - 1], CommonSet.ColorNotUse);
            tray.updateColor();


            UpdateControl();
            bInit = true;

        }
        public void UpdateControl()
        {
            #region"标定"
            setNumerialControl(nudCalibUpX1, CalibrationBL.lstCalibCameraUpXY[0].X);
            setNumerialControl(nudCalibUpY1, CalibrationBL.lstCalibCameraUpXY[0].Y);
            setNumerialControl(nudCamUpPosR1, CalibrationBL.lstCalibCameraUpRC[0].X);
            setNumerialControl(nudCamUpPosC1, CalibrationBL.lstCalibCameraUpRC[0].Y);

            setNumerialControl(nudCalibUpX2, CalibrationBL.lstCalibCameraUpXY[1].X);
            setNumerialControl(nudCalibUpY2, CalibrationBL.lstCalibCameraUpXY[1].Y);
            setNumerialControl(nudCamUpPosR2, CalibrationBL.lstCalibCameraUpRC[1].X);
            setNumerialControl(nudCamUpPosC2, CalibrationBL.lstCalibCameraUpRC[1].Y);

           

           
            #endregion
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            setNumerialControl(nudIndex1, ModelManager.BarrelParam.lstIndex1[index]);
            setNumerialControl(nudIndex2, ModelManager.BarrelParam.lstIndex2[index]);
            setNumerialControl(nudIndex3, ModelManager.BarrelParam.lstIndex3[index]);
            setNumerialControl(nudPX1, ModelManager.BarrelParam.lstP1[index].X);
            setNumerialControl(nudPX2, ModelManager.BarrelParam.lstP2[index].X);
            setNumerialControl(nudPX3, ModelManager.BarrelParam.lstP3[index].X);

            setNumerialControl(nudPY1, ModelManager.BarrelParam.lstP1[index].Y);
            setNumerialControl(nudPY2, ModelManager.BarrelParam.lstP2[index].Y);
            setNumerialControl(nudPY3, ModelManager.BarrelParam.lstP3[index].Y);
            setNumerialControl(nudExpCamUp, ModelManager.BarrelParam.IProductUpExposure);

            setNumerialControl(nudGetCalibPosX, ModelManager.SuctionLParam.DPutPosX);
            setNumerialControl(nudGetCalibPosZ, CalibrationBL.DGetCalibPosZ);
            setNumerialControl(nudCamUpCalibExposure, CalibrationBL.ICalibUpExposure);
            setNumerialControl(nudCamDownCalibPosX, ModelManager.SuctionLParam.DPosCameraDownX);
            setNumerialControl(nudCamDownCalibPosZ, CalibrationBL.DCalibDownPosZ);
            setNumerialControl(nudCamDownCalibExposure, CalibrationBL.ICalibDownExposure);

            setNumerialControl(nudGetCalibPosX2, ModelManager.SuctionRParam.DPutPosX);
            setNumerialControl(nudGetCalibPosZ2, CalibrationBR.DGetCalibPosZ);
            setNumerialControl(nudCamUpCalibExposure2, CalibrationBR.ICalibUpExposure);
            setNumerialControl(nudCamDownCalibPosX2, ModelManager.SuctionRParam.DPosCameraDownX);
            setNumerialControl(nudCamDownCalibPosZ2, CalibrationBR.DCalibDownPosZ);
            setNumerialControl(nudCamDownCalibExposure2, CalibrationBR.ICalibDownExposure);
        }
        public void SaveUI()
        {
            CalibrationBL.lstCalibCameraUpXY[0].X = (double)nudCalibUpX1.Value;
            CalibrationBL.lstCalibCameraUpXY[0].Y = (double)nudCalibUpY1.Value;
            CalibrationBL.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
            CalibrationBL.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

            CalibrationBL.lstCalibCameraUpXY[1].X = (double)nudCalibUpX2.Value;
            CalibrationBL.lstCalibCameraUpXY[1].Y = (double)nudCalibUpY2.Value;
            CalibrationBL.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
            CalibrationBL.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            ModelManager.BarrelParam.lstIndex1[index] = (int)nudIndex1.Value;
            ModelManager.BarrelParam.lstIndex2[index] = (int)nudIndex2.Value;
            ModelManager.BarrelParam.lstIndex3[index] = (int)nudIndex3.Value;

            ModelManager.BarrelParam.lstP1[index].X = (double)nudPX1.Value;
            ModelManager.BarrelParam.lstP2[index].X = (double)nudPX2.Value;
            ModelManager.BarrelParam.lstP3[index].X = (double)nudPX3.Value;

            ModelManager.BarrelParam.lstP1[index].Y = (double)nudPY1.Value;
            ModelManager.BarrelParam.lstP2[index].Y = (double)nudPY2.Value;
            ModelManager.BarrelParam.lstP3[index].Y = (double)nudPY3.Value;

            ModelManager.BarrelParam.IProductUpExposure = (int)nudExpCamUp.Value;

            CalibrationBL.DGetCalibPosZ = (double)nudGetCalibPosZ.Value;
            CalibrationBL.ICalibUpExposure = (int)nudCamUpCalibExposure.Value;
            CalibrationBL.DCalibDownPosZ = (double)nudCamDownCalibPosZ.Value;
            CalibrationBL.ICalibDownExposure = (int)nudCamDownCalibExposure.Value;

            CalibrationBR.DGetCalibPosZ = (double)nudGetCalibPosZ2.Value;
            CalibrationBR.ICalibUpExposure = (int)nudCamUpCalibExposure2.Value;
            CalibrationBR.DCalibDownPosZ = (double)nudCamDownCalibPosZ2.Value;
            CalibrationBR.ICalibDownExposure = (int)nudCamDownCalibExposure2.Value;

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

        private void btnGetCalibUp1_Click(object sender, EventArgs e)
        {
            if (!ModelManager.BarrelParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX1.Value = (decimal)mc.dic_Axis[AXIS.镜筒X轴].dPos;
            nudCalibUpY1.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;

            nudCamUpPosR1.Value = (decimal)ModelManager.BarrelParam.imgResultUp.CenterRow;
            nudCamUpPosC1.Value = (decimal)ModelManager.BarrelParam.imgResultUp.CenterColumn;
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

        private void btnSetTray_Click(object sender, EventArgs e)
        {
            List<int> lst = new List<int>();
            lst.Add(iCurrentTrayIndex + 4);


            TestTray test = new TestTray(lst, CommonSet.strProductParamPath + ModelManager.strProductName + "\\Tray.ini");
            test.ShowDialog();
            InitTray();
            Barrel.InitTray();
            if (iCurrentTrayIndex == ModelManager.BarrelParam.iCurrentTrayIndex)
            {
                Barrel.InitTrayPanel(FrmMain.trayPB, iCurrentTrayIndex, CommonSet.ColorInit);
                FrmMain.trayPB.ShowTray(Barrel.lstTray[iCurrentTrayIndex - 1]);
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

        private void btnTriggerCamUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure((double)nudCamUpExp.Value);
                    ModelManager.CamBarrel.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }
        }

        private void cbContinousUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure((double)nudCamUpExp.Value);
                    ModelManager.CamBarrel.SetTriggerMode(!cbContinousUp.Checked);
                }
            }
            catch (Exception)
            {


            }
        }

        private void nudCamUpExp_ValueChanged(object sender, int value)
        {
            //try
            //{
            //    if (ModelManager.CamBarrel != null)
            //    {
            //        ModelManager.CamBarrel.SetExposure(value);
            //    }
            //}
            //catch (Exception)
            //{


            //}
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

        private void btnGoP1_Click(object sender, EventArgs e)
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
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.镜筒X轴, ModelManager.BarrelParam.lstP1[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.镜筒Y轴, ModelManager.BarrelParam.lstP1[index].Y, velXY, 0.1, 0.1);

        }

        private void btnGoP2_Click(object sender, EventArgs e)
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
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.镜筒X轴, ModelManager.BarrelParam.lstP2[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.镜筒Y轴, ModelManager.BarrelParam.lstP2[index].Y, velXY, 0.1, 0.1);

        }

        private void btnGoP3_Click(object sender, EventArgs e)
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
            int index = 0;
            if (rbtnLen1.Checked)
                index = 0;
            else
                index = 1;
            mc.AbsMove(AXIS.镜筒X轴, ModelManager.BarrelParam.lstP3[index].X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.镜筒Y轴, ModelManager.BarrelParam.lstP3[index].Y, velXY, 0.1, 0.1);
        }

        private void btnGetP1_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX1.Value = (decimal)mc.dic_Axis[AXIS.镜筒X轴].dPos;
            nudPY1.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;
        }

        private void btnGetP2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX2.Value = (decimal)mc.dic_Axis[AXIS.镜筒X轴].dPos;
            nudPY2.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;
        }

        private void btnGetP3_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudPX3.Value = (decimal)mc.dic_Axis[AXIS.镜筒X轴].dPos;
            nudPY3.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;
        }
        Stopwatch sw = new Stopwatch();
        public void UpdateUI()
        {
            try
            {
                if (uiTabControl1.SelectedIndex == 3)
                {
                    AxisC = AXIS.C2轴;
                    AxisX = AXIS.组装X2轴;
                    AxisZ = AXIS.组装Z2轴;
                    uiLabel24.Text = "吸真空右";
                    uiLabel25.Text = "破真空右";
                    lblSuctionGet.On = mc.dic_DO[DO.右吸嘴吸气];
                    lblSuctionPut.On = mc.dic_DO[DO.右吸嘴吹气];
                    cbContinousDown.Checked = !ModelManager.CamDownR.bTrigger;
                }
                else
                {
                    AxisC = AXIS.C1轴;
                    AxisX = AXIS.组装X1轴;
                    AxisZ = AXIS.组装Z1轴;
                    uiLabel24.Text = "吸真空左";
                    uiLabel25.Text = "破真空左";
                    lblSuctionGet.On = mc.dic_DO[DO.左吸嘴吸气];
                    lblSuctionPut.On = mc.dic_DO[DO.左吸嘴吹气];
                    cbContinousDown.Checked = !ModelManager.CamDownL.bTrigger;
                }
                label9.Text = AxisZ.ToString();
                label10.Text = AxisC.ToString();
                label11.Text = AxisX.ToString();
                lbl镜筒X轴Pos.Text = mc.dic_Axis[AXIS.镜筒X轴].dPos.ToString("0.000");
                lbl镜筒Y轴Pos.Text = mc.dic_Axis[AXIS.镜筒Y轴].dPos.ToString("0.000");
                lbl取料Z1轴Pos.Text = mc.dic_Axis[AxisZ].dPos.ToString("0.000");
                lbl取料C1轴Pos.Text = mc.dic_Axis[AxisC].dPos.ToString("0.000");
                lbl组装X1轴Pos.Text = mc.dic_Axis[AxisX].dPos.ToString("0.000");
                UpdateControl();
                lblResolutionUp.Text = "像素比例:" + CalibrationBL.GetUpResulotion().ToString("0.0000") + "mm/p 相机角度:" + CalibrationBL.GetUpCameraAngle().ToString("0.000");

                cbUseCalibUp.Checked = CalibrationBL.bUseUpAngle;
                cbContinousUp.Checked = !ModelManager.CamBarrel.bTrigger;

                ShowImageClass.bGrabDataB = cbGrabData.Checked;
                if (cbGrabData.Checked)
                {
                    sw.Start();
                    if (sw.ElapsedMilliseconds > 2000)
                    {
                        btnTriggerCamUp_Click(null, null);
                        sw.Restart();
                    }
                }
            }
            catch (Exception)
            {
                
               
            }
        }

        private void 镜筒X轴P_Click(object sender, EventArgs e)
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

        private void 镜筒X轴P_MouseDown(object sender, MouseEventArgs e)
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

        private void 镜筒X轴P_MouseUp(object sender, MouseEventArgs e)
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

        public AXIS AxisX, AxisC, AxisZ;
        private void Z轴P_Click(object sender, EventArgs e)
        {
            if (rbtnContinue.Checked)
                return;

            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            
            AXIS axis;
            if (name.Contains("X"))
                axis = AxisX;
            else if(name.Contains("C"))
                axis = AxisC;
            else
                axis = AxisZ;
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

        private void nudCalibUpX1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void btnTriggerCamUpProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure(ModelManager.BarrelParam.IProductUpExposure);
                    ModelManager.CamBarrel.SoftTrigger();
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

            strName = ModelManager.BarrelParam.strPicCameraUpName;
            hoImage = ModelManager.BarrelParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "镜筒检测";
                ModelManager.BarrelParam.strPicCameraUpName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckUp_Click(object sender, EventArgs e)
        {
            HObject hImage = ModelManager.BarrelParam.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            // string strProcessName = suction.strPicCameraUpName;
            ModelManager.BarrelParam.InitImageUp(ModelManager.BarrelParam.strPicCameraUpName);

            if (ModelManager.BarrelParam.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            ModelManager.BarrelParam.pfUp.SetRun(true);
            ModelManager.BarrelParam.pfUp.hwin = hWindowControl1.HalconWindow;
            ModelManager.BarrelParam.actionUp(hImage);
            ModelManager.BarrelParam.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.BarrelParam.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.BarrelParam.imgResultUp.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (ModelManager.BarrelParam.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }

        private void nudExpCamUp_ValueChanged(object sender, int value)
        {
            try
            {
                if (!bInit)
                    return;
                //if (ModelManager.CamBarrel != null)
                //{
                //    ModelManager.CamBarrel.SetExposure(value);
                //}
                SaveUI();
            }
            catch (Exception)
            {


            }
        }

        private void btnGetCalibPosX_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            ModelManager.SuctionLParam.DPutPosX = mc.dic_Axis[AXIS.组装X1轴].dPos;
            nudGetCalibPosX.Value = (decimal)ModelManager.SuctionLParam.DPutPosX;
        }

        private void btnGoCalibPosX_Click(object sender, EventArgs e)
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
            if (Math.Abs(mc.dic_Axis[AXIS.组装X1轴].dPos - ModelManager.SuctionLParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X1轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z1轴, (double)nudGetCalibPosZ.Value, velZ, 0.1, 0.1);
        }

        private void btnGoCamUpCalibPos_Click(object sender, EventArgs e)
        {

        }

        private void btnTriggerUpCalib_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure(CalibrationBL.ICalibUpExposure);
                    ModelManager.CamBarrel.SoftTrigger();
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

            strName = CalibrationBL.strPicUpCalibName;
            hoImage = ModelManager.BarrelParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "镜筒上相机标定";
                CalibrationBL.strPicUpCalibName = strName;
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
                    ModelManager.CamDownL.SetExposure(CalibrationBL.ICalibDownExposure);
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

            strName = CalibrationBL.strPicDownCalibName;
            hoImage = ModelManager.SuctionLParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "左下相机标定";
                CalibrationBL.strPicDownCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnTestGet_Click(object sender, EventArgs e)
        {
            CalibBarrelLModule.strType = "取标定块";
            CalibBarrelLModule.bContinue = cbContinue.Checked;
            CalibBarrelLModule.iTimes = (int)nudTimes.Value;
            Run.calibBLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibBLModule, RunMode.组装标定, 5, "取标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnTestPut_Click(object sender, EventArgs e)
        {
            CalibBarrelLModule.strType = "放标定块";
            CalibBarrelLModule.bContinue = cbContinue.Checked;
            CalibBarrelLModule.iTimes = (int)nudTimes.Value;
            Run.calibBLModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibBLModule, RunMode.组装标定, 5, "放标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnGoCalibPosX2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            if (mc.dic_Axis[AXIS.组装X1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.X + 0.1)
            {
                MessageBox.Show("组装X1轴超出安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPutPosX, velXY, 0.1, 0.1);
        }

        private void uiSymbolButton6_Click(object sender, EventArgs e)
        {

        }

        private void btnGetCalibPosZ2_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("是否获取当前位置？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            nudGetCalibPosZ2.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }

        private void btnGoCalibPosZ2_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DPutPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在组装位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, (double)nudGetCalibPosZ2.Value, velZ, 0.1, 0.1);
        }

        private void btnTriggerUpCalib2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure(CalibrationBR.ICalibUpExposure);
                    ModelManager.CamBarrel.SoftTrigger();
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCalibUpCheckSet2_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = CalibrationBR.strPicUpCalibName;
            hoImage = ModelManager.BarrelParam.hImageUP;
            if (strName.Equals(""))
            {
                strName = "镜筒上相机标定";
                CalibrationBR.strPicUpCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnGoCamDownCalibPos2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPosCameraDownX, velXY, 0.1, 0.1);

        }

        private void btnGoCamDownCalibPosZ2_Click(object sender, EventArgs e)
        {
            if (Math.Abs(mc.dic_Axis[AXIS.组装X2轴].dPos - ModelManager.SuctionRParam.DGetPosX) > 0.01)
            {
                MessageBox.Show("组装X2轴不在下相机拍照位");
                return;
            }
            mc.AbsMove(AXIS.组装Z2轴, (double)nudCamDownCalibPosZ2.Value, velZ, 0.1, 0.1);
        }

        private void btnTriggerDownCalib2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure(CalibrationBR.ICalibDownExposure);
                    ModelManager.CamDownR.SoftTrigger();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCalibDownCheckSet2_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = CalibrationBR.strPicDownCalibName;
            hoImage = ModelManager.SuctionRParam.hImageDown;
            if (strName.Equals(""))
            {
                strName = "右下相机标定";
                CalibrationBR.strPicDownCalibName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnTestGet2_Click(object sender, EventArgs e)
        {
            CalibBarrelRModule.strType = "取标定块";
            CalibBarrelRModule.bContinue = cbContinue2.Checked;
            CalibBarrelRModule.iTimes = (int)nudTimes2.Value;
            Run.calibBRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibBRModule, RunMode.组装标定, 6, "取标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnTestPut2_Click(object sender, EventArgs e)
        {
            CalibBarrelRModule.strType = "放标定块";
            CalibBarrelRModule.bContinue = cbContinue2.Checked;
            CalibBarrelRModule.iTimes = (int)nudTimes2.Value;
            Run.calibBRModule.IStep = 0;

            FrmTestDialog ftd = new FrmTestDialog(Run.calibBRModule, RunMode.组装标定, 6, "放标定块");
            ftd.ShowDialog();
            ftd = null;
        }

        private void btnSaveUp_Click(object sender, EventArgs e)
        {
            if (ModelManager.BarrelParam.hImageUP == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "PicBarrel" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(ModelManager.BarrelParam.hImageUP, "bmp", 0, fileName);
            }
        }

        private void btnTriggerCamDown_Click(object sender, EventArgs e)
        {
            if (AxisC == AXIS.C1轴)
            {

                try
                {
                    if (ModelManager.CamDownL != null)
                    {
                        ModelManager.CamDownL.SetExposure((double)nudCamDownExp.Value);
                        ModelManager.CamDownL.SoftTrigger();
                    }
                }
                catch (Exception)
                {


                }
            }
            else
            {
                try
                {
                    if (ModelManager.CamDownR != null)
                    {
                        ModelManager.CamDownR.SetExposure((double)nudCamDownExp.Value);
                        ModelManager.CamDownR.SoftTrigger();
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        private void Z轴P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtnHand.Checked)
                return;
            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
           
            AXIS axis;
            if (name.Contains("X"))
                axis = AxisX;
            else if (name.Contains("C"))
                axis = AxisC;
            else
                axis = AxisZ;
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

        private void Z轴P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtnHand.Checked)
                return;

            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            AXIS axis;
            if (name.Contains("X"))
                axis = AxisX;
            else if (name.Contains("C"))
                axis = AxisC;
            else
                axis = AxisZ;
            mc.StopAxis(axis);
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
                    if(AxisX == AXIS.组装X1轴)
                        HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                    else
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
                if (AxisX == AXIS.组装X1轴)
                    HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                else
                    HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }

        private void btnGetCalibUp2_Click(object sender, EventArgs e)
        {
            if (!ModelManager.BarrelParam.imgResultUp.bImageResult)
            {
                MessageBox.Show("图像检测结果NG");
                return;
            }
            nudCalibUpX2.Value = (decimal)mc.dic_Axis[AXIS.镜筒X轴].dPos;
            nudCalibUpY2.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;

            nudCamUpPosR2.Value = (decimal)ModelManager.BarrelParam.imgResultUp.CenterRow;
            nudCamUpPosC2.Value = (decimal)ModelManager.BarrelParam.imgResultUp.CenterColumn;
        }

        private void nudCamDownExp_ValueChanged(object sender, EventArgs e)
        {
            if (AxisC == AXIS.C1轴)
            {

                try
                {
                    if (ModelManager.CamDownL != null)
                    {
                        ModelManager.CamDownL.SetExposure((double)nudCamDownExp.Value);
                      
                    }
                }
                catch (Exception)
                {


                }
            }
            else
            {
                try
                {
                    if (ModelManager.CamDownR != null)
                    {
                        ModelManager.CamDownR.SetExposure((double)nudCamDownExp.Value);
                       
                    }
                }
                catch (Exception)
                {


                }
            }
        }

        private void cbContinousDown_Click(object sender, EventArgs e)
        {
            if (AxisC == AXIS.C1轴)
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
            else
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
        }

        private void nudCamUpExp_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure((double)nudCamUpExp.Value);
                   
                }
            }
            catch (Exception)
            {


            }
        }

        private void nudCamUpCalibExposure_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure((double)nudCamUpCalibExposure.Value);

                }
                SaveUI();
            }
            catch (Exception)
            {
            }
        }

        private void nudCamDownCalibExposure_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SetExposure((double)nudCamDownCalibExposure.Value);

                }
                SaveUI();
            }
            catch (Exception)
            {
            }
        }

        private void nudCamUpCalibExposure2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                if (ModelManager.CamBarrel != null)
                {
                    ModelManager.CamBarrel.SetExposure((double)nudCamUpCalibExposure2.Value);

                }
                SaveUI();
            }
            catch (Exception)
            {
            }
        }

        private void nudCamDownCalibExposure2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SetExposure((double)nudCamDownCalibExposure2.Value);

                }
                SaveUI();
            }
            catch (Exception)
            {
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

        private void lblSuctionGet_Click(object sender, EventArgs e)
        {
           if(AxisC == AXIS.C1轴)
                 mc.setDO(DO.左吸嘴吸气, !mc.dic_DO[DO.左吸嘴吸气]);
            else
                mc.setDO(DO.右吸嘴吸气, !mc.dic_DO[DO.右吸嘴吸气]);
        }

        private void lblSuctionPut_Click(object sender, EventArgs e)
        {
            if (AxisC == AXIS.C1轴)
                mc.setDO(DO.左吸嘴吹气, !mc.dic_DO[DO.左吸嘴吹气]);
            else
                mc.setDO(DO.右吸嘴吹气, !mc.dic_DO[DO.右吸嘴吹气]);
        }

        private void btnSaveDown_Click(object sender, EventArgs e)
        {

        }

        private void cbUseCalibUp_Click(object sender, EventArgs e)
        {
            CalibrationBL.bUseUpAngle = !CalibrationBL.bUseUpAngle;
        }


    }
}
