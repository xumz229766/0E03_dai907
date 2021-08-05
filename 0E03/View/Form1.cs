using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using CameraSet;
using Motion;
namespace _0E03
{
    public partial class Form1:UIHeaderAsideMainFrame
    {
        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern void timeEndPeriod(int t);

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        SynchronizationContext _context = null;
        Thread th_UpdateUI = null;
        public static FrmMain frmMain = null;
        public static FrmParamSet frmParamSet = null;
        public static MotionCard mc = null;
        public Form1()
        {
            CommonSet.LoadTray();
            InitializeComponent();
            int pageIndex = 1;
            frmMain = new FrmMain();
            UIPage page1 = AddPage(frmMain, pageIndex);
            TreeNode treenode = Aside.CreateNode(page1, pageIndex + 10000, pageIndex);
            treenode.Text = "Main";
            Aside.SetNodeItem(treenode, new NavMenuItem(page1));

            pageIndex = 2;
             frmParamSet = new FrmParamSet();
             page1 = AddPage(frmParamSet, pageIndex);
             treenode = Aside.CreateNode(page1, pageIndex + 10000, pageIndex);
             treenode.Text = "Param";
             Aside.SetNodeItem(treenode, new NavMenuItem(page1));


             _context = SynchronizationContext.Current;

           
        }
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //Process[] processes = Process.GetProcesses();
            //foreach (Process process in processes)
            //{

            try
            {
                Process process = Process.GetCurrentProcess();
                EmptyWorkingSet(process.Handle);
            }
            catch
            {
            }
            //}
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timeBeginPeriod(1);
            ModelManager.InitParam();
            ModelManager.InitCamera(CommonSet.strCameraFile);
            mc = MotionCard.getMotionCard();

          
            th_UpdateUI = new Thread(UpdateUI);
            th_UpdateUI.Start();

            Aside.SelectPage(1);
           
            Aside.SelectPage(2);
            Aside.SelectPage(1);
        }
        //界面更新
        public void UpdateUI()
        {
            while (true)
            {
                try
                {
                    _context.Post(UpdateForms, null);
                    ClearMemory();

                }
                catch (Exception ex)
                {

                    CommonSet.WriteDebug("更新界面异常：", ex);
                }


                Thread.Sleep(300);

            }

        }

       
        private void UpdateForms(object o)
        {
            if (frmMain != null)
            {
                frmMain.UpdateUI();
            }
            if (frmParamSet != null)
            {
                frmParamSet.UpdateUI();
            }
            //报警灯显示
            if (Alarminfo.GetAlarmList().Count > 0)
            {
                uiAlaramLight.OnColor = Color.Red;
                uiAlaramLight.OffColor = Color.Gray;
                if(uiAlaramLight.State != UILightState.Blink)
                   uiAlaramLight.State = UILightState.Blink;
            }
            else
            {
                if (Run.runMode == RunMode.运行)
                {
                    uiAlaramLight.OnColor = Color.Green;
                }
                else if (Run.runMode == RunMode.暂停)
                {
                    uiAlaramLight.OnColor = Color.Yellow;
                }
                else
                {
                    uiAlaramLight.OnColor = Color.Red;
                }
                uiAlaramLight.State = UILightState.On;
            }
        }
        private void uiSymbolMain_Click(object sender, EventArgs e)
        {
            Aside.SelectPage(1);
        }

        private void uiSymbolParamSet_Click(object sender, EventArgs e)
        {
            Aside.SelectPage(2);
        }

        private void uiSymbolExit_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timeEndPeriod(1);
            try
            {
                ModelManager.ReleaseCamera();
            }
            catch (Exception)
            {

               
            }
            try
            {
                if (th_UpdateUI.IsAlive)
                    th_UpdateUI.Abort();
                th_UpdateUI = null;
            }
            catch (Exception)
            {
                
                
            }
            System.Environment.Exit(0);
        }

        private void btnSaveParam_Click(object sender, EventArgs e)
        {
            if (ModelManager.SaveParam())
            {
                MessageBox.Show("保存参数成功!");
            }
            else
            {
                MessageBox.Show("保存参数失败!");
            }
        }

        FrmStatus frmAxisIO = null;
        private void uiSymbolIO_Click(object sender, EventArgs e)
        {
            if (frmAxisIO == null)
                frmAxisIO = new FrmStatus();
            frmAxisIO.Show();
            frmAxisIO.Activate();

        }
    }
}
