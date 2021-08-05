using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Motion;
using System.Diagnostics;
namespace Assembly
{
    /// <summary>
    /// 报警信息
    /// </summary>
   public class Alarminfo
    {
       private static List<Alarm> lstAlarm = new List<Alarm>();
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
                            }

                            Alarm info = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "驱动器报警");
                            //伺服报警
                            if (pair.Value.ALM)
                            {
                                
                                Alarminfo.AddAlarm(info);
                                //Alarminfo.AddAlarm(Alarm.Z1轴伺服报警 + ((int)pair.Key ));
                               
                                //如果不是绝对编码器的轴和取料C轴
                                if (!mc.dic_AbsAxis.Keys.Contains(pair.Key)&&!pair.Key.ToString().Contains("取料C"))
                                {
                                    Alarm org = (Alarm)Enum.Parse(typeof(Alarm), pair.Key.ToString() + "原点丢失");
                                    Alarminfo.AddAlarm(org);
                                }
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
                    foreach (KeyValuePair<AXIS,bool> pair in mc.dic_HomeStatus)
                    {

                        if (!mc.dic_AbsAxis.Keys.Contains(pair.Key))
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
                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(100);
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
       取料X1轴驱动器报警,
       取料Y1轴驱动器报警,
       取料Z1轴驱动器报警,
       取料C1轴驱动器报警,
       取料C2轴驱动器报警,
       取料C3轴驱动器报警,
       取料C4轴驱动器报警,
       取料C5轴驱动器报警,
       取料X2轴驱动器报警,
       取料Y2轴驱动器报警,
       取料Z2轴驱动器报警,
       取料C6轴驱动器报警,
       取料C7轴驱动器报警,
       取料C8轴驱动器报警,
       取料C9轴驱动器报警,
       取料C10轴驱动器报警,
       镜筒Y轴驱动器报警,
       组装X1轴驱动器报警,
       组装Y1轴驱动器报警,
       组装Z1轴驱动器报警,
       组装X2轴驱动器报警,
       组装Y2轴驱动器报警,
       组装Z2轴驱动器报警,
       点胶X轴驱动器报警,
       点胶Y轴驱动器报警,
       点胶Z轴驱动器报警,
       点胶C轴驱动器报警,


       取料Y1轴使能off,
       取料Y2轴使能off,
       组装Z1轴使能off,
       组装Z2轴使能off,
       点胶X轴使能off,
       取料Z1轴使能off,
       取料Z2轴使能off,
       镜筒Y轴使能off,
       点胶Z轴使能off,
       取料X1轴使能off,
       取料X2轴使能off,
       组装X1轴使能off,
       组装X2轴使能off,
       组装Y1轴使能off,
       组装Y2轴使能off,
       取料C1轴使能off,
       取料C2轴使能off,
       取料C3轴使能off,
       取料C4轴使能off,
       取料C5轴使能off,
       取料C6轴使能off,
       取料C7轴使能off,
       取料C8轴使能off,
       取料C9轴使能off,
       取料C10轴使能off,
       点胶C轴使能off,
       点胶Y轴使能off,

       取料Y1轴原点丢失,
       取料Y2轴原点丢失,
       组装Z1轴原点丢失,
       组装Z2轴原点丢失,
       点胶X轴原点丢失,
       取料Z1轴原点丢失,
       取料Z2轴原点丢失,
       镜筒Y轴原点丢失,
       点胶Z轴原点丢失,
       取料X1轴原点丢失,
       取料X2轴原点丢失,
       组装X1轴原点丢失,
       组装X2轴原点丢失,
       组装Y1轴原点丢失,
       组装Y2轴原点丢失,
       取料C1轴原点丢失,
       取料C2轴原点丢失,
       取料C3轴原点丢失,
       取料C4轴原点丢失,
       取料C5轴原点丢失,
       取料C6轴原点丢失,
       取料C7轴原点丢失,
       取料C8轴原点丢失,
       取料C9轴原点丢失,
       取料C10轴原点丢失,
       点胶C轴原点丢失,
       点胶Y轴原点丢失,
       //Z1轴使能off,
       //Z2轴使能off,
       //Y1轴使能off,
       //C轴使能off,
       //X1轴使能off,
       //X2轴使能off,
       //Y2轴使能off,



       //Z1轴原点丢失,
       //Z2轴原点丢失,
       //Y1轴原点丢失,
       //C轴原点丢失,
       //X1轴原点丢失,
       //X2轴原点丢失,
       //Y2轴原点丢失,
       组装1飞拍数目异常,
       组装2飞拍数目异常,
       取料1飞拍数目异常,
       取料2飞拍数目异常,

      
       
      
       取料上相机1拍照超时,
       取料上相机2拍照超时,
       组装上相机1拍照超时,
       组装上相机2拍照超时,
       取料下相机1飞拍超时,
       取料下相机2飞拍超时,
       组装下相机1飞拍超时,
       组装下相机2飞拍超时,
       组装1镜筒检测NG,
       组装2镜筒检测NG,

       取料吸笔1下降超时,
       取料吸笔2下降超时,
       取料吸笔3下降超时,
       取料吸笔4下降超时,
       取料吸笔5下降超时,
       取料吸笔6下降超时,
       取料吸笔7下降超时,
       取料吸笔8下降超时,
       取料吸笔9下降超时,
       取料吸笔10下降超时,
       取料吸笔11下降超时,
       取料吸笔12下降超时,
       取料吸笔13下降超时,
       取料吸笔14下降超时,
       取料吸笔15下降超时,
       取料吸笔16下降超时,
       取料吸笔17下降超时,
       取料吸笔18下降超时,

       组装吸笔1下降超时,
       组装吸笔2下降超时,
       组装吸笔3下降超时,
       组装吸笔4下降超时,
       组装吸笔5下降超时,
       组装吸笔6下降超时,
       组装吸笔7下降超时,
       组装吸笔8下降超时,
       组装吸笔9下降超时,
       组装吸笔10下降超时,
       组装吸笔11下降超时,
       组装吸笔12下降超时,
       组装吸笔13下降超时,
       组装吸笔14下降超时,
       组装吸笔15下降超时,
       组装吸笔16下降超时,
       组装吸笔17下降超时,
       组装吸笔18下降超时,

       中转位工位1吸真空检测异常,
       中转位工位2吸真空检测异常,
       
       组装工位1夹子夹紧超时,
       组装工位2夹子夹紧超时,
       组装工位1夹子张开超时,
       组装工位2夹子张开超时,
       点胶位夹子张开超时,
       点胶位夹子夹紧超时,
       点胶位真空检测异常,
       点胶气缸上升超时,
       点胶气缸下降超时,
       点胶上相机拍照超时,
       点胶下相机拍照超时,
       点胶上相机拍照检测NG,
       点胶下相机拍照检测NG,

       组装工位1吸真空检测无料,
       组装工位2吸真空检测无料,
       镜筒夹子1张开超时,
       镜筒夹子2张开超时,
       镜筒夹子1闭合超时,
       镜筒夹子2闭合超时,
       镜筒气缸1下降超时,
       镜筒气缸2下降超时,
       镜筒气缸1上升超时,
       镜筒气缸2上升超时,
       C轴吸真空检测无料,
       C轴夹子闭合超时,
       C轴夹子张开超时,
       飞拍1全部吸笔上升超时,
       飞拍1全部吸笔下降超时,
       飞拍2全部吸笔上升超时,
       飞拍2全部吸笔下降超时,
       吸笔吸取NG,
       取料吸笔连续吸取NG,
       取料盘空,
       取料1有空盘,
       取料2有空盘,
       镜筒盘满,
       吸笔抛料位1下降超时,
       吸笔抛料位2下降超时,
       放料位真空检测异常,
       取料1飞拍图像数目异常,
       取料2飞拍图像数目异常,
       组装1飞拍图像数目异常,
       组装2飞拍图像数目异常,
       取料1飞拍图像NG,
       取料2飞拍图像NG,
       组装1飞拍图像NG,
       组装2飞拍图像NG,

       飞拍图像处理超时,
       组装吸笔在中转1取料真空检测异常,
       组装吸笔在中转2取料真空检测异常,
       组装1吸笔取料时下降超时,
       组装2吸笔取料时下降超时,
       组装1吸笔取料时上升超时,
       组装2吸笔取料时上升超时,
       组装1部吸笔上升超时,
       组装2部吸笔上升超时,
       镜筒吸笔1上升超时,
       成品吸笔2上升超时,
       镜筒吸笔1下降超时,
       成品吸笔2下降超时,
       镜筒吸笔1真空检测无料,
       成品吸笔2真空检测无料,
       求心1张开超时,
       求心1闭合超时,
       求心2张开超时,
       求心2闭合超时,
       镜片夹子张开超时,
       镜片夹子闭合超时,
       组装位1真空检测异常,
       组装位2真空检测异常,
       点胶气缸1上升超时,
       点胶气缸1下降超时,
       点胶气缸2上升超时,
       点胶气缸2下降超时,
       镜筒相机摄像超时,
       工位1镜筒中心NG,
       工位2镜筒中心NG,
       工位1镜筒角度NG,
       工位2镜筒角度NG,
      
       打开电气比例阀串口异常,
       设置组装压力电流异常,
       打开测高串口异常,
       打开测压串口1异常,
       打开测压串口2异常,   
       
       取料1上相机连接异常,
       取料1下相机连接异常,
       取料2上相机连接异常,
       取料2下相机连接异常,
       组装1上相机连接异常,
       组装1下相机连接异常,
       组装2上相机连接异常,
       组装2下相机连接异常,
       点胶和镜筒上相机连接异常,
       点胶和镜筒下相机连接异常,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
    }
}
