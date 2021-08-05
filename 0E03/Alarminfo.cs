using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Motion;
using System.Diagnostics;
namespace _0E03
{
    /// <summary>
    /// 报警信息
    /// </summary>
   public class Alarminfo
    {
       public static List<Alarm> lstAlarm = new List<Alarm>();
        private static Dictionary<Alarm, int> dic_Alarm = new Dictionary<Alarm, int>();
       private static Object obj = new Object();
       private static Thread th_Alarm = null;
       private MotionCard mc = null;
       private static bool bAlarm = false;//是否有报警信息
       public static bool bRun = false;//是否是运行状态
       public static bool bPause = false;//暂停
       public static bool BAlarm
       {
           get { return Alarminfo.bAlarm; }
       }
      
       public Alarminfo()
       {
           mc = MotionCard.getMotionCard();
           th_Alarm = new Thread(AlarmListen);
           th_Alarm.Start();
       }
        public static List<Alarm> GetAlarmList()
        {
                return lstAlarm;
           
        }
        public void AlarmListen()
        {
            while (true)
            {
                try
                {
                    try
                    {
                        //监听轴报警信息
                        foreach (KeyValuePair<AXIS, AXStatus> pair in mc.dic_Axis)
                        {
                            //if (pair.Key.Equals(AXIS.组装Y1轴))
                            //    continue;

                            Alarm svon = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "使能off");
                            if (!pair.Value.SVON)
                            {
                                Alarminfo.AddAlarm(svon);
                                if (!pair.Key.ToString().Contains("C"))
                                {
                                    mc.dic_HomeStatus[pair.Key] = false;
                                }
                            }
                            else
                            {
                                Alarminfo.RemoveAlarm(svon);
                            }

                            Alarm info = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "驱动器报警");
                            //伺服报警
                            if (pair.Value.ALM)
                            {
                                
                                Alarminfo.AddAlarm(info);
                                if (!pair.Key.ToString().Contains("C"))
                                {
                                    mc.dic_HomeStatus[pair.Key] = false;
                                }
                                //Alarminfo.AddAlarm(Alarm.Z1轴伺服报警 + ((int)pair.Key ));

                                ////如果不是绝对编码器的轴和取料C轴
                                //if (!mc.dic_AbsAxis.Keys.Contains(pair.Key)&&!pair.Key.ToString().Contains("取料C"))
                                //{
                                //    Alarm org = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "原点丢失");
                                //    Alarminfo.AddAlarm(org);
                                //}
                            }
                            else
                            {
                                Alarminfo.RemoveAlarm(info);
                            }
                            //急停按下
                            if (pair.Value.EMG)
                            {
                                Alarminfo.AddAlarm(Alarm.急停按下);
                            }
                            else
                            {
                                Alarminfo.RemoveAlarm(Alarm.急停按下);
                            }

                        }
                    }
                    catch (Exception)
                    {
                    }
                    //int countAxis = mc.dic_HomeStatus.Count;
                    //string[] values = Enum.GetNames(typeof(AXIS));
                    //foreach (string value in values)
                    //{

                    //    AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                    //    if (!mc.dic_HomeStatus.ContainsKey(axis))
                    //        continue;

                    //    Alarm org = (Alarm)Enum.Parse(typeof(Alarm), axis.ToString() + "原点丢失");
                    //    if (mc.dic_HomeStatus[axis])
                    //        Alarminfo.RemoveAlarm(org);
                    //    else
                    //        Alarminfo.AddAlarm(org);

                    //}
                    foreach (KeyValuePair<AXIS, bool> pair in mc.dic_HomeStatus)
                    {

                        if (!pair.Key.ToString().Contains("C"))
                        {

                            Alarm org = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "原点丢失");
                            if (pair.Value)
                                Alarminfo.RemoveAlarm(org);
                            else
                                Alarminfo.AddAlarm(org);
                        }

                    }
                    if (lstAlarm.Count > 0)
                    {
                        bAlarm = true;

                    }
                    else
                    {
                        bAlarm = false;
                    }
                   
