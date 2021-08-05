using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Motion;
using System.Diagnostics;
namespace _0E03
{
    public enum RunMode
    {
        运行,
        手动,
        暂停,
        飞拍测试,
        旋转,
        单轴测试,
        高度测试,
        压力测试,
        取料测试,
        取料标定,
        组装标定,
        组装验证,
        取料中心对位测试,
        自动标定,
        组装取料测试,
        像素标定,

    }
    public class Run
    {
        public static RunMode runMode = RunMode.手动;
        private static Run demo = null;
        private static bool[] bHomeTrack;
        private Motion.MotionCard mc = null;
        Thread th_Run = null;

        public static bool bHome = false;//回原点
        public static bool bReset = false;//复位
        public static bool bRunStep = false;//单步模式
        public static bool bNext = false;//单步模式，下一步
        public static bool bFull = false;//盘满

        public static Stopwatch swHome = new Stopwatch();
        //运行模块
        public static CameraLModule camLModule = null;
        public static CameraRModule camRModule = null;
        public static AssemLModule assemLModule = null;
        public static AssemRModule assemRModule = null;
        public static BarrelModule barrelModule = null;
        //测试模块
        public static bool[] bTestFlag = new bool[10];
        public static TestAxisModule testAxisModule = null;//单轴测试
        //取料测试
        public static TestGetOptLModule testGetLModule = new TestGetOptLModule();
        public static TestGetOptRModule testGetRModule = new TestGetOptRModule();
        //标定块测试
        public static CalibOptLModule calibLModule = new CalibOptLModule();
        public static CalibOptRModule calibRModule = new CalibOptRModule();
        public static CalibBarrelLModule calibBLModule = new CalibBarrelLModule();
        public static CalibBarrelRModule calibBRModule = new CalibBarrelRModule();
        //旋转测试
        public static RotateTestModule rotateModule = new RotateTestModule();

