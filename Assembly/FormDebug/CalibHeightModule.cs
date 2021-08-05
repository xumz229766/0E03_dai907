using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace Assembly
{
    public class CalibHeightModule:ActionModule
    {
        private string strOut = "CalibHeightModule-Action-";

        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static Point pTest = new Point();//测试点位
        public static double dDistX = 0;//吸笔的距离
        public static double dPosZ = 0;//Z轴初始位
        public static AXIS axisX, axisY, axisZ;
        public static int iCurrentSuction = 1;//当前吸笔序号
        public static int iStartSuction = 1;
        public static int iEndSuction = 1;
        public static double dMoveDistZ = 1;//实际移动距离
        public static double dSetMoveDistZ = 1;//Z轴初始移动距离
        public static int iStation = 1;//测试工位
        public static double dTargetN = 1;//测试返回压力值
        
        public static double dCurrentHeight = 0;
        public CalibHeightModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._80Z轴到安全位);
            lstAction.Add(ActionName._80Z轴到位完成);
            lstAction.Add(ActionName._80所有吸笔上升);
            lstAction.Add(ActionName._80XY到测试位);
            lstAction.Add(ActionName._80XY到位完成);
            lstAction.Add(ActionName._80吸笔下降);
            lstAction.Add(ActionName._80Z轴到测试起始位);
            lstAction.Add(ActionName._80Z轴到位完成);
            lstAction.Add(ActionName._80Z轴到下压位);
            lstAction.Add(ActionName._80Z轴到位完成);
            lstAction.Add(ActionName._80位置判断);
            lstAction.Add(ActionName._80测高完成);
            
        }
        public override void Reset()
        {
           
        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                { 

                    case ActionName._80XY到测试位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            if (!CommonSet.dic_OptSuction1[iCurrentSuction].BUse)
                            {
                                step = lstAction.IndexOf(ActionName._80测高完成);
                                break;
                            }

                        }
                        else
                        {
                            if (!CommonSet.dic_OptSuction2[iCurrentSuction].BUse)
                            {
                                step = lstAction.IndexOf(ActionName._80测高完成);
                                break;
                            }
                        }
                        dMoveDistZ = dSetMoveDistZ;
                        dDestPosX = pTest.X+(iCurrentSuction-iStartSuction)*dDistX;
                        dDestPosY = pTest.Y;
                        mc.AbsMove(axisX, dDestPosX, (int)50);
                        mc.AbsMove(axisY, dDestPosY, (int)50);
                       WriteOutputInfo(strOut + "组装吸笔" );
                        step = step + 1;
                        break;
                    case ActionName._80XY到位完成:
                        if (IsAxisINP(dDestPosX, axisX) && IsAxisINP(dDestPosY, axisY))
                        {
                           WriteOutputInfo(strOut + "XY轴到位");
                            step = step + 1;
                        }
                        break;
                     
                    case ActionName._80Z轴到测试起始位:
                        
                        dDestPosZ = dPosZ;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                       WriteOutputInfo(strOut + "组装Z轴到测试起始位");
                        step = step + 1;
                        break;
                    case ActionName._80所有吸笔上升:
                        if (axisX == AXIS.组装X1轴)
                        {
                            mc.WriteOutDA((float)1.5, (ushort)0);

                        }
                        else
                        {
                            mc.WriteOutDA((float)1.5, (ushort)2);
                        }
                        AssemblePutSuction1(false);
                        AssemblePutSuction2(false);
                        if(sw.WaitSetTime(500))
                        {
                           WriteOutputInfo(strOut + "所有吸笔上升");
                             step = step + 1;
                        }
                        break;
                    case ActionName._80吸笔下降:
                        string strDo = "组装气缸下降" + iCurrentSuction.ToString();
                          DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                          mc.setDO(dOut, true);
                         WriteOutputInfo(strOut + strDo);
                          step = step + 1;
                        break;
                    case ActionName._80Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisZ))
                        {
                           WriteOutputInfo(strOut + "组装Z轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._80Z轴到安全位:
                        dDestPosZ = 0;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                       WriteOutputInfo(strOut + "组装Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._80Z轴到下压位:
                        dDestPosZ = mc.dic_Axis[axisZ].dPos + dMoveDistZ;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                       WriteOutputInfo(strOut + "Z轴到下压位");
                        step = step + 1;
                        break;
                    case ActionName._80位置判断:
                        if (!sw.WaitSetTime(500))
                        {
                            break;
                        }
                        double height,pressure;
                        if (iStation == 1)
                        {
                            height=SerialPortMeasureHeight.dStation1;
                            pressure = SerialPortMeasurePressure.dStation1;
                        }
                        else
                        {
                            height = SerialPortMeasureHeight.dStation2;
                            pressure = SerialPortMeasurePressure.dStation2;
                        }
                        //如果有高度数据
                        if ((pressure > dTargetN))
                        {
                            //如果目标值大于2N，直接返回
                            if (dTargetN > 2)
                            {
                                step = step + 1;
                                break;
                            }

                            double diffH = height - dCurrentHeight;
                           
                           
                            //如果移动的距离小于5um，则代表找到位置
                            if (dMoveDistZ < 0.005)
                            {
                                if (iStation == 1)
                                {
                                    if (axisX == AXIS.组装X1轴)
                                    {
                                        AssembleHeightRelation.lstRelationAssem1Station1[iCurrentSuction - 1].MeasureZ = height;
                                        AssembleHeightRelation.lstRelationAssem1Station1[iCurrentSuction - 1].PosZ = mc.dic_Axis[axisZ].dPos;
                                    }
                                    else
                                    {
                                        AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuction - 10].MeasureZ = height;
                                        AssembleHeightRelation.lstRelationAssem2Station1[iCurrentSuction - 10].PosZ = mc.dic_Axis[axisZ].dPos;
                                    }

                                }
                                else
                                {
                                    if (axisX == AXIS.组装X1轴)
                                    {
                                        AssembleHeightRelation.lstRelationAssem1Station2[iCurrentSuction - 1].MeasureZ = height;
                                        AssembleHeightRelation.lstRelationAssem1Station2[iCurrentSuction - 1].PosZ = mc.dic_Axis[axisZ].dPos;
                                    }
                                    else
                                    {
                                        AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuction - 10].MeasureZ = height;
                                        AssembleHeightRelation.lstRelationAssem2Station2[iCurrentSuction - 10].PosZ = mc.dic_Axis[axisZ].dPos;
                                    }
                                }
                                step = step + 1;
                                break;
                            }
                                dMoveDistZ = dMoveDistZ / 10;
                                dDestPosZ = mc.dic_Axis[axisZ].dPos - dMoveDistZ*9;
                                mc.AbsMove(axisZ, dDestPosZ, (int)50);
                               WriteOutputInfo(strOut + "组装Z轴到测试位" + dDestPosZ.ToString());
                                step = lstAction.LastIndexOf(ActionName._80Z轴到位完成);
                        

                        }
                        else
                        {
                            dCurrentHeight = height;
                            dDestPosZ = mc.dic_Axis[axisZ].dPos + dMoveDistZ;
                            mc.AbsMove(axisZ, dDestPosZ, (int)50);
                           WriteOutputInfo(strOut + "组装Z轴到测试位"+dDestPosZ.ToString());
                            step = lstAction.LastIndexOf(ActionName._80Z轴到位完成);
                        }

                        break;
                    case ActionName._80测高完成:
                        iCurrentSuction++;
                        if (iCurrentSuction > iEndSuction)
                        {
                           WriteOutputInfo(strOut + "测高完成");
                            Run.runMode = RunMode.手动;
                            dDestPosZ = 0;
                            mc.AbsMove(axisZ, dDestPosZ, (int)50);
                            break;
                        }
                        step = step + 1;

                        break;


                }
            }
            catch (Exception ex)
            {
               WriteOutputInfo(strOut + "测高标定模块运行异常"+ToString());
               
            }
        }

        public override void Action2()
        {
           
        }
    }
}
