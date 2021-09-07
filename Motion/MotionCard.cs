using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigureFile;
using APS168_W32;
using APS_Define_W32;
using log4net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
namespace Motion0
{
    //常用输出
    public enum DO
    {
        //卡1
        左吸嘴吸气=8,
        左吸嘴吹气,
        右吸嘴吸气,
        右吸嘴吹气,
        镜筒吸气,
        旋转气缸,
        前后气缸,
        震盘吸气1,
        震盘吸气2,
        报警清除继电器=18,

        绿=24,
        黄,
        红,
        蜂鸣器,
        启动按钮灯,
        复位按钮灯,
        停止按钮灯,
        暂停按钮灯,
    }
    //常用输入
    public enum DI
    {
       //卡1
        镜片盘1有料感应=8,
        镜片盘2有料感应,
        镜筒盘1有料感应,
        镜筒盘2有料感应,

        左吸嘴负压感应1=14,
        左吸嘴正压感应1,
        右吸嘴负压感应1,
        右吸嘴正压感应1,
        镜筒负压感应1=19,
        镜筒负压感应2,

        //卡2
        启动=24,
        复位,
        停止,
        急停,
        暂停,
        旋转气缸原点,
        旋转气缸动点,
        前后气缸原点,
        前后气缸动点,
        A2轴定位完成 = 36,
        A6轴定位完成,
        A2轴报警,
        A6轴报警,
        //卡3
        A1轴报警 = 40,
        A3轴报警,
        A4轴报警,
        A5轴报警,
        A7轴报警,
        A8轴报警,
        A9轴报警,
        A10轴报警,
        A11轴报警,
        A12轴报警,


    }
    //轴号
    public enum AXIS {
        //卡1
        镜筒Y轴 = 1001,
        取料Y1轴,
        取料Y2轴,
        组装X2轴,
        组装X1轴,
        取料X2轴,
        镜筒X轴,
        取料X1轴,
        组装Z2轴,
        组装Z1轴,
        C1轴,
        C2轴,
        


    }
    //轴IO状态和速度，位置等
    public struct AXStatus {
        //IO状态
        public bool ALM;//报警
        public bool PEL;//正极限
        public bool MEL;//负极限
        public bool ORG;//原点
        public bool EMG;//急停
        public bool EZ;//Z相
        public bool INP;//到位信号
        public bool SVON;//使能
        public bool RDY;//Ready
        public double dVel;//反馈速度
        public double dPos;//反馈位置
        public double dCmdPos;//目标位置
    }
    public abstract class MotionCard
    {
        protected static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
         
        private static MotionCard mc = null;
        public Action<string> ShowInfo;//显示信息的委托 
        public Dictionary<DI, bool> dic_DI = new Dictionary<DI, bool>();//获取到的输入状态
        public Dictionary<DO, bool> dic_DO = new Dictionary<DO, bool>();//获取到的输出状态

        public Dictionary<AXIS, AXStatus> dic_Axis = new Dictionary<AXIS, AXStatus>();//获取到的轴状态
        public long lRuntime = 0;//一个循环周期时间

        public readonly static Dictionary<AXIS, double> dic_AxisRatio = new Dictionary<AXIS, double>();
        public Dictionary<AXIS, bool> dic_HomeStatus = new Dictionary<AXIS, bool>();//回原点标志，标志为true时，回原点完成
        public Dictionary<AXIS, ushort> dic_AbsAxis = new Dictionary<AXIS, ushort>();//存储绝对编码器的序号，用于初始化和判断软硬极限使用
        public bool bHome = false;

        internal MotionCard()
        {
          
        }
        /// <summary>
        /// 获取板卡实例
        /// </summary>
        /// <returns></returns>
        public static MotionCard getMotionCard()
        {
            if (mc == null)
            {
                mc = new LeiE3032();//根据所用的卡实例化
                return mc;
            }
            else
            {
                return mc;
            }
        }

        public abstract void WriteOutDA(float value, ushort subindex);
        public abstract float ReadOutAD(ushort subindex);
        /// <summary>
        /// 初始化运动控制卡
        /// </summary>
        /// <param name="strXmlFile">参数文件</param>
        public abstract void initCard(string strXmlFile);

        /// <summary>
        /// 电机使能
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="status">状态,0代表关,1代表开</param>
        public abstract void SeverOn(AXIS axis, int status);
        /// <summary>
        /// 复位轴报警
        /// </summary>
        public abstract void ResetAxisAlarm();

