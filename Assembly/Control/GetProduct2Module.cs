using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using HalconDotNet;
namespace Assembly
{
    public class GetProduct2Module:ActionModule
    {

        private string strOut = "GetProduct2Module-Action-";
        //private static GetProduct2Module module = null;
        public static int iCurrentSuction = 10;//当前吸笔序号
        public static List<int> lstNGSuction = new List<int>();//飞拍NG的图片
        private Point currentPoint = null;
        private int iCurrentTimes = 1;
        private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static bool bIsProductEmpty = false;//盘子中有无料，true为无料
        public static int iCurrentBarrel = 0;//当前镜筒位置
        public static int iResetStep = 0;
        private string strD = "";
        private DO dOut;
        private DI dIn;

       

         public GetProduct2Module()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._10判断取料盘是否为空);
            //获取吸笔的序号
            lstAction.Add(ActionName._10取料条件判断);
            lstAction.Add(ActionName._10Z轴到安全位);
            //获取XY坐标，判断是否拍照,如果不需要拍照，跳到XY取料位
            lstAction.Add(ActionName._10计算吸笔XY坐标);
            //相机拍照 
            lstAction.Add(ActionName._10XY轴到拍照位);
            lstAction.Add(ActionName._10XY到位完成);
            lstAction.Add(ActionName._10上相机拍照);
            lstAction.Add(ActionName._10上相机拍照完成);
            // 吸笔取料
            lstAction.Add(ActionName._10XY到取料位);
            lstAction.Add(ActionName._10XY到位完成);
            lstAction.Add(ActionName._10吸笔气缸下降);
            lstAction.Add(ActionName._10Z到取料位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10吸笔气缸下降到位);
            lstAction.Add(ActionName._10吸笔吸真空);
            //lstAction.Add(ActionName._10吸笔气缸上升);
            lstAction.Add(ActionName._10吸笔真空检测);
            lstAction.Add(ActionName._10Z轴到安全位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10取料完成);

        }
         public override void Action2()
         {

         }
         public override void Reset()
         {
             switch (iResetStep)
             {
                 case 0:
                     if (bResetFinish)
                         break;
                     IStep = 0;
                     iCurrentTimes = 1;
                     iCurrentNum = 1;
                     iCurrentSuction = 10;
                     bIsProductEmpty = false;
                     iCurrentBarrel = 0;
                     lstNGSuction.Clear();
                     GetState2 = ActionState.取料;
                     iResetStep = iResetStep + 1;

                     break;
                 case 1:
                     if (Run.flashModule2.bResetFinish)
                     {
                         if (mc.dic_Axis[AXIS.取料X2轴].INP && mc.dic_Axis[AXIS.取料Y2轴].INP && mc.dic_Axis[AXIS.取料Z2轴].INP)
                             Action(ActionName._10Z轴到待机位, ref iResetStep);
                     }
                     break;
                 case 2:
                     Action(ActionName._10Z轴到位, ref iResetStep);

                     break;

                 case 3:
                     Action(ActionName._10XY到安全位, ref iResetStep);

                     break;
                 case 4:
                     Action(ActionName._10XY到位完成, ref iResetStep);

                     break;
                 case 5:
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

                    case ActionName._10判断取料盘是否为空:
                        if (GetState2 == ActionState.空闲 || GetState2 == ActionState.取料)
                        {
                            ////如果为刚启动时，可直接取料，其它情况，需要判断取料
                            //if (GetState1 == ActionState.空闲)
                            //{ 

                            //}

                            //if (bIsProductEmpty)
                            //{
                            //    //state1 = ActionState.组装完成;
                            //    //if (state2 == ActionState.组装完成)
                            //    //{
                            //    //    Run.runMode = RunMode.暂停;
                            //    //    Run.bReset = true;
                            //    //    Run.bFull = true;
                            //    //    Run.productModule1.bResetFinish = false;
                            //    //    Run.productModule2.bResetFinish = false;
                            //    //    Run.assembleModule1.bResetFinish = false;
                            //    //    Run.assembleModule2.bResetFinish = false;
                            //    //    Run.flashModule1.bResetFinish = false;
                            //    //    Run.flashModule2.bResetFinish = false;
                            //    //    iResetStep = 0;
                            //    //}
                            //    break;
                            //}
                            //判断料盘是否已取完
                            if (IsBarrelEmpty2())
                            {
                                bIsProductEmpty = true;
                                Alarminfo.AddAlarm(Alarm.取料2有空盘);
                                Run.runMode = RunMode.暂停;
                                WriteOutputInfo(strOut + "取料2盘空，停止取料！");
                            }
                            if (iCurrentBarrel > 0)
                            {
                                //如果镜筒盘已满，则等组装位飞拍完成后再取料
                                if (CommonSet.dic_BarrelSuction[1].bFinish || CommonSet.bForceFinish)
                                {
                                    if ((Run.assem2Module.IStep > Run.assem2Module.lstAction.IndexOf(ActionName._30开始组装) + 1) || (iCurrentBarrel < CommonSet.dic_BarrelSuction[1].EndPos) || (lstNGSuction.Count > 0))
                                    {
                                        step = step + 1;
                                        CommonSet.swCircleA2.Restart();
                                        break;
                                    }
                                    //如果最后收尾组装飞拍NG后抛料
                                    if ((!Assem2Module.bImageResult) && (Run.assem2Module.IStep > Run.assem2Module.lstAction.IndexOf(ActionName._30开始抛料)))
                                    {
                                        step = step + 1;
                                        CommonSet.swCircleA2.Restart();
                                        break;
                                    }

                                }
                                else
                                {
                                    step = step + 1;
                                    CommonSet.swCircleA2.Restart();
                                }
                            }

                        }
                        break;
                    case ActionName._10取料条件判断:
                        int iFlag = iCurrentSuction;
                        iCurrentSuction = FindSuctionEmpty();
                        if (iCurrentSuction > 18)
                        {
                            iCurrentSuction = 10;
                            step = lstAction.IndexOf(ActionName._10吸笔真空检测) + 1;
                            break;
                        }


                        if (iCurrentSuction != iFlag)
                            iCurrentTimes = 1;//将每个位置吸取的次数置为初始值1
                        WriteOutputInfo(strOut + "-------吸笔" + iCurrentSuction.ToString() + "开始取料--------------");
                        step = step + 1;
                        break;
                    case ActionName._10计算吸笔XY坐标:
                        //如果使用相机拍照
                        if (CommonSet.dic_OptSuction2[iCurrentSuction].BUseCameraUp)
                        {
                            OptSution2 suction = CommonSet.dic_OptSuction2[iCurrentSuction];
                            currentPoint = suction.getCameraCoordinateByIndex();
                            currentPoint.Z = CommonSet.dic_OptSuction2[iCurrentSuction].DGetPosZ;
                            step = step + 1;
                        }
                        else
                        {
                            //不使用则直接到吸笔取料位
                            OptSution2 suction = CommonSet.dic_OptSuction2[iCurrentSuction];
                            currentPoint = suction.getCoordinateByIndex();
                            //currentPoint.Z = OptSution2.pSafeXYZ.Z;
                            step = lstAction.IndexOf(ActionName._10XY到取料位);
                        }
                        break;
                    case ActionName._10上相机拍照:
                        mc.setDO(DO.取料上相机2, true);
                        if (sw.WaitSetTime(10))
                        {
                            mc.setDO(DO.取料上相机2, false);
                            WriteOutputInfo("取料2上相机拍照");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10上相机拍照完成:
                        if (CommonSet.dic_OptSuction2[iCurrentSuction].imgResultUp.bStatus)
                        {
                            if (CommonSet.dic_OptSuction2[iCurrentSuction].imgResultUp.bImageResult)
                            {
                                step = step + 1;
                            }
                            else
                            {
                                CommonSet.dic_OptSuction2[iCurrentSuction].CurrentPos++;
                                step = lstAction.IndexOf(ActionName._10计算吸笔XY坐标);
                            }
                        }
                        else
                        {
                            Alarminfo.AddAlarm(Alarm.取料上相机2拍照超时);
                            WriteOutputInfo(strOut + "取料上相机2拍照超时");

                        }
                        break;
                    case ActionName._10XY轴到拍照位:
                        if ((mc.dic_Axis[AXIS.取料Z2轴].dPos < (currentPoint.Z - 2)) || IsAxisINP(dDestPosZ, AXIS.取料Z2轴, 1))
                        {
                            dDestPosX = currentPoint.X;
                            dDestPosY = currentPoint.Y;
                            mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.取料Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "XY轴到拍照位:(" + currentPoint.X.ToString() + "," + currentPoint.Y.ToString() + ")");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10XY到取料位:
                        if ((mc.dic_Axis[AXIS.取料Z2轴].dPos < (currentPoint.Z - 2)) || IsAxisINP(dDestPosZ, AXIS.取料Z2轴, 1))
                        {
                            dDestPosX = currentPoint.X;
                            dDestPosY = currentPoint.Y;
                            mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.取料Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "XY轴到吸取位:(" + currentPoint.X.ToString() + "," + currentPoint.Y.ToString() + ")");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10XY到安全位:
                        dDestPosX = OptSution2.pSafeXYZ.X;
                        dDestPosY = OptSution2.pSafeXYZ.Y;
                        mc.AbsMove(AXIS.取料X2轴, dDestPosX, (int)100);
                        mc.AbsMove(AXIS.取料Y2轴, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "XY轴到安全位:(" + dDestPosX.ToString() + "," + dDestPosY.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._10XY到位完成:
                        if (IsAxisINP(dDestPosX, AXIS.取料X2轴) && IsAxisINP(dDestPosY, AXIS.取料Y2轴))
                        {
                            WriteOutputInfo("取料XY到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10Z到取料位:
                        if (!mc.dic_Axis[AXIS.取料Z2轴].INP)
                            break;
                        if (CommonSet.bTest)
                        {
                            dDestPosZ = currentPoint.Z - 3;
                        }
                        else
                        {
                            dDestPosZ = currentPoint.Z;
                        }
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到取料位：" + dDestPosZ.ToString());
                        step = step + 1;

                        break;
                    case ActionName._10Z轴到飞拍高度:
                        dDestPosZ = OptSution2.dFlashZ;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "取料Z2轴到飞拍高度：" + dDestPosZ.ToString());
                        step = step + 1;

                        break;
                    case ActionName._10Z轴到安全位:
                        dDestPosZ = OptSution2.pSafeXYZ.Z;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)CommonSet.dVelRunZ);
                        WriteOutputInfo(strOut + "Z轴到安全位：" + dDestPosZ.ToString());
                        step = step + 1;

                        break;
                    case ActionName._10Z轴到待机位:
                        dDestPosZ = OptSution2.pSafeXYZ.Z;
                        mc.AbsMove(AXIS.取料Z2轴, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位：" + dDestPosZ.ToString());
                        step = step + 1;

                        break;
                    case ActionName._10Z轴到位:
                        if (IsAxisINP(dDestPosZ, AXIS.取料Z2轴, 1))
                        {
                            WriteOutputInfo(strOut + "取料Z2轴到位完成");
                            step = step + 1;
                        }

                        break;
                    case ActionName._10吸笔气缸上升:
                        strD = "取料气缸下降" + iCurrentSuction.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, false);
                        strD = "取料升降气缸" + iCurrentSuction + "下检测";
                        dIn = (DI)Enum.Parse(typeof(DI), strD);
                        if (mc.dic_DI[dIn] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "气缸上升");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10吸笔气缸下降:

                        strD = "取料气缸下降" + iCurrentSuction.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "气缸下降");
                        step = step + 1;
                        break;
                    case ActionName._10吸笔气缸下降到位:
                        strD = "取料升降气缸" + iCurrentSuction + "下检测";
                        dIn = (DI)Enum.Parse(typeof(DI), strD);
                        if (mc.dic_DI[dIn] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "吸笔" + iCurrentSuction.ToString() + "下降到位");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.取料吸笔1下降超时 + iCurrentSuction - 1);
                                WriteOutputInfo(strOut + "吸笔" + iCurrentSuction.ToString() + "下降超时");
                            }
                        }
                        break;

                    case ActionName._10吸笔吸真空:
                        strD = "取料吸真空" + iCurrentSuction.ToString();
                        dOut = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut, true);
                        strD = "取料破真空" + iCurrentSuction.ToString();
                        DO dOut2 = (DO)Enum.Parse(typeof(DO), strD);
                        mc.setDO(dOut2, false);
                        long lTime1 = (long)(CommonSet.dic_OptSuction2[iCurrentSuction].DGetTime * 1000);
                        if (sw.WaitSetTime(lTime1))
                        {
                            WriteOutputInfo(strOut + "吸笔" + iCurrentSuction.ToString() + "吸真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._10吸笔关闭吸真空:
                        strD = "取料吸真空" + iCurrentSuction.ToString();
                        DO dOut3 = (DO)Enum.Parse(typeof(DO), strD);
                        WriteOutputInfo(strOut + "吸笔" + iCurrentSuction.ToString() + "关闭吸真空");
                        mc.setDO(dOut3, false);
                        step = step + 1;

                        break;
                    case ActionName._10吸笔真空检测:
                        strD = "取料气缸下降" + iCurrentSuction.ToString();
                         dOut = (DO)Enum.Parse(typeof(DO), strD);
                         mc.setDO(dOut, false);
                         strD = "取料真空检测" + iCurrentSuction.ToString();
                         dIn = (DI)Enum.Parse(typeof(DI), strD);

                         if (CommonSet.bTest || mc.dic_DI[dIn] || CommonSet.bForceOk)
                        {
                            CommonSet.bForceOk = false;
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 1;
                                break;
                            }
                            WriteOutputInfo(strOut + "吸笔" + iCurrentSuction.ToString() + "吸取成功！");
                            CommonSet.dic_OptSuction2[iCurrentSuction].CurrentPos++;
                            // CommonSet.dic_ProductSuction[iCurrentSuction].bGet = true;
                            iCurrentNum = 1;
                            iCurrentTimes = 1;
                            int iNGCount = lstNGSuction.Count;
                            if (iNGCount > 0)
                            {
                                //NG取料结束，直接跳到最后
                                if (iNGCount == 1)
                                {
                                    lstNGSuction.Clear();
                                    iCurrentSuction = 10;
                                    step = step + 1 ;
                                    // break;
                                }
                                else
                                {
                                    lstNGSuction.RemoveAt(0);
                                    step = lstAction.IndexOf(ActionName._10取料条件判断);
                                }
                                break;
                            }

                            iCurrentSuction = iCurrentSuction + 1;
                            int iFindSuction = FindSuctionEmpty();

                            if (iFindSuction > 18)
                            {
                                iCurrentSuction = 10;
                                step = step + 1;
                            }
                            else
                            {

                                step = lstAction.IndexOf(ActionName._10取料条件判断);
                            }
                            // step = step + 1;
                        }
                        else
                        {
                            //等待气缸上升
                            if (!sw.WaitSetTime(500))
                            {
                                break;
                            }

                            WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "吸取失败！");
                            //strD = "取料吸真空" + iCurrentSuction.ToString();
                            // dOut = (DO)Enum.Parse(typeof(DO), strD);
                            //mc.setDO(dOut, false);
                            strD = "取料破真空" + iCurrentSuction.ToString();
                            dOut = (DO)Enum.Parse(typeof(DO), strD);
                            mc.setDO(dOut, false);
                          

                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 2;
                                Alarminfo.AddAlarm(Alarm.吸笔吸取NG);
                                WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "吸取NG");
                                break;
                            }

                            if (iCurrentTimes > CommonSet.iGetTimes - 1)
                            {
                                if (iCurrentNum > CommonSet.iTimes - 1)
                                {
                                    if (Run.runMode == RunMode.运行)
                                    {
                                        // Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                        int iIndex = lstAction.IndexOf(ActionName._10取料条件判断);
                                        CommonSet.AlarmProcessShow(AlarmBlock.取料2, step, step-iIndex, "取料吸笔" + iCurrentSuction.ToString() + "连续吸取NG", true);
                                        Run.runMode = RunMode.暂停;
                                    }

                                    Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                    WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "连续吸取NG");
                                }
                                else
                                {
                                    iCurrentTimes = 1;
                                    WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "吸取第" + iCurrentNum.ToString() + "穴NG");
                                    CommonSet.dic_OptSuction2[iCurrentSuction].CurrentPos++;
                                    //如果盘子无料
                                    if (CommonSet.dic_OptSuction2[iCurrentSuction].bFinish)
                                    {

                                        // Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                        int iIndex = lstAction.IndexOf(ActionName._10取料条件判断);
                                        CommonSet.AlarmProcessShow(AlarmBlock.取料2, step, step-iIndex, "取料吸笔" + iCurrentSuction.ToString() + "连续吸取NG", true);


                                        Alarminfo.AddAlarm(Alarm.取料吸笔连续吸取NG);
                                        WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "连续吸取NG");
                                        Run.runMode = RunMode.暂停;
                                        //iCurrentNum = 1;
                                        //step = 0;
                                        break;
                                    }

                                    iCurrentNum++;

                                    step = lstAction.IndexOf(ActionName._10取料条件判断);
                                }
                            }
                            else
                            {

                                iCurrentTimes++;
                                WriteOutputInfo(strOut + "取料吸笔" + iCurrentSuction.ToString() + "第" + iCurrentTimes.ToString() + "次取料");
                                step = lstAction.IndexOf(ActionName._10吸笔气缸下降);
                            }
                        }
                        break;
                    case ActionName._10取料完成:
                        WriteOutputInfo(strOut + "取料完成");
                        GetState2 = ActionState.飞拍和放料;
                        step = step + 1;
                        break;


                }

            }
            catch (Exception ex)
            {

                WriteOutputInfo(strOut + "取料1异常" + ex.ToString());
            }
        }

        /// <summary>
        /// 判断是否有空吸笔,并返回第一个吸笔的序号
        /// </summary>
        /// <returns></returns>
        public int FindSuctionEmpty()
        {
            //iCurrentSuction = iCurrentSuction + 1;
            //if (iCurrentSuction > 12)
            //    iCurrentSuction = 1;
            //return iCurrentSuction;
            if (lstNGSuction.Count > 0)
            {
                return lstNGSuction[0];

            }

            int result = iCurrentSuction;
            int len = CommonSet.dic_OptSuction2.Count;
            for (; result < 19; result++)
            {

                OptSution2 p = CommonSet.dic_OptSuction2[result];
                string strDI = "取料真空检测" + p.SuctionOrder.ToString();
                DI di = (DI)Enum.Parse(typeof(DI), strDI);

                if ((CommonSet.bTest || !mc.dic_DI[di]) && p.BUse)
                {
                    result = p.SuctionOrder;
                    break;
                }
                
            }
           

            
            return result;

        }
    }
}
