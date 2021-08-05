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
using Tray;
using CameraSet;
using HalconDotNet;
using ImageProcess;
namespace Assembly
{
    public partial class FrmOpt1Set : Form
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
        AXIS axisX, axisY, axisZ;
        ICamera camUp, camDown;
        public FrmOpt1Set(TrayPanel panel = null)
        {
            axisX = AXIS.取料X1轴;
            axisY = AXIS.取料Y1轴;
            axisZ = AXIS.取料Z1轴;
            iStation = 1;
            mc = MotionCard.getMotionCard();
            InitializeComponent();
        }

        private void FrmOptSet_Load(object sender, EventArgs e)
        {
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
            nudIndex4.Maximum = count;
            nudIndex5.Maximum = count;
            nudIndex6.Maximum = count;
            // ProductSuction suction = CommonSet.dic_ProductSuction1[iSuctionNum];
            
                OptSution1 optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                iListIndex = optSuction.lstTrayNum.IndexOf(iCurrentTrayIndex);
                if (optSuction.lstIndex1[iListIndex] > count)
                {
                    optSuction.lstIndex1[iListIndex] = count;
                    nudIndex4.Value = optSuction.lstIndex1[iListIndex];
                }
                else
                {
                    nudIndex4.Value = optSuction.lstIndex1[iListIndex];
                }

                if (optSuction.lstIndex2[iListIndex] > count)
                {
                    optSuction.lstIndex2[iListIndex] = count;
                    nudIndex5.Value = optSuction.lstIndex2[iListIndex];
                }
                else
                {
                    nudIndex5.Value = optSuction.lstIndex2[iListIndex];
                }
                if (optSuction.lstIndex3[iListIndex] > count)
                {
                    optSuction.lstIndex3[iListIndex] = count;
                    nudIndex6.Value = optSuction.lstIndex3[iListIndex];
                }
                else
                {
                    nudIndex6.Value = optSuction.lstIndex3[iListIndex];
                }

                CommonSet.dic_OptSuction1[iSuctionNum].EndPos = CommonSet.dic_OptSuction1[iSuctionNum].GetCount();
                tray.setNumColor(optSuction.lstIndex1[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex2[iListIndex], CommonSet.ColorNotUse);
                tray.setNumColor(optSuction.lstIndex3[iListIndex], CommonSet.ColorNotUse);
                tray.updateColor();
            
            
            UpdateControl();
            bInit = true;

        }

        bool bAutoUp = true;
        bool bAutoDown = true;

        public void updateUI()
        {
            try
            {
                lblSuctionNum.Text = iSuctionNum.ToString();
                lblCurrentTrayIndex.Text = iCurrentTrayIndex.ToString();


                showListTrayPanel1.SetStaion(iStation);
                showListTrayPanel1.SetSuction(iSuctionNum);
                showListTrayPanel1.UpdateUI();
              
                
                iStation = 1;
                if (camUp != CommonSet.camUpA1)
                {
                    camUp = CommonSet.camUpA1;
                    camDown = CommonSet.camDownA1;
                }
                lblCurrentSum.Text = CommonSet.dic_OptSuction1[iSuctionNum].GetCount().ToString();
                //nudTestNum.Maximum = (decimal)CommonSet.dic_OptSuction1[iSuctionNum].GetCount();
                if (!txtSuctionName.Focused)
                {
                    txtSuctionName.Text = CommonSet.dic_OptSuction1[iSuctionNum].Name;
                }
                //groupBox4.Text = "上相机(取料1)";
                //groupBox5.Text = "下相机(取料1)";
                //groupBox7.Text = "飞拍(取料1)";
                //groupBox8.Text = "放料(中转1)";
                //groupBox9.Text = "操作(取料1)";
                //groupBox11.Text = "求心(工位1)";
                //groupBox13.Text = "抛料位(取料1)";

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
                   
                if (dataGridView1.RowCount > 1)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();

                    }
                }
                   
                

                panelProduct.Visible = rbtProductA.Checked;

                lblSuction.Text = "取料气缸下降" + iSuctionNum.ToString();
                lblGet.Text = "取料吸真空" + iSuctionNum.ToString();
                lblPut.Text = "取料破真空" + iSuctionNum.ToString();

                string strDo = "取料气缸下降" + iSuctionNum.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);

