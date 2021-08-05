using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Motion;
namespace Motion
{
   public  class Assem1
    {
        public static bool bTestFlag = false;//测试标志
        public static double dAcc = 0.05, dDec = 0.05, dAccZ = 0.05, dDecZ = 0.05;
        public static double dVel = 100, dVelZ = 50;
        public static double dPos1 = 0, dPos2 = 0, dPos3 = 0, dPosZ1 = 0, dPosZ2 = 0;
        public static long lStopTime = 0;
        Stopwatch sw = new Stopwatch();
        Stopwatch swWait = new Stopwatch();
        bool bshow = false;
        public static int step = 0;
        public static AXIS axisTest, axisZ;
        double dDestPos = 0, dDestPosZ = 5;
        public static long ct = 0;
        public static Action<string> ShowInfo;
        MotionCard mc = null;
        public static bool bAssemFlag = false;//工位组装标志
        public static long lLenTime = 0;
       
        public Assem1()
        {
            mc = MotionCard.getMotionCard();
        }

        public void Run()
        {
            
                switch (step)
                {
                    case -3:
                        dDestPosZ = dPosZ1;
                        ((LeiE3032)mc).AbsMove(axisZ, dDestPosZ, 50, dAccZ, dDecZ);
                   
                        ShowInfo(axisZ.ToString() + "到安全位");
                        step = -2;
                        break;
                    case -2:
                        if ((Math.Abs(mc.dic_Axis[axisZ].dPos - dDestPosZ) < 0.003))
                        {
                            ShowInfo(axisTest.ToString() + "Z轴位置：" + mc.dic_Axis[axisZ].dPos.ToString("0.000"));
                            dDestPos = dPos2;
                            ((LeiE3032)mc).AbsMove(axisTest, dDestPos, 100, dAcc, dDec);

                            ShowInfo(axisTest.ToString() + "到第二个点到待机位");
                            bshow = true;
                            swWait.Stop();
                            swWait.Reset();
                            step = -1;
                        }
                        break;
                    case -1:
                        if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.01))
                        {
                         
                            ShowInfo(axisZ.ToString() + "到待机位完成");
                            step = 0;
                        }

                        break;

                    case 0:
                        dDestPosZ = dPosZ1;
                        ((LeiE3032)mc).AbsMove(axisZ, dDestPosZ, dVelZ, dAccZ, dDecZ);
                        ShowInfo("总用时" + sw.ElapsedMilliseconds.ToString() + "ms");
                        ct = sw.ElapsedMilliseconds;
                        ShowInfo(axisZ.ToString() + "到安全位");
                        sw.Restart();
                        step = 1;
                        break;

