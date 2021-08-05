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
using HalconDotNet;
using ImageProcess;
using CameraSet;
using ConfigureFile;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    public partial class AssembleUI : UserControl
    {

        public int iSuctionNum = 1;//当前吸笔序号
        public int iStation = 1;//当前站号
        public int iCurrentTrayIndex = 0;//当前盘索引
        public int iListIndex = 0;//存储到OptSuction所有List索引
        MotionCard mc = null;
        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        bool bInit = false;//代表是否初始化完成
        int SuctionSUM = 9;//工站吸笔总数
        // ProductSuction suction = null;
        AXIS axisX, axisZ;
        ICamera camUp, camDown;
        public static bool bGarbData = true;
        
        public AssembleUI()
        {
            if (iSuctionNum <= SuctionSUM)
            {
                // suction = CommonSet.dic_ProductSuction[iSuctionNum];
                axisX = AXIS.组装X1轴;

                axisZ = AXIS.组装Z1轴;
                iStation = 1;
                // CommonSet.iCameraDownFlag = 1;
            }
            else
            {
                iStation = 2;
                axisX = AXIS.组装X2轴;

                axisZ = AXIS.组装Z2轴;
                // CommonSet.iCameraDownFlag = 2;
            }
            mc = MotionCard.getMotionCard();
            InitializeComponent();
        }
        bool bAutoUp = true;
        bool bAutoDown = true;
        int iStationFlag = 1;
        Stopwatch swSaveData = new Stopwatch();
        public void updateUI()
        {
            try
            {
                lblSuctionNum.Text = iSuctionNum.ToString();
               
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
                  
                    if (!txtSuctionName.Focused)
                    {
                        txtSuctionName.Text = CommonSet.dic_OptSuction2[iSuctionNum].Name;
                    }

                    groupBox4.Text = "上相机(组装2)";
                    groupBox5.Text = "下相机(组装2)";
                    groupBox7.Text = "组装飞拍(组装2)";
                    groupBox8.Text = "组装取料(中转2)";
                    groupBox3.Text = "组装求心(中转2)";
                    groupBox9.Text = "操作(组装2)";
                    groupBox11.Text = "抛料(组装2)";
                    groupBox12.Text = "吸笔平面校正位(组装2)";
                    groupBox13.Text = "吸笔中心(吸笔"+iSuctionNum.ToString()+")";
                    groupBox14.Text = "上相机标定(组装2)";
                    groupBox15.Text = "下相机标定(组装2)";

                    lblAxisX.Text = "组装X2轴";
                    lblAxisZ.Text = "组装Z2轴";
                    label39.Text = "组装Y2轴";

                    lblPosX.Text = mc.dic_Axis[AXIS.组装X2轴].dPos.ToString("0.000");
                    lblPosZ.Text = mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString("0.000");
                    nudCamDownExposure.Value = (decimal)AssembleSuction2.dExposureTimeDown;
                    nudCamUpExposure.Value = (decimal)AssembleSuction2.dExposureTimeUp;
                    nudGainCamDown.Value = (decimal)AssembleSuction2.dGainDown;
                    nudGainCamUp.Value = (decimal)AssembleSuction2.dGainUp;
                    if (cbContinusCamDown.Checked && (Run.runMode == RunMode.手动))
                    {
                        if (CommonSet.camDownC2 != null)
                        {
                            CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);
                            CommonSet.camDownC2.SetGain(AssembleSuction2.dGainDown);
                            bAutoDown = !bAutoDown;
                            mc.setDO(DO.组装下相机2, bAutoDown);
                        }
                    }
                    if (cbContinousCamUp.Checked && (Run.runMode == RunMode.手动))
                    {
                        if (CommonSet.camUpC2 != null)
                        {
                            //CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                            //CommonSet.camUpC2.SetGain(AssembleSuction2.dGainUp);
                            bAutoUp = !bAutoUp;
                            if (bGarbData)
                            {
                                mc.setDO(DO.组装上相机1, true);
                                System.Threading.Thread.Sleep(100);
                                mc.setDO(DO.组装上相机1, false);
                                System.Threading.Thread.Sleep(200);
                                CheckUpC1(AssembleSuction1.strPicCameraUpCheckName);
                                System.Threading.Thread.Sleep(200);
                               
                                mc.setDO(DO.组装上相机2, true);
                                System.Threading.Thread.Sleep(100);
                                mc.setDO(DO.组装上相机2, false);
                                System.Threading.Thread.Sleep(200);
                                CheckUpC2(AssembleSuction2.strPicCameraUpCheckName);
                                System.Threading.Thread.Sleep(200);
                            }

                        }
                    }
                    if (iStationFlag == 1)
                    {
                        this.dataGridView1.AutoGenerateColumns = false;
                        this.dataGridView1.DataSource = CommonSet.dic_Assemble2.Values.ToList();

                    }
                    for (int i = 0; i < 9; i++)
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 10).ToString();

                    }
                    iStationFlag = 2;
                    lblShowStation.Text = "工位2吸笔参数：";
                    label34.Text = "吸笔10抛料位1";
                    label37.Text = "吸笔10抛料位2";
                    //btnTrigger.Enabled = CommonSet.camDown2.bTrigger;
                    //cbContinous.Checked = !CommonSet.camDown2.bTrigger;
                    if (iSuctionNum == 10)
                    {
                        nudCheckPosX.Enabled = true;
                        nudCheckPosY.Enabled = true;
                        nudCheckPosZ.Enabled = true;
                        btnGetCheckXY.Enabled = true;
                    }
                    else
                    {
                        nudCheckPosX.Enabled = false;
                        nudCheckPosY.Enabled = false;
                        nudCheckPosZ.Enabled = false;
                        btnGetCheckXY.Enabled = false;
                    }
                    lblResolution1.Text = "上相机分辨率:" + AssembleSuction2.GetUpResulotion().ToString("0.0000") + " 角度:" + AssembleSuction2.GetUpCameraAngle().ToString("0.00");
                    lblResolution2.Text = "下相机分辨率:" + AssembleSuction2.GetDownResulotion().ToString("0.0000") + " 角度:" + AssembleSuction2.GetDownCameraAngle().ToString("0.00");
                    
                }
                else
                {
                    iStation = 1;
                    if (camUp != CommonSet.camUpA1)
                    {
                        camUp = CommonSet.camUpA1;
                        camDown = CommonSet.camDownA1;
                    }
                 
                    if (!txtSuctionName.Focused)
                    {
                        txtSuctionName.Text = CommonSet.dic_OptSuction1[iSuctionNum].Name;
                    }
                    
                    groupBox5.Text = "下相机(组装1)";
                    groupBox4.Text = "上相机(组装1)";
                    groupBox7.Text = "组装飞拍(组装1)";
                    groupBox8.Text = "组装取料(中转1)";
                    groupBox3.Text = "组装求心(中转1)";
                    groupBox9.Text = "操作(组装1)";
                    groupBox11.Text = "抛料(组装1)";
                    groupBox12.Text = "吸笔平面校正位(组装1)";
                    groupBox13.Text = "吸笔中心(吸笔" + iSuctionNum.ToString() + ")";

                    groupBox14.Text = "上相机标定(组装1)";
                    groupBox15.Text = "下相机标定(组装1)";

                    lblAxisX.Text = "组装X1轴";
                    lblAxisZ.Text = "组装Z1轴";
                    label39.Text = "组装Y1轴";

                    lblPosX.Text = mc.dic_Axis[AXIS.组装X1轴].dPos.ToString("0.000");
                    lblPosZ.Text = mc.dic_Axis[AXIS.组装Z1轴].dPos.ToString("0.000");
                    nudCamDownExposure.Value = (decimal)AssembleSuction1.dExposureTimeDown;
                    nudCamUpExposure.Value = (decimal)AssembleSuction1.dExposureTimeUp;
                    nudGainCamDown.Value = (decimal)AssembleSuction1.dGainDown;
                    nudGainCamUp.Value = (decimal)AssembleSuction1.dGainUp;
                    if (cbContinusCamDown.Checked && (Run.runMode == RunMode.手动))
                    {
                        if (CommonSet.camDownC1 != null)
                        {
                            CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                            CommonSet.camDownC1.SetGain(AssembleSuction1.dGainDown);
                            bAutoDown = !bAutoDown;
                            mc.setDO(DO.组装下相机1, bAutoDown);
                            swSaveData.Start();
                        }
                    }
                    if (cbContinousCamUp.Checked && (Run.runMode == RunMode.手动))
                    {
                       
                        if (CommonSet.camUpC1 != null)
                        {
                            CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                            CommonSet.camUpC1.SetGain(AssembleSuction1.dGainUp);
                            bAutoUp = !bAutoUp;
                            mc.setDO(DO.组装上相机1, bAutoUp);
                            
                          
                        }
                    }
                    if (iStationFlag == 2)
                    {
                        this.dataGridView1.AutoGenerateColumns = false;
                        this.dataGridView1.DataSource = CommonSet.dic_Assemble1.Values.ToList();

                    }
                    for (int i = 0; i < 9; i++)
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();

                    }
                    iStationFlag = 1;
                    lblShowStation.Text = "工位1吸笔参数：";
                    label34.Text = "吸笔1抛料位1";
                    label37.Text = "吸笔1抛料位2";

                    //if (swSaveData.ElapsedMilliseconds > 1000)
                    //{
                    //    swSaveData.Stop();
                    //    swSaveData.Reset();

                    //    //CheckDownC1(CommonSet.dic_Assemble1[iSuctionNum].strPicDownProduct);
                    //    CheckUpC1(AssembleSuction1.strPicCameraUpCheckName);

                    //}
                    if (iSuctionNum == 1)
                    {
                        nudCheckPosX.Enabled = true;
                        nudCheckPosY.Enabled = true;
                        nudCheckPosZ.Enabled = true;
                        btnGetCheckXY.Enabled = true;
                    }
                    else
                    {
                        nudCheckPosX.Enabled = false;
                        nudCheckPosY.Enabled = false;
                        nudCheckPosZ.Enabled = false;
                        btnGetCheckXY.Enabled = false;
                    }
                    //btnTrigger.Enabled = CommonSet.camDown1.bTrigger;
                    //cbContinous.Checked = !CommonSet.camDown1.bTrigger;
                    lblResolution1.Text = "上相机分辨率:" + AssembleSuction1.GetUpResulotion().ToString("0.0000") + " 角度:" + AssembleSuction1.GetUpCameraAngle().ToString("0.00");
                    lblResolution2.Text = "下相机分辨率:" + AssembleSuction1.GetDownResulotion().ToString("0.0000") + " 角度:" + AssembleSuction1.GetDownCameraAngle().ToString("0.00");
                    
                
                }
               

                lblSuction.Text = "组装气缸下降" + iSuctionNum.ToString();
                lblGet.Text = "组装吸真空" + iSuctionNum.ToString();
                lblPut.Text = "组装破真空" + iSuctionNum.ToString();
                label41.Text = "吸笔"+iSuctionNum.ToString()+"校正位";

                string strDo = "组装气缸下降" + iSuctionNum.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);

                lblSuction.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "组装吸真空" + iSuctionNum.ToString();
                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblGet.BackColor = getColor(mc.dic_DO[dOut]);

                strDo = "组装破真空" + iSuctionNum.ToString();
                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                lblPut.BackColor = getColor(mc.dic_DO[dOut]);

                lblPosY1.Text = mc.dic_Axis[AXIS.组装Y1轴].dPos.ToString("0.000");
                lblPosY2.Text = mc.dic_Axis[AXIS.组装Y2轴].dPos.ToString("0.000");
                groupBox1.Text = "下相机拍照(吸笔" + iSuctionNum.ToString() + ")";
                groupBox6.Text = "组装参数";
                panelProduct.Visible = rbtProductA.Checked;
                if(bInit)
                 UpdateControl();

               
               
            }
            catch (Exception ex )
            {
                CommonSet.WriteInfo("组装界面更新异常:" + ex.ToString());
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
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                setNumerialControl(nudCamUpExposure, AssembleSuction1.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, AssembleSuction1.dGainUp);
                setNumerialControl(nudCamUpLight, AssembleSuction1.dLightUp);

                setNumerialControl(nudCamDownExposure, AssembleSuction1.dExposureTimeDown);
                setNumerialControl(nudGainCamDown, AssembleSuction1.dGainDown);
                setNumerialControl(nudCamDownLight, AssembleSuction1.dLightDown);

                setNumerialControl(nudCamDownPos, optSuction.DPosCameraDownX);
                //setNumerialControl(nudPutTime, optSuction.dPutDelay);
                //setNumerialControl(nudAssemDist1, optSuction.dAssemblePosZ1);
                //setNumerialControl(nudAssemDist2, optSuction.dAssemblePosZ2);
                setNumerialControl(nudAssemDist1, CommonSet.dic_Assemble1[iSuctionNum].dPreH1);
                setNumerialControl(nudAssemDist2,  CommonSet.dic_Assemble1[iSuctionNum].dPreH2);
                setNumerialControl(nudAssemZ1, CommonSet.dic_Assemble1[iSuctionNum].dAssembleH);
                setNumerialControl(nudAssemZ2, CommonSet.dic_Assemble1[iSuctionNum].dAssembleH2);

                setNumerialControl(nudFlashEndX, AssembleSuction1.dFlashEndX);
                setNumerialControl(nudGetTime, AssembleSuction1.DGetTime);

                setNumerialControl(nudCamUpPos1X, AssembleSuction1.pCamBarrelPos1.X);
                setNumerialControl(nudCamUpPos1Y, AssembleSuction1.pCamBarrelPos1.Y);
                setNumerialControl(nudCamUpPos2X, AssembleSuction1.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPos2Y, AssembleSuction1.pCamBarrelPos2.Y);

                setNumerialControl(nudGetX, AssembleSuction1.dFlashStartX);
                setNumerialControl(nudGetZ, AssembleSuction1.DGetPosZ);

                setNumerialControl(nudCorrentTime, AssembleSuction1.DCorrectTime);
                setNumerialControl(nudCorrectZ, AssembleSuction1.DCorrectPosZ);
                //setNumerialControl(nudMakeUpX, optSuction.dMakeUpX);
                //setNumerialControl(nudMakeUpY, optSuction.dMakeUpY);
                setNumerialControl(nudDiscardX1, AssembleSuction1.pDiscardC1.X);
                setNumerialControl(nudDiscardY1, AssembleSuction1.pDiscardC1.Y);
                setNumerialControl(nudDiscardZ1, AssembleSuction1.pDiscardC1.Z);

                setNumerialControl(nudDiscardX2, AssembleSuction1.pDiscardC2.X);
                setNumerialControl(nudDiscardY2, AssembleSuction1.pDiscardC2.Y);
                setNumerialControl(nudDiscardZ2, AssembleSuction1.pDiscardC2.Z);
                setNumerialControl(nudCheckPosX,AssembleSuction1.pCheckSuction.X -25*(iSuctionNum-1));
                setNumerialControl(nudCheckPosY, AssembleSuction1.pCheckSuction.Y);
                setNumerialControl(nudCheckPosZ, AssembleSuction1.pCheckSuction.Z);
                setNumerialControl(nudGainCalib, AssembleSuction1.dResultTestGain);
                setNumerialControl(nudCheckHeight, AssembleSuction1.dResultPutZ);

                setNumerialControl(nudCamUpPosX1, AssembleSuction1.lstCalibCameraUpXY[0].X);
                setNumerialControl(nudCamUpPosR1, AssembleSuction1.lstCalibCameraUpRC[0].X);
                setNumerialControl(nudCamUpPosC1, AssembleSuction1.lstCalibCameraUpRC[0].Y);

                setNumerialControl(nudCamUpPosX2, AssembleSuction1.lstCalibCameraUpXY[1].X);
                setNumerialControl(nudCamUpPosR2, AssembleSuction1.lstCalibCameraUpRC[1].X);
                setNumerialControl(nudCamUpPosC2, AssembleSuction1.lstCalibCameraUpRC[1].Y);

                setNumerialControl(nudCamDownPosX1, AssembleSuction1.lstCalibCameraDownXY[0].X);
                setNumerialControl(nudCamDownPosR1, AssembleSuction1.lstCalibCameraDownRC[0].X);
                setNumerialControl(nudCamDownPosC1, AssembleSuction1.lstCalibCameraDownRC[0].Y);

                setNumerialControl(nudCamDownPosX2, AssembleSuction1.lstCalibCameraDownXY[1].X);
                setNumerialControl(nudCamDownPosR2, AssembleSuction1.lstCalibCameraDownRC[1].X);
                setNumerialControl(nudCamDownPosC2, AssembleSuction1.lstCalibCameraDownRC[1].Y);
                

                setNumerialControl(nudSuctionRow, optSuction.pSuctionCenter.X);
                setNumerialControl(nudSuctionCol, optSuction.pSuctionCenter.Y);



            }
            else
            {
                var optSuction = CommonSet.dic_Assemble2[iSuctionNum];


                setNumerialControl(nudCamUpExposure, AssembleSuction2.dExposureTimeUp);
                setNumerialControl(nudGainCamUp, AssembleSuction2.dGainUp);
                setNumerialControl(nudCamUpLight, AssembleSuction2.dLightUp);

                setNumerialControl(nudCamDownExposure, AssembleSuction2.dExposureTimeDown);
                setNumerialControl(nudGainCamDown, AssembleSuction2.dGainDown);
                setNumerialControl(nudCamDownLight, AssembleSuction2.dLightDown);

                setNumerialControl(nudCamDownPos, optSuction.DPosCameraDownX);
                //setNumerialControl(nudPutTime, optSuction.dPutDelay);
                //setNumerialControl(nudAssemDist1, CommonSet.dic_Assemble1[iSuctionNum].dDist2);
                //setNumerialControl(nudAssemDist2, CommonSet.dic_Assemble2[iSuctionNum].dDist2);
                setNumerialControl(nudAssemDist1,  CommonSet.dic_Assemble2[iSuctionNum].dPreH1);
                setNumerialControl(nudAssemDist2,  CommonSet.dic_Assemble2[iSuctionNum].dPreH2);
                setNumerialControl(nudAssemZ1, CommonSet.dic_Assemble2[iSuctionNum].dAssembleH);
                setNumerialControl(nudAssemZ2, CommonSet.dic_Assemble2[iSuctionNum].dAssembleH2);

                setNumerialControl(nudFlashEndX, AssembleSuction2.dFlashEndX);
                setNumerialControl(nudGetTime, AssembleSuction2.DGetTime);
                setNumerialControl(nudCamUpPos1X, AssembleSuction2.pCamBarrelPos1.X);
                setNumerialControl(nudCamUpPos1Y, AssembleSuction2.pCamBarrelPos1.Y);
                setNumerialControl(nudCamUpPos2X, AssembleSuction2.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPos2Y, AssembleSuction2.pCamBarrelPos2.Y);

                setNumerialControl(nudGetX, AssembleSuction2.dFlashStartX);
                setNumerialControl(nudGetZ, AssembleSuction2.DGetPosZ);

                setNumerialControl(nudCorrentTime, AssembleSuction2.DCorrectTime);
                setNumerialControl(nudCorrectZ, AssembleSuction2.DCorrectPosZ);
                //setNumerialControl(nudMakeUpX, optSuction.dMakeUpX);
                //setNumerialControl(nudMakeUpY, optSuction.dMakeUpY);
                setNumerialControl(nudDiscardX1, AssembleSuction2.pDiscardC1.X);
                setNumerialControl(nudDiscardY1, AssembleSuction2.pDiscardC1.Y);
                setNumerialControl(nudDiscardZ1, AssembleSuction2.pDiscardC1.Z);

                setNumerialControl(nudDiscardX2, AssembleSuction2.pDiscardC2.X);
                setNumerialControl(nudDiscardY2, AssembleSuction2.pDiscardC2.Y);
                setNumerialControl(nudDiscardZ2, AssembleSuction2.pDiscardC2.Z);

                setNumerialControl(nudCheckPosX, AssembleSuction2.pCheckSuction.X - 25 * (iSuctionNum - 10));
                setNumerialControl(nudCheckPosY, AssembleSuction2.pCheckSuction.Y);
                setNumerialControl(nudCheckPosZ, AssembleSuction2.pCheckSuction.Z);
                setNumerialControl(nudGainCalib, AssembleSuction2.dResultTestGain);
                setNumerialControl(nudCheckHeight, AssembleSuction2.dResultPutZ);

                setNumerialControl(nudCamUpPosX1, AssembleSuction2.lstCalibCameraUpXY[0].X);
                setNumerialControl(nudCamUpPosR1, AssembleSuction2.lstCalibCameraUpRC[0].X);
                setNumerialControl(nudCamUpPosC1, AssembleSuction2.lstCalibCameraUpRC[0].Y);

                setNumerialControl(nudCamUpPosX2, AssembleSuction2.lstCalibCameraUpXY[1].X);
                setNumerialControl(nudCamUpPosR2, AssembleSuction2.lstCalibCameraUpRC[1].X);
                setNumerialControl(nudCamUpPosC2, AssembleSuction2.lstCalibCameraUpRC[1].Y);

                setNumerialControl(nudCamDownPosX1, AssembleSuction2.lstCalibCameraDownXY[0].X);
                setNumerialControl(nudCamDownPosR1, AssembleSuction2.lstCalibCameraDownRC[0].X);
                setNumerialControl(nudCamDownPosC1, AssembleSuction2.lstCalibCameraDownRC[0].Y);

                setNumerialControl(nudCamDownPosX2, AssembleSuction2.lstCalibCameraDownXY[1].X);
                setNumerialControl(nudCamDownPosR2, AssembleSuction2.lstCalibCameraDownRC[1].X);
                setNumerialControl(nudCamDownPosC2, AssembleSuction2.lstCalibCameraDownRC[1].Y);

                setNumerialControl(nudSuctionRow, optSuction.pSuctionCenter.X);
                setNumerialControl(nudSuctionCol, optSuction.pSuctionCenter.Y);
                


            }
        }
        //保存界面的参数
        public void SaveUI()
        {

            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];
            

                AssembleSuction1.dExposureTimeUp = (double)nudCamUpExposure.Value;
                AssembleSuction1.dGainUp = (double)nudGainCamUp.Value;
                AssembleSuction1.dLightUp = (double)nudCamUpLight.Value;

                AssembleSuction1.dExposureTimeDown = (double)nudCamDownExposure.Value;
                AssembleSuction1.dGainDown = (double)nudGainCamDown.Value;
                AssembleSuction1.dLightDown = (double)nudCamDownLight.Value;

                optSuction.DPosCameraDownX = (double)nudCamDownPos.Value;

                optSuction.dPreH1 = (double)nudAssemDist1.Value;
                optSuction.dPreH2 = (double)nudAssemDist2.Value;
                optSuction.dAssembleH = (double)nudAssemZ1.Value;
                optSuction.dAssembleH2 = (double)nudAssemZ2.Value;
                //CommonSet.dic_Assemble1[iSuctionNum].dDist2 = (double)nudAssemDist1.Value;
                //CommonSet.dic_Assemble2[iSuctionNum].dDist2 = (double)nudAssemDist2.Value;
                //optSuction.dPutDelay = (double)nudPutTime.Value;
                //optSuction.dAssemblePosZ1 = (double)nudAssemDist1.Value;
                //optSuction.dAssemblePosZ2 = (double)nudAssemDist2.Value;
                //optSuction.dMakeUpX = (double)nudMakeUpX.Value;
                //optSuction.dMakeUpY = (double)nudMakeUpY.Value;
                
                AssembleSuction1.dFlashEndX = (double)nudFlashEndX.Value;
                AssembleSuction1.DGetTime = (double)nudGetTime.Value;
                AssembleSuction1.pCamBarrelPos1.X = (double)nudCamUpPos1X.Value;
                AssembleSuction1.pCamBarrelPos1.Y = (double)nudCamUpPos1Y.Value;
                AssembleSuction1.pCamBarrelPos2.X = (double)nudCamUpPos2X.Value;
                AssembleSuction1.pCamBarrelPos2.Y = (double)nudCamUpPos2Y.Value;

                AssembleSuction1.dFlashStartX = (double)nudGetX.Value;
                AssembleSuction1.DGetPosZ = (double)nudGetZ.Value;

                AssembleSuction1.DCorrectTime = (double)nudCorrentTime.Value;
                AssembleSuction1.DCorrectPosZ = (double)nudCorrectZ.Value;

                AssembleSuction1.pDiscardC1.X = (double)nudDiscardX1.Value;
                AssembleSuction1.pDiscardC1.Y = (double)nudDiscardY1.Value;
                AssembleSuction1.pDiscardC1.Z = (double)nudDiscardZ1.Value;

                AssembleSuction1.pDiscardC2.X = (double)nudDiscardX2.Value;
                AssembleSuction1.pDiscardC2.Y = (double)nudDiscardY2.Value;
                AssembleSuction1.pDiscardC2.Z = (double)nudDiscardZ2.Value;
                AssembleSuction1.dResultTestGain = (double)nudGainCalib.Value;
                AssembleSuction1.dResultPutZ = (double)nudCheckHeight.Value;

                AssembleSuction1.lstCalibCameraUpXY[0].X = (double)nudCamUpPosX1.Value;
                AssembleSuction1.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
                AssembleSuction1.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

                AssembleSuction1.lstCalibCameraUpXY[1].X = (double)nudCamUpPosX2.Value;
                AssembleSuction1.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
                AssembleSuction1.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

                AssembleSuction1.lstCalibCameraDownXY[0].X = (double)nudCamDownPosX1.Value;
                AssembleSuction1.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
                AssembleSuction1.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

                AssembleSuction1.lstCalibCameraDownXY[1].X = (double)nudCamDownPosX2.Value;
                AssembleSuction1.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
                AssembleSuction1.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;

                if (iSuctionNum == 1)
                {
                    AssembleSuction1.pCheckSuction.X = (double)nudCheckPosX.Value;
                    AssembleSuction1.pCheckSuction.Y = (double)nudCheckPosY.Value;
                    AssembleSuction1.pCheckSuction.Y = (double)nudCheckPosY.Value;
                }
                optSuction.pSuctionCenter.X = (double)nudSuctionRow.Value;
                optSuction.pSuctionCenter.Y = (double)nudSuctionCol.Value;


                CommonSet.dic_Assemble1[iSuctionNum] = optSuction;

               
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = CommonSet.dic_Assemble1.Values.ToList();

               
            }
            else
            {
                var optSuction = CommonSet.dic_Assemble2[iSuctionNum];


                AssembleSuction2.dExposureTimeUp = (double)nudCamUpExposure.Value;
                AssembleSuction2.dGainUp = (double)nudGainCamUp.Value;
                AssembleSuction2.dLightUp = (double)nudCamUpLight.Value;

                AssembleSuction2.dExposureTimeDown = (double)nudCamDownExposure.Value;
                AssembleSuction2.dGainDown = (double)nudGainCamDown.Value;
                AssembleSuction2.dLightDown = (double)nudCamDownLight.Value;

                optSuction.DPosCameraDownX = (double)nudCamDownPos.Value;
                optSuction.dPreH1 = (double)nudAssemDist1.Value;
                optSuction.dPreH2 = (double)nudAssemDist2.Value;
                optSuction.dAssembleH = (double)nudAssemZ1.Value;
                optSuction.dAssembleH2 = (double)nudAssemZ2.Value;
                //optSuction.dPutDelay = (double)nudPutTime.Value;
                //optSuction.dAssemblePosZ1 = (double)nudAssemDist1.Value;
                //optSuction.dAssemblePosZ2 = (double)nudAssemDist2.Value;
                //optSuction.dMakeUpX = (double)nudMakeUpX.Value;
                //optSuction.dMakeUpY = (double)nudMakeUpY.Value;
                //CommonSet.dic_Assemble1[iSuctionNum].dDist2 = (double)nudAssemDist1.Value;
                //CommonSet.dic_Assemble2[iSuctionNum].dDist2 = (double)nudAssemDist2.Value;
                AssembleSuction2.dFlashEndX = (double)nudFlashEndX.Value;
                AssembleSuction2.DGetTime = (double)nudGetTime.Value;
                AssembleSuction2.pCamBarrelPos1.X = (double)nudCamUpPos1X.Value;
                AssembleSuction2.pCamBarrelPos1.Y = (double)nudCamUpPos1Y.Value;
                AssembleSuction2.pCamBarrelPos2.X = (double)nudCamUpPos2X.Value;
                AssembleSuction2.pCamBarrelPos2.Y = (double)nudCamUpPos2Y.Value;

                AssembleSuction2.dFlashStartX = (double)nudGetX.Value;
                AssembleSuction2.DGetPosZ = (double)nudGetZ.Value;

                AssembleSuction2.DCorrectTime = (double)nudCorrentTime.Value;
                AssembleSuction2.DCorrectPosZ = (double)nudCorrectZ.Value;

                AssembleSuction2.pDiscardC1.X = (double)nudDiscardX1.Value;
                AssembleSuction2.pDiscardC1.Y = (double)nudDiscardY1.Value;
                AssembleSuction2.pDiscardC1.Z = (double)nudDiscardZ1.Value;

                AssembleSuction2.pDiscardC2.X = (double)nudDiscardX2.Value;
                AssembleSuction2.pDiscardC2.Y = (double)nudDiscardY2.Value;
                AssembleSuction2.pDiscardC2.Z = (double)nudDiscardZ2.Value;
                AssembleSuction2.dResultTestGain = (double)nudGainCalib.Value;
                AssembleSuction2.dResultPutZ = (double)nudCheckHeight.Value;

                AssembleSuction2.lstCalibCameraUpXY[0].X = (double)nudCamUpPosX1.Value;
                AssembleSuction2.lstCalibCameraUpRC[0].X = (double)nudCamUpPosR1.Value;
                AssembleSuction2.lstCalibCameraUpRC[0].Y = (double)nudCamUpPosC1.Value;

                AssembleSuction2.lstCalibCameraUpXY[1].X = (double)nudCamUpPosX2.Value;
                AssembleSuction2.lstCalibCameraUpRC[1].X = (double)nudCamUpPosR2.Value;
                AssembleSuction2.lstCalibCameraUpRC[1].Y = (double)nudCamUpPosC2.Value;

                AssembleSuction2.lstCalibCameraDownXY[0].X = (double)nudCamDownPosX1.Value;
                AssembleSuction2.lstCalibCameraDownRC[0].X = (double)nudCamDownPosR1.Value;
                AssembleSuction2.lstCalibCameraDownRC[0].Y = (double)nudCamDownPosC1.Value;

                AssembleSuction2.lstCalibCameraDownXY[1].X = (double)nudCamDownPosX2.Value;
                AssembleSuction2.lstCalibCameraDownRC[1].X = (double)nudCamDownPosR2.Value;
                AssembleSuction2.lstCalibCameraDownRC[1].Y = (double)nudCamDownPosC2.Value;

                if (iSuctionNum == 10)
                {
                    AssembleSuction2.pCheckSuction.X = (double)nudCheckPosX.Value;
                    AssembleSuction2.pCheckSuction.Y = (double)nudCheckPosY.Value;
                    AssembleSuction2.pCheckSuction.Y = (double)nudCheckPosY.Value;
                }
                optSuction.pSuctionCenter.X = (double)nudSuctionRow.Value;
                optSuction.pSuctionCenter.Y = (double)nudSuctionCol.Value;

                CommonSet.dic_Assemble2[iSuctionNum] = optSuction;

                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = CommonSet.dic_Assemble2.Values.ToList();
            }

        }

        private void btnCamUpCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckUpC1(AssembleSuction1.strPicCameraUpCheckName);
                }
                else
                {
                    CheckUpC2(AssembleSuction2.strPicCameraUpCheckName);
                }
            }
            catch (Exception)
            {


            }
        }

        private void CheckUpC1(string strProcessName)
        {
            //AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = AssembleSuction1.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            //string strProcessName = AssembleSuction1.strPicCameraUpCheckName;
            AssembleSuction1.InitImageUp(strProcessName);

            if (AssembleSuction1.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            AssembleSuction1.pfUp.SetRun(true);
            AssembleSuction1.pfUp.hwin = hWindowControl1.HalconWindow;
            AssembleSuction1.actionUp(hImage);
            AssembleSuction1.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (AssembleSuction1.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");
            double dDiffRow = AssembleSuction1.imgResultUp.CenterRow - 1024;
            double dDiffCol = AssembleSuction1.imgResultUp.CenterColumn - 1224;
            string strDiffX = "中心差X:" + (dDiffRow * AssembleSuction1.GetUpResulotion()).ToString("0.000");
            string strDiffY = "中心差Y:" + (dDiffCol * AssembleSuction1.GetUpResulotion()).ToString("0.000");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDiffX, "window", 80, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDiffY, "window", 100, 0, "black", "true");
            //IniOperate.INIWriteValue("D:\\TestCenterDataUpC1.ini", "Data", DateTime.Now.ToString("MM-dd HH:mm:ss"), strDispRow + "," + strDispCol);
            WriteFile("D:\\中心检测数据\\工位1\\", "1," + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000") + ",");
            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckUpC2(string strProcessName)
        {
            AssembleSuction2 suction = CommonSet.dic_Assemble2[iSuctionNum];
            HObject hImage = AssembleSuction2.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            //string strProcessName = AssembleSuction2.strPicCameraUpCheckName;
            AssembleSuction2.InitImageUp(strProcessName);

            if (AssembleSuction2.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            AssembleSuction2.pfUp.SetRun(true);
            AssembleSuction2.pfUp.hwin = hWindowControl1.HalconWindow;
            AssembleSuction2.actionUp(hImage);
            AssembleSuction2.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");

          

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
            if (AssembleSuction2.imgResultUp.bImageResult)
                CommonSet.disp_message(hWindowControl1.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl1.HalconWindow, "NG", "window", 60, 0, "red", "true");
            double dDiffRow = AssembleSuction2.imgResultUp.CenterRow - 1024;
            double dDiffCol = AssembleSuction2.imgResultUp.CenterColumn - 1224;
            string strDiffX = "中心差X:" + (dDiffRow * AssembleSuction2.GetUpResulotion()).ToString("0.000");
            string strDiffY = "中心差Y:" + (dDiffCol * AssembleSuction2.GetUpResulotion()).ToString("0.000");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDiffX, "window", 80, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDiffY, "window", 100, 0, "black", "true");
            WriteFile("D:\\中心检测数据\\工位2\\", "2," + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000") + ",");
            //IniOperate.INIWriteValue("D:\\TestCenterDataUpC2.ini", "Data", DateTime.Now.ToString("MM-dd HH:mm:ss"), strDispRow + "," + strDispCol);
            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckDownC1(string strProcName)
        {
            AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = CommonSet.dic_Assemble1[1].hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
           // string strProcessName = suction.strPicDownProduct;
            suction.InitImageDown(strProcName);

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
            CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow = suction.imgResultDown.CenterRow;
            CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn = suction.imgResultDown.CenterColumn;

            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, suction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "black", "true");
            if (suction.imgResultDown.bImageResult)
                CommonSet.disp_message(hWindowControl2.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl2.HalconWindow, "NG", "window", 60, 0, "red", "true");

            //double dDiffRow = suction.imgResultDown.CenterRow - 1024;
            //double dDiffCol = suction.imgResultDown.CenterColumn - 1224;
            double dDiffRow = suction.imgResultDown.CenterRow - suction.pSuctionCenter.X;
            double dDiffCol = suction.imgResultDown.CenterColumn - suction.pSuctionCenter.Y;
            string strDiffX = "中心差X:"+(dDiffRow*AssembleSuction1.GetDownResulotion()).ToString("0.000");
            string strDiffY = "中心差Y:" + (dDiffCol * AssembleSuction1.GetDownResulotion()).ToString("0.000");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDiffX, "window", 80, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDiffY, "window", 100, 0, "black", "true");
           
             //IniOperate.INIWriteValue("D:\\TestCenterDataC1.ini","Data",DateTime.Now.ToString("MM-dd HH:mm:ss"),strDispRow + ","+strDispCol);
            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl2.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void CheckDownC2(string strProcName)
        {
            AssembleSuction2 suction = CommonSet.dic_Assemble2[iSuctionNum];
            HObject hImage = CommonSet.dic_Assemble2[10].hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
           // string strProcessName = suction.strPicDownProduct;
            suction.InitImageDown(strProcName);

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
            CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterRow = suction.imgResultDown.CenterRow;
            CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterColumn = suction.imgResultDown.CenterColumn;

            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, suction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "black", "true");
            if (suction.imgResultDown.bImageResult)
                CommonSet.disp_message(hWindowControl2.HalconWindow, "OK", "window", 60, 0, "green", "true");
            else
                CommonSet.disp_message(hWindowControl2.HalconWindow, "NG", "window", 60, 0, "red", "true");

            //double dDiffRow = suction.imgResultDown.CenterRow - 1024;
            //double dDiffCol = suction.imgResultDown.CenterColumn - 1224;
            double dDiffRow = suction.imgResultDown.CenterRow - suction.pSuctionCenter.X;
            double dDiffCol = suction.imgResultDown.CenterColumn - suction.pSuctionCenter.Y;
            string strDiffX = "中心差X:" + (dDiffRow * AssembleSuction2.GetDownResulotion()).ToString("0.000");
            string strDiffY = "中心差Y:" + (dDiffCol * AssembleSuction2.GetDownResulotion()).ToString("0.000");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDiffX, "window", 80, 0, "black", "true");
            CommonSet.disp_message(hWindowControl2.HalconWindow, strDiffY, "window", 100, 0, "black", "true");

            //IniOperate.INIWriteValue("D:\\TestCenterDataC2.ini", "Data", DateTime.Now.ToString("MM-dd HH:mm:ss"), strDispRow + "," + strDispCol);
            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl2.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }

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
                    {
                        iStation = 2;
                        axisX = AXIS.组装X2轴;
                        axisZ = AXIS.组装Z2轴;

                    }
                    else
                    {
                        iStation = 1;
                        axisX = AXIS.组装X1轴;
                        axisZ = AXIS.组装Z1轴;
                    }
                    bInit = false;
                    UpdateControl();
                    bInit = true;
                }
            }

        }

        private void nudGetTime_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void btnTriggerCamUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dGainUp);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dGainUp);
                        mc.setDO(DO.组装上相机2, true);
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
                    if (CommonSet.camUpC1 != null)
                    {
                       
                        mc.setDO(DO.组装上相机1, false);
                    }
                }
                else
                {
                    if (CommonSet.camUpC2 != null)
                    {
                      
                        mc.setDO(DO.组装上相机2, false);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnGetAssem1Z_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudAssemDist1.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoAssem1Z_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.dAssemblePosZ1;
                mc.AbsMove(axisZ, z, iVelRunZ);
            }
            else
            {
                var optSuction = CommonSet.dic_Assemble2[iSuctionNum];
                double z = (optSuction.dAssemblePosZ1);
                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetAssem2Z_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudAssemDist2.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoAssem2Z_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.dAssemblePosZ2;
                mc.AbsMove(axisZ, z, iVelRunZ);
            }
            else
            {
                var optSuction = CommonSet.dic_Assemble2[iSuctionNum];
                double z = (optSuction.dAssemblePosZ2);
                mc.AbsMove(axisZ, z, iVelRunZ);

            }
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
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                if (OptSution1.pSafeXYZ.X + 100 < mc.dic_Axis[AXIS.取料X1轴].dPos)
                {
                    MessageBox.Show("取料X1超过安全位");
                    return;
                }
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.DPosCameraDownX;
                mc.AbsMove(axisX, z, iVelRunX);
            }
            else
            {
                if (OptSution2.pSafeXYZ.X + 100 < mc.dic_Axis[AXIS.取料X2轴].dPos)
                {
                    MessageBox.Show("取料X2轴超过安全位");
                    return;
                }
              
                var optSuction = CommonSet.dic_Assemble2[iSuctionNum];
                double z = (optSuction.DPosCameraDownX);
                mc.AbsMove(axisX, z, iVelRunX);

            }
        }

        private void btnCamUpCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = AssembleSuction1.strPicCameraUpCheckName;
                hoImage = AssembleSuction1.hImageUP;
                if (strName.Equals(""))
                {
                    strName = "AssemUp" + iStation.ToString();
                    AssembleSuction1.strPicCameraUpCheckName = strName;
                }
            }
            else
            {
                strName = AssembleSuction2.strPicCameraUpCheckName;
                hoImage = AssembleSuction2.hImageUP;
                if (strName.Equals(""))
                {
                    strName = "AssemUp" + iStation.ToString();
                    AssembleSuction2.strPicCameraUpCheckName = strName;
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
                if (AssembleSuction1.hImageUP == null)
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
                    HOperatorSet.WriteImage(AssembleSuction1.hImageUP, "bmp", 0, fileName);
                }
            }
            else
            {
                if (AssembleSuction2.hImageUP == null)
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
                    HOperatorSet.WriteImage(AssembleSuction2.hImageUP, "bmp", 0, fileName);
                }

            }
        }

        private void btnTriggerCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camDownC1 != null)
                    {
                        CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                        CommonSet.camDownC1.SetGain(AssembleSuction1.dGainDown);
                        mc.setDO(DO.组装下相机1, true);
                    }
                }
                else
                {
                    if (CommonSet.camDownC2 != null)
                    {
                        CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dGainDown);
                        mc.setDO(DO.组装下相机2, true);
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
                    if (CommonSet.camDownC1 != null)
                    {
                     
                        mc.setDO(DO.组装下相机1, false);
                    }
                }
                else
                {
                    if (CommonSet.camDownC2 != null)
                    {

                        mc.setDO(DO.组装下相机2, false);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCamDownCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckDownC1(CommonSet.dic_Assemble1[iSuctionNum].strPicDownProduct);
                }
                else
                {
                    CheckDownC2(CommonSet.dic_Assemble2[iSuctionNum].strPicDownProduct);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCamDownCheckSet_Click(object sender, EventArgs e)
        {

            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = CommonSet.dic_Assemble1[iSuctionNum].strPicDownProduct;
                hoImage = CommonSet.dic_Assemble1[1].hImageDown;
            }
            else
            {
                strName = CommonSet.dic_Assemble2[iSuctionNum].strPicDownProduct;
                hoImage = CommonSet.dic_Assemble2[10].hImageDown;
            }
            if (strName.Equals(""))
            {
                strName = "AssemDown" + iSuctionNum.ToString();
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamDownImg_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                if (CommonSet.dic_Assemble1[iSuctionNum].hImageDown == null)
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
                    HOperatorSet.WriteImage(CommonSet.dic_Assemble1[iSuctionNum].hImageDown, "bmp", 0, fileName);
                }
            }
            else
            {
                if (CommonSet.dic_Assemble2[iSuctionNum].hImageDown == null)
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
                    HOperatorSet.WriteImage(CommonSet.dic_Assemble2[iSuctionNum].hImageDown, "bmp", 0, fileName);
                }

            }
        }

       

        private void btnGetCamUpPos1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
           
            nudCamUpPos1X.Value = (decimal)mc.dic_Axis[axisX].dPos;
             nudCamUpPos1Y.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;

           
        }

        private void btnGoCamUpPos1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
               // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pCamBarrelPos1.X);
                double y = (AssembleSuction1.pCamBarrelPos1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
            }
            else
            {
                double x = (AssembleSuction2.pCamBarrelPos1.X);
                double y = (AssembleSuction2.pCamBarrelPos1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
             
            }
        }

        private void btnGetCamUpPos2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudCamUpPos2X.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudCamUpPos2Y.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
        }

        private void btnGoCamUpPos2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pCamBarrelPos2.X);
                double y = (AssembleSuction1.pCamBarrelPos2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
            }
            else
            {
                double x = (AssembleSuction2.pCamBarrelPos2.X);
                double y = (AssembleSuction2.pCamBarrelPos2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);

            }
        }

        private void btnGetXZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudGetX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudGetZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoGetX_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                if (OptSution1.pSafeXYZ.X + 10 < mc.dic_Axis[AXIS.取料X1轴].dPos)
                {
                    MessageBox.Show("取料X1超过安全位");
                    return;
                }
               
                double x = (AssembleSuction1.dFlashStartX);

                mc.AbsMove(axisX, x, iVelRunX);

            }
            else
            {
                if (OptSution2.pSafeXYZ.X + 10 < mc.dic_Axis[AXIS.取料X2轴].dPos)
                {
                    MessageBox.Show("取料X2轴超过安全位");
                    return;
                }
              
                double x = (AssembleSuction2.dFlashStartX);
                mc.AbsMove(axisX, x, iVelRunX);

            }
        }

        private void btnGoGetZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {

                double z = (AssembleSuction1.DGetPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
            else
            {
                double z = (AssembleSuction2.DGetPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetCorrectZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
           
            nudCorrectZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoCorrectZ_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {

                double z = (AssembleSuction1.DCorrectPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
            else
            {
                double z = (AssembleSuction2.DCorrectPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

            }
        }

        private void btnGetFlashX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudFlashEndX.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoFlashX_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {

                double x = (AssembleSuction1.dFlashEndX);

                mc.AbsMove(axisX, x, iVelRunX);

            }
            else
            {
                double x = (AssembleSuction2.dFlashEndX);
                mc.AbsMove(axisX, x, iVelRunX);

            }
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
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

        private void groupBox_Paint(object sender, PaintEventArgs e)
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
        private void AssembleUI_Load(object sender, EventArgs e)
        {
           // mc = MotionCard.getMotionCard();
            bInit = false;
            UpdateControl();
            cbUseCalibUp.Checked = true;
            cbUseCalibDown.Checked = true;
            bInit = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridView1.DataSource = CommonSet.dic_Assemble1.Values.ToList();
        }

        private void X轴M_MouseDown(object sender, MouseEventArgs e)
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
            else if (name.Contains("Y1"))
            {
                axis = AXIS.组装Y1轴;
                vel = iVelRunY;
            }
            else if (name.Contains("Y2"))
            {
                axis = AXIS.组装Y2轴;
                vel = iVelRunY;
            }
            else
            {
                axis = axisZ;
                vel = iVelRunZ;
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

        private void X轴M_MouseUp(object sender, MouseEventArgs e)
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
            else if (name.Contains("Y1"))
            {
                axis = AXIS.组装Y1轴;
                vel = iVelRunY;
            }
            else if (name.Contains("Y2"))
            {
                axis = AXIS.组装Y2轴;
                vel = iVelRunY;
            }
            else
            {
                axis = axisZ;
                vel = iVelRunZ;
            }
            mc.StopAxis(axis);
        }

        private void X轴M_Click(object sender, EventArgs e)
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
            else if (name.Contains("Y1"))
            {
                axis = AXIS.组装Y1轴;
                vel = iVelRunY;
            }
            else if (name.Contains("Y2"))
            {
                axis = AXIS.组装Y2轴;
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

        private void lblSuction_Click(object sender, EventArgs e)
        {
            string strDo = "组装气缸下降" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblGet_Click(object sender, EventArgs e)
        {
            string strDo = "组装吸真空" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblPut_Click(object sender, EventArgs e)
        {
            string strDo = "组装破真空" + iSuctionNum.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
           // mc.setDO(dOut, !mc.dic_DO[dOut]);
           
            if (CommonSet.bUseAutoParam)
            {
                CommonSet.dOutTest = dOut;
                CommonSet.dOutTestTime = (long)(CommonSet.asm.dic_Solution[1].lstTime[iSuctionNum-1] * 1000);

                string strD = "组装吸真空" + iSuctionNum.ToString();
                DO dO1 = (DO)Enum.Parse(typeof(DO), strD);
                CommonSet.dOutIn = dO1;
            }
            else
            {
                mc.setDO(dOut, !mc.dic_DO[dOut]);
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
                if (iStation == 1)
                    HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                else
                    HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
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
                        HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                    else
                        HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
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
                        HOperatorSet.DispImage(CommonSet.dic_Assemble1[1].hImageDown, hwin);
                    else
                        HOperatorSet.DispImage(CommonSet.dic_Assemble2[10].hImageDown, hwin);
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
                    HOperatorSet.DispImage(CommonSet.dic_Assemble1[1].hImageDown, hwin);
                else
                    HOperatorSet.DispImage(CommonSet.dic_Assemble2[10].hImageDown, hwin);
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
            if (iStation == 1)
            {
                ResultTestModule.bUseOptOnly = false;
                ResultTestModule.currentPoint = CommonSet.dic_OptSuction1[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                ResultTestModule.currentPoint.Z = CommonSet.dic_OptSuction1[iSuctionNum].DGetPosZ;
                ResultTestModule.axisX = AXIS.组装X1轴;
                ResultTestModule.axisZ = AXIS.组装Z1轴;
                ResultTestModule.axisGetX = AXIS.取料X1轴;
                ResultTestModule.axisGetY = AXIS.取料Y1轴;
                ResultTestModule.axisGetZ = AXIS.取料Z1轴;
                ResultTestModule.dPutZ = (double)nudCheckHeight.Value;
                ResultTestModule.iCurrentSuctionOrder = iSuctionNum;
                ResultTestModule.iCurrentStation = (int)nudCheckStation.Value;
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
                ResultTestModule.bUseOptOnly = false;
                ResultTestModule.currentPoint = CommonSet.dic_OptSuction2[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                ResultTestModule.currentPoint.Z = CommonSet.dic_OptSuction2[iSuctionNum].DGetPosZ;
                ResultTestModule.axisX = AXIS.组装X2轴;
                ResultTestModule.axisZ = AXIS.组装Z2轴;
                ResultTestModule.axisGetX = AXIS.取料X2轴;
                ResultTestModule.axisGetY = AXIS.取料Y2轴;
                ResultTestModule.axisGetZ = AXIS.取料Z2轴;
                ResultTestModule.dPutZ = (double)nudCheckHeight.Value;
                ResultTestModule.iCurrentSuctionOrder = iSuctionNum;
                ResultTestModule.iCurrentStation = (int)nudCheckStation.Value;
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

        private void btnTriggerCamUp_Click(object sender, EventArgs e)
        {

        }

        private void btnTriggerCamDown_Click(object sender, EventArgs e)
        {

        }

        private void btnGoDiscard1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pDiscardC1.X);
                double y = (AssembleSuction1.pDiscardC1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
            }
            else
            {
                double x = (AssembleSuction2.pDiscardC1.X);
                double y = (AssembleSuction2.pDiscardC1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);

            }
        }

        private void btnGoDiscard2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pDiscardC2.X);
                double y = (AssembleSuction1.pDiscardC2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
            }
            else
            {
                double x = (AssembleSuction2.pDiscardC2.X);
                double y = (AssembleSuction2.pDiscardC2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);

            }
        }

        private void btnGetDiscard1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudDiscardX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudDiscardY1.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
            nudDiscardZ1.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            //nudDiscardZ1.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGetDiscard2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudDiscardX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudDiscardY2.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            nudDiscardZ2.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGetCheckXY_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            if (iSuctionNum == 1)
            {

                nudCheckPosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
                nudCheckPosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
                nudCheckPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            }
            else
            {
                nudCheckPosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
                nudCheckPosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
                nudCheckPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;


            }
        }

        private void btnGoCheckXY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (iStation == 1)
            {
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (double)nudCheckPosX.Value;
                double y = (double)nudCheckPosY.Value;
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
            }
            else
            {
                double x = (double)nudCheckPosX.Value;
                double y = (double)nudCheckPosY.Value;
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);

            }
        }

        private void btnGoCheckZ_Click(object sender, EventArgs e)
        {
          
                double z = (double)nudCheckPosZ.Value;
               
                mc.AbsMove(axisX, z, iVelRunZ);
               

           
        }

        private void btnGetSuctionCenter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前吸笔中心位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            try
            {
                if (iStation == 1)
                {
                    nudSuctionRow.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudSuctionCol.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
                }
                else
                {
                    nudSuctionRow.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterRow;
                    nudSuctionCol.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterColumn;
                }

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
                strName = CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName;
                hoImage = CommonSet.dic_Assemble1[1].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptSuctionC" + iSuctionNum.ToString();
                    CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName = strName;
                }
            }
            else
            {
                strName = CommonSet.dic_Assemble2[iSuctionNum].strPicDownSuctionCenterName;
                hoImage = CommonSet.dic_Assemble2[10].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptSuctionC" + iSuctionNum.ToString();
                    CommonSet.dic_Assemble2[iSuctionNum].strPicDownSuctionCenterName = strName;
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
                    CheckDownC1(CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName);
                }
                else
                {
                    CheckDownC2(CommonSet.dic_Assemble2[iSuctionNum].strPicDownSuctionCenterName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("下相机检测异常" + ex.ToString());
            }
        }

        private void btnSafeZ1_Click(object sender, EventArgs e)
        {
            double z = Assembly.AssembleSuction1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z1轴, z, iVelRunZ);
        }

        private void btnSafeZ2_Click(object sender, EventArgs e)
        {
            double z = Assembly.AssembleSuction2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z2轴, z, iVelRunZ);

        }

        private void btnTriggerUp_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dResultTestGain);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dResultTestGain);
                        mc.setDO(DO.组装上相机2, true);
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCalibSuctionCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckUpC1(AssembleSuction1.strResultSuctionName);
                }
                else
                {
                    CheckUpC2(AssembleSuction2.strResultSuctionName);
                }
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    CheckUpC1(AssembleSuction1.strResultLenName);
                }
                else
                {
                    CheckUpC2(AssembleSuction2.strResultLenName);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCamProSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = AssembleSuction1.strResultSuctionName;
                hoImage = AssembleSuction1.hImageUP;
                //if (strName.Equals(""))
                //{
                //    strName = "AssemUp" + iStation.ToString();
                //    AssembleSuction1.strResultSuctionName = strName;
                //}
            }
            else
            {
                strName = AssembleSuction2.strResultSuctionName;
                hoImage = AssembleSuction2.hImageUP;
                //if (strName.Equals(""))
                //{
                //    strName = "AssemUp" + iStation.ToString();
                //    AssembleSuction2.strResultSuctionName = strName;
                //}
            }

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnResultLenSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            if (iStation == 1)
            {
                strName = AssembleSuction1.strResultLenName;
                hoImage = AssembleSuction1.hImageUP;
                //if (strName.Equals(""))
                //{
                //    strName = "AssemUp" + iStation.ToString();
                //    AssembleSuction1.strResultLenName = strName;
                //}
            }
            else
            {
                strName = AssembleSuction2.strResultLenName;
                hoImage = AssembleSuction2.hImageUP;
                //if (strName.Equals(""))
                //{
                //    strName = "AssemUp" + iStation.ToString();
                //    AssembleSuction2.strResultLenName = strName;
                //}
            }

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnGetH_Click(object sender, EventArgs e)
        {
            nudCheckHeight.Value = (decimal)mc.dic_Axis[axisZ].dPos; 
        }

        private void btnGoH_Click(object sender, EventArgs e)
        {
            if (iStation == 1)
            {
                mc.AbsMove(axisZ, (double)nudCheckHeight.Value, 10);

            }
            else
            {
                mc.AbsMove(axisZ, (double)nudCheckHeight.Value, 10);

            }
        }

        private void btnGoDist1_Click(object sender, EventArgs e)
        {
            

                double z = (double)nudAssemDist1.Value;

                mc.AbsMove(axisZ, z, iVelRunZ);

           
        }

        private void btnGoDist2_Click(object sender, EventArgs e)
        {
            double z = (double)nudAssemDist2.Value;

            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnGoAssemZ1_Click(object sender, EventArgs e)
        {
            double z = (double)nudAssemZ1.Value;

            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnGoAssemZ2_Click(object sender, EventArgs e)
        {
            double z = (double)nudAssemZ2.Value;

            mc.AbsMove(axisZ, z, iVelRunZ);
        }

        private void btnPosSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
           
            strName = AssembleSuction2.strPicUpCameraLastCheckName;
            hoImage = AssembleSuction2.hImageUP;
            if (strName.Equals(""))
            {
                strName = "组装2上相机最后镜筒中心检测";
                AssembleSuction2.strPicCameraUpCheckName = strName;
            }
           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;


        }

        private void btnGetCamUp1_Click(object sender, EventArgs e)
        {
            try
            {
                if (iStation == 1)
                {
                    nudCamUpPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR1.Value = (decimal)AssembleSuction1.imgResultUp.CenterRow;
                    nudCamUpPosC1.Value = (decimal)AssembleSuction1.imgResultUp.CenterColumn;
                }
                else
                {
                    nudCamUpPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR1.Value = (decimal)AssembleSuction2.imgResultUp.CenterRow;
                    nudCamUpPosC1.Value = (decimal)AssembleSuction2.imgResultUp.CenterColumn;
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
                    nudCamUpPosR2.Value = (decimal)AssembleSuction1.imgResultUp.CenterRow;
                    nudCamUpPosC2.Value = (decimal)AssembleSuction1.imgResultUp.CenterColumn;
                }
                else
                {
                    nudCamUpPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR2.Value = (decimal)AssembleSuction2.imgResultUp.CenterRow;
                    nudCamUpPosC2.Value = (decimal)AssembleSuction2.imgResultUp.CenterColumn;
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
                    nudCamDownPosR1.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC1.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
                }
                else
                {
                    nudCamDownPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR1.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC1.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterColumn;
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
                    nudCamDownPosR2.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC2.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
                }
                else
                {
                    nudCamDownPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR2.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC2.Value = (decimal)CommonSet.dic_Assemble2[iSuctionNum].imgResultDown.CenterColumn;
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), (comboBox1.Text));
            mc.AbsMove(axis, (double)numericUpDown1.Value, (double)20);
        }

        private void btnTestGetProduct_Click(object sender, EventArgs e)
        {
            AssemGetProductModule.iTimes = (int)nudTimes.Value;
            if (iStation == 1)
            {

                AssemGetProductModule.currentPoint = CommonSet.dic_OptSuction1[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                AssemGetProductModule.currentPoint.Z = CommonSet.dic_OptSuction1[iSuctionNum].DGetPosZ;
                AssemGetProductModule.axisX = AXIS.组装X1轴;
                AssemGetProductModule.axisZ = AXIS.组装Z1轴;
                AssemGetProductModule.axisGetX = AXIS.取料X1轴;
                AssemGetProductModule.axisGetY = AXIS.取料Y1轴;
                AssemGetProductModule.axisGetZ = AXIS.取料Z1轴;
                AssemGetProductModule.dPutZ = (double)nudCheckHeight.Value;
                AssemGetProductModule.iCurrentSuctionOrder = iSuctionNum;
                AssemGetProductModule.iCurrentStation = (int)nudCheckStation.Value;
                if (AssemGetProductModule.iCurrentStation == 1)
                {
                    AssemGetProductModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    AssemGetProductModule.axisY = AXIS.组装Y2轴;
                }

            }
            else
            {
                AssemGetProductModule.currentPoint = CommonSet.dic_OptSuction2[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
                AssemGetProductModule.currentPoint.Z = CommonSet.dic_OptSuction2[iSuctionNum].DGetPosZ;
                AssemGetProductModule.axisX = AXIS.组装X2轴;
                AssemGetProductModule.axisZ = AXIS.组装Z2轴;
                AssemGetProductModule.axisGetX = AXIS.取料X2轴;
                AssemGetProductModule.axisGetY = AXIS.取料Y2轴;
                AssemGetProductModule.axisGetZ = AXIS.取料Z2轴;
                AssemGetProductModule.dPutZ = (double)nudCheckHeight.Value;
                AssemGetProductModule.iCurrentSuctionOrder = iSuctionNum;
                AssemGetProductModule.iCurrentStation = (int)nudCheckStation.Value;
                if (AssemGetProductModule.iCurrentStation == 1)
                {
                    AssemGetProductModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    AssemGetProductModule.axisY = AXIS.组装Y2轴;
                }

            }

            FrmTestAssemGetProduct frmCheck = new FrmTestAssemGetProduct();
            frmCheck.ShowDialog();
            frmCheck = null;
        }

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            if (cbUseCalibDown.Checked)
            {
                AssembleSuction1.dDownAng = AssembleSuction1.GetDownCameraAngle();
                AssembleSuction2.dDownAng = AssembleSuction1.GetDownCameraAngle();
            }
            else
            {
                AssembleSuction1.dDownAng = 0;
                AssembleSuction2.dDownAng = 0;
            }
        }

        private void cbUseCalibUp_Click(object sender, EventArgs e)
        {
            if (cbUseCalibUp.Checked)
            {
                AssembleSuction1.dUpAng = AssembleSuction1.GetUpCameraAngle();
                AssembleSuction2.dUpAng = AssembleSuction1.GetUpCameraAngle();
            }
            else
            {
                AssembleSuction1.dUpAng = 0;
                AssembleSuction2.dUpAng = 0;
            }
        }
        #region"写入数据到文件"
        private void WriteFile(string strPath,string strContent)
        {
            if (!Directory.Exists(strPath))
            {
                try
                {
                    DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
                }
                catch (Exception)
                {

                }
            }
            string file = strPath + DateTime.Now.ToString("yyMMdd") + ".csv";
            try
            {

                if (!File.Exists(file))
                {
                    FileStream fs = File.Create(file);
                    fs.Close();
                    string strHead = "时间,位置,Row,Col,";
                    //for (int i = 1; i < 10; i++)
                    //{

                    //    strHead += "C" + i.ToString() + "_," + "C" + i.ToStrsing() + "_Col,";
                    //}

                    strHead = strHead.Remove(strHead.LastIndexOf(','));
                    StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                    sw.WriteLine(strHead);
                    sw.Close();
                }

            }
            catch (Exception)
            {
            }
            try
            {
                
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ","+ strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WriteResult(string strPath, string strContent)
        {
            Action<string,string> dele = WriteFile;
            dele.BeginInvoke(strPath,strContent, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string, string> dele = (Action<string, string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }
        #endregion
    }
}