        /// <summary>
        /// 使能所有轴
        /// </summary>
        public abstract void SeverOnAll();
        /// <summary>
        /// 关闭所有轴使能
        /// </summary>
        public abstract void SeverOffAll();
        /// <summary>
        /// 绝对运动，轴以指定最大速度运行
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>    
        public abstract void AbsMove(AXIS axis, int pos, int vel);
        /// <summary>
        /// 绝对运动，轴以指定最大速度运行
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        public abstract void AbsMove(AXIS axis, double pos, int vel);
        public abstract void AbsMove(AXIS axis, double pos, double vel);
        public abstract void AbsMove(AXIS axis, double pos, double vel, double acc = 0.1, double dec = 0.1);
        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="len">运动距离</param>
        /// <param name="vel">最大运行速度</param>
        public abstract void RelativeMove(AXIS axis, int len, double vel);
        /// <summary>
        /// 速度运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">最大运行速度</param>
        public abstract void VelMove(AXIS axis, int vel);
        /// <summary>
        /// 停止运动
        /// </summary>
        /// <param name="axis">轴号</param>
        public abstract void StopAxis(AXIS axis);
        /// <summary>
        /// 停止所有轴
        /// </summary>
        public abstract void StopAllAxis();
        /// <summary>
        /// 回原点
        /// </summary>
        /// <param name="axis">轴号</param>

        public abstract void Home(AXIS axis);

        /// <summary>
        /// 绝对值编码器回原点,返回0位置
        /// </summary>
        /// <param name="axis"></param>
        public abstract void HomeAbs(AXIS axis);
        /// <summary>
        /// 启动比较触发
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="data">数据列表</param>
        public abstract void startCmpTrigger(AXIS axis, int[] data);
        /// <summary>
        /// 启动比较触发
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="data">数据列表</param>
        public abstract void startCmpTrigger(AXIS axis, double[] data, ushort cmpPort = 0);
        /// <summary>
        /// 启动比较触发
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="data">数据列表</param>
        public abstract void startCmpTrigger2(AXIS axis, double[] data, ushort cmpPort = 0,ushort cmpPort2 = 1);

        public abstract void stopCmpTrigger(AXIS axis);
        public abstract void stopCmpTrigger(AXIS axis, ushort cmpPort = 0);
        public abstract void stopCmpTrigger(AXIS axis, ushort cmpPort = 0,ushort cmpPort1 = 1);
        public abstract void Interpolation_2D_Arc_Move(AXIS axis1, AXIS axis2, double centerX, double centerY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1);
        public abstract void Interpolation_2D_Arc_Move2(AXIS axis1, AXIS axis2, double centerX, double centerY, double endX,double endY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1);
        /// <summary>
        /// 在运动过程中重置速度
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="newSpeed">设置速度</param>
        public abstract void SpeedOvrd(AXIS axis, int newSpeed);
        /// <summary>
        /// 绝对运动过程中更改目标位置和速度
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="newPos">更改的目标位置</param>
        /// <param name="newSpeed">更改的速度</param>
        public abstract void AbsMoveOvrd(AXIS axis, int newPos, int newSpeed);
        /// <summary>
        /// 查询轴的到位信号 
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>返回结果</returns>
        public abstract bool IsAxisINP(AXIS axis);
     
        /// <summary>
        /// 更新数据，包括IO状态和轴数据
        /// </summary>
        public abstract void updateStatus();
     
        /// <summary>
        /// 设置输出
        /// </summary>
        /// <param name="d">设置输出的点，枚举类型DO</param>
        /// <param name="bValue">输出的值</param>
        public abstract void setDO(DO d, bool bValue);
        /// <summary>
        /// 获取指定轴的状态
        /// </summary>
        /// <param name="id">轴号</param>
        public abstract void getAxisStatus(AXIS id);
        /// <summary>
        /// 更新所有轴的状态
        /// </summary>
        public abstract void updateAxis();

        public abstract void SetEncoderValue(AXIS axis, double dSetValue);
       
       /// <summary>
        /// 通过Z相回原点
       /// </summary>
       /// <param name="axis">轴号</param>       
        public abstract void HomeZ(AXIS axis);
        /// <summary>
        /// 释放卡片资源
        /// </summary>
        public abstract void releaseCard();

        public abstract void writeInfo(string info);
        public abstract void writeDebug(string info, Exception ex);


    }
}
