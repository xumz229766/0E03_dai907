using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using HalconDotNet;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    public class ResultTestModule:ActionModule
    {
        private string strOut = "ResultTestModule-Action-";
        // private static Assem1Module module = null;
        public static Point currentPoint = new Point();
        //public static List<int> lstNGSuction = new List<int>();//飞拍NG的图片
        // private Point currentPoint = null;
        //private int iCurrentTimes = 1;
        //private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static int iCurrentSuctionOrder = 0;//当前吸笔序号
        public static int iCurrentStation = 1;//当前组装工位
        private string strD = "";
        private DO dOut;
        private DI dIn;
        public static AXIS axisX, axisY, axisZ;//组装轴
        public static AXIS axisGetX, axisGetY, axisGetZ;//取料轴

        public static double dPutZ = 0;//Z轴验证高度
        public static int iPos = 0;
        public static double dRow = 0;
        public static double dCol = 0;
        public static bool bUseAssembleParam = false;//使用组装参数
        public static bool bUseOptOnly = true;//只用取料部分
        public static bool bPutBack = false;//取料后是否放回原盘
      
        public static double dDiffOptX = 0;
        public static double dDiffOptY = 0;
        public static double dDiffAssemX = 0;
        public static double dDiffAssemY = 0;
        public static double dPosX = 0;
        public static double dPosY = 0;

        public ResultTestModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到待机位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30X轴到飞拍结束位);
            lstAction.Add(ActionName._30X轴到位);

            //取料
            lstAction.Add(ActionName._100气缸上升);
            lstAction.Add(ActionName._100XY到取料位);
            lstAction.Add(ActionName._100XY到位);
            lstAction.Add(ActionName._100Z轴到取料高度);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100气缸下降);
            lstAction.Add(ActionName._100吸笔真空);
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);

            lstAction.Add(ActionName._20X轴到下相机拍照位);
            lstAction.Add(ActionName._20X到位完成);
            lstAction.Add(ActionName._30吸笔停留时间);
            lstAction.Add(ActionName._20开始飞拍);
            lstAction.Add(ActionName._20飞拍结束);

            //只使用取料的情况下放料到托盘
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100XY到取料位);
            lstAction.Add(ActionName._100XY到位);
            lstAction.Add(ActionName._100气缸下降);
            lstAction.Add(ActionName._100Z轴到取料高度);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100吸笔破真空);
            lstAction.Add(ActionName._100气缸上升);
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100放料完成);
           

            //放到中转位料
            lstAction.Add(ActionName._20X轴到飞拍结束位);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._20X到位完成);
            lstAction.Add(ActionName._20Z轴到放料位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._20中转位取放料);
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._20X轴到飞拍起始位);
            lstAction.Add(ActionName._20X到位完成);

            //组装取料
            lstAction.Add(ActionName._30X轴到取料位);
            lstAction.Add(ActionName._30吸笔全部上升);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30吸笔下降);
            lstAction.Add(ActionName._30Z轴到取料位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30吸笔全部吸真空);
            lstAction.Add(ActionName._30Z轴到求心位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30求心闭合);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._30求心闭合);
            lstAction.Add(ActionName._30求心张开);
            //lstAction.Add(ActionName._30求心张开检测);

            //组装
            lstAction.Add(ActionName._30Z轴到待机位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Y轴到镜筒拍照位);
            lstAction.Add(ActionName._30Y轴到位完成);
            lstAction.Add(ActionName._30X轴到取料位);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30吸笔下降判定);
            lstAction.Add(ActionName._30开始飞拍);
            lstAction.Add(ActionName._30X轴到飞拍结束位);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30飞拍完成);
           // lstAction.Add(ActionName._30吸笔上升);
            lstAction.Add(ActionName._30XY轴到组装位);
            lstAction.Add(ActionName._30XY轴到位完成);
            lstAction.Add(ActionName._30Z轴到组装位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30吸笔破真空);
            lstAction.Add(ActionName._30吸笔停留时间);
            lstAction.Add(ActionName._30Z轴到组装上升位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到待机位);
            lstAction.Add(ActionName._30Z轴到位完成);
           // lstAction.Add(ActionName._30吸笔上升);
            lstAction.Add(ActionName._30XY到镜筒拍照位);
            lstAction.Add(ActionName._30XY轴到位完成);
            lstAction.Add(ActionName._30镜筒固定块拍照);
            lstAction.Add(ActionName._30镜筒拍照完成);
            //lstAction.Add(ActionName._30镜筒标定块拍照);
            //lstAction.Add(ActionName._30镜筒拍照完成);

            lstAction.Add(ActionName._30验证完成);
           
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
                    #region"取料"
                    case ActionName._100XY到取料位:
                        dDestPosX = currentPoint.X;
                        dDestPosY = currentPoint.Y;
                        mc.AbsMove(axisGetX, dDestPosX, (int)100);
                        mc.AbsMove(axisGetY, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "XY到取料位:(" + dDestPosX.ToString() + "," + dDestPosY.ToString() + ")");
                        step = step + 1;
                        break;

                    case ActionName._100XY到位:
                        if (IsAxisINP(dDestPosX, axisGetX) && IsAxisINP(dDestPosY, axisGetY))
                        {
                            WriteOutputInfo(strOut + "XY到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100Z轴到安全位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosZ = OptSution1.pSafeXYZ.Z;

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            dDestPosZ = OptSution2.pSafeXYZ.Z;
                        }
                       
                        else
                        {
                            dDestPosZ = 0;
                        }
                        mc.AbsMove(axisGetZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位:(" + dDestPosZ.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到放料位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosZ = OptSution1.pPutPos.Z;

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            dDestPosZ = OptSution2.pPutPos.Z;
                        }
                       
                        mc.AbsMove(axisGetZ, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z轴到放料位：" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                 

                   
                
                    case ActionName._100Z轴到取料高度:

                        dDestPosZ = currentPoint.Z;
                        mc.AbsMove(axisGetZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到取料高度:(" + dDestPosZ.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._100Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisGetZ, 0.1))
                        {
                            WriteOutputInfo(strOut + "Z轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100气缸上升:
                        if (axisX == AXIS.组装X1轴)
                        {
                            OptPutSuction1(false);

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            OptPutSuction2(false);
                        }
                       
                        WriteOutputInfo(strOut + "取料吸笔上升");
                        step = step + 1;

                        break;

                    case ActionName._100气缸下降:
                        strD = "取料气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        if (sw.WaitSetTime(1000))
                        {
                            WriteOutputInfo(strOut + "取料气缸" + iCurrentSuctionOrder.ToString() + "下降");
                            step = step + 1;
                        }
                        break;
                  
                    case ActionName._100吸笔真空:
                        strD = "取料吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        long ltime = (long)(0.2 * 1000);
                        if (sw.WaitSetTime(ltime))
                        {
                            WriteOutputInfo(strOut + dOut.ToString() + "真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100吸笔破真空:
                        strD = "取料吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        strD = "取料破真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        long ltime1 = (long)(0.1 * 1000);
                        if (sw.WaitSetTime(ltime1))
                        {
                            mc.setDO(dOut, false);
                          
                            WriteOutputInfo(strOut + dOut.ToString() + "破真空");
                            step = step + 1;
                        }
                        break;

                    case ActionName._20开始飞拍:
                        if (axisX == AXIS.组装X1轴)
                        {
                            string strProcessName = CommonSet.dic_OptSuction1[iCurrentSuctionOrder].strPicDownProduct;
                            CommonSet.dic_OptSuction1[iCurrentSuctionOrder].InitImageDown(strProcessName);
                            mc.setDO(DO.取料下相机1, true);
                        }
                        else
                        {
                            string strProcessName = CommonSet.dic_OptSuction2[iCurrentSuctionOrder].strPicDownProduct;
                            CommonSet.dic_OptSuction2[iCurrentSuctionOrder].InitImageDown(strProcessName);
                            //上相机的比较触发值存入最后一个
                            mc.setDO(DO.取料下相机2, true);
                        }

                        WriteOutputInfo(strOut + "开始飞拍");
                        step = step + 1;
                        break;
                    case ActionName._20飞拍结束:
                        //if (!sw.WaitSetTime(50))
                        //    break;

                        if (sw.WaitSetTime(200))
                        {

                            bool bResult = false;
                            if (axisX == AXIS.组装X1轴)
                            {
                                mc.setDO(DO.取料下相机1, false);
                                bResult = CommonSet.dic_OptSuction1[iCurrentSuctionOrder].imgResultDown.bImageResult;
                                if (CommonSet.dic_OptSuction1[iCurrentSuctionOrder].imgResultDown.bImageResult)
                                {
                                    dRow = CommonSet.dic_OptSuction1[iCurrentSuctionOrder].imgResultDown.CenterRow;
                                    dCol = CommonSet.dic_OptSuction1[iCurrentSuctionOrder].imgResultDown.CenterColumn;
                                    dDiffOptX= OptSution1.GetDownResulotion() * (CommonSet.dic_OptSuction1[iCurrentSuctionOrder].pSuctionCenter.X - dRow);
                                    dDiffOptY = OptSution1.GetDownResulotion() * (CommonSet.dic_OptSuction1[iCurrentSuctionOrder].pSuctionCenter.Y - dCol);
                                }

                            }
                            else
                            {
                                mc.setDO(DO.取料下相机2, false);
                                bResult = CommonSet.dic_OptSuction2[iCurrentSuctionOrder].imgResultDown.bImageResult;
                                if (CommonSet.dic_OptSuction2[iCurrentSuctionOrder].imgResultDown.bImageResult)
                                {
                                    dRow = CommonSet.dic_OptSuction2[iCurrentSuctionOrder].imgResultDown.CenterRow;
                                    dCol = CommonSet.dic_OptSuction2[iCurrentSuctionOrder].imgResultDown.CenterColumn;
                                    dDiffOptX = OptSution2.GetDownResulotion() * (CommonSet.dic_OptSuction2[iCurrentSuctionOrder].pSuctionCenter.X - dRow);
                                    dDiffOptY = OptSution2.GetDownResulotion() * (CommonSet.dic_OptSuction2[iCurrentSuctionOrder].pSuctionCenter.Y - dCol);
                                }

                            }
                            //如果只使用取料
                            if (bUseOptOnly)
                            {
                                double x = currentPoint.X + dDiffOptX;
                                double y = currentPoint.Y - dDiffOptY;
                                dPosX = x;
                                dPosY = y;
                                if (bPutBack)
                                {
                                    step = step + 1;
                                    WriteOutputInfo(strOut + "拍照完成");
                                }
                                else
                                {
                                    Run.runMode = RunMode.手动;
                                    step = 0;
                                }
                                //System.Windows.Forms.MessageBox.Show("校正后坐标(X,Y)：("+x.ToString("0.000")+","+y.ToString("0.000")+")");
                                break;
                            }
                            if (bResult)
                            {

                                step = lstAction.IndexOf(ActionName._100放料完成) + 1;
                                WriteOutputInfo(strOut + "拍照完成");
                            }
                            else
                            {

                                System.Windows.Forms.MessageBox.Show("图像检测NG或超时!");
                                Run.runMode = RunMode.手动;
                                step = 0;
                            }
                        }
                        break;
                    case ActionName._100放料完成:

                        Run.runMode = RunMode.手动;
                        step = 0;
                        WriteOutputInfo(strOut + "放料完成");
                        break;
                    case ActionName._20X轴到飞拍起始位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosX = OptSution1.dFlashStartX;

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            dDestPosX = OptSution2.dFlashStartX;
                        }
                        mc.AbsMove(axisGetX, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "取料X轴到飞拍起始位");
                        step = step + 1;

                        break;
                    case ActionName._20X轴到下相机拍照位:
                         if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosX = CommonSet.dic_OptSuction1[iCurrentSuctionOrder].DPosCameraDownX;
                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            dDestPosX = CommonSet.dic_OptSuction2[iCurrentSuctionOrder].DPosCameraDownX;
                        }

                        mc.AbsMove(axisGetX, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "取料X轴到下相机拍照位");
                        step = step + 1;
                        break;
                    case ActionName._20X轴到飞拍结束位:

                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosX = OptSution1.pPutPos.X;

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            dDestPosX = OptSution2.pPutPos.X;
                        }

                        mc.AbsMove(axisGetX, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "取料X轴到飞拍结束位");
                        step = step + 1;

                        break;
                 
                    case ActionName._20X到位完成:
                        if (IsAxisINP(dDestPosX, axisGetX))
                        {
                            WriteOutputInfo(strOut + "取料X轴到位完成");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20中转位取放料:
                        if (axisX == AXIS.组装X1轴)
                        {
                            mc.setDO(DO.中转吸真空气源1, true);

                        }
                        else if (axisX == AXIS.组装X2轴)
                        {
                            mc.setDO(DO.中转吸真空气源2, true);
                        }
                        strD = "取料吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        strD = "取料破真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        strD = "中转吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);

                        strD = "中转真空检测" + iCurrentSuctionOrder.ToString();
                        dIn = (DI)Enum.Parse(typeof(DI), strD);       
                        long lPutTime = (long)(0.5*1000);
                        if (sw.WaitSetTime(lPutTime))
                        {        
                            strD = "取料破真空" + iCurrentSuctionOrder.ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);
                            WriteOutputInfo(strOut + "中转位取放料");
                            step = step + 1;
                        }

                       break;
                    #endregion
                    #region"组装取料"
                    case ActionName._30求心张开:
                       if (axisX == AXIS.组装X1轴)
                       {
                           mc.setDO(DO.中转1夹紧, false);

                       }
                       else if (axisX == AXIS.组装X2轴)
                       {
                           mc.setDO(DO.中转2夹紧, false);
                       }
                      
                       if (sw.WaitSetTime(500))
                       {
                           WriteOutputInfo(strOut + "求心张开");
                           step = step + 1;
                       }
                       break;
                    case ActionName._30求心闭合:
                       if (axisX == AXIS.组装X1轴)
                       {
                           mc.setDO(DO.中转1夹紧, true);

                       }
                       else if (axisX == AXIS.组装X2轴)
                       {
                           mc.setDO(DO.中转2夹紧, true);
                       }

                       if (sw.WaitSetTime(500))
                       {
                           WriteOutputInfo(strOut + "求心闭合");
                           step = step + 1;
                       }
                       break;
                    case ActionName._30吸笔下降:
                        strD = "组装气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "下降");
                        step = step + 1;
                       
                      
                       break;

                    case ActionName._30Z轴到取料位:
                       if (axisX == AXIS.组装X1轴)
                       {
                           dDestPosZ = AssembleSuction1.DGetPosZ;

                       }
                       else if (axisX == AXIS.组装X2轴)
                       {
                           dDestPosZ = AssembleSuction2.DGetPosZ;
                       }
                       
                       mc.AbsMove(axisZ, dDestPosZ, (int)CommonSet.dVelRunZ);
                       WriteOutputInfo(strOut + "组装Z轴到取料位");
                       step = step + 1;
                       break;
                    case ActionName._30吸笔全部吸真空:
                       if (axisX == AXIS.组装X1轴)
                       {
                           mc.setDO(DO.中转吸真空气源1, false);

                       }
                       else if (axisX == AXIS.组装X2轴)
                       {
                           mc.setDO(DO.中转吸真空气源2, false);
                       }

                       if (iCurrentSuctionOrder == 1)
                       {
                           if (iCurrentStation == 1)
                           {
                               mc.setDO(DO.组装位1真空吸, false);
                               mc.setDO(DO.组装位1真空破, false);
                           }
                           else
                           {
                               mc.setDO(DO.组装位2真空吸, false);
                               mc.setDO(DO.组装位2真空破, false);
                           }
                       }

                       strD = "组装吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        // mc.setDO(DO.组装吸嘴1破真空 + iCurrentSuctionOrder - 1, true);
                        strD = "组装破真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        
                        if (sw.WaitSetTime(500))
                        {
                            //    mc.setDO(dOut, false);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "吸真空");
                            step = step + 1;
                        }
                       break;
                    case ActionName._30Z轴到求心位:
                       if (axisX == AXIS.组装X1轴)
                       {
                           dDestPosZ = AssembleSuction1.DCorrectPosZ;

                       }
                       else if (axisX == AXIS.组装X2轴)
                       {
                           dDestPosZ = AssembleSuction2.DCorrectPosZ;
                       }
                      
                       mc.AbsMove(axisZ, dDestPosZ, (int)CommonSet.dVelRunZ);
                       WriteOutputInfo(strOut + "组装Z轴到取料位");
                       step = step + 1;
                       break;

                    #endregion
                    #region "组装验证"
                    case ActionName._30吸笔上升:
                        strD = "组装气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "上升");
                        step = step + 1;
                        break;
                    case ActionName._30吸笔全部上升:
                        if (axisX == AXIS.组装X1轴)
                        {
                            AssemblePutSuction1(false);
                        }
                        else
                        {
                            AssemblePutSuction2(false);
                        }
                        step = step + 1;

                        break;

                    case ActionName._30吸笔下降判定:
                        strD = "组装气缸下降" + (iCurrentSuctionOrder).ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        if (sw.WaitSetTime(500))
                        {
                            step = step + 1;
                        }

                        break;
                    case ActionName._30吸笔破真空:


                        strD = "组装吸真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        // mc.setDO(DO.组装吸嘴1破真空 + iCurrentSuctionOrder - 1, true);
                        strD = "组装破真空" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                       
                        if (sw.WaitSetTime(50))
                        {
                            mc.setDO(dOut, false);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "破真空");
                            if (iCurrentSuctionOrder == 1)
                            {
                                if (iCurrentStation == 1)
                                {
                                    mc.setDO(DO.组装位1真空吸, true);
                                    mc.setDO(DO.组装位1真空破, false);
                                }
                                else
                                {
                                    mc.setDO(DO.组装位2真空吸, true);
                                    mc.setDO(DO.组装位2真空破, false);
                                }
                            }

                            step = step + 1;
                        }
                        break;
                    case ActionName._30吸笔停留时间:
                        if (sw.WaitSetTime(200))
                        {
                            if (bUseAssembleParam)
                            {
                                if (axisX == AXIS.组装X1轴)
                                {
                                    //获取吸笔高度和压力值
                                    if (iCurrentStation == 1)
                                    {
                                        ParamListerner.station1.initData();
                                        ParamListerner.station1.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation1;
                                        ParamListerner.station1.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation1;
                                        ParamListerner.station1.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z1轴].dPos;
                                    }
                                    else
                                    {
                                        ParamListerner.station2.initData();
                                        ParamListerner.station2.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation2;
                                        ParamListerner.station2.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation2;
                                        ParamListerner.station2.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z1轴].dPos;
                                    }
                                }
                                else
                                {
                                    //获取吸笔高度和压力值
                                    if (iCurrentStation == 1)
                                    {
                                        ParamListerner.station1.initData();
                                        ParamListerner.station1.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation1;
                                        ParamListerner.station1.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation1;
                                        ParamListerner.station1.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                                    }
                                    else
                                    {
                                        ParamListerner.station2.initData();
                                        ParamListerner.station2.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation2;
                                        ParamListerner.station2.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation2;
                                        ParamListerner.station2.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                                    }
                                }
                            }
                            step = step + 1;
                        }
                        break;
                    case ActionName._30X轴到取料位:
                        if (bUseAssembleParam)
                        {
                            if (axisX == AXIS.组装X1轴)
                            {
                                if (iCurrentStation == 1)
                                {

                                    //mc.setDO(DO.组装位1夹紧, true);
                                }
                                else
                                {
                                    //mc.setDO(DO.组装位2夹紧, true);
                                }
                            }
                            else
                            {
                                if (iCurrentStation == 1)
                                {
                                   // mc.setDO(DO.组装位1夹紧, true);
                                }
                                else
                                {
                                   // mc.setDO(DO.组装位2夹紧, true);
                                }
                            }
                        }
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosX = AssembleSuction1.dFlashStartX;
                        }
                        else
                        {
                            dDestPosX = AssembleSuction2.dFlashStartX;
                        }
                        mc.AbsMove(axisX, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "组装X轴到取料位");
                        step = step + 1;
                        break;
                    case ActionName._30X轴到飞拍结束位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosX = AssembleSuction1.dFlashEndX;
                        }
                        else
                        {
                            dDestPosX = AssembleSuction2.dFlashEndX;
                        }
                    
                        mc.AbsMove(axisX, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "组装X轴到飞拍结束位");
                        step = step + 1;
                        break;
                    case ActionName._30X轴到位:
                        if (IsAxisINP(dDestPosX, axisX))
                        {
                            WriteOutputInfo(strOut + "组装X轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30Z轴到组装上升位:
                        double v1 = 5;
                        if (axisX == AXIS.组装X1轴)
                        {
                            //dDestPosZ = AssembleHeightRelation.lstRelationAssem1Station1[iCurrentSuctionOrder - 1].PosZ - CommonSet.dic_Assemble1[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble1[iCurrentSuctionOrder].dDist2;
                            if (iCurrentStation == 1)
                            {
                                v1 = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                dDestPosZ = CommonSet.dic_Assemble1[iCurrentSuctionOrder].dPreH1;
                            }
                            else
                            {
                                v1 = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                dDestPosZ = CommonSet.dic_Assemble1[iCurrentSuctionOrder].dPreH2;
                            }
                            mc.AbsMove(AXIS.组装Z1轴, dDestPosZ, (int)v1);
                            WriteOutputInfo(strOut + "组装Z1轴到组装上升位" + dDestPosZ.ToString());
                        }
                        else 
                        {

                            //dDestPosZ = AssembleHeightRelation.lstRelationAssem1Station2[iCurrentSuctionOrder - 1].PosZ - CommonSet.dic_Assemble1[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble1[iCurrentSuctionOrder].dDist2;
                           // dDestPosZ = CommonSet.dic_Assemble1[iCurrentSuctionOrder].dPreH2;
                            if (iCurrentStation == 1)
                            {
                                v1 = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH1;
                            }
                            else
                            {
                                v1 = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH2;
                            }

                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)v1);
                            WriteOutputInfo(strOut + "组装Z2轴到组装上升位" + dDestPosZ.ToString());
                        }
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到待机位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            dDestPosZ = AssembleSuction1.pSafeXYZ.Z;
                        }
                        else
                        {
                            dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                        }
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "组装Z轴到安全位");
                        step = step + 1;
                        break;

                    case ActionName._30Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisZ))
                        {
                            WriteOutputInfo(strOut + "组装Z轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30Z轴到组装位:
                         int v = 10;//下降速度
                        if (bUseAssembleParam)
                        {
                            if (axisX == AXIS.组装X1轴)
                            {
                                float dOutv = AssemblePressureRelation.GetVBySuction1(iCurrentSuctionOrder, CommonSet.asm.dic_Solution[1].lstPressure[iCurrentSuctionOrder - 1]);
                                // float dOutv = CommonSet.fCommonPressureV1;
                                mc.WriteOutDA(dOutv, 0);
                                v = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                if (iCurrentStation == 1)
                                {
                                    //dDestPosZ = AssembleHeightRelation.lstRelationAssem1Station1[iCurrentSuctionOrder - 1].PosZ - CommonSet.asm.dic_Solution[1].lstHieght[iCurrentSuctionOrder - 1];
                                    dDestPosZ = CommonSet.dic_Assemble1[iCurrentSuctionOrder].dAssembleH + CommonSet.asm.dic_Solution[1].lstHeight2[iCurrentSuctionOrder - 1];
                                }
                                else
                                {
                                    dDestPosZ = CommonSet.dic_Assemble1[iCurrentSuctionOrder].dAssembleH2 + CommonSet.asm.dic_Solution[1].lstHeight2[iCurrentSuctionOrder - 1];
                                    //dDestPosZ = AssembleHeightRelation.lstRelationAssem1Station2[iCurrentSuctionOrder - 1].PosZ - CommonSet.asm.dic_Solution[1].lstHieght[iCurrentSuctionOrder - 1];

                                }
                            }
                            else
                            {
                                float dOutv = AssemblePressureRelation.GetVBySuction2(iCurrentSuctionOrder, CommonSet.asm.dic_Solution[1].lstPressure[iCurrentSuctionOrder - 1]);
                                //float dOutv = CommonSet.fCommonPressureV2;
                                mc.WriteOutDA(dOutv, 2);
                                v = (int)CommonSet.asm.dic_Solution[1].lstVel[iCurrentSuctionOrder - 1];
                                if (iCurrentStation == 1)
                                {
                                   // dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - CommonSet.asm.dic_Solution[1].lstHieght[iCurrentSuctionOrder - 1];
                                    dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH + CommonSet.asm.dic_Solution[1].lstHeight2[iCurrentSuctionOrder - 1];
                                }
                                else
                                {
                                    //dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - CommonSet.asm.dic_Solution[1].lstHieght[iCurrentSuctionOrder - 1];
                                    dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH2 + CommonSet.asm.dic_Solution[1].lstHeight2[iCurrentSuctionOrder - 1];
                                }
                                
                            }
                        }
                        else
                        {
                            dDestPosZ = dPutZ;

                        }
                        mc.AbsMove(axisZ, dDestPosZ, (int)v);
                        WriteOutputInfo(strOut + "组装Z轴到验证高度位："+dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._30XY到镜筒拍照位:

                        if (axisX == AXIS.组装X1轴)
                        {
                            if (iCurrentStation == 1)
                            {
                                dDestPosX = AssembleSuction1.pCamBarrelPos1.X;
                                dDestPosY = AssembleSuction1.pCamBarrelPos1.Y;

                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());

                            }
                            else
                            {
                                dDestPosX = AssembleSuction1.pCamBarrelPos2.X;
                                dDestPosY = AssembleSuction1.pCamBarrelPos2.Y;

                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());

                            }
                        }
                        else
                        {
                            if (iCurrentStation == 1)
                            {
                                dDestPosX = AssembleSuction2.pCamBarrelPos1.X;
                                dDestPosY = AssembleSuction2.pCamBarrelPos1.Y;

                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());

                            }
                            else
                            {
                                dDestPosX = AssembleSuction2.pCamBarrelPos2.X;
                                dDestPosY = AssembleSuction2.pCamBarrelPos2.Y;
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());

                            }
                        }
                       
                        mc.AbsMove(axisX, dDestPosX, (int)CommonSet.dVelRunX);
                        mc.AbsMove(axisY, dDestPosY, (int)CommonSet.dVelRunY);
                        step = step + 1;
                        break;
                    case ActionName._30Y轴到镜筒拍照位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            if (iCurrentStation == 1)
                                dDestPosY = AssembleSuction1.pCamBarrelPos1.Y;
                            else
                            {
                                dDestPosY = AssembleSuction1.pCamBarrelPos2.Y;
                            }
                        }
                        else
                        {
                            if (iCurrentStation == 1)
                                dDestPosY = AssembleSuction2.pCamBarrelPos1.Y;
                            else
                            {
                                dDestPosY = AssembleSuction2.pCamBarrelPos2.Y;
                            }
                        }
                        mc.AbsMove(axisY, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "Y到镜筒拍照位");
                        step = step + 1;
                        break;
                    case ActionName._30Y轴到位完成:
                        if (IsAxisINP(dDestPosY, axisY, 0.02))
                        {
                            WriteOutputInfo(strOut + "Y轴到位");
                            step = step + 1;
                        }

                        break;
                    case ActionName._30XY轴到位完成:
                        if (IsAxisINP(dDestPosX, axisX, 0.02)&&IsAxisINP(dDestPosY, axisY, 0.02))
                        {
                            WriteOutputInfo(strOut + "XY轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30开始飞拍:
                           iPos = 1;
                           List<double> lstFlashData = new List<double>();
                           
                            int index = iCurrentSuctionOrder;
                            if (axisX == AXIS.组装X1轴)
                            {
                                string strProcessUpName = AssembleSuction1.strResultLenName;
                                AssembleSuction1.InitImageUp(strProcessUpName);
                                double pos = ((CommonSet.dic_Assemble1[index].DPosCameraDownX + AssembleSuction1.dFalshMakeUpX));
                                lstFlashData.Add(pos);
                                string strProcessName = CommonSet.dic_Assemble1[index].strPicDownProduct;
                                CommonSet.dic_Assemble1[index].InitImageDown(strProcessName);
                                //上相机的比较触发值存入最后一个
                                if (iCurrentStation == 1)
                                    lstFlashData.Add(AssembleSuction1.pCamBarrelPos1.X + AssembleSuction1.dFalshMakeUpX);
                                else
                                    lstFlashData.Add(AssembleSuction1.pCamBarrelPos2.X + AssembleSuction1.dFalshMakeUpX);
                                mc.startCmpTrigger2(axisX, lstFlashData.ToArray(), (ushort)0, (ushort)1);
                                CommonSet.camDownC1.SetGain(AssembleSuction1.dGainDown);
                                CommonSet.camDownC1.SetExposure(AssembleSuction1.dExposureTimeDown);

                                CommonSet.camUpC1.SetGain(AssembleSuction1.dResultTestGain);
                                CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                            }
                            else
                            {
                                string strProcessUpName = AssembleSuction2.strResultLenName;
                                AssembleSuction2.InitImageUp(strProcessUpName);
                                double pos = ((CommonSet.dic_Assemble2[index].DPosCameraDownX + AssembleSuction2.dFalshMakeUpX));
                                lstFlashData.Add(pos);

                                string strProcessName = CommonSet.dic_Assemble2[index].strPicDownProduct;
                                CommonSet.dic_Assemble2[index].InitImageDown(strProcessName);
                                //上相机的比较触发值存入最后一个
                                if (iCurrentStation == 1)
                                    lstFlashData.Add(AssembleSuction2.pCamBarrelPos1.X + AssembleSuction2.dFalshMakeUpX);
                                else
                                    lstFlashData.Add(AssembleSuction2.pCamBarrelPos2.X + AssembleSuction2.dFalshMakeUpX);
                                mc.startCmpTrigger2(axisX, lstFlashData.ToArray(), (ushort)2, (ushort)3);
                                CommonSet.camDownC2.SetGain(AssembleSuction2.dGainDown);
                                CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);

                                CommonSet.camUpC2.SetGain(AssembleSuction2.dResultTestGain);
                                CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                            }
                      
                        WriteOutputInfo(strOut + "开始飞拍");
                        
                        step = step + 1;
                        break;
                    case ActionName._30飞拍完成:
                        if (sw.WaitSetTime(100))
                        {
                            mc.stopCmpTrigger(AXIS.组装X1轴, 0, 1);
                            mc.stopCmpTrigger(AXIS.组装X2轴, 2, 3);
                            bool bResult = false;
                            if (axisX == AXIS.组装X1轴)
                            {
                                bResult = AssembleSuction1.imgResultUp.bImageResult && CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.bImageResult;
                                if (bResult)
                                {
                                    dRow = CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterRow;
                                    dCol = CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterColumn;
                                    dDiffAssemX = AssembleSuction1.GetDownResulotion() * (dRow - CommonSet.dic_Assemble1[iCurrentSuctionOrder].pSuctionCenter.X);
                                    dDiffAssemY = AssembleSuction1.GetDownResulotion() * (dCol - CommonSet.dic_Assemble1[iCurrentSuctionOrder].pSuctionCenter.Y);

                                    WriteResult(CommonSet.strReportPath + "物料偏移吸笔中心数据\\", "");
                                }


                            }
                            else
                            {
                                bResult = AssembleSuction2.imgResultUp.bImageResult && CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.bImageResult;
                                if (bResult)
                                {
                                    dRow = CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow;
                                    dCol = CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn;
                                    dDiffAssemX = AssembleSuction2.GetDownResulotion() * (dRow - CommonSet.dic_Assemble2[iCurrentSuctionOrder].pSuctionCenter.X);
                                    dDiffAssemY = AssembleSuction2.GetDownResulotion() * (dCol - CommonSet.dic_Assemble2[iCurrentSuctionOrder].pSuctionCenter.Y);

                                    WriteResult(CommonSet.strReportPath + "物料偏移吸笔中心数据\\", "");
                                }
                            }
                            if (bResult)
                            {
                                if (bUseAssembleParam)
                                {
                                    step = step + 1;

                                }
                                else
                                {
                                    strD = "组装气缸下降" + iCurrentSuctionOrder.ToString();
                                    dOut = (DO)Enum.Parse(typeof(DO), strD);
                                    mc.setDO(dOut, false);
                                    Run.runMode = RunMode.手动;
                                    step = 0;
                                }
                                WriteOutputInfo(strOut + "飞拍完成");
                                break;
                            }
                            else
                            {

                                System.Windows.Forms.MessageBox.Show("图像检测NG或超时!");
                                Run.runMode = RunMode.手动;
                                step = 0;
                            }
                            
                        }
                        break;
                    case ActionName._30镜筒标定块拍照:
                        iPos = 0;
                        if (axisX == AXIS.组装X1轴)
                        {
                            AssembleSuction1.InitImageUp(AssembleSuction1.strResultSuctionName);
                            //AssembleSuction1.InitImageUp(AssembleSuction1.strResultSuctionName);
                            CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                            CommonSet.camUpC1.SetGain(AssembleSuction1.dResultTestGain);
                            mc.setDO(DO.组装上相机1, true);
                            WriteOutputInfo(strOut + "镜筒标定块拍照");
                        }
                        else 
                        {
                            AssembleSuction2.InitImageUp(AssembleSuction2.strResultSuctionName);
                            CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                            CommonSet.camUpC2.SetGain(AssembleSuction2.dResultTestGain);
                            mc.setDO(DO.组装上相机2, true);
                            WriteOutputInfo(strOut + "镜筒标定块拍照");
                        }
                        step = step + 1;
                        break;
                    case ActionName._30镜筒固定块拍照:
                        iPos = 1;
                        if (axisX == AXIS.组装X1轴)
                        {
                            AssembleSuction1.InitImageUp(AssembleSuction1.strResultLenName);
                            //AssembleSuction1.InitImageUp(AssembleSuction1.strResultSuctionName);
                            CommonSet.camUpC1.SetExposure(AssembleSuction1.dExposureTimeUp);
                            CommonSet.camUpC1.SetGain(AssembleSuction1.dResultTestGain);
                            mc.setDO(DO.组装上相机1, true);
                            WriteOutputInfo(strOut + "镜筒固定块拍照");
                        }
                        else
                        {
                            AssembleSuction2.InitImageUp(AssembleSuction2.strResultLenName);
                            CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                            CommonSet.camUpC2.SetGain(AssembleSuction2.dResultTestGain);
                            mc.setDO(DO.组装上相机2, true);
                            WriteOutputInfo(strOut + "镜筒固定块拍照");
                        }
                        step = step + 1;
                        break;
                    case ActionName._30镜筒拍照完成:
                        if (!sw.WaitSetTime(50))
                            break;
                        if (axisX == AXIS.组装X1轴)
                        {
                            mc.setDO(DO.组装上相机1, false);
                            if (AssembleSuction1.imgResultUp.bStatus)
                            {
                                if (AssembleSuction1.imgResultUp.bImageResult)
                                {
                                    if (iPos == 1) {
                                        
                                        dRow = AssembleSuction1.imgResultUp.CenterRow;
                                        dCol = AssembleSuction1.imgResultUp.CenterColumn;
                                       
                                    }
                                    step = step + 1;
                                }
                                else
                                {
                                    step = step + 1;
                                    //Run.runMode = RunMode.手动;
                                    //step = 0;
                                }

                            }

                        }
                        else
                        {
                            mc.setDO(DO.组装上相机2, false);
                            if (AssembleSuction2.imgResultUp.bStatus)
                            {
                                if (AssembleSuction2.imgResultUp.bImageResult)
                                {
                                    if (iPos == 1)
                                    {
                                        dRow = AssembleSuction2.imgResultUp.CenterRow;
                                        dCol = AssembleSuction2.imgResultUp.CenterColumn;
                                    }
                                    step = step + 1;
                                }
                                else
                                {
                                    step = step + 1;
                                    //Run.runMode = RunMode.手动;
                                    //step = 0;
                                }

                            }
                        }
                       

                        break;
                    case ActionName._30XY轴到组装位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            if (iCurrentStation == 1)
                            {

                                double dDiffDownX = (1024-CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterRow)*AssembleSuction1.GetDownResulotion();
                                double dDiffDownY = (1224-CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterColumn)*AssembleSuction1.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();

                                dDestPosX = (CommonSet.dic_Assemble1[1].pAssemblePos1.X + CommonSet.dic_Assemble1[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX) + CommonSet.dic_Assemble1[iCurrentSuctionOrder].dMakeUpX - (dDiffDownX+dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble1[1].pAssemblePos1.Y + CommonSet.dic_Assemble1[iCurrentSuctionOrder].dMakeUpY +(dDiffDownY+dDiffUpY);
                                
                                
                                mc.AbsMove(AXIS.组装X1轴, dDestPosX, (int)CommonSet.dVelRunX);
                                mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                                step = step + 1;
                            }
                            else if (iCurrentStation == 2)
                            {

                                double dDiffDownX = (1024 - CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterRow) * AssembleSuction1.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble1[iCurrentSuctionOrder].imgResultDown.CenterColumn) * AssembleSuction1.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();
                                dDestPosX = (CommonSet.dic_Assemble1[1].pAssemblePos2.X + CommonSet.dic_Assemble1[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble1[1].DPosCameraDownX) + CommonSet.dic_Assemble1[iCurrentSuctionOrder].dMakeUpX2 - (dDiffDownX + dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble1[1].pAssemblePos2.Y + CommonSet.dic_Assemble1[iCurrentSuctionOrder].dMakeUpY2 + (dDiffDownY + dDiffUpY);
                                
                                mc.AbsMove(AXIS.组装X1轴, dDestPosX, (int)CommonSet.dVelRunX);
                                mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                                step = step + 1;
                            }
                        }
                        else
                        {
                            if (iCurrentStation == 1)
                            {

                                double dDiffDownX = (1024 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                                dDestPosX = (CommonSet.dic_Assemble2[10].pAssemblePos1.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX) + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpX - (dDiffDownX + dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble2[10].pAssemblePos1.Y + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpY + (dDiffDownY + dDiffUpY);
                               
                                mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                                mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                                step = step + 1;
                            }
                            else if (iCurrentStation == 2)
                            {

                                double dDiffDownX = (1024 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                                dDestPosX = (CommonSet.dic_Assemble2[10].pAssemblePos2.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX) + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpX2 - (dDiffDownX + dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble2[10].pAssemblePos2.Y + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpY2 + (dDiffDownY + dDiffUpY);
                              
                                mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                                mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                                step = step + 1;
                            }
                        
                        }
                        break;


                    case ActionName._30验证完成:

                        if (bUseAssembleParam)
                        {
                            if (axisX == AXIS.组装X1轴)
                            {
                                if (iCurrentStation == 1)
                                {

                                    mc.setDO(DO.组装位1夹紧, false);
                                    ParamListerner.station1.WriteHeightResult(CommonSet.strReportPath + "\\Height\\");
                                    ParamListerner.station1.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                                    ParamListerner.station1.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                                }
                                else
                                {
                                   // mc.setDO(DO.组装位2夹紧, false);
                                    ParamListerner.station2.WriteHeightResult(CommonSet.strReportPath + "\\Height\\");
                                    ParamListerner.station2.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                                    ParamListerner.station2.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                                }
                            }
                            else
                            {
                                if (iCurrentStation == 1)
                                {
                                    //mc.setDO(DO.组装位1夹紧, false);
                                    ParamListerner.station1.WriteHeightResult(CommonSet.strReportPath + "\\Height\\");
                                    ParamListerner.station1.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                                    ParamListerner.station1.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                                }
                                else
                                {
                                    //mc.setDO(DO.组装位2夹紧, false);
                                    ParamListerner.station2.WriteHeightResult(CommonSet.strReportPath + "\\Height\\");
                                    ParamListerner.station2.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                                    ParamListerner.station2.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                                }
                            }
                        }
                          strD = "取料气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                       
                          strD = "组装气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        Run.runMode = RunMode.手动;
                        step = 0;

                        break;
                    #endregion
                }


            }
            catch (Exception)
            {
                
                
            }


        }

        public override void Action2()
        {
           
        }
        private void WriteFile(string strPath)
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
                    string strHead = "时间,吸笔,取料吸笔中心差X,取料吸笔中心差Y,组装吸笔中心差X,组装吸笔中心差Y,";
                    //for (int i = 1; i < 10; i++)
                    //{

                    //    strHead += "C" + i.ToString() + "_," + "C" + i.ToString() + "_Col,";
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
                string strContent = "";
                strContent += dDiffOptX.ToString("0.000") + ",";
                strContent += dDiffOptY.ToString("0.000") + ",";
                strContent += dDiffAssemX.ToString("0.000") + ",";
                strContent += dDiffAssemY.ToString("0.000") + ",";
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + iCurrentSuctionOrder.ToString() + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WriteResult(string strPath, string strContent)
        {
            Action<string> dele = WriteFile;
            dele.BeginInvoke(strPath, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string> dele = (Action<string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }
        
    }
}
