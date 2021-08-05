using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using HalconDotNet;
using System.Diagnostics;
namespace Assembly
{
    public class BarrelAndGlueModule:ActionModule
    {
        private string strOut = "BarrelAndGlueModule-Action-";
        private static GetProduct1Module module = null;
    
       
        private Point currentPoint = null;
       
       
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        private double dDestPosC = 0;
        private int iSuction1 = 1;//取空镜筒吸笔
        private int iSuction2 = 2;//放成品镜筒吸笔
        public static bool bIsProductEmpty = false;//盘子中有无料，true为无料
        public static int iCurrentLen = 0;//当前成品序号
        public static int iCurrentBarrel = 0;//当前吸笔取的镜筒序号
        public static int iCurrentIndex = 0;//当前工位号
        public static int iResetStep = 0;
        private string strD = "";
        private DO dOut;
        private DI dIn;
        public Stopwatch swShotTime = new Stopwatch();
        public bool bStopGlue = false;
        public int iGlueCircle = 0;//点胶圈数
        public bool bMoveZ = false;
        public override void Action2()
        {
            //int iTargetStep = lstAction.IndexOf(ActionName._40开始点胶);
            //if ((IStep >= iTargetStep) && (IStep <= iTargetStep + 1))
            //{
            //    int i=0;
            //    Action(ActionName._40点胶判断,ref i);

            //}

        }
        public BarrelAndGlueModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._40判断动作);
            //取镜筒-------------------------------------------
            lstAction.Add(ActionName._40计算取空镜筒XY坐标);
            lstAction.Add(ActionName._40XY到取空镜筒位);
            lstAction.Add(ActionName._40镜筒XY轴到位完成);
            lstAction.Add(ActionName._40Z轴到镜筒取料位);
            lstAction.Add(ActionName._40吸笔1下降);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40吸笔1真空);
            lstAction.Add(ActionName._40吸笔1上升);
            lstAction.Add(ActionName._40吸笔1真空检知);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40X轴到镜筒拍照位);
            lstAction.Add(ActionName._40X轴到位完成);
            lstAction.Add(ActionName._40下相机拍照);
            lstAction.Add(ActionName._40下相机拍照完成);
            lstAction.Add(ActionName._40取料完成);

            //放镜筒-------------------------------------------
            lstAction.Add(ActionName._40XY到放空镜筒位);
            lstAction.Add(ActionName._40组装位张开);
            lstAction.Add(ActionName._40组装轴XY到位完成);
            lstAction.Add(ActionName._40Z轴到镜筒放料位);
            lstAction.Add(ActionName._40吸笔1下降);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40组装位真空);
            lstAction.Add(ActionName._40吸笔1破真空);
            //lstAction.Add(ActionName._40组装位夹紧);
            lstAction.Add(ActionName._40吸笔1上升);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40组装位真空检测);
            lstAction.Add(ActionName._40放空镜筒完成);

            //取成品------------------------------------------------
            #region"装夹子之前的取成品动作"
            //lstAction.Add(ActionName._40XY到取成品镜筒位);
            //lstAction.Add(ActionName._40组装轴XY到位完成);
            //lstAction.Add(ActionName._40Z轴到成品取料位);
            //lstAction.Add(ActionName._40组装位真空关闭);
            //lstAction.Add(ActionName._40吸笔2下降);
            //lstAction.Add(ActionName._40Z轴到位完成);
            //lstAction.Add(ActionName._40组装位张开);
            //lstAction.Add(ActionName._40吸笔2真空);
            //lstAction.Add(ActionName._40吸笔2上升);
            //lstAction.Add(ActionName._40Z轴到安全位);
            //lstAction.Add(ActionName._40Z轴到位完成);
            //lstAction.Add(ActionName._40吸笔2真空检知);
            //lstAction.Add(ActionName._40取成品完成);
            #endregion
            lstAction.Add(ActionName._40XY到取成品镜筒位);
            lstAction.Add(ActionName._40组装位张开);
            lstAction.Add(ActionName._40组装轴XY到位完成);
            lstAction.Add(ActionName._40成品夹子张开);
            lstAction.Add(ActionName._40Z轴到成品取料位);
            lstAction.Add(ActionName._40组装位真空关闭);
            //lstAction.Add(ActionName._40吸笔2下降);
            lstAction.Add(ActionName._40夹子气缸下降);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40成品夹子闭合);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40夹子气缸上升);
            lstAction.Add(ActionName._40Z轴到位完成);
           // lstAction.Add(ActionName._40吸笔2真空检知);
            lstAction.Add(ActionName._40取成品完成);

            //点胶和放成品--------------------------------------------------
            lstAction.Add(ActionName._40开始放成品);//判断是否要点胶
            #region"加夹子之前的动作"
            //lstAction.Add(ActionName._40Z轴到安全位);
            //lstAction.Add(ActionName._40Z轴到位完成);
            //lstAction.Add(ActionName._40XY到点胶取放位);
            //lstAction.Add(ActionName._40点胶XY到位完成);
            //lstAction.Add(ActionName._40点胶夹子张开);
            //lstAction.Add(ActionName._40Z轴到点胶取放位);
            //lstAction.Add(ActionName._40吸笔2下降);
            //lstAction.Add(ActionName._40Z轴到位完成);
            //lstAction.Add(ActionName._40点胶位真空);
            //lstAction.Add(ActionName._40吸笔2破真空);
            //lstAction.Add(ActionName._40Z轴到安全位);
            //lstAction.Add(ActionName._40吸笔2上升);
            //lstAction.Add(ActionName._40Z轴到位完成);
            //lstAction.Add(ActionName._40点胶夹子夹紧);
            //lstAction.Add(ActionName._40点胶位真空检测);
            //lstAction.Add(ActionName._40点胶位真空关闭);
            #endregion
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY到点胶放料位);
            lstAction.Add(ActionName._40C轴到放料角度);
            lstAction.Add(ActionName._40C轴到位完成);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40点胶夹子张开);
            lstAction.Add(ActionName._40Z轴到点胶放料位);
            lstAction.Add(ActionName._40夹子气缸下降);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40点胶位真空);
            lstAction.Add(ActionName._40成品夹子张开);
           // lstAction.Add(ActionName._40吸笔2破真空);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40夹子气缸上升);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40点胶夹子夹紧);
            lstAction.Add(ActionName._40点胶位真空检测);
            lstAction.Add(ActionName._40点胶位真空关闭);

            //点胶
            lstAction.Add(ActionName._40XY到点胶拍照位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40Z轴到点胶拍照位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40上相机拍照);
            lstAction.Add(ActionName._40上相机拍照完成);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY轴到点胶位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40Z轴到点胶位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40点胶气缸下降);
            lstAction.Add(ActionName._40开始点胶);
            lstAction.Add(ActionName._40点胶判断);
            lstAction.Add(ActionName._40点胶到位);
            lstAction.Add(ActionName._40点胶气缸上升);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40XY到UV位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40开始UV);
            lstAction.Add(ActionName._40UV);
            lstAction.Add(ActionName._40UV完成);

            lstAction.Add(ActionName._40XY到点胶取放位);
            lstAction.Add(ActionName._40点胶XY到位完成);
            lstAction.Add(ActionName._40Z轴到点胶取放位);
            lstAction.Add(ActionName._40点胶夹子张开);
            lstAction.Add(ActionName._40吸笔2下降);
            lstAction.Add(ActionName._40吸笔2点胶位真空);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40吸笔2上升);
            lstAction.Add(ActionName._40Z轴到位完成);

            ////放成品盘
            lstAction.Add(ActionName._40计算放成品镜筒XY坐标);
            lstAction.Add(ActionName._40XY到放成品镜筒位);
            lstAction.Add(ActionName._40镜筒XY轴到位完成);
            lstAction.Add(ActionName._40Z轴到成品放料位);
            lstAction.Add(ActionName._40吸笔2下降);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40吸笔2破真空);
            lstAction.Add(ActionName._40吸笔2上升);
            lstAction.Add(ActionName._40Z轴到安全位);
            lstAction.Add(ActionName._40Z轴到位完成);
            lstAction.Add(ActionName._40放成品完成);


        }
        public override void Reset()
        {
            switch (iResetStep)
            {
                case 0:
                    if (bResetFinish)
                        break;
                    IStep = 0;
                    iCurrentLen = 0;
                    iCurrentBarrel = 0;
                    iCurrentIndex = 0;
                    mc.setDO(DO.点胶夹紧, false);
                    mc.setDO(DO.点胶气缸下降, false);
                    mc.setDO(DO.点胶清洁夹紧, false);
                  
                    mc.setDO(DO.点胶位破真空, false);
                    mc.setDO(DO.点胶位吸真空, false);
                    mc.setDO(DO.点胶镜筒吸笔下降, false);
                    mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);
                    mc.setDO(DO.点胶镜筒吸笔真空, false);
                    mc.setDO(DO.点胶成品吸笔真空_夹子, false);
                    mc.setDO(DO.点胶成品吸笔破真空, false);
                    mc.setDO(DO.点胶成品镜头气缸下降, false);
                    mc.setDO(DO.点胶成品吸笔真空_夹子, false);
                    mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);
                    ParamListerner.station1.initData();
                    ParamListerner.station2.initData();
                    GlueAndBarrelState = ActionState.镜筒取料;
                    iResetStep = iResetStep + 1;

                    break;
                case 1:

                    if (sw.WaitSetTime(2000))
                    {
                        if (!mc.dic_DI[DI.点胶成品气缸2上升])
                        {
                            Alarminfo.AddAlarm(Alarm.成品吸笔2上升超时);
                            WriteOutputInfo(strOut + "复位时,成品吸笔2上升超时");
                            Run.bReset = false;
                            break;
                        }
                        if (!mc.dic_DI[DI.点胶升降气缸上检测])
                        {
                            Alarminfo.AddAlarm(Alarm.点胶气缸上升超时);
                            WriteOutputInfo(strOut + "复位时,点胶气缸上升超时");
                            Run.bReset = false;
                            break;
                        }
                        if (!mc.dic_DI[DI.镜筒取料气缸1上升])
                        {
                            Alarminfo.AddAlarm(Alarm.镜筒吸笔1上升超时);
                            WriteOutputInfo(strOut + "复位时,镜筒吸笔1上升超时");
                            Run.bReset = false;
                            break;
                        }
                        if (mc.dic_Axis[AXIS.点胶X轴].INP && mc.dic_Axis[AXIS.镜筒Y轴].INP && mc.dic_Axis[AXIS.点胶Z轴].INP)
                            Action(ActionName._40Z轴到待机位, ref iResetStep);
                    }
                    break;
                case 2:
                    sw.ResetSetWatch();
                    Action(ActionName._40Z轴到位完成, ref iResetStep);

                    break;

                case 3:
                    Action(ActionName._40XY到待机位, ref iResetStep);

                    break;
                case 4:
                    Action(ActionName._40镜筒XY轴到位完成, ref iResetStep);

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

        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                {
                    case ActionName._40判断动作:
                        if (GlueAndBarrelState == ActionState.镜筒取料)
                        {
                            //如果镜筒盘不为空则去取料,否则去取成品
                            if (!(CommonSet.dic_BarrelSuction[iSuction1].bFinish ||CommonSet.bForceFinish))
                            {
                                CommonSet.swCircleD.Restart();
                                step = lstAction.IndexOf(ActionName._40计算取空镜筒XY坐标);
                            }
                            else
                            {
                                GlueAndBarrelState = ActionState.取成品;
                                step = 0;
                            }
                        }
                        else if (GlueAndBarrelState == ActionState.镜筒放料)
                        {
                            if ((iCurrentBarrel != 0)&&mc.dic_DO[DO.点胶镜筒吸笔真空])
                            {
                                step = lstAction.IndexOf(ActionName._40XY到放空镜筒位);
                            }
                        }
                        else if (GlueAndBarrelState == ActionState.取成品)
                        {
                            //判断工位1是否有料
                            if (ParamListerner.station1.iCurrentBarrelNum > 0)
                            {
                                //判断工位1是否组装完成
                                if (ParamListerner.station1.bFinish1 && ParamListerner.station1.bFinish2)
                                {
                                    iCurrentIndex = 1;
                                   
                                    step = lstAction.IndexOf(ActionName._40XY到取成品镜筒位);
                                    break;
                                }
                            }
                            //判断工位1是否有料
                            if (ParamListerner.station2.iCurrentBarrelNum > 0)
                            {
                                //判断工位1是否组装完成
                                if (ParamListerner.station2.bFinish1 && ParamListerner.station2.bFinish2)
                                {
                                    iCurrentIndex = 2;

                                    step = lstAction.IndexOf(ActionName._40XY到取成品镜筒位);
                                    break;
                                }
                            }

                        }
                        else if (GlueAndBarrelState == ActionState.点胶和放成品)
                        {
                            if (mc.dic_DO[DO.点胶成品吸笔真空_夹子] && (iCurrentLen != 0))
                            {
                                step = lstAction.IndexOf(ActionName._40开始放成品);
                            }
                        
                        }


                        break;

                    case ActionName._40取料完成:
                        //如果工位1使用
                        if (BarrelSuction.bUseAssem1)
                        {
                            //判断工位1是否有料
                            if (ParamListerner.station1.iCurrentBarrelNum > 0)
                            {
                                //判断工位1是否组装完成
                                if (ParamListerner.station1.bFinish1 && ParamListerner.station1.bFinish2)
                                {
                                    iCurrentIndex = 1;
                                    GlueAndBarrelState = ActionState.取成品;
                                    step = 0;
                                    break;
                                }
                            }
                            else
                            {
                                //如果工位1没料，则先放料
                                iCurrentIndex = 1;
                                GlueAndBarrelState = ActionState.镜筒放料;
                                step = 0;
                                break;
                            }
                           
                        }
                        //如果工位2使用
                        if (BarrelSuction.bUseAssem2)
                        {
                            //判断工位1是否有料
                            if (ParamListerner.station2.iCurrentBarrelNum > 0)
                            {
                                //判断工位1是否组装完成
                                if (ParamListerner.station2.bFinish1 && ParamListerner.station2.bFinish2)
                                {
                                    iCurrentIndex = 2;
                                    GlueAndBarrelState = ActionState.取成品;
                                    step = 0;
                                    break;
                                }
                            }
                            else
                            {
                                //如果工位2没料，则先放料
                                iCurrentIndex = 2;
                                GlueAndBarrelState = ActionState.镜筒放料;
                                step = 0;
                                break;
                            }

                        }
                        
                        break;
                    case ActionName._40放空镜筒完成:
                        if (iCurrentIndex == 1)
                        {
                           

                            ParamListerner.station1.initData();
                            ParamListerner.station1.iCurrentBarrelNum = iCurrentBarrel;
                            ParamListerner.station1.dCurrentBarrelAngle = BarrelSuction.imgResultDown.dAngle;
                          
                            iCurrentBarrel = 0;
                        }
                        else if(iCurrentIndex == 2)
                        {
                           
                            ParamListerner.station2.initData();
                            ParamListerner.station2.iCurrentBarrelNum = iCurrentBarrel;
                            ParamListerner.station2.dCurrentBarrelAngle = BarrelSuction.imgResultDown.dAngle;
                            iCurrentBarrel = 0;
                        }
                        //当成品吸笔有吸真空，且当前成品吸笔上的镜头序号不为0时，去点胶
                        if (mc.dic_DO[DO.点胶成品吸笔真空_夹子] && (iCurrentLen != 0))
                        {
                            GlueAndBarrelState = ActionState.点胶和放成品;
                            step = 0;
                            WriteOutputInfo(strOut + "放空镜筒完成");
                        }
                        else
                        {
                            //如果成品吸笔上没有料，则代表是刚开始，继续取料
                            GlueAndBarrelState = ActionState.镜筒取料;
                            step = 0;
                            WriteOutputInfo(strOut + "放空镜筒完成");
                        }
                        

                        break;
                    case ActionName._40取成品完成:
                        if (iCurrentIndex == 1)
                        {
                            iCurrentLen = ParamListerner.station1.iCurrentBarrelNum;
                            
                            ParamListerner.station1.WriteHeightResult(CommonSet.strReportPath +"\\Height\\");
                            ParamListerner.station1.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                            ParamListerner.station1.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                            ParamListerner.station1.iCurrentBarrelNum = 0;
                        }
                        else
                        {
                            iCurrentLen = ParamListerner.station2.iCurrentBarrelNum;
                           
                            ParamListerner.station2.WriteHeightResult(CommonSet.strReportPath + "\\Height\\");
                            ParamListerner.station2.WritePressureResult(CommonSet.strReportPath + "\\Pressure\\");
                            ParamListerner.station2.WriteZPosResult(CommonSet.strReportPath + "\\Z轴高度\\");
                            ParamListerner.station2.iCurrentBarrelNum = 0;
                        }
                        //如果有空镜筒，则放镜筒
                        if (mc.dic_DO[DO.点胶镜筒吸笔真空] && (iCurrentBarrel != 0))
                        {
                            GlueAndBarrelState = ActionState.镜筒放料;
                            step = 0;
                            WriteOutputInfo(strOut + "取成品完成");
                        }
                        else
                        {
                            //如果是最后一个，将Y轴移动到待机位
                            if (iCurrentIndex == 1)
                            {
                                double posY = AssembleSuction1.pSafeXYZ.Y;
                                if (mc.dic_Axis[AXIS.组装Y1轴].INP)
                                {
                                    mc.AbsMove(AXIS.组装Y1轴, posY, (int)CommonSet.dVelRunY);
                                    WriteOutputInfo(strOut + "组装完成后，组装Y1轴到待机位");
                                }
                            }
                            else if (iCurrentIndex == 2)
                            {
                                double posY = AssembleSuction2.pSafeXYZ.Y;
                                if (mc.dic_Axis[AXIS.组装Y2轴].INP)
                                {
                                    mc.AbsMove(AXIS.组装Y2轴, posY, (int)CommonSet.dVelRunY);
                                    WriteOutputInfo(strOut + "组装完成后，组装Y2轴到待机位");
                                }
                            }

                            GlueAndBarrelState = ActionState.点胶和放成品;
                            step = 0;
                            WriteOutputInfo(strOut + "取成品完成");

                        }

                        break;
                    case ActionName._40放成品完成:
                        //如果有空镜筒，则放镜筒
                        if (!(CommonSet.dic_BarrelSuction[iSuction1].bFinish||CommonSet.bForceFinish))
                        {
                            GlueAndBarrelState = ActionState.镜筒取料;
                            step = 0;
                            WriteOutputInfo(strOut + "放成品完成");
                        }
                        else
                        {
                            iCurrentIndex = 0;
                            if ((ParamListerner.station1.iCurrentBarrelNum != 0) || (ParamListerner.station2.iCurrentBarrelNum != 0))
                            {
                                GlueAndBarrelState = ActionState.取成品;
                               
                                step = 0;
                                WriteOutputInfo(strOut + "放成品完成");
                            }
                            else
                            { 
                                //盘完成复位
                                Run.runMode = RunMode.暂停;
                                Run.bFull = true;
                                Run.ResetStatus();

                                Run.bReset = true;
                            }

                        }
                        break;
                    case ActionName._40开始放成品:
                        mc.SetEncoderValue(AXIS.点胶C轴, mc.dic_Axis[AXIS.点胶C轴].dPos % 360);
                        WriteOutputInfo(strOut + "开始放成品");
                        step = step + 1;
                        //以下为加夹子之前的动作
                        //if (CommonSet.bUseGlue)
                        //{

                        //    step = step + 1;
                        //    break;
                        //}
                        //else
                        //{
                        //    step = lstAction.IndexOf(ActionName._40计算放成品镜筒XY坐标);
                        //    break;
                        //}

                        break;
                    case ActionName._40计算取空镜筒XY坐标:
                         currentPoint = CommonSet.dic_BarrelSuction[iSuction1].getCoordinateByIndex();
                         WriteOutputInfo(strOut + "当前取料位为:" + CommonSet.dic_BarrelSuction[iSuction1].CurrentPos+"坐标(:"+currentPoint.X.ToString()+","+currentPoint.Y.ToString()+")");
                         currentPoint.Z = BarrelSuction.DGetPosZ;
                         //iCurrentBarrel = CommonSet.dic_BarrelSuction[iSuction1].CurrentPos;
                          
                         step = step + 1;
                        break;
                    case ActionName._40XY到待机位:
                        dDestPosX = BarrelSuction.pSafe.X;
                        dDestPosY = BarrelSuction.pSafe.Y;
                        mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)100);
                        mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "XY到待机位");
                        step = step + 1;
                        break;
                    case ActionName._40镜筒XY轴到位完成:
                        if (IsAxisINP(dDestPosX, AXIS.点胶X轴, 0.02)&&IsAxisINP(dDestPosY, AXIS.镜筒Y轴, 0.02))
                        {
                            WriteOutputInfo(strOut + "镜筒XY轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40X轴到镜筒拍照位:
                        BarrelSuction.InitImageDown(BarrelSuction.strPicCameraDownName);
                           CommonSet.camDownD1.SetExposure(BarrelSuction.dExposureTimeDown);
                           CommonSet.camDownD1.SetGain(BarrelSuction.dGainDown);
                        dDestPosX = BarrelSuction.dCameraPos;
                        mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                        WriteOutputInfo(strOut + "X轴到镜筒拍照位");
                        step = step + 1;
                        break;
                    case ActionName._40X轴到位完成:
                        if (IsAxisINP(dDestPosX, AXIS.点胶X轴))
                        {
                            WriteOutputInfo(strOut + "X轴到位完成");
                            step = step + 1;
                        }
                        break;

                    case ActionName._40Z轴到位完成:
                        if (IsAxisINP(dDestPosZ,AXIS.点胶Z轴))
                        {
                            WriteOutputInfo(strOut + "Z轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40Z轴到镜筒取料位:
                        dDestPosZ = (currentPoint.Z);
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到镜筒取料位");
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到安全位:
                        dDestPosZ = (BarrelSuction.pSafe.Z);
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "点胶Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到待机位:
                        dDestPosZ = (BarrelSuction.pSafe.Z);
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "点胶Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._40下相机拍照:
                        mc.setDO(DO.点胶下相机, true);
                        if (sw.WaitSetTime(10))
                        {
                            mc.setDO(DO.点胶下相机, false);
                            WriteOutputInfo(strOut + "下相机拍照");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40下相机拍照完成:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            WriteOutputInfo(strOut + "点胶下相机拍照");
                            break;
                        }

                        if (BarrelSuction.imgResultDown.bStatus || CommonSet.bUnableCameraLen)
                        {
                            if (BarrelSuction.imgResultDown.bImageResult || CommonSet.bUnableCameraLen)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult =1;
                                     break;
                                }
                                step = step + 1;
                                WriteOutputInfo(strOut + "点胶下相机拍照");
                            }
                            else
                            {
                                if (Run.runMode == RunMode.运行)
                                {
                                    // Alarminfo.AddAlarm(Alarm.工位1镜筒角度NG);
                                   /// alarmBlock = GetAlarmBlock(sw);
                                    int iAlarmStep = step;
                                    BarrelSuction.InitImageDown(BarrelSuction.strPicCameraDownName);
                                    CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 1, "点胶下相机拍照检测NG");
                                    Run.runMode = RunMode.暂停;

                                }
                                else if (Run.runMode == RunMode.暂停)
                                {
                                    BarrelSuction.InitImageDown(BarrelSuction.strPicCameraDownName);
                                    Run.iDebugResult = 2;
                                    // break;
                                }
                                Alarminfo.AddAlarm(Alarm.点胶下相机拍照检测NG);
                                WriteOutputInfo(strOut + "点胶下相机拍照检测NG");
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                BarrelSuction.InitImageDown(BarrelSuction.strPicCameraDownName);
                                Alarminfo.AddAlarm(Alarm.点胶下相机拍照超时);
                                WriteOutputInfo(strOut + "点胶下相机拍照超时");
                                step = step - 1;
                            }
                        }
                        break;

                    case ActionName._40上相机拍照:
                        mc.setDO(DO.点胶上相机, true);
                        if (sw.WaitSetTime(5))
                        {
                            mc.setDO(DO.点胶上相机, false);
                            WriteOutputInfo(strOut + "点胶上相机拍照");
                            step = step + 1;
                        }

                        break;
                    case ActionName._40上相机拍照完成:
                        if (BarrelSuction.imgResultUp.bStatus)
                        {
                            if (BarrelSuction.imgResultUp.bImageResult || CommonSet.bTest||CommonSet.bForceOk)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 1;
                                    break;
                                }
                                if (CommonSet.bForceOk)
                                {
                                    CommonSet.bForceOk = false;
                                    step = lstAction.LastIndexOf(ActionName._40XY到点胶取放位);
                                    break;
                                }
                                step = step + 1;
                                WriteOutputInfo(strOut + "点胶上相机拍照");
                            }
                            else
                            {
                                if (Run.runMode == RunMode.点胶测试)
                                {
                                    Run.runMode = RunMode.手动;
                                    break;
                                }
                                if (Run.runMode == RunMode.运行)
                                {
                                    // Alarminfo.AddAlarm(Alarm.工位1镜筒角度NG);
                                    /// alarmBlock = GetAlarmBlock(sw);
                                    int iAlarmStep = step;
                                    CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 1, "点胶上相机拍照检测NG");
                                    Run.runMode = RunMode.暂停;
                                }
                                else if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 2;
                                    // break;
                                }
                                Alarminfo.AddAlarm(Alarm.点胶上相机拍照检测NG);
                                WriteOutputInfo(strOut + "点胶上相机拍照检测NG");
                            }
                        }
                        else
                        {

                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.点胶上相机拍照超时);
                                WriteOutputInfo(strOut + "点胶上相机拍照超时");
                                if (Run.runMode == RunMode.点胶测试)
                                {
                                    Run.runMode = RunMode.手动;
                                    break;
                                }
                                step = step - 1;
                            }
                        }
                        break;
                     
                    case ActionName._40吸笔1下降:
                        mc.setDO(DO.点胶镜筒吸笔下降, true);
                        //long t2 = (long)(CommonSet.dic_BarrelSuction[iSuction1].dSuctionUpTime * 1000);
                        if (mc.dic_DI[DI.镜筒取料气缸1下降] || CommonSet.bTest)
                        {
                            if (sw.WaitSetTime(200))
                            {
                                WriteOutputInfo(strOut + "吸笔1下降");
                                step = step + 1;
                            }
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.镜筒吸笔1下降超时);
                                WriteOutputInfo(strOut + "镜筒吸笔1下降超时");
                            }
                        }
                        break;
                    case ActionName._40吸笔1上升:
                        mc.setDO(DO.点胶镜筒吸笔下降, false);

                        if (mc.dic_DI[DI.镜筒取料气缸1上升] || CommonSet.bTest)
                        {
                            if (CommonSet.bTest)
                            {
                                if (!sw.WaitSetTime(500))
                                {
                                    break;
                                }
                            }
                            WriteOutputInfo(strOut + "吸笔1上升");
                            step = step + 1;
                          
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.镜筒吸笔1上升超时);
                                WriteOutputInfo(strOut + "镜筒吸笔1上升超时");
                            }
                        }
                        
                        break;
                    case ActionName._40吸笔1真空:
                        mc.setDO(DO.点胶镜筒吸笔真空, true);
                       // mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);

                        long tSuction1 = (long)(BarrelSuction.DGetTime * 1000);
                        if (sw.WaitSetTime(tSuction1))
                        {
                            WriteOutputInfo(strOut + "吸笔1真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40吸笔1破真空:
                        mc.setDO(DO.点胶镜筒吸笔真空, false);
                       // mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, true);
                        long tSuctionOut = (long)(BarrelSuction.DPutTime * 1000);
                        if (sw.WaitSetTime(tSuctionOut))
                        {
                            //mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);
                            WriteOutputInfo(strOut + "吸笔1破真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40吸笔2上升:
                        mc.setDO(DO.点胶成品镜头气缸下降, false);

                        if (mc.dic_DI[DI.点胶成品气缸2上升] || CommonSet.bTest)
                        {
                            if (CommonSet.bTest)
                            {
                                if (!sw.WaitSetTime(500))
                                {
                                    break;
                                }
                            }
                            WriteOutputInfo(strOut + "吸笔2上升");
                            step = step + 1;
                          
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.成品吸笔2上升超时);
                                WriteOutputInfo(strOut + "成品吸笔2上升超时");
                            }
                        }
                        break;
                    case ActionName._40吸笔2下降:
                        mc.setDO(DO.点胶成品镜头气缸下降, true);
                        //long t2 = (long)(CommonSet.dic_BarrelSuction[iSuction1].dSuctionUpTime * 1000);
                        if (mc.dic_DI[DI.点胶成品气缸2下降] || CommonSet.bTest)
                        {
                            if (CommonSet.bTest)
                            {
                                if (!sw.WaitSetTime(500))
                                {
                                    break;
                                }
                            }
                            WriteOutputInfo(strOut + "吸笔2下降");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.成品吸笔2下降超时);
                                WriteOutputInfo(strOut + "成品吸笔2下降超时");
                            }
                        }
                        break;
                    case ActionName._40吸笔2点胶位真空:
                        mc.setDO(DO.点胶成品吸笔真空_夹子, true);
                        mc.setDO(DO.点胶成品吸笔破真空, false);
                        mc.setDO(DO.点胶位破真空, false);
                        mc.setDO(DO.点胶位吸真空, false);
                        long lGetLenTime =(long)(BarrelSuction.dGetLenTime * 1000);
                        if (sw.WaitSetTime(lGetLenTime))
                        {
                            WriteOutputInfo(strOut + "吸笔2点胶位真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40吸笔2真空:
                        mc.setDO(DO.点胶成品吸笔真空_夹子, true);
                        mc.setDO(DO.点胶成品吸笔破真空, false);
                       
                        if (iCurrentIndex == 1)
                        {
                            mc.setDO(DO.组装位1真空破, true);
                            mc.setDO(DO.组装位1真空吸, false);
                        }
                        else if (iCurrentIndex == 2)
                        {
                            mc.setDO(DO.组装位2真空破, true);
                            mc.setDO(DO.组装位2真空吸, false);
                        }
                        //long tSuction2 = (long)(BarrelSuction.DGetTime * 1000);
                        //if (sw.WaitSetTime(tSuction2))
                        //{
                        //    if (iCurrentIndex == 1)
                        //    {
                        //        mc.setDO(DO.组装位1真空破, false);
                        //    }
                        //    else if (iCurrentIndex == 2)
                        //    {
                        //        mc.setDO(DO.组装位2真空破, false);
                        //    }
                        //    WriteOutputInfo(strOut + "吸笔2真空");
                        //    step = step + 1;

                        //}
                        long lGetLenTime1 =(long)(BarrelSuction.dGetLenTime * 1000);
                        if (sw.WaitSetTime(lGetLenTime1))
                        {
                            if (iCurrentIndex == 1)
                            {
                                mc.setDO(DO.组装位1真空破, false);
                              
                            }
                            else if (iCurrentIndex == 2)
                            {
                                mc.setDO(DO.组装位2真空破, false);
                               
                            }
                            WriteOutputInfo(strOut + "吸笔2真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40吸笔2破真空:
                        mc.setDO(DO.点胶成品吸笔真空_夹子, false);
                        mc.setDO(DO.点胶成品吸笔破真空, true);
                        long tSuctionOut2 = (long)(BarrelSuction.DPutTime * 1000);
                        if (sw.WaitSetTime(tSuctionOut2))
                        {
                            mc.setDO(DO.点胶成品吸笔破真空, false);
                            WriteOutputInfo(strOut + "吸笔2破真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40夹子气缸上升:
                        mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);
                        if (sw.WaitSetTime(1000))
                        {
                            WriteOutputInfo(strOut + "夹子气缸上升");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40夹子气缸下降:
                        mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, true);
                        if (sw.WaitSetTime(1000))
                        {
                            WriteOutputInfo(strOut + "夹子气缸下降");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40成品夹子张开:
                        mc.setDO(DO.点胶成品吸笔真空_夹子, false);
                       long t1 =(long)(BarrelSuction.dGetLenTime * 1000);
                        if (sw.WaitSetTime(t1))
                        {
                            WriteOutputInfo(strOut + "成品夹子张开");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40成品夹子闭合:
                        mc.setDO(DO.点胶成品吸笔真空_夹子, true);
                        long t2 = (long)(BarrelSuction.dGetLenTime * 1000);
                        if (sw.WaitSetTime(t2))
                        {
                            WriteOutputInfo(strOut + "成品夹子闭合");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40吸笔1真空检知:
                        if (mc.dic_DI[DI.点胶取料真空1检测] || CommonSet.bTest || CommonSet.bForceOk)
                        {
                           
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 1;
                                break;
                            }
                            CommonSet.bForceOk = false;
                            iCurrentBarrel = CommonSet.dic_BarrelSuction[iSuction1].CurrentPos;
                            CommonSet.dic_BarrelSuction[iSuction1].CurrentPos++;
                            WriteOutputInfo(strOut + "镜筒吸笔1真空检测有料");
                            step = step + 1;
                        }
                        else
                        {
                            if (Run.runMode == RunMode.运行)
                            {
                                //Alarminfo.AddAlarm(Alarm.镜筒吸笔1真空检测无料);
                                //alarmBlock = GetAlarmBlock(sw);
                                int iAlarmStep = step;
                                CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 8, "镜筒吸笔1真空检测无料", true);
                                Run.runMode = RunMode.暂停;
                            }
                            else if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 2;
                            }
                            Alarminfo.AddAlarm(Alarm.镜筒吸笔1真空检测无料);
                            WriteOutputInfo(strOut + "镜筒吸笔1真空检测无料");
                        }

                        break;


                    case ActionName._40吸笔2真空检知:
                        if (mc.dic_DI[DI.点胶取料真空2检测] || CommonSet.bTest || CommonSet.bForceOk||true)
                        {
                           
                            if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 1;
                                break;
                            }
                            CommonSet.bForceOk = false;
                            WriteOutputInfo(strOut + "吸笔2真空检测有料");
                            step = step + 1;
                        }
                        else
                        {
                            if (Run.runMode == RunMode.运行)
                            {
                                //Alarminfo.AddAlarm(Alarm.镜筒吸笔2真空检测无料);
                               // alarmBlock = GetAlarmBlock(sw);
                                int iAlarmStep = step;
                                CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 9, "镜筒吸笔2真空检测无料");
                                Run.runMode = RunMode.暂停;
                            }
                            else if (Run.runMode == RunMode.暂停)
                            {
                                Run.iDebugResult = 2;
                                //break;
                            }
                            Alarminfo.AddAlarm(Alarm.成品吸笔2真空检测无料);
                            WriteOutputInfo(strOut + "镜筒吸笔2真空检测无料");
                        }

                        break;
                    case ActionName._40组装位夹紧:

                        if (iCurrentIndex == 1)
                        {
                            mc.setDO(DO.组装位1夹紧, false);
                            if (!mc.dic_DI[DI.组装位1夹子张开检测] || CommonSet.bTest)
                            {
                                WriteOutputInfo(strOut + "组装位夹紧" + iCurrentIndex.ToString());
                                step = step + 1;
                            }
                            else
                            {
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    Alarminfo.AddAlarm(Alarm.组装工位1夹子夹紧超时);
                                    WriteOutputInfo(strOut + "组装夹子1闭合超时");

                                }
                            }
                        }
                        else if (iCurrentIndex == 2)
                        {
                            mc.setDO(DO.组装位2夹紧, false);
                            if (!mc.dic_DI[DI.组装位2夹子张开检测] || CommonSet.bTest)
                            {
                                WriteOutputInfo(strOut + "组装位夹紧" + iCurrentIndex.ToString());
                                step = step + 1;
                            }
                            else
                            {
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    Alarminfo.AddAlarm(Alarm.组装工位2夹子夹紧超时);
                                    WriteOutputInfo(strOut + "组装夹子2闭合超时");

                                }
                            }
                        }
                        break;
                    case ActionName._40组装位张开:

                        if (iCurrentIndex == 1)
                        {
                            mc.setDO(DO.组装位1夹紧, false);
                            if (mc.dic_DI[DI.组装位1夹子张开检测] || CommonSet.bTest)
                            {
                                if (sw.WaitSetTime(800))
                                {
                                    WriteOutputInfo(strOut + "组装位张开" + iCurrentIndex.ToString());
                                    step = step + 1;
                                }
                            }
                            else
                            {
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    Alarminfo.AddAlarm(Alarm.组装工位1夹子张开超时);
                                    WriteOutputInfo(strOut + "组装夹子1张开超时");

                                }
                            }
                        }
                        else if (iCurrentIndex == 2)
                        {
                            mc.setDO(DO.组装位2夹紧, false);
                            if (mc.dic_DI[DI.组装位2夹子张开检测] || CommonSet.bTest)
                            {
                                if (sw.WaitSetTime(800))
                                {
                                    WriteOutputInfo(strOut + "组装位张开" + iCurrentIndex.ToString());
                                    step = step + 1;
                                }
                            }
                            else
                            {
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    Alarminfo.AddAlarm(Alarm.组装工位2夹子张开超时);
                                    WriteOutputInfo(strOut + "组装夹子2张开超时");
                                }
                            }

                        }
                        break;
                  
                    case ActionName._40组装位真空:
                        if (iCurrentIndex == 1)
                        {
                            mc.setDO(DO.组装位1真空吸, true);
                            mc.setDO(DO.组装位1真空破, false);
                            WriteOutputInfo(strOut + "组装位1真空");
                            step = step + 1;
                         
                        }
                        else if (iCurrentIndex == 2)
                        {
                            mc.setDO(DO.组装位2真空吸, true);
                            mc.setDO(DO.组装位2真空破, false);
                            WriteOutputInfo(strOut + "组装位2真空");
                            step = step + 1;
                        
                        }

                        break;
                    case ActionName._40组装位真空检测:
                        if (iCurrentIndex == 1)
                        {
                            if (mc.dic_DI[DI.组装位1真空检测] || CommonSet.bTest || CommonSet.bForceOk)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 1;
                                  
                                    break;
                                }
                                CommonSet.bForceOk = false;
                                WriteOutputInfo(strOut + "组装位1真空");
                                step = step + 1;
                            }
                            else
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.组装位1真空检测异常);
                                    WriteOutputInfo(strOut + "组装位1真空检测异常");
                                     break;
                                }
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    if (Run.runMode == RunMode.运行)
                                    {
                                        // Alarminfo.AddAlarm(Alarm.工位1镜筒角度NG);
                                        /// alarmBlock = GetAlarmBlock(sw);
                                        int iAlarmStep = step;
                                        CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 0, "组装位1真空检测异常");
                                        Run.runMode = RunMode.暂停;
                                    }
                                    
                                    Alarminfo.AddAlarm(Alarm.组装位1真空检测异常);
                                    WriteOutputInfo(strOut + "组装位1真空检测异常");

                                }
                            }
                        }
                        else if (iCurrentIndex == 2)
                        {
                            if (mc.dic_DI[DI.组装位2真空检测] || CommonSet.bTest || CommonSet.bForceOk)
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 1;

                                    break;
                                }
                                CommonSet.bForceOk = false;
                                WriteOutputInfo(strOut + "组装位2真空");
                                step = step + 1;
                            }
                            else
                            {
                                if (Run.runMode == RunMode.暂停)
                                {
                                    Run.iDebugResult = 2;
                                    Alarminfo.AddAlarm(Alarm.组装位2真空检测异常);
                                    WriteOutputInfo(strOut + "组装位2真空检测异常");
                                    break;
                                }
                                if (sw.AlarmWaitTime(iActionTime))
                                {
                                    if (Run.runMode == RunMode.运行)
                                    {
                                        // Alarminfo.AddAlarm(Alarm.工位1镜筒角度NG);
                                        /// alarmBlock = GetAlarmBlock(sw);
                                        int iAlarmStep = step;
                                        CommonSet.AlarmProcessShow(AlarmBlock.镜筒和点胶, iAlarmStep, 0, "组装位2真空检测异常");
                                        Run.runMode = RunMode.暂停;
                                    }
                                    Alarminfo.AddAlarm(Alarm.组装位2真空检测异常);
                                    WriteOutputInfo(strOut + "组装位2真空检测异常");
                                }
                            }
                        }
                        break;
                    case ActionName._40组装位真空关闭:
                        if (iCurrentIndex == 1)
                        {
                            mc.setDO(DO.组装位1真空吸, false);
                            mc.setDO(DO.组装位1真空破, false);
                            WriteOutputInfo(strOut + "组装位1真空关闭");
                            step = step + 1;

                        }
                        else if (iCurrentIndex == 2)
                        {
                            mc.setDO(DO.组装位2真空吸, false);
                            mc.setDO(DO.组装位2真空破, false);
                            WriteOutputInfo(strOut + "组装位2真空关闭");
                            step = step + 1;
                        }

                        break;

                    case ActionName._40点胶夹子张开:

                        mc.setDO(DO.点胶夹紧, false);
                        //if (mc.dic_DI[DI.点胶夹子张开检测] || CommonSet.bTest)
                        //{
                        if (sw.WaitSetTime(500))
                        {
                            WriteOutputInfo(strOut + "点胶夹子张开");
                            step = step + 1;
                        }
                        //}
                        //else
                        //{
                        //    if (sw.AlarmWaitTime(iActionTime))
                        //    {
                        //        Alarminfo.AddAlarm(Alarm.点胶位夹子张开超时);
                        //        WriteOutputInfo(strOut + "点胶位夹子张开超时");
                        //    }
                        //}
                        break;
                    case ActionName._40点胶夹子夹紧:

                        mc.setDO(DO.点胶夹紧, true);
                        //if (mc.dic_DI[DI.点胶夹子夹紧检测] || CommonSet.bTest)
                        //{
                        if (sw.WaitSetTime(500))
                        {
                            WriteOutputInfo(strOut + "点胶夹子夹紧");
                            step = step + 1;
                        }
                        //}
                        //else
                        //{
                        //    if (sw.AlarmWaitTime(iActionTime))
                        //    {
                        //        Alarminfo.AddAlarm(Alarm.点胶位夹子夹紧超时);
                        //        WriteOutputInfo(strOut + "点胶位夹子夹紧超时");
                        //    }
                        //}
                        break;
                    case ActionName._40点胶气缸上升:
                        mc.setDO(DO.点胶气缸下降, false);
                        if (mc.dic_DI[DI.点胶升降气缸上检测] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "点胶气缸上升");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.点胶气缸上升超时);
                                WriteOutputInfo(strOut + "点胶气缸上升超时");
                            }
                        }

                        break;
                    case ActionName._40点胶气缸下降:
                        mc.setDO(DO.点胶气缸下降, true);
                        if (mc.dic_DI[DI.点胶升降气缸下检测] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "点胶气缸下降");
                            step = step + 1;
                        }
                        else
                        {
                            if (sw.AlarmWaitTime(iActionTime))
                            {
                                Alarminfo.AddAlarm(Alarm.点胶气缸下降超时);
                                WriteOutputInfo(strOut + "点胶气缸下降超时");
                            }
                        }

                        break;
                    case ActionName._40点胶位真空:
                         mc.setDO(DO.点胶位吸真空, true);
                         mc.setDO(DO.点胶位破真空, false);
                         WriteOutputInfo(strOut + "点胶位真空");
                         step = step + 1;
                        break;
                    case ActionName._40点胶位真空关闭:
                      
                          mc.setDO(DO.点胶位吸真空, false);
                         mc.setDO(DO.点胶位破真空, false);
                         WriteOutputInfo(strOut + "点胶位真空关闭");
                        //加夹子之后的动作
                         if (CommonSet.bUseGlue)
                         {

                             step = step + 1;
                             break;
                         }
                         else
                         {
                             step = lstAction.IndexOf(ActionName._40XY到点胶取放位);
                             break;
                         }
                        
                        break;
                    case ActionName._40点胶位真空检测:
                        if (mc.dic_DI[DI.点胶位真空检测] || CommonSet.bTest)
                        {
                            WriteOutputInfo(strOut + "点胶位真空检测");
                            step = step + 1;
                        }
                        else
                        {
                            Alarminfo.AddAlarm(Alarm.点胶位真空检测异常);
                            WriteOutputInfo(strOut + "点胶位真空检测异常");
                        }

                        break;
                    case ActionName._40XY到点胶取放位:
                            dDestPosX = BarrelSuction.pPutLen.X;
                            dDestPosY = BarrelSuction.pPutLen.Y;
                            mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                            mc.AbsMove(AXIS.点胶Y轴, dDestPosY, (int)CommonSet.dVelGlueY);
                            WriteOutputInfo(strOut + "XY到点胶取放位");
                            step = step + 1;
                        break;
                    case ActionName._40XY到点胶放料位:
                      
                        dDestPosX = BarrelSuction.pPutProductGlue.X;
                        dDestPosY = BarrelSuction.pPutProductGlue.Y;
                        mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                        mc.AbsMove(AXIS.点胶Y轴, dDestPosY, (int)CommonSet.dVelGlueY);
                        WriteOutputInfo(strOut + "XY到点胶放料位");
                        step = step + 1;
                        break;
                   
                  
                    case ActionName._40XY到放空镜筒位:

                        if (iCurrentIndex == 1)
                        {
                            dDestPosX = BarrelSuction.pPutBarrel1.X;
                            dDestPosY = BarrelSuction.pPutBarrel1.Y;
                            mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                        }
                        else if (iCurrentIndex == 2)
                        {
                            dDestPosX = BarrelSuction.pPutBarrel2.X;
                            dDestPosY = BarrelSuction.pPutBarrel2.Y;
                            mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                        }
                        WriteOutputInfo(strOut + "XY到放空镜筒组装位" + iCurrentIndex.ToString());
                        step = step + 1;
                        break;
                    case ActionName._40组装轴XY到位完成:
                        if (iCurrentIndex == 1)
                        {
                            if (IsAxisINP(dDestPosX, AXIS.点胶X轴, 0.02) && IsAxisINP(dDestPosY, AXIS.组装Y1轴, 0.02))
                            {
                                WriteOutputInfo(strOut + "组装轴XY到位完成" + iCurrentIndex.ToString());
                                step = step + 1;

                            }
                        }
                        else if (iCurrentIndex == 2)
                        {
                            if (IsAxisINP(dDestPosX, AXIS.点胶X轴, 0.02) && IsAxisINP(dDestPosY, AXIS.组装Y2轴, 0.02))
                            {
                                WriteOutputInfo(strOut + "组装轴XY到位完成" + iCurrentIndex.ToString());
                                step = step + 1;
                            }
                        }

                        break;
                    case ActionName._40XY到UV位:
                           if(!IsAxisINP(AXIS.点胶X轴)||!IsAxisINP(AXIS.点胶Y轴))
                           {
                               break;
                           }
                            dDestPosX = BarrelSuction.pUV.X;
                            dDestPosY = BarrelSuction.pUV.Y;
                            mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                            mc.AbsMove(AXIS.点胶Y轴, dDestPosY, (int)CommonSet.dVelGlueY);
                            WriteOutputInfo(strOut + "XY到UV位");
                            step = step + 1;
                        break;
                    case ActionName._40点胶XY到位完成:
                       
                            if (IsAxisINP(dDestPosX, AXIS.点胶X轴, 0.02) && IsAxisINP(dDestPosY, AXIS.点胶Y轴, 0.02))
                            {
                                WriteOutputInfo(strOut + "点胶XY到位完成");
                                step = step + 1;
                            }
                       

                        break;
                    case ActionName._40计算放成品镜筒XY坐标:
                         CommonSet.dic_BarrelSuction[iSuction2].CurrentPos = iCurrentLen;
                         currentPoint = CommonSet.dic_BarrelSuction[iSuction2].getCoordinateByIndex(iCurrentLen);
                         currentPoint.Z = BarrelSuction.DPutPosZ;
                         WriteOutputInfo(strOut + "当前放料位为:" + CommonSet.dic_BarrelSuction[iSuction2].CurrentPos + "坐标(:" + currentPoint.X.ToString() + "," + currentPoint.Y.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._40XY到取成品镜筒位:
                      
                        if (iCurrentIndex == 1)
                        {
                            if (mc.dic_Axis[AXIS.点胶X轴].INP && mc.dic_Axis[AXIS.组装Y1轴].INP)
                            {
                                dDestPosX = (BarrelSuction.pGetLen1.X);
                                dDestPosY = (BarrelSuction.pGetLen1.Y);
                                mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                                mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "XY到取成品镜筒位" + iCurrentIndex.ToString());
                                step = step + 1;
                            }
                        }
                        else if (iCurrentIndex == 2)
                        {
                            if (mc.dic_Axis[AXIS.点胶X轴].INP && mc.dic_Axis[AXIS.组装Y2轴].INP)
                            {
                                dDestPosX = (BarrelSuction.pGetLen2.X);
                                dDestPosY = (BarrelSuction.pGetLen2.Y);
                                mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                                mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                                WriteOutputInfo(strOut + "XY到取成品镜筒位" + iCurrentIndex.ToString());
                                step = step + 1;
                            }
                        }
                      
                        break;
                    case ActionName._40XY到取空镜筒位:
                        dDestPosX = (currentPoint.X);
                        dDestPosY = (currentPoint.Y);
                        mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                        mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, (int)CommonSet.dVelLenY);
                        WriteOutputInfo(strOut + "XY到取空镜筒位");
                        step = step + 1;
                        break;
                    case ActionName._40XY到点胶拍照位:
                        if (Run.runMode == RunMode.点胶测试)
                        {
                            mc.setDO(DO.点胶夹紧, true);
                           
                        }
                          CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                          CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                        BarrelSuction.InitImageUp(BarrelSuction.strPicCameraUpName);
                         dDestPosX = (BarrelSuction.pCamGlue.X );
                         dDestPosY = (BarrelSuction.pCamGlue.Y );
                         mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                         mc.AbsMove(AXIS.点胶Y轴, dDestPosY, (int)CommonSet.dVelGlueY);

                         WriteOutputInfo(strOut + "XY到点胶拍照位" + iCurrentIndex.ToString());
                        step = step + 1;
                        break;
                    case ActionName._40XY到放成品镜筒位:
                        dDestPosX = currentPoint.X;
                        dDestPosY = currentPoint.Y;
                        mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                        mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, (int)CommonSet.dVelLenY);
                        WriteOutputInfo(strOut + "XY到放成品镜筒位");
                        step = step + 1;
                        break;
                 
                    case ActionName._40Y到组装位:
                        if (iCurrentIndex == 1)
                        {
                            dDestPosY = (AssembleSuction1.pCamBarrelPos1.Y);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装Y1轴到组装位");
                            step = step + 1;
                        }
                        else if (iCurrentIndex == 2)
                        {
                            dDestPosY = (AssembleSuction1.pCamBarrelPos2.Y);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "组装Y2轴到组装位");
                            step = step + 1;
                        }
                        break;
                   


                    case ActionName._40Z轴到成品取料位:
                      
                        if (iCurrentIndex == 1)
                        {
                            dDestPosZ = (BarrelSuction.pGetLen1.Z );
                        }
                        else if (iCurrentIndex == 2)
                        {
                            dDestPosZ = (BarrelSuction.pGetLen2.Z);
                        }
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到成品取料位" + iCurrentIndex.ToString());
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到成品放料位:
                        if (CommonSet.bTest)
                            dDestPosZ = currentPoint.Z - 10;
                        else
                            dDestPosZ = (currentPoint.Z);
                       
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到成品放料位");
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到点胶位:
                        if ((mc.dic_Axis[AXIS.组装Y1轴].dPos > 420) || (mc.dic_Axis[AXIS.组装Y2轴].dPos > 420))
                        {
                            break;
                        }
                        if (CommonSet.bTest)
                             dDestPosZ = BarrelSuction.pGlue.Z - 10;
                        else
                            dDestPosZ = BarrelSuction.pGlue.Z;
                       
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到点胶位");
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到点胶取放位:
                        if (CommonSet.bTest)
                            dDestPosZ = BarrelSuction.pPutLen.Z;
                        else
                            dDestPosZ = (BarrelSuction.pPutLen.Z );
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到点胶取放位");
                        step = step + 1;
                        break;
                       
                    case ActionName._40Z轴到点胶放料位:
                        if (CommonSet.bTest)
                            dDestPosZ = BarrelSuction.pPutProductGlue.Z;
                        else
                            dDestPosZ = (BarrelSuction.pPutProductGlue.Z);
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到点胶放料位");
                        step = step + 1;
                        break;
                    case ActionName._40Z轴到点胶拍照位:
                        
                        dDestPosZ = (BarrelSuction.pCamGlue.Z );
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                        WriteOutputInfo(strOut + "Z轴到点胶拍照位");
                        step = step + 1;
                        break;
                   
                    case ActionName._40Z轴到镜筒放料位:
                       
                        if (iCurrentIndex == 1)
                        {
                            dDestPosZ = BarrelSuction.pPutBarrel1.Z;
                           
                        }
                        else if (iCurrentIndex == 2)
                        {
                            dDestPosZ = BarrelSuction.pPutBarrel2.Z;
                        }
                        mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, (int)CommonSet.dVelGlueZ);
                          WriteOutputInfo(strOut + "Z轴到镜筒放料位" + iCurrentIndex.ToString());
                        step = step + 1;
                        break;
                    case ActionName._40XY轴到点胶位:
                        try
                        {
                            iGlueCircle = 0;
                            HTuple hom = CommonSet.HomatPixelToAxis(BarrelSuction.lstCalibCameraUpRC, BarrelSuction.lstCalibCameraUpXY);
                            //实际镜筒轴坐标
                            HTuple outBarrelX, outBarrelY;
                            HOperatorSet.AffineTransPoint2d(hom, BarrelSuction.imgResultUp.CenterRow, BarrelSuction.imgResultUp.CenterColumn, out outBarrelX, out outBarrelY);
                            //调试镜筒对应的轴坐标
                            HTuple outLenX, outLenY;
                            HOperatorSet.AffineTransPoint2d(hom, BarrelSuction.pCalibLenPos.X, BarrelSuction.pCalibLenPos.Y, out outLenX, out outLenY);
                            dDestPosX = BarrelSuction.pGlue.X - (outBarrelX - outLenX);
                            dDestPosY = BarrelSuction.pGlue.Y - (outBarrelY - outLenY);
                            mc.AbsMove(AXIS.点胶X轴, dDestPosX, (int)CommonSet.dVelGlueX);
                            mc.AbsMove(AXIS.点胶Y轴, dDestPosY, (int)CommonSet.dVelGlueY);
                            WriteOutputInfo(strOut + "XY轴到点胶位（"+dDestPosX.ToString("0.000")+","+dDestPosY.ToString("0.000")+")");
                           // dDestPosC = mc.dic_Axis[AXIS.点胶C轴].dPos;
                            step = step + 1;
                            // p.Z = DGetPosZ;
                        }
                        catch (Exception ex)
                        {

                        }

                      
                        break;
                    case ActionName._40开始点胶:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;
                        }
                        //点胶XY轴运动完成
                        //if (!IsAxisINP(AXIS.点胶X轴) || !IsAxisINP(AXIS.点胶Y轴) || !IsAxisINP(dDestPosC, AXIS.点胶C轴, 1))
                        //{
                        //    break;
                        //}
                        double dRotateX = 0, dRotateY = 0;
                        try
                        {
                            HTuple hom = CommonSet.HomatPixelToAxis(BarrelSuction.lstCalibCameraUpRC, BarrelSuction.lstCalibCameraUpXY);
                         
                            HTuple outBarrelX, outBarrelY;
                            HOperatorSet.AffineTransPoint2d(hom, BarrelSuction.pCalibLenPos.X, BarrelSuction.pCalibLenPos.Y, out outBarrelX, out outBarrelY);
                            //旋转中心对应的轴坐标
                            HTuple outRotateCenterX, outRotateCenterY;
                            HOperatorSet.AffineTransPoint2d(hom, BarrelSuction.dRoateCenterRow, BarrelSuction.dRoateCenterColumn, out outRotateCenterX, out outRotateCenterY);
                            dRotateX = BarrelSuction.pGlue.X - (outRotateCenterX - outBarrelX);
                            dRotateY = BarrelSuction.pGlue.Y - (outRotateCenterY - outBarrelY);
                            WriteOutputInfo(strOut + "点胶旋转中心位（" + dRotateX.ToString("0.000") + "," + dRotateY.ToString("0.000") + ")");
                          
                            // p.Z = DGetPosZ;
                        }
                        catch (Exception ex)
                        {

                        }
                        double dRotateAngle = BarrelSuction.dShotAngle;
                        //if (BarrelSuction.dShotAngle - 360 * iGlueCircle >= 360)
                        //{
                        //    dRotateAngle = 360;
                        //    iGlueCircle = iGlueCircle + 1;
                        //     mc.setDO(DO.出胶, true);
                        //}
                        //else
                        //{
                        //    dRotateAngle = BarrelSuction.dShotAngle - 360 * iGlueCircle;
                        //    if ((dRotateAngle < BarrelSuction.dStopAngle))
                        //    {
                        //        mc.setDO(DO.出胶, false);
                        //    }
                        //    iGlueCircle = iGlueCircle + 1;
                        //}
                        if (BarrelSuction.bUseRotate)
                            dDestPosC = mc.dic_Axis[AXIS.点胶C轴].dPos - dRotateAngle;
                        else
                            dDestPosC = mc.dic_Axis[AXIS.点胶C轴].dPos;
                        mc.setDO(DO.出胶, true);
                        //mc.setDOSingle(DO.出墨, 1);  

                        double ang = Math.Abs(dRotateAngle) * 3.14159 / 180;
                        HTuple hmat;
                        HTuple endX, endY;
                        HOperatorSet.HomMat2dIdentity(out hmat);
                        //if (angDiff > 0)
                        //{
                        //    HOperatorSet.HomMat2dRotate(hmat, -ang, currentPoint.X, currentPoint.Y, out hmat);

                        //}
                        //else {
                        HOperatorSet.HomMat2dRotate(hmat, ang, dRotateX, dRotateY, out hmat);
                        //}

                        HOperatorSet.AffineTransPoint2d(hmat, mc.dic_Axis[AXIS.点胶X轴].dPos, mc.dic_Axis[AXIS.点胶Y轴].dPos, out endX, out endY);
                        HTuple r;
                        HOperatorSet.DistancePp(dRotateX, dRotateY, mc.dic_Axis[AXIS.点胶X轴].dPos, mc.dic_Axis[AXIS.点胶Y轴].dPos, out r);
                        WriteOutputInfo(strOut + "镜筒到旋转中心位偏差（" + (mc.dic_Axis[AXIS.点胶X轴].dPos - dRotateX).ToString("0.000") + "," + (mc.dic_Axis[AXIS.点胶Y轴].dPos-dRotateY).ToString("0.000") + ")");
                        WriteOutputInfo(strOut + "镜筒到旋转中心位半径R（" + r.D.ToString("0.000") + ")");
                        WriteOutputInfo(strOut + "旋转结束位（" + endX.D.ToString("0.000,")+endY.D.ToString("0.000") + ")");
                        mc.Interpolation_2D_Arc_Move2(AXIS.点胶X轴, AXIS.点胶Y轴, dRotateX, dRotateY, endX, endY, CommonSet.dVelGlueC, dRotateAngle, BarrelSuction.bUseRotate, r.D, BarrelSuction.iDirect);
                      
                        WriteOutputInfo(strOut + "开始点胶");
                        //if (iGlueCircle == 1)
                        //{
                            bStopGlue = false;
                            bMoveZ = false;
                            swShotTime.Restart();
                        //}
                        step = step + 1;
                        break;
                      
                    case ActionName._40点胶判断:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;
                        }
                        double dZAngle = BarrelSuction.dAngleZStop;
                        long shotTime = (long)((BarrelSuction.dShotAngle - dZAngle) * 1000 / CommonSet.dVelGlueC);
                        long shotTime2 = (long)((BarrelSuction.dShotAngle - BarrelSuction.dStopAngle) * 1000 / CommonSet.dVelGlueC);
                        long totalTime = (long)((BarrelSuction.dShotAngle) * 1000 / CommonSet.dVelGlueC);

                        
                        long Elapse = swShotTime.ElapsedMilliseconds;
                        ////延迟100ms后再判断

                        if ((Elapse < 100)&&(swShotTime.IsRunning))
                        {
                            break;
                        }
                        //if (dZAngle < 1)
                        //{
                        //    bMoveZ = true;
                        //}
                        //if (BarrelSuction.dStopAngle < 1)
                        //{
                        //    bStopGlue = true;
                        //}
                        ////判断Z轴上升
                        if (((Elapse > shotTime)) && !bMoveZ)
                        {
                            bMoveZ = true;
                            double len = BarrelSuction.dDstZStop;
                            dDestPosZ = mc.dic_Axis[AXIS.点胶Z轴].dPos - len;
                            double vel = len * 1000 / (totalTime - Elapse);

                            WriteOutputInfo(strOut + "涂胶上升判定时间---" + swShotTime.ElapsedMilliseconds.ToString() + " 剩余" + (totalTime - Elapse).ToString());
                            if (vel > 0)
                            {
                                mc.AbsMove(AXIS.点胶Z轴, dDestPosZ, vel);
                                WriteOutputInfo(strOut + "点胶上升速度"+vel.ToString());
                                //mc.setDO(DO.出胶, false);
                                //swShotTime.Stop();
                                //step = step + 1;
                                //break;
                            }

                           

                        }
                        //判断
                        if ((Elapse > shotTime2) && !bStopGlue)
                        {
                            bStopGlue = true;
                            mc.setDO(DO.出胶, false);

                            WriteOutputInfo(strOut + "涂胶时间:" + totalTime.ToString() + "断胶时间----" + swShotTime.ElapsedMilliseconds.ToString());

                        }
                        if (bStopGlue && bMoveZ)
                        {
                            swShotTime.Stop();
                            step = step + 1;
                        }
                        

                        break;
                    case ActionName._40点胶到位:
                        if (CommonSet.bTest)
                        {
                            step = step + 1;
                            break;
                        }
                        //if (BarrelSuction.dShotAngle > 360)
                        //{
                        //   if(BarrelSuction.dShotAngle-iGlueCircle*360 >0){
                            
                        //        step = step - 1;
                        //        break;
                        //    }
                        //}
                       
                        if (IsAxisINP(AXIS.点胶X轴) && IsAxisINP(AXIS.点胶Y轴) && IsAxisINP(AXIS.点胶Z轴) && IsAxisINP(dDestPosC, AXIS.点胶C轴, 1))
                        {
                            
                            mc.setDO(DO.出胶, false);
                            WriteOutputInfo(strOut + "点胶到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._40开始UV:
                        //if (CommonSet.bTest)
                        //{
                        //    step = step + 1;
                        //    break;
                        //}
                        //mc.setDO(DO.点胶UV, false);
                        //if (sw.WaitSetTime(50))
                        //{
                            double velC = (double)(360 / BarrelSuction.dUVTime);
                            double pos = mc.dic_Axis[AXIS.点胶C轴].dPos;
                            dDestPosC = pos + 360;
                            mc.AbsMove(AXIS.点胶C轴, dDestPosC, (int)(velC));


                            mc.setDO(DO.点胶UV, true);
                            step = step + 1;
                            WriteOutputInfo(strOut + "开始UV");
                        //}
                        

                        break;

                    case ActionName._40UV完成:
                        //if (CommonSet.bTest)
                        //{
                        //    step = step + 1;
                        //    break;
                        //}
                        mc.StopAxis(AXIS.点胶C轴);
                        //if (sw.WaitSetTime(50))
                        //{

                        mc.setDO(DO.点胶UV, false);
                            
                           // mc.SetEncoderValue(AXIS.点胶C轴, mc.dic_Axis[AXIS.点胶C轴].dPos/360);
                            step = step + 1;
                            WriteOutputInfo(strOut + "UV完成");
                        //}
                        if (Run.runMode == RunMode.点胶测试)
                        {
                            mc.setDO(DO.点胶夹紧, false);
                            step = 0;
                            Run.runMode = RunMode.手动;
                        }
                        break;
                    case ActionName._40UV:
                        //if (CommonSet.bTest)
                        //{
                        //    step = step + 1;
                        //    break;
                        //}
                         //mc.setDO(DO.点胶UV, false);
                         long lUVTime =(long)(BarrelSuction.dUVTime*1000);
                         
                         if (sw.WaitSetTime(lUVTime))
                         {
                             //mc.setDO(DO.点胶UV, true);
                             step = step + 1;
                             WriteOutputInfo(strOut + "UV");
                         }
                       
                        break;
                    case ActionName._40C轴到放料角度:
                            dDestPosC = BarrelSuction.pPutProductGlue.Theta;
                            mc.AbsMove(AXIS.点胶C轴, dDestPosC, CommonSet.dVelC);
                            step = step + 1;
                            WriteOutputInfo(strOut + "点胶C轴到放料角度:"+dDestPosC.ToString());

                        break;
                   
                    case ActionName._40C轴到位完成:
                        if (IsAxisINP(dDestPosC, AXIS.点胶C轴, 2))
                        {
                            WriteOutputInfo(strOut + "点胶C轴到位完成");
                            step = step + 1;
                        }
                          
                        break;
                  
                   
                }

            }
            catch (Exception ex)
            {
                 WriteOutputInfo(strOut + "点胶异常"+ex.ToString());
               
            }
        }
    }
}
