using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tray;
using Motion;
using CameraSet;
using HalconDotNet;
using ImageProcess;

namespace Assembly
{
    public partial class BarrelUI : UserControl
    {

        Tray.Tray tray = null;
        TrayPanel trayPanel1 = null;
        public int iSuctionNum = 1;//当前吸笔序号
       
        public int iCurrentTrayIndex = 0;//当前盘索引
        public int iListIndex = 0;//存储到OptSuction所有List索引
        MotionCard mc = null;
        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        bool bInit = false;//代表是否初始化完成
        // ProductSuction suction = null;

        AXIS axisX, axisY, axisZ;
        ICamera camUp, camDown;
        public FrmRotate frmRotate = null;
        public BarrelUI(TrayPanel panel)
        {
            trayPanel1 = panel;
            camDown = CommonSet.camDownD1;
            camUp = CommonSet.camUpD1;
            axisX = AXIS.点胶X轴;
            axisY = AXIS.镜筒Y轴;
            axisZ = AXIS.点胶Z轴;
           
            InitializeComponent();
        }

        private void btnTraySelect_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(cbBarrelIndex.Text);
            if (i != iCurrentTrayIndex)
            {
                iCurrentTrayIndex = i;
                InitTray();
            }
        }

        private void BarrelUI_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            if (trayPanel1 != null)
            {
                trayPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                trayPanel1.Location = new System.Drawing.Point(3, 11);
                // this.panel2.Name = "panel2";
                trayPanel1.Size = new System.Drawing.Size(326, 200);
                trayPanel1.Margin = new System.Windows.Forms.Padding(1);
                groupBox1.Controls.Add(trayPanel1);
            }
            //trayPanel1.TabIndex = 8;
            LoadCbBarrel();
            CommonSet.hPixelToAxisCalibWin = hWindowControl1.HalconWindow;
            InitTray();
        }
        private void LoadCbBarrel()
        {
            cbBarrelIndex.Items.Clear();
           
                int[] arr = CommonSet.dic_BarrelSuction[iSuctionNum].lstTrayNum.ToArray();
                if (arr.Length > 0)
                {
                    iCurrentTrayIndex = arr[0];
                    for (int i = 0; i < arr.Length; i++)
                        cbBarrelIndex.Items.Add(arr[i]);
                    cbBarrelIndex.Text = iCurrentTrayIndex.ToString();
                }
                else
                {
                    iCurrentTrayIndex = 0;
                    cbBarrelIndex.Text = "无";
                }
            
            

        }

        //初始化托盘参数
        public void InitTray()
        {
            bInit = false;
            //当前吸笔无盘时,不加载盘
            if (iCurrentTrayIndex == 0)
                return;
            tray = null;
            tray = TrayFactory.getTrayFactory(iCurrentTrayIndex.ToString()).Clone();
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
           
            BarrelSuction suction = CommonSet.dic_BarrelSuction[iSuctionNum];
            iListIndex = suction.lstTrayNum.IndexOf(iCurrentTrayIndex);
            if (suction.lstIndex1[iListIndex] > count)
            {
                suction.lstIndex1[iListIndex] = count;
                nudIndex1.Value = suction.lstIndex1[iListIndex];
            }
            else
            {
                nudIndex1.Value = suction.lstIndex1[iListIndex];
            }

            if (suction.lstIndex2[iListIndex] > count)
            {
                suction.lstIndex2[iListIndex] = count;
                nudIndex2.Value = suction.lstIndex2[iListIndex];
            }
            else
            {
                nudIndex2.Value = suction.lstIndex2[iListIndex];
            }
            if (suction.lstIndex3[iListIndex] > count)
            {
                suction.lstIndex3[iListIndex] = count;
                nudIndex3.Value = suction.lstIndex3[iListIndex];
            }
            else
            {
                nudIndex3.Value = suction.lstIndex3[iListIndex];
            }


            tray.setNumColor(suction.lstIndex1[iListIndex], CommonSet.ColorNotUse);
            tray.setNumColor(suction.lstIndex2[iListIndex], CommonSet.ColorNotUse);
            tray.setNumColor(suction.lstIndex3[iListIndex], CommonSet.ColorNotUse);
            tray.updateColor();
            CommonSet.dic_BarrelSuction[iSuctionNum].EndPos = CommonSet.dic_BarrelSuction[iSuctionNum].GetCount();

            UpdateControl();
            bInit = true;

        }
        bool bAutoUp = true;
        bool bAutoDown = true;
        private string strHead1 = "已标定值\r\n序号  X     Y       Row     Column \r\n";
        private string strHead2 = "当前标定值\r\n序号  X     Y       Row     Column \r\n";
        public void updateUI()
        {
            try
            {

                if (rbtnSuction1.Checked)
                {
                    groupBox1.Text = "托盘(镜筒吸笔)";
                }
                else
                {
                    groupBox1.Text = "托盘(成品吸笔)";
                }
                if (cbContinusCamDown.Checked && (Run.runMode == RunMode.手动))
                {
                    if (CommonSet.camDownD1 != null)
                    {
                        CommonSet.camDownD1.SetExposure(BarrelSuction.dExposureTimeDown);
                        CommonSet.camDownD1.SetGain(BarrelSuction.dGainDown);
                        bAutoDown = !bAutoDown;
                        mc.setDO(DO.点胶下相机, bAutoDown);
                    }
                }
                if (cbContinousCamUp.Checked && (Run.runMode == RunMode.手动))
                {
                    if (CommonSet.camUpD1 != null)
                    {
                        CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                        CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                        bAutoUp = !bAutoUp;
                        mc.setDO(DO.点胶上相机, bAutoUp);
                       
                    }
                }


                lblCurrentTrayIndex.Text = iCurrentTrayIndex.ToString();


               // barrelListTray1.SetStaion(iStation);
                barrelListTray1.SetSuction(iSuctionNum);
                barrelListTray1.UpdateUI();
                //nudAssemX1.Value = (decimal)suction.pAssembly.X;
                //nudAssemY1.Value = (decimal)suction.pAssembly.Y;

                //lblSuctionCenter.Text = "R:" + suction.dSuctionCenterR.ToString("0.000") + "\r\n" +
                //                   "C:" + suction.dSuctionCenterC.ToString("0.000");
                //lblCenterPos.Text = "R:" + CommonSet.dic_ProductSuction[iSuctionNum].dFlashCalibRow.ToString("0.000") + "\r\n" +
                //                   "C:" + CommonSet.dic_ProductSuction[iSuctionNum].dFlashCalibColumn.ToString("0.000");
              
                  
                lblCurrentSum.Text = CommonSet.dic_BarrelSuction[iSuctionNum].GetCount().ToString();

                nudTestNum.Maximum = (decimal)CommonSet.dic_BarrelSuction[iSuctionNum].GetCount();
                lblBarrelYPos.Text = mc.dic_Axis[AXIS.镜筒Y轴].dPos.ToString("0.000");
                lblGluePosC.Text = mc.dic_Axis[AXIS.点胶C轴].dPos.ToString("0.0");
                lblGluePosX.Text = mc.dic_Axis[AXIS.点胶X轴].dPos.ToString("0.000");
                lblGluePosY.Text = mc.dic_Axis[AXIS.点胶Y轴].dPos.ToString("0.000");
                lblGluePosZ.Text = mc.dic_Axis[AXIS.点胶Z轴].dPos.ToString("0.000");
                panel7.Enabled = rbtProductA3.Checked;

                //btnTrigger.Enabled = CommonSet.camDown1.bTrigger;
               if(bInit)
                 UpdateControl();
               txtCalib.Text = "点胶" + strHead1 + GetString(BarrelSuction.lstCalibCameraUpXY, BarrelSuction.lstCalibCameraUpRC);
               txtCalibNow.Text = strHead2 + GetString(CalibModule.lstPointXY, CalibModule.lstPointRC);
               lblCurrentAxisPos.Text = "X:" + mc.dic_Axis[AXIS.点胶X轴].dPos.ToString("0.000") + "Y:" + mc.dic_Axis[AXIS.点胶Y轴].dPos.ToString("0.000");
               lblRotateCenter.Text = "R:" + BarrelSuction.dRoateCenterRow.ToString("0.000") + "  " + "C:" + BarrelSuction.dRoateCenterColumn.ToString("0.000");

               if (frmRotate != null)
               {
                   frmRotate.UpdateUI();
               }
            }
            catch (Exception)
            {


            }


        }
        private string GetString(List<Point> lstXY, List<Point> lstRC)
        {
            StringBuilder sb = new StringBuilder();
            if (lstXY.Count == lstRC.Count)
            {
                int count = lstRC.Count;
                for (int i = 0; i < count; i++)
                {
                    sb.Append(i.ToString("0") + "    " + lstXY[i].X.ToString("0.000") + "  " + lstXY[i].Y.ToString("0.000") + "   " + lstRC[i].X.ToString("0.000") + "   " + lstRC[i].Y.ToString("0.000"));
                    sb.Append("\r\n");
                }
            }
            return sb.ToString();
        }

        private void btnTraySet_Click(object sender, EventArgs e)
        {
            CommonSet.frmBAndTR = new FrmBarrelTrayRelation();
            CommonSet.frmBAndTR.ShowDialog();
            CommonSet.frmBAndTR = null;
            LoadCbBarrel();
            InitTray();
        }
        public HWindow GetUpWindow()
        {
            return hWindowControl1.HalconWindow;
        }
        public HWindow GetDownWindow()
        {
            return hWindowControl2.HalconWindow;
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
        public void UpdateControl()
        {
           
                var optSuction = CommonSet.dic_BarrelSuction[iSuctionNum];
                setNumerialControl(nudIndex1, optSuction.lstIndex1[iListIndex]);
                setNumerialControl(nudIndex2, optSuction.lstIndex2[iListIndex]);
                setNumerialControl(nudIndex3, optSuction.lstIndex3[iListIndex]);
               
                setNumerialControl(nudPX1, optSuction.lstP1[iListIndex].X);
                setNumerialControl(nudPY1, optSuction.lstP1[iListIndex].Y);

                setNumerialControl(nudPX2, optSuction.lstP2[iListIndex].X);
                setNumerialControl(nudPY2, optSuction.lstP2[iListIndex].Y);

                setNumerialControl(nudPX3, optSuction.lstP3[iListIndex].X);
                setNumerialControl(nudPY3, optSuction.lstP3[iListIndex].Y);

               
                
                setNumerialControl(nudCamUpExposure, BarrelSuction.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, BarrelSuction.dGainUp);
                setNumerialControl(nudCamUpLight, BarrelSuction.dLightUp);

                setNumerialControl(nudCamDownExposure, BarrelSuction.dExposureTimeDown);
                setNumerialControl(nudGainCamDown, BarrelSuction.dGainDown);
                setNumerialControl(nudCamDownLight, BarrelSuction.dLightDown);

                setNumerialControl(nudCamDownPos,BarrelSuction.dCameraPos);
                setNumerialControl(nudGetTime, BarrelSuction.DGetTime);
                setNumerialControl(nudGetPosZ, BarrelSuction.DGetPosZ);
                setNumerialControl(nudGetLenTime, BarrelSuction.dGetLenTime);
                


                setNumerialControl(nudPutAssem1PosX, BarrelSuction.pPutBarrel1.X);
                setNumerialControl(nudPutAssem1PosY, BarrelSuction.pPutBarrel1.Y);
                setNumerialControl(nudPutAssem2PosX, BarrelSuction.pPutBarrel2.X);
                setNumerialControl(nudPutAssem2PosY, BarrelSuction.pPutBarrel2.Y);
                setNumerialControl(nudPutAssem1PosZ, BarrelSuction.pPutBarrel1.Z);
                setNumerialControl(nudPutAssem2PosZ, BarrelSuction.pPutBarrel2.Z);

                setNumerialControl(nudGetAssem1PosX, BarrelSuction.pGetLen1.X);
                setNumerialControl(nudGetAssem1PosY, BarrelSuction.pGetLen1.Y);
                setNumerialControl(nudGetAssem2PosX, BarrelSuction.pGetLen2.X);
                setNumerialControl(nudGetAssem2PosY, BarrelSuction.pGetLen2.Y);
                setNumerialControl(nudGetAssem1PosZ, BarrelSuction.pGetLen1.Z);
                setNumerialControl(nudGetAssem2PosZ, BarrelSuction.pGetLen2.Z);

                setNumerialControl(nudPutTime, BarrelSuction.DPutTime);
                setNumerialControl(nudGluePutX, BarrelSuction.pPutLen.X);
                setNumerialControl(nudGluePutY, BarrelSuction.pPutLen.Y);
                setNumerialControl(nudGluePutZ, BarrelSuction.pPutLen.Z);
                setNumerialControl(nudGlueCamX, BarrelSuction.pCamGlue.X);
                setNumerialControl(nudGlueCamY, BarrelSuction.pCamGlue.Y);
                setNumerialControl(nudGlueCamZ, BarrelSuction.pCamGlue.Z);
                setNumerialControl(nudGlueX, BarrelSuction.pGlue.X);
                setNumerialControl(nudGlueY, BarrelSuction.pGlue.Y);
                setNumerialControl(nudGlueZ, BarrelSuction.pGlue.Z);
                setNumerialControl(nudPutPosZ, BarrelSuction.DPutPosZ);
                setNumerialControl(nudUVX, BarrelSuction.pUV.X);
                setNumerialControl(nudUVY, BarrelSuction.pUV.Y);
                setNumerialControl(nudUVTime, BarrelSuction.dUVTime);
                setNumerialControl(nudPutAssem1PosZ, BarrelSuction.pPutBarrel1.Z);
                setNumerialControl(nudGlueLenPosX, BarrelSuction.pCalibLenPos.X);
                setNumerialControl(nudGlueLenPosY, BarrelSuction.pCalibLenPos.Y);

                setNumerialControl(nudProductGluePutX, BarrelSuction.pPutProductGlue.X);
                setNumerialControl(nudProductGluePutY, BarrelSuction.pPutProductGlue.Y);
                setNumerialControl(nudProductGluePutZ, BarrelSuction.pPutProductGlue.Z);
                setNumerialControl(nudProductGluePutC, BarrelSuction.pPutProductGlue.Theta);
                setNumerialControl(nudCalibX, BarrelSuction.pPicGlueCalib.X);
                setNumerialControl(nudCalibY, BarrelSuction.pPicGlueCalib.Y);
                setNumerialControl(nudGain, BarrelSuction.dCalibGain);
               

        }
        //保存界面的参数
        public void SaveUI()
        {

                var optSuction = CommonSet.dic_BarrelSuction[iSuctionNum];
                optSuction.lstIndex1[iListIndex] = (int)nudIndex1.Value;
                optSuction.lstIndex2[iListIndex] = (int)nudIndex2.Value;
                optSuction.lstIndex3[iListIndex] = (int)nudIndex3.Value;

                Point p = new Point();
                p.X = (double)nudPX1.Value;
                p.Y = (double)nudPY1.Value;
                optSuction.lstP1[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX2.Value;
                p.Y = (double)nudPY2.Value;
                optSuction.lstP2[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX3.Value;
                p.Y = (double)nudPY3.Value;
                optSuction.lstP3[iListIndex] = p;
                //optSuction.lstP1[iListIndex].X = (double)nudPX1.Value;
                //optSuction.lstP1[iListIndex].Y = (double)nudPY1.Value;

                //optSuction.lstP2[iListIndex].X = (double)nudPX2.Value;
                //optSuction.lstP2[iListIndex].Y = (double)nudPY2.Value;

                //optSuction.lstP3[iListIndex].X = (double)nudPX3.Value;
                //optSuction.lstP3[iListIndex].Y = (double)nudPY3.Value;



                BarrelSuction.dExposureTimeUp = (double)nudCamUpExposure.Value;
                BarrelSuction.dGainUp = (double)nudGainCamUp.Value;
                BarrelSuction.dLightUp = (double)nudCamUpLight.Value;

                BarrelSuction.dExposureTimeDown = (double)nudCamDownExposure.Value;
                BarrelSuction.dGainDown = (double)nudGainCamDown.Value;
                BarrelSuction.dLightDown = (double)nudCamDownLight.Value;

                BarrelSuction.dCameraPos = (double)nudCamDownPos.Value;
                BarrelSuction.DGetTime = (double)nudGetTime.Value;
                BarrelSuction.DGetPosZ = (double)nudGetPosZ.Value;

                BarrelSuction.pPutBarrel1.X = (double)nudPutAssem1PosX.Value;
                BarrelSuction.pPutBarrel1.Y = (double)nudPutAssem1PosY.Value;
                BarrelSuction.pPutBarrel2.X = (double)nudPutAssem2PosX.Value;
                BarrelSuction.pPutBarrel2.Y = (double)nudPutAssem2PosY.Value;
                BarrelSuction.pPutBarrel1.Z = (double)nudPutAssem1PosZ.Value;
                BarrelSuction.pPutBarrel2.Z = (double)nudPutAssem2PosZ.Value;

                BarrelSuction.pGetLen1.X = (double)nudGetAssem1PosX.Value;
                BarrelSuction.pGetLen1.Y = (double)nudGetAssem1PosY.Value;
                BarrelSuction.pGetLen2.X = (double)nudGetAssem2PosX.Value;
                BarrelSuction.pGetLen2.Y = (double)nudGetAssem2PosY.Value;
                BarrelSuction.pGetLen1.Z = (double)nudGetAssem1PosZ.Value;
                BarrelSuction.pGetLen2.Z = (double)nudGetAssem2PosZ.Value;

                BarrelSuction.DPutTime = (double)nudPutTime.Value;
                BarrelSuction.dGetLenTime = (double)nudGetLenTime.Value;

                BarrelSuction.pPutLen.X = (double)nudGluePutX.Value;
                BarrelSuction.pPutLen.Y = (double)nudGluePutY.Value;
                BarrelSuction.pPutLen.Z = (double)nudGluePutZ.Value;
                BarrelSuction.pCamGlue.X = (double)nudGlueCamX.Value;
                BarrelSuction.pCamGlue.Y = (double)nudGlueCamY.Value;
                BarrelSuction.pCamGlue.Z = (double)nudGlueCamZ.Value;

                BarrelSuction.pGlue.X = (double)nudGlueX.Value;
                BarrelSuction.pGlue.Y = (double)nudGlueY.Value;
                BarrelSuction.pGlue.Z = (double)nudGlueZ.Value;
                BarrelSuction.DPutPosZ = (double)nudPutPosZ.Value;
                BarrelSuction.pUV.X = (double)nudUVX.Value;
                BarrelSuction.pUV.Y = (double)nudUVY.Value;
                BarrelSuction.dUVTime = (double)nudUVTime.Value;

                BarrelSuction.pCalibLenPos.X = (double)nudGlueLenPosX.Value;
                BarrelSuction.pCalibLenPos.Y = (double)nudGlueLenPosY.Value;

                BarrelSuction.pPutProductGlue.X = (double)nudProductGluePutX.Value;
                BarrelSuction.pPutProductGlue.Y = (double)nudProductGluePutY.Value;
                BarrelSuction.pPutProductGlue.Z = (double)nudProductGluePutZ.Value;
                BarrelSuction.pPutProductGlue.Theta = (double)nudProductGluePutC.Value;
                //optSuction.DPosCameraDownX = (double)nudCamDownPos.Value;

                //optSuction.DGetTime = (double)nudGetTime.Value;
                //optSuction.DGetPosZ = (double)nudGetPosZ.Value;
                //OptSution1.dFlashStartX = (double)nudFalshStartX.Value;
                //OptSution1.dPutTime = (double)nudPutTime.Value;
                //OptSution1.pPutPos.X = (double)nudPutX.Value;
                // OptSution1.pPutPos.Y = (double)nudPutY.Value;
                //OptSution1.pPutPos.Z = (double)nudPutZ.Value;


                CommonSet.dic_BarrelSuction[iSuctionNum] = optSuction;

                CommonSet.dic_BarrelSuction[1].lstHomatSuction[0] = null;
                CommonSet.dic_BarrelSuction[2].lstHomatSuction[1] = null;


                BarrelSuction.pPicGlueCalib.X = (double)nudCalibX.Value;
                BarrelSuction.pPicGlueCalib.Y = (double)nudCalibY.Value;
                BarrelSuction.dCalibGain = (double)nudGain.Value;

        }

        #region "绘制 GroupBox"
       
        #endregion

        private void nudPX1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void nudIndex1_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
           
            if (bInit)
            {
                SaveUI();
            }
        }

        private void nudIndex2_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
           // nudIndex5.Value = nudIndex2.Value;
            if (bInit)
            {
                SaveUI();
            }
        }

        private void nudIndex3_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
            //nudIndex6.Value = nudIndex3.Value;

            if (bInit)
            {
                SaveUI();
            }
        }

      

        private void btnGet1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY1.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

       
        private void btnGet2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY2.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }
        private void btnGet3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX3.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY3.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }
        private void btnGo1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }
           
          
                var optSuction = CommonSet.dic_BarrelSuction[iSuctionNum];
                double x = (optSuction.lstP1[iListIndex].X);
                double y = (optSuction.lstP1[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
           
        }

        private void btnGo2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }


            var optSuction = CommonSet.dic_BarrelSuction[iSuctionNum];
            double x = (optSuction.lstP2[iListIndex].X);
            double y = (optSuction.lstP2[iListIndex].Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(axisY, y, iVelRunY);
        }

        private void btnGo3_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }
            var optSuction = CommonSet.dic_BarrelSuction[iSuctionNum];
            double x = (optSuction.lstP3[iListIndex].X);
            double y = (optSuction.lstP3[iListIndex].Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(axisY, y, iVelRunY);
        }

        private void btnCamUpCheck_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUp();
            }
            catch (Exception)
            {
                
              
            }
        }
        private void CheckUp()
        {
           // AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = BarrelSuction.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            string strProcessName = BarrelSuction.strPicCameraUpName;
            BarrelSuction.InitImageUp(strProcessName);

            if (BarrelSuction.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            BarrelSuction.pfUp.SetRun(true);
            BarrelSuction.pfUp.hwin = hWindowControl1.HalconWindow;
            BarrelSuction.actionUp(hImage);
            BarrelSuction.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");

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
        private void CheckDown()
        {
            //AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = BarrelSuction.hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            string strProcessName = BarrelSuction.strPicCameraDownName;
            BarrelSuction.InitImageDown(strProcessName);

            if (BarrelSuction.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            BarrelSuction.pfDown.SetRun(true);
            BarrelSuction.pfDown.hwin = hWindowControl2.HalconWindow;
            BarrelSuction.actionDown(hImage);
            BarrelSuction.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + BarrelSuction.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + BarrelSuction.imgResultDown.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, BarrelSuction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "black", "true");
            if (BarrelSuction.imgResultDown.bImageResult)
                CommonSet.disp_message(hWindowControl2.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl2.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl2.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void btnCamUpCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = BarrelSuction.strPicCameraUpName;
            hoImage = BarrelSuction.hImageUP;
           
            if (strName.Equals(""))
            {
                strName = "BarrelUp" ;
                 BarrelSuction.strPicCameraUpName=strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamUpImg_Click(object sender, EventArgs e)
        {
            if (BarrelSuction.hImageUP == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Barrel" + iSuctionNum.ToString() + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(BarrelSuction.hImageUP, "bmp", 0, fileName);
            }
        }

        private void btnTriggerCamUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
               
                    if (CommonSet.camUpD1 != null)
                    {
                        CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                        CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                        mc.setDO(DO.点胶上相机, true);
                    }
               
            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCamUp_MouseUp(object sender, MouseEventArgs e)
        {
            mc.setDO(DO.点胶上相机, false);
        }

      

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gp = (GroupBox)sender;
            e.Graphics.Clear(gp.BackColor);
            SizeF fontSize = e.Graphics.MeasureString(gp.Text, gp.Font);
            string text = gp.Text;
            int pos = text.IndexOf('(');
            string front = "";
            string highLight = "";
            SizeF fontSize2 = new SizeF();
            if (pos > 0)
            {
                front = gp.Text.Substring(0, pos);
                highLight = gp.Text.Substring(pos);
                fontSize2 = e.Graphics.MeasureString(gp.Text.Substring(0, pos), gp.Font);
            }

            Pen p = new Pen(Brushes.LightSteelBlue, fontSize.Height + 12);
            e.Graphics.DrawLine(p, 1, 1, gp.Width - 2, 1);
            //e.Graphics.DrawLine(p, fontSize.Width + 8 + 2, 1, gp.Width - 2, 1);


            if (pos > 0)
            {
                e.Graphics.DrawString(front, gp.Font, Brushes.DimGray, (gp.Width - fontSize.Width) / 2, 1);
                e.Graphics.DrawString(highLight, gp.Font, Brushes.Blue, (gp.Width - fontSize.Width) / 2 + fontSize2.Width, 1);
            }
            else
            {
                e.Graphics.DrawString(gp.Text, gp.Font, Brushes.DimGray, (gp.Width - fontSize.Width) / 2, 1);
            }

            p = new Pen(Brushes.LightSteelBlue, 4);
            e.Graphics.DrawLine(p, 1, 1, 1, gp.Height);
            e.Graphics.DrawLine(p, 1, gp.Height - 2, gp.Width - 2, gp.Height - 2);
            e.Graphics.DrawLine(p, gp.Width - 2, 1, gp.Width - 2, gp.Height);
           
        }

        private void btnTriggerCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (CommonSet.camDownD1 != null)
                {
                    CommonSet.camDownD1.SetExposure(BarrelSuction.dExposureTimeDown);
                    CommonSet.camDownD1.SetGain(BarrelSuction.dGainDown);
                    mc.setDO(DO.点胶下相机, true);
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCamDown_MouseUp(object sender, MouseEventArgs e)
        {
            mc.setDO(DO.点胶下相机, false);
        }

        private void btnCamDownCheck_Click(object sender, EventArgs e)
        {
            try
            {
                CheckDown();
            }
            catch (Exception)
            {


            }
        }

        private void btnCamDownCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = BarrelSuction.strPicCameraDownName;
            hoImage = BarrelSuction.hImageDown;

            if (strName.Equals(""))
            {
                strName = "BarrelDown";
                BarrelSuction.strPicCameraDownName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamDownImg_Click(object sender, EventArgs e)
        {
            if (BarrelSuction.hImageDown == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "BarrelDown"  + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(BarrelSuction.hImageDown, "bmp", 0, fileName);
            }
        }
        private void btnGetPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            // nudAssem2PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            nudGetPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnAssem1Pos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudPutAssem1PosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPutAssem1PosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
            nudPutAssem1PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnAssem2Pos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudPutAssem2PosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPutAssem2PosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            nudPutAssem2PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoAssem1Pos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)) )
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }
            
                double x = (BarrelSuction.pPutBarrel1.X);
                double y = (BarrelSuction.pPutBarrel1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);

        }

        private void btnGoAssem2Pos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pPutBarrel2.X);
            double y = (BarrelSuction.pPutBarrel2.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
        }

        private void btnPutAssem1Pos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGetAssem1PosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGetAssem1PosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
            nudGetAssem1PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnPutAssem2Pos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGetAssem2PosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGetAssem2PosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            nudGetAssem2PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoPutAssem1Pos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pGetLen1.X);
            double y = (BarrelSuction.pGetLen1.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
        }

        private void btnGoPutAssem2Pos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pGetLen2.X);
            double y = (BarrelSuction.pGetLen2.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
        }

        private void btnGetCamDownPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            // nudAssem2PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            nudCamDownPos.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoCamDownPos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.dCameraPos);
            mc.AbsMove(axisX, x, iVelRunX);
        }

        private void btnGoPosZ_Click(object sender, EventArgs e)
        {
            double z = (BarrelSuction.DGetPosZ);
            
            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnPutZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            // nudAssem2PosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            nudPutPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoPutZ_Click(object sender, EventArgs e)
        {
            double z = (BarrelSuction.DPutPosZ);

            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnPutGluePos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGluePutX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGluePutY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;
        }

        private void btnGetCamGluePos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGlueCamX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGlueCamY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;
        }

        private void btnGluePos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGlueX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGlueY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;
        }

        private void btnGoPutGluePos_Click(object sender, EventArgs e)
        {
           
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pPutLen.X);
            double y = (BarrelSuction.pPutLen.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.点胶Y轴, y, 10);
        }

        private void btnGoCamGluePos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pCamGlue.X);
            double y = (BarrelSuction.pCamGlue.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.点胶Y轴, y, 10);
        }

        private void btnGoGluePos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pGlue.X);
            double y = (BarrelSuction.pGlue.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.点胶Y轴, y, 10);
        }

        private void rbtnSuction1_Click(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
           // int index = Convert.ToInt32(rbtn.Text);
            if (rbtn.Checked)
            {
                
                if (iSuctionNum != 1)
                {
                    iSuctionNum = 1;
                    bInit = false;
                    LoadCbBarrel();
                    InitTray();
                    bInit = true;
                }
            }
        }

        private void rbtnSuction2_Click(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            if (rbtn.Checked)
            {

                if (iSuctionNum != 2)
                {
                    iSuctionNum = 2;
                    bInit = false;
                    LoadCbBarrel();
                    InitTray();
                    bInit = true;
                }
            }
        }

        private void 镜筒Y轴P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtProductA3.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            if (name.Contains("X") || name.Contains("Y"))
            {
                if (mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01))
                {
                    MessageBox.Show("点胶Z轴低于安全高度!");
                    return;
                }
                //if (mc.dic_Axis[AXIS.Z2轴].dPos > (ProductSuction.pSafe2.Z + 0.01))
                //{
                //    MessageBox.Show("Z2轴低于安全高度!");
                //    return;
                //}
            }
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis;
            int vel = 0;
            if (name.Contains("X"))
            {
                axis = AXIS.点胶X轴;
                vel = iVelRunX;
            }
            else if (name.Contains("镜筒Y"))
            {
                axis = AXIS.镜筒Y轴;
                vel = iVelRunY;
            }
            else if (name.Contains("点胶Y"))
            {
                axis = AXIS.点胶Y轴;
                vel = iVelRunY;
            }
            else if (name.Contains("点胶Z"))
            {
                axis = AXIS.点胶Z轴;
                vel = iVelRunZ;
            }
            else
            {
                axis = AXIS.点胶C轴;
                vel = iVelRunX;
            }
            if (direct.Equals("P"))
            {
                // double newPos = ((posNow + (double)nudProductSize.Value));
                mc.VelMove(axis, vel);
            }
            else
            {
                mc.VelMove(axis, -vel);
                //double newPos = ((posNow - (double)nudProductSize.Value));
                //mc.AbsMove(axis, newPos, -vel);
            }
        }

        private void 镜筒Y轴P_MouseUp(object sender, MouseEventArgs e)
        {

            if (rbtProductA3.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis;
           
            if (name.Contains("X"))
            {
                axis = AXIS.点胶X轴;
             
            }
            else if (name.Contains("镜筒Y"))
            {
                axis = AXIS.镜筒Y轴;
         
            }
            else if (name.Contains("点胶Y"))
            {
                axis = AXIS.点胶Y轴;
          
            }
            else if (name.Contains("点胶Z"))
            {
                axis = AXIS.点胶Z轴;
             
            }
            else
            {
                axis = AXIS.点胶C轴;
      
            }
            mc.StopAxis(axis);
        }

        private void 镜筒Y轴P_Click(object sender, EventArgs e)
        {
            if (rbtProductC3.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();

            int len = name.Length;
            string direct = name.Substring(len - 1);
            AXIS axis;
            int vel = 0;
            if (name.Contains("X"))
            {
                axis = AXIS.点胶X轴;
                vel = iVelRunX;
            }
            else if (name.Contains("镜筒Y"))
            {
                axis = AXIS.镜筒Y轴;
                vel = iVelRunY;
            }
            else if (name.Contains("点胶Y"))
            {
                axis = AXIS.点胶Y轴;
                vel = iVelRunY;
            }
            else if (name.Contains("点胶Z"))
            {
                axis = AXIS.点胶Z轴;
                vel = iVelRunZ;
            }
            else
            {
                axis = AXIS.点胶C轴;
                vel = iVelRunX;
            }
            double posNow = mc.dic_Axis[axis].dPos;

            if (direct.Equals("P"))
            {
                double newPos = ((posNow + (double)nudProductSize3.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                double newPos = ((posNow - (double)nudProductSize3.Value));
                mc.AbsMove(axis, newPos, vel);
            }
        }
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
                    HOperatorSet.DispImage(BarrelSuction.hImageUP,hwin);
                    return;
                }

            }
            catch (Exception ex) { }
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
                HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                //control.DisplayImage(mc.hwindow, ho_Image, startX, startY, endX, endY);
                // displayObj();

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
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
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}


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
                    HOperatorSet.DispImage(BarrelSuction.hImageDown, hwin);
                    return;
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
                HOperatorSet.DispImage(BarrelSuction.hImageDown, hwin);
                //control.DisplayImage(mc.hwindow, ho_Image, startX, startY, endX, endY);
                // displayObj();

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

        private void btnTest_Click(object sender, EventArgs e)
        {
           
                GetProductTestModule.currentPoint = CommonSet.dic_BarrelSuction[1].getCoordinateByIndex((int)nudTestNum.Value);
                GetProductTestModule.currentPoint.Z = BarrelSuction.DGetPosZ;

                GetProductTestModule.axisX = AXIS.点胶X轴;
                GetProductTestModule.axisY = AXIS.镜筒Y轴;
                GetProductTestModule.axisZ = AXIS.点胶Z轴;
                GetProductTestModule.dGetTime = BarrelSuction.DGetTime;
                GetProductTestModule.dSuction = DO.点胶镜筒吸笔下降;
                GetProductTestModule.dGet = DO.点胶镜筒吸笔真空;

           

            FrmGetTest f = new FrmGetTest();
            f.ShowDialog();
            f = null;
        }

        private void btnTriggerCamUp_Click(object sender, EventArgs e)
        {

        }

        private void btnPutGluePosZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGluePutZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGetCamGluePosZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGlueCamZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGluePosZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudGlueZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoPutGluePosZ_Click(object sender, EventArgs e)
        {
            double z = (double)nudGluePutZ.Value;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);

        }

        private void btnGoCamGluePosZ_Click(object sender, EventArgs e)
        {
            double z = (double)nudGlueCamZ.Value;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void btnGoGluePosZ_Click(object sender, EventArgs e)
        {
            double z = (double)nudGlueZ.Value;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void btnGetUVXY_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudUVX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudUVY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;

            
        }

        private void btnGoUVXY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }
            mc.AbsMove(AXIS.点胶X轴, (double)nudUVX.Value, (int)iVelRunX);
            mc.AbsMove(AXIS.点胶Y轴, (double)nudUVY.Value, (int)10);
        }

        private void btnGetGlueLenPos_Click(object sender, EventArgs e)
        {
            try
            {
                CheckUp();
                nudGlueLenPosX.Value = (decimal)BarrelSuction.imgResultUp.CenterRow;
                nudGlueLenPosY.Value = (decimal)BarrelSuction.imgResultUp.CenterColumn;
            }
            catch (Exception)
            {


            }
        }

        private void btnTestGlue_Click(object sender, EventArgs e)
        {
            FrmGlueTest frmGlue = new FrmGlueTest();

            frmGlue.ShowDialog();
            frmGlue = null;
        }

        private void btnGoPut1_Click(object sender, EventArgs e)
        {
            if ((Math.Abs(BarrelSuction.pPutBarrel1.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.1) || (Math.Abs(BarrelSuction.pPutBarrel1.Y - mc.dic_Axis[AXIS.组装Y1轴].dPos) > 0.1))
            {
                MessageBox.Show("XY轴超范围!");
                return;
            }
            mc.AbsMove(AXIS.点胶Z轴, BarrelSuction.pPutBarrel1.Z, (int)iVelRunZ);
        }

        private void btnGoPut2_Click(object sender, EventArgs e)
        {
            if ((Math.Abs(BarrelSuction.pPutBarrel2.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.1) || (Math.Abs(BarrelSuction.pPutBarrel2.Y - mc.dic_Axis[AXIS.组装Y2轴].dPos) > 0.1))
            {
                MessageBox.Show("XY轴超范围!");
                return;
            }
            mc.AbsMove(AXIS.点胶Z轴, BarrelSuction.pPutBarrel2.Z, (int)iVelRunZ);
        }

        private void btnGoGet1_Click(object sender, EventArgs e)
        {
            if ((Math.Abs(BarrelSuction.pGetLen1.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.1) || (Math.Abs(BarrelSuction.pGetLen1.Y - mc.dic_Axis[AXIS.组装Y1轴].dPos) > 0.1))
            {
                MessageBox.Show("XY轴超范围!");
                return;
            }
            mc.AbsMove(AXIS.点胶Z轴, BarrelSuction.pGetLen1.Z, (int)iVelRunZ);
        }

        private void btnGoGet2_Click(object sender, EventArgs e)
        {
            if ((Math.Abs(BarrelSuction.pGetLen2.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.1) || (Math.Abs(BarrelSuction.pGetLen2.Y - mc.dic_Axis[AXIS.组装Y2轴].dPos) > 0.1))
            {
                MessageBox.Show("XY轴超范围!");
                return;
            }
            mc.AbsMove(AXIS.点胶Z轴, BarrelSuction.pGetLen2.Z, (int)iVelRunZ);
        }

        private void btnProductPutGluePos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudProductGluePutX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudProductGluePutY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;
        }

        private void btnGoProductPutGluePos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = (BarrelSuction.pPutProductGlue.X);
            double y = (BarrelSuction.pPutProductGlue.Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(AXIS.点胶Y轴, y, 10);
        }

        private void btnProductPutGluePosZ_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudProductGluePutZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double z = (double)nudProductGluePutZ.Value;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void btnPutGluePosC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudProductGluePutC.Value = (decimal)mc.dic_Axis[AXIS.点胶C轴].dPos;
        }

        private void btnGoPutGluePosC_Click(object sender, EventArgs e)
        {
            double c = (double)nudProductGluePutC.Value;
            mc.AbsMove(AXIS.点胶C轴, c, 100);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            nudCalibX.Value = (decimal)mc.dic_Axis[AXIS.点胶X轴].dPos;
            nudCalibY.Value = (decimal)mc.dic_Axis[AXIS.点胶Y轴].dPos;

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.点胶X轴, (double)nudCalibX.Value, 100);
            mc.AbsMove(AXIS.点胶Y轴, (double)nudCalibY.Value, 30);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckUpD();
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

        private void btnCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            strName = BarrelSuction.strPicUpCalibName;
            hoImage = BarrelSuction.hImageUP;
            if (strName.Equals(""))
            {
                strName = "点胶标定";

            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Run.calibModule.IStep = 0;
            CommonSet.hPixelToAxisCalibWin = hWindowControl1.HalconWindow;
            CalibModule.currentPoint.X = (double)nudCalibX.Value;
            CalibModule.currentPoint.Y = (double)nudCalibY.Value;
            CalibModule.dDist = (double)nudDist.Value;
          
            CalibModule.axisX = AXIS.点胶X轴;
            CalibModule.axisY = AXIS.点胶Y轴;

            CalibModule.iCamPos = 7;
            Run.runMode = RunMode.像素标定;
        }

        private void btnUpdateCalib_Click(object sender, EventArgs e)
        {
            BarrelSuction.lstCalibCameraUpXY.Clear();
            BarrelSuction.lstCalibCameraUpRC.Clear();
            BarrelSuction.lstCalibCameraUpXY.AddRange(CalibModule.lstPointXY.ToArray());
            BarrelSuction.lstCalibCameraUpRC.AddRange(CalibModule.lstPointRC.ToArray());
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

           

            mc.AbsMove(AXIS.点胶X轴, (double)nudPosX.Value, 100);
            mc.AbsMove(AXIS.点胶Y轴, (double)nudPosY.Value, 30);

            
        }

        private void btnCheckResult_Click(object sender, EventArgs e)
        {
            Point p = new Point();
            HTuple outX, outY;
            HTuple hom;
            if (CalibModule.lstPointRC.Count < 9)
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
                HOperatorSet.AffineTransPoint2d(hom, (double)nudRow.Value, (double)nudCol.Value, out outX, out outY);
                p.X = outX;
                p.Y = outY;

                lblResult.Text = "X:" + p.X.ToString("0.000") + " Y:" + p.Y.ToString("0.000");
                // p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnTriggerCam_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (CommonSet.camUpD1 != null)
                    {
                        CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                        CommonSet.camUpD1.SetGain(BarrelSuction.dCalibGain);
                        mc.setDO(DO.点胶上相机, true);
                    }
            }
            catch (Exception)
            {
                
                
            }
        }

        private void btnRotateTest_Click(object sender, EventArgs e)
        {
            try
            {
                frmRotate = new FrmRotate();
                frmRotate.ShowDialog();
                frmRotate = null;
            }
            catch (Exception)
            {
                
               
            }
           
        }
    }
}
