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
using CameraSet;
using ConfigureFile;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    public partial class FrmAssem1Set : Form
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
        AXIS axisX, axisZ,axisY;
        ICamera camUp, camDown;
        public static bool bGarbData = true;

        public FrmAssem1Set()
        {
            
                // suction = CommonSet.dic_ProductSuction[iSuctionNum];
                axisX = AXIS.组装X1轴;
                axisY = AXIS.组装Y1轴;
                axisZ = AXIS.组装Z1轴;
                iStation = 1;
                // CommonSet.iCameraDownFlag = 1;
            
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
                lblCalibNum.Text = iSuctionNum.ToString();
               
                    iStation = 1;
                   
                 
                    if (!txtSuctionName.Focused)
                    {
                        txtSuctionName.Text = CommonSet.dic_OptSuction1[iSuctionNum].Name;
                    }
                    
                    //groupBox5.Text = "下相机(组装1)";
                    //groupBox4.Text = "上相机(组装1)";
                    //groupBox7.Text = "组装飞拍(组装1)";
                    //groupBox8.Text = "组装取料(中转1)";
                    //groupBox3.Text = "组装求心(中转1)";
                    //groupBox9.Text = "操作(组装1)";
                    //groupBox11.Text = "抛料(组装1)";
                    //groupBox12.Text = "吸笔平面校正位(组装1)";
                    //groupBox13.Text = "吸笔中心(吸笔" + iSuctionNum.ToString() + ")";

                    //groupBox14.Text = "上相机标定(组装1)";
                    //groupBox15.Text = "下相机标定(组装1)";
                    lblSuctionPos.Text = "吸笔"+iSuctionNum.ToString()+"拍照位:";
                    lblSuctionCenter.Text = "吸笔" + iSuctionNum.ToString() + "中心像素坐标:";
                    //lblDiscard1.Text = "吸笔"+iSuctionNum.ToString()+"抛料位1";
                    //lblDiscard2.Text = "吸笔" + iSuctionNum.ToString() + "抛料位2";

                    lblAxisX.Text = "组装X1轴";
                    lblAxisZ.Text = "组装Z1轴";
                    lblAxisY1.Text = "组装Y1轴";
                    lblAxisY2.Text = "组装Y2轴";

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
                    //if (iStationFlag == 2)
                    //{
                    //    this.dataGridView1.AutoGenerateColumns = false;
                    //    this.dataGridView1.DataSource = CommonSet.dic_Assemble1.Values.ToList();

                    //}
                    for (int i = 0; i < 9; i++)
                    {
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();

                    }
                   // iStationFlag = 1;
                  
                    lblResolution1.Text = "上相机分辨率:" + AssembleSuction1.GetUpResulotion().ToString("0.0000") + " 角度:" + AssembleSuction1.GetUpCameraAngle().ToString("0.00");
                    lblResolution2.Text = "下相机分辨率:" + AssembleSuction1.GetDownResulotion().ToString("0.0000") + " 角度:" + AssembleSuction1.GetDownCameraAngle().ToString("0.00");
                    
                
               
               

                lblSuction.Text = "组装气缸下降" + iSuctionNum.ToString();
                lblGet.Text = "组装吸真空" + iSuctionNum.ToString();
                lblPut.Text = "组装破真空" + iSuctionNum.ToString();
               // label41.Text = "吸笔"+iSuctionNum.ToString()+"校正位";

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
                //groupBox1.Text = "下相机拍照(吸笔" + iSuctionNum.ToString() + ")";
                //groupBox6.Text = "组装参数";
                panelProduct.Visible = rbtProductA.Checked;
                if(bInit)
                 UpdateControl();
                btnControls1.UpdateBtnControlUI();

                if (iSuctionNum == 1)
                {
                    btnGetCalibPosXY.Enabled = true;
                }
                else
                {
                  
                    btnGetCalibPosXY.Enabled = false;
                }

                double dUpx = 0;
                double dUpy = 0;
                double dDownX = 0;
                double dDownY = 0;
                
                dUpx = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                dUpy = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();
                dDownX = (CommonSet.dic_Assemble1[1].imgResultDown.CenterRow - 1024) * AssembleSuction1.GetDownResulotion();
                dDownY = (CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn - 1224) * AssembleSuction1.GetDownResulotion();
               
                lblDiffUp.Text = "dx:" + dUpx.ToString("0.000") + "mm  dy:" + dUpy.ToString("0.000") + "mm";
                lblDiffDown.Text = "dx:" + dDownX.ToString("0.000") + "mm  dy:" + dDownY.ToString("0.000") + "mm";
                //lblAxisPos.Text = axisX.ToString() + ":" + mc.dic_Axis[axisX].dPos.ToString("0.000") + " " + axisY.ToString() + ":" + mc.dic_Axis[axisY].dPos.ToString("0.000") + " " + axisZ.ToString() + ":" + mc.dic_Axis[axisZ].dPos.ToString("0.000");
                if (Run.runMode == RunMode.自动标定)
                {
                    if (AutoCalibModule.iMode == 0)
                    {
                        lblTestState.Text = "取标定块测试中。。";
                    }
                    else
                    {
                        lblTestState.Text = "放标定块测试中。。";
                    }

                }
                else
                {
                    lblTestState.Text = "空";
                }
               
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
           
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                setNumerialControl(nudCamUpExpProduct, AssembleSuction1.dExposureTimeUp);
                setNumerialControl(nudCamUpGainProduct, AssembleSuction1.dGainUp);
                setNumerialControl(nudCamUpLight, AssembleSuction1.dLightUp);

                setNumerialControl(nudExpProduct, AssembleSuction1.dExposureTimeDown);
                setNumerialControl(nudGainProduct, AssembleSuction1.dGainDown);
                setNumerialControl(nudCamDownLight, AssembleSuction1.dLightDown);

                setNumerialControl(nudExpSuction, AssembleSuction1.dExposureTimeSuction);
                setNumerialControl(nudGainSuction, AssembleSuction1.dGainSuction);

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
                //setNumerialControl(nudCheckPosX,AssembleSuction1.pCheckSuction.X -25*(iSuctionNum-1));
                //setNumerialControl(nudCheckPosY, AssembleSuction1.pCheckSuction.Y);
                //setNumerialControl(nudCheckPosZ, AssembleSuction1.pCheckSuction.Z);
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


                if (rbtn1.Checked)
                {
                    setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos1.X + CommonSet.dic_Assemble1[iSuctionNum].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                    setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos1.Y);
                    setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos1.Z);
                    setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos1.X);
                    setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos1.Y);

                    setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX);
                    setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY);
                    setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos1.Z);
                    setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuctionNum].dCalibPosZ);
                }
                else
                {
                    setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos2.X + CommonSet.dic_Assemble1[iSuctionNum].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                    setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos2.Y);



                    setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos2.Z);
                    setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos2.X);
                    setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos2.Y);

                    setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX2);
                    setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY2);
                    setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos2.Z);
                    setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuctionNum].dCalibPosZ);
                }

                setNumerialControl( nudGainCalibCamDown, AssembleSuction1.dCalibGainDown);
                setNumerialControl(nudGainCalibCamUp, AssembleSuction1.dCalibGainUp);
           
        }
        //保存界面的参数
        public void SaveUI()
        {

            //if (bInit)
            //    return;
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];


                AssembleSuction1.dExposureTimeUp = (double)nudCamUpExpProduct.Value;
                AssembleSuction1.dGainUp = (double)nudCamUpGainProduct.Value;
                AssembleSuction1.dLightUp = (double)nudCamUpLight.Value;

                AssembleSuction1.dExposureTimeDown = (double)nudExpProduct.Value;
                AssembleSuction1.dGainDown = (double)nudGainProduct.Value;
                AssembleSuction1.dLightDown = (double)nudCamDownLight.Value;

                AssembleSuction1.dExposureTimeSuction = (double)nudExpSuction.Value;
                AssembleSuction1.dGainSuction = (double)nudGainSuction.Value;

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

                //if (iSuctionNum == 1)
                //{
                //    AssembleSuction1.pCheckSuction.X = (double)nudCheckPosX.Value;
                //    AssembleSuction1.pCheckSuction.Y = (double)nudCheckPosY.Value;
                //    AssembleSuction1.pCheckSuction.Y = (double)nudCheckPosY.Value;
                //}
                optSuction.pSuctionCenter.X = (double)nudSuctionRow.Value;
                optSuction.pSuctionCenter.Y = (double)nudSuctionCol.Value;


                CommonSet.dic_Assemble1[iSuctionNum] = optSuction;

               
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = CommonSet.dic_Assemble1.Values.ToList();
                //组装平台1的标定参数
                if (rbtn1.Checked)
                {
                    //上下相机标定参数设置
                    if (iSuctionNum == 1)
                    {
                        CommonSet.dic_Assemble1[1].pAssemblePos1.X = (double)nudCalibX.Value;
                        CommonSet.dic_Assemble1[1].pAssemblePos1.Y = (double)nudCalibY.Value;
                    }
                    CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY = (double)nudCalibMakeUpY.Value;
                    CommonSet.dic_Assemble1[1].pAssemblePos1.Z = (double)nudCalibZ.Value;
                    AssembleSuction1.pCamBarrelPos1.Z = (double)nudCamUpPosZ.Value;
                    
                    CommonSet.dic_Assemble1[iSuctionNum].dCalibPosZ = (double)nudCamDownPosZ.Value;
                }
                else
                {
                    //组装平台2的标定参数
                    if (iSuctionNum == 1)
                    {
                        CommonSet.dic_Assemble1[1].pAssemblePos2.X = (double)nudCalibX.Value;
                        CommonSet.dic_Assemble1[1].pAssemblePos2.Y = (double)nudCalibY.Value;
                    }
                    CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                    CommonSet.dic_Assemble1[1].pAssemblePos2.Z = (double)nudCalibZ.Value;
                    AssembleSuction1.pCamBarrelPos2.Z = (double)nudCamUpPosZ.Value;
                    CommonSet.dic_Assemble1[iSuctionNum].dCalibPosZ = (double)nudCamDownPosZ.Value;
                }

                AssembleSuction1.dCalibGainUp = (double)nudGainCalibCamUp.Value;
                AssembleSuction1.dCalibGainDown = (double)nudGainCalibCamDown.Value;
        }
        private void FrmAssem1Set_Load(object sender, EventArgs e)
        {
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
        private void btnCamUpCheck_Click(object sender, EventArgs e)
        {
            try
            {
                
                    CheckUpC1(AssembleSuction1.strPicCameraUpCheckName);
               
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
                   
                    iStation = 1;
                    axisX = AXIS.组装X1轴;
                    axisZ = AXIS.组装Z1轴;
                   
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
               
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure((double)nudCamUpExposure.Value);
                        CommonSet.camUpC1.SetGain((double)nudGainCamUp.Value);
                        mc.setDO(DO.组装上相机1, true);
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
                
                    if (CommonSet.camUpC1 != null)
                    {
                       
                        mc.setDO(DO.组装上相机1, false);
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
            
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.dAssemblePosZ1;
                mc.AbsMove(axisZ, z, iVelRunZ);
            
            
        }

        private void btnGetAssem2Z_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudAssemDist2.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoAssem2Z_Click(object sender, EventArgs e)
        {
           
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.dAssemblePosZ2;
                mc.AbsMove(axisZ, z, iVelRunZ);
           
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
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
           
           
                if (OptSution1.pSafeXYZ.X + 100 < mc.dic_Axis[AXIS.取料X1轴].dPos)
                {
                    MessageBox.Show("取料X1超过安全位");
                    return;
                }
                var optSuction = CommonSet.dic_Assemble1[iSuctionNum];

                double z = optSuction.DPosCameraDownX;
                mc.AbsMove(axisX, z, iVelRunX);
            
            
        }

        private void btnCamUpCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
           
            strName = AssembleSuction1.strPicCameraUpCheckName;
            hoImage = AssembleSuction1.hImageUP;
            if (strName.Equals(""))
            {
                strName = "AssemUp" + iStation.ToString();
                AssembleSuction1.strPicCameraUpCheckName = strName;
            }
           
           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnSaveCamUpImg_Click(object sender, EventArgs e)
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

        private void btnTriggerCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (CommonSet.camDownC1 != null)
                {
                    CommonSet.camDownC1.SetExposure((double)nudCamDownExposure.Value);
                    CommonSet.camDownC1.SetGain((double)nudGainCamDown.Value);
                    mc.setDO(DO.组装下相机1, true);
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
                
                    if (CommonSet.camDownC1 != null)
                    {
                     
                        mc.setDO(DO.组装下相机1, false);
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
               
                    CheckDownC1(CommonSet.dic_Assemble1[iSuctionNum].strPicDownProduct);
                
               
            }
            catch (Exception)
            {


            }
        }

        private void btnCamDownCheckSet_Click(object sender, EventArgs e)
        {

            HObject hoImage;
            string strName = "";
           
            strName = CommonSet.dic_Assemble1[iSuctionNum].strPicDownProduct;
            hoImage = CommonSet.dic_Assemble1[1].hImageDown;
           
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

       

        private void btnGetCamUpPos1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
           
            nudCamUpPos1X.Value = (decimal)mc.dic_Axis[axisX].dPos;
             nudCamUpPos1Y.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;

           
        }

        private void btnGoCamUpPos1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
               // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pCamBarrelPos1.X);
                double y = (AssembleSuction1.pCamBarrelPos1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
          
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
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
           
            
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pCamBarrelPos2.X);
                double y = (AssembleSuction1.pCamBarrelPos2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
           
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
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
           
           
                if (OptSution1.pSafeXYZ.X + 10 < mc.dic_Axis[AXIS.取料X1轴].dPos)
                {
                    MessageBox.Show("取料X1超过安全位");
                    return;
                }
               
                double x = (AssembleSuction1.dFlashStartX);

                mc.AbsMove(axisX, x, iVelRunX);

        }

        private void btnGoGetZ_Click(object sender, EventArgs e)
        {
            

                double z = (AssembleSuction1.DGetPosZ);

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
            

                double z = (AssembleSuction1.DCorrectPosZ);

                mc.AbsMove(axisZ, z, iVelRunZ);

           
        }

        private void btnGetFlashX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudFlashEndX.Value = (decimal)mc.dic_Axis[axisX].dPos;
        }

        private void btnGoFlashX_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
          
           

                double x = (AssembleSuction1.dFlashEndX);

                mc.AbsMove(axisX, x, iVelRunX);

            
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
            //if (iStation == 1)
            //{
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

            //}
            //else
            //{
            //    ResultTestModule.bUseOptOnly = false;
            //    ResultTestModule.currentPoint = CommonSet.dic_OptSuction2[iSuctionNum].getCoordinateByIndex((int)nudPos.Value);
            //    ResultTestModule.currentPoint.Z = CommonSet.dic_OptSuction2[iSuctionNum].DGetPosZ;
            //    ResultTestModule.axisX = AXIS.组装X2轴;
            //    ResultTestModule.axisZ = AXIS.组装Z2轴;
            //    ResultTestModule.axisGetX = AXIS.取料X2轴;
            //    ResultTestModule.axisGetY = AXIS.取料Y2轴;
            //    ResultTestModule.axisGetZ = AXIS.取料Z2轴;
            //    ResultTestModule.dPutZ = (double)nudCheckHeight.Value;
            //    ResultTestModule.iCurrentSuctionOrder = iSuctionNum;
            //    ResultTestModule.iCurrentStation = (int)nudCheckStation.Value;
            //    if (ResultTestModule.iCurrentStation == 1)
            //    {
            //        ResultTestModule.axisY = AXIS.组装Y1轴;
            //    }
            //    else
            //    {
            //        ResultTestModule.axisY = AXIS.组装Y2轴;
            //    }

            //}

            FrmCheckResult frmCheck = new FrmCheckResult();
            frmCheck.ShowDialog();
            frmCheck = null;
        }

        

        private void btnGoDiscard1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) )
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
          
           
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pDiscardC1.X);
                double y = (AssembleSuction1.pDiscardC1.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
           
        }

        private void btnGoDiscard2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            
           
                // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
                double x = (AssembleSuction1.pDiscardC2.X);
                double y = (AssembleSuction1.pDiscardC2.Y);
                mc.AbsMove(axisX, x, iVelRunX);
                mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
           
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
            //if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            //    return;
            //if (iSuctionNum == 1)
            //{

            //    nudCheckPosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            //    nudCheckPosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
            //    nudCheckPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
            //}
            //else
            //{
            //    nudCheckPosX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            //    nudCheckPosY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            //    nudCheckPosZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;


            //}
        }

        private void btnGoCheckXY_Click(object sender, EventArgs e)
        {
            //if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (iStation == 1))
            //{
            //    MessageBox.Show("组装Z1轴低于安全位！");
            //    return;
            //}
            //if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (iStation == 2))
            //{
            //    MessageBox.Show("组装Z2轴低于安全位！");
            //    return;
            //}
            //if (iStation == 1)
            //{
            //    // var optSuction = CommonSet.dic_OptSuction1[iSuctionNum];
            //    double x = (double)nudCheckPosX.Value;
            //    double y = (double)nudCheckPosY.Value;
            //    mc.AbsMove(axisX, x, iVelRunX);
            //    mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
            //}
            //else
            //{
            //    double x = (double)nudCheckPosX.Value;
            //    double y = (double)nudCheckPosY.Value;
            //    mc.AbsMove(axisX, x, iVelRunX);
            //    mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);

            //}
        }

        private void btnGoCheckZ_Click(object sender, EventArgs e)
        {
          
                //double z = (double)nudCheckPosZ.Value;
               
                //mc.AbsMove(axisX, z, iVelRunZ);
               

           
        }

        private void btnGetSuctionCenter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前吸笔中心位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            try
            {
                
                    nudSuctionRow.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudSuctionCol.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
               

            }
            catch (Exception)
            {


            }
        }

        private void btnCenterCheckSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
           
                strName = CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName;
                hoImage = CommonSet.dic_Assemble1[1].hImageDown;
                if (strName.Equals(""))
                {
                    strName = "OptSuctionC" + iSuctionNum.ToString();
                    CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName = strName;
                }
           

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCenterCheck_Click(object sender, EventArgs e)
        {
            try
            {
                
                    CheckDownC1(CommonSet.dic_Assemble1[iSuctionNum].strPicDownSuctionCenterName);
               
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
                
                    if (CommonSet.camUpC1 != null)
                    {
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dResultTestGain);
                        mc.setDO(DO.组装上相机1, true);
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
                
                    CheckUpC1(AssembleSuction1.strResultSuctionName);
               
            }
            catch (Exception)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                    CheckUpC1(AssembleSuction1.strResultLenName);
               
            }
            catch (Exception)
            {


            }
        }

        private void btnCamProSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
           
            strName = AssembleSuction1.strResultSuctionName;
            hoImage = AssembleSuction1.hImageUP;
             

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnResultLenSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
            
            strName = AssembleSuction1.strResultLenName;
            hoImage = AssembleSuction1.hImageUP;
            

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
           
                mc.AbsMove(axisZ, (double)nudCheckHeight.Value, 10);

           
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
                
                    nudCamUpPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR1.Value = (decimal)AssembleSuction1.imgResultUp.CenterRow;
                    nudCamUpPosC1.Value = (decimal)AssembleSuction1.imgResultUp.CenterColumn;
               

            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamUp2_Click(object sender, EventArgs e)
        {
            try
            {
                
                    nudCamUpPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamUpPosR2.Value = (decimal)AssembleSuction1.imgResultUp.CenterRow;
                    nudCamUpPosC2.Value = (decimal)AssembleSuction1.imgResultUp.CenterColumn;
               
            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamDown1_Click(object sender, EventArgs e)
        {
            try
            {
                
                    nudCamDownPosX1.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR1.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC1.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
               

            }
            catch (Exception)
            {


            }
        }

        private void btnGetCamDown2_Click(object sender, EventArgs e)
        {
            try
            {
                
                    nudCamDownPosX2.Value = (decimal)mc.dic_Axis[axisX].dPos;
                    nudCamDownPosR2.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterRow;
                    nudCamDownPosC2.Value = (decimal)CommonSet.dic_Assemble1[iSuctionNum].imgResultDown.CenterColumn;
               

            }
            catch (Exception)
            {


            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), (comboBox1.Text));
            //mc.AbsMove(axis, (double)nudGetTime.Value, (double)20);
        }

        

        private void cbUseCalibDown_Click(object sender, EventArgs e)
        {
            if (cbUseCalibDown.Checked)
            {
                AssembleSuction1.dDownAng = AssembleSuction1.GetDownCameraAngle();
               // AssembleSuction2.dDownAng = AssembleSuction1.GetDownCameraAngle();
            }
            else
            {
                AssembleSuction1.dDownAng = 0;
                //AssembleSuction2.dDownAng = 0;
            }
        }

        private void cbUseCalibUp_Click(object sender, EventArgs e)
        {
            if (cbUseCalibUp.Checked)
            {
                AssembleSuction1.dUpAng = AssembleSuction1.GetUpCameraAngle();
                //AssembleSuction2.dUpAng = AssembleSuction1.GetUpCameraAngle();
            }
            else
            {
                AssembleSuction1.dUpAng = 0;
               // AssembleSuction2.dUpAng = 0;
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

        private void btnTriggerCamDownProduct_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

               

                if (CommonSet.camDownC1 != null)
                {
                    CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                    CommonSet.camDownC1.SetGain(AssembleSuction1.dGainDown);
                    mc.setDO(DO.组装下相机1, true);
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

                
                if (CommonSet.camDownC1 != null)
                {
                    CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeSuction);
                    CommonSet.camDownC1.SetGain(AssembleSuction1.dGainSuction);
                    mc.setDO(DO.组装下相机1, true);
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnTriggerCamUpProduct_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (CommonSet.camUpC1 != null)
                {
                    CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                    CommonSet.camUpC1.SetGain(AssembleSuction1.dExposureTimeUp);
                    mc.setDO(DO.组装上相机1, true);
                }


            }
            catch (Exception)
            {


            }
        }

        private void btnGetCalibPosXY_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudCalibX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudCalibY.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

        private void btnGoCalibXY_Click(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos1.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos1.Y, 50);
            }
            else 
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos2.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos2.Y, 50);
            }
        }

        private void btnGetCalibPosZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            nudCalibZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
        }

        private void btnGoCalibPosZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCalibZ.Value, 10);
        }

        private void btnGoMakeUp_Click(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos1.X + (double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos1.Y + (double)nudCalibMakeUpY.Value, 50);
            }
            else
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos2.X + (double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuctionNum].pAssemblePos2.Y + (double)nudCalibMakeUpY.Value, 50);
            }
        }

        private void btnUpdatePos_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("是否更新补偿位?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            double dUpx = 0;
            double dUpy = 0;
            double dDownX = 0;
            double dDownY = 0;
            bInit = false;
            //System.Threading.Thread.Sleep(500);
           
            dUpx = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
            dUpy = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();
            dDownX = (CommonSet.dic_Assemble1[1].imgResultDown.CenterRow - 1024) * AssembleSuction1.GetDownResulotion();
            dDownY = (CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn - 1224) * AssembleSuction1.GetDownResulotion();
            if (rbtn1.Checked)
            {
                nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX);
                nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY) + CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY);
                CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY = (double)nudCalibMakeUpY.Value;

                   
            }
            else
            {
                nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX2);
                nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY) + CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY2);
                CommonSet.dic_Assemble1[iSuctionNum].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble1[iSuctionNum].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                  
            }
            bInit = true;
        }

        private void btnGoCamUpPos_Click(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos1.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos1.Y, 100);
            }
            else 
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos2.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos2.Y, 100);
            }
        }

        private void btnGoCamUpZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCamUpPosZ.Value, 10);
        }

        private void rbtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn2.Checked)
            {
                bInit = false;

                axisY = AXIS.组装Y2轴;
                UpdateControl();
                bInit = true;
            }
        }

        private void rbtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn1.Checked)
            {
                bInit = false;

                axisY = AXIS.组装Y1轴;
                UpdateControl();
                bInit = true;
            }
        }

        private void btnGoCamDownPosZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCamDownPosZ.Value, 10);
        }

        private void btnCamUp_MouseDown(object sender, MouseEventArgs e)
        {
           // CommonSet.bCalibFlag = true;
            try
            {
                
                if (CommonSet.camUpC1 != null)
                {
                    AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
                    CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                    CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGainUp);
                    mc.setDO(DO.组装上相机1, true);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnCamDown_MouseDown(object sender, MouseEventArgs e)
        {
           // CommonSet.bCalibFlag = true;

            try
            {
               
                if (CommonSet.camDownC1 != null)
                {
                    CommonSet.dic_Assemble1[1].InitImageDown(AssembleSuction1.strPicDownCalibName);
                    CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                    CommonSet.camDownC1.SetGain(AssembleSuction1.dCalibGainDown);
                    mc.setDO(DO.组装下相机1, true);
                }
               

            }
            catch (Exception)
            {


            }
        }

        private void btnCheckUpSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

           
            strName = AssembleSuction1.strPicUpCalibName;
            hoImage = AssembleSuction1.hImageUP;
           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckDownSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";
           
            strName = AssembleSuction1.strPicDownCalibName;
            hoImage = CommonSet.dic_Assemble1[1].hImageDown;

            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCalibTest_Click(object sender, EventArgs e)
        {
            AutoCalibModule.iMode = 0;
            AutoCalibModule.dPutZ = (double)nudCalibZ.Value;
            AutoCalibModule.axisX = axisX;
            AutoCalibModule.axisY = axisY;
            AutoCalibModule.axisZ = axisZ;
            AutoCalibModule.bContinue = cbContinue.Checked;
            AutoCalibModule.iTimes = (int)nudTimes.Value;
            

            AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
            if (rbtn1.Checked)
            {
                AutoCalibModule.iCurrentStation = 1;
                AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos1.Z;
            }
            else
            {
                AutoCalibModule.iCurrentStation = 2;
                AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos2.Z;
            }
           
            AutoCalibModule.iCurrentSuctionOrder = iSuctionNum;
            FrmTestAutoCalib.strTitle = "取标定块测试";
            FrmTestAutoCalib frmCalib = new FrmTestAutoCalib();
            frmCalib.ShowDialog();
            frmCalib = null;
        }

        private void btnPutTest_Click(object sender, EventArgs e)
        {
            AutoCalibModule.iMode = 1;
            AutoCalibModule.dPutZ = (double)nudCalibZ.Value;
            AutoCalibModule.axisX = axisX;
            AutoCalibModule.axisY = axisY;
            AutoCalibModule.axisZ = axisZ;
            AutoCalibModule.bContinue = cbContinue.Checked;
            AutoCalibModule.iTimes = (int)nudTimes.Value;
            AutoCalibModule.bSuction = cbSuction.Checked;
          
                //AutoCalibModule.iCurrentStation = 1;
            AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
            if (rbtn1.Checked)
            {
                AutoCalibModule.iCurrentStation = 1;
                AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos1.Z;
            }
            else
            {
                AutoCalibModule.iCurrentStation = 2;
                AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos2.Z;

            }
           
            AutoCalibModule.iCurrentSuctionOrder = iSuctionNum;
            FrmTestAutoCalib.strTitle = "放标定块测试";
            FrmTestAutoCalib frmCalib = new FrmTestAutoCalib();
            frmCalib.ShowDialog();
            frmCalib = null;
        }

        private void btnCamX_Click(object sender, EventArgs e)
        {
           
            if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            
          
            mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuctionNum].DPosCameraDownX, 100);
        }



    }
}
