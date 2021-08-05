using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using HalconDotNet;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.IO;
namespace Assembly
{
   public class Assem2Module:ActionModule
    {
        private string strOut = "Assem2Module-Action-";
        // private static Assem1Module module = null;
      
        //public static List<int> lstNGSuction = new List<int>();//飞拍NG的图片
       // private Point currentPoint = null;
        //private int iCurrentTimes = 1;
        //private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static int iCurrentSuctionOrder = 9;//当前吸笔序号
        public static int iPicNum = 1;//拍照顺序
        public static int iPicSum = 0;//拍照总数,飞拍之前赋值确认
        private static int iFlashTimes = 1;//飞拍次数计数
        public static bool bIsProductEmpty = false;//盘子中有无料，true为无料
        public static int iCurrentLen = 0;//当前镜筒位置
        public static int iCurrentStation = 1;//当前组装工位
        int iMaxSuctionOrder = 1;//最大吸笔序号
        public static int iCurrentSolution = 0;//当前组装方案
        public static int iResetStep = 0;
        private string strD = "";
        private DO dOut;
        private DI dIn;
        public static bool bImageResult = false;
        public static List<Point> lstLenCenter = new List<Point>();//存储组装过程中镜筒拍照中心
        public static double dTestZStep = 1;//自动测压的移动步长

