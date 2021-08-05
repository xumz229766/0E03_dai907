using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using ImageProcess;
using Motion;
namespace Assembly
{
    public partial class FrmCalib : Form
    {
        MotionCard mc = null;
        public int iCalibPos = 1;//1代表工位1,2代表工位2
       public HWindow hwinUp, hwinDown;
       bool bInit = true;
       AXIS axisX, axisY, axisZ;
       CalibStation station=CalibStation.取料工位1;
       public int iSuction = 1;
       
       public bool bAuto = true;
        public FrmCalib()
        {
            InitializeComponent();
        }

        private void FrmCalib_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            hwinUp = hWindowControl2.HalconWindow;
            hwinDown = hWindowControl1.HalconWindow;
            bInit = true;
            //nudCalibX.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.X;
            //nudCalibY.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.Z;
            setNumerialControl(nudGainCamDown, OptSution1.dCalibGainDown);
            setNumerialControl(nudGainCamUp, OptSution1.dCalibGainUp);
            initControl();
            bInit = false;
        }
        public void UpdateControl()
        {
            if (bInit)
            {

                return;

            }
            iSuction = (int)nudSuction.Value;
            if (rbtn1.Checked)
            {
                iCalibPos = 1;

                axisX = AXIS.组装X1轴;
                axisZ = AXIS.组装Z1轴;
                axisY = AXIS.组装Y1轴;
                setNumerialControl(nudGainCamDown, AssembleSuction1.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction1.dCalibGainUp);
                if (cbContinousCam.Checked && (Run.runMode == RunMode.手动))
                {
                    if ((CommonSet.camDownC1 != null) && (CommonSet.camUpC1 != null))
                    {
                        CommonSet.camDownC1.SetGain(AssembleSuction1.dCalibGainDown);
                        bAuto = !bAuto;
                        mc.setDO(DO.组装下相机1, bAuto);

                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGainUp);
                        mc.setDO(DO.组装上相机1, bAuto);
                    }

                }

                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos1.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos1.Y);
                    if (iSuction == 1)
                    {
                        btnGetZ.Enabled = true;
                    }
                    else
                    {
                        //nudCalibX.Value = (decimal)(CommonSet.dic_Assemble1[1].pAssemblePos1.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX);
                        //nudCalibY.Value = (decimal)CommonSet.dic_Assemble1[1].pAssemblePos1.Y;
                        btnGetZ.Enabled = false;
                    }
                    //nudCalibZ.Value = (decimal)CommonSet.dic_Assemble1[1].pAssemblePos1.Z;
                    //nudCamUpPosX.Value = (decimal)AssembleSuction1.pCamBarrelPos1.X;
                    //nudCamUpPosY.Value = (decimal)AssembleSuction1.pCamBarrelPos1.Y;
                    setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos1.Z);
                    setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos1.X);
                    setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos1.Y);

                    setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuction].dMakeUpX);
                    setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuction].dMakeUpY);
                    setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos1.Z);
                    setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuction].dCalibPosZ);
                  
               
            }
            else if(rbtn2.Checked)
            {
                iCalibPos = 2;

                axisX = AXIS.组装X1轴;
                axisZ = AXIS.组装Z1轴;
                axisY = AXIS.组装Y2轴;
                setNumerialControl(nudGainCamDown, AssembleSuction1.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction1.dCalibGainUp);
                if (cbContinousCam.Checked && (Run.runMode == RunMode.手动))
                {
                    if ((CommonSet.camDownC1 != null) && (CommonSet.camUpC1 != null))
                    {
                        CommonSet.camDownC1.SetGain(AssembleSuction1.dCalibGainDown);
                        bAuto = !bAuto;
                        mc.setDO(DO.组装下相机1, bAuto);

                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGainUp);
                        mc.setDO(DO.组装上相机1, bAuto);
                    }

                }
                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos2.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos2.Y);
                
                if (iSuction == 1)
                {
                    btnGetZ.Enabled = true;
                }
                else
                {
                    //nudCalibX.Value = (decimal)(CommonSet.dic_Assemble1[1].pAssemblePos2.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX);
                    //nudCalibY.Value = (decimal)CommonSet.dic_Assemble1[1].pAssemblePos2.Y;
                    btnGetZ.Enabled = false;
                }
                //nudCalibZ.Value = (decimal)CommonSet.dic_Assemble1[1].pAssemblePos2.Z;
                //nudCamUpPosX.Value = (decimal)AssembleSuction1.pCamBarrelPos2.X;
                //nudCamUpPosY.Value = (decimal)AssembleSuction1.pCamBarrelPos2.Y;

                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos2.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos2.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuction].dMakeUpX2);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuction].dMakeUpY2);
                setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos2.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuction].dCalibPosZ);
               
            }
            else if (rbtn3.Checked)
            {
                iCalibPos = 3;
               
                axisX = AXIS.组装X2轴;
                axisZ = AXIS.组装Z2轴;
                axisY = AXIS.组装Y1轴;


                setNumerialControl(nudGainCamDown, AssembleSuction2.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction2.dCalibGainUp);

                if (cbContinousCam.Checked && (Run.runMode == RunMode.手动))
                {
                    if ((CommonSet.camDownC2 != null) && (CommonSet.camUpC2 != null))
                    {
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dCalibGainDown);
                        bAuto = !bAuto;
                        mc.setDO(DO.组装下相机2, bAuto);

                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGainUp);
                        mc.setDO(DO.组装上相机2, bAuto);
                    }

                }
                //nudCalibX.Value = (decimal)(CommonSet.dic_Assemble2[10].pAssemblePos1.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX);
                //nudCalibY.Value = (decimal)CommonSet.dic_Assemble2[10].pAssemblePos1.Y;
                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble2[10].pAssemblePos1.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble2[10].pAssemblePos1.Y);
                
                if (iSuction == 10)
                {
                    btnGetZ.Enabled = true;
                }
                else
                {
                    btnGetZ.Enabled = false;
                }
                //nudCalibZ.Value = (decimal)CommonSet.dic_Assemble2[1].pAssemblePos1.Z;
                //nudCamUpPosX.Value = (decimal)AssembleSuction2.pCamBarrelPos1.X;
                //nudCamUpPosY.Value = (decimal)AssembleSuction2.pCamBarrelPos1.Y;
                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble2[10].pAssemblePos1.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction2.pCamBarrelPos1.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction2.pCamBarrelPos1.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble2[iSuction].dMakeUpX);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble2[iSuction].dMakeUpY);
                setNumerialControl(nudCamUpPosZ, AssembleSuction2.pCamBarrelPos1.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble2[iSuction].dCalibPosZ);
            }
            else if (rbtn4.Checked)
            {
                iCalibPos = 4;
               
                axisX = AXIS.组装X2轴;
                axisZ = AXIS.组装Z2轴;
                axisY = AXIS.组装Y2轴;
                

                setNumerialControl(nudGainCamDown, AssembleSuction2.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction2.dCalibGainUp);

                if (cbContinousCam.Checked && (Run.runMode == RunMode.手动))
                {
                    if ((CommonSet.camDownC2 != null) && (CommonSet.camUpC2 != null))
                    {
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dCalibGainDown);
                        bAuto = !bAuto;
                        mc.setDO(DO.组装下相机2, bAuto);

                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGainUp);
                        mc.setDO(DO.组装上相机2, bAuto);
                    }
                   
                }
                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble2[10].pAssemblePos2.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble2[10].pAssemblePos2.Y);
                 //nudCalibX.Value = (decimal)(CommonSet.dic_Assemble2[10].pAssemblePos2.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[1].DPosCameraDownX);
                 //nudCalibY.Value = (decimal)CommonSet.dic_Assemble2[10].pAssemblePos2.Y;
                if (iSuction == 10)
                {
                    btnGetZ.Enabled = true;
                }
                else
                {
                    btnGetZ.Enabled = false;
                }
                //nudCalibZ.Value = (decimal)CommonSet.dic_Assemble2[1].pAssemblePos2.Z;
                //nudCamUpPosX.Value = (decimal)AssembleSuction2.pCamBarrelPos2.X;
                //nudCamUpPosY.Value = (decimal)AssembleSuction2.pCamBarrelPos2.Y;
                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble2[10].pAssemblePos2.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction2.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction2.pCamBarrelPos2.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble2[iSuction].dMakeUpX2);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble2[iSuction].dMakeUpY2);
                setNumerialControl(nudCamUpPosZ, AssembleSuction2.pCamBarrelPos2.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble2[iSuction].dCalibPosZ);
            }

            lblSuction.Text = "组装气缸下降" + iSuction.ToString();
            lblGet.Text = "组装吸真空" + iSuction.ToString();
            lblPut.Text = "组装破真空" + iSuction.ToString();


            string strDo = "组装气缸下降" + iSuction.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);

            lblSuction.BackColor = getColor(mc.dic_DO[dOut]);

            strDo = "组装吸真空" + iSuction.ToString();
            dOut = (DO)Enum.Parse(typeof(DO), strDo);
            lblGet.BackColor = getColor(mc.dic_DO[dOut]);

            strDo = "组装破真空" + iSuction.ToString();
            dOut = (DO)Enum.Parse(typeof(DO), strDo);
            lblPut.BackColor = getColor(mc.dic_DO[dOut]);

            double dUpx = 0; 
            double dUpy = 0;
            double dDownX = 0;
            double dDownY = 0;
            if (iCalibPos <= 2)
            {
                dUpx = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                dUpy = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();
                dDownX = (CommonSet.dic_Assemble1[1].imgResultDown.CenterRow - 1024) * AssembleSuction1.GetDownResulotion();
                dDownY = (CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn - 1224) * AssembleSuction1.GetDownResulotion();
            }
            else
            {
                dUpx = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                dUpy = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                dDownX = (CommonSet.dic_Assemble2[10].imgResultDown.CenterRow - 1024) * AssembleSuction2.GetDownResulotion();
                dDownY = (CommonSet.dic_Assemble2[10].imgResultDown.CenterColumn - 1224) * AssembleSuction2.GetDownResulotion();
                
            }
            lblDiffUp.Text = "dx:" + dUpx.ToString("0.000") + "mm  dy:"+dUpy.ToString("0.000")+"mm";
            lblDiffDown.Text = "dx:" + dDownX.ToString("0.000") + "mm  dy:" + dDownY.ToString("0.000") + "mm";
            lblAxisPos.Text = axisX.ToString() + ":" + mc.dic_Axis[axisX].dPos.ToString("0.000") + " " + axisY.ToString() + ":" + mc.dic_Axis[axisY].dPos.ToString("0.000") + " " + axisZ.ToString() + ":" + mc.dic_Axis[axisZ].dPos.ToString("0.000");
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
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Gray;
        }
        private void initControl()
        {

            if (iCalibPos ==1)
            {
               // iCalibPos = 1;

      
                setNumerialControl(nudGainCamDown, AssembleSuction1.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction1.dCalibGainUp);
               

                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos1.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos1.Y);
              
                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos1.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos1.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos1.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuction].dMakeUpX);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuction].dMakeUpY);
                setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos1.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuction].dCalibPosZ);


            }
            else if (iCalibPos == 2)
            {
             
                setNumerialControl(nudGainCamDown, AssembleSuction1.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction1.dCalibGainUp);
               
                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble1[1].pAssemblePos2.X + CommonSet.dic_Assemble1[iSuction].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble1[1].pAssemblePos2.Y);

               

                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble1[1].pAssemblePos2.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction1.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction1.pCamBarrelPos2.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble1[iSuction].dMakeUpX2);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble1[iSuction].dMakeUpY2);
                setNumerialControl(nudCamUpPosZ, AssembleSuction1.pCamBarrelPos2.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble1[iSuction].dCalibPosZ);

            }
            else if (iCalibPos == 3)
            {
               

                setNumerialControl(nudGainCamDown, AssembleSuction2.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction2.dCalibGainUp);

                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble2[10].pAssemblePos1.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble2[10].pAssemblePos1.Y);

              
                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble2[10].pAssemblePos1.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction2.pCamBarrelPos1.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction2.pCamBarrelPos1.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble2[iSuction].dMakeUpX);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble2[iSuction].dMakeUpY);
                setNumerialControl(nudCamUpPosZ, AssembleSuction2.pCamBarrelPos1.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble2[iSuction].dCalibPosZ);
            }
            else if (iCalibPos == 4)
            {
               


                setNumerialControl(nudGainCamDown, AssembleSuction2.dCalibGainDown);
                setNumerialControl(nudGainCamUp, AssembleSuction2.dCalibGainUp);

               
                setNumerialControl(nudCalibX, (CommonSet.dic_Assemble2[10].pAssemblePos2.X + CommonSet.dic_Assemble2[iSuction].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX));
                setNumerialControl(nudCalibY, CommonSet.dic_Assemble2[10].pAssemblePos2.Y);
              
                setNumerialControl(nudCalibZ, CommonSet.dic_Assemble2[10].pAssemblePos2.Z);
                setNumerialControl(nudCamUpPosX, AssembleSuction2.pCamBarrelPos2.X);
                setNumerialControl(nudCamUpPosY, AssembleSuction2.pCamBarrelPos2.Y);

                setNumerialControl(nudCalibMakeUpX, CommonSet.dic_Assemble2[iSuction].dMakeUpX2);
                setNumerialControl(nudCalibMakeUpY, CommonSet.dic_Assemble2[iSuction].dMakeUpY2);
                setNumerialControl(nudCamUpPosZ, AssembleSuction2.pCamBarrelPos2.Z);
                setNumerialControl(nudCamDownPosZ, CommonSet.dic_Assemble2[iSuction].dCalibPosZ);
            }

        }
        public void SaveUI()
        {
            if (iCalibPos == 1)
            {
                if (iSuction == 1)
                {
                    CommonSet.dic_Assemble1[1].pAssemblePos1.X = (double)nudCalibX.Value;
                    CommonSet.dic_Assemble1[1].pAssemblePos1.Y = (double)nudCalibY.Value;
                }
                CommonSet.dic_Assemble1[iSuction].dMakeUpX = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble1[iSuction].dMakeUpY = (double)nudCalibMakeUpY.Value;
                CommonSet.dic_Assemble1[1].pAssemblePos1.Z = (double)nudCalibZ.Value;
                AssembleSuction1.pCamBarrelPos1.Z = (double)nudCamUpPosZ.Value;
                CommonSet.dic_Assemble1[iSuction].dCalibPosZ = (double)nudCamDownPosZ.Value;
            }
            else if (iCalibPos == 2)
            {
                if (iSuction == 1)
                {
                    CommonSet.dic_Assemble1[1].pAssemblePos2.X = (double)nudCalibX.Value;
                    CommonSet.dic_Assemble1[1].pAssemblePos2.Y = (double)nudCalibY.Value;
                }
                CommonSet.dic_Assemble1[iSuction].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble1[iSuction].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                CommonSet.dic_Assemble1[1].pAssemblePos2.Z = (double)nudCalibZ.Value;
                AssembleSuction1.pCamBarrelPos2.Z = (double)nudCamUpPosZ.Value;
                CommonSet.dic_Assemble1[iSuction].dCalibPosZ = (double)nudCamDownPosZ.Value;
            }
            else if (iCalibPos == 3)
            {
                if (iSuction == 10)
                {
                    CommonSet.dic_Assemble2[10].pAssemblePos1.X = (double)nudCalibX.Value;
                    CommonSet.dic_Assemble2[10].pAssemblePos1.Y = (double)nudCalibY.Value;
                }
                CommonSet.dic_Assemble2[iSuction].dMakeUpX = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble2[iSuction].dMakeUpY = (double)nudCalibMakeUpY.Value;
                CommonSet.dic_Assemble2[10].pAssemblePos1.Z = (double)nudCalibZ.Value;
                AssembleSuction2.pCamBarrelPos1.Z = (double)nudCamUpPosZ.Value;
                CommonSet.dic_Assemble2[iSuction].dCalibPosZ = (double)nudCamDownPosZ.Value;
            }
            else if (iCalibPos == 4)
            {
                if (iSuction == 10)
                {
                    CommonSet.dic_Assemble2[10].pAssemblePos2.X = (double)nudCalibX.Value;
                    CommonSet.dic_Assemble2[10].pAssemblePos2.Y = (double)nudCalibY.Value;
                }
                CommonSet.dic_Assemble2[iSuction].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                CommonSet.dic_Assemble2[iSuction].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                CommonSet.dic_Assemble2[10].pAssemblePos2.Z = (double)nudCalibZ.Value;
                AssembleSuction2.pCamBarrelPos2.Z = (double)nudCamUpPosZ.Value;
                CommonSet.dic_Assemble2[iSuction].dCalibPosZ = (double)nudCamDownPosZ.Value;
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
        private void SaveControParam()
        {
            Dictionary<int, double> dicUpC = new Dictionary<int, double>();
            Dictionary<int, double> dicUpR = new Dictionary<int, double>();
            Dictionary<int, double> dicDownC = new Dictionary<int, double>();
            Dictionary<int, double> dicDownR = new Dictionary<int, double>();
            
           
            
        }
        
        private void btnCamUp_Click(object sender, EventArgs e)
        {
           
        }
        private void CheckCamUp()
        {

            //if (CommonSet.dic_BarrelSuction[iCalibPos].hImageCenter == null)
            //{
            //    MessageBox.Show("请先获取图像！");
            //    return;
            //}

            //CommonSet.dic_BarrelSuction[iCalibPos].InitImageCenter(BarrelSuction.strCalibNameUp);

            //if (CommonSet.dic_BarrelSuction[iCalibPos].pfCenter == null)
            //{
            //    MessageBox.Show("检测方法为空，请先设定！");
            //    return;
            //}
            //CommonSet.dic_BarrelSuction[iCalibPos].pfCenter.SetRun(true);
            //CommonSet.dic_BarrelSuction[iCalibPos].pfCenter.hwin = hwinUp;
            //CommonSet.dic_BarrelSuction[iCalibPos].actionCenter(CommonSet.dic_BarrelSuction[iCalibPos].hImageCenter);
            //CommonSet.dic_BarrelSuction[iCalibPos].pfCenter.showObj();
            //HOperatorSet.WriteString(hwinUp, CommonSet.dic_BarrelSuction[iCalibPos].dCenterUpRow);
            //string strDispRow = "Row:" + CommonSet.dic_BarrelSuction[iCalibPos].dCenterUpRow.ToString("0.000");
            //string strDispCol = "Col:" + CommonSet.dic_BarrelSuction[iCalibPos].dCenterUpColumn.ToString("0.000");
            //CommonSet.disp_message(hwinUp, strDispRow, "window", 0, 0, "black", "true");
            //CommonSet.disp_message(hwinUp, strDispCol, "window", 20, 0, "black", "true");
        }
        List<double> lstRows = new List<double>();
        List<double> lstColumns = new List<double>();
        private bool action(HObject ho_Image,HWindow hwin, string color)
        {


            // Stack for temporary objects 
           

            // Local iconic variables 

            HObject  ho_Region, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_SelectedRegions1, ho_rCircles;
            HObject ho_DestRegions = null, ho_Contours = null, ho_Contour = null;
            HObject ho_Contours1 = null, ho_Cross;
            HObject ho_Rect, ho_ReducedImg;
            // Local control variables 

            HTuple hv_color = null, hv_UsedThreshold = null;
            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Sorted = null, hv_Indices = null, hv_r = null;
            HTuple hv_c = null, hv_MetrologyHandle = null, hv_i = null;
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius = new HTuple(), hv_StartPhi = new HTuple();
            HTuple hv_EndPhi = new HTuple(), hv_PointOrder = new HTuple();
            HTuple hv_Index = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Parameter = new HTuple();
            // Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_rCircles);
            HOperatorSet.GenEmptyObj(out ho_DestRegions);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_Contours1);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_ReducedImg);
            HOperatorSet.GenEmptyObj(out ho_Rect);
            //hv_color = "dark";
            //ho_Image.Dispose();
            //HOperatorSet.ReadImage(out ho_Image, "E:/S1200919093605.bmp");
            try
            {

                for (int minThreshold = 5; minThreshold < 200; minThreshold = minThreshold +5)
                {
                    try
                    {
                        //HOperatorSet.GenRectangle1(out ho_Rect, 50, 50, 2000, 2400);
                        //HOperatorSet.ReduceDomain(ho_Image, ho_Rect, out ho_ReducedImg);
                      
                    ho_Region.Dispose();
                    
                    //HOperatorSet.BinaryThreshold(ho_Image, out ho_Region, "max_separability", color,
                    //    out hv_UsedThreshold);

                    HOperatorSet.Threshold(ho_Image, out ho_Region, minThreshold, 255);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "circularity",
                        "and", 0.85, 1);
                    ho_SelectedRegions1.Dispose();
                    HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions1, "area",
                        "and", 5000, 999999);
                    //hv_Area = new HTuple();
                    //hv_Row = new HTuple();
                    //hv_Column = new HTuple();
                    HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                    HOperatorSet.TupleSortIndex(hv_Area, out hv_Indices);
                   // System.Threading.Thread.Sleep(10);
                    if (hv_Area.Length != 5)
                        continue;
                    else
                        break;

                    }
                    catch (Exception)
                    {

                        
                    }
                    
                }
            hv_r = new HTuple();
            hv_c = new HTuple();
            ho_rCircles.Dispose();
            HOperatorSet.GenEmptyObj(out ho_rCircles);
            HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
            for (hv_i = 0; (int)hv_i <= 4; hv_i = (int)hv_i + 1)
            {
                ho_DestRegions.Dispose();
                HOperatorSet.SelectRegionPoint(ho_SelectedRegions1, out ho_DestRegions, hv_Row.TupleSelect(
                    hv_Indices.TupleSelect(hv_i)), hv_Column.TupleSelect(hv_Indices.TupleSelect(
                    hv_i)));
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_DestRegions, out ho_Contours, "border");
                HOperatorSet.FitCircleContourXld(ho_Contours, "geotukey", -1, 0, 0, 3, 2, out hv_Row1,
                    out hv_Column1, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
                HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, hv_Row1, hv_Column1,
                    hv_Radius, 30, 3, 1, 30, new HTuple(), new HTuple(), out hv_Index);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_distance",
                    3);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_select",
                    "first");
                HOperatorSet.ApplyMetrologyModel(ho_Image, hv_MetrologyHandle);
                //if (HDevWindowStack.IsOpen())
                //{
                //    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                //}
                ho_Contour.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Contour, hv_MetrologyHandle,
                    "all", "all", 1.5);
                ho_Contours1.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours1, hv_MetrologyHandle,
                    "all", "all", out hv_Row2, out hv_Column2);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, "all", "all", "result_type",
                    (new HTuple("row")).TupleConcat("column"), out hv_Parameter);
                HOperatorSet.TupleConcat(hv_r, hv_Parameter.TupleSelect(0), out hv_r);
                HOperatorSet.TupleConcat(hv_c, hv_Parameter.TupleSelect(1), out hv_c);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_rCircles, ho_Contour, out ExpTmpOutVar_0);
                    ho_rCircles.Dispose();
                    ho_rCircles = ExpTmpOutVar_0;
                }
                HOperatorSet.ClearMetrologyObject(hv_MetrologyHandle, "all");
            }

            //if (HDevWindowStack.IsOpen())
            //{
            //    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            //}
            ho_Cross.Dispose();

            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_r, hv_c, 6, 0);
            lstColumns.Clear();
            lstRows.Clear();

            HOperatorSet.SetColor(hwin, "green");
            for (int i = 0; i < 5; i++)
            {

                lstRows.Add(hv_r[i]);
                lstColumns.Add(hv_c[i]);
                HOperatorSet.SetTposition(hwin, hv_r[i], hv_c[i]);
                HOperatorSet.WriteString(hwin, (i + 1).ToString());
            }
            HOperatorSet.DispObj(ho_Cross, hwin);
            HOperatorSet.DispObj(ho_rCircles, hwin);
            HOperatorSet.ClearMetrologyModel(hv_MetrologyHandle);
            }
            catch (Exception)
            {

                return false;
            }
            //if (HDevWindowStack.IsOpen())
            //{
            //    HOperatorSet.DispObj(ho_rCircles, HDevWindowStack.GetActive());
            //}
           
            //ho_Image.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_rCircles.Dispose();
            ho_DestRegions.Dispose();
            ho_Contours.Dispose();
            ho_Contour.Dispose();
            ho_Contours1.Dispose();
            ho_Cross.Dispose();
            return true;
        }
        private void btnCalib_Click(object sender, EventArgs e)
        {
            SaveControParam();
            if (CommonSet.dic_Calib[station].createCamDownToUp())
            {
                MessageBox.Show("标定成功!");
            }
            else {
                MessageBox.Show("标定失败!");
            }
        }

        private void btnCamDown_Click(object sender, EventArgs e)
        {
            //if (iCalibPos == 1)
            //{
            //    if (CommonSet.camDownA1 != null)
            //    {
            //        CommonSet.camDownA1.SetExposure(OptSution1.dExposureTimeDown);
            //        CommonSet.camDownA1.SetGain(OptSution1.dGainDown);
            //        mc.setDO(DO.取料下相机1, true);
            //    }
            //}
            //else if (iCalibPos == 2)
            //{
            //    if (CommonSet.camDownA1 != null)
            //    {
            //        CommonSet.camDownA1.SetExposure(OptSution1.dExposureTimeDown);
            //        CommonSet.camDownA1.SetGain(OptSution1.dGainDown);
            //        mc.setDO(DO.取料下相机1, true);
            //    }
            //}
        }
        private void checkCamDown()
        {
            //int iSuctionNum = 1;
            //if (CommonSet.dic_ProductSuction[iSuctionNum].hImage == null)
            //{
            //    MessageBox.Show("请先获取图像！");
            //    return;
            //}

            //CommonSet.dic_ProductSuction[iSuctionNum].InitImage(BarrelSuction.strCalibNameDown);

            //if (CommonSet.dic_ProductSuction[iSuctionNum].pf == null)
            //{
            //    MessageBox.Show("检测方法为空，请先设定！");
            //    return;
            //}
            //CommonSet.dic_ProductSuction[iSuctionNum].pf.SetRun(true);
            //CommonSet.dic_ProductSuction[iSuctionNum].pf.hwin = hwinDown;
            //CommonSet.dic_ProductSuction[iSuctionNum].action(CommonSet.dic_ProductSuction[iSuctionNum].hImage);
            //CommonSet.dic_ProductSuction[iSuctionNum].pf.showObj();
            //HOperatorSet.WriteString(hwinDown, CommonSet.dic_ProductSuction[iSuctionNum].dCenterDownRow);
            //string strDispRow = "Row:" + CommonSet.dic_ProductSuction[iSuctionNum].dCenterDownRow.ToString("0.000");
            //string strDispCol = "Col:" + CommonSet.dic_ProductSuction[iSuctionNum].dCenterDownColumn.ToString("0.000");
            //CommonSet.disp_message(hwinDown, strDispRow, "window", 0, 0, "black", "true");
            //CommonSet.disp_message(hwinDown, strDispCol, "window", 20, 0, "black", "true");
        
        }
       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //cmbProcessNameUp.Items.Clear();
            //cmbProcessNameUp.Items.AddRange(CommonSet.imageManager.GetFactoryNames());
            //cmbProcessNameUp.Text = BarrelSuction.strCalibNameUp;
            //cmbProcessNameDown.Items.Clear();
            //cmbProcessNameDown.Items.AddRange(CommonSet.imageManager.GetFactoryNames());
            //cmbProcessNameDown.Text = BarrelSuction.strCalibNameDown;
        }
        FrmProcess frmProcess = null;
        private void btnSet_Click(object sender, EventArgs e)
        {
            frmProcess = null;
            if (frmProcess == null)
            {
                frmProcess = new FrmProcess();
            }
            frmProcess.Show();
        }

        private void btnSaveImgUp_Click(object sender, EventArgs e)
        {
           // int iSuctionNum = 1;
            if (CommonSet.dic_Calib[station].hImageUp == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Len_" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(CommonSet.dic_Calib[station].hImageUp, "bmp", 0, fileName);
            }
        }

        private void btnSaveImgDown_Click(object sender, EventArgs e)
        {
            //int iSuctionNum = 1;
            if (CommonSet.dic_Calib[station].hImageDown == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Opt_" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(CommonSet.dic_Calib[station].hImageDown, "bmp", 0, fileName);
            }
        }

        private void cmbProcessNameUp_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BarrelSuction.strCalibNameUp = cmbProcessNameUp.Text;
        }

        private void cmbProcessNameDown_SelectedIndexChanged(object sender, EventArgs e)
        {
           // BarrelSuction.strCalibNameDown = cmbProcessNameDown.Text;
        }

        private void btnGetX_Click(object sender, EventArgs e)
        {
           
           nudCalibX.Value = (decimal)mc.dic_Axis[axisX].dPos;
           
        }

       

        private void btnGetZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudCalibX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudCalibY.Value = (decimal)mc.dic_Axis[axisY].dPos;
            //if (iCalibPos == 1)
            //{
            //   CommonSet.dic_Assemble1[1].pAssemblePos1.X = mc.dic_Axis[axisX].dPos;
            //   CommonSet.dic_Assemble1[1].pAssemblePos1.Y = mc.dic_Axis[axisY].dPos;
               
            //}
            //else if (iCalibPos == 2)
            //{
            //    CommonSet.dic_Assemble1[1].pAssemblePos2.X = mc.dic_Axis[axisX].dPos;
            //    CommonSet.dic_Assemble1[1].pAssemblePos2.Y = mc.dic_Axis[axisY].dPos;
               
            //}
            //else if (iCalibPos == 3)
            //{
            //    CommonSet.dic_Assemble2[10].pAssemblePos1.X = mc.dic_Axis[axisX].dPos;
            //    CommonSet.dic_Assemble2[10].pAssemblePos1.Y = mc.dic_Axis[axisY].dPos;
                
            //}
            //else if (iCalibPos == 4)
            //{
            //    CommonSet.dic_Assemble2[10].pAssemblePos2.X = mc.dic_Axis[axisX].dPos;
            //    CommonSet.dic_Assemble2[10].pAssemblePos2.Y = mc.dic_Axis[axisY].dPos;
                
            //}
          
        }

        private void nudCalibX_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
            {
                return;
            }
            try
            {
                SaveUI();
           
               // CommonSet.dic_Calib[station].pCalibPos.X = (double)nudCalibX.Value;
                //if (rbtn1.Checked)
                //{
                //    CommonSet.dic_Assemble1[iSuction].pAssemblePos1.X = mc.dic_Axis[axisX].dPos;
                //    CommonSet.dic_Assemble1[iSuction].pAssemblePos1.Y = mc.dic_Axis[axisY].dPos;
                //}
                //else if(rbtn2.Checked)
                //{
                //    CommonSet.dic_Assemble1[iSuction].pAssemblePos2.X = mc.dic_Axis[axisX].dPos;
                //    CommonSet.dic_Assemble1[iSuction].pAssemblePos2.Y = mc.dic_Axis[axisY].dPos;
                //}
                //else if (rbtn3.Checked)
                //{
                //    CommonSet.dic_Assemble2[iSuction].pAssemblePos1.X = mc.dic_Axis[axisX].dPos;
                //    CommonSet.dic_Assemble2[iSuction].pAssemblePos1.Y = mc.dic_Axis[axisY].dPos;
                //}
                //else if (rbtn4.Checked)
                //{
                //    CommonSet.dic_Assemble2[iSuction].pAssemblePos2.X = mc.dic_Axis[axisX].dPos;
                //    CommonSet.dic_Assemble2[iSuction].pAssemblePos2.Y = mc.dic_Axis[axisY].dPos;
                //}
            }
            catch (Exception)
            {

                
            }
        }

        private void nudCalibZ_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSafeZ_Click(object sender, EventArgs e)
        {
            if (iCalibPos == 1)
            {
                mc.AbsMove(axisZ, AssembleSuction1.pSafeXYZ.Z, 10);
            }
            else if(iCalibPos == 2){

                mc.AbsMove(axisZ, AssembleSuction1.pSafeXYZ.Z, 10);
            }
            else if (iCalibPos == 3)
            {

                mc.AbsMove(axisZ, AssembleSuction2.pSafeXYZ.Z, 10);
            }
            else if (iCalibPos == 4)
            {

                mc.AbsMove(axisZ, AssembleSuction2.pSafeXYZ.Z, 10);
            }
        }

        private void btnCamX_Click(object sender, EventArgs e)
        {
            if (iCalibPos == 1)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos1.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos1.Y, 100);
            }
            else if (iCalibPos == 2)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos2.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos2.Y, 100);
            }
            else if (iCalibPos ==3)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }

                mc.AbsMove(axisX, AssembleSuction2.pCamBarrelPos1.X, 100);
                mc.AbsMove(axisY, AssembleSuction2.pCamBarrelPos1.Y, 100);
            }
            else if (iCalibPos == 4)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction2.pCamBarrelPos2.X, 100);
                mc.AbsMove(axisY, AssembleSuction2.pCamBarrelPos2.Y, 100);
            }
        }

        private void btnCamZ_Click(object sender, EventArgs e)
        {
            if (iCalibPos <=2)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
            }
            else if (iCalibPos >= 3)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }

            }
           mc.AbsMove(axisZ, CommonSet.dic_Calib[station].pCalibPos.Z, 100);
            
        }

        private void btnCamUp_MouseDown(object sender, MouseEventArgs e)
        {
            CommonSet.bCalibFlag = true;
            try
            {
                if (iCalibPos == 1)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGainUp);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else if (iCalibPos == 2)
                {
                    if (CommonSet.camUpC1 != null)
                    {
                        AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
                        CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                        CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGainUp);
                        mc.setDO(DO.组装上相机1, true);
                    }
                }
                else if (iCalibPos == 3)
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCalibName);
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGainUp);
                        mc.setDO(DO.组装上相机2, true);
                    }
                }

                else if (iCalibPos == 4)
                {
                    if (CommonSet.camUpC2 != null)
                    {
                        AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCalibName);
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGainUp);
                        mc.setDO(DO.组装上相机2, true);
                    }
                }

            }
            catch (Exception)
            {


            }



        }

        private void btnCamUp_MouseUp(object sender, MouseEventArgs e)
        {
            CommonSet.bCalibFlag = true;
            if (iCalibPos == 1)
            {
                mc.setDO(DO.组装上相机1, false);

            }
            else if (iCalibPos == 2)
            {
                mc.setDO(DO.组装上相机1, false);
            }
            else if (iCalibPos == 3)
            {
                mc.setDO(DO.组装上相机2, false);
            }
            else if (iCalibPos == 4)
            {
                mc.setDO(DO.组装上相机2, false);
            }
        }

        private void btnCamDown_MouseDown(object sender, MouseEventArgs e)
        {
            CommonSet.bCalibFlag = true;
          
            try
            {
                if (iCalibPos == 1)
                {
                    if (CommonSet.camDownC1 != null)
                    {
                       CommonSet.dic_Assemble1[1].InitImageDown(AssembleSuction1.strPicDownCalibName);
                        CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                        CommonSet.camDownC1.SetGain(AssembleSuction1.dCalibGainDown);
                        mc.setDO(DO.组装下相机1, true);
                    }
                }
                else if (iCalibPos == 2)
                {
                    if (CommonSet.camDownC1 != null)
                    {
                        CommonSet.dic_Assemble1[1].InitImageDown(AssembleSuction1.strPicDownCalibName);
                        CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);
                        CommonSet.camDownC1.SetGain(AssembleSuction1.dCalibGainDown);
                        mc.setDO(DO.组装下相机1, true);
                    }
                }
                else if (iCalibPos == 3)
                {
                    if (CommonSet.camDownC2 != null)
                    {
                        CommonSet.dic_Assemble2[10].InitImageDown(AssembleSuction2.strPicDownCalibName);
                        CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dCalibGainDown);
                        mc.setDO(DO.组装下相机2, true);
                    }
                }

                else if (iCalibPos == 4)
                {
                    if (CommonSet.camDownC2 != null)
                    {
                        CommonSet.dic_Assemble2[10].InitImageDown(AssembleSuction2.strPicDownCalibName);
                        CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dCalibGainDown);
                        mc.setDO(DO.组装下相机2, true);
                    }
                }
               
            }
            catch (Exception)
            {


            }

        }

        private void btnCamDown_MouseUp(object sender, MouseEventArgs e)
        {
            CommonSet.bCalibFlag = true;
            if (iCalibPos == 1)
            {

                mc.setDO(DO.组装下相机1, false);

            }
            else if (iCalibPos == 2)
            {
                mc.setDO(DO.组装下相机1, false);
            }
            else if (iCalibPos == 3)
            {
                mc.setDO(DO.组装下相机2, false);
            }
            else if (iCalibPos == 4)
            {
                mc.setDO(DO.组装下相机2, false);
            }
        }

        private void rbtn1_CheckedChanged(object sender, EventArgs e)
        {
           
            bInit = true;


            iCalibPos = 1;
            station = CalibStation.取料工位1;
            axisX = AXIS.取料X1轴;
            axisZ = AXIS.取料Z1轴;
            //nudCalibX.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.X;
            //nudCalibY.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.Z;
           
            nudSuction.Maximum = 9;
            nudSuction.Minimum = 1;
            setNumerialControl(nudSuction, 1);
           // iSuction = 1;
            initControl();
            bInit = false;
        }
        private void rbtn2_CheckedChanged(object sender, EventArgs e)
        {
           
            bInit = true;
          
           
            iCalibPos = 2;
            station = CalibStation.取料工位2;
            axisX = AXIS.取料X2轴;
            axisZ = AXIS.取料Z2轴;
            //nudCalibX.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.X;
            //nudCalibY.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.Z;
         
            nudSuction.Maximum = 9;
            nudSuction.Minimum = 1;
            setNumerialControl(nudSuction, 1);
           // iSuction = 1;
            initControl();
            bInit = false;
        }

        private void rbtn3_CheckedChanged(object sender, EventArgs e)
        {

            
            bInit = true;


            iCalibPos = 3;
            station = CalibStation.组装工位1;
            axisX = AXIS.组装X1轴;
            axisZ = AXIS.组装Z1轴;
            //nudCalibX.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.X;
            //nudCalibY.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.Z;
           
           
            nudSuction.Maximum = 18;
            nudSuction.Minimum = 10;
            setNumerialControl(nudSuction, 10);
           // iSuction = 10;
            initControl();
            bInit = false;
           
        }

        private void rbtn4_CheckedChanged(object sender, EventArgs e)
        {
           
            bInit = true;


            iCalibPos = 4;
            station = CalibStation.组装工位2;
            axisX = AXIS.组装X2轴;
            axisZ = AXIS.组装Z2轴;
            //nudCalibX.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.X;
            //nudCalibY.Value = (decimal)CommonSet.dic_Calib[station].pCalibPos.Z;
           
            nudSuction.Maximum = 18;
            nudSuction.Minimum = 10;
            setNumerialControl(nudSuction, 10);
            iSuction = 10;
            initControl();
            bInit = false;
        }

        private void nudGainCamUp_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
            {
                return;
            }
            if (iCalibPos == 1)
            {

                AssembleSuction1.dCalibGainUp = (double)nudGainCamUp.Value;

            }
            else if (iCalibPos == 2)
            {
                AssembleSuction1.dCalibGainUp = (double)nudGainCamUp.Value;
            }
            else if (iCalibPos == 3)
            {
                AssembleSuction2.dCalibGainUp = (double)nudGainCamUp.Value;
            }
            else if (iCalibPos == 4)
            {
                AssembleSuction2.dCalibGainUp = (double)nudGainCamUp.Value;
            }
           
        }

        private void nudGainCamDown_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
            {
                return;
            }
            if (iCalibPos == 1)
            {

                AssembleSuction1.dCalibGainDown = (double)nudGainCamDown.Value;

            }
            else if (iCalibPos == 2)
            {
                AssembleSuction1.dCalibGainDown = (double)nudGainCamDown.Value;
            }
            else if (iCalibPos == 3)
            {
                AssembleSuction2.dCalibGainDown = (double)nudGainCamDown.Value;
            }
            else if (iCalibPos == 4)
            {
                AssembleSuction2.dCalibGainDown = (double)nudGainCamDown.Value;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (iCalibPos == 1)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuction].pAssemblePos1.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuction].pAssemblePos1.Y, 50);
            }
            else if (iCalibPos == 2)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuction].pAssemblePos2.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuction].pAssemblePos2.Y, 50);
            }
            else if (iCalibPos == 3)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }


                mc.AbsMove(axisX, CommonSet.dic_Assemble2[iSuction].pAssemblePos1.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble2[iSuction].pAssemblePos1.Y, 50);
            }
            else if (iCalibPos == 4)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble2[iSuction].pAssemblePos2.X, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble2[iSuction].pAssemblePos2.Y, 50);
            }
        }

        private void nudCalibZ_ValueChanged_1(object sender, EventArgs e)
        {
            if (bInit)
                return;
            if (iCalibPos == 1)
            {
               
            }
            else if (iCalibPos == 2)
            {
                CommonSet.dic_Assemble1[1].pAssemblePos2.Z = (double)nudCalibZ.Value;
            }
            else if (iCalibPos == 3)
            {
                CommonSet.dic_Assemble2[10].pAssemblePos1.Z = (double)nudCalibZ.Value;
            }
            else if (iCalibPos == 4)
            {
                CommonSet.dic_Assemble2[10].pAssemblePos2.Z = (double)nudCalibZ.Value;
            }
        }

        private void btnGetPosZ_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            //if (iCalibPos == 1)
            //{
            //    CommonSet.dic_Assemble1[1].pAssemblePos1.Z = mc.dic_Axis[axisZ].dPos;
            //}
            //else if (iCalibPos == 2)
            //{
            //    CommonSet.dic_Assemble1[1].pAssemblePos2.Z = mc.dic_Axis[axisZ].dPos;
            //}
            //else if (iCalibPos == 3)
            //{
            //    CommonSet.dic_Assemble2[10].pAssemblePos1.Z = mc.dic_Axis[axisZ].dPos;
            //}
            //else if (iCalibPos == 4)
            //{
            //    CommonSet.dic_Assemble2[10].pAssemblePos2.Z = mc.dic_Axis[axisZ].dPos;
            //}

            nudCalibZ.Value = (decimal)mc.dic_Axis[axisZ].dPos;
           
        }

        private void btnGoPosZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCalibZ.Value, 10);
        }

        

        private void btnGoCamUpPos_Click(object sender, EventArgs e)
        {
            if (iCalibPos == 1)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos1.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos1.Y, 100);
            }
            else if (iCalibPos == 2)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction1.pCamBarrelPos2.X, 100);
                mc.AbsMove(axisY, AssembleSuction1.pCamBarrelPos2.Y, 100);
            }
            else if (iCalibPos == 3)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }

                mc.AbsMove(axisX, AssembleSuction2.pCamBarrelPos1.X, 100);
                mc.AbsMove(axisY, AssembleSuction2.pCamBarrelPos1.Y, 100);
            }
            else if (iCalibPos == 4)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, AssembleSuction2.pCamBarrelPos2.X, 100);
                mc.AbsMove(axisY, AssembleSuction2.pCamBarrelPos2.Y, 100);
            }
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
            if ((iCalibPos == 1) || (iCalibPos == 2))
            {
              
                AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
                if (iCalibPos == 1)
                {
                    AutoCalibModule.iCurrentStation = 1;
                    AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos1.Z;
                }
                else
                {
                    AutoCalibModule.iCurrentStation = 2;
                    AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos2.Z;
                }
            }
            else
            {
                
                AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
                if (iCalibPos == 3)
                {
                    AutoCalibModule.iCurrentStation = 1;
                    AutoCalibModule.dCamUpZ = AssembleSuction2.pCamBarrelPos1.Z;
                }
                else
                {
                    AutoCalibModule.iCurrentStation = 2;
                    AutoCalibModule.dCamUpZ = AssembleSuction2.pCamBarrelPos2.Z;
                }
            }
            AutoCalibModule.iCurrentSuctionOrder = iSuction;
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
            if ((iCalibPos == 1) || (iCalibPos == 2))
            {
                //AutoCalibModule.iCurrentStation = 1;
                AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
                if (iCalibPos == 1)
                {
                    AutoCalibModule.iCurrentStation = 1;
                    AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos1.Z;
                }
                else
                {
                    AutoCalibModule.iCurrentStation = 2;
                    AutoCalibModule.dCamUpZ = AssembleSuction1.pCamBarrelPos2.Z;

                }
            }
            else
            {
               
                AutoCalibModule.dCamDownZ = (double)nudCamDownPosZ.Value;
                if (iCalibPos == 3)
                {
                    AutoCalibModule.iCurrentStation = 1;
                    AutoCalibModule.dCamUpZ = AssembleSuction2.pCamBarrelPos1.Z;
                }
                else
                {
                    AutoCalibModule.iCurrentStation = 2;
                    AutoCalibModule.dCamUpZ = AssembleSuction2.pCamBarrelPos2.Z;
                }
            }
            AutoCalibModule.iCurrentSuctionOrder = iSuction;
            FrmTestAutoCalib.strTitle = "放标定块测试";
            FrmTestAutoCalib frmCalib = new FrmTestAutoCalib();
            frmCalib.ShowDialog();
            frmCalib = null;
        }

        private void btnUpdatePos_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("是否更新补偿位?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
             double dUpx = 0; 
            double dUpy = 0;
            double dDownX = 0;
            double dDownY = 0;
            bInit = true;
            //System.Threading.Thread.Sleep(500);
            if (iCalibPos <= 2)
            {
                dUpx = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                dUpy = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();
                dDownX = (CommonSet.dic_Assemble1[1].imgResultDown.CenterRow - 1024) * AssembleSuction1.GetDownResulotion();
                dDownY = (CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn - 1224) * AssembleSuction1.GetDownResulotion();
                if (iCalibPos == 1)
                {
                    nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble1[iSuction].dMakeUpX);
                    nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY)+ CommonSet.dic_Assemble1[iSuction].dMakeUpY);
                    CommonSet.dic_Assemble1[iSuction].dMakeUpX = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble1[iSuction].dMakeUpY = (double)nudCalibMakeUpY.Value;
                   
                    //nudCamUpPosX.Value = (decimal)(AssembleSuction1.pCamBarrelPos1.X - (dUpx - dDownX));
                    //nudCamUpPosY.Value =(decimal)( AssembleSuction1.pCamBarrelPos1.Y + (dUpy - dDownY));
                    //AssembleSuction1.pCamBarrelPos1.X = AssembleSuction1.pCamBarrelPos1.X - (dUpx - dDownX);
                    //AssembleSuction1.pCamBarrelPos1.Y = AssembleSuction1.pCamBarrelPos1.Y + (dUpy - dDownY);
                }
                else
                {
                    nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble1[iSuction].dMakeUpX2);
                    nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY) + CommonSet.dic_Assemble1[iSuction].dMakeUpY2);
                    CommonSet.dic_Assemble1[iSuction].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble1[iSuction].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                    //nudCamUpPosX.Value = (decimal)(AssembleSuction1.pCamBarrelPos2.X - (dUpx - dDownX));
                    //nudCamUpPosY.Value = (decimal)(AssembleSuction1.pCamBarrelPos2.Y + (dUpy - dDownY));
                    //AssembleSuction1.pCamBarrelPos2.X = AssembleSuction1.pCamBarrelPos2.X - (dUpx - dDownX);
                    //AssembleSuction1.pCamBarrelPos2.Y = AssembleSuction1.pCamBarrelPos2.Y + (dUpy - dDownY);
                }
            }
            else
            {
                dUpx = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                dUpy = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                dDownX = (CommonSet.dic_Assemble2[10].imgResultDown.CenterRow - 1024) * AssembleSuction2.GetDownResulotion();
                dDownY = (CommonSet.dic_Assemble2[10].imgResultDown.CenterColumn - 1224) * AssembleSuction2.GetDownResulotion();
                if (iCalibPos == 3)
                {
                    nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble2[iSuction].dMakeUpX);
                    nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY)+ CommonSet.dic_Assemble2[iSuction].dMakeUpY);
                    CommonSet.dic_Assemble2[iSuction].dMakeUpX = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble2[iSuction].dMakeUpY = (double)nudCalibMakeUpY.Value;
                   
                    //AssembleSuction2.pCamBarrelPos1.X = AssembleSuction2.pCamBarrelPos1.X - (dUpx - dDownX);
                    //AssembleSuction2.pCamBarrelPos1.Y = AssembleSuction2.pCamBarrelPos1.Y + (dUpy - dDownY);
                    //nudCamUpPosX.Value = (decimal)(AssembleSuction2.pCamBarrelPos1.X - (dUpx - dDownX));
                    //nudCamUpPosY.Value = (decimal)(AssembleSuction2.pCamBarrelPos1.Y + (dUpy - dDownY));
                }
                else
                {
                    nudCalibMakeUpX.Value = (decimal)((dUpx - dDownX) + CommonSet.dic_Assemble2[iSuction].dMakeUpX2);
                    nudCalibMakeUpY.Value = (decimal)(-(dUpy - dDownY) + CommonSet.dic_Assemble2[iSuction].dMakeUpY2);
                    CommonSet.dic_Assemble2[iSuction].dMakeUpX2 = (double)nudCalibMakeUpX.Value;
                    CommonSet.dic_Assemble2[iSuction].dMakeUpY2 = (double)nudCalibMakeUpY.Value;
                  
                    ////nudCamUpPosX.Value = (decimal)(AssembleSuction2.pCamBarrelPos2.X - (dUpx - dDownX));
                    //nudCamUpPosY.Value = (decimal)(AssembleSuction2.pCamBarrelPos2.Y + (dUpy - dDownY));
                    //AssembleSuction2.pCamBarrelPos2.X = AssembleSuction2.pCamBarrelPos2.X - (dUpx - dDownX);
                    //AssembleSuction2.pCamBarrelPos2.Y = AssembleSuction2.pCamBarrelPos2.Y + (dUpy - dDownY);
                }
            }

            bInit = false;
        }

        private void btnCheckUpSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            if (iCalibPos <= 2)
            {
                strName = AssembleSuction1.strPicUpCalibName;
                hoImage = AssembleSuction1.hImageUP;
            }
            else
            {
                strName = AssembleSuction2.strPicUpCalibName;
                hoImage = AssembleSuction2.hImageUP;
            }
           

           
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnCheckDownSet_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            if (iCalibPos <= 2)
            {
                strName = AssembleSuction1.strPicDownCalibName;
                hoImage = CommonSet.dic_Assemble1[1].hImageDown;
            }
            else
            {
                strName = AssembleSuction2.strPicDownCalibName;
                hoImage = CommonSet.dic_Assemble2[10].hImageDown;
            }



            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void lblSuction_Click(object sender, EventArgs e)
        {
            string strDo = "组装气缸下降" + iSuction.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblGet_Click(object sender, EventArgs e)
        {
            string strDo = "组装吸真空" + iSuction.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblPut_Click(object sender, EventArgs e)
        {
            string strDo = "组装破真空" + iSuction.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            // mc.setDO(dOut, !mc.dic_DO[dOut]);

            if (CommonSet.bUseAutoParam)
            {
                CommonSet.dOutTest = dOut;
                CommonSet.dOutTestTime = (long)(CommonSet.asm.dic_Solution[1].lstTime[iSuction - 1] * 1000);

                string strD = "组装吸真空" + iSuction.ToString();
                DO dO1 = (DO)Enum.Parse(typeof(DO), strD);
                CommonSet.dOutIn = dO1;
            }
            else
            {
                mc.setDO(dOut, !mc.dic_DO[dOut]);
            }
        }

        private void nudCamUpPosX_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;

            if (iCalibPos == 1)
            {
                AssembleSuction1.pCamBarrelPos1.X =(double) nudCamUpPosX.Value ;
                
            }
            else if(iCalibPos == 2)
            {
                AssembleSuction1.pCamBarrelPos2.X = (double)nudCamUpPosX.Value;


            }
            else if (iCalibPos == 3)
            {
                AssembleSuction2.pCamBarrelPos1.X = (double)nudCamUpPosX.Value;
            }
            else if (iCalibPos == 4)
            {
                AssembleSuction2.pCamBarrelPos2.X = (double)nudCamUpPosX.Value;
            }
        }

        private void nudCamUpPosY_ValueChanged(object sender, EventArgs e)
        {
            if (bInit)
                return;

            if (iCalibPos == 1)
            {
                AssembleSuction1.pCamBarrelPos1.Y = (double)nudCamUpPosY.Value;

            }
            else if (iCalibPos == 2)
            {
                AssembleSuction1.pCamBarrelPos2.Y = (double)nudCamUpPosY.Value;


            }
            else if (iCalibPos == 3)
            {
                AssembleSuction2.pCamBarrelPos1.Y = (double)nudCamUpPosY.Value;
            }
            else if (iCalibPos == 4)
            {
                AssembleSuction2.pCamBarrelPos2.Y = (double)nudCamUpPosY.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), (comboBox1.Text));
            mc.AbsMove(axis, (double)numericUpDown1.Value, (double)20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), (comboBox2.Text));
            mc.AbsMove(axis, (double)numericUpDown2.Value, (double)20);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = 0;
        }

        private void nudCamUpPosZ_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnGoCamUpZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCamUpPosZ.Value, 10);
        }

        private void btnGoCamDownPosZ_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisZ, (double)nudCamDownPosZ.Value, 10);
        }

        private void btnGoMakeUp_Click(object sender, EventArgs e)
        {
            if (iCalibPos == 1)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuction].pAssemblePos1.X+(double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuction].pAssemblePos1.Y + (double)nudCalibMakeUpY.Value, 50);
            }
            else if (iCalibPos == 2)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z1轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble1[iSuction].pAssemblePos2.X + (double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble1[iSuction].pAssemblePos2.Y + (double)nudCalibMakeUpY.Value, 50);
            }
            else if (iCalibPos == 3)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }


                mc.AbsMove(axisX, CommonSet.dic_Assemble2[iSuction].pAssemblePos1.X + (double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble2[iSuction].pAssemblePos1.Y + (double)nudCalibMakeUpY.Value, 50);
            }
            else if (iCalibPos == 4)
            {
                if (mc.dic_Axis[axisZ].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.05))
                {
                    MessageBox.Show("组装Z2轴低于安全位！");
                    return;
                }
                mc.AbsMove(axisX, CommonSet.dic_Assemble2[iSuction].pAssemblePos2.X + (double)nudCalibMakeUpX.Value, 50);
                mc.AbsMove(axisY, CommonSet.dic_Assemble2[iSuction].pAssemblePos2.Y + (double)nudCalibMakeUpY.Value, 50);
            }
        }

        private void nudSuction_ValueChanged(object sender, EventArgs e)
        {
         
            bInit = true;
            iSuction = (int)nudSuction.Value;
            initControl();
            bInit = false;

        }

        private void nudCalibMakeUpX_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudCalibMakeUpY_ValueChanged(object sender, EventArgs e)
        {

        }

      
    }
}
