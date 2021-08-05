using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using HalconDotNet;
using System.Windows.Forms;
using CameraSet;
namespace Assembly
{
    /// <summary>
    /// 上相机对位时，自动对位中心
    /// </summary>
    public class AutoGetCenterPosTestModule:ActionModule
    {
        private string strOut = "AutoGetCenterPosTestModule-Action-";
        //private static GetProduct1Module module = null;

        public static int iSuctionNum = 0;//吸笔序号
     
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static List<Point> lstPointXY = new List<Point>();//九点标定对应的轴坐标
        public static List<Point> lstPointRC = new List<Point>();//九点标定对应的像素坐标 X存储Row,Y存储Col
        public static double dDist = 0.5;//移动距离
        public static AXIS axisX;
        public static AXIS axisY;
        public static AXIS axisZ;
        public static double dSafeZ = 0;//安全位
        public static int iCamPos = 0;//1代表取料1,2代表取料2
        public ImageResult imgResult = new ImageResult();
        private double dRow = 0;
        private double dCol = 0;

        public AutoGetCenterPosTestModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._a0Z轴到安全位);
            lstAction.Add(ActionName._a0Z轴到位完成);
            lstAction.Add(ActionName._a0上相机拍照);
            lstAction.Add(ActionName._a0上相机拍照完成);
            lstAction.Add(ActionName._a0XY轴补正移动);
            lstAction.Add(ActionName._a0XY到位完成);
            lstAction.Add(ActionName._a0对位完成);
         }
        public override void Reset()
        {
            
          
        }

       
        public override void Action(ActionName action, ref int step)
        {
            try
            {
                
                switch (action)
                { 
                    case ActionName._a0Z轴到安全位:
                         dDestPosZ = dSafeZ;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._a0Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisZ))
                        {
                            WriteOutputInfo(strOut + "Z轴到位");
                            step = step + 1;
                        }
                        
                        break;

                    case ActionName._a0XY轴补正移动:
                        dDestPosX = mc.dic_Axis[axisX].dPos -  dRow*0.007;
                        dDestPosY = mc.dic_Axis[axisY].dPos +  dCol*0.007;
                        mc.AbsMove(axisX, dDestPosX, 50);
                        mc.AbsMove(axisY, dDestPosY, 50);
                        WriteOutputInfo(strOut + "XY轴补正移动:" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                        step = step + 1;

                        break;
                    case ActionName._a0XY到位完成:
                        if (IsAxisINP(dDestPosX, axisX, 0.01) && IsAxisINP(dDestPosY, axisY, 0.01))
                        {
                            WriteOutputInfo(strOut + "XY轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._a0上相机拍照:
                        if (!sw.WaitSetTime(1000))
                        {
                            break;
                        }
                        imgResult = new ImageResult();
                        if (iCamPos == 1)
                        {
                            CommonSet.dic_OptSuction1[1].InitImageUp(CommonSet.dic_OptSuction1[iSuctionNum].strPicCameraUpName);
                            CommonSet.camUpA1.SetExposure(OptSution1.dExposureTimeUp);
                            CommonSet.camUpA1.SetGain(OptSution1.dGainUp);

                            mc.setDO(DO.取料上相机1, true);
                        }
                        else if (iCamPos == 2)
                        {
                            CommonSet.dic_OptSuction2[10].InitImageUp(CommonSet.dic_OptSuction2[iSuctionNum].strPicCameraUpName);
                            CommonSet.camUpA2.SetExposure(OptSution2.dExposureTimeUp);
                            CommonSet.camUpA2.SetGain(OptSution2.dGainUp);
                            mc.setDO(DO.取料上相机2, true);

                        }
                        WriteOutputInfo(strOut + "开始拍照");
                        step = step + 1;
                        break;
                    case ActionName._a0上相机拍照完成:
                        if (!sw.WaitSetTime(50))
                            break;
                        HTuple width= new HTuple(), height = new HTuple();
                        if (iCamPos == 1)
                        {
                           // CommonSet.dic_OptSuction1[1].InitImageUp(OptSution1.strPicUpCalibName);
                            mc.setDO(DO.取料上相机1, false);
                            imgResult = CommonSet.dic_OptSuction1[iSuctionNum].imgResultUp;
                            HOperatorSet.GetImageSize(CommonSet.dic_OptSuction1[iSuctionNum].hImageUP, out width, out height);
                            
                        }
                        else if (iCamPos == 2)
                        {
                          //  CommonSet.dic_OptSuction2[1].InitImageUp(OptSution2.strPicUpCalibName);
                            mc.setDO(DO.取料上相机2, false);
                            imgResult = CommonSet.dic_OptSuction2[iSuctionNum].imgResultUp;
                            HOperatorSet.GetImageSize(CommonSet.dic_OptSuction2[iSuctionNum].hImageUP, out width, out height);
                        }
                       
                        if (imgResult.bImageResult)
                        {
                            if (imgResult.bStatus)
                            {

                                dRow = imgResult.CenterRow - height.D / 2;
                                dCol = imgResult.CenterColumn - width.D / 2;
                            
                                 if((Math.Abs(dRow)<0.5)&&(Math.Abs(dCol)<0.5))
                                 {
                                      WriteOutputInfo(strOut + "拍照完成");
                                    step = lstAction.IndexOf(ActionName._a0对位完成);
                                    
                                 }else{
                                    WriteOutputInfo(strOut + "拍照完成");
                                    step = lstAction.IndexOf(ActionName._a0XY轴补正移动);
                                 }
                            }
                            else
                            {
                                Run.runMode = RunMode.手动;
                                WriteOutputInfo(strOut + "未检测到中心,中心对位失败!");
                                MessageBox.Show("未检测到中心,中心对位失败!");
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(2000))
                            {
                                Run.runMode = RunMode.手动;
                                WriteOutputInfo(strOut + "检测超时,中心对位失败!");
                                MessageBox.Show("检测超时,中心对位失败!");
                            }
                        }

                        break;
                    case ActionName._a0对位完成:
                        if ((Math.Abs(dRow) < 0.5) && (Math.Abs(dCol) < 0.5))
                        {
                            Run.runMode = RunMode.手动;
                            IStep = 0;
                            WriteOutputInfo(strOut + "对位完成!");
                            MessageBox.Show("对位完成!");
                        }
                        else
                        {
                             step =  lstAction.IndexOf(ActionName._a0上相机拍照);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Run.runMode = RunMode.手动;
                WriteOutputInfo(strOut + "中心对位异常"+ex.ToString());
                MessageBox.Show("中心对位异常!" + ex.ToString());  
               
            }
        
        }

        public override void Action2()
        {
           
        }
    }
}