                lblSuction.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "取料吸真空" + iSuctionNum.ToString();
                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblGet.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "取料破真空" + iSuctionNum.ToString();
                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblPut.BackColor = getColor(mc.dic_DO[dOut]);

                //groupBox1.Text = "托盘(吸笔" + iSuctionNum.ToString() + ")";
                //groupBox6.Text = "取料参数(吸笔" + iSuctionNum.ToString() + ")";
                //groupBox10.Text = "取料测试(吸笔" + iSuctionNum.ToString() + ")";
                //groupBox12.Text = "吸笔中心(吸笔" + iSuctionNum.ToString() + ")";

                UpdateControl();

                if (Run.runMode == RunMode.组装验证)
                    lblMakeUp.Text = "位置补偿坐标(X,Y): " + ResultTestModule.dPosX.ToString("0.000") + "," + ResultTestModule.dPosY.ToString("0.000");


                // this.dataGridView1.DataSource = lookUp.ToList();

                btnControls1.UpdateBtnControlUI();
            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("取料1界面更新异常:" + ex.ToString());

            }


        }
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Gray;
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

            //if (bsave)
            //    return;
           
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];


                //setNumerialControl(nudPX1, optSuction.lstP1[iListIndex].X);
                //setNumerialControl(nudPY1, optSuction.lstP1[iListIndex].Y);

                //setNumerialControl(nudPX2, optSuction.lstP2[iListIndex].X);
                //setNumerialControl(nudPY2, optSuction.lstP2[iListIndex].Y);

                //setNumerialControl(nudPX3, optSuction.lstP3[iListIndex].X);
                //setNumerialControl(nudPY3, optSuction.lstP3[iListIndex].Y);

                setNumerialControl(nudPX4, optSuction.lstP4[iListIndex].X);
                setNumerialControl(nudPY4, optSuction.lstP4[iListIndex].Y);

                setNumerialControl(nudPX5, optSuction.lstP5[iListIndex].X);
                setNumerialControl(nudPY5, optSuction.lstP5[iListIndex].Y);

                setNumerialControl(nudPX6, optSuction.lstP6[iListIndex].X);
                setNumerialControl(nudPY6, optSuction.lstP6[iListIndex].Y);

                setNumerialControl(nudExpProduct, OptSution1.dExposureTimeDown);
                setNumerialControl(nudGainProduct, OptSution1.dGainDown);
                //setNumerialControl(nudCamUpLight, OptSution1.dLightUp);
                setNumerialControl(nudExpSuction, OptSution1.dExposureTimeSuction);
                setNumerialControl(nudGainSuction, OptSution1.dGainSuction);

                setNumerialControl(nudCamUpExposure, OptSution1.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, OptSution1.dGainUp);
                setNumerialControl(nudCamUpLight, OptSution1.dLightUp);

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
        //bool bsave = false;
        //保存界面的参数
        public void SaveUI()
        {
            //bsave = true;
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                optSuction.lstIndex1[iListIndex] = (int)nudIndex4.Value;
                optSuction.lstIndex2[iListIndex] = (int)nudIndex5.Value;
                optSuction.lstIndex3[iListIndex] = (int)nudIndex6.Value;

                //Point p = new Point();
                //p.X = (double)nudPX1.Value;
                //p.Y = (double)nudPY1.Value;
                //optSuction.lstP1[iListIndex] = p;

                //p = new Point();
                //p.X = (double)nudPX2.Value;
                //p.Y = (double)nudPY2.Value;
                //optSuction.lstP2[iListIndex] = p;


                //p = new Point();
                //p.X = (double)nudPX3.Value;
                //p.Y = (double)nudPY3.Value;
                //optSuction.lstP3[iListIndex] = p;


                Point p = new Point();
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
                //OptSution1.dLightUp = (double)nudCamUpLight.Value;


                OptSution1.dExposureTimeDown = (double)nudExpProduct.Value;
                OptSution1.dGainDown = (double)nudGainProduct.Value;

                OptSution1.dExposureTimeSuction= (double)nudExpSuction.Value;
                OptSution1.dGainSuction = (double)nudGainSuction.Value;
                //OptSution1.dLightDown = (double)nudCamDownLight.Value;

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
            
            //bsave = false;
        }

        private void nudIndex4_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex4.Value);
            lstPoint.Add((int)nudIndex5.Value);
            lstPoint.Add((int)nudIndex6.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
           
            if (bInit)
            {
                SaveUI();
            }
        }

        private void nudIndex5_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex4.Value);
            lstPoint.Add((int)nudIndex5.Value);
            lstPoint.Add((int)nudIndex6.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);
           
            if (bInit)
            {
                SaveUI();
            }
        }

        private void nudIndex6_ValueChanged(object sender, EventArgs e)
        {
            List<int> lstPoint = new List<int>();
            lstPoint.Add((int)nudIndex4.Value);
            lstPoint.Add((int)nudIndex5.Value);
            lstPoint.Add((int)nudIndex6.Value);
            if (tray != null)
                tray.setPosShowAlone(lstPoint, CommonSet.ColorInit, CommonSet.ColorNotUse);

            if (bInit)
            {
                SaveUI();
            }
        }

        private void radioButton9_Click(object sender, EventArgs e)
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

        

        private void txtSuctionName_TextChanged(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                CommonSet.dic_OptSuction1[iSuctionNum].Name = txtSuctionName.Text.Trim();
            }
        }

        private void nudCamUpPosX1_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }
        #region"托盘参数"
        private void btnTraySelect_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(cbBarrelIndex.Text);
            if (i != iCurrentTrayIndex)
            {
                iCurrentTrayIndex = i;
                InitTray();
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
            
            var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
            double x = (optSuction.lstP4[iListIndex].X);
            double y = (optSuction.lstP4[iListIndex].Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(axisY, y, iVelRunY);
            
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
            
            var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
            double x = (optSuction.lstP5[iListIndex].X);
            double y = (optSuction.lstP5[iListIndex].Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(axisY, y, iVelRunY);
            
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
            
            var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
            double x = (optSuction.lstP6[iListIndex].X);
            double y = (optSuction.lstP6[iListIndex].Y);
            mc.AbsMove(axisX, x, iVelRunX);
            mc.AbsMove(axisY, y, iVelRunY);

        }
        #endregion

        #region"标定"
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
                // OptSution2.dUpAng = OptSution2.GetUpCameraAngle();
            }
            else
            {
                OptSution1.dUpAng = 0;
                // OptSution2.dUpAng = 0;
            }
        }

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            if (cbUseCalibDown.Checked)
            {
                OptSution1.dDownAng = OptSution1.GetDownCameraAngle();
                //OptSution2.dDownAng = OptSution2.GetDownCameraAngle();
            }
            else
            {
                OptSution1.dDownAng = 0;
                // OptSution2.dDownAng = 0;
            }
        }

        #endregion
        #region"中转"
        private void btnGetPutXYZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudPutX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudPutZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoPutXY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("取料Z1轴低于安全位！");
                return;
            }
          
         
            if (AssembleSuction1.pSafeXYZ.X - 10 > mc.dic_Axis[AXIS.组装X1轴].dPos)
            {
                MessageBox.Show("组装X1轴超过安全位");
                return;
            }
            double x = (OptSution1.pPutPos.X);

            mc.AbsMove(axisX, x, iVelRunX);

            
        }

        private void btnGoPutZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(OptSution1.pPutPos.X - mc.dic_Axis[AXIS.取料X1轴].dPos) > 0.01)
            {

                MessageBox.Show("取料X1轴不在中转放料位！");
                return;
            }

            double z = (OptSution1.pPutPos.Z);

            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnGetCorrectZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudCorrectZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoCorrectZ_Click(object sender, EventArgs e)
        {
            if (Math.Abs(OptSution1.pPutPos.X - mc.dic_Axis[AXIS.取料X1轴].dPos) > 0.01)
            {

                MessageBox.Show("取料X1轴不在中转放料位！");
                return;
            }
            double z = (OptSution1.DCorrectPosZ);
            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        #endregion
        #region"取料"
        private void btnGetCamDownPos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudCamDownPos.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoCamDownPos_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("取料Z1轴低于安全位！");
                return;
            }
           
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (optSuction.DPosCameraDownX);

                mc.AbsMove(axisX, x, iVelRunX);

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
        private void btnCamDownCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckDownA1(CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("下相机检测异常" + ex.ToString());
            }
        }

        private void btnCamDownCheckSet_Click(object sender, EventArgs e)
        {

            HObject hoImage;
            string strName = "";
           
            strName = CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct;
            hoImage = CommonSet.dic_OptSuction1[1].hImageDown;
            if (strName.Equals(""))
            {
                strName = "OptDownS" + iSuctionNum.ToString();
                CommonSet.dic_OptSuction1[iSuctionNum].strPicDownProduct = strName;
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
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("下相机检测异常" + ex.ToString());
            }
        }

        private void btnCenterCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
          
            strName = CommonSet.dic_OptSuction1[iSuctionNum].strPicDownSuctionCenterName;
            hoImage = CommonSet.dic_OptSuction1[1].hImageDown;
            if (strName.Equals(""))
            {
                strName = "OptSuction" + iSuctionNum.ToString();
                CommonSet.dic_OptSuction1[iSuctionNum].strPicDownSuctionCenterName = strName;
            }
           

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;

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
              

            }
            catch (Exception)
            {


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
           
            if (iStation == 1)
            {

                double x = (OptSution1.dFlashStartX);

                mc.AbsMove(axisX, x, iVelRunX);

            }
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
                double z = (optSuction.DGetPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
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
            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("取料Z1轴低于安全位！");
                return;
            }
          
            if (iStation == 1)
            {

                double x = (OptSution1.pDiscard.X);
                double y = OptSution1.pDiscard.Y;

                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(axisY, y, iVelRunY);

            }
        }

        private void btnGoDiscardZ_Click(object sender, EventArgs e)
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

        private void btnTriggerCamDownProduct_MouseDown(object sender, MouseEventArgs e)
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
               
            }
            catch (Exception)
            {


            }
        }
        private void btnTriggerCamDownSuction_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camDownA1 != null)
                    {
                        CommonSet.camDownA1.SetExposure(OptSution1.dExposureTimeSuction);
                        CommonSet.camDownA1.SetGain(OptSution1.dGainSuction);
                        mc.setDO(DO.取料下相机1, true);
                    }
                }

            }
            catch (Exception)
            {


            }
        }
        private void btnTriggerCamDownProduct_MouseUp(object sender, MouseEventArgs e)
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
               
            }
            catch (Exception)
            {


            }
        }

        #endregion

        private void X轴P_Click(object sender, EventArgs e)
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

        private void X轴P_MouseDown(object sender, MouseEventArgs e)
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

        private void X轴P_MouseUp(object sender, MouseEventArgs e)
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

        private void btnTriggerCamUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camUpA1 != null)
                    {
                        CommonSet.camUpA1.SetExposure((double)nudCamUpExposure.Value);
                        CommonSet.camUpA1.SetGain((double)nudGainCamUp.Value);
                        mc.setDO(DO.取料上相机1, true);
                    }
                }
               
            }
            catch (Exception)
            {


            }
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
        }

        private void btnTriggerCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camDownA1 != null)
                    {
                        CommonSet.camDownA1.SetExposure((double)nudCamDownExposure.Value);
                        CommonSet.camDownA1.SetGain((double)nudGainCamDown.Value);
                        mc.setDO(DO.取料下相机1, true);
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

            }
            catch (Exception)
            {


            }
        }

        private void btnSaveCamDownImg_Click(object sender, EventArgs e)
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

        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
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

        private void btnTestGetProduct_Click(object sender, EventArgs e)
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



    }
}
