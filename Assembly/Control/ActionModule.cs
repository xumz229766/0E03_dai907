using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using System.Diagnostics;
using System.IO;
namespace Assembly
{
    //工作流程状态
    public enum ActionState { 
        //取料状态
        取料,
        飞拍和放料,
        //组装
        组装,
        空闲,
        组装完成,
        //镜筒和点胶
        镜筒取料,
        镜筒放料,
        取成品,
        点胶和放成品,
        等待,
    }
    //动作定义
    public enum ActionName
    {

        #region"取料1"
        _10开始取料,
        _10判断取料盘是否为空,
        _10取料条件判断,
        
        _10计算吸笔XY坐标,
        _10XY轴到拍照位,
        _10XY到取料位,
        _10X轴到飞拍起始位,
        _10X轴到飞拍结束位,
        _10X轴到位完成,
        _10XY到安全位,
        _10XY到位完成,
        _10Z轴到安全位,
        _10Z轴到取料运行位,
        _10Z轴到待机位,
        _10Z到取料位,
        _10Z轴到位,
        _10上相机拍照,
        _10上相机拍照完成,
        _10Z轴到飞拍高度,

        _10吸笔吸真空,
        _10吸笔真空检测,
        _10吸笔气缸下降到位,
        _10吸笔气缸下降,
        _10吸笔气缸上升,
        _10吸笔关闭吸真空,
        _10吸笔关闭所有吸真空,
        _10取料完成,

        #endregion
        #region"飞拍1"
        _20等待飞拍,
        _20所有吸笔下降,
        _20所有吸笔上升,
        _20中转位取放料,
        _20中转位真空检测,
        _20放料完成,
        _20X轴到飞拍起始位,
        _20X轴到飞拍结束位,
        _20X轴到飞拍结束位校正,
        _20X轴到下相机拍照位,
        _20X到位完成,
        _20Z轴到飞拍位,
        _20Z轴到放料位,
        _20Z轴到抛料位,
        _20Z轴到安全位,
        _20Z轴到求心位,
        _20求心闭合,
        _20Z轴到位,
        _20求心张开,
        _20求心张开检测,
        _20开始飞拍,
        _20飞拍结束,
        _20XY到抛料位,
        _20XY到位完成,
        _20NG吸笔下降,
        _20NG吸笔破真空,
        _20抛料完成,
        
        

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

        #region"镜筒和点胶"
        _40判断动作,
        _40计算取空镜筒XY坐标,
        _40计算放成品镜筒XY坐标,
        _40XY到取空镜筒位,
        _40XY到放成品镜筒位,
        _40XY到点胶取放位,
        _40XY到点胶放料位,
        
        _40XY到待机位,
        _40XY到点胶拍照位,
        _40X轴到镜筒拍照位,
        _40X轴到位完成,
        _40下相机拍照,
        _40下相机拍照完成,
        _40上相机拍照,
        _40上相机拍照完成,
        _40XY轴到点胶位,
        _40点胶气缸下降,
        _40点胶气缸上升,
        _40开始放成品,
        _40开始点胶,
        _40点胶夹子夹紧,
        _40点胶夹子张开,
        _40点胶位真空,
        _40点胶位真空检测,
        _40点胶位真空关闭,
        _40点胶判断,
        _40点胶到位,
        _40开始UV,
        _40UV,
        _40UV完成,
        _40镜筒XY轴到位完成,
        _40XY到放空镜筒位,
        _40XY到取成品镜筒位,
        _40Y到组装位,
        _40Y到组装位完成,
        _40XY到UV位,
        _40点胶XY到位完成,
        _40组装轴XY到位完成,
        
        _40吸笔1下降,
        _40吸笔1上升,
        _40吸笔2下降,
        _40吸笔2上升,
        _40吸笔1真空,
        _40吸笔2真空,
        _40吸笔2点胶位真空,
        _40吸笔1破真空,
        _40吸笔2破真空,
        _40夹子气缸上升,
        _40夹子气缸下降,
        _40成品夹子张开,
        _40成品夹子闭合,
        _40C轴到放料角度,
        _40C轴到位完成,

