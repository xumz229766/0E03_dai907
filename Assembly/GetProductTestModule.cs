using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace Assembly
{
    /// <summary>
    /// 测试取料模块
    /// </summary>
    public class GetProductTestModule:ActionModule
    {
        private string strOut = "GetProductTestModule-Action-";
        //private static GetProduct1Module module = null;
        public static int iCurrentSuction = 1;//当前吸笔序号
     
        public static  Point currentPoint =  new Point();
    
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static AXIS axisX, axisY, axisZ;
        public static double dGetTime = 0;//吸真空时间
        public static DO dSuction, dGet;
        public GetProductTestModule()
        {
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100气缸上升);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100XY到取料位);
            lstAction.Add(ActionName._100XY到位);
            lstAction.Add(ActionName._100Z轴到取料高度);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100气缸下降);
            lstAction.Add(ActionName._100吸笔真空);
            lstAction.Add(ActionName._100气缸上升);
            lstAction.Add(ActionName._100Z轴到安全位);
            lstAction.Add(ActionName._100Z轴到位完成);
            lstAction.Add(ActionName._100取料完成);
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
                    case ActionName._100XY到取料位:
                        dDestPosX = currentPoint.X;
                        dDestPosY = currentPoint.Y;
                         mc.AbsMove(axisX, dDestPosX, (int)100);
                        mc.AbsMove(axisY, dDestPosY, (int)100);
                        WriteOutputInfo(strOut + "XY到取料位:(" + dDestPosX.ToString() + "," + dDestPosY.ToString() + ")");
                        step = step + 1;

                        break;

                    case ActionName._100XY到位:
                        if (IsAxisINP(dDestPosX, axisX) && IsAxisINP(dDestPosY, axisY))
                        {
                            WriteOutputInfo(strOut + "XY到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100Z轴到安全位:
                        if (axisZ == AXIS.取料Z1轴)
                        {
                            dDestPosZ = OptSution1.pSafeXYZ.Z;

                        }
                        else if (axisZ == AXIS.取料Z2轴)
                        {
                            dDestPosZ = OptSution2.pSafeXYZ.Z;
                        }
                        else if (axisZ == AXIS.点胶Z轴)
                        {
                            dDestPosZ = BarrelSuction.pSafe.Z;
                        }
                        else
                        {
                            dDestPosZ = 0;
                        }
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位:(" + dDestPosZ.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._100Z轴到取料高度:
                       
                        dDestPosZ = currentPoint.Z;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到取料高度:(" + dDestPosZ.ToString() + ")");
                        step = step + 1;
                        break;
                    case ActionName._100Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisZ, 0.1))
                        {
                            WriteOutputInfo(strOut + "Z轴到位完成");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100气缸上升:
                        mc.setDO(dSuction, false);
                        WriteOutputInfo(strOut + dSuction.ToString()+"上升");
                        step = step + 1;

                        break;

                    case ActionName._100气缸下降:
                        mc.setDO(dSuction, true);
                        if (sw.WaitSetTime(1000))
                        {
                            WriteOutputInfo(strOut + dSuction.ToString() + "下降");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100吸笔真空:

                        mc.setDO(dGet, true);
                        long ltime = (long)(dGetTime * 1000);
                        if (sw.WaitSetTime(ltime))
                        {
                            WriteOutputInfo(strOut + dGet.ToString() + "真空");
                            step = step + 1;
                        }
                        break;
                    case ActionName._100取料完成:
                        Run.runMode = RunMode.手动;
                        step = 0;
                        WriteOutputInfo(strOut + "取料完成");
                        break;
                }
	        }
	        catch (Exception)
	        {
		
		    
	        }


        }

        public override void Action2()
        {
            
        }
    }
}
