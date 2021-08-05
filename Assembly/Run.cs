using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Motion;
using System.Diagnostics;
namespace Assembly
{
    public enum RunMode { 
      运行,
      手动,
      暂停,
      飞拍测试,
      旋转,
      点胶测试,
      像素标定,
      单轴测试,
      高度测试,
      压力测试,
      取料测试,
      组装验证,
      取料中心对位测试,
      自动标定,
      组装取料测试,
   

    }
    /// <summary>
    /// 运行控制类
    /// </summary>
   public class Run
    {
       public static RunMode runMode = RunMode.手动;
       private static Run demo = null;
    
       public static GetProduct1Module productModule1 = null;
       public static GetProduct2Module productModule2 = null;
       public static FlashModule1 flashModule1 = null;
       public static FlashModule2 flashModule2 = null;
       public static BarrelAndGlueModule barrelModule = null;
       public static Assem1Module assem1Module = null;
       public static Assem2Module assem2Module = null;
       //public static Assemble1Module assembleModule1 = null;
       //public static Assemble2Module assembleModule2 = null;
       //public static FlashTest1 flashTest1 = null;
       //public static FlashTest2 flashTest2 = null;
       //public static RotateTest rotateTest = null;
       public static CalibModule calibModule = null;
       public static SingleAxisTest singleTest = null;
       public static TestFlash testFlash = null;
       public static CalibHeightModule calibHeight = null;
       public static CalibPressureModule calibPre = null;
       public static GetProductTestModule getTestModule = null;
       public static ResultTestModule resultTestModule = null;
       public static AutoGetCenterPosTestModule centerTestModule = null;
       public static RotateTest rotate = null;
       public static GlueTest glueTest = null;
       public static AutoCalibModule autoCalib = null;
       public static AssemGetProductModule assemGetProduct = null;

       public static bool bHome = false;//回原点
       public static bool bReset = false;//复位
       public static bool bRunStep = false;//单步模式
       public static bool bNext = false;//单步模式，下一步
       public static bool bFull = false;//盘满
       public static int iGlueTestPos = 1;//点胶测试位置，0为暂停
       public static bool bHeightControlFlag = true;//高度测试暂停标志,
       public static bool bPressureControlFlag = true;//压力测试暂停标志,
       public static bool bTestGetFlag = true;
       public static bool bCheckFlag = true;
       public static bool bAutoCalibFlag = true;
       public static bool bAssemGetFlag = true;

       private static bool[] bHomeTrack ;
       
       public static bool bDebugException = false; //暂停状态下调试异常
       public static bool bSetStep = false;
       public static AlarmBlock iDebugBlock;//在暂停状态下调试的模块
       public static int iDebugResult = 0;//调试结果, 1代表OK，2代表NG
       public static int iDebugFlagStep = 0;//调试设定截止步数
       public static int iDebugStartStep = 0;//调试起始位置

     
       public static Stopwatch swStart = new Stopwatch();
       public static Stopwatch swReset = new Stopwatch();
       public static Stopwatch swAuto = new Stopwatch();
       public static Stopwatch swStop = new Stopwatch();
       public static Stopwatch swSuction = new Stopwatch();

       public static Stopwatch swHome = new Stopwatch();
       public static Stopwatch swWaitSuction = new Stopwatch();
       public static Stopwatch swTestDo = new Stopwatch();