        _40Z轴到安全位,
        _40Z轴到待机位,
        _40Z轴到镜筒取料位,
        _40Z轴到镜筒放料位,
        _40Z轴到成品放料位,
        _40Z轴到成品取料位,
        _40Z轴到点胶取放位,
        _40Z轴到点胶放料位,
        _40Z轴到点胶位,
        _40Z轴到位完成,
        _40Z轴到点胶拍照位,
        
        _40组装位张开,
        _40组装位夹紧,
        _40组装位真空,
        _40组装位真空关闭,
        _40组装位破真空,
        _40组装位真空检测,
        _40吸笔1真空检知,
        _40吸笔2真空检知,
        _40取料完成,
        _40取成品完成,
        _40放空镜筒完成,
        _40放成品完成,
        #endregion

        #region"像素标定"
        _50计算坐标,
        _50XY轴到标定位,
        _50XY轴到位完成,
        _50开始拍照,
        _50拍照完成,
        _50标定完成,
        #endregion

        #region"点位走位"
        _60轴到指定位,
        _60轴到位完成,
        #endregion
        #region"飞拍测试动作"
        _70X到飞拍起始位,
        _70X到飞拍结束位,
        _70X到定位拍照位,
        _70X到位完成,
        _70相机拍照,
        _70相机拍照完成,
        _70开始飞拍,
        _70飞拍结束,
        _70Z轴到安全位,
        _70Z轴到位完成,
        _70Z轴到原点位,
        _70上相机拍照,
        _70上相机拍照完成,
        _70XY轴到拍照位,
        _70XY轴到位完成,
       


        #endregion
        #region"测高和测压标定流程"
        _80XY到测试位,
        _80XY到位完成,
        _80Z轴到测试起始位,
        _80Z轴到位完成,
        _80Z轴到下压位,
        _80Z轴到安全位,
        _80所有吸笔上升,
        _80吸笔下降,
        _80位置判断,
        _80测高完成,
        _80电压设定,
        _80数据读取,
        _80压力读取,
        _80测压完成,
        #endregion

        #region"取料测试动作"
        _100Z轴到安全位,
        _100Z轴到取料高度,
        _100Z轴到位完成,
        _100吸笔真空,
        _100吸笔检测,
        _100XY到位,
        _100XY到取料位,
        _100气缸下降,
        _100气缸上升,
        _100取料完成,
        _100吸笔破真空,
        _100放料完成,
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
        public static ActionState GetState1 = ActionState.空闲;//取料流程1模块状态
        public static ActionState GetState2 = ActionState.空闲;//取料流程2模块状态
        public static ActionState AssemState1 = ActionState.空闲;//组装1流程状态
        public static ActionState AssemState2 = ActionState.空闲;//组装2流程状态
        public static ActionState GlueAndBarrelState = ActionState.镜筒取料;//点胶和镜筒
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
        /// <summary>
        /// 判断是否有空盘
        /// </summary>
        /// <returns></returns>
        public bool IsBarrelEmpty()
        {
            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
            {
                if (pair.Value.BUse)
                {
                    if (pair.Value.bFinish)
                        return true;
                }
            }
         
           return false;
        }
        /// <summary>
        /// 判断是否有空盘
        /// </summary>
        /// <returns></returns>
        public bool IsBarrelEmpty2()
        {
            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
            {
                if (pair.Value.BUse)
                {
                    if (pair.Value.bFinish)
                        return true;
                }
            }
         
            return false;
        }

        /// <summary>
        /// 工位1所有取料吸笔气缸
        /// </summary>
        public void OptPutSuction1(bool bDown)
        {
            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
            {
                OptSution1 p = pair.Value;
               

                if (p.BUse && (pair.Key < 10))
                {
                    string strDo = "取料气缸下降" + p.SuctionOrder;
                    DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                    mc.setDO(dOut, bDown);

                }
            }
        }
        