                    case 1:
                        if ((Math.Abs(mc.dic_Axis[axisZ].dPos - dDestPosZ) < 3))
                        {
                            ShowInfo(axisTest.ToString() + "Z轴位置：" + mc.dic_Axis[axisZ].dPos.ToString("0.000"));
                            dDestPos = dPos1;
                            ((LeiE3032)mc).AbsMove(axisTest, dDestPos, dVel, dAcc, dDec);

                            ShowInfo(axisTest.ToString() + "到第一个点");
                            bAssemFlag = false;
                            bshow = true;
                            swWait.Stop();
                            swWait.Reset();
                            step = 2;
                        }
                        break;
                    case 2:
                        if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.5))
                        {
                            dDestPosZ = dPosZ2;
                            ((LeiE3032)mc).AbsMove(axisZ, dDestPosZ, dVelZ, dAccZ, dDecZ);
                            ShowInfo(axisZ.ToString() + "到下降位");
                            step = 3;

                            bshow = true;
                            swWait.Stop();
                            swWait.Reset();
                        }


                        break;

                    case 3:
                        if (bshow)
                            ShowInfo(axisTest.ToString() + "位置：" + mc.dic_Axis[axisTest].dPos.ToString("0.000") + " Z轴" + mc.dic_Axis[axisZ].dPos.ToString("0.000"));

                        if ((Math.Abs(mc.dic_Axis[axisZ].dPos - dDestPosZ) < 0.2))
                        {
                            if (bshow)
                            {
                                swWait.Restart();
                                bshow = false;
                                ShowInfo(axisZ.ToString() + "到下降位完成，停留" + lStopTime.ToString() + "ms");
                                ShowInfo(axisTest.ToString() + "位置：" + mc.dic_Axis[axisTest].dPos.ToString("0.000"));
                            }
                            if (swWait.ElapsedMilliseconds >= lStopTime)
                            {
                                dDestPosZ = dPosZ1;
                                ((LeiE3032)mc).AbsMove(axisZ, dDestPosZ, dVelZ, dAccZ, dDecZ);

                                ShowInfo(axisZ.ToString() + "到安全位");
                                // sw.Restart();
                                step = 4;
                            }
                        }
                        break;
                    case 4:
                        if ((Math.Abs(mc.dic_Axis[axisZ].dPos - dDestPosZ) < 3))
                        {
                            ShowInfo(axisTest.ToString() + "Z轴位置：" + mc.dic_Axis[axisZ].dPos.ToString("0.000"));
                            dDestPos = dPos2;

                            ((LeiE3032)mc).AbsMove(axisTest, dDestPos, dVel, dAcc, dDec);
                            ShowInfo(axisTest.ToString() + "到第二个点");
                            step = 5;
                            bshow = true;
                            swWait.Stop();
                            swWait.Reset();
                        }
                        break;
                    case 5:
                        if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.003) && mc.dic_Axis[axisTest].INP)
                        {
                            if (bshow)
                            {
                                swWait.Restart();
                                bshow = false;
                                ShowInfo(axisTest.ToString() + "到第二个点,拍照等待" + lStopTime.ToString() + "ms");
                                ShowInfo(axisTest.ToString() + "位置：" + mc.dic_Axis[axisTest].dPos.ToString("0.000"));
                            }
                            if (swWait.ElapsedMilliseconds >= (lStopTime+lLenTime))
                            {
                                //如果工位2不是在组装状态
                                if (!Assem2.bAssemFlag)
                                {
                                    bAssemFlag = true;
                                    bshow = true;
                                    swWait.Stop();
                                    swWait.Reset();
                                    dDestPos = dPos3;

                                    ((LeiE3032)mc).AbsMove(axisTest, dDestPos, dVel, dAcc, dDec);
                                    ShowInfo(axisTest.ToString() + "停留时间:"+swWait.ElapsedMilliseconds.ToString());
                                    ShowInfo(axisTest.ToString() + "到第三个点");
                                    step = 6;
                                }
                            }
                        }
                        break;
                    case 6:
                        if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.5))
                        {

                            dDestPosZ = dPosZ2;
                            ((LeiE3032)mc).AbsMove(axisZ, dDestPosZ, dVelZ, dAccZ, dDecZ);
                            ShowInfo(axisZ.ToString() + "到下降位");
                            bshow = true;
                            swWait.Stop();
                            swWait.Reset();
                            step = 7;
                        }
                        break;
                    case 7:
                        //if(bshow)
                        //ShowInfo(axisTest.ToString() + "位置：" + mc.dic_Axis[axisTest].dPos.ToString("0.000")+" Z轴" + mc.dic_Axis[axisZ].dPos.ToString("0.000"));

                        if ((Math.Abs(mc.dic_Axis[axisZ].dPos - dDestPosZ) < 0.2))
                        {
                            if (bshow)
                            {
                                swWait.Restart();
                                ShowInfo("当前计时器时间" + swWait.ElapsedMilliseconds.ToString("0.000"));
                                bshow = false;
                                ShowInfo(axisZ.ToString() + "到下降位完成，停留" + lStopTime.ToString() + "ms");
                                ShowInfo(axisTest.ToString() + "位置：" + mc.dic_Axis[axisTest].dPos.ToString("0.000"));

                            }
                            if (swWait.ElapsedMilliseconds >= lStopTime)
                            {
                                FrmTestCard.lLenTime = FrmTestCard.swLenTime.ElapsedMilliseconds;
                                FrmTestCard.swLenTime.Restart();
                                ShowInfo("当前计时器时间" + swWait.ElapsedMilliseconds.ToString("0.000"));
                                bshow = true;
                                swWait.Stop();
                                swWait.Reset();
                                step = 0;
                            }
                        }
                        break;

                }
                   
           
        }

    }
}
