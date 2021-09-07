using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion0;
using System.Diagnostics;
using System.IO;
namespace _0E030
{
    //工作流程状态
    public enum ActionState { 
        //取料状态
        取料,
        组装,
        抛料,
        拍照,
        等待,
    }
    //动作定义
    public enum ActionName
    {

        #region"取料和拍照"
        _10开始取料,
        _10取料条件判断,
        
        _10计算吸笔XY坐标,
        _10XY到取料位,
        _10X轴到取料位,
        _10X轴到下相机拍照位,
        _10X轴到下相机拍照位完成,
        _10X轴到组装位,
        _10X轴到抛料位,
        _10X轴到位完成,
        _10XY到安全位,
        _10XY到位完成,
        _10等待镜筒拍照完成,
        _10Z轴到安全位,
        _10Z轴到取料位,
        _10Z轴到取标定块位,
        _10Z轴到标定块拍照高度,
        _10Z轴到组装位,
        _10Z轴到位,
        _10Z轴到位吸真空,
        _10Z轴上升时吸真空检测,
        _10下相机拍照,
        _10下相机拍照完成,

        _10吸笔吸真空,
        _10吸笔破真空,
        _10吸笔真空检测,
 
        _10吸笔关闭吸真空,
   
        _10取料完成,
        _10抛料完成,

        _10判断使用二次组装,
        _10Z轴到组装准备位,
        _10C轴旋转,
        _10镜筒XY轴到组装位,
        _10镜筒XY轴到位完成,
        _10X轴组装位到位判断,
        _10X轴离开组装位判断,
        _10组装完成,

        

        #endregion

        #region"拍照"
        _20相机拍照,
        _20相机拍照完成,
        _20XY到安全位,
        _20XY轴到拍照位,
        _20XY到标定拍照位,
        _20XY轴到位完成,
        _20获取拍照位,
        _20开始拍照,
        _20拍照完成,
        #endregion
        #region"单轴测试"
        _40开始测试,
        _40轴到点位,
        _40到位完成,
        _40测试完成,
        #endregion
        #region"组装"


        _30等待组装,
        _30吸笔全部下降,
        _30吸笔全部上升,
        _30吸笔全部吸真空,
        _30吸笔全部破真空,
        _30取料位真空关闭,
        _30吸笔真空检测,
        _30X轴到取料位,
        _30XY轴到位,
        _30复位取料标志,
        _30开始飞拍,
        _30求心张开,
        _30求心闭合,
        _30求心张开检测,
        _30Z轴到求心位,
        _30飞拍完成,
        _30X轴到飞拍结束位,
        _30X轴到位,
        _30X轴到飞拍位,

        _30Y轴到镜筒拍照位,
        _30Y轴到位完成,
        _30XY到镜筒拍照位,
        _30开始组装,
        _30开始抛料,
        _30抛料完成,
        _30获取组装吸笔,
        _30Z轴到镜筒拍照高度,
        _30Z轴到吸笔拍照高度,
        _30Z轴到取料位,
        _30Z轴到安全位,
        _30Z轴到待机位,
        _30Z轴到抛料位,
        _30Z轴到位完成,
        _30Z轴到组装位,
        _30Z轴到组装上升位,
        _30Z轴到组装准备位,
        _30Z轴到组装位完成,
        _30Z轴到组装测压位,
        _30读取数据,
        _30Z轴测压判断,
        _30XY轴到组装位,
        _30XY轴到抛料位,
        _30XY轴到待机位,
        _30XY轴到组装验证位,
        _30XY轴到位完成,
        _30吸笔下降,
        _30吸笔上升,
        _30吸笔下降判定,
        _30吸笔破真空,
        _30吸笔停留时间,
        _30镜筒标定块拍照,
        _30镜筒固定块拍照,
        _30镜筒拍照完成,
        //_30Z轴到二次组装高度,
        
        _30使用二次拍照,
        _30使用打压,
        _30设置组装压力,
        _30打压吸笔上升,
        _30打压吸笔下降,
        _30打压时间保持,
        _30打压完成,
        _30组装完成,
        _30取料完成,
        _30验证完成,
        _30验证判断,
        #endregion

     



        #region"取料测试动作"
         _50动作选择,
         _50动作完成,
        #endregion

        #region"相机对中心动作"
        _a0Z轴到安全位,
        _a0Z轴到位完成,
        _a0XY轴补正移动,
        _a0XY到位完成,
        _a0上相机拍照,
        _a0上相机拍照完成,
        _a0对位完成,
        #endregion
        #region"旋转中心"
        _b0C轴到原点位,
        _b0C轴到位,
        _b0C轴定长旋转,
        _b0镜筒中心拍照,
        _b0中心拍照完成,
        #endregion

    }
    public abstract class ActionModule
    {
        public List<ActionName> lstAction = new List<ActionName>();
        public static MotionCard mc = null;
        public Watch sw = new Watch();//计时器
        public int iActionTime = 3000;//动作允许执行时间
        public static ActionState CameraLState = ActionState.等待;
        public static ActionState AssemLState = ActionState.等待;
        public static ActionState BarrelState = ActionState.等待;
        public static ActionState CameraRState = ActionState.等待;
        public static ActionState AssemRState = ActionState.等待;
       
