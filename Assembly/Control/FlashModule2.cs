using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    public class FlashModule2:ActionModule
    {
        private string strOut = "Flash2Module-Action-";
        //private static Flash2Module module = null;
        // public static int iCurrentSuction = 1;//当前吸笔序号
        // public static List<int> lstNGSuction = new List<int>();//飞拍NG的图片
        // private Point currentPoint = null;
        //private int iCurrentTimes = 1;
        //private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static bool bFlashOK = false;//飞拍OK
        public static bool bFlashNG = false;//飞拍NG   
        // public static bool bClearFlag = false;//自动清料标志
        public static int iPicNum = 1;//拍照顺序
        public static int iPicSum = 0;//拍照总数,飞拍之前赋值确认
        private static int iFlashTimes = 1;//飞拍次数计数
        public static int iResetStep = 0;
        private string strD = "";
        private DO dOut;
        private DI dIn;
        public static bool bPut = false;//中转位是否有料
        public static Dictionary<int, bool> dic_BRotate = new Dictionary<int, bool>();//是否旋转角度
        public override void Action2()
        {

        }
        public FlashModule2()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._20等待飞拍);
            ////先夹，后回来飞拍
            //lstAction.Add(ActionName._20Z轴到飞拍位);
            //lstAction.Add(ActionName._20所有吸笔下降);
            //lstAction.Add(ActionName._20Z轴到位);
            //lstAction.Add(ActionName._20求心张开);
            //lstAction.Add(ActionName._20X轴到飞拍结束位校正);
            //lstAction.Add(ActionName._20X到位完成);
            //lstAction.Add(ActionName._20求心张开检测);
            //lstAction.Add(ActionName._20Z轴到求心位);
            //lstAction.Add(ActionName._20Z轴到位);
            //lstAction.Add(ActionName._20求心闭合);
            //lstAction.Add(ActionName._20求心张开);
            //lstAction.Add(ActionName._20求心张开检测);
            //lstAction.Add(ActionName._20Z轴到飞拍位);
            //lstAction.Add(ActionName._20Z轴到位);
            /////////////////////////////////////////


            lstAction.Add(ActionName._20Z轴到飞拍位);
            lstAction.Add(ActionName._20X轴到飞拍起始位);
            lstAction.Add(ActionName._20所有吸笔下降);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20求心张开);
            lstAction.Add(ActionName._20X到位完成);
            lstAction.Add(ActionName._20开始飞拍);
            lstAction.Add(ActionName._20X轴到飞拍结束位);
            lstAction.Add(ActionName._20X到位完成);
            lstAction.Add(ActionName._20飞拍结束);
            //lstAction.Add(ActionName._20求心张开检测);
            //lstAction.Add(ActionName._20Z轴到求心位);
            //lstAction.Add(ActionName._20Z轴到位);
            //lstAction.Add(ActionName._20求心闭合);
            //lstAction.Add(ActionName._20求心张开);
            lstAction.Add(ActionName._20求心张开检测);
            lstAction.Add(ActionName._20Z轴到放料位);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20中转位取放料);

            lstAction.Add(ActionName._20所有吸笔上升);
            lstAction.Add(ActionName._20Z轴到安全位);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20中转位真空检测);
            lstAction.Add(ActionName._20放料完成);
           
           

            //如果有NG的，则执行抛料动作
            lstAction.Add(ActionName._20Z轴到安全位);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20XY到抛料位);
            lstAction.Add(ActionName._20XY到位完成);
            lstAction.Add(ActionName._20Z轴到抛料位);
            lstAction.Add(ActionName._20NG吸笔下降);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20NG吸笔破真空);
            lstAction.Add(ActionName._20所有吸笔上升);
            lstAction.Add(ActionName._20Z轴到安全位);
            lstAction.Add(ActionName._20Z轴到位);
            lstAction.Add(ActionName._20抛料完成);
           
        }

        public override void Reset()
        {
            switch (iResetStep)
            {
                case 0:
                    if (bResetFinish)
                        break;
                    IStep = 0;
                    iPicNum = 1;
                    iPicSum = 0;
                    iFlashTimes = 0;
                    bPut = false;
                    mc.setDO(DO.中转2夹紧, false);
                    OptPutSuction2(false);
                    foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                    {

                        OptSution2 p = pair.Value;
                        if (p.BUse)
                        {
                            strD = "取料吸真空" + p.SuctionOrder.ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);
                            strD = "取料破真空" + p.SuctionOrder.ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);

                            strD = "中转吸真空" + p.SuctionOrder.ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);

                        }


                    }
                    iResetStep = iResetStep + 1;

                    break;
                case 1:
                    iResetStep = 0;
                    bResetFinish = true;
                    break;
                default:
                    iResetStep = 0;
                    bResetFinish = false;
                    break;
            }
        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                {
                    case ActionName._20等待飞拍:
                        if (GetState2 == ActionState.飞拍和放料)
                        {

                            bool bCheckFlag1 = true;
                            if (BarrelSuction.bUseAssem1 && BarrelSuction.bUseAssem2)
                            {
                                bCheckFlag1 = mc.dic_DO[DO.组装吸真空10] || mc.dic_DO[DO.组装吸真空11] || mc.dic_DO[DO.组装吸真空12] || mc.dic_DO[DO.组装吸真空13] || mc.dic_DO[DO.组装吸真空14] || mc.dic_DO[DO.组装吸真空15];
                                if (bPut || ((Assem2Module.iCurrentSuctionOrder < 16) && (bCheckFlag1)))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                bCheckFlag1 = mc.dic_DO[DO.组装吸真空10] || mc.dic_DO[DO.组装吸真空11] || mc.dic_DO[DO.组装吸真空12] || mc.dic_DO[DO.组装吸真空13] || mc.dic_DO[DO.组装吸真空14] || mc.dic_DO[DO.组装吸真空15] || mc.dic_DO[DO.组装吸真空16] || mc.dic_DO[DO.组装吸真空17] || mc.dic_DO[DO.组装吸真空18];
                                if (bPut || ((Assem2Module.iCurrentSuctionOrder < 19) && (bCheckFlag1)))
                                {
                                    break;
                                }
                            }
                            
                            //当已经设定好镜筒序号
                            if (GetProduct2Module.iCurrentBarrel > 0)
                            {
                                //设定压力
                                // SerialAV.WriteOutputA(1, SerialAV.GetAByPressure(ProductSuction.dPresureUsual));
                                iFlashTimes = 0;
                                WriteOutputInfo(strOut + "开始飞拍");
                                step = step + 1;
                            }
                        }

                        break;
                    case ActionName._20Z轴到飞拍位:
                        dDestPosZ = OptSution2.pSafeXYZ.Z;
                        //dDestPosZ = OptSution2.dFlashZ;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到飞拍高度：" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到抛料位:
                        dDestPosZ = OptSution2.pDiscard.Z;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到抛料位：" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到放料位:
                        //等待旋转完成0.5s后，再放料
                        if (!sw.WaitSetTime(500))
                        {
                           
                            break;
                        }
                        dDestPosZ = OptSution2.pPutPos.Z;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到放料位：" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到求心位:
                        dDestPosZ = OptSution2.DCorrectPosZ;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到取料位");
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到安全位:
                        dDestPosZ = OptSution2.pSafeXYZ.Z;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到安全位：" + dDestPosZ.ToString());
                        step = step + 1;
                        break;
                    case ActionName._20Z轴到位:
                        if (IsAxisINP(dDestPosZ, AXIS.取料Z2轴))
                        {
                            WriteOutputInfo(strOut + "取料Z2轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._20求心闭合:
                        mc.setDO(DO.中转2夹紧, true);
                        if (CommonSet.bTest || mc.dic_DI[DI.中转2夹紧气缸闭合检测])
                        {
                            long lc = (long)(OptSution2.DCorrectTime * 1000);
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
                    case ActionName._20中转位取放料:
                        mc.setDO(DO.中转吸真空气源2, true);
                        bool bCheck = true;
                        foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                        {

                            OptSution2 p = pair.Value;
                            if (p.BUse)
                            {
                                strD = "取料吸真空" + p.SuctionOrder.ToString();
                                dOut = (DO)Enum.Parse(typeof(DO), strD);
                                mc.setDO(dOut, false);
                                strD = "取料破真空" + p.SuctionOrder.ToString();
                                dOut = (DO)Enum.Parse(typeof(DO), strD);
                                mc.setDO(dOut, CommonSet.dic_OptSuction2[p.SuctionOrder].bPut);

                                strD = "中转吸真空" + p.SuctionOrder.ToString();
                                dOut = (DO)Enum.Parse(typeof(DO), strD);
                                mc.setDO(dOut, true);

                                strD = "中转真空检测" + p.SuctionOrder.ToString();
                                dIn = (DI)Enum.Parse(typeof(DI), strD);
                                bCheck = bCheck && mc.dic_DI[dIn];

                            }


                        }
                        //if (bCheck || CommonSet.bUnableCheckSuction)
                        //{
                        long lPutTime = (long)(OptSution2.dPutTime * 1000);
                        if (sw.WaitSetTime(lPutTime))
                        {
                            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                            {

                                OptSution2 p = pair.Value;
                                if (p.BUse)
                                {
                                  
                                    strD = "取料破真空" + p.SuctionOrder.ToString();
                                    dOut = (DO)Enum.Parse(typeof(DO), strD);
                                    mc.setDO(dOut,false);

                                }


                            }

                            WriteOutputInfo(strOut + "中转位取放料");
                            step = step + 1;
                        }
                        //}
                        //else
                        //{
                        //    if (sw.AlarmWaitTime(iActionTime))
                        //    {
                        //        Alarminfo.AddAlarm(Alarm.中转位工位2吸真空检测异常);
                        //        WriteOutputInfo(strOut + "中转位工位2中转位吸真空检测异常");
                        //    }
                        //}


                        break;
                    case ActionName._20中转位真空检测:
                        bool bFlag = true;
                        foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                        {

                            OptSution2 p = pair.Value;
                            if (p.BUse)
                            {
                                strD = "中转真空检测" + p.SuctionOrder.ToString();
                                dIn = (DI)Enum.Parse(typeof(DI), strD);
                                bFlag = bFlag && mc.dic_DI[dIn];
                            }


                        }
                        if (bFlag || CommonSet.bTest || CommonSet.bForceOk)
                        {
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 1;

                                break;
                            }
                            CommonSet.bForceOk = false;
                            WriteOutputInfo(strOut + "中转位2真空检测");
                            step = step + 1;
                        }
                        else
                        {
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 2;
                                Alarminfo.AddAlarm(Alarm.中转位工位2吸真空检测异常);
                                WriteOutputInfo(strOut + "中转位工位2吸真空检测异常");
                                break;
                            }
                            if (sw.AlarmWaitTime(1000))
                            {
                                if (Run.runMode == RunMode.运行)
                                {
                                    // Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);

                                    CommonSet.AlarmProcessShow(AlarmBlock.飞拍2, step, 0, "中转位工位2吸真空检测异常", false);
                                    Run.runMode = RunMode.暂停;
                                }
                                Alarminfo.AddAlarm(Alarm.中转位工位2吸真空检测异常);
                                WriteOutputInfo(strOut + "工位2中转位吸真空检测异常");
                            }
                        }
                        break;
                    case ActionName._20求心张开:
                        mc.setDO(DO.中转2夹紧, false);
                        WriteOutputInfo(strOut + "求心2张开");
                        step = step + 1;
                        //if (CommonSet.bTest || !mc.dic_DI[DI.求心1夹子1闭合] && !mc.dic_DI[DI.求心1夹子2闭合])
                        //{

                        //        WriteOutputInfo(strOut + "求心1张开");
                        //        step = step + 1;

                        //}
                        //else {
                        //    if (sw.AlarmWaitTime(iActionTime))
                        //    {
                        //        Alarminfo.AddAlarm(Alarm.求心1张开超时);
                        //        WriteOutputInfo(strOut + "求心1张开超时");
                        //    }
                        //}
                        break;
                    case ActionName._20求心张开检测:
                        if (CommonSet.bTest || mc.dic_DI[DI.中转2夹紧气缸张开检测])
                        {
                            if (sw.WaitSetTime(500))
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



                    case ActionName._20所有吸笔上升:
                        OptPutSuction2(false);
                        if (OptCheckSuctionUp2() || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "所有吸笔上升");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.飞拍2全部吸笔上升超时);
                                WriteOutputInfo(strOut + "飞拍2全部吸笔上升超时");
                            }
                        }
                        break;
                    case ActionName._20所有吸笔下降:
                        //设定压力
                        // SerialAV.WriteOutputA(1, SerialAV.GetAByPressure(ProductSuction.dPresureUsual));

                        OptPutSuction2(true);
                        if (OptCheckSuctionDown2() || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "所有吸笔下降");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.飞拍2全部吸笔下降超时);
                                WriteOutputInfo(strOut + "飞拍2全部吸笔下降超时");
                            }
                        }
                        break;

                    case ActionName._20X轴到飞拍起始位:
                        dDestPosX = OptSution2.dFlashStartX;
                        mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "取料X2轴到飞拍起始位");
                        step = step + 1;

                        break;
                    case ActionName._20X轴到飞拍结束位:
                        dDestPosX = OptSution2.pPutPos.X;
                        mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "取料X2轴到飞拍结束位");
                        step = step + 1;

                        break;
                    case ActionName._20X轴到飞拍结束位校正:
                         dDestPosX = OptSution2.pPutPos.X;
                         mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "取料X2轴到飞拍结束位校正");
                        step = step + 1;

                        break;
                    case ActionName._20X到位完成:
                        if (IsAxisINP(dDestPosX, AXIS.取料X2轴))
                        {
                            WriteOutputInfo(strOut + "取料X2轴到位完成");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20开始飞拍:
                        bool bCheckFlag = mc.dic_DO[DO.组装吸真空10] || mc.dic_DO[DO.组装吸真空11] || mc.dic_DO[DO.组装吸真空12] || mc.dic_DO[DO.组装吸真空13] || mc.dic_DO[DO.组装吸真空14] || mc.dic_DO[DO.组装吸真空15];

                        if (bPut || ((Assem2Module.iCurrentSuctionOrder < 16) && (bCheckFlag)))
                        {
                            break;
                        }
                        //if (CommonSet.bTest)
                        //{

                        //    step = step + 1;
                        //    break;
                        //}
                        int sum = 0;
                        
                        iFlashTimes++;
                        CommonSet.iCameraDownFlag = 1;
                        //foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                        //{
                        //    if (pair.Value.BUse&&(pair.Key < 10))
                        //    {
                        //        iPicSum++;                               
                        //    }

                        //}
                        //打开光源
                        //int channel = LightManager.iLightDownChannel;
                        //CommonSet.lc.OpenChanel(channel);
                        List<double> lstFlashData = new List<double>();
                        dic_BRotate.Clear();
                        foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                        {
                            if (pair.Value.BUse && (pair.Key < 19))
                            {
                                sum++;
                                int index = pair.Value.SuctionOrder;
                                double pos = (double)((CommonSet.dic_OptSuction2[index].DPosCameraDownX + OptSution2.dFalshMakeUpX));

                                lstFlashData.Add(pos);
                                string strProcessName = CommonSet.dic_OptSuction2[index].strPicDownProduct;
                                CommonSet.dic_OptSuction2[index].InitImageDown(strProcessName);
                                dic_BRotate.Add(index,false);
                            }
                        }
                        //lstFlashData.Reverse();
                        mc.startCmpTrigger(AXIS.取料X2轴, lstFlashData.ToArray(), (ushort)1);
                        CommonSet.camDownA2.SetExposure(OptSution2.dExposureTimeDown);
                        CommonSet.camDownA2.SetGain(OptSution2.dGainDown);
                        WriteOutputInfo(strOut + "取料X2轴开始飞拍");
                        iPicNum = 1;
                        iPicSum = sum;
                        step = step + 1;
                        break;
                    case ActionName._20飞拍结束:
                        mc.stopCmpTrigger(AXIS.取料X2轴, (ushort)1);

                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;
                        }
                        //关闭光源
                        //int channel1 = LightManager.iLightDownChannel;
                        //CommonSet.lc.CloseChannel(channel1);
                        //触发关闭
                        //mc.stopCmpTrigger(AXIS.X1轴);
                        bool bFlashFinish = true;//图像处理完成标志
                        bool bResult = true; //图像处理结果（所有结果进行并运算）
                        string strWrite = "";
                        if (iPicSum != 0)
                        {
                            bool bIsOutTime = sw.AlarmWaitTime(3000);
                            if (bIsOutTime)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    sw.ResetAlarmWatch();
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.取料2飞拍图像数目异常);
                                    WriteOutputInfo(strOut + "取料2飞拍图像数目异常");
                                    step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                    break;
                                }
                                //如果超时一次,重新飞拍一次
                                if (iFlashTimes < 2)
                                {
                                    step = lstAction.IndexOf(ActionName._20X轴到飞拍起始位);

                                }
                                else
                                {
                                    if (Run.runMode == RunMode.运行)
                                    {
                                        // Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                        int iIndex = lstAction.IndexOf(ActionName._20X轴到飞拍起始位);
                                        CommonSet.AlarmProcessShow(AlarmBlock.飞拍2, step, step - iIndex, "取料2飞拍图像数目异常", false);
                                        Run.runMode = RunMode.暂停;
                                    }

                                    Alarminfo.AddAlarm(Alarm.取料2飞拍图像数目异常);
                                    WriteOutputInfo(strOut + "取料2飞拍图像数目异常");
                                    step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                }

                            }
                            break;
                        }
                           int iCurrentSolution = CommonSet.asm.getSolution(GetProduct2Module.iCurrentBarrel);
                               
                        GetProduct2Module.lstNGSuction.Clear();
                        foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                        {
                            if (pair.Value.BUse && (pair.Key < 19))
                            {
                                int index = pair.Value.SuctionOrder;
                                bFlashFinish = bFlashFinish && CommonSet.dic_OptSuction2[index].imgResultDown.bStatus;
                                bResult = bResult && CommonSet.dic_OptSuction2[index].imgResultDown.bImageResult;
                                strWrite += CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow + "," + CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn + ",";
                                if (!CommonSet.dic_OptSuction2[index].imgResultDown.bImageResult)
                                    GetProduct2Module.lstNGSuction.Add(pair.Key);
                                if (CommonSet.dic_OptSuction2[index].imgResultDown.bImageResult && !dic_BRotate[index])
                                {
                                    dic_BRotate[index] = true;
                                    //旋转
                                    double dAngle = CommonSet.asm.dic_Solution[iCurrentSolution].lstAngle[index - 1];
                                    double dRotateAngle = CommonSet.dic_OptSuction2[index].imgResultDown.dAngle - dAngle;
                                    string strAxis = "取料C" + index + "轴";
                                    AXIS axisC = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
                                    double dCurrentPos = mc.dic_Axis[axisC].dPos;
                                    double dRAngle = (dRotateAngle - ParamListerner.GetLenAngle(GetProduct2Module.iCurrentBarrel)-360)%360+360;
                                    mc.AbsMove(axisC, dCurrentPos +dRAngle , (int)CommonSet.dVelC);
                                    WriteOutputInfo(strOut + "镜筒角度:" + ParamListerner.GetLenAngle(GetProduct2Module.iCurrentBarrel).ToString() + "镜片角度:"+ CommonSet.dic_OptSuction2[index].imgResultDown.dAngle.ToString()+ strAxis + "旋转" + dRAngle.ToString());
                                
                                }
                            }
                            if (!bFlashFinish)
                                break;
                        }
                        //如果图像已处理完成
                        if (bFlashFinish || CommonSet.bForceOk)
                        {
                            
                           
                            //strWrite = strWrite.Remove(strWrite.LastIndexOf(','));
                            //WriteResult(CommonSet.strReportPath + "\\FlashTest2\\", strWrite);
                            WriteOutputInfo(strOut + "飞拍2完成"+CommonSet.bForceOk.ToString());
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
                                if (CommonSet.bForceOk)
                                {
                                    CommonSet.bForceOk = false;
                                    if (GetProduct2Module.lstNGSuction.Count > 0)
                                    {
                                        step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                        break;
                                    }
                                }
                                //state1 = ActionState.组装;
                                //step = 0;
                                step = step + 1;
                                break;
                            }
                            else
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    sw.ResetAlarmWatch();
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.取料2飞拍图像NG);
                                    WriteOutputInfo(strOut + "取料2飞拍图像NG");
                                    step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                    break;
                                }
                                //如果检测有NG的重新飞拍一次
                                if (iFlashTimes < 2)
                                {
                                    step = lstAction.IndexOf(ActionName._20X轴到飞拍起始位);
                                    break;
                                }
                                else
                                {
                                    //if (Run.runMode == RunMode.运行)
                                    //{
                                    //    // Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                    //    int iIndex = lstAction.IndexOf(ActionName._20X轴到飞拍起始位);
                                    //    CommonSet.AlarmProcessShow(AlarmBlock.飞拍2, step, step - iIndex, "取料2飞拍图像NG", false);
                                    //    Run.runMode = RunMode.暂停;
                                    //}

                                    //Alarminfo.AddAlarm(Alarm.取料2飞拍图像NG);
                                    //WriteOutputInfo(strOut + "取料2飞拍图像NG");
                                    step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                    break;
                                }
                            }

                           
                        }
                        else
                        {
                            if (CommonSet.bTest)
                            {
                               
                                step = step + 1;
                                break;
                            }
                            if (Run.runMode == RunMode.暂停)
                            {
                                if (sw.AlarmWaitTime(3000))
                                {
                                    sw.ResetAlarmWatch();
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.取料下相机2飞拍超时);
                                    WriteOutputInfo(strOut + "取料下相机2飞拍超时");
                                    step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                                }
                                break;
                            }
                            //如果超时
                            if (sw.AlarmWaitTime(5000))
                            {
                                //if (CommonSet.bUnableCameraLen)
                                //{
                                //    // GetState1 = ActionState.组装;
                                //    step = step + 1;
                                //    break;
                                //}
                                if (Run.runMode == RunMode.运行)
                                {
                                    //Alarminfo.AddAlarm(Alarm.组装1飞拍数目异常);
                                    sw.ResetAlarmWatch();
                                    int iIndex = lstAction.IndexOf(ActionName._20X轴到飞拍起始位);
                                    CommonSet.AlarmProcessShow(AlarmBlock.飞拍2, step, step - iIndex, "取料下相机2飞拍超时", false);
                                    Run.runMode = RunMode.暂停;

                                }
                                Alarminfo.AddAlarm(Alarm.取料下相机2飞拍超时);
                                WriteOutputInfo(strOut + "取料下相机2飞拍超时");
                                step = lstAction.IndexOf(ActionName._20放料完成) + 1;
                            }
                            

                        }


                        break;

                    case ActionName._20放料完成:
                        GetState2 = ActionState.取料;
                        //AssemState2 = ActionState.组装;
                        WriteOutputInfo(strOut + "放料完成");
                         bPut = true;//放料完成
                         if (IsBarrelEmpty2() || (GetProduct2Module.iCurrentBarrel == CommonSet.dic_BarrelSuction[1].EndPos))
                         {
                             dDestPosX = OptSution2.pSafeXYZ.X;
                             dDestPosY = OptSution2.pSafeXYZ.Y;
                             mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                             mc.AbsMove(AXIS.取料Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                             WriteOutputInfo(strOut + "X2Y2轴到安全位:(" + dDestPosX.ToString() + "," + dDestPosY.ToString() + ")");
                         }
                         GetProduct2Module.iCurrentBarrel = 0;
                        step = 0;
                        break;
                    case ActionName._20XY到抛料位:

                        dDestPosX = OptSution2.pDiscard.X;
                        dDestPosY = OptSution2.pDiscard.Y;
                        mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                        mc.AbsMove(AXIS.取料Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                        WriteOutputInfo(strOut + "XY到抛料位");
                        step = step + 1;
                        break;
                    case ActionName._20XY到位完成:
                        if (IsAxisINP(dDestPosX, AXIS.取料X2轴) && IsAxisINP(dDestPosY, AXIS.取料Y2轴))
                        {
                            WriteOutputInfo(strOut + "XY到位完成");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20NG吸笔下降:
                        int Count = GetProduct2Module.lstNGSuction.Count;
                        bool bIsDown = true;
                        for (int i = 0; i < Count; i++)
                        {
                            strD = "取料气缸下降" + GetProduct2Module.lstNGSuction[i].ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, true);
                            strD = " 取料升降气缸" + GetProduct2Module.lstNGSuction[i].ToString() + "下检测";
                            dIn = (DI)Enum.Parse(typeof(DI), strD);
                            bIsDown = bIsDown && mc.dic_DI[dIn];
                        }
                        if (bIsDown)
                        {
                            step = step + 1;
                            WriteOutputInfo(strOut + "NG吸笔下降");
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.吸笔抛料位2下降超时);
                                WriteOutputInfo(strOut + "吸笔抛料位2下降超时");
                            }
                        }
                        // step = step + 1;
                        break;
                    case ActionName._20NG吸笔破真空:
                        int iNGCount = GetProduct2Module.lstNGSuction.Count;
                        // bool bIsDown = true;
                        for (int i = 0; i < iNGCount; i++)
                        {
                            strD = "取料吸真空" + GetProduct2Module.lstNGSuction[i].ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);
                            strD = "取料破真空" + GetProduct2Module.lstNGSuction[i].ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, true);

                        }
                        if (sw.WaitSetTime(200))
                        {
                            for (int i = 0; i < iNGCount; i++)
                            {
                                strD = "取料吸真空" + GetProduct2Module.lstNGSuction[i].ToString();
                                dOut = (DO)Enum.Parse(typeof(DO), strD);
                                mc.setDO(dOut, false);
                                strD = "取料破真空" + GetProduct2Module.lstNGSuction[i].ToString();
                                dOut = (DO)Enum.Parse(typeof(DO), strD);
                                mc.setDO(dOut, false);
                            }
                            WriteOutputInfo(strOut + "NG吸笔破真空");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20抛料完成:
                        GetState2 = ActionState.取料;
                        WriteOutputInfo(strOut + "抛料完成");
                        step = step + 1;
                        break;
                }

            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug(strOut + "飞拍2模块异常!", ex);

            }
        }

        private void WriteFile(string strPath, string strContent)
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
                    string strHead = "";
                    foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                    {
                        if (pair.Key < 10)
                            strHead += "S" + pair.Key.ToString() + "_Row," + "S" + pair.Key.ToString() + "_Col,";
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
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(strContent);
                sw.Close();
            }
            catch (Exception)
            {


            }

        }

        public void WriteResult(string strPath, string strContent)
        {
            Action<string, string> dele = WriteFile;
            dele.BeginInvoke(strPath, strContent, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string, string> dele = (Action<string, string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }
    }
}
