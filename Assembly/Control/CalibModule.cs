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
    public class CalibModule:ActionModule
    {
        private string strOut = "CalibModule-Action-";
        //private static GetProduct1Module module = null;
      
        public static Point currentPoint = new Point();//标定点位
        
        private int iCurrentNum = 0;//当标定位置
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static List<Point> lstPointXY = new List<Point>();//九点标定对应的轴坐标
        public static List<Point> lstPointRC = new List<Point>();//九点标定对应的像素坐标 X存储Row,Y存储Col
        public static double dDist = 0.5;//移动距离
        public static AXIS axisX;
        public static AXIS axisY;
        public static int iCamPos = 0;//1代表取料1,2代表取料2,3代表组装1,4代表组装2,7代表点胶
        public ImageResult imgResult = new ImageResult();
        public CalibModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._50计算坐标);
            lstAction.Add(ActionName._50XY轴到标定位);
            lstAction.Add(ActionName._50XY轴到位完成);
            lstAction.Add(ActionName._50开始拍照);
            lstAction.Add(ActionName._50拍照完成);
            lstAction.Add(ActionName._50标定完成);
        }

        public override void Reset()
        {
           
        }
        public override void Action2()
        {

        }
        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                { 
                    case ActionName._50计算坐标:
                        iCurrentNum = 0;
                        lstPointXY.Clear();
                        lstPointRC.Clear();
                        double[] dX= new double[]{-dDist,0,dDist};
                        double[] dY = new double[]{-dDist,0,dDist};
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                Point p = new Point();
                                p.X = currentPoint.X + dX[i];
                                p.Y = currentPoint.Y + dY[j];
                                lstPointXY.Add(p);
                            }
                        }

                        step = step + 1;
                        break;
                    case ActionName._50XY轴到标定位:
                        if (iCurrentNum > 8)
                        {
                            step = lstAction.IndexOf(ActionName._50标定完成);
                            break;
                        }
                        dDestPosX = lstPointXY[iCurrentNum].X;
                        dDestPosY = lstPointXY[iCurrentNum].Y;
                        mc.AbsMove(axisX, dDestPosX, 50);
                        mc.AbsMove(axisY, dDestPosY, 50);
                        WriteOutputInfo(strOut + "XY轴到标定位:"+dDestPosX.ToString()+","+dDestPosY.ToString());
                        step = step + 1;
                        break;
                    case ActionName._50XY轴到位完成:
                        if (IsAxisINP(dDestPosX, axisX, 0.01) && IsAxisINP(dDestPosY, axisY, 0.01))
                        {
                            WriteOutputInfo(strOut + "XY轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._50开始拍照:
                        if (!sw.WaitSetTime(1000))
                        {
                            break;
                        }
                        imgResult = new ImageResult();
                        if (iCamPos == 1)
                        {
                            CommonSet.dic_OptSuction1[1].InitImageUp(OptSution1.strPicUpCalibName);
                            CommonSet.camUpA1.SetExposure(OptSution1.dExposureTimeUp);
                            CommonSet.camUpA1.SetGain(OptSution1.dCalibGain);

                            mc.setDO(DO.取料上相机1, true);
                        }
                        else if (iCamPos == 2)
                        {
                            CommonSet.dic_OptSuction2[1].InitImageUp(OptSution2.strPicUpCalibName);
                            CommonSet.camUpA2.SetExposure(OptSution2.dExposureTimeUp);
                            CommonSet.camUpA2.SetGain(OptSution2.dCalibGain);
                            mc.setDO(DO.取料上相机2, true);

                        }
                        else if ((iCamPos == 3) || (iCamPos == 4))
                        {
                            AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
                            CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                            CommonSet.camUpC1.SetGain(AssembleSuction1.dCalibGain);
                            mc.setDO(DO.组装上相机1, true);

                        }
                        else if ((iCamPos == 5) || (iCamPos == 6))
                        {
                            AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCalibName);
                            CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                            CommonSet.camUpC2.SetGain(AssembleSuction2.dCalibGain);
                            mc.setDO(DO.组装上相机2, true);

                        }
                        else if (iCamPos == 7)
                        {
                            BarrelSuction.InitImageUp(BarrelSuction.strPicUpCalibName);
                         
                            CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                            CommonSet.camUpD1.SetGain(BarrelSuction.dCalibGain);
                            mc.setDO(DO.点胶上相机, true);
                        }
                        WriteOutputInfo(strOut + "开始拍照");
                        step = step + 1;
                        break;
                    case ActionName._50拍照完成:
                        if (!sw.WaitSetTime(50))
                            break;

                        if (iCamPos == 1)
                        {
                           // CommonSet.dic_OptSuction1[1].InitImageUp(OptSution1.strPicUpCalibName);
                            mc.setDO(DO.取料上相机1, false);
                            imgResult = CommonSet.dic_OptSuction1[1].imgResultUp;
                        }
                        else if (iCamPos == 2)
                        {
                          //  CommonSet.dic_OptSuction2[1].InitImageUp(OptSution2.strPicUpCalibName);
                            mc.setDO(DO.取料上相机2, false);
                            imgResult = CommonSet.dic_OptSuction2[1].imgResultUp;
                        }
                        else if ((iCamPos == 3) || (iCamPos == 4))
                        {
                           // AssembleSuction1.InitImageUp(AssembleSuction1.strPicUpCalibName);
                            mc.setDO(DO.组装上相机1, false);
                            imgResult = AssembleSuction1.imgResultUp;
                        }
                        else if ((iCamPos == 5) || (iCamPos == 6))
                        {
                           // AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCalibName);
                            mc.setDO(DO.组装上相机2, false);
                            imgResult = AssembleSuction2.imgResultUp;
                        }
                        else if (iCamPos == 7)
                        {
                           // BarrelSuction.InitImageUp(BarrelSuction.strPicUpCalibName);
                            mc.setDO(DO.点胶上相机, false);
                            imgResult = BarrelSuction.imgResultUp;
                        }
                        if (imgResult.bImageResult)
                        {
                            if (imgResult.bStatus)
                            {
                                iCurrentNum = iCurrentNum + 1;
                                Point pRC = new Point();
                                pRC.X = imgResult.CenterRow;
                                pRC.Y = imgResult.CenterColumn;
                                lstPointRC.Add(pRC);
                                WriteOutputInfo(strOut + "拍照完成");
                                step = lstAction.IndexOf(ActionName._50XY轴到标定位);
                            }
                            else
                            {
                                Run.runMode = RunMode.手动;
                                WriteOutputInfo(strOut + "未检测到中心,标定失败!");
                                MessageBox.Show("未检测到中心,标定失败!");
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(2000))
                            {
                                Run.runMode = RunMode.手动;
                                WriteOutputInfo(strOut + "检测超时,标定失败!");
                                MessageBox.Show("检测超时,标定失败!");
                            }
                        }

                        break;
                    case ActionName._50标定完成:
                        Run.runMode = RunMode.手动;
                        IStep = 0;
                        WriteOutputInfo(strOut + "标定完成!");
                        MessageBox.Show("标定完成!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Run.runMode = RunMode.手动;
                WriteOutputInfo(strOut + "像素标定异常"+ex.ToString());
                MessageBox.Show("标定失败!"+ex.ToString());  
               
            }
        }
    }
}