       private Motion.MotionCard mc = null;
       Thread th_Run = null;
       private Run()
       {
           mc = MotionCard.getMotionCard();
           //int axisCount = Enum.GetNames(typeof(AXIS)).Length;
           bHomeTrack = new bool[17];
           productModule1 = new GetProduct1Module();
           productModule2 = new GetProduct2Module();
           flashModule1 = new FlashModule1();
           flashModule2 = new FlashModule2();
           barrelModule = new BarrelAndGlueModule();
           assem1Module = new Assem1Module();
           assem2Module = new Assem2Module();

           calibModule = new CalibModule();
           singleTest = new SingleAxisTest();
           testFlash = new TestFlash();
           calibHeight = new CalibHeightModule();
           calibPre = new CalibPressureModule();
           getTestModule = new GetProductTestModule();

           resultTestModule = new ResultTestModule();
           centerTestModule = new AutoGetCenterPosTestModule();
           glueTest = new GlueTest();
           rotate = new RotateTest();
           autoCalib = new AutoCalibModule();
           assemGetProduct = new AssemGetProductModule();

           th_Run = new Thread(Working);
           th_Run.Start();
       }
       /// <summary>
       /// 单例模式
       /// </summary>
       /// <returns></returns>
       public static Run getInstance()
       {
           if (demo == null)
           {
               demo = new Run();
           }
           return demo;
       }
       public static void setRunMode(RunMode mode)
       {
           runMode = mode;
       }
       //工作流程
       private void Working()
       {
           while (true)
           {
               try
               {
                  
                   switch (runMode)
                   { 
                     
                       case RunMode.手动:
                           //回原点
                           if (bHome)
                           {

                                #region"回原点"
                                //检查是否有其它报警
                                List<Alarm> lst = new List<Alarm>();
                                lst.AddRange(Alarminfo.GetAlarmList());
                                for (int i = 0; i < 6; i++)
                                {
                                    lst.Remove(Alarm.取料X1轴原点丢失 + i);
                                    //lst.Remove(Alarm.Z1轴原点丢失 + i);
                                }
                                lst.Remove(Alarm.点胶C轴原点丢失);
                                lst.Remove(Alarm.点胶Y轴原点丢失);
                              
                                if (lst.Count > 0)
                                {
                                    bHome = false;
                                    bHomeTrack = new bool[17];
                                    swHome.Stop();
                                    swHome.Reset();
                                    CommonSet.WriteInfo("回原点失败！");
                                    mc.bHome = false;
                                    break;
                                }

                                if (mc.dic_HomeStatus[AXIS.点胶Z轴] && mc.dic_HomeStatus[AXIS.取料Z1轴] && mc.dic_HomeStatus[AXIS.取料Z2轴] && mc.dic_HomeStatus[AXIS.组装Z1轴] && mc.dic_HomeStatus[AXIS.组装Z2轴] && !bHomeTrack[0])
                                {
                                   
                                    if (!mc.dic_DI[DI.镜筒取料气缸1下降] && !mc.dic_DI[DI.点胶成品气缸2下降])
                                    {
                                        if (swWaitSuction.ElapsedMilliseconds < 1000)
                                        {
                                            break;
                                        }
                                        swWaitSuction.Stop();
                                    }
                                    else
                                    {
                                        if (swWaitSuction.ElapsedMilliseconds > 2000)
                                        {
                                            
                                            swHome.Stop();
                                            swHome.Reset();

                                            bHome = false;
                                            bHomeTrack = new bool[17];
                                            CommonSet.WriteInfo("气缸上升超时！");
                                            CommonSet.WriteInfo("回原点失败！");
                                            mc.bHome = false;
                                            swWaitSuction.Stop();
                                        }
                                        else
                                        {
                                        }
                                        break;
                                    }
                                    CommonSet.WriteInfo("Z轴回原点完成");
                                    bHomeTrack[0] = true;
                                    mc.Home(AXIS.取料X1轴);
                                    mc.HomeAbs(AXIS.取料Y1轴);
                                    mc.Home(AXIS.取料X2轴);
                                    mc.HomeAbs(AXIS.取料Y2轴);
                                    mc.Home(AXIS.组装X1轴);
                                    mc.Home(AXIS.组装Y1轴);
                                    
                                    mc.Home(AXIS.组装X2轴);
                                    mc.Home(AXIS.组装Y2轴);
                                    mc.HomeAbs(AXIS.点胶X轴);
                                    mc.Home(AXIS.点胶Y轴);
                                    mc.Home(AXIS.点胶C轴);
                                    mc.HomeAbs(AXIS.镜筒Y轴);
                                    swHome.Restart();
                                }
                                if (mc.dic_HomeStatus[AXIS.取料X1轴] && !bHomeTrack[1])
                                {
                                    CommonSet.WriteInfo("取料X1轴回原点完成");
                                    bHomeTrack[1] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.取料Y1轴] && !bHomeTrack[2])
                                {
                                    CommonSet.WriteInfo("取料Y1轴回原点完成");
                                    bHomeTrack[2] = true;

                                }
                                if (mc.dic_HomeStatus[AXIS.取料X2轴] && !bHomeTrack[3])
                                {
                                    CommonSet.WriteInfo("取料X2轴回原点完成");
                                    bHomeTrack[3] = true;

                                }
                                if (mc.dic_HomeStatus[AXIS.取料Y2轴] && !bHomeTrack[4])
                                {
                                    CommonSet.WriteInfo("取料Y2轴回原点完成");
                                    bHomeTrack[4] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.组装X1轴] && !bHomeTrack[5])
                                {
                                    CommonSet.WriteInfo("组装X1轴回原点完成");
                                    bHomeTrack[5] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.组装Y1轴] && !bHomeTrack[6])
                                {
                                    CommonSet.WriteInfo("组装Y1轴回原点完成");
                                    bHomeTrack[6] = true;

                                }
                                if (mc.dic_HomeStatus[AXIS.组装X2轴] && !bHomeTrack[7])
                                {
                                    CommonSet.WriteInfo("组装X2轴回原点完成");
                                    bHomeTrack[7] = true;

                                }
                                if (mc.dic_HomeStatus[AXIS.组装Y2轴] && !bHomeTrack[8])
                                {
                                    CommonSet.WriteInfo("组装Y2轴回原点完成");
                                    bHomeTrack[8] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.点胶X轴] && !bHomeTrack[9])
                                {
                                    CommonSet.WriteInfo("点胶X轴回原点完成");
                                    bHomeTrack[9] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.点胶Y轴] && !bHomeTrack[10])
                                {
                                    CommonSet.WriteInfo("点胶Y轴回原点完成");
                                    bHomeTrack[10] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.点胶C轴] && !bHomeTrack[11])
                                {
                                    CommonSet.WriteInfo("点胶C轴回原点完成");
                                    bHomeTrack[11] = true;
                                }
                                if (mc.dic_HomeStatus[AXIS.镜筒Y轴] && !bHomeTrack[12])
                                {
                                    CommonSet.WriteInfo("镜筒Y轴回原点完成");
                                    bHomeTrack[12] = true;
                                }
                                if (swHome.ElapsedMilliseconds > 40000)
                                {
                                    swHome.Stop();
                                    swHome.Reset();
                                   
                                    bHome = false;
                                    bHomeTrack = new bool[17];
                                    CommonSet.WriteInfo("回原点超时！");
                                    CommonSet.WriteInfo("回原点失败！");
                                    mc.bHome = false;
                                }
                               bool bflag = true;
                               for (int i = 0; i < 13; i++)
                               {
                                   bflag = bflag && bHomeTrack[i];
                                  
                               }
                               if (bflag)
                               {
                                   swHome.Stop();
                                   swHome.Reset();
                                   bHome = false;
                                   bHomeTrack = new bool[17];
                                   ((LeiE3032)mc).ResetExtraEncoder();
                                   CommonSet.WriteInfo("回原点完成");
                                   mc.bHome = false;
                                   break;
                               }
                               #endregion
                           }
                           if (mc.dic_DI[DI.复位按钮] && !bReset)
                           {
                               swReset.Start();
                               if (swReset.ElapsedMilliseconds > 100)
                               {
                                   bReset = true;
                               }

                           }
                           //复位
                           if (bReset)
                           {
                               swReset.Stop();
                               swReset.Reset();
                               //if (mc.dic_Axis[AXIS.X轴].ALM || mc.dic_Axis[AXIS.Y轴].ALM || mc.dic_Axis[AXIS.Z轴].ALM)
                               //{
                               //    bReset = false;
                               //    mc.setDO(DO.报警复位1, true);
                               //    mc.setDO(DO.报警复位2, true);
                               //    mc.setDO(DO.报警复位3, true);
                               //    break;
                               //}
                               mc.ResetAxisAlarm();//复位驱动器报警
                               if (Alarminfo.BAlarm)
                               {
                                   Alarminfo.ClearAllAlarm();
                                   bReset = false;
                                   break;
                               }


                               ResetModule();
                               if (productModule1.bResetFinish && productModule2.bResetFinish && assem1Module.bResetFinish && assem2Module.bResetFinish && flashModule1.bResetFinish && flashModule2.bResetFinish && barrelModule.bResetFinish)
                               {
                                   CommonSet.WriteInfo("复位完成！");
                                   bReset = false;
                                  
                                   break;
                               }
                           }
                           if (mc.dic_DI[DI.启动按钮])
                           {
                               swStart.Start();
                               if (swStart.ElapsedMilliseconds > 2000)
                               {
                                   if (Alarminfo.BAlarm)
                                       break;

                                   if (Run.runMode == RunMode.手动)
                                   {
                                       if (CommonSet.iLevel < 2)
                                           CommonSet.iLevel = 0;
                                       runMode = RunMode.暂停;
                                       ResetStatus();
                                       CommonSet.bResetOne = true;
                                       bReset = true;
                                   }
                               }
                           }
                          // flashModule.reseTestFlash();
                         
                           CommonSet.swCircleA1.Stop();
                           CommonSet.swCircleA2.Stop();
                           CommonSet.swCircleC1.Stop();
                           CommonSet.swCircleC2.Stop();
                           CommonSet.swCircleD.Stop();
                            #region
                           if (CommonSet.bUseAutoParam)
                           {
                               if (CommonSet.dOutTest != DO.点胶清洁夹紧)
                               {
                                   mc.setDO(CommonSet.dOutIn, false);
                                   mc.setDO(CommonSet.dOutTest, true);
                                   swTestDo.Start();
                                   if (swTestDo.ElapsedMilliseconds > CommonSet.dOutTestTime)
                                   {
                                       mc.setDO(CommonSet.dOutTest, false);
                                      
                                       CommonSet.WriteInfo("打开"+CommonSet.dOutTest.ToString()+"----"+swTestDo.ElapsedMilliseconds.ToString()+"ms");
                                       CommonSet.dOutTest = DO.点胶清洁夹紧;
                                   }

                               }
                               else
                               {
                                   swTestDo.Stop();
                                   swTestDo.Reset();
                               }

                           }
                           else
                           {
                               swTestDo.Stop();
                               swTestDo.Reset();
                           }

                            #endregion

                           break;

                       case RunMode.运行:
                            swAuto.Stop();
                            swAuto.Reset();
                           if (mc.dic_DI[DI.暂停按钮])
                           {
                               swStop.Start();
                               if (swStop.ElapsedMilliseconds > 100)
                               {
                                   Run.runMode = RunMode.暂停;
                                   break;
                               }
                           }
                           if (bRunStep)
                           {
                               if (bNext )
                               {
                                   bNext = false;

                               }
                               else
                               {
                                   break;
                               }

                           }


                           productModule1.MakeAction();
                           productModule2.MakeAction();
                           flashModule1.MakeAction();
                           flashModule2.MakeAction();
                           assem1Module.MakeAction();
                           assem2Module.MakeAction();
                           barrelModule.MakeAction();
                          
                           break;

                       case RunMode.暂停:
                           swStart.Stop();
                           swStart.Reset();
                           swStop.Stop();
                           swStop.Reset();
                           CommonSet.swCircleA1.Stop();
                           CommonSet.swCircleA2.Stop();
                           CommonSet.swCircleC1.Stop();
                           CommonSet.swCircleC2.Stop();
                           CommonSet.swCircleD.Stop();
                           if (mc.dic_DI[DI.复位按钮]&&!mc.dic_DI[DI.启动按钮] && !bReset)
                           {
                               if (!Alarminfo.BAlarm)
                               {
                                
                                   break;
                               }
                               swReset.Start();
                               if (swReset.ElapsedMilliseconds > 100)
                               {
                                   bReset = true;
                               }

                           }
                           if (!mc.dic_DI[DI.复位按钮] && mc.dic_DI[DI.启动按钮] )
                           {
                               swAuto.Start();
                               if (swAuto.ElapsedMilliseconds > 100)
                               {
                                   runMode = RunMode.运行;
                               }
                               

                           }
                           if (bReset)
                           {
                               swReset.Stop();
                               swReset.Reset();
                               if (Alarminfo.BAlarm)
                               {
                                   Alarminfo.ClearAllAlarm();
                                   bReset = false;
                                   break;
                               }
                               if (CommonSet.bResetOne)
                               {
                                   ResetModule();
                                   if (productModule1.bResetFinish && productModule2.bResetFinish && assem1Module.bResetFinish && assem2Module.bResetFinish && flashModule1.bResetFinish && flashModule2.bResetFinish&&barrelModule.bResetFinish)
                                   {
                                       CommonSet.WriteInfo("复位完成！");
                                       bReset = false;
                                       CommonSet.bResetOne = false;
                                       break;
                                   }
                                   
                               }

                               if (bFull)
                               {
                                   ResetModule();
                                   if (productModule1.bResetFinish && productModule2.bResetFinish && assem1Module.bResetFinish && assem2Module.bResetFinish && flashModule1.bResetFinish && flashModule2.bResetFinish&&barrelModule.bResetFinish)
                                   {
                                       CommonSet.WriteInfo("满盘复位完成！");
                                       bReset = false;
                                       break;
                                   }
                                   
                               }
                           }

                           #region"报警调试"
                           if (bDebugException)
                           {
                               //在暂停状态下如果出现报警，在弹出的对话框中调试对应的模块
                               if (iDebugBlock == AlarmBlock.取料1)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       productModule1.IStep = iDebugStartStep;
                                   }

                                   productModule1.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }


                               }else if (iDebugBlock == AlarmBlock.取料2)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       productModule2.IStep = iDebugStartStep;
                                   }

                                   productModule2.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }


                               }
                               else if (iDebugBlock == AlarmBlock.飞拍1)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       flashModule1.IStep = iDebugStartStep;
                                   }

                                   flashModule1.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }
                               }
                               else if (iDebugBlock == AlarmBlock.飞拍2)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       flashModule2.IStep = iDebugStartStep;
                                   }

                                   flashModule2.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }
                               }
                               else if (iDebugBlock == AlarmBlock.组装1)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       assem1Module.IStep = iDebugStartStep;
                                   }
                                   assem1Module.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }
                               }
                               else if (iDebugBlock == AlarmBlock.组装2)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       assem2Module.IStep = iDebugStartStep;
                                   }
                                   assem2Module.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }
                               }
                              
                               else if (iDebugBlock == AlarmBlock.镜筒和点胶)
                               {
                                   if (bSetStep)
                                   {
                                       bSetStep = false;
                                       barrelModule.IStep = iDebugStartStep;
                                   }
                                   barrelModule.MakeAction();
                                   if ((iDebugResult == 1) || (iDebugResult == 2))
                                   {
                                       bDebugException = false;
                                   }
                               }
                             
                           }
                           #endregion
                  
                           break;
                       case RunMode.飞拍测试:

                           testFlash.MakeAction();

                           break;
                       case RunMode.旋转:
                          // barrelModule.TestRotateAction();
                           rotate.MakeAction();
                           break;
                       case RunMode.像素标定:
                           calibModule.MakeAction();
                           break;
                       case RunMode.单轴测试:
                           singleTest.MakeAction();
                           break;
                       case RunMode.高度测试:
                           if (bHeightControlFlag)
                           {
                               calibHeight.MakeAction();
                           }
                           break;
                       case RunMode.压力测试:
                           if (bPressureControlFlag)
                           {
                               calibPre.MakeAction();
                           }
                           break;
                       case RunMode.取料测试:
                           if (bTestGetFlag)
                           {
                               getTestModule.MakeAction();
                           }
                           break;
                       case RunMode.组装验证:
                           if (bCheckFlag)
                           {
                               resultTestModule.MakeAction();
                           }
                           break;
                       case RunMode.取料中心对位测试:
                           if (bTestGetFlag)
                           {
                               centerTestModule.MakeAction();
                           }
                           break;
                       case RunMode.点胶测试:
                           glueTest.MakeAction();
                           break;
                       case RunMode.自动标定:
                           if(bAutoCalibFlag)
                            autoCalib.MakeAction();
                           break;
                       case RunMode.组装取料测试:
                           if (bAssemGetFlag)
                               assemGetProduct.MakeAction();
                           break;
                      
                   }
                   //输出按钮灯
                   mc.setDO(DO.复位, bReset);
                   mc.setDO(DO.启动, (runMode == RunMode.运行) || (runMode == RunMode.暂停));
                   mc.setDO(DO.暂停, runMode == RunMode.暂停);

                   if (mc.dic_DI[DI.真空泵启动按钮])
                   {
                       long lsution = swSuction.ElapsedMilliseconds;
                       if (lsution > 100)
                       {
                           if (swSuction.IsRunning)
                           {
                               swSuction.Stop();
                               mc.setDO(DO.真空泵继电器, !mc.dic_DO[DO.真空泵继电器]);
                           }
                       }
                       else
                       {
                           swSuction.Start();
                       }

                   }
                   else
                   {
                       swSuction.Stop();
                       swSuction.Reset();
                   }
                  

               }
               catch (Exception ex)
               {
                   CommonSet.WriteDebug("运行异常:",ex);
               }
               Thread.Sleep(5);
           
           
           }
       
       }
       public static void ResetModule()
       {

           productModule1.Reset();
           productModule2.Reset();
           flashModule1.Reset();
           flashModule2.Reset();
           assem1Module.Reset();
           assem2Module.Reset();
           barrelModule.Reset();
       }

       public static void ResetStatus()
       {
           GetProduct1Module.iResetStep = 0;
           GetProduct2Module.iResetStep = 0;
           FlashModule1.iResetStep = 0;
           FlashModule2.iResetStep = 0;
           Assem1Module.iResetStep = 0;
           Assem2Module.iResetStep = 0;
           BarrelAndGlueModule.iResetStep = 0;

           productModule1.bResetFinish = false;
           productModule2.bResetFinish = false;
           flashModule1.bResetFinish = false;
           flashModule2.bResetFinish = false;
           assem1Module.bResetFinish = false;
           assem2Module.bResetFinish = false;
           barrelModule.bResetFinish = false;
          
       }

    }
}