        /// <summary>
        /// 工位1取料吸笔是否都已放下
        /// </summary>
        /// <returns></returns>
        public bool OptCheckSuctionDown1()
        {
            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
            {
                OptSution1 p = pair.Value;


                if (p.BUse && (pair.Key < 10))
                {
                    string strD = "取料升降气缸" + p.SuctionOrder.ToString()+ "下检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }

           
            return true;

        }
        /// <summary>
        /// 工位1取料吸笔是否都已往上
        /// </summary>
        /// <returns></returns>
        public bool OptCheckSuctionUp1()
        {
            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
            {
                OptSution1 p = pair.Value;


                if (p.BUse && (pair.Key < 10))
                {
                    string strD = "取料升降气缸" + p.SuctionOrder.ToString() + "上检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }

          
            return true;

        }


        /// <summary>
        /// 工位2所有取料吸笔气缸
        /// </summary>
        public void OptPutSuction2(bool bDown)
        {
            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
            {
                OptSution2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strDo = "取料气缸下降" + p.SuctionOrder;
                    DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                    mc.setDO(dOut, bDown);

                }
            }
        }

        /// <summary>
        /// 取料吸笔放料时，吸笔是否都已放下
        /// </summary>
        /// <returns></returns>
        public bool OptCheckSuctionDown2()
        {
            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
            {
                OptSution2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strD = "取料升降气缸" + p.SuctionOrder.ToString() + "下检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }
            return true;

        }

        /// <summary>
        /// 工位2取料吸笔是否都已往上
        /// </summary>
        /// <returns></returns>
        public bool OptCheckSuctionUp2()
        {
            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
            {
                OptSution2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strD = "取料升降气缸" + p.SuctionOrder.ToString() + "上检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }


            return true;

        }

        /// <summary>
        /// 在放料位将吸笔往上
        /// </summary>
        public void AssemblePutSuction1(bool bDown)
        {
            foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
            {
                AssembleSuction1 p = pair.Value;


                if (p.BUse && (pair.Key < 10))
                {
                    string strDo = "组装气缸下降" + p.SuctionOrder;
                    DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                    mc.setDO(dOut, bDown);

                }
            }
        }

        public void AssemblePutSuction2(bool bDown)
        {
            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
            {
                AssembleSuction2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strDo = "组装气缸下降" + p.SuctionOrder;
                    DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                    mc.setDO(dOut, bDown);

                }
            }
        }

        /// <summary>
        /// 取料吸笔放料时，吸笔是否都已往上
        /// </summary>
        /// <returns></returns>
        public bool AssembleCheckSuctionUp1()
        {

            foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
            {
                AssembleSuction1 p = pair.Value;


                if (p.BUse && (pair.Key < 10))
                {
                    string strD = "组装升降气缸" + p.SuctionOrder.ToString() + "上检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }
            return true;

        }

        public bool AssembleCheckSuctionUp2()
        {

            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
            {
                AssembleSuction2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strD = "组装升降气缸" + p.SuctionOrder.ToString() + "上检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }
            return true;

        }
       
       
         
        /// <summary>
        /// 组装吸笔都放下
        /// </summary>
        public bool AssembleCheckSuctionDown1()
        {
            foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
            {
                AssembleSuction1 p = pair.Value;


                if (p.BUse && (pair.Key < 10))
                {
                    string strD = "组装升降气缸" + p.SuctionOrder.ToString() + "下检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        /// <summary>
        /// 取料吸笔放料时，吸笔是否都已放下
        /// </summary>
        /// <returns></returns>
        public bool AssembleCheckSuctionDown2()
        {

            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
            {
                AssembleSuction2 p = pair.Value;


                if (p.BUse && (pair.Key < 19))
                {
                    string strD = "组装升降气缸" + p.SuctionOrder.ToString() + "下检测";
                    DI dI = (DI)Enum.Parse(typeof(DI), strD);
                    if (!mc.dic_DI[dI])
                    {
                        return false;
                    }

                }
            }
            return true;

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