                    ShowAlarmLight();
                   // CommonSet.WriteInfo("报警数" + lstAlarm.Count.ToString());
                }
                catch (Exception ex)
                {
                    CommonSet.WriteInfo("报警模块异常"+ ex.ToString());
                }
                Thread.Sleep(50);
            }

        }
        private static Stopwatch sw = new Stopwatch();//闪灯或蜂鸣时长
       private long lTime = 0;
        /// <summary>
        /// 报警灯信息
        /// </summary>
        private void ShowAlarmLight()
        {
            //如果是运行情况下
            if (RunMode.运行 == Run.runMode)
            {
                if (bAlarm)
                {
                    Run.runMode = RunMode.暂停;

                }
                else
                {
                    mc.setDO(DO.红, false);
                    mc.setDO(DO.黄, false);
                    mc.setDO(DO.绿, true);
                    mc.setDO(DO.蜂鸣器, false);
                }
                lTime = 0;
                sw.Reset();
                sw.Stop();
            }
            else if (Run.runMode == RunMode.暂停)
            {
                if (bAlarm)
                {
                    sw.Start();
                    long l = sw.ElapsedMilliseconds;

                    mc.setDO(DO.黄, true);
                    mc.setDO(DO.绿, true);
                    long span = l - lTime;
                    //如果间隔少于1s,则蜂鸣
                    if (span < 1000)
                    {
                        mc.setDO(DO.红, true);
                        mc.setDO(DO.蜂鸣器, true);
                    }
                    else
                    {
                        mc.setDO(DO.红, false);
                        mc.setDO(DO.蜂鸣器, false);
                        if (span > 2000)
                            lTime = l;
                    }

                }
                else if (Run.bFull) //如果是盘满则长鸣3秒
                {
                        sw.Start();
                        long l = sw.ElapsedMilliseconds;
                        long span = l - lTime;
                        //如果间隔少于3s,则蜂鸣
                        if ((span < 3000)||Run.bReset)
                        {
                            mc.setDO(DO.红, false);
                            mc.setDO(DO.黄, true);
                            mc.setDO(DO.绿, true);
                            mc.setDO(DO.蜂鸣器, true);
                        }
                        else
                        {
                            Run.bFull = false;
                            sw.Reset();
                            sw.Stop();
                        }
                 }
                else
                {

                        mc.setDO(DO.红, false);
                        mc.setDO(DO.黄, true);
                        mc.setDO(DO.绿, true);
                        mc.setDO(DO.蜂鸣器, false);
                }
                

            }
            else
            {
                //手动状态下
                mc.setDO(DO.红, true);
                mc.setDO(DO.黄, false);
                mc.setDO(DO.绿, false);
                mc.setDO(DO.蜂鸣器, false);
                sw.Reset();
                sw.Stop();
                lTime = 0;
            }

        }

        /// <summary>
        /// 添加报警信息
        /// </summary>
        /// <param name="alarm">报警信息，枚举类型</param>
        public static void AddAlarm(Alarm alarm)
       {
           
            if (!lstAlarm.Contains(alarm))
            {
                lock (obj)
                {
                   lstAlarm.Add(alarm);
                    
                }
            }
       }
       /// <summary>
       /// 清除所有报警信息
       /// </summary>
       public static void ClearAllAlarm()
       {
           lock (obj)
           {
               lstAlarm.Clear();
               
           }
       }
       /// <summary>
       /// 清除某项报警信息
       /// </summary>
       /// <param name="alarm">报警信息</param>
       public static void RemoveAlarm(Alarm alarm)
       {
           if (lstAlarm.Contains(alarm))
           {
                lock (obj)
                {              
                   lstAlarm.Remove(alarm);
                }
           }
            
       }
       

    }
   public enum AlarmBlock
   { 
       取料1=1,
       取料2,
       飞拍1,
       飞拍2,
       组装1,
       组装2,
       镜筒和点胶,
       
   }
   public enum Alarm
   {
       急停按下,
        镜筒Y轴驱动器报警,
        取料Y1轴驱动器报警,
        取料Y2轴驱动器报警,
        组装X2轴驱动器报警,
        组装X1轴驱动器报警,
        取料X2轴驱动器报警,
        镜筒X轴驱动器报警,
        取料X1轴驱动器报警,
        组装Z2轴驱动器报警,
        组装Z1轴驱动器报警,
        C1轴驱动器报警,
        C2轴驱动器报警,
        

        镜筒Y轴使能off,
        取料Y1轴使能off,
        取料Y2轴使能off,
        组装X2轴使能off,
        组装X1轴使能off,
        取料X2轴使能off,
        镜筒X轴使能off,
        取料X1轴使能off,
        组装Z2轴使能off,
        组装Z1轴使能off,
        C1轴使能off,
        C2轴使能off,
        

        镜筒Y轴原点丢失,
        取料Y1轴原点丢失,
        取料Y2轴原点丢失,
        组装X2轴原点丢失,
        组装X1轴原点丢失,
        取料X2轴原点丢失,
        镜筒X轴原点丢失,
        取料X1轴原点丢失,
        组装Z2轴原点丢失,
        组装Z1轴原点丢失,
        
        C1轴原点丢失,
        C2轴原点丢失,
        

    
        左取料盘空料,
        右取料盘空料,
        镜筒盘无空镜筒,
        组装镜筒连续检测NG,
       
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
    }
}
