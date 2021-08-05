using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;
using Tray;
using CameraSet;
using HalconDotNet;
using ImageProcess;
namespace Assembly
{
    public partial class OptSuctionUI : UserControl
    {
        Tray.Tray tray = null;
      //  TrayPanel trayPanel1 = null;
        public int iSuctionNum = 1;//当前吸笔序号
        public int iStation = 1;//当前站号
        public int iCurrentTrayIndex = 0;//当前盘索引
        public int iListIndex = 0;//存储到OptSuction所有List索引
        MotionCard mc = null;
        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        bool bInit = false;//代表是否初始化完成
       // ProductSuction suction = null;

        int SuctionSUM = 9;//工站吸笔总数
        AXIS axisX,axisY, axisZ;
        ICamera camUp, camDown;
        public OptSuctionUI(TrayPanel panel =null)
        {
          
            if (iSuctionNum <= SuctionSUM)
            {
               // suction = CommonSet.dic_ProductSuction[iSuctionNum];
                axisX = AXIS.取料X1轴;
                axisY = AXIS.取料Y1轴;
                axisZ = AXIS.取料Z1轴;
                iStation = 1;
               // CommonSet.iCameraDownFlag = 1;
            }
            else
            {
                iStation = 2;
                axisX = AXIS.取料X2轴;
                axisY = AXIS.取料Y2轴;
                axisZ = AXIS.取料Z2轴;
               // CommonSet.iCameraDownFlag = 2;
            }
            mc = MotionCard.getMotionCard();
            InitializeComponent();
            //trayPanel1 = panel;
           
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;////用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}
        bool bAutoUp = true;
        bool bAutoDown = true;
        int iStationFlag = 1;
        public void updateUI()
        {
            try
            {
                lblSuctionNum.Text = iSuctionNum.ToString();
                lblCurrentTrayIndex.Text = iCurrentTrayIndex.ToString();
              

                showListTrayPanel1.SetStaion(iStation);
                showListTrayPanel1.SetSuction(iSuctionNum);
                showListTrayPanel1.UpdateUI();
                //nudAssemX1.Value = (decimal)suction.pAssembly.X;
                //nudAssemY1.Value = (decimal)suction.pAssembly.Y;

                //lblSuctionCenter.Text = "R:" + suction.dSuctionCenterR.ToString("0.000") + "\r\n" +
                //                   "C:" + suction.dSuctionCenterC.ToString("0.000");
                //lblCenterPos.Text = "R:" + CommonSet.dic_ProductSuction[iSuctionNum].dFlashCalibRow.ToString("0.000") + "\r\n" +
                //                   "C:" + CommonSet.dic_ProductSuction[iSuctionNum].dFlashCalibColumn.ToString("0.000");
                if (iSuctionNum > SuctionSUM)
                {
                    iStation = 2;
                    if (camUp != CommonSet.camUpA2)
                    {
                        camUp = CommonSet.camUpA2;
                        camDown = CommonSet.camDownA2;
                    }
                    lblCurrentSum.Text = CommonSet.dic_OptSuction2[iSuctionNum].GetCount().ToString();
                    nudTestNum.Maximum = (decimal)CommonSet.dic_OptSuction2[iSuctionNum].GetCount();
                      if (!txtSuctionName.Focused)
                        {
                            txtSuctionName.Text = CommonSet.dic_OptSuction2[iSuctionNum].Name;
                        }
                      axisX = AXIS.取料X2轴;
                      axisY = AXIS.取料Y2轴;
                      axisZ = AXIS.取料Z2轴;
                      groupBox4.Text = "上相机(取料2)";
                      groupBox5.Text = "下相机(取料2)";
                      groupBox7.Text = "飞拍(取料2)";
                      groupBox8.Text = "放料(中转2)";
                      groupBox9.Text = "操作(取料2)";
                      groupBox11.Text = "求心(工位2)";
                      groupBox13.Text = "抛料位(取料2)";

                      lblAxisX.Text = "取料X2轴";
                      lblAxisY.Text = "取料Y2轴";
                      lblAxisZ.Text = "取料Z2轴";

                      lblPosX.Text = mc.dic_Axis[AXIS.取料X2轴].dPos.ToString("0.000");
                      lblPosY.Text = mc.dic_Axis[AXIS.取料Y2轴].dPos.ToString("0.000");
                      lblPosZ.Text = mc.dic_Axis[AXIS.取料Z2轴].dPos.ToString("0.000");

                      if (cbContinusCamDown.Checked && (Run.runMode == RunMode.手动))
                      {
                          if (CommonSet.camDownA2 != null)
                          {
                              CommonSet.camDownA2.SetExposure(OptSution2.dExposureTimeDown);
                              CommonSet.camDownA2.SetGain(OptSution2.dGainDown);
                              bAutoDown = !bAutoDown;
                              mc.setDO(DO.取料下相机2, bAutoDown);
                          }
                      }
                      if (cbContinousCamUp.Checked && (Run.runMode == RunMode.手动))
                      {
                          if (CommonSet.camUpA2 != null)
                          {
                              CommonSet.camUpA2.SetExposure(OptSution2.dExposureTimeUp);
                              CommonSet.camUpA2.SetGain(OptSution2.dGainUp);
                              bAutoUp = !bAutoUp;
                              mc.setDO(DO.取料上相机2, bAutoUp);

                          }
                      }

                      if (iStationFlag == 1)
                      {
                          this.dataGridView1.AutoGenerateColumns = false;
                          this.dataGridView1.DataSource = CommonSet.dic_OptSuction2.Values.ToList();

                      }
                    for (int i = 0; i < 9; i++)
                      {
                          dataGridView1.Rows[i].HeaderCell.Value = (i + 10).ToString();

                      }
                    iStationFlag = 2;
                    lblShowStation.Text = "工位2吸笔参数：";
                    //btnTrigger.Enabled = CommonSet.camDown2.bTrigger;
                    //cbContinous.Checked = !CommonSet.camDown2.bTrigger;
                }
                else
                {
                    iStation = 1;
                    if (camUp != CommonSet.camUpA1)
                    {
                        camUp = CommonSet.camUpA1;
                        camDown = CommonSet.camDownA1;
                    }
                    lblCurrentSum.Text = CommonSet.dic_OptSuction1[iSuctionNum].GetCount().ToString();
                    nudTestNum.Maximum = (decimal)CommonSet.dic_OptSuction1[iSuctionNum].GetCount();
                    if (!txtSuctionName.Focused)
                    {
                        txtSuctionName.Text = CommonSet.dic_OptSuction1[iSuctionNum].Name;
                    }
                    groupBox4.Text = "上相机(取料1)";
                    groupBox5.Text = "下相机(取料1)";
                    groupBox7.Text = "飞拍(取料1)";
                    groupBox8.Text = "放料(中转1)";
                    groupBox9.Text = "操作(取料1)";
                    groupBox11.Text = "求心(工位1)";
                    groupBox13.Text = "抛料位(取料1)";

                    lblAxisX.Text = "取料X1轴";
                    lblAxisY.Text = "取料Y1轴";
                    lblAxisZ.Text = "取料Z1轴";
                    lblPosX.Text = mc.dic_Axis[AXIS.取料X1轴].dPos.ToString("0.000");
                    lblPosY.Text = mc.dic_Axis[AXIS.取料Y1轴].dPos.ToString("0.000");
                    lblPosZ.Text = mc.dic_Axis[AXIS.取料Z1轴].dPos.ToString("0.000");
                    axisX = AXIS.取料X1轴;
                    axisY = AXIS.取料Y1轴;
                    axisZ = AXIS.取料Z1轴;
                    //btnTrigger.Enabled = CommonSet.camDown1.bTrigger;
                    //cbContinous.Checked = !CommonSet.camDown1.bTrigger;

                    if (cbContinusCamDown.Checked && (Run.runMode == RunMode.手动))
                    {
                        if (CommonSet.camDownA1 != null)
                        {
                            CommonSet.camDownA1.SetExposure(OptSution1.dExposureTimeDown);
                            CommonSet.camDownA1.SetGain(OptSution1.dGainDown);
                            bAutoDown = !bAutoDown;
                            mc.setDO(DO.取料下相机1, bAutoDown);
                        }
                    }
                    if (cbContinousCamUp.Checked && (Run.runMode == RunMode.手动))
                    {
                        if (CommonSet.camUpA1 != null)
                        {
                            CommonSet.camUpA1.SetExposure(OptSution1.dExposureTimeUp);
                            CommonSet.camUpA1.SetGain(OptSution1.dGainUp);
                            bAutoUp = !bAutoUp;
                            mc.setDO(DO.取料上相机1, bAutoUp);

                        }
                    }
                    if (iStationFlag == 2)
                    {
                        this.dataGridView1.AutoGenerateColumns = false;
                        this.dataGridView1.DataSource = CommonSet.dic_OptSuction1.Values.ToList();

                    }
                    if (dataGridView1.RowCount > 1)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        }
                    }
                    iStationFlag = 1;
                    lblShowStation.Text = "工位1吸笔参数：";
                }
               
                panelProduct.Visible = rbtProductA.Checked;
                
                lblSuction.Text = "取料气缸下降"+iSuctionNum.ToString();
                lblGet.Text = "取料吸真空"+iSuctionNum.ToString();
                lblPut.Text = "取料破真空"+iSuctionNum.ToString();

                string strDo = "取料气缸下降" + iSuctionNum.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                
                lblSuction.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "取料吸真空" + iSuctionNum.ToString();
                 dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblGet.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "取料破真空" + iSuctionNum.ToString();
                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblPut.BackColor = getColor(mc.dic_DO[dOut]);

                groupBox1.Text = "托盘(吸笔" + iSuctionNum.ToString() + ")";
                groupBox6.Text = "取料参数(吸笔" + iSuctionNum.ToString() + ")";
                groupBox10.Text = "取料测试(吸笔"+iSuctionNum.ToString()+")";
                groupBox12.Text = "吸笔中心(吸笔" + iSuctionNum.ToString() + ")";
               
                UpdateControl();

                if(Run.runMode == RunMode.组装验证)
                    lblMakeUp.Text = "位置补偿坐标(X,Y): " + ResultTestModule.dPosX.ToString("0.000") + "," + ResultTestModule.dPosY.ToString("0.000");
               
                
                // this.dataGridView1.DataSource = lookUp.ToList();


            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("取料界面更新异常:"+ex.ToString());
              
            }
           

        }
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Gray;
        }
        public   HWindow GetUpWindow()
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
        private void btnTraySet_Click(object sender, EventArgs e)
        {
            CommonSet.frmSAndTR = new FrmSuctionAndTrayRelation();
            CommonSet.frmSAndTR.ShowDialog();
            CommonSet.frmSAndTR = null;
            LoadCbBarrel();
            InitTray();

        }
        private void LoadTray()
        {
           
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
            if (iStation == 1)
            {
                OptSution1 optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                iListIndex = optSuction.lstTrayNum.IndexOf(iCurrentTrayIndex);
                if (optSuction.lstIndex1[iListIndex] > count)
                {
                    optSuction.lstIndex1[iListIndex] = count;
                    nudIndex1.Value = optSuction.lstIndex1[iListIndex];
                }
                else
                {
                    nudIndex1.Value = optSuction.lstIndex1[iListIndex];
                }

                if (optSuction.lstIndex2[iListIndex] > count)
                {
                    optSuction.lstIndex2[iListIndex] = count;
                    nudIndex2.Value = optSuction.lstIndex2[iListIndex];
                }
                else
                {
                    nudIndex2.Value = optSuction.lstIndex2[iListIndex];
                }
                if (optSuction.lstIndex3[iListIndex] > count)
                {
                    optSuction.lstIndex3[iListIndex] = count;
                    nudIndex3.Value = optSuction.lstIndex3[iListIndex];
                }
                else
                {
                    nudIndex3.Value = optSuction.lstIndex3[iListIndex];
                }

                CommonSet.dic_OptSuction1[iSuctionNum].EndPos = CommonSet.dic_OptSuction1[iSuctionNum].GetCount();
                tray.setNumColor(optSuction.lstIndex1[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex2[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex3[iListIndex], CommonSet.ColorNotUse);
                tray.updateColor();
            }
            else
            {
                OptSution2 optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                iListIndex = optSuction.lstTrayNum.IndexOf(iCurrentTrayIndex);
                if (optSuction.lstIndex1[iListIndex] > count)
                {
                    optSuction.lstIndex1[iListIndex] = count;
                    nudIndex1.Value = optSuction.lstIndex1[iListIndex];
                }
                else
                {
                    nudIndex1.Value = optSuction.lstIndex1[iListIndex];
                }
                if (optSuction.lstIndex2[iListIndex] > count)
                {
                    optSuction.lstIndex2[iListIndex] = count;
                    nudIndex2.Value = optSuction.lstIndex2[iListIndex];
                }
                else
                {
                    nudIndex2.Value = optSuction.lstIndex2[iListIndex];
                }
                if (optSuction.lstIndex3[iListIndex] > count)
                {
                    optSuction.lstIndex3[iListIndex] = count;
                    nudIndex3.Value = optSuction.lstIndex3[iListIndex];
                }
                else
                {
                    nudIndex3.Value = optSuction.lstIndex3[iListIndex];
                }
                //nudIndex1.Maximum = count;
                //nudIndex2.Maximum = count;
                //nudIndex3.Maximum = count;
                CommonSet.dic_OptSuction2[iSuctionNum].EndPos = CommonSet.dic_OptSuction2[iSuctionNum].GetCount();
                tray.setNumColor(optSuction.lstIndex1[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex2[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex3[iListIndex], CommonSet.ColorNotUse);
                tray.updateColor();

            }
            UpdateControl();
            bInit = true;

        }
        List<OptSution1> listOpt1;
        private void OptUI_Load(object sender, EventArgs e)
        {
          
            //if (trayPanel1 != null)
            //{
            //    trayPanel1.Anchor = AnchorStyles.Left|AnchorStyles.Top|AnchorStyles.Right;
            //    trayPanel1.Location = new System.Drawing.Point(3, 11);
            //    // this.panel2.Name = "panel2";
            //    trayPanel1.Size = new System.Drawing.Size(326, 200);
            //    trayPanel1.Margin = new System.Windows.Forms.Padding(1);
            //    groupBox1.Controls.Add(trayPanel1);
            //}
            //trayPanel1.TabIndex = 8;
            LoadCbBarrel();
            InitTray();
            cbUseCalibDown.Checked = true;
            cbUseCalibUp.Checked = true;
            //this.dataGridView1.Columns[0].HeaderText = "吸笔名称";
            //this.dataGridView1.Columns[0].Width = 80;
            //this.dataGridView1.Columns[0].DataPropertyName = "Name";
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridView1.DataSource = CommonSet.dic_OptSuction1.Values.ToList();
           // bInit = true;
        }
       
        private void LoadCbBarrel()
        {
            cbBarrelIndex.Items.Clear();
            if (iStation == 1)
            {
                int[] arr = CommonSet.dic_OptSuction1[iSuctionNum].lstTrayNum.ToArray();
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
            else
            {
                int[] arr = CommonSet.dic_OptSuction2[iSuctionNum].lstTrayNum.ToArray();
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
        public void UpdateControl()
        {
           
            //if (bsave)
            //    return;
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];

                
                setNumerialControl(nudPX1, optSuction.lstP1[iListIndex].X);
                setNumerialControl(nudPY1, optSuction.lstP1[iListIndex].Y);

                setNumerialControl(nudPX2, optSuction.lstP2[iListIndex].X);
                setNumerialControl(nudPY2, optSuction.lstP2[iListIndex].Y);

                setNumerialControl(nudPX3, optSuction.lstP3[iListIndex].X);
                setNumerialControl(nudPY3, optSuction.lstP3[iListIndex].Y);

                setNumerialControl(nudPX4, optSuction.lstP4[iListIndex].X);
                setNumerialControl(nudPY4, optSuction.lstP4[iListIndex].Y);

                setNumerialControl(nudPX5, optSuction.lstP5[iListIndex].X);
                setNumerialControl(nudPY5, optSuction.lstP5[iListIndex].Y);

                setNumerialControl(nudPX6, optSuction.lstP6[iListIndex].X);
                setNumerialControl(nudPY6, optSuction.lstP6[iListIndex].Y);

                setNumerialControl(nudCamUpExposure, OptSution1.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, OptSution1.dGainUp);
                setNumerialControl(nudCamUpLight, OptSution1.dLightUp);

                setNumerialControl(nudCamDownExposure, OptSution1.dExposureTimeDown);
                setNumerialControl(nudGainCamDown, OptSution1.dGainDown);
                setNumerialControl(nudCamDownLight, OptSution1.dLightDown);

                setNumerialControl(nudCamDownPos, optSuction.DPosCameraDownX);
                setNumerialControl(nudGetTime, optSuction.DGetTime);
                setNumerialControl(nudGetPosZ, optSuction.DGetPosZ);
                setNumerialControl(nudCorrentTime, OptSution1.DCorrectTime);
                setNumerialControl(nudCorrectZ, OptSution1.DCorrectPosZ);
                setNumerialControl(nudFalshStartX, OptSution1.dFlashStartX);
                setNumerialControl(nudPutTime, OptSution1.dPutTime);
                setNumerialControl(nudPutX, OptSution1.pPutPos.X);
               // setNumerialControl(nudPutY, OptSution1.pPutPos.Y);
                setNumerialControl(nudPutZ, OptSution1.pPutPos.Z);
                setNumerialControl(nudDiscardX, OptSution1.pDiscard.X);
                setNumerialControl(nudDiscardY, OptSution1.pDiscard.Y);
                setNumerialControl(nudDiscardZ, OptSution1.pDiscard.Z);


                setNumerialControl(nudSuctionRow, optSuction.pSuctionCenter.X);
                setNumerialControl(nudSuctionCol, optSuction.pSuctionCenter.Y);

                setNumerialControl(nudCamUpPosX1, OptSution1.lstCalibCameraUpXY[0].X);
                setNumerialControl(nudCamUpPosR1, OptSution1.lstCalibCameraUpRC[0].X);
                setNumerialControl(nudCamUpPosC1, OptSution1.lstCalibCameraUpRC[0].Y);

                setNumerialControl(nudCamUpPosX2, OptSution1.lstCalibCameraUpXY[1].X);
                setNumerialControl(nudCamUpPosR2, OptSution1.lstCalibCameraUpRC[1].X);
                setNumerialControl(nudCamUpPosC2, OptSution1.lstCalibCameraUpRC[1].Y);

                setNumerialControl(nudCamDownPosX1, OptSution1.lstCalibCameraDownXY[0].X);
                setNumerialControl(nudCamDownPosR1, OptSution1.lstCalibCameraDownRC[0].X);
                setNumerialControl(nudCamDownPosC1, OptSution1.lstCalibCameraDownRC[0].Y);

                setNumerialControl(nudCamDownPosX2, OptSution1.lstCalibCameraDownXY[1].X);
                setNumerialControl(nudCamDownPosR2, OptSution1.lstCalibCameraDownRC[1].X);
                setNumerialControl(nudCamDownPosC2, OptSution1.lstCalibCameraDownRC[1].Y);

                //setNumerialControl(nudSuctionRow,optSuctio
                cbUseCam.Checked = optSuction.BUseCameraUp;
                lblResolution1.Text = "上相机分辨率:" + OptSution1.GetUpResulotion().ToString("0.0000") + " 角度:" + OptSution1.GetUpCameraAngle().ToString("0.00");
                lblResolution2.Text = "下相机分辨率:" + OptSution1.GetDownResulotion().ToString("0.0000") + " 角度:" + OptSution1.GetDownCameraAngle().ToString("0.00");
                    

            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                setNumerialControl(nudPX1, optSuction.lstP1[iListIndex].X);
                setNumerialControl(nudPY1, optSuction.lstP1[iListIndex].Y);

                setNumerialControl(nudPX2, optSuction.lstP2[iListIndex].X);
                setNumerialControl(nudPY2, optSuction.lstP2[iListIndex].Y);

                setNumerialControl(nudPX3, optSuction.lstP3[iListIndex].X);
                setNumerialControl(nudPY3, optSuction.lstP3[iListIndex].Y);

                setNumerialControl(nudPX4, optSuction.lstP4[iListIndex].X);
                setNumerialControl(nudPY4, optSuction.lstP4[iListIndex].Y);

                setNumerialControl(nudPX5, optSuction.lstP5[iListIndex].X);
                setNumerialControl(nudPY5, optSuction.lstP5[iListIndex].Y);

                setNumerialControl(nudPX6, optSuction.lstP6[iListIndex].X);
                setNumerialControl(nudPY6, optSuction.lstP6[iListIndex].Y);

                setNumerialControl(nudCamUpExposure, OptSution2.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, OptSution2.dGainUp);
                setNumerialControl(nudCamUpLight, OptSution2.dLightUp);

                setNumerialControl(nudCamDownExposure, OptSution2.dExposureTimeDown);
                setNumerialControl(nudGainCamDown, OptSution2.dGainDown);
                setNumerialControl(nudCamDownLight, OptSution2.dLightDown);

                setNumerialControl(nudCamDownPos, optSuction.DPosCameraDownX);

                setNumerialControl(nudGetTime, optSuction.DGetTime);
                setNumerialControl(nudGetPosZ, optSuction.DGetPosZ);
                setNumerialControl(nudCorrentTime, OptSution2.DCorrectTime);
                setNumerialControl(nudCorrectZ, OptSution2.DCorrectPosZ);
                setNumerialControl(nudFalshStartX, OptSution2.dFlashStartX);
                setNumerialControl(nudPutTime, OptSution2.dPutTime);
                setNumerialControl(nudPutX, OptSution2.pPutPos.X);
                //setNumerialControl(nudPutY, OptSution2.pPutPos.Y);
                setNumerialControl(nudPutZ, OptSution2.pPutPos.Z);

                setNumerialControl(nudDiscardX, OptSution2.pDiscard.X);
                setNumerialControl(nudDiscardY, OptSution2.pDiscard.Y);
                setNumerialControl(nudDiscardZ, OptSution2.pDiscard.Z);

                setNumerialControl(nudSuctionRow, optSuction.pSuctionCenter.X);
                setNumerialControl(nudSuctionCol, optSuction.pSuctionCenter.Y);

                setNumerialControl(nudCamUpPosX1, OptSution2.lstCalibCameraUpXY[0].X);
                setNumerialControl(nudCamUpPosR1, OptSution2.lstCalibCameraUpRC[0].X);
                setNumerialControl(nudCamUpPosC1, OptSution2.lstCalibCameraUpRC[0].Y);

                setNumerialControl(nudCamUpPosX2, OptSution2.lstCalibCameraUpXY[1].X);
                setNumerialControl(nudCamUpPosR2, OptSution2.lstCalibCameraUpRC[1].X);
                setNumerialControl(nudCamUpPosC2, OptSution2.lstCalibCameraUpRC[1].Y);

                setNumerialControl(nudCamDownPosX1, OptSution2.lstCalibCameraDownXY[0].X);
                setNumerialControl(nudCamDownPosR1, OptSution2.lstCalibCameraDownRC[0].X);
                setNumerialControl(nudCamDownPosC1, OptSution2.lstCalibCameraDownRC[0].Y);

                setNumerialControl(nudCamDownPosX2, OptSution2.lstCalibCameraDownXY[1].X);
                setNumerialControl(nudCamDownPosR2, OptSution2.lstCalibCameraDownRC[1].X);
                setNumerialControl(nudCamDownPosC2, OptSution2.lstCalibCameraDownRC[1].Y);

                cbUseCam.Checked = optSuction.BUseCameraUp;
                lblResolution1.Text = "上相机分辨率:" + OptSution2.GetUpResulotion().ToString("0.0000") + " 角度:" + OptSution2.GetUpCameraAngle().ToString("0.00");
                lblResolution2.Text = "下相机分辨率:" + OptSution2.GetDownResulotion().ToString("0.0000") + " 角度:" + OptSution2.GetDownCameraAngle().ToString("0.00");
                 
            }
        }
        //bool bsave = false;
        //保存界面的参数
        public void SaveUI()
        {
            //bsave = true;
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
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
          

                p = new Point();
                p.X = (double)nudPX4.Value;
                p.Y = (double)nudPY4.Value;
                optSuction.lstP4[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX5.Value;
                p.Y = (double)nudPY5.Value;
                optSuction.lstP5[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX6.Value;
                p.Y = (double)nudPY6.Value;
                optSuction.lstP6[iListIndex] = p;
               

                OptSution1.dExposureTimeUp = (double)nudCamUpExposure.Value;
                OptSution1.dGainUp = (double)nudGainCamUp.Value;
                OptSution1.dLightUp = (double)nudCamUpLight.Value;

                OptSution1.dExposureTimeDown = (double)nudCamDownExposure.Value;
                OptSution1.dGainDown = (double)nudGainCamDown.Value;
                OptSution1.dLightDown = (double)nudCamDownLight.Value;

                optSuction.DPosCameraDownX = (double)nudCamDownPos.Value;

                optSuction.DGetTime = (double)nudGetTime.Value;
                optSuction.DGetPosZ = (double)nudGetPosZ.Value;
                OptSution1.DCorrectPosZ = (double)nudCorrectZ.Value;
                OptSution1.DCorrectTime = (double)nudCorrentTime.Value;

                OptSution1.dFlashStartX = (double)nudFalshStartX.Value;
                OptSution1.dPutTime = (double)nudPutTime.Value;
                OptSution1.pPutPos.X = (double)nudPutX.Value;
               // OptSution1.pPutPos.Y = (double)nudPutY.Value;
                OptSution1.pPutPos.Z = (double)nudPutZ.Value;

                OptSution1.pDiscard.X = (double)nudDiscardX.Value;
                OptSution1.pDiscard.Y = (double)nudDiscardY.Value;
                OptSution1.pDiscard.Z = (double)nudDiscardZ.Value;

                OptSution1.lstCalibCameraUpXY[0].X = (double)nudCamUpPosX1.Value;
                OptSution1.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
                OptSution1.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

                OptSution1.lstCalibCameraUpXY[1].X = (double)nudCamUpPosX2.Value;
                OptSution1.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
                OptSution1.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

                OptSution1.lstCalibCameraDownXY[0].X = (double)nudCamDownPosX1.Value;
                OptSution1.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
                OptSution1.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

                OptSution1.lstCalibCameraDownXY[1].X = (double)nudCamDownPosX2.Value;
                OptSution1.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
                OptSution1.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;

                optSuction.pSuctionCenter.X = (double)nudSuctionRow.Value;
                optSuction.pSuctionCenter.Y = (double)nudSuctionCol.Value;


                
                for (int i = 0; i < 9; i++)
                {
                    CommonSet.dic_OptSuction1[iSuctionNum].lstHomatCamera[i] = null;
                    CommonSet.dic_OptSuction1[iSuctionNum].lstHomatSuction[i] = null;
                }

                CommonSet.dic_OptSuction1[iSuctionNum] = optSuction;
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = CommonSet.dic_OptSuction1.Values.ToList();
            }
            else
            {
                OptSution2 optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
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


                p = new Point();
                p.X = (double)nudPX4.Value;
                p.Y = (double)nudPY4.Value;
                optSuction.lstP4[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX5.Value;
                p.Y = (double)nudPY5.Value;
                optSuction.lstP5[iListIndex] = p;

                p = new Point();
                p.X = (double)nudPX6.Value;
                p.Y = (double)nudPY6.Value;
                optSuction.lstP6[iListIndex] = p;

                OptSution2.dExposureTimeUp = (double)nudCamUpExposure.Value;
                OptSution2.dGainUp = (double)nudGainCamUp.Value;
                OptSution2.dLightUp = (double)nudCamUpLight.Value;

                OptSution2.dExposureTimeDown = (double)nudCamDownExposure.Value;
                OptSution2.dGainDown = (double)nudGainCamDown.Value;
                OptSution2.dLightDown = (double)nudCamDownLight.Value;

                optSuction.DPosCameraDownX = (double)nudCamDownPos.Value;

                optSuction.DGetTime = (double)nudGetTime.Value;
                optSuction.DGetPosZ = (double)nudGetPosZ.Value;

                OptSution2.DCorrectPosZ = (double)nudCorrectZ.Value;
                OptSution2.DCorrectTime = (double)nudCorrentTime.Value;

                OptSution2.DCorrectPosZ = (double)nudCorrectZ.Value;
                OptSution2.DCorrectTime = (double)nudCorrentTime.Value;
                OptSution2.dFlashStartX = (double)nudFalshStartX.Value;
                OptSution2.dPutTime = (double)nudPutTime.Value;
                OptSution2.pPutPos.X = (double)nudPutX.Value;
               // OptSution2.pPutPos.Y = (double)nudPutY.Value;
                OptSution2.pPutPos.Z = (double)nudPutZ.Value;

                OptSution2.pDiscard.X = (double)nudDiscardX.Value;
                OptSution2.pDiscard.Y = (double)nudDiscardY.Value;
                OptSution2.pDiscard.Z = (double)nudDiscardZ.Value;

                OptSution2.lstCalibCameraUpXY[0].X = (double)nudCamUpPosX1.Value;
                OptSution2.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
                OptSution2.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

                OptSution2.lstCalibCameraUpXY[1].X = (double)nudCamUpPosX2.Value;
                OptSution2.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
                OptSution2.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

                OptSution2.lstCalibCameraDownXY[0].X = (double)nudCamDownPosX1.Value;
                OptSution2.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
                OptSution2.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

                OptSution2.lstCalibCameraDownXY[1].X = (double)nudCamDownPosX2.Value;
                OptSution2.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
                OptSution2.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;

                optSuction.pSuctionCenter.X = (double)nudSuctionRow.Value;
                optSuction.pSuctionCenter.Y = (double)nudSuctionCol.Value;

               
             

                for (int i = 0; i < 9; i++)
                {
                    CommonSet.dic_OptSuction2[iSuctionNum].lstHomatCamera[i] = null;
                    CommonSet.dic_OptSuction2[iSuctionNum].lstHomatSuction[i] = null;
                }

                CommonSet.dic_OptSuction2[iSuctionNum] = optSuction;
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = CommonSet.dic_OptSuction2.Values.ToList();
            }
            //bsave = false;
        }

        #region "绘制 GroupBox"
        private void groupBox8_Paint(object sender, PaintEventArgs e)
        {

            groupBox_Paint(sender, e);

        }
        private void groupBox_Paint(object sender, PaintEventArgs e) {
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
               fontSize2 = e.Graphics.MeasureString(gp.Text.Substring(0,pos), gp.Font);
           }
           
           Pen p = new Pen(Brushes.LightSteelBlue,fontSize.Height+12);
           e.Graphics.DrawLine(p, 1, 1, gp.Width-2, 1);
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

            p = new Pen(Brushes.LightSteelBlue,  4);
            e.Graphics.DrawLine(p, 1, 1, 1, gp.Height );
            e.Graphics.DrawLine(p, 1, gp.Height - 2, gp.Width - 2, gp.Height - 2);
            e.Graphics.DrawLine(p, gp.Width - 2, 1, gp.Width - 2, gp.Height );
           

        }
        #endregion

        private void radioButton18_Click(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            int index = Convert.ToInt32(rbtn.Text);
            if (rbtn.Checked)
            {
                if (iSuctionNum != index)
                {
                    iSuctionNum = index;
                    if (iSuctionNum > SuctionSUM)
                        iStation = 2;
                    else
                        iStation = 1;
                    LoadCbBarrel();
                    InitTray();
                }
            }
            

        }

        private void nudIndex1_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex1.Value);
            lstPoint.Add((int)nudIndex2.Value);
            lstPoint.Add((int)nudIndex3.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
            nudIndex4.Value = nudIndex1.Value;
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
            nudIndex5.Value = nudIndex2.Value;
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
            nudIndex6.Value = nudIndex3.Value;

            if (bInit)
            {
                SaveUI();
            }
        }

       
        private void txtSuctionName_TextChanged(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                CommonSet.dic_OptSuction1[iSuctionNum].Name = txtSuctionName.Text.Trim();
            }
            else
            {
                CommonSet.dic_OptSuction2[iSuctionNum].Name = txtSuctionName.Text.Trim();
            }
        }
        #region"托盘点位"
        private void nudPX1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
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

        private void btnGet4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX4.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY4.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

        private void btnGet5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX5.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY5.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

        private void btnGet6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPX6.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPY6.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

        private void btnGo4_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP4[iListIndex].X);
                double y = (optSuction.lstP4[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP4[iListIndex].X);
                double y = (optSuction.lstP4[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }

        private void btnGo5_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP5[iListIndex].X);
                double y = (optSuction.lstP5[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP5[iListIndex].X);
                double y = (optSuction.lstP5[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }

        private void btnGo6_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP6[iListIndex].X);
                double y = (optSuction.lstP6[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP6[iListIndex].X);
                double y = (optSuction.lstP6[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }

        private void btnGo1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01))&&(iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP1[iListIndex].X);
                double y = (optSuction.lstP1[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP1[iListIndex].X);
                double y = (optSuction.lstP1[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }

        private void btnGo2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP2[iListIndex].X);
                double y = (optSuction.lstP2[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP2[iListIndex].X);
                double y = (optSuction.lstP2[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }

        private void btnGo3_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.lstP3[iListIndex].X);
                double y = (optSuction.lstP3[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.lstP3[iListIndex].X);
                double y = (optSuction.lstP3[iListIndex].Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);
            }
        }
        #endregion


        private void btnTriggerCamUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camUpA1 != null)
                    {
                        CommonSet.camUpA1.SetExposure(OptSution1.dExposureTimeUp);
                        CommonSet.camUpA1.SetGain(OptSution1.dGainUp);
                        mc.setDO(DO.取料上相机1, true);
                    }
                }
                else
                {
                    if (CommonSet.camUpA2 != null)
                    {
                        CommonSet.camUpA2.SetExposure(OptSution2.dExposureTimeUp);
                        CommonSet.camUpA2.SetGain(OptSution2.dGainUp);
                        mc.setDO(DO.取料上相机2, true);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCamUp_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camUpA1 != null)
                    {
                        mc.setDO(DO.取料上相机1, false);
                    }
                }
                else
                {
                    if (CommonSet.camUpA2 != null)
                    {
                        mc.setDO(DO.取料上相机2, false);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCamUpCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckUpA1(CommonSet.dic_OptSuction1[iSuctionNum].strPicCameraUpName);
                }
                else
                {
                    CheckUpA2(CommonSet.dic_OptSuction2[iSuctionNum].strPicCameraUpName);
                }
            }
            catch (Exception)
            {
                
               
            }
            

        }
        private void CheckUpA1(string strPrcName)
        {
            OptSution1 suction = CommonSet.dic_OptSuction1[iSuctionNum];
            HObject hImage = CommonSet.dic_OptSuction1[1].hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
           // string strProcessName = suction.strPicCameraUpName;
            suction.InitImageUp(strPrcName);

            if (suction.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            suction.pfUp.SetRun(true);
            suction.pfUp.hwin = hWindowControl1.HalconWindow;
            suction.actionUp(hImage);
            suction.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultUp.CenterColumn.ToString("0.000");

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
        private void CheckUpA2(string strPrcName)
        {
            OptSution2 suction = CommonSet.dic_OptSuction2[iSuctionNum];
            HObject hImage = CommonSet.dic_OptSuction2[1].hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
           // string strProcessName = suction.strPicCameraUpName;
            suction.InitImageUp(strPrcName);

            if (suction.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            suction.pfUp.SetRun(true);
            suction.pfUp.hwin = hWindowControl1.HalconWindow;
            suction.actionUp(hImage);
            suction.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultUp.CenterColumn.ToString("0.000");

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
        private void CheckDownA1(string strPrcName)
        {
            OptSution1 suction = CommonSet.dic_OptSuction1[iSuctionNum];
            HObject hImage = CommonSet.dic_OptSuction1[1].hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            //string strProcessName = suction.strPicDownProduct;
            suction.InitImageDown(strPrcName);

            if (suction.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            suction.pfDown.SetRun(true);
            suction.pfDown.hwin = hWindowControl2.HalconWindow;
            suction.actionDown(hImage);
            suction.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultDown.CenterColumn.ToString("0.000");
            CommonSet.dic_OptSuction1[iSuctionNum].imgResultDown.CenterRow = suction.imgResultDown.CenterRow;
            CommonSet.dic_OptSuction1[iSuctionNum].imgResultDown.CenterColumn = suction.imgResultDown.CenterColumn;

            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, suction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "black", "true");
            if (suction.imgResultDown.bImageResult)
                CommonSet.disp_message(hWindowControl2.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl2.HalconWindow, "NG", "window", 60, 0, "red", "true");

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl2.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckDownA2(string strPrcName)
        {
            OptSution2 suction = CommonSet.dic_OptSuction2[iSuctionNum];
            HObject hImage = CommonSet.dic_OptSuction2[10].hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            //string strProcessName = suction.strPicDownProduct;
            suction.InitImageDown(strPrcName);

            if (suction.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            suction.pfDown.SetRun(true);
            suction.pfDown.hwin = hWindowControl2.HalconWindow;
            suction.actionDown(hImage);
            suction.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + suction.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + suction.imgResultDown.CenterColumn.ToString("0.000");
            CommonSet.dic_OptSuction2[iSuctionNum].imgResultDown.CenterRow = suction.imgResultDown.CenterRow;
            CommonSet.dic_OptSuction2[iSuctionNum].imgResultDown.CenterColumn = suction.imgResultDown.CenterColumn;

            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, suction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "black", "true");
            if (suction.imgResultDown.bImageResult)
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
            if (iStation == 1)
            {
                strName = CommonSet.dic_OptSuction1[iSuctionNum].strPicCameraUpName;
                hoImage = CommonSet.dic_OptSuction1[1].hImageUP;
                if (strName.Equals(""))
                {
                    strName = "OptS" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction1[iSuctionNum].strPicCameraUpName = strName;
                }
            }
            else
            {
                strName = CommonSet.dic_OptSuction2[iSuctionNum].strPicCameraUpName;
                hoImage = CommonSet.dic_OptSuction2[10].hImageUP;
                if (strName.Equals(""))
                {
                    strName = "OptS" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction2[iSuctionNum].strPicCameraUpName = strName;
                }
            }
            
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamUpImg_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                if (CommonSet.dic_OptSuction1[1].hImageUP == null)
                {
                    MessageBox.Show("请先获取图像！");
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "S" + iSuctionNum.ToString() + DateTime.Now.ToString("yyMMddHHmmss");
                sfd.Filter = "bmp图片(*.bmp)|*.bmp";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    HOperatorSet.WriteImage(CommonSet.dic_OptSuction1[1].hImageUP, "bmp", 0, fileName);
                }
            }
            else
            {
                if (CommonSet.dic_OptSuction2[10].hImageUP == null)
                {
                    MessageBox.Show("请先获取图像！");
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "S" + iSuctionNum.ToString() + DateTime.Now.ToString("yyMMddHHmmss");
                sfd.Filter = "bmp图片(*.bmp)|*.bmp";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    HOperatorSet.WriteImage(CommonSet.dic_OptSuction2[10].hImageUP, "bmp", 0, fileName);
                }
                
            }
           

           
        
        }

        private void nudCamUpExposure_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
            //camUp.SetExposure((double)nudCamUpExposure.Value);
           
        }

        private void nudGainCamUp_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
           // camUp.SetGain((double)nudGainCamUp.Value);
        }

        private void nudCamUpLight_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (iStation == 1)
            {
                // OptSution1.dExposureTimeUp = (double)nudCamUpExposure.Value;
               // CommonSet.camUpA1.SetExposure((double)nudCamUpLight.Value);
            }
            else
            {
               // CommonSet.camUpA2.SetExposure((double)nudCamUpLight.Value);
            }
        }

        private void btnCamDownCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckDownA1(CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct);
                }
                else
                {
                    CheckDownA2(CommonSet.dic_OptSuction2[iSuctionNum].strPicDownProduct);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("下相机检测异常"+ex.ToString());
            }
        }

        private void btnCamDownCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct;
                hoImage = CommonSet.dic_OptSuction1[1].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptDownS" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct = strName;
                }
            }
            else
            {
                strName = CommonSet.dic_OptSuction2[iSuctionNum].strPicDownProduct;
                hoImage = CommonSet.dic_OptSuction2[10].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptDownS" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction2[iSuctionNum].strPicDownProduct = strName;
                }
            }
           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamDownImg_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                if (CommonSet.dic_OptSuction1[1].hImageDown == null)
                {
                    MessageBox.Show("请先获取图像！");
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "S" + iSuctionNum.ToString() + DateTime.Now.ToString("yyMMddHHmmss");
                sfd.Filter = "bmp图片(*.bmp)|*.bmp";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    HOperatorSet.WriteImage(CommonSet.dic_OptSuction1[1].hImageDown, "bmp", 0, fileName);
                }
            }
            else
            {
                if (CommonSet.dic_OptSuction2[10].hImageDown == null)
                {
                    MessageBox.Show("请先获取图像！");
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "S" + iSuctionNum.ToString() + DateTime.Now.ToString("yyMMddHHmmss");
                sfd.Filter = "bmp图片(*.bmp)|*.bmp";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    HOperatorSet.WriteImage(CommonSet.dic_OptSuction2[10].hImageDown, "bmp", 0, fileName);
                }

            }
           
        }

        private void btnTriggerCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camDownA1 != null)
                    {
                        CommonSet.camDownA1.SetExposure(OptSution1.dExposureTimeDown);
                        CommonSet.camDownA1.SetGain(OptSution1.dGainDown);
                        mc.setDO(DO.取料下相机1, true);
                    }
                }
                else
                {
                    if (CommonSet.camDownA2 != null)
                    {
                        CommonSet.camDownA2.SetExposure(OptSution2.dExposureTimeDown);
                        CommonSet.camDownA2.SetGain(OptSution2.dGainDown);
                        mc.setDO(DO.取料下相机2, true);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCamDown_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camDownA1 != null)
                    {
                        mc.setDO(DO.取料下相机1, false);
                    }
                }
                else
                {
                    if (CommonSet.camDownA2 != null)
                    {
                        mc.setDO(DO.取料下相机2, false);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void nudCamDownExposure_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
            //camDown.SetExposure((double)nudCamDownExposure.Value);
        }

        private void nudGainCamDown_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
            //camDown.SetGain((double)nudGainCamDown.Value);
        }

        private void nudCamDownLight_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;

            if (iStation == 1)
            {
                // OptSution1.dExposureTimeUp = (double)nudCamUpExposure.Value;
                // CommonSet.camUpA1.SetExposure((double)nudCamUpLight.Value);
            }
            else
            {
                // CommonSet.camUpA2.SetExposure((double)nudCamUpLight.Value);
            }
        }

        private void nudCamDownPos_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void btnGetCamDownPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudCamDownPos.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoCamDownPos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.DPosCameraDownX);
               
                mc.AbsMove(axisX, x, iVelRunX);
              
            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double x = (optSuction.DPosCameraDownX);
               
                mc.AbsMove(axisX, x, iVelRunX);
               
            }
        }

        private void cbUseCam_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                CommonSet.dic_OptSuction1[iSuctionNum].BUseCameraUp = !CommonSet.dic_OptSuction1[iSuctionNum].BUseCameraUp;
            }
            else
            {
                CommonSet.dic_OptSuction2[iSuctionNum].BUseCameraUp = !CommonSet.dic_OptSuction2[iSuctionNum].BUseCameraUp;
            }
        }

        private void nudGetTime_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();

        }

        private void btnGetZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudGetPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double z= (optSuction.DGetPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
            else
            {
                var optSuction = CommonSet.dic_OptSuction2[iSuctionNum];
                double z = (optSuction.DGetPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetFlashX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudFalshStartX.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoFlashX_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
               
                double x = (OptSution1.dFlashStartX);

                mc.AbsMove(axisX, x, iVelRunX);

            }
            else
            {

                double x = (OptSution2.dFlashStartX);

                mc.AbsMove(axisX, x, iVelRunX);

            }
        }

       


        private void btnGoPutXY_Click_1(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                if (AssembleSuction1.pSafeXYZ.X - 10 > mc.dic_Axis[AXIS.组装X1轴].dPos)
                {
                    MessageBox.Show("组装X1超过安全位");
                    return;
                }
                double x = (OptSution1.pPutPos.X);

                mc.AbsMove(axisX, x, iVelRunX);

            }
            else
            {
                if (AssembleSuction2.pSafeXYZ.X - 10 > mc.dic_Axis[AXIS.组装X2轴].dPos)
                {
                    MessageBox.Show("组装X2超过安全位");
                    return;
                }

                double x = (OptSution2.pPutPos.X);

                mc.AbsMove(axisX, x, iVelRunX);

            }
        }

        private void btnGoPutZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                double z = (OptSution1.pPutPos.Z);

                mc.AbsMove(axisZ, z, iVelRunZ);
            }
            else if (iStation == 2)
            {
                double z = (OptSution2.pPutPos.Z);

                mc.AbsMove(axisZ, z, iVelRunZ);
            }
        }

        private void btnGetPutXYZ_Click_1(object sender, EventArgs e)
        {
           
                if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return;
                nudPutX.Value = (decimal)mc.dic_Axis[axisX].dPos;
                nudPutZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
           
        }

        private void 取料X1轴P_Click(object sender, EventArgs e)
        {
            if (rbtProductC.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();

            int len = name.Length;
            string direct = name.Substring(len - 1);
            AXIS axis;
            int vel = 0;
            if (name.Contains("X"))
            {
                axis = axisX;
                vel = iVelRunX;
            }
            else if (name.Contains("Y"))
            {
                axis = axisY;
                vel = iVelRunY;
            }
            else
            {
                axis = axisZ;
                vel = iVelRunZ;
            }
            double posNow = mc.dic_Axis[axis].dPos;
            
            if (direct.Equals("P"))
            {
                double newPos = ((posNow + (double)nudProductSize.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                double newPos = ((posNow - (double)nudProductSize.Value));
                mc.AbsMove(axis, newPos, vel);
            }
        }

        private void 取料X1轴P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtProductA.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            if (name.Contains("X") || name.Contains("Y"))
            {
                if (mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01))
                {
                    MessageBox.Show("取料Z1轴低于安全高度!");
                    return;
                }
                if (mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01))
                {
                    MessageBox.Show("取料Z2轴低于安全高度!");
                    return;
                }
            }
            int len = name.Length;
            string direct = name.Substring(len - 1);
            AXIS axis;
            int vel = 0;
            if (name.Contains("X"))
            {
                axis = axisX;
                vel = iVelRunX;
            }
            else if (name.Contains("Y"))
            {
                axis = axisY;
                vel = iVelRunY;
            }
            else
            {
                axis = axisZ;
                vel = iVelRunZ;
            }
           // double posNow = mc.dic_Axis[axis].dPos;

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

        private void 取料X1轴P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtProductA.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
          
            int len = name.Length;
            string direct = name.Substring(len - 1);
            AXIS axis;
            int vel = 0;
            if (name.Contains("X"))
            {
                axis = axisX;
                vel = iVelRunX;
            }
            else if (name.Contains("Y"))
            {
                axis = axisY;
                vel = iVelRunY;
            }
            else
            {
                axis = axisZ;
                vel = iVelRunZ;
            }
            mc.StopAxis(axis);
        }

        private void lblSuction_Click(object sender, EventArgs e)
        {
            string strDo = "取料气缸下降" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblGet_Click(object sender, EventArgs e)
        {
            string strDo = "取料吸真空" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblPut_Click(object sender, EventArgs e)
        {
            string strDo = "取料破真空" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
            if (CommonSet.bUseAutoParam)
            {
                CommonSet.dOutTest = dOut;
                if (iSuctionNum < 10)
                    CommonSet.dOutTestTime = (long)(OptSution1.dPutTime * 1000);
                else
                    CommonSet.dOutTestTime = (long)(OptSution2.dPutTime * 1000);
                string strD = "取料吸真空" + iSuctionNum.ToString();
                DO dO1 = (DO)Enum.Parse(typeof(DO), strD);
                CommonSet.dOutIn = dO1;
            }
            else
            {
                mc.setDO(dOut, !mc.dic_DO[dOut]);
            }
        }

        private void btnSafeZ1_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.取料Z1轴, OptSution1.pSafeXYZ.Z, (int)iVelRunZ);
        }

        private void btnSafeZ2_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.取料Z2轴, OptSution2.pSafeXYZ.Z, (int)iVelRunZ);
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
               if(iStation == 1)
                HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageUP, hwin);
               else
                HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageUP, hwin);
                //control.DisplayImage(mc.hwindow, ho_Image, startX, startY, endX, endY);
                // displayObj();

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
                    if (iStation == 1)
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageUP, hwin);
                    else
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageUP, hwin);
                    return;
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
                    if (iStation == 1)
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageDown, hwin);
                    else
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageDown, hwin);
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
                if (iStation == 1)
                    HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageDown, hwin);
                else
                    HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageDown, hwin);
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
            try
            {

           
                if (iStation == 1)
                {
                    GetProductTestModule.currentPoint = CommonSet.dic_OptSuction1[iSuctionNum].getCoordinateByIndex((int)nudTestNum.Value);
                    GetProductTestModule.currentPoint.Z = CommonSet.dic_OptSuction1[iSuctionNum].DGetPosZ;
                    GetProductTestModule.axisX = AXIS.取料X1轴;
                    GetProductTestModule.axisY = AXIS.取料Y1轴;
                    GetProductTestModule.axisZ = AXIS.取料Z1轴;
                    GetProductTestModule.dGetTime = CommonSet.dic_OptSuction1[iSuctionNum].DGetTime;
                    GetProductTestModule.dSuction = (DO)Enum.Parse(typeof(DO), "取料气缸下降" + iSuctionNum.ToString());
                    GetProductTestModule.dGet = (DO)Enum.Parse(typeof(DO), "取料吸真空" + iSuctionNum.ToString());
                }
                else
                {
                    GetProductTestModule.currentPoint = CommonSet.dic_OptSuction2[iSuctionNum].getCoordinateByIndex((int)nudTestNum.Value);
                    GetProductTestModule.currentPoint.Z = CommonSet.dic_OptSuction2[iSuctionNum].DGetPosZ;
                
                    GetProductTestModule.axisX = AXIS.取料X2轴;
                    GetProductTestModule.axisY = AXIS.取料Y2轴;
                    GetProductTestModule.axisZ = AXIS.取料Z2轴;
                    GetProductTestModule.dGetTime = CommonSet.dic_OptSuction2[iSuctionNum].DGetTime;
                    GetProductTestModule.dSuction = (DO)Enum.Parse(typeof(DO), "取料气缸下降" + iSuctionNum.ToString());
                    GetProductTestModule.dGet = (DO)Enum.Parse(typeof(DO), "取料吸真空" + iSuctionNum.ToString());

                }
           
                FrmGetTest f = new FrmGetTest();
                f.ShowDialog();
                f = null;

            }
            catch (Exception)
            {

               
            }
        }

        private void btnGetSuctionCenter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前吸笔中心位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            try
            {
                if (iStation == 1)
                {
                    nudSuctionRow.Value = (decimal)CommonSet.dic_OptSuction1[iSuctionNum].imgResultDown.CenterRow;
                    nudSuctionCol.Value = (decimal)CommonSet.dic_OptSuction1[iSuctionNum].imgResultDown.CenterColumn;
                }
                else
                {
                    nudSuctionRow.Value = (decimal)CommonSet.dic_OptSuction2[iSuctionNum].imgResultDown.CenterRow;
                    nudSuctionCol.Value = (decimal)CommonSet.dic_OptSuction2[iSuctionNum].imgResultDown.CenterColumn;
                }

            }
            catch (Exception)
            {
                
               
            }
          
        }

        private void btnCenterTest_Click(object sender, EventArgs e)
        {
            try
            {


                if (iStation == 1)
                {

                    AutoGetCenterPosTestModule.axisX = AXIS.取料X1轴;
                    AutoGetCenterPosTestModule.axisY = AXIS.取料Y1轴;
                    AutoGetCenterPosTestModule.axisZ = AXIS.取料Z1轴;
                    AutoGetCenterPosTestModule.dSafeZ = OptSution1.pSafeXYZ.Z;
                    AutoGetCenterPosTestModule.iCamPos = 1;
                    AutoGetCenterPosTestModule.iSuctionNum = iSuctionNum;
                }
                else
                {
                    AutoGetCenterPosTestModule.axisX = AXIS.取料X2轴;
                    AutoGetCenterPosTestModule.axisY = AXIS.取料Y2轴;
                    AutoGetCenterPosTestModule.axisZ = AXIS.取料Z2轴;
                    AutoGetCenterPosTestModule.dSafeZ = OptSution2.pSafeXYZ.Z;
                    AutoGetCenterPosTestModule.iCamPos = 2;
                    AutoGetCenterPosTestModule.iSuctionNum = iSuctionNum;

                }

                FrmAutoCenter f = new FrmAutoCenter();
                f.ShowDialog();
                f = null;

            }
            catch (Exception)
            {


            }
        }

        private void btnCenterCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = CommonSet.dic_OptSuction1[iSuctionNum].strPicDownSuctionCenterName;
                hoImage = CommonSet.dic_OptSuction1[1].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptSuction" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction1[iSuctionNum].strPicDownSuctionCenterName = strName;
                }
            }
            else
            {
                strName = CommonSet.dic_OptSuction2[iSuctionNum].strPicDownSuctionCenterName;
                hoImage = CommonSet.dic_OptSuction2[10].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptSuction" + iSuctionNum.ToString();
                    CommonSet.dic_OptSuction2[iSuctionNum].strPicDownSuctionCenterName = strName;
                }
            }

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCenterCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckDownA1(CommonSet.dic_OptSuction1[iSuctionNum].strPicDownSuctionCenterName);
                }
                else
                {
                    CheckDownA2(CommonSet.dic_OptSuction2[iSuctionNum].strPicDownSuctionCenterName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("下相机检测异常" + ex.ToString());
            }
        }

        private void btnGoCorrectZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                double z = (OptSution1.DCorrectPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
            else
            {
                double z = (OptSution2.DCorrectPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetCorrectZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudCorrectZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGetDiscard_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudDiscardX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudDiscardY.Value = (decimal)mc.dic_Axis[axisY].dPos;
            nudDiscardZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoDiscardXY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("取料Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("取料Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
               
                double x = (OptSution1.pDiscard.X);
                double y = OptSution1.pDiscard.Y;

                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);

            }
            else
            {
                double x = (OptSution2.pDiscard.X);
                double y = OptSution2.pDiscard.Y;

                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);

            }
        }

        private void btnGoDiscardZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {

                double x = (OptSution1.pDiscard.X);
                double y = OptSution1.pDiscard.Y;

                if ((Math.Abs(x - mc.dic_Axis[axisX].dPos) > 0.1) || (Math.Abs(y - mc.dic_Axis[axisY].dPos) > 0.1))
                {
                    MessageBox.Show("XY不在抛料位");
                    return;
                }

                double z = (OptSution1.pDiscard.Z);

                mc.AbsMove(axisZ, z, iVelRunZ);
               
            }
            else
            {
                double x = (OptSution2.pDiscard.X);
                double y = OptSution2.pDiscard.Y;

                if ((Math.Abs(x - mc.dic_Axis[axisX].dPos) > 0.1) || (Math.Abs(y - mc.dic_Axis[axisY].dPos) > 0.1))
                {
                    MessageBox.Show("XY不在抛料位");
                    return;
                }

                double z = (OptSution2.pDiscard.Z);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetCamUp1_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    nudCamUpPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR1.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultUp.CenterRow;
                    nudCamUpPosC1.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultUp.CenterColumn;
                    
                }
                else
                {
                    nudCamUpPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR1.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultUp.CenterRow;
                    nudCamUpPosC1.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultUp.CenterColumn;
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamUp2_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    nudCamUpPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR2.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultUp.CenterRow;
                    nudCamUpPosC2.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultUp.CenterColumn;
                }
                else
                {
                    nudCamUpPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR2.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultUp.CenterRow;
                    nudCamUpPosC2.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultUp.CenterColumn;
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamDown1_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    nudCamDownPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR1.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultDown.CenterRow;
                    nudCamDownPosC1.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultDown.CenterColumn;
                }
                else
                {
                    nudCamDownPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR1.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultDown.CenterRow;
                    nudCamDownPosC1.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultDown.CenterColumn;
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamDown2_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    nudCamDownPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR2.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultDown.CenterRow;
                    nudCamDownPosC2.Value = (decimal)CommonSet.dic_OptSuction1[1].imgResultDown.CenterColumn;
                }
                else
                {
                    nudCamDownPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR2.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultDown.CenterRow;
                    nudCamDownPosC2.Value = (decimal)CommonSet.dic_OptSuction2[10].imgResultDown.CenterColumn;
                }

            }
            catch (Exception)
            {


            }
        }

        private void cbUseCalibUp_Click(object sender, EventArgs e)
        {
            if (cbUseCalibUp.Checked)
            {
                OptSution1.dUpAng = OptSution1.GetUpCameraAngle();
                OptSution2.dUpAng = OptSution2.GetUpCameraAngle();
            }
            else
            {
                OptSution1.dUpAng = 0;
                OptSution2.dUpAng = 0;
            }
        }

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            if (cbUseCalibDown.Checked)
            {
                OptSution1.dDownAng = OptSution1.GetDownCameraAngle();
                OptSution2.dDownAng = OptSution2.GetDownCameraAngle();
            }
            else
            {
                OptSution1.dDownAng = 0;
                OptSution2.dDownAng = 0;
            }
        }

        private void btnTestGetProduct_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                ResultTestModule.bUseAssembleParam = false;
                ResultTestModule.bUseOptOnly = true;
                ResultTestModule.bPutBack = cbPutBack.Checked;
                ResultTestModule.currentPoint = CommonSet.dic_OptSuction1[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                ResultTestModule.currentPoint.Z = CommonSet.dic_OptSuction1[iSuctionNum].DGetPosZ;

                ResultTestModule.axisX = AXIS.组装X1轴;
                ResultTestModule.axisZ = AXIS.组装Z1轴;
                ResultTestModule.axisGetX = AXIS.取料X1轴;
                ResultTestModule.axisGetY = AXIS.取料Y1轴;
                ResultTestModule.axisGetZ = AXIS.取料Z1轴;
               // ResultTestModule.dPutZ = (double)nudCheckHeight.Value;
                ResultTestModule.iCurrentSuctionOrder = iSuctionNum;
                //ResultTestModule.iCurrentStation = (int)nudCheckStation.Value;
                if (ResultTestModule.iCurrentStation == 1)
                {
                    ResultTestModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    ResultTestModule.axisY = AXIS.组装Y2轴;
                }

            }
            else
            {
                ResultTestModule.bUseAssembleParam = false;
                ResultTestModule.bUseOptOnly = true;
                ResultTestModule.bPutBack = cbPutBack.Checked;
                ResultTestModule.currentPoint = CommonSet.dic_OptSuction2[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                ResultTestModule.currentPoint.Z = CommonSet.dic_OptSuction2[iSuctionNum].DGetPosZ;
                ResultTestModule.axisX = AXIS.组装X2轴;
                ResultTestModule.axisZ = AXIS.组装Z2轴;
                ResultTestModule.axisGetX = AXIS.取料X2轴;
                ResultTestModule.axisGetY = AXIS.取料Y2轴;
                ResultTestModule.axisGetZ = AXIS.取料Z2轴;
                //ResultTestModule.dPutZ = (double)nudCheckHeight.Value;
                ResultTestModule.iCurrentSuctionOrder = iSuctionNum;
               // ResultTestModule.iCurrentStation = (int)nudCheckStation.Value;
                if (ResultTestModule.iCurrentStation == 1)
                {
                    ResultTestModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    ResultTestModule.axisY = AXIS.组装Y2轴;
                }

            }

            FrmCheckResult frmCheck = new FrmCheckResult();
            frmCheck.ShowDialog();
            frmCheck = null;
        }
        

    }
}