        public static double dDiffOptX = 0;
        public static double dDiffOptY = 0;
        public static double dDiffAssemX = 0;
        public static double dDiffAssemY = 0;
        public Assem2Module()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._30等待组装);
            //取料飞拍
            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30吸笔全部上升);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30X轴到取料位);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30求心张开检测);
            lstAction.Add(ActionName._30吸笔全部下降);
            lstAction.Add(ActionName._30Z轴到取料位);
            lstAction.Add(ActionName._30Z轴到位完成);
            //lstAction.Add(ActionName._30求心闭合);
            //lstAction.Add(ActionName._30求心张开);
            //lstAction.Add(ActionName._30求心张开检测);
            lstAction.Add(ActionName._30吸笔全部吸真空);
            lstAction.Add(ActionName._30Z轴到求心位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30求心闭合);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._30求心张开检测);
            lstAction.Add(ActionName._30求心闭合);
            lstAction.Add(ActionName._30求心张开);
            lstAction.Add(ActionName._30求心张开检测);
            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30吸笔真空检测);
            //飞拍
            lstAction.Add(ActionName._30X轴到取料位);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30开始飞拍);
            lstAction.Add(ActionName._30X轴到飞拍结束位);
            lstAction.Add(ActionName._30X轴到位);
            lstAction.Add(ActionName._30飞拍完成);
            lstAction.Add(ActionName._30吸笔全部上升);
            //组装
            lstAction.Add(ActionName._30开始组装);
            lstAction.Add(ActionName._30获取组装吸笔);
            lstAction.Add(ActionName._30Z轴到位完成);
            //组装再次拍照
            lstAction.Add(ActionName._30XY到镜筒拍照位);
            lstAction.Add(ActionName._30XY轴到位);
            lstAction.Add(ActionName._30镜筒固定块拍照);
            lstAction.Add(ActionName._30镜筒拍照完成);

            lstAction.Add(ActionName._30XY轴到组装位);
            lstAction.Add(ActionName._30吸笔下降判定);
            lstAction.Add(ActionName._30XY轴到位);
           // lstAction.Add(ActionName._30Z轴到组装准备位);
            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到组装准备位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到组装测压位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴测压判断);
            lstAction.Add(ActionName._30Z轴到组装位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30吸笔破真空);
            lstAction.Add(ActionName._30读取数据);
            lstAction.Add(ActionName._30Z轴到组装上升位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30吸笔上升);
            lstAction.Add(ActionName._30吸笔下降);
            lstAction.Add(ActionName._30组装完成);

            //组装NG抛料

            lstAction.Add(ActionName._30开始抛料);
            lstAction.Add(ActionName._30获取组装吸笔);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30XY轴到抛料位);
            lstAction.Add(ActionName._30吸笔下降判定);
            lstAction.Add(ActionName._30XY轴到位);
            //lstAction.Add(ActionName._30Z轴到组装准备位);
            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30Z轴到抛料位);
            lstAction.Add(ActionName._30Z轴到位完成);
            lstAction.Add(ActionName._30吸笔破真空);

            lstAction.Add(ActionName._30Z轴到安全位);
            lstAction.Add(ActionName._30吸笔上升);
            lstAction.Add(ActionName._30吸笔下降);
            lstAction.Add(ActionName._30抛料完成);
            
        }
        public override void Reset()
        {
            switch (iResetStep)
            {
                case 0:
                    if (bResetFinish)
                        break;
                    IStep = 0;
                    iPicNum = 0;
                    iPicSum = 0;
                    iFlashTimes = 0;
                    iMaxSuctionOrder = 1;
                    iCurrentSolution = 0;
                    iCurrentSuctionOrder = 9;
                    AssemState2 = ActionState.组装;
                    mc.setDO(DO.组装位2夹紧, false);
                    mc.setDO(DO.组装位2真空破, false);
                    mc.setDO(DO.组装位2真空吸, false);
                    AssemblePutSuction2(false);
                    iCurrentStation = 0;
                    foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                    {
                        AssembleSuction2 p = pair.Value;
                       
                            string strDo = "组装吸真空" + p.SuctionOrder;
                            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                            mc.setDO(dOut, false);
                           
                       
                    }
                    //iCurrentTimes = 1;
                    //iCurrentNum = 1;
                    //iCurrentSuction = 1;
                    // bIsProductEmpty = false;
                    //iCurrentBarrel = 0;
                    //lstNGSuction.Clear();
                    iResetStep = iResetStep + 1;

                    break;
                case 1:
                    if (Run.flashModule2.bResetFinish)
                    {
                        if (sw.WaitSetTime(1000))
                        {
                            if (!AssembleCheckSuctionUp2())
                            {
                                Alarminfo.AddAlarm(Alarm.组装2部吸笔上升超时);
                                WriteOutputInfo(strOut + "复位时,组装2部吸笔上升超时");
                                Run.bReset = false;
                                break;
                            }
                            if (mc.dic_Axis[AXIS.组装X2轴].INP && mc.dic_Axis[AXIS.组装Y2轴].INP && mc.dic_Axis[AXIS.组装Z2轴].INP)
                                Action(ActionName._30Z轴到待机位, ref iResetStep);
                        }
                    }
                    break;
                case 2:
                    Action(ActionName._30Z轴到位完成, ref iResetStep);

                    break;

                case 3:
                    Action(ActionName._30XY轴到待机位, ref iResetStep);

                    break;
                case 4:
                    if (IsAxisINP(dDestPosX, AXIS.组装X2轴) && IsAxisINP(dDestPosY, AXIS.组装Y2轴))
                    {
                        WriteOutputInfo(strOut + "组装X2轴Y2轴到位");
                        iResetStep = iResetStep + 1;
                    }

                    break;
                case 5:
                    sw.ResetSetWatch();
                    iResetStep = 0;
                    bResetFinish = true;
                    break;

                default:
                    iResetStep = 0;
                    bResetFinish = false;
                    break;

            }
           
        }
        public override void Action2()
        {

            if (iCurrentStation == 1)
            {
                if (IStep <= lstAction.IndexOf(ActionName._30开始飞拍) && ParamListerner.station1.bFinish1 && !ParamListerner.station1.bFinish2)
                {
                    double posY = AssembleSuction2.pCamBarrelPos1.Y;
                    if (mc.dic_Axis[AXIS.组装Y1轴].INP && (Math.Abs(mc.dic_Axis[AXIS.组装Y1轴].dPos - posY) > 0.01))
                    {
                        WriteOutputInfo(strOut + "工位2组装Y1轴到飞拍位2");
                        mc.AbsMove(AXIS.组装Y1轴, posY, (int)CommonSet.dVelRunY);
                    }
                }
            }
            else if (iCurrentStation == 2)
            {
                if (IStep <= lstAction.IndexOf(ActionName._30开始飞拍) && ParamListerner.station2.bFinish1 && !ParamListerner.station2.bFinish2)
                {
                    double posY = AssembleSuction2.pCamBarrelPos2.Y;
                    if (mc.dic_Axis[AXIS.组装Y2轴].INP && (Math.Abs(mc.dic_Axis[AXIS.组装Y2轴].dPos - posY) > 0.01))
                    {
                        WriteOutputInfo(strOut + "工位2组装Y2轴到飞拍位2");
                        mc.AbsMove(AXIS.组装Y2轴, posY, (int)CommonSet.dVelRunY);
                    }
                }
            }
           
                //如果已放好则将取镜筒的序号给取料
                if (BarrelSuction.bUseAssem1 && !ParamListerner.station1.bGet2&&(ParamListerner.station1.iCurrentBarrelNum>0))
                {
                    GetProduct2Module.iCurrentBarrel = ParamListerner.station1.iCurrentBarrelNum;
                    // iCurrentStation = 1;
                }
                else if (BarrelSuction.bUseAssem2 && !ParamListerner.station2.bGet2 && (ParamListerner.station2.iCurrentBarrelNum > 0))
                {
                    GetProduct2Module.iCurrentBarrel = ParamListerner.station2.iCurrentBarrelNum;
                    // iCurrentStation = 2;
                }
                else
                {
                    GetProduct2Module.iCurrentBarrel = 0;
                }
            
        }
        public override void Action(ActionName action, ref int step)
        {

            try
            {
               
                switch (action)
                {
                    case ActionName._30等待组装:
                        //空跑赋初始值
                        //ParamListerner.station1.bGet2 = false;
                        //ParamListerner.station2.bGet2 = false;
                        //如果已放好则将取镜筒的序号给取料
                        if (BarrelSuction.bUseAssem1 && !ParamListerner.station1.bGet2 && ParamListerner.station1.bFinish1)
                        {
                           // GetProduct2Module.iCurrentBarrel = ParamListerner.station1.iCurrentBarrelNum;
                            iCurrentStation = 1;
                        }
                        else if (BarrelSuction.bUseAssem2 && !ParamListerner.station2.bGet2 && ParamListerner.station2.bFinish1)
                        {
                           // GetProduct2Module.iCurrentBarrel = ParamListerner.station2.iCurrentBarrelNum;
                            iCurrentStation = 2;
                        }
                        else
                        {
                            GetProduct2Module.iCurrentBarrel = 0;
                        }

                        if (AssemState2 == ActionState.组装)
                        {
                            //当取料X轴在往回走的时候组装位才过来取料
                            if ((mc.dic_Axis[AXIS.取料X2轴].dPos < OptSution2.pPutPos.X - 5) && FlashModule2.bPut)
                            {
                                iFlashTimes = 0;
                                WriteOutputInfo(strOut + "开始组装" + iCurrentStation.ToString());
                                step = step + 1;
                                CommonSet.swCircleC2.Restart();
                                lstLenCenter.Clear();
                                for (int i = 0; i < 9; i++)
                                {
                                    Point p = new Point();
                                    lstLenCenter.Add(p);
                                }
                            }

                        }
                       
                           
                        

                        break;
                    case ActionName._30吸笔全部下降:
                        mc.WriteOutDA(CommonSet.fCommonPressureV2, 2);
                        AssemblePutSuction2(true);
                        if (AssembleCheckSuctionDown2())
                        {
                            WriteOutputInfo(strOut + "组装2吸笔全部下降");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.组装2吸笔取料时下降超时);
                                WriteOutputInfo(strOut + "组装2吸笔取料时下降超时");
                            }
                        }
                        break;
                    case ActionName._30吸笔全部上升:
                        mc.WriteOutDA(CommonSet.fCommonPressureV2, 2);
                        AssemblePutSuction2(false);
                        if (AssembleCheckSuctionUp2()||CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "组装2吸笔全部上升");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.组装2吸笔取料时上升超时);
                                WriteOutputInfo(strOut + "组装2吸笔取料时上升超时");
                            }
                        }

                        break;
                    case ActionName._30吸笔全部吸真空:
                        mc.setDO(DO.中转吸真空气源2, false);
                        foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                        {
                            AssembleSuction2 p = pair.Value;
                            if (p.BUse && (pair.Key < 19))
                            {
                                string strDo = "组装吸真空" + p.SuctionOrder;
                                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                                mc.setDO(dOut, true);
                                strDo = "中转吸真空" + p.SuctionOrder;
                                dOut = (DO)Enum.Parse(typeof(DO), strDo);
                                mc.setDO(dOut, false);
                            }
                        }
                        long l = (long)(AssembleSuction2.DGetTime * 1000);
                        if (sw.WaitSetTime(l))
                        {
                            WriteOutputInfo(strOut + "吸笔全部吸真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30吸笔真空检测:
                        bool bFlag2 = true;
                        foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                        {
                            AssembleSuction2 p = pair.Value;
                            if (p.BUse && (pair.Key < 19))
                            {
                                string strD = "组装真空检测" + p.SuctionOrder.ToString();
                                DI dI = (DI)Enum.Parse(typeof(DI), strD);
                                bFlag2 = bFlag2 && mc.dic_DI[dI];

                            }
                        }
                        if (bFlag2 || CommonSet.bTest || CommonSet.bForceOk)
                        {
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 1;

                                break;
                            }
                            CommonSet.bForceOk = false;
                            WriteOutputInfo(strOut + "吸笔全部吸真空");
                            step = step + 1;
                        }
                        else
                        {
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 2;
                                Alarminfo.AddAlarm(Alarm.组装吸笔在中转2取料真空检测异常);
                                WriteOutputInfo(strOut + "组装吸笔在中转2取料真空检测异常");
                                break;
                            }
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                if (Run.runMode == RunMode.运行)
                                {
                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                    int alarmStep = lstAction.IndexOf(ActionName._30Z轴到取料位);
                                    CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, step-alarmStep, "组装吸笔在中转2取料真空检测异常", false);
                                    Run.runMode = RunMode.暂停;
                                }
                                Alarminfo.AddAlarm(Alarm.组装吸笔在中转2取料真空检测异常);
                                WriteOutputInfo(strOut + "组装2吸笔取料真空检测异常");
                            }
                        }
                        break;
                    case ActionName._30求心闭合:
                        mc.setDO(DO.中转2夹紧, true);
                        if (CommonSet.bTest || mc.dic_DI[DI.中转2夹紧气缸闭合检测])
                        {
                            long lc = (long)(AssembleSuction2.DCorrectTime * 1000);
                            if (sw.WaitSetTime(lc))
                            {
                                WriteOutputInfo(strOut + "求心2闭合");
                                step = step + 1;
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.求心2闭合超时);
                                WriteOutputInfo(strOut + "求心2闭合超时");
                            }
                        }
                        break;
                    case ActionName._30求心张开:
                        mc.setDO(DO.中转2夹紧, false);
                        WriteOutputInfo(strOut + "求心2张开");
                        step = step + 1;

                        break;

                    case ActionName._30求心张开检测:
                        if (CommonSet.bTest || mc.dic_DI[DI.中转2夹紧气缸张开检测])
                        {
                            if (sw.WaitSetTime(300))
                            {
                                WriteOutputInfo(strOut + "求心2张开检测");
                                step = step + 1;
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                            Alarminfo.AddAlarm(Alarm.求心2张开超时);
                            WriteOutputInfo(strOut + "求心2张开超时");
                            }
                        }
                        break;
                    case ActionName._30X轴到取料位:
                        if (!IsAxisINP(AXIS.组装X2轴))
                        {
                            break;
                        }
                        //在X去取料位的同时，将气缸向下
                        //foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                        //{
                        //    if (pair.Value.BUse)
                        //    {
                        //        string strDo = "组装气缸下降" + pair.Value.SuctionOrder;
                        //        DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                        //        mc.setDO(dOut, true);
                        //    }
                        //}
                        dDestPosX = AssembleSuction2.dFlashStartX;
                        mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "组装X2轴到取料位");
                        step = step + 1;
                        break;
                    case ActionName._30X轴到飞拍结束位:
                        dDestPosX = AssembleSuction2.dFlashEndX;
                        mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "组装X2轴到飞拍结束位");
                        step = step + 1;
                        break;
                    case ActionName._30X轴到位:
                        if (IsAxisINP(dDestPosX, AXIS.组装X2轴))
                        {
                            WriteOutputInfo(strOut + "组装X2轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30Z轴到安全位:
                        dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "组装Z2轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到待机位:
                        dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "组装Z2轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到抛料位:
                        if (iCurrentStation == 1)
                        {
                            dDestPosZ = AssembleSuction2.pDiscardC1.Z;
                        }
                        else
                        {
                            dDestPosZ = AssembleSuction2.pDiscardC2.Z;
                        }
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "组装Z2轴到抛料位");
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, AXIS.组装Z2轴))
                        {
                            WriteOutputInfo(strOut + "组装Z2轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30Z轴到取料位:
                        if (CommonSet.bTest)
                        {
                            dDestPosZ = AssembleSuction2.DGetPosZ -10;
                        }
                        else
                        {
                            dDestPosZ = AssembleSuction2.DGetPosZ;
                        }
                      
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "组装Z2轴到取料位");
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到求心位:
                        if (CommonSet.bTest)
                        {
                            dDestPosZ = AssembleSuction2.DCorrectPosZ -10;
                        }
                        else
                        {
                            dDestPosZ = AssembleSuction2.DCorrectPosZ; ;
                        }
                        
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "组装Z2轴到取料位");
                        step = step + 1;
                        break;

                    case ActionName._30XY到镜筒拍照位:
                        if ((!CommonSet.dic_Assemble2[iCurrentSuctionOrder].bCam) || CommonSet.bTest)
                        {
                            step = lstAction.IndexOf(ActionName._30XY轴到组装位);
                            break;

                        }
                        if (iCurrentSuctionOrder > 15)
                        {
                            if (!FlashModule2.bPut)
                            {
                                bool bStation = BarrelSuction.bUseAssem1 && BarrelSuction.bUseAssem2;
                                if (iCurrentStation == 1)
                                {
                                    if (bStation)
                                    {
                                        if (!(ParamListerner.station1.iCurrentBarrelNum == CommonSet.dic_BarrelSuction[1].EndPos))
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (iCurrentStation == 2)
                                {
                                    if (bStation)
                                    {
                                        if (!(ParamListerner.station2.iCurrentBarrelNum == CommonSet.dic_BarrelSuction[1].EndPos))
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }

                        if (iCurrentStation == 1)
                        {
                            dDestPosX = AssembleSuction2.pCamBarrelPos1.X;
                            dDestPosY = AssembleSuction2.pCamBarrelPos1.Y;
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }
                        else if (iCurrentStation == 2)
                        {
                            dDestPosX = AssembleSuction2.pCamBarrelPos2.X;
                            dDestPosY = AssembleSuction2.pCamBarrelPos2.Y;
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }
                        break;

                    case ActionName._30镜筒固定块拍照:
                        if (iCurrentSuctionOrder >= 16)
                        {
                            AssembleSuction2.InitImageUp(AssembleSuction2.strPicUpCameraLastCheckName);
                        }
                        else
                        {
                            AssembleSuction2.InitImageUp(AssembleSuction2.strPicCameraUpCheckName);
                        }
                        //AssembleSuction1.InitImageUp(AssembleSuction1.strResultSuctionName);
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        CommonSet.camUpC2.SetGain(AssembleSuction2.dGainUp);
                        mc.setDO(DO.组装上相机2, true);
                        WriteOutputInfo(strOut + "镜筒固定块拍照");
                        step = step + 1;

                        break;
                    case ActionName._30镜筒拍照完成:
                        if (!sw.WaitSetTime(50))
                            break;
                        mc.setDO(DO.组装上相机2, false);
                        //判断镜筒结果
                        if (AssembleSuction2.imgResultUp.bStatus)
                        {
                            
                            //判断镜筒结果 
                            if (!AssembleSuction2.imgResultUp.bImageResult && !CommonSet.bForceOk)
                            {
                                if ((Run.runMode == RunMode.暂停))
                                {

                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                    Run.iDebugResult = 2;
                                    break;

                                }
                                if ((Run.runMode == RunMode.运行))
                                {
                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                    Run.iDebugResult = 2;
                                    CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, 1, "组装2镜筒检测NG", false);
                                    Run.runMode = RunMode.暂停;
                                }

                                Alarminfo.AddAlarm(Alarm.组装2镜筒检测NG);
                                WriteOutputInfo(strOut + "组装2镜筒检测NG");

                                break;
                            }
                            else
                            {
                                if ((Run.runMode == RunMode.暂停))
                                {

                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                    Run.iDebugResult = 1;
                                    break;

                                }
                                Point pCenter = new Point();
                                pCenter.X = AssembleSuction2.imgResultUp.CenterRow;
                                pCenter.Y = AssembleSuction2.imgResultUp.CenterColumn;
                                lstLenCenter[iCurrentSuctionOrder - 10] = pCenter;
                                CommonSet.bForceOk = false;
                                step = step + 1;
                                WriteOutputInfo(strOut + "镜筒拍照完成"+iCurrentSuctionOrder.ToString());
                            }

                        }
                        break;
                    case ActionName._30XY轴到位:
                        if (iCurrentStation == 1)
                        {
                            if (IsAxisINP(dDestPosX, AXIS.组装X2轴) && IsAxisINP(dDestPosY, AXIS.组装Y1轴))
                            {
                                WriteOutputInfo(strOut + "X2Y1轴到位");
                                step = step + 1;
                            }
                        }
                        else if (iCurrentStation == 2)
                        {
                            if (IsAxisINP(dDestPosX, AXIS.组装X2轴) && IsAxisINP(dDestPosY, AXIS.组装Y2轴))
                            {
                                WriteOutputInfo(strOut + "X1Y2轴到位");
                                step = step + 1;
                            }
                        }
                        break;

                    case ActionName._30XY轴到待机位:

                        dDestPosX = AssembleSuction2.pSafeXYZ.X;
                        dDestPosY = AssembleSuction2.pSafeXYZ.Y;
                        mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)100);
                        mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "组装2到待机位");
                        step = step + 1;
                        break;
                    case ActionName._30开始抛料:
                        if (GlueAndBarrelState == ActionState.点胶和放成品)
                        {
                            break;
                        }

                        iMaxSuctionOrder = 1;
                        if (iCurrentStation == 1)
                        {
                            if (!ParamListerner.station1.bFinish1 || ParamListerner.station1.bFinish2)
                                break;

                            // BarrelModule.bPut1Finish = false;
                          
                            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                            {
                                if (CommonSet.dic_Assemble2[pair.Key].BUse)
                                {
                                    if (pair.Key > iMaxSuctionOrder)
                                        iMaxSuctionOrder = pair.Key;
                                  
                                }
                            }
                            
                            //ParamListerner.station1.iAssembleOrder = iCurrentStation;
                            WriteOutputInfo(strOut + "开始组装工位1抛料");
                            ParamListerner.station1.bGet2 = false;
                            step = step + 1;
                        }
                        else if (iCurrentStation == 2)
                        {
                            if (!ParamListerner.station2.bFinish1 ||ParamListerner.station2.bFinish2)
                                break;

                         
                            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                            {
                                if (CommonSet.dic_Assemble2[pair.Key].BUse)
                                {
                                    if (pair.Key > iMaxSuctionOrder)
                                        iMaxSuctionOrder = pair.Key;
                                }
                            }
                           // ParamListerner.station1.iAssembleOrder = iCurrentStation;
                            WriteOutputInfo(strOut + "开始组装工位2抛料");
                            //ParamListerner.station2.bGet2 = true;
                            ParamListerner.station2.bGet2 = false;
                            step = step + 1;
                        }
                          FlashModule2.bPut = false;//将飞拍放料标志置位，代表可以放料
                          iCurrentSuctionOrder = 9;
                     
                        break;
                    case ActionName._30开始组装:
                        iMaxSuctionOrder = 1;
                        if (iCurrentStation == 1)
                        {
                            if (!ParamListerner.station1.bFinish1 || ParamListerner.station1.bFinish2)
                                break;

                            // BarrelModule.bPut1Finish = false;
                            iCurrentSolution = CommonSet.asm.getSolution(ParamListerner.station1.iCurrentBarrelNum);
                            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                            {
                                if (CommonSet.dic_Assemble2[pair.Key].BUse)
                                {
                                    if (pair.Key > iMaxSuctionOrder)
                                        iMaxSuctionOrder = pair.Key;
                                    pair.Value.dAngleSet = CommonSet.asm.dic_Solution[iCurrentSolution].lstAngle[pair.Key - 1];
                                    pair.Value.dPressureSet = CommonSet.asm.dic_Solution[iCurrentSolution].lstPressure[pair.Key - 1];
                                    pair.Value.dAssemblVel = CommonSet.asm.dic_Solution[iCurrentSolution].lstVel[pair.Key - 1];
                                    pair.Value.dPutDelay = CommonSet.asm.dic_Solution[iCurrentSolution].lstTime[pair.Key - 1];
                                    //pair.Value.dAssembleH = CommonSet.asm.dic_Solution[iCurrentSolution].lstHieght[pair.Key - 1];
                                    //pair.Value.dAssembleH2 = CommonSet.asm.dic_Solution[iCurrentSolution].lstHeight2[pair.Key - 1];
                                    pair.Value.dAssembleOver = CommonSet.asm.dic_Solution[iCurrentSolution].lstHeight2[pair.Key - 1];
                                }
                            }
                            
                            //ParamListerner.station1.iAssembleOrder = iCurrentStation;
                            WriteOutputInfo(strOut + "开始组装工位1");
                            ParamListerner.station1.bGet2 = true;
                            step = step + 1;
                        }
                        else if (iCurrentStation == 2)
                        {
                            if (!ParamListerner.station2.bFinish1 ||ParamListerner.station2.bFinish2)
                                break;

                            iCurrentSolution = CommonSet.asm.getSolution(ParamListerner.station2.iCurrentBarrelNum);
                            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                            {
                                if (CommonSet.dic_Assemble2[pair.Key].BUse)
                                {
                                    if (pair.Key > iMaxSuctionOrder)
                                        iMaxSuctionOrder = pair.Key;
                                    pair.Value.dAngleSet = CommonSet.asm.dic_Solution[iCurrentSolution].lstAngle[pair.Key - 1];
                                    pair.Value.dPressureSet = CommonSet.asm.dic_Solution[iCurrentSolution].lstPressure[pair.Key - 1];
                                    pair.Value.dAssemblVel = CommonSet.asm.dic_Solution[iCurrentSolution].lstVel[pair.Key - 1];
                                    pair.Value.dPutDelay = CommonSet.asm.dic_Solution[iCurrentSolution].lstTime[pair.Key - 1];
                                    //pair.Value.dAssembleH = CommonSet.asm.dic_Solution[iCurrentSolution].lstHieght[pair.Key - 1];
                                    //pair.Value.dAssembleH2 = CommonSet.asm.dic_Solution[iCurrentSolution].lstHeight2[pair.Key - 1];
                                    pair.Value.dAssembleOver = CommonSet.asm.dic_Solution[iCurrentSolution].lstHeight2[pair.Key - 1];
                                }
                            }
                           // ParamListerner.station1.iAssembleOrder = iCurrentStation;
                            WriteOutputInfo(strOut + "开始组装工位2");
                            //ParamListerner.station2.bGet2 = true;
                            ParamListerner.station2.bGet2 = true;
                            step = step + 1;
                        }
                          FlashModule2.bPut = false;//将飞拍放料标志置位，代表可以放料
                          iCurrentSuctionOrder = 9;
                        break;
                    case ActionName._30获取组装吸笔:

                        // int count = lstAssembleAction.Count;
                       // int num = CommonSet.dic_Assemble2.Count;
                        do
                        {
                            iCurrentSuctionOrder = iCurrentSuctionOrder + 1;
                            //如果吸笔序号超过总数，则表明已组装完成，跳到最后一步
                            if (iCurrentSuctionOrder > iMaxSuctionOrder)
                            {

                                if (bImageResult)
                                {
                                    step = lstAction.IndexOf(ActionName._30组装完成);
                                }
                                else
                                {
                                    step = lstAction.IndexOf(ActionName._30抛料完成);
                                }
                                break;
                            }
                            if (CommonSet.dic_Assemble2[iCurrentSuctionOrder].BUse)
                            {
                                step = step + 1;
                                WriteOutputInfo(strOut + "当前组装吸笔为" + iCurrentSuctionOrder.ToString());
                                break;
                            }

                        } while (iCurrentSuctionOrder < iMaxSuctionOrder + 1);

                        break;
                    case ActionName._30XY轴到抛料位:
                        if (iCurrentStation == 1)
                        {
                            dDestPosX = AssembleSuction2.pDiscardC1.X + (iCurrentSuctionOrder - 10) * 25;
                            dDestPosY = AssembleSuction2.pDiscardC1.Y;
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装抛料位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }
                        else if (iCurrentStation == 2)
                        {
                            dDestPosX = AssembleSuction2.pDiscardC2.X + (iCurrentSuctionOrder - 10) * 25;
                            dDestPosY = AssembleSuction2.pDiscardC2.Y;
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装抛料位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }

                        break;
                    case ActionName._30XY轴到组装位:
                        if (iCurrentStation == 1)
                        {
                            if (CommonSet.bTest)
                            {
                                dDestPosX = AssembleSuction2.pCamBarrelPos1.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Calib[CalibStation.组装工位2].pCalibPos.X;
                                dDestPosY = AssembleSuction2.pCamBarrelPos1.Y;

                            }
                            else
                            {
                                double dDiffDownX = (1024 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();

                                dDiffAssemX = AssembleSuction2.GetDownResulotion() * (CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow - CommonSet.dic_Assemble2[iCurrentSuctionOrder].pSuctionCenter.X);
                                dDiffAssemY = AssembleSuction2.GetDownResulotion() * (CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn - CommonSet.dic_Assemble2[iCurrentSuctionOrder].pSuctionCenter.Y);
                             
                                //写入文件
                                WriteCenterDiffResult(CommonSet.strReportPath + "物料偏移吸笔中心数据\\", "");

                                dDestPosX = (CommonSet.dic_Assemble2[10].pAssemblePos1.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX) + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpX - (dDiffDownX+dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble2[10].pAssemblePos1.Y + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpY + (dDiffDownY+dDiffUpY);
                               
                            }
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                         
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }
                        else if (iCurrentStation == 2)
                        {
                            if (CommonSet.bTest)
                            {
                                dDestPosX = AssembleSuction2.pCamBarrelPos2.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Calib[CalibStation.组装工位2].pCalibPos.X;
                                dDestPosY = AssembleSuction2.pCamBarrelPos2.Y;
                            }
                            else
                            {
                                double dDiffDownX = (1024 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                                double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                                dDiffAssemX = dDiffDownX;
                                dDiffAssemY = dDiffDownY;
                                //写入文件
                                WriteCenterDiffResult(CommonSet.strReportPath + "物料偏移吸笔中心数据\\", "");

                                dDestPosX = (CommonSet.dic_Assemble2[10].pAssemblePos2.X + CommonSet.dic_Assemble2[iCurrentSuctionOrder].DPosCameraDownX - CommonSet.dic_Assemble2[10].DPosCameraDownX) + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpX2 - (dDiffDownX + dDiffUpX);
                                dDestPosY = CommonSet.dic_Assemble2[10].pAssemblePos2.Y + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dMakeUpY2 + (dDiffDownY + dDiffUpY);
                              
                            }
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            //WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位");
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "到XY组装位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            step = step + 1;
                        }
                        break;
                    case ActionName._30开始飞拍:
                        //等待Y轴到位
                        if (iCurrentStation == 1)
                        {
                            double posY = AssembleSuction2.pCamBarrelPos1.Y;
                            if (!IsAxisINP(posY, AXIS.组装Y1轴))
                            {
                                break;
                            }
                            //如果另一个工位Y轴没超过组装位的测高位，则等待
                            if (mc.dic_Axis[AXIS.组装Y2轴].dPos > (AssembleSuction2.pCamBarrelPos2.Y +0.1))
                            {
                                break;
                            }
                            iCurrentLen = ParamListerner.station1.iCurrentBarrelNum;
                        }
                        else if (iCurrentStation == 2)
                        {
                            double posY = AssembleSuction2.pCamBarrelPos2.Y;
                            if (!IsAxisINP(posY, AXIS.组装Y2轴))
                            {
                                break;
                            }
                            //如果另一个工位Y轴没超过组装位的测高位，则等待
                            if (mc.dic_Axis[AXIS.组装Y1轴].dPos > (AssembleSuction2.pCamBarrelPos1.Y +0.1))
                            {
                                break;
                            }
                            iCurrentLen = ParamListerner.station2.iCurrentBarrelNum;
                        }
                        else
                        {
                            if (BarrelSuction.bUseAssem1 && !ParamListerner.station1.bGet2&& ParamListerner.station1.bFinish1)
                            {
                                // GetProduct2Module.iCurrentBarrel = ParamListerner.station1.iCurrentBarrelNum;
                                iCurrentStation = 1;
                            }
                            else if (BarrelSuction.bUseAssem2 && !ParamListerner.station2.bGet2 && ParamListerner.station2.bFinish1)
                            {
                                // GetProduct2Module.iCurrentBarrel = ParamListerner.station2.iCurrentBarrelNum;
                                iCurrentStation = 2;
                            }
                            
                            break;
                        }

                        //lstLenCenter.Clear();

                        int sum = 0;
                        iFlashTimes++;
                        List<double> lstFlashData = new List<double>();
                        string strProcessUpName = AssembleSuction2.strPicCameraUpCheckName;
                        AssembleSuction2.InitImageUp(strProcessUpName);

                        foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                        {
                            if (pair.Value.BUse)
                            {
                                sum++;
                                int index = pair.Value.SuctionOrder;
                                double pos =((CommonSet.dic_Assemble2[index].DPosCameraDownX + AssembleSuction2.dFalshMakeUpX));
                                lstFlashData.Add(pos);
                                string strProcessName = CommonSet.dic_Assemble2[pair.Key].strPicDownProduct;
                                CommonSet.dic_Assemble2[pair.Key].InitImageDown(strProcessName);
                            }
                        }
                        //上相机的比较触发值存入最后一个
                        if (iCurrentStation == 1)
                            lstFlashData.Add(AssembleSuction2.pCamBarrelPos1.X + AssembleSuction2.dFalshMakeUpX);
                        else
                            lstFlashData.Add(AssembleSuction2.pCamBarrelPos2.X + AssembleSuction2.dFalshMakeUpX);
                        mc.startCmpTrigger2(AXIS.组装X2轴, lstFlashData.ToArray(), (ushort)2, (ushort)3);
                        CommonSet.camDownC2.SetGain(AssembleSuction2.dGainDown);
                        CommonSet.camDownC2.SetExposure(AssembleSuction2.dExposureTimeDown);

                        CommonSet.camUpC2.SetGain(AssembleSuction2.dGainUp);
                        CommonSet.camUpC2.SetExposure(AssembleSuction2.dExposureTimeUp);
                        WriteOutputInfo(strOut + "开始飞拍");
                         iPicNum = 1;
                        iPicSum = sum;
                        step = step + 1;
                        break;
                    case ActionName._30飞拍完成:
                        mc.stopCmpTrigger(AXIS.组装X2轴, (ushort)2, (ushort)3);
                        if (CommonSet.bTest)
                        {
                            bImageResult = true;
                            step = step + 1;
                            break;
                        }
                          bool bFlashFinish = true;//图像处理完成标志
                        bool bResult = true; //图像处理结果（所有结果进行并运算）
                        string strWrite = "";
                        if (iPicSum != 0)
                        {
                            if (Run.runMode == RunMode.暂停)
                            {
                                sw.ResetAlarmWatch();
                                Run.iDebugResult = 2;
                                Alarminfo.AddAlarm(Alarm.组装2飞拍数目异常);
                                WriteOutputInfo(strOut + "组装2飞拍数目异常");
                                break;
                            }
                            bool bIsOutTime = sw.AlarmWaitTime(2000);
                            if (bIsOutTime)
                            {
                                //如果超时一次,重新飞拍一次
                                if (iFlashTimes < 2)
                                {
                                    step = lstAction.LastIndexOf(ActionName._30X轴到取料位);

                                }
                                else
                                {
                                    if (Run.runMode == RunMode.运行)
                                    {
                                        //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                        sw.ResetAlarmWatch();
                                        CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, 5, "飞拍2图像数目异常", false);
                                        Run.runMode = RunMode.暂停;
                                    }

                                    bImageResult = false;
                                    Alarminfo.AddAlarm(Alarm.组装2飞拍数目异常);
                                    WriteOutputInfo(strOut + "组装2飞拍数目异常");
                                    step = lstAction.IndexOf(ActionName._30开始抛料);
                                }

                            }
                            break;
                        }
                       
                        foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                        {
                            if (pair.Value.BUse)
                            {
                                int index = pair.Value.SuctionOrder;
                                bFlashFinish = bFlashFinish && CommonSet.dic_Assemble2[index].imgResultDown.bStatus;
                                bResult = bResult && CommonSet.dic_Assemble2[index].imgResultDown.bImageResult;
                                strWrite += CommonSet.dic_Assemble2[index].imgResultDown.CenterRow + "," + CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn + ",";
                                //if (!CommonSet.dic_Assemble1[index].imgResultDown.bImageResult)
                                //    GetProduct1Module.lstNGSuction.Add(pair.Key);
                            }
                            if (!bFlashFinish)
                                break;
                        }
                        //判断镜筒结果
                        bFlashFinish = bFlashFinish && AssembleSuction2.imgResultUp.bStatus;
                        //判断镜筒结果 
                        if (!AssembleSuction2.imgResultUp.bImageResult&&!CommonSet.bForceOk)
                        {
                            if ((Run.runMode == RunMode.运行)||(Run.runMode == RunMode.暂停))
                            {
                                sw.ResetAlarmWatch();
                                //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                Run.iDebugResult = 2;
                                CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, 5, "组装2镜筒检测NG", false);
                                Run.runMode = RunMode.暂停;
                            }
                            bImageResult = false;
                            Alarminfo.AddAlarm(Alarm.组装2镜筒检测NG);
                            WriteOutputInfo(strOut + "组装2镜筒检测NG");
                            step = lstAction.IndexOf(ActionName._30开始抛料);
                            break;
                        }
                        //bFlashFinish = true;
                        //bResult = true;
                        //如果图像已处理完成
                        if (bFlashFinish||CommonSet.bTest|| CommonSet.bForceOk)
                        {
                           
                            //strWrite = strWrite.Remove(strWrite.LastIndexOf(','));
                           // WriteResult(CommonSet.strReportPath + "\\FlashTest1\\", strWrite);
                            WriteOutputInfo(strOut + "飞拍完成");
                            if (CommonSet.bTest)
                                bResult = true;
                            //如果所有图像结果OK
                            if (bResult || CommonSet.bForceOk)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 1;

                                    break;
                                }
                                //如果是强制OK，则抛料
                                if (CommonSet.bForceOk)
                                {
                                    CommonSet.bForceOk = false;
                                    bImageResult = false;
                                    step = lstAction.IndexOf(ActionName._30开始抛料);
                                    break;
                                }
                                else
                                    bImageResult = true;

                                Point pCenter = new Point();
                                pCenter.X = AssembleSuction2.imgResultUp.CenterRow;
                                pCenter.Y = AssembleSuction2.imgResultUp.CenterColumn;
                                //lstLenCenter.Add(pCenter);
                                lstLenCenter[0] = pCenter;
                                //state1 = ActionState.组装;
                                step = step + 1;
                                break;
                            }
                            else
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    sw.ResetAlarmWatch();
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.组装2飞拍图像NG);
                                    WriteOutputInfo(strOut + "组装2飞拍图像NG");
                                    break;
                                }
                                //如果检测有NG的重新飞拍一次
                                if (iFlashTimes < 2)
                                {
                                    step = lstAction.LastIndexOf(ActionName._30X轴到取料位);
                                    break;
                                }
                                else
                                {
                                    //判断镜筒结果 
                                    
                                        //if ((Run.runMode == RunMode.运行))
                                        //{
                                        //    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                        //   // Run.iDebugResult = 2;
                                        //    CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, 5, "组装2飞拍图像NG", false);
                                        //    sw.ResetAlarmWatch();
                                        //    Run.runMode = RunMode.暂停;
                                        //}

                                        //Alarminfo.AddAlarm(Alarm.组装2飞拍图像NG);
                                        WriteOutputInfo(strOut + "组装2飞拍图像NG");
                                   
                                        bImageResult = false;
                                        step = lstAction.IndexOf(ActionName._30开始抛料);
                                }
                               

                            }

                           
                        }
                        else
                        {

                            //if (RunMode.测试 == Run.runMode)
                            //{
                            //    step = step + 1;
                            //    break;
                            //}

                            if (CommonSet.bTest)
                            {
                                step = step + 1;
                                break;
                            }
                            if (Run.runMode == RunMode.暂停)
                            {
                                if (sw.AlarmWaitTime(3000))
                                {
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.组装下相机2飞拍超时);
                                    WriteOutputInfo(strOut + "组装下相机2飞拍超时");
                                    sw.ResetAlarmWatch();
                                }
                                break;
                            }
                            //如果超时
                            if (sw.AlarmWaitTime(5000))
                            {

                                if (Run.runMode == RunMode.运行)
                                {
                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);

                                    CommonSet.AlarmProcessShow(AlarmBlock.组装2, step, 5, "组装下相机2飞拍超时", false);
                                    Run.runMode = RunMode.暂停;
                                }
                              
                                Alarminfo.AddAlarm(Alarm.组装下相机2飞拍超时);
                                WriteOutputInfo(strOut + "组装下相机2飞拍超时");
                                step = lstAction.IndexOf(ActionName._30开始抛料);
                            }

                        }
                        break;
                    case ActionName._30抛料完成:
                         if (iCurrentSuctionOrder < iMaxSuctionOrder + 1)
                        {
                            IStep = lstAction.LastIndexOf(ActionName._30获取组装吸笔);
                            break;
                        }
                        if (!IsAxisINP(dDestPosZ, AXIS.组装Z2轴))
                        {
                            break;
                        }
                        iCurrentSuctionOrder = 9;

                        iCurrentSolution = 0;
                      //  CommonSet.iAssembleNum++;
                        if (iCurrentStation == 1)
                        {
                            ParamListerner.station1.bFinish2 = false;
                            if (CommonSet.bTest)
                                ParamListerner.station1.bFinish2 = true;
                            //ParamListerner.station1.bFinish1 = false;//局部联动
                            //ParamListerner.station2.bGet2 = false;
                            //ParamListerner.station1.bGet1 = false;
                        }
                        else if (iCurrentStation == 2)
                        {
                            ParamListerner.station2.bFinish2 = false;
                                  if(CommonSet.bTest)
                                ParamListerner.station2.bFinish2 = true;;
                            //ParamListerner.station2.bFinish1 = false;//局部联动
                            //ParamListerner.station2.bGet1 = false;
                            //ParamListerner.station1.bGet2 = false;
                        }
                         dDestPosX = AssembleSuction2.pSafeXYZ.X;
                          mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        // barrelData.WriteResult(CommonSet.strReportPath + "\\AssembleData\\");
                       // AssemState2 = ActionState.组装完成;
                        WriteOutputInfo(strOut + "抛料完成" + iCurrentStation.ToString());
                        step = 0;
                        CommonSet.swCircleC2.Stop();//计时器复位，重新开始计时
                        break;
                    case ActionName._30组装完成:

                        if (iCurrentSuctionOrder < iMaxSuctionOrder + 1)
                        {
                            IStep = lstAction.IndexOf(ActionName._30获取组装吸笔);
                            break;
                        }
                        if (!IsAxisINP(dDestPosZ, AXIS.组装Z2轴))
                        {
                            break;
                        }
                        iCurrentSuctionOrder = 9;

                        iCurrentSolution = 0;
                        CommonSet.iAssembleNum++;
                        if (iCurrentStation == 1)
                        {
                            ParamListerner.station1.bFinish2 = true;
                            //ParamListerner.station1.bFinish1 = false;//局部联动
                            //ParamListerner.station2.bGet2 = false;
                            //ParamListerner.station1.bGet1 = false;
                            mc.setDO(DO.组装位1真空吸, false);
                        }
                        else if (iCurrentStation == 2)
                        {
                            ParamListerner.station2.bFinish2 = true;
                            //ParamListerner.station2.bFinish1 = false;//局部联动
                            //ParamListerner.station2.bGet1 = false;
                            //ParamListerner.station1.bGet2 = false;
                            mc.setDO(DO.组装位2真空吸, false);
                        }
                       
                        WriteOutputInfo(strOut + "组装完成"+iCurrentStation.ToString());
                        iCurrentStation = 0;
                        // barrelData.WriteResult(CommonSet.strReportPath + "\\AssembleData\\");
                       // AssemState2 = ActionState.组装完成;
                        step = 0;
                        dDestPosX = AssembleSuction2.pSafeXYZ.X;
                        mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        CommonSet.swCircleC2.Stop();//计时器复位，重新开始计时
                         WriteResult(CommonSet.strReportPath + "LenCenter2\\","");
                        break;




                    case ActionName._30Z轴到组装位:
                        int v = 0;
                        if (CommonSet.dic_Assemble2[iCurrentSuctionOrder].bTwo)
                        {

                            v = (int)CommonSet.dVelAssemble2;
                        }
                        else
                        {
                            if (CommonSet.bTest)
                            {
                                v = (int)CommonSet.dVelRunZ;
                            }
                            else
                            {
                                v = (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel;
                                //设定组装压力
                                //SerialAV.WriteOutputA((int)SerialAV.iWriteAddr, SerialAV.GetAByPressure(CommonSet.dic_AssemblySuction[iCurrentSuctionOrder].dPressureSet));
                                float dOutv = AssemblePressureRelation.GetVBySuction2(iCurrentSuctionOrder, CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPressureSet);
                                //float dOutv = CommonSet.fCommonPressureV2;
                                mc.WriteOutDA(dOutv, 2);
                            }
                        }
                        //设定组装压力
                        // SerialAV.WriteOutputA((int)SerialAV.iWriteAddr, SerialAV.GetAByPressure(CommonSet.dic_AssemblySuction[iCurrentSuctionOrder].dPressureSet));
                        if (iCurrentStation == 1)
                        {
                            if (CommonSet.bTest)
                            {
                                //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ1;
                                //dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH-10;
                                dDestPosZ =  CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - 10;
                            }
                            else
                            {
                                //dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH;
                                dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleOver;

                            }
                            if (!CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.bImageResult)
                            {
                                dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                            }
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ1;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, v);
                            WriteOutputInfo(strOut + "组装Z2轴到组装位" + CommonSet.dic_Assemble2[iCurrentSuctionOrder].pAssembly1.Z.ToString());
                        }
                        else if (iCurrentStation == 2)
                        {
                            if (CommonSet.bTest)
                            {
                               // dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ2;
                               // dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH-10;
                                dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - 10;
                            }
                            else
                            {
                                //dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH;
                                dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH2 + CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleOver;

                            }
                            if (!CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.bImageResult)
                            {
                                dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                            }
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ2;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, v);
                            WriteOutputInfo(strOut + "组装Z2轴到组装位" + CommonSet.dic_Assemble2[iCurrentSuctionOrder].pAssembly2.Z.ToString());
                        }

                        step = step + 1;
                        break;
                    case ActionName._30Z轴到组装准备位:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;

                        }
                        else
                        {
                            float dOutv = AssemblePressureRelation.GetVBySuction2(iCurrentSuctionOrder, CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPressureSet);
                            //float dOutv = CommonSet.fCommonPressureV2;
                            mc.WriteOutDA(dOutv, 2);
                        }
                        if (iCurrentStation == 1)
                        {

                            //dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dDist2;
                            dDestPosZ =  CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH1;
                            if (!CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.bImageResult)
                            {
                                dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                            }
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ1;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                            WriteOutputInfo(strOut + "组装Z2轴到组装准备位" + dDestPosZ.ToString());
                        }
                        else if (iCurrentStation == 2)
                        {

                           // dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dDist2;
                            dDestPosZ =  CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH2;
                            if (!CommonSet.dic_Assemble2[iCurrentSuctionOrder].imgResultDown.bImageResult)
                            {
                                dDestPosZ = AssembleSuction2.pSafeXYZ.Z;
                            }
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ2;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                            WriteOutputInfo(strOut + "组装Z2轴到组装准备位" + dDestPosZ.ToString());
                        }
                        dTestZStep = 0.1;//初始化自动测高的步长
                        step = step + 1;
                        break;
                    case ActionName._30Z轴到组装测压位:
                        if (!CommonSet.bAutoTestHeight || !CommonSet.dic_Assemble2[iCurrentSuctionOrder].bAutoTestHeight)
                        {
                            step = step + 1;
                            break;
                        }
                        dDestPosZ = mc.dic_Axis[AXIS.组装Z2轴].dPos + dTestZStep;
                        mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                        WriteOutputInfo(strOut + "组装Z2轴到组装测压位" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._30Z轴测压判断:
                        if (!CommonSet.bAutoTestHeight || !CommonSet.dic_Assemble2[iCurrentSuctionOrder].bAutoTestHeight)
                        {
                            step = step + 1;
                            break;
                        }
                        #region"判断"
                        if (sw.WaitSetTime(100))
                        {
                            //如果压力小于设定压力-1N
                            double dP = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPressureSet;
                            if (iCurrentStation == 1)
                            {
                                //超出平台高度，测停止
                                if (mc.dic_Axis[AXIS.组装Z2轴].dPos > AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - 0.1)
                                {
                                    step = step + 3;
                                    break;
                                }
                                if (SerialPortMeasurePressure.dStation1 >= dP - 1)
                                {
                                    step = step + 3;
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation1.ToString());
                                    break;
                                }
                                //如果小于2N,则按设定步长下压
                                if (SerialPortMeasurePressure.dStation1 < 5)
                                {
                                    dDestPosZ = mc.dic_Axis[AXIS.组装Z2轴].dPos + dTestZStep;
                                    mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + dDestPosZ.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation1.ToString());
                                    step = step - 1;
                                }
                                else if ((SerialPortMeasurePressure.dStation1 >= 2) && (SerialPortMeasurePressure.dStation1 < dP))
                                {
                                    dTestZStep = 0.002;
                                    dDestPosZ = mc.dic_Axis[AXIS.组装Z2轴].dPos + dTestZStep;
                                    mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + dDestPosZ.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation1.ToString());
                                    step = step - 1;
                                }
                                else
                                {
                                    step = step + 3;
                                }
                            }
                            else
                            {
                                //超出平台高度，测停止
                                if (mc.dic_Axis[AXIS.组装Z2轴].dPos > AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - 0.1)
                                {
                                    step = step + 3;
                                    break;
                                }
                                if (SerialPortMeasurePressure.dStation2 >= dP - 1)
                                {
                                    step = step + 3;
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation2.ToString());
                                    break;
                                }
                                //如果小于2N,则按设定步长下压
                                if (SerialPortMeasurePressure.dStation2 < 2)
                                {
                                    dDestPosZ = mc.dic_Axis[AXIS.组装Z2轴].dPos + dTestZStep;
                                    mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + dDestPosZ.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation2.ToString());
                                    step = step - 1;
                                }
                                else if ((SerialPortMeasurePressure.dStation2 >= 2) && (SerialPortMeasurePressure.dStation2 < dP))
                                {
                                    dTestZStep = 0.002;
                                    dDestPosZ = mc.dic_Axis[AXIS.组装Z2轴].dPos + dTestZStep;
                                    mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + dDestPosZ.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation2.ToString());
                                    step = step - 1;
                                }
                                else
                                {
                                    WriteOutputInfo(strOut + "组装Z2轴测压判断,当前位置：" + mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString() + ",当前压力:" + SerialPortMeasurePressure.dStation2.ToString());
                                    step = step + 3;
                                }
                            }

                        }
                        #endregion

                        break;
                    case ActionName._30Z轴到组装上升位:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;
                        }
                        if (iCurrentStation == 1)
                        {

                           // dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dDist2;
                            dDestPosZ =  CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH1;
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ1;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                            WriteOutputInfo(strOut + "组装Z2轴到组装上升位" + dDestPosZ.ToString());
                        }
                        else if (iCurrentStation == 2)
                        {

                           // dDestPosZ = AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuctionOrder - 10].PosZ - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssembleH - CommonSet.dic_Assemble2[iCurrentSuctionOrder].dDist2;
                            dDestPosZ =  CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPreH2;
                            //dDestPosZ = CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblePosZ2;
                            mc.AbsMove(AXIS.组装Z2轴, dDestPosZ, (int)CommonSet.dic_Assemble2[iCurrentSuctionOrder].dAssemblVel);
                            WriteOutputInfo(strOut + "组装Z2轴到组装上升位" + dDestPosZ.ToString());
                        }

                        step = step + 1;
                        break;

                    case ActionName._30Z轴到组装位完成:
                        //设定组装压力
                        // SerialAV.WriteOutputA((int)SerialAV.iWriteAddr, SerialAV.GetAByPressure(CommonSet.dic_AssemblySuction[iCurrentSuctionOrder].dPressureSet));
                        if (mc.dic_Axis[AXIS.组装Z2轴].INP)
                        {
                            WriteOutputInfo(strOut + "Z轴到组装位完成");
                            step = step + 1;
                        }

                        break;


                    case ActionName._30吸笔下降:
                        //设定组装压力
                        // SerialAV.WriteOutputA((int)SerialAV.iWriteAddr, SerialAV.GetAByPressure(AssembleSuction.dPresureUsual));
                        mc.WriteOutDA(CommonSet.fCommonPressureV2, 2);
                        if (iCurrentSuctionOrder < iMaxSuctionOrder)
                        {
                            strD = "组装气缸下降" + (iCurrentSuctionOrder + 1).ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);

                            WriteOutputInfo(strOut + "组装吸笔" + (iCurrentSuctionOrder + 1).ToString() + "下降");
                        }
                        step = step + 1;

                        break;
                    case ActionName._30吸笔上升:
                        mc.WriteOutDA(CommonSet.fCommonPressureV2, 2);
                        strD = "组装气缸下降" + iCurrentSuctionOrder.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "上升");
                        step = step + 1;
                        break;
                    case ActionName._30吸笔下降判定:
                        mc.WriteOutDA(CommonSet.fCommonPressureV2, 2);
                        strD = "组装气缸下降" + (iCurrentSuctionOrder).ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        strD = "组装升降气缸" + iCurrentSuctionOrder.ToString() + "下检测";
                        dIn = (DI)Enum.Parse(typeof(DI), strD);
                        if (mc.dic_DI[dIn] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "下降完成");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                //step = step + 1;
                                Alarminfo.AddAlarm(Alarm.组装吸笔1下降超时 + iCurrentSuctionOrder - 1);
                                WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "下降超时");
                            }
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
                        long putTime = (long)(CommonSet.dic_Assemble2[iCurrentSuctionOrder].dPutDelay * 1000);

                        if (sw.WaitSetTime(putTime))
                        {
                           
                            mc.setDO(dOut, false);

                            WriteOutputInfo(strOut + "组装吸笔" + iCurrentSuctionOrder.ToString() + "破真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._30读取数据:
                        if (!sw.WaitSetTime(100))
                        {
                            break;
                        }
                        //获取吸笔高度和压力值
                        if (iCurrentStation == 1)
                        {
                            ParamListerner.station1.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation1;
                            ParamListerner.station1.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation1;
                            ParamListerner.station1.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                        }
                        else
                        {
                            ParamListerner.station2.lstHeight[iCurrentSuctionOrder - 1] = SerialPortMeasureHeight.dStation2;
                            ParamListerner.station2.lstPressure[iCurrentSuctionOrder - 1] = SerialPortMeasurePressure.dStation2;
                            ParamListerner.station2.lstZPos[iCurrentSuctionOrder - 1] = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                        }
                        WriteOutputInfo(strOut + "读取组装吸笔" + iCurrentSuctionOrder.ToString() + "数据");
                        step = step + 1;
                        break;

                }

            }
            catch (Exception ex)
            {
                WriteOutputInfo(strOut + "Assemb2Module运行异常" + ex.ToString());

            }
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
                    string strHead = "时间,工位,";
                    for (int i = 1; i < 10; i++)
                    {

                        strHead += CommonSet.dic_OptSuction2[i + 9].Name + "C" + (i + 9).ToString() + "_Row," + CommonSet.dic_OptSuction2[i+9].Name + "C" + (i+9).ToString() + "_Col,";
                    }

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
                int count = lstLenCenter.Count;
                for (int i = 0; i < count; i++)
                {
                    strContent += lstLenCenter[i].X + "," + lstLenCenter[i].Y + ",";
                }
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + ","+iCurrentStation.ToString()+"," + strContent);
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
        private void WriteCenterDiffFile(string strPath)
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

        public void WriteCenterDiffResult(string strPath, string strContent)
        {
            Action<string> dele = WriteCenterDiffFile;
            dele.BeginInvoke(strPath, WriteCallBack, null);
        }
    }
}