        //public static ActionState GetState1 = ActionState.空闲;//取料流程1模块状态
        //public static ActionState GetState2 = ActionState.空闲;//取料流程2模块状态
        //public static ActionState AssemState1 = ActionState.空闲;//组装1流程状态
        //public static ActionState AssemState2 = ActionState.空闲;//组装2流程状态
        //public static ActionState GlueAndBarrelState = ActionState.镜筒取料;//点胶和镜筒
        public bool bResetFinish = false;
        public Stopwatch swStep = new Stopwatch();//单步计时
        public long lStepTime = 0;//单步时长
       // public int iResetStep = 0;
       // public int iMaxStep = 0;//动作模块的最大步数
        private int iStep = 0;//动作索引
        public int IStep
        {
            get { return iStep; }
            set
            {
                iStep = value;
                int count = lstAction.Count;
                if (iStep > count - 1)
                {
                    iStep = 0;
                }
            }

        }
        private int iStepListener = -1;//跟踪动作步骤
        public ActionModule()
        {
            mc = MotionCard.getMotionCard();
        }
        /// <summary>
        /// 执行动作流程
        /// </summary>
        public void MakeAction()
        {
           // lStepTime = swStep.ElapsedMilliseconds;
            //如果跟踪步骤与运行的不一致，则复位报警计时器
            if (iStepListener != IStep)
            {
                sw.ResetAlarmWatch();
                sw.ResetSetWatch();
                iStepListener = IStep;
              
                swStep.Stop();
                swStep.Restart();
                
            }
            
            Action(lstAction[iStep], ref iStep);
            int count = lstAction.Count;
            if (iStep > count - 1)
            {
                iStep = 0;
            }
            Action2();

        }
        public void WriteOutputInfo(string info)
        {
            CommonSet.WriteInfo(info + "---" + swStep.ElapsedMilliseconds.ToString()+ "ms");
        }
        public abstract void Reset();
        public abstract void Action(ActionName action, ref int step);
        public abstract void Action2();
       
        public bool IsAxisINP(double pos, AXIS axis,double diff = 0.02)
        {
            if ((Math.Abs(mc.dic_Axis[axis].dPos - pos) < diff) && mc.dic_Axis[axis].INP)
            {
               // CommonSet.WriteInfo(axis.ToString()+"到达位置:"+pos.ToString());
                return true;
            }
               
            return false;

        }
        public bool IsAxisINP(AXIS axis)
        {
            if (mc.dic_Axis[axis].INP)
            {
               
                return true;

            }
            return false;

        }
        public bool IsAxisINP2(double pos, AXIS axis, double diff = 0.02)
        { 
            
              if ((Math.Abs(mc.dic_Axis[axis].dPos - pos) < diff))
            {
               // CommonSet.WriteInfo(axis.ToString()+"到达位置:"+pos.ToString());
                return true;
            }
               
            return false;
        }
            
       
    

      
    }
    /// <summary>
    /// 计时器类
    /// </summary>
    public class Watch
    {
        private Stopwatch sw = new Stopwatch();//动作超时计时
        // private  long lTime = 0;//动作超时计时比较时间    
        private Stopwatch swSet = new Stopwatch();//设定时间计时

        public Watch()
        {


        }
        /// <summary>
        /// 重置计时，在执行某个动作之前调用
        /// </summary>
        public void ResetAlarmWatch()
        {
            sw.Stop();
            sw.Reset();

        }
        /// <summary>
        /// 超时计时，超过设定时间返回true,否则返回false
        /// </summary>
        /// <param name="time">设定等待时间,单位ms</param>
        /// <returns></returns>
        public bool AlarmWaitTime(long time)
        {
            sw.Start();
            long l = sw.ElapsedMilliseconds;
            if (l > time)
            {
                ResetAlarmWatch();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 复位设置定时器
        /// </summary>
        public void ResetSetWatch()
        {
            swSet.Stop();
            swSet.Reset();

        }
        /// <summary>
        /// 设定计时器,超过设定时间返回true
        /// </summary>
        /// <param name="time">设定等待时间,单位ms</param>
        /// <returns></returns>
        public bool WaitSetTime(long time)
        {
            swSet.Start();
            long l = swSet.ElapsedMilliseconds;
            if (l > time)
            {
                ResetSetWatch();
                return true;
            }
            return false;
        }
    }
}