        public static bool bResetStatus = false;//机台复位结果
        public static Stopwatch swCircleTime = new Stopwatch();//组装计时
        public static long lAssemTime = 0;
        private Run()
        {
            mc = MotionCard.getMotionCard();
            //int axisCount = Enum.GetNames(typeof(AXIS)).Length;
            bHomeTrack = new bool[17];
            camLModule = new CameraLModule();
            camRModule = new CameraRModule();
            assemLModule = new AssemLModule();
            assemRModule = new AssemRModule();
            barrelModule = new BarrelModule();
            testAxisModule = new TestAxisModule();

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
        private void Working()
        {
            while (true)
            {
                try
                {

                    PressJudge();
                    switch (runMode)
                    {

                        case RunMode.手动:
                            ResetVel();
                            bResetStatus = false;
                            swCircleTime.Stop();
                            swCircleTime.Reset();
                            //回原点
                            if (bHome)
                            {

                                #region"回原点"
                                //检查是否有其它报警
                                List<Alarm> lst = new List<Alarm>();
                                lst.AddRange(Alarminfo.GetAlarmList());
                                for (int i = 0; i < 10; i++)
                                {
                                    if(lst.Contains(Alarm.镜筒Y轴原点丢失 + i))
                                    lst.Remove(Alarm.镜筒Y轴原点丢失 + i);
                                    //lst.Remove(Alarm.Z1轴原点丢失 + i);
                                }

                              
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

                                if (mc.dic_HomeStatus[AXIS.组装Z2轴] && mc.dic_HomeStatus[AXIS.组装Z1轴] && !bHomeTrack[0])
                                {

                                    CommonSet.WriteInfo("Z轴回原点完成");
                                    bHomeTrack[0] = true;
                                    mc.Home(AXIS.取料X1轴);
                                    mc.Home(AXIS.取料Y1轴);
                                    mc.Home(AXIS.取料X2轴);
                                    mc.Home(AXIS.取料Y2轴);
                                    mc.Home(AXIS.组装X1轴);
                                    mc.Home(AXIS.镜筒X轴);
                                    mc.Home(AXIS.组装X2轴);
                                    mc.Home(AXIS.镜筒Y轴);
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
                               
                                if (mc.dic_HomeStatus[AXIS.组装X2轴] && !bHomeTrack[6])
                                {
                                    CommonSet.WriteInfo("组装X2轴回原点完成");
                                    bHomeTrack[6] = true;

                                }
                                if (mc.dic_HomeStatus[AXIS.镜筒X轴] && !bHomeTrack[7])
                                {
                                    CommonSet.WriteInfo("镜筒X轴回原点完成");
                                    bHomeTrack[7] = true;
                                }
                               
                                if (mc.dic_HomeStatus[AXIS.镜筒Y轴] && !bHomeTrack[8])
                                {
                                    CommonSet.WriteInfo("镜筒Y轴回原点完成");
                                    bHomeTrack[8] = true;
                                }
                                
                                if (swHome.ElapsedMilliseconds > 20000)
                                {
                                    swHome.Stop();
                                    swHome.Reset();

                                    bHome = false;
                                    bHomeTrack = new bool[17];
                                    CommonSet.WriteInfo("回原点超时！");
                                    CommonSet.WriteInfo("回原点失败！");
                                    mc.StopAllAxis();
                                    mc.bHome = false;
                                }
                                bool bflag = true;
                                for (int i = 0; i < 9; i++)
                                {
                                    bflag = bflag && bHomeTrack[i];

                                }
                                if (bflag)
                                {
                                    swHome.Stop();
                                    swHome.Reset();
                                    bHome = false;
                                    bHomeTrack = new bool[17];
                                    //((LeiE3032)mc).ResetExtraEncoder();
                                    mc.SetEncoderValue(AXIS.C1轴, 0);
                                    mc.SetEncoderValue(AXIS.C2轴, 0);
                                    CommonSet.WriteInfo("回原点完成");
                                    mc.bHome = false;
                                    break;
                                }
                                #endregion
                            }
                            //if (mc.dic_DI[DI.复位按钮] && !bReset)
                            //{
                            //    swReset.Start();
                            //    if (swReset.ElapsedMilliseconds > 100)
                            //    {
                            //        bReset = true;
                            //    }

                            //}
                            mc.setDO(DO.启动按钮灯, false);
                            mc.setDO(DO.暂停按钮灯, false);
                           
                            //复位
                            if (bReset)
                            {
                                mc.setDO(DO.复位按钮灯, true);
                                mc.ResetAxisAlarm();//复位驱动器报警
                                if (Alarminfo.BAlarm)
                                {
                                    Alarminfo.ClearAllAlarm();
                                    bReset = false;
                                    break;
                                }
                                
                                ResetModule();
                                if (camLModule.bResetFinish && camRModule.bResetFinish && assemLModule.bResetFinish && assemRModule.bResetFinish && barrelModule.bResetFinish)
                                {
                                    CommonSet.WriteInfo("复位完成！");
                                    bReset = false;
                                   
                                    break;
                                }
                            }
                            else
                            {
                                mc.setDO(DO.复位按钮灯, false);
                            }
                            break;

                        case RunMode.运行:
                            mc.setDO(DO.启动按钮灯, true);
                            mc.setDO(DO.暂停按钮灯, false);
                            mc.setDO(DO.复位按钮灯, false);
                            if (bRunStep)
                            {
                                if (bNext)
                                {
                                    bNext = false;

                                }
                                else
                                {
                                    break;
                                }

                            }
                            if (!bResetStatus)
                            {
                                ResetModule();
                                if (camLModule.bResetFinish && camRModule.bResetFinish && assemLModule.bResetFinish && assemRModule.bResetFinish && barrelModule.bResetFinish)
                                {
                                    CommonSet.WriteInfo("复位完成！");
                                  
                                    bResetStatus = true;
                                    break;
                                }
                            }
                            camLModule.MakeAction();
                            camRModule.MakeAction();
                            assemLModule.MakeAction();
                            assemRModule.MakeAction();
                            barrelModule.MakeAction();

                            break;

                        case RunMode.暂停:
                            swCircleTime.Stop();
                            mc.setDO(DO.启动按钮灯, false);
                            mc.setDO(DO.暂停按钮灯, true);
                            if (bReset)
                            {
                                mc.setDO(DO.复位按钮灯, true);
                                bReset = false;
                                if (Alarminfo.BAlarm)
                                {
                                   
                                    Alarminfo.ClearAllAlarm();
                                   
                                    break;
                                }
                               
                                //ResetModule();
                                //if (camLModule.bResetFinish && camRModule.bResetFinish && assemLModule.bResetFinish && assemRModule.bResetFinish && barrelModule.bResetFinish )
                                //{
                                //    CommonSet.WriteInfo("复位完成！");
                                //    bReset = false;
                                //    break;
                                //}



                                //if (bFull)
                                //{
                                //    ResetModule();
                                //    if (productModule1.bResetFinish && productModule2.bResetFinish && assem1Module.bResetFinish && assem2Module.bResetFinish && flashModule1.bResetFinish && flashModule2.bResetFinish && barrelModule.bResetFinish)
                                //    {
                                //        CommonSet.WriteInfo("满盘复位完成！");
                                //        bReset = false;
                                //        break;
                                //    }

                                //}
                            }
                            else
                            {
                                mc.setDO(DO.复位按钮灯, false);
                            }
                         
                            break;
                        case RunMode.单轴测试:
                            SetCalibVel();
                            if (bTestFlag[0])
                                testAxisModule.MakeAction();
                            break;
                        case RunMode.取料测试:
                            SetCalibVel();
                            if (bTestFlag[1])
                            { 
                                testGetLModule.MakeAction();
                            }
                            if (bTestFlag[2])
                            {
                                testGetRModule.MakeAction();
                            }
                            break;
                        case RunMode.取料标定:
                            SetCalibVel();
                            if (bTestFlag[3])
                            {
                                calibLModule.MakeAction();
                            }
                            if (bTestFlag[4])
                            {
                                calibRModule.MakeAction();
                            }
                            break;
                        case RunMode.组装标定:
                            SetCalibVel();
                            if (bTestFlag[5])
                            {
                                calibBLModule.MakeAction();
                            }
                            if (bTestFlag[6])
                            {
                                calibBRModule.MakeAction();
                            }
                            break;
                        case RunMode.旋转:
                            rotateModule.MakeAction();

                            break;
                    }
                   
                  


                }
                catch (Exception ex)
                {
                    CommonSet.WriteDebug("运行异常:", ex);
                }
                Thread.Sleep(5);


            }
           

        }
        public static Stopwatch swPressTime = new Stopwatch();
        int PressButton = 0;
        long PressTime = 50;//ms
        public void PressJudge()
        {
            //启动
            if (mc.dic_DI[DI.启动] && !mc.dic_DI[DI.暂停] && !mc.dic_DI[DI.复位] && !mc.dic_DI[DI.停止])
            {
                PressButton = 1;
            }//暂停
            else if (!mc.dic_DI[DI.启动] && mc.dic_DI[DI.暂停] && !mc.dic_DI[DI.复位] && !mc.dic_DI[DI.停止])
            {
                PressButton = 2;
            }//复位
            else if (!mc.dic_DI[DI.启动] && !mc.dic_DI[DI.暂停] && mc.dic_DI[DI.复位] && !mc.dic_DI[DI.停止])
            {
                PressButton = 3;
            }//停止
            else if (!mc.dic_DI[DI.启动] && !mc.dic_DI[DI.暂停] && !mc.dic_DI[DI.复位] && mc.dic_DI[DI.停止])
            {
                PressButton = 4;
            }
            else
            {
                swPressTime.Stop();
                swPressTime.Reset();
                PressButton = 0;
            }
           


            switch (PressButton)
            {
                //启动
                case 1:
                    if (swPressTime.IsRunning)
                    {
                        if (runMode == RunMode.手动)
                        {
                            if (swPressTime.ElapsedMilliseconds > 2000)
                            {
                                swCircleTime.Start();
                                camLModule.bResetFinish = false;
                                camRModule.bResetFinish = false;
                                assemLModule.bResetFinish = false;
                                assemRModule.bResetFinish = false;
                                barrelModule.bResetFinish = false;
                                runMode = RunMode.运行;
                            }
                        }
                        else if (runMode == RunMode.暂停)
                        {
                            if (swPressTime.ElapsedMilliseconds > PressTime)
                            {
                                swCircleTime.Start();
                                runMode = RunMode.运行;
                            }
                        }
                    }
                    else
                    {
                        swPressTime.Restart();
                    }

                    break;
                //暂停
                case 2:
                    if (swPressTime.IsRunning)
                    {
                        if (runMode == RunMode.运行)
                        {
                            if (swPressTime.ElapsedMilliseconds > PressTime)
                            {
                               
                                runMode = RunMode.暂停;
                            }
                        }
                        
                    }
                    else
                    {
                        swPressTime.Restart();
                    }
                    break;
                    //复位
                case 3:
                    if (swPressTime.IsRunning)
                    {
                        if (runMode == RunMode.手动)
                        {
                            if (swPressTime.ElapsedMilliseconds > PressTime)
                            {
                                camLModule.bResetFinish = false;
                                camRModule.bResetFinish = false;
                                assemLModule.bResetFinish = false;
                                assemRModule.bResetFinish = false;
                                barrelModule.bResetFinish = false;
                                bReset = true;
                            }
                        }else if (runMode == RunMode.暂停)
                        {
                            if (swPressTime.ElapsedMilliseconds > PressTime)
                            {
                                camLModule.bResetFinish = false;
                                camRModule.bResetFinish = false;
                                assemLModule.bResetFinish = false;
                                assemRModule.bResetFinish = false;
                                barrelModule.bResetFinish = false;
                                bReset = true;
                            }
                        }

                    }
                    else
                    {
                        swPressTime.Restart();
                    }

                    break;
                default:
                    break;

            }

        }

        public void ResetModule()
        {
            //bResetStatus = false;
           

            camLModule.Reset();
            camRModule.Reset();
            assemLModule.Reset();
            assemRModule.Reset();
            barrelModule.Reset();

        }

        private static bool bSetVel = false;
        private static double dVelAssemX = 100;
        private static double dVelZ = 20;
        //设置标定时的速度
        private static void SetCalibVel()
        {
            if (!bSetVel)
            {
                bSetVel = true;
                dVelAssemX = ModelManager.VelAssemX.Vel;
                dVelZ = ModelManager.VelZ.Vel;
                ModelManager.VelAssemX.Vel = 100;
                ModelManager.VelZ.Vel = 20;
               
            }
        }
        private static void ResetVel()
        {
            if (bSetVel)
            {
                bSetVel = false;
                ModelManager.VelAssemX.Vel = dVelAssemX;
                ModelManager.VelZ.Vel = dVelZ;

            }
        }
    }
}
