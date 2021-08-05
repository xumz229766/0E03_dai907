using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace _0E03
{
    /// <summary>
    /// 测试单轴往返运动
    /// </summary>
    public class TestAxisModule : ActionModule
    {

        private string strOut = ",单轴测试模块,Action,";
        //private static GetProduct1Module module = null;
        public static int iCurrentSuction = 1;//当前吸笔序号
        public static List<double> lstPos = new List<double>();//测试点位
 
        private int iCurrentIndex = 1;
        private double dDestPos = 0;
        public static AXIS axis;
        public static double dVel = 100;
        public static double dAcc = 0.1;
        public static long stopTime = 1000;
        public TestAxisModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._40开始测试);
            lstAction.Add(ActionName._40轴到点位);
            lstAction.Add(ActionName._40到位完成);
            lstAction.Add(ActionName._40测试完成);
        }
        public override void Action(ActionName action, ref int step)
        {
            try
            {
                switch (action) {
                    case ActionName._40开始测试:
                        iCurrentIndex = 0;
                        step = step + 1;
                        WriteOutputInfo(strOut + "开始测试,当前轴为："+axis.ToString()+" 测试速度："+dVel.ToString()+" 加减速："+dAcc.ToString());
                        break;
                    case ActionName._40轴到点位:
                        dDestPos = lstPos[iCurrentIndex];
                        mc.AbsMove(axis, dDestPos, dVel, dAcc, dAcc);
                        WriteOutputInfo(strOut + axis.ToString()+"到位置"+dDestPos.ToString("0.000"));
                        step = step + 1;
                        break;
                    case ActionName._40到位完成:
                        if (IsAxisINP(dDestPos, axis, 0.2))
                        {
                            if (sw.WaitSetTime(stopTime)) {
                                step = step + 1;
                                WriteOutputInfo(strOut + axis.ToString() + "到位完成" );

                            }
                        }
                        break;
                    case ActionName._40测试完成:
                        iCurrentIndex++;
                        if (iCurrentIndex < lstPos.Count)
                            step = 1;
                        else
                            step = 0;
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

        public override void Reset()
        {
            
        }
    }
}
