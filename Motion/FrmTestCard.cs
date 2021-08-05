using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Motion
{
    public partial class FrmTestCard : Form
    {
        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern void timeEndPeriod(int t);
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        private static SynchronizationContext synContext = null;
        public MotionCard mc = null;
        public static string strStartPath = Application.StartupPath;
        Thread th_UpdateUI = null;
        Thread th_Run = null;
        public static Stopwatch swLenTime = new Stopwatch();
        public static long lLenTime = 0;
       
        public FrmTestCard()
        {
           
            //mc = MotionCard.getMotionCard();
            InitializeComponent();
            synContext = SynchronizationContext.Current;
        }
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            try
            {
                Process process = Process.GetCurrentProcess();
                EmptyWorkingSet(process.Handle);
            }
            catch(Exception ex)
            {

                throw;
            }
        }

        private void FrmTestCard_Load(object sender, EventArgs e)
        {

           string[] values = Enum.GetNames(typeof(AXIS));
            cmdAxis.Items.Clear();
            cmdAxis.Items.AddRange(values);
            cmdAxis.SelectedIndex = 0;

            timeBeginPeriod(1);
            mc = MotionCard.getMotionCard();

            mc.ShowInfo += ShowInfo;
            mc.initCard(Application.StartupPath + "\\");
            mc.SeverOnAll();
            Assem1.ShowInfo += ShowInfo;
            Assem2.ShowInfo += ShowInfo;

            th_UpdateUI = new Thread(UpdateUI);
            th_UpdateUI.Start();

            th_UpdateUI = new Thread(Run);
            th_UpdateUI.Start();
        }

        public void UpdateUI()
        {
            while (true)
            {
                try
                {
                    synContext.Send(UpdateC, null);
                    ClearMemory();   

                }
                catch (Exception ex)
                {

                    ShowInfo("更新界面异常："+ex.ToString());
                }





                Thread.Sleep(500);

            }

        }
        public void UpdateC(object o)
        { 
            try 
	        {	        
		         string strAxis = cmdAxis.Text;
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
                lblAxisPos.Text = mc.dic_Axis[axis].dPos.ToString("0.000");
                lblCT.Text = (Assem1.ct / 1000.0).ToString("0.00")+"s";
                lblCT2.Text = (Assem2.ct / 1000.0).ToString("0.00") + "s";
                lblCT3.Text = (lLenTime / 1000.0).ToString("0.00") + "s";
                if (bTestFlag)
                {
                    btnRun.Text = "停止";
                }
                else
                {
                    btnRun.Text = "启动";
                }
                int count = textBox1.Lines.Length;
                if (count > 10000)
                    textBox1.Clear();
	        }
	        catch (Exception)
	        {
		
		       
	        }
           

        }
        private void DisplayInfo(object info)
        {
            try
            {
                if (textBox1 != null)
                    textBox1.AppendText(DateTime.Now.ToString("yy-MM-dd HH:mm:ss:fff") + " " + info.ToString() + "\r\n");
            }
            catch (Exception ex) { }
        }
        public  void ShowInfo(string info)
        {
            try
            {
                synContext.Post(DisplayInfo, info);
            }
            catch (Exception) { }
        }

        private void btnAbsMove_Click(object sender, EventArgs e)
        {

            string strAxis = cmdAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            
            vel = 50;
            double newPos = (double)nudCmdPos.Value;
            mc.AbsMove(axis, newPos, vel);
           
        }

        private void btnJOGP_MouseDown(object sender, MouseEventArgs e)
        {
            string strAxis = cmdAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
           
            int vel = 0;

            vel = 50;
            mc.VelMove(axis, vel);
        }

        private void btnJOGP_MouseUp(object sender, MouseEventArgs e)
        {
            string strAxis = cmdAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);

            mc.StopAxis(axis);
        }

        private void btnJOGM_MouseDown(object sender, MouseEventArgs e)
        {
            string strAxis = cmdAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);

            int vel = 0;

            vel = -50;
            mc.VelMove(axis, vel);
        }

        private void btnJOGM_MouseUp(object sender, MouseEventArgs e)
        {
            string strAxis = cmdAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);

            mc.StopAxis(axis);
        }

        #region "运动参数"
        public bool bTestFlag = false;
        Assem1 assem1 = new Assem1();
        Assem1 assem2 = new Assem1();
        private void Run()
        {
            while (true)
            {
                try
                {
                    if (bTestFlag)
                    {
                        assem1.Run();
                        assem2.Run();
                       
                    }
                }
                catch (Exception)
                {
                    
                   
                }
                Thread.Sleep(1);
            }
        }

        private void Run1()
        {
            //while (true)
            //{
            //    try
            //    {
            //        if (bTestFlag)
            //        {
            //            switch (step)
            //            {
            //                case 0:
            //                    dDestPos = dPos1;
            //                    ((LeiE3032)mc).AbsMove2(axisTest, dDestPos, dVel, dAcc, dDec);
            //                    ShowInfo("总用时" + sw.ElapsedMilliseconds.ToString() + "ms");
            //                    sw.Restart();
            //                    ShowInfo(axisTest.ToString() + "到第一个点");
            //                    bshow = true;
            //                    swWait.Stop();
            //                    swWait.Reset();
            //                    step = 1;
            //                    break;
            //                case 1:
            //                    if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.02) && mc.dic_Axis[axisTest].INP)
            //                    {
            //                        if (bshow)
            //                        {
            //                            swWait.Restart();
            //                            bshow = false;
            //                            ShowInfo(axisTest.ToString() + "到第一个点---用时" + sw.ElapsedMilliseconds.ToString() + "ms");
            //                        }
            //                        if (swWait.ElapsedMilliseconds >= lStopTime)
            //                        {
            //                            bshow = true;
            //                            swWait.Stop();
            //                            swWait.Reset();
            //                            dDestPos = dPos2;

            //                            ((LeiE3032)mc).AbsMove2(axisTest, dDestPos, dVel, dAcc, dDec);
            //                            ShowInfo(axisTest.ToString() + "到第二个点");
            //                            step = 2;
            //                        }
            //                    }
            //                    break;
            //                case 2:
            //                    if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.02) && mc.dic_Axis[axisTest].INP)
            //                    {
            //                        if (bshow)
            //                        {
            //                            swWait.Restart();
            //                            bshow = false;
            //                            ShowInfo(axisTest.ToString() + "到第二个点---用时" + sw.ElapsedMilliseconds.ToString() + "ms");
            //                        }
            //                        if (swWait.ElapsedMilliseconds >= lStopTime)
            //                        {
            //                            bshow = true;
            //                            swWait.Stop();
            //                            swWait.Reset();
            //                            dDestPos = dPos3;

            //                            ((LeiE3032)mc).AbsMove2(axisTest, dDestPos, dVel, dAcc, dDec);
            //                            ShowInfo(axisTest.ToString() + "到第三个点");
            //                            step = 3;
            //                        }
            //                    }
            //                    break;
            //                case 3:
            //                    if ((Math.Abs(mc.dic_Axis[axisTest].dPos - dDestPos) < 0.02) && mc.dic_Axis[axisTest].INP)
            //                    {

            //                        if (bshow)
            //                        {
            //                            swWait.Restart();
            //                            bshow = false;
            //                            ShowInfo(axisTest.ToString() + "到第三个点---用时" + sw.ElapsedMilliseconds.ToString() + "ms");
            //                        }
            //                        if (swWait.ElapsedMilliseconds >= lStopTime)
            //                        {
            //                            bshow = true;
            //                            swWait.Stop();
            //                            swWait.Reset();
            //                            step = 0;
            //                        }
            //                    }
            //                    break;
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {


            //    }
            //    Thread.Sleep(1);
            //}
        }

        #endregion

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (bTestFlag)
            {
                bTestFlag = false;
            }
            else
            {
                Assem1.lLenTime = (long)nudLenTime.Value;
                swLenTime.Restart();
                
                bTestFlag = true;
                Assem1.bAssemFlag = false;
                Assem1.step = -3;
                Assem1.dAcc = (double)nudAcc.Value;
                Assem1.dDec = (double)nudDec.Value;
                Assem1.dVel = (double)nudRunVel.Value;
                Assem1.dPos1 = (double)nudPos1.Value;
                Assem1.dPos2 = (double)nudPos2.Value;
                Assem1.dPos3 = (double)nudPos3.Value;
                Assem1.lStopTime = (long)nudStopTime.Value;
                //string strAxis = cmdAxis.Text;
                //AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
                Assem1.axisTest = AXIS.组装X1轴;
                Assem1.axisZ = AXIS.组装Z1轴;

                Assem1.dAccZ = (double)nudAccZ.Value;
                Assem1.dDecZ = (double)nudDecZ.Value;
                Assem1.dVelZ = (double)nudVelZ.Value;
                Assem1.dPosZ1 = (double)nudPosZ1.Value;
                Assem1.dPosZ2 = (double)nudPosZ2.Value;

                Assem2.bAssemFlag = false;
                Assem2.step = -3;
                Assem2.dAcc = (double)nudAcc2.Value;
                Assem2.dDec = (double)nudDec2.Value;
                Assem2.dVel = (double)nudRunVel2.Value;
                Assem2.dPos1 = (double)nudPos4.Value;
                Assem2.dPos2 = (double)nudPos5.Value;
                Assem2.dPos3 = (double)nudPos6.Value;
                Assem2.lStopTime = (long)nudStopTime2.Value;
                //string strAxis = cmdAxis.Text;
                //AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
                Assem2.axisTest = AXIS.组装X2轴;
                Assem2.axisZ = AXIS.组装Z2轴;

                Assem2.dAccZ = (double)nudAccZ2.Value;
                Assem2.dDecZ = (double)nudDecZ2.Value;
                Assem2.dVelZ = (double)nudVelZ2.Value;
                Assem2.dPosZ1 = (double)nudPosZ3.Value;
                Assem2.dPosZ2 = (double)nudPosZ4.Value;


            }
        }

        private void FrmTestCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            timeEndPeriod(1);
            try
            {
                mc.StopAllAxis();
                mc.releaseCard();

               
                th_UpdateUI.Abort();
                th_UpdateUI = null;

               
            }
            catch (Exception ex)
            {
            }
            try
            {
                th_Run.Abort();
                th_Run = null;
            }
            catch (Exception)
            {
                
               
            }
            System.Environment.Exit(0);
        }

        private void btnSeverOn_Click(object sender, EventArgs e)
        {
            mc.SeverOnAll();
        }

        private void btnSeverOff_Click(object sender, EventArgs e)
        {
            mc.SeverOffAll();
        }


    }
}
