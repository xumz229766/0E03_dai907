using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace Assembly
{
    public class SingleAxisTest:ActionModule
    {
        private string strOut = "SingleAxisTest-Action-";
        // private static Assem1Module module = null;
       
        public static int iCurrentNum = 0;
        private double dDestPos = 0;

        public static double stopTime = 0.5;
        public static int NUM = 2;
        public static List<double> lstPos = new List<double>();
        public static AXIS axis;
        public override void Reset()
        {
           
            
        }
        public SingleAxisTest()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._60轴到指定位);
            lstAction.Add(ActionName._60轴到位完成);
        }
        public override void Action2()
        {
           
        }
        public override void Action(ActionName action, ref int step)
        {
            try
            {
              
                switch (action) { 
                    case ActionName._60轴到指定位:

                        long ltime = (long)(stopTime*1000);
                        if (sw.WaitSetTime(ltime))
                        { 
                            dDestPos = lstPos[iCurrentNum];
                            mc.AbsMove(axis, dDestPos, 100);
                            step = step + 1;
                        }

                        break;

                    case ActionName._60轴到位完成:

                        if (IsAxisINP(dDestPos, axis, 0.01))
                        {
                            iCurrentNum++;
                            if (iCurrentNum >= NUM)
                                iCurrentNum = 0;
                            step = step + 1;
                        }

                        break;

                
                }

            }
            catch (Exception)
            {
                
                
            }
        }
    }
}
