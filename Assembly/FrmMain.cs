using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using Tray;
using ConfigureFile;
using log4net;
using Motion;
using ImageProcess;
using CameraSet;
using HalconDotNet;
using System.IO;
using System.Runtime.InteropServices;
namespace Assembly
{
    public partial class FrmMain : Form
    {
        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern void timeEndPeriod(int t);

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        //[DllImport("FreeMem.dll")]
        //static extern void FreeMem();
        //public const string str_free_file = @".\FreeMem.dll";
        //[DllImport(str_free_file, CallingConvention = CallingConvention.Cdecl, EntryPoint = "FreeMem")]
        //public static extern void FreeMem();
        //Tray.Tray tray = null;//托盘
        //TrayPanel trayPanel = null;//托盘显示
        MotionCard mc = null;//运动控制卡
        Alarminfo alarmInfo = null; 
       
        SynchronizationContext _context = null;
        Thread th_UpdateUI = null;
        Thread th_ShowDialog = null;
        Run run = null;
        FrmShowImage frmShowImage = new FrmShowImage();
        List<Label> lstCurrentNum = new List<Label>();//存放取料当前位置
        List<Label> lstStartPos = new List<Label>();//取料起始位
        List<Label> lstEndPos = new List<Label>();//取料结束位
        List<Label> lstProductSuctionCheck = new List<Label>();//取料真空检测
        List<Label> lstAssembleSuctionCheck = new List<Label>();//组装真空检测
        List<Label> lstAssembleSetAngle = new List<Label>();//组装设定角度
        List<Label> lstAssembleSetPressure = new List<Label>();//组装设定压力
        List<Label> lstAssembleImgResult = new List<Label>();//组装实际压力
        List<Label> lstCorrect = new List<Label>();//中转位真空检测
        
        List<HWindow> lstHWindow = new List<HWindow>();//自动图像显示窗口
        public ShowImageClass ShowImage = new ShowImageClass();

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
        public FrmMain()
        {
          
           // SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            CommonSet.LoadTray();
            CommonSet.InitParam();
           
            InitializeComponent();
            _context = SynchronizationContext.Current;
            this.IsMdiContainer = true;
           
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;////用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}
        private void initCamera()
        {
            try
            {
                ICamera.ShowInfo += CommonSet.WriteInfo;
                //初始化相机参数
                ICamera.getCameras();
                CommonSet.cameraManager = CameraManager.getCameraManager( CommonSet.strCameraFile);
                CommonSet.camUpA1 = CommonSet.cameraManager.getCamera(CameraName.CamUpA1);
                CommonSet.camUpA1.RemoveDelegate();
                CommonSet.camUpA1.ProcessImage += ShowImage.ProcessImageA1Up;
               // CommonSet.camUpA1.InitCamera(CameraName.CamUpA1.ToString());
                

                CommonSet.camDownA1 = CommonSet.cameraManager.getCamera(CameraName.CamDownA1);
                CommonSet.camDownA1.RemoveDelegate();
                CommonSet.camDownA1.ProcessImage += ShowImage.ProcessImageA1Down;
               // CommonSet.camDownA1.InitCamera(CameraName.CamDownA1.ToString());

                CommonSet.camUpA2 = CommonSet.cameraManager.getCamera(CameraName.CamUpA2);
                CommonSet.camUpA2.RemoveDelegate();
                CommonSet.camUpA2.ProcessImage += ShowImage.ProcessImageA2Up;
               // CommonSet.camUpA2.InitCamera(CameraName.CamUpA2.ToString());

                CommonSet.camDownA2 = CommonSet.cameraManager.getCamera(CameraName.CamDownA2);
                CommonSet.camDownA2.RemoveDelegate();
                CommonSet.camDownA2.ProcessImage += ShowImage.ProcessImageA2Down;
               // CommonSet.camDownA2.InitCamera(CameraName.CamDownA2.ToString());

                CommonSet.camUpC1 = CommonSet.cameraManager.getCamera(CameraName.CamUpC1);
                CommonSet.camUpC1.RemoveDelegate();
                CommonSet.camUpC1.ProcessImage += ShowImage.ProcessImageC1Up;
               // CommonSet.camUpC1.InitCamera(CameraName.CamUpC1.ToString());

                CommonSet.camDownC1 = CommonSet.cameraManager.getCamera(CameraName.CamDownC1);
                CommonSet.camDownC1.RemoveDelegate();
                CommonSet.camDownC1.ProcessImage += ShowImage.ProcessImageC1Down;
                //CommonSet.camDownC1.InitCamera(CameraName.CamDownC1.ToString());

                CommonSet.camUpC2 = CommonSet.cameraManager.getCamera(CameraName.CamUpC2);
                CommonSet.camUpC2.RemoveDelegate();
                CommonSet.camUpC2.ProcessImage += ShowImage.ProcessImageC2Up;
               // CommonSet.camUpC2.InitCamera(CameraName.CamUpC2.ToString());

                CommonSet.camDownC2 = CommonSet.cameraManager.getCamera(CameraName.CamDownC2);
                CommonSet.camDownC2.RemoveDelegate();
                CommonSet.camDownC2.ProcessImage += ShowImage.ProcessImageC2Down;
               // CommonSet.camDownC2.InitCamera(CameraName.CamDownC2.ToString());

                CommonSet.camDownD1 = CommonSet.cameraManager.getCamera(CameraName.CamDownD1);
                CommonSet.camDownD1.RemoveDelegate();
                CommonSet.camDownD1.ProcessImage += ShowImage.ProcessImageDDown;
                // CommonSet.camDownD1.InitCamera(CameraName.CamDownD1.ToString());

                CommonSet.camUpD1 = CommonSet.cameraManager.getCamera(CameraName.CamUpD1);
                CommonSet.camUpD1.RemoveDelegate();
                CommonSet.camUpD1.ProcessImage += ShowImage.ProcessImageDUp;
                // CommonSet.camUpD1.InitCamera(CameraName.CamUpD1.ToString());
           
            }
            catch (Exception ex)
            {

                CommonSet.WriteInfo("初始化相机异常,"+ex.ToString());
            }

        }
        //初始化光源
        private void initLight()
        {
            CommonSet.lightManager = LightManager.getLightManager();
            CommonSet.lc = CommonSet.lightManager.getControl(0);
        }
        //初始化串口参数，用于电气比例阀
        private void initSerialAV()
        {
            CommonSet.serailAV = SerialAV.GetInstance();
            CommonSet.serailAV.InitParam(CommonSet.strSaveFile);
        }
        #region"TabParamSet选项卡"
        bool[] bShowSuctionUI = new bool[15];
        //Dictionary<int, ControlSuction> dic_SuctionUI = new Dictionary<int, ControlSuction>();//镜片吸笔界面
       // BarrelSuctionUI bs = null;//镜筒吸笔界面
        int iSelectSuctionTabIndex = 1;//当前选中的吸笔索引
        public static  int iSelectParamSetIndex = 0;//参数界面索引
        private void LoadSuctionSetUI()
        {
            //optSuctionUI = new OptSuctionUI(CommonSet.tpOptUIPanel);
            // optSuctionUI.Parent = tabPageParamSuction;
            // optSuctionUI.Dock = DockStyle.Fill;
            // optSuctionUI.Show();
            // optSuctionUI.Dock = DockStyle.Fill;
            // bShowSuctionUI[1] = true;

            frmOpt1 = new FrmOpt1Set(CommonSet.tpOptUIPanel1);
            frmOpt1.FormBorderStyle = FormBorderStyle.None;
            frmOpt1.TopLevel = false;
            frmOpt1.Parent = tabPageParamSuction1;
            frmOpt1.ControlBox = false;
            frmOpt1.Dock = DockStyle.Fill;
            frmOpt1.Show();
            bParamSetUI[0] = true;
           // bShowSuctionUI[1] = true;
        }
      
       
        bool[] bParamSetUI = new bool[6];

        public static FrmOpt1Set frmOpt1 = null;
        public static FrmOpt2Set frmOpt2 = null;
        public static FrmAssem1Set frmAssem1 = null;
        public static FrmAssem2Set frmAssem2 = null;


       public static OptSuctionUI optSuctionUI = null;
       public static AssembleUI assembUI = null;
       public static BarrelUI barrelUI = null;
       //public static FrmCalib frmCalib = null;
       public static FrmOtherSet frmOther = null;
        //FrmCalib fc = null;
        //FrmPixelCompute fpc = null;
        //FrmOtherSet frmOther = null;
        private void tabParamSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabParamSet.SelectedIndex ;
            switch (index)
            {
                case 0:
                    iSelectParamSetIndex = 0;
                    if (!bParamSetUI[index])
                    {
                        frmOpt1 = new FrmOpt1Set();
                        GenerateForm(frmOpt1, ((TabControl)sender).SelectedTab);
                        frmOpt1.Dock = DockStyle.Fill;
                        frmOpt1.Show();
                      
                        bParamSetUI[index] = true;
                       
                       // bShowSuctionUI[iSelectSuctionTabIndex] = true;
                    }
                    break;
                case 1:
                    iSelectParamSetIndex = 1;
                    if (!bParamSetUI[index])
                    {
                        frmOpt2 = new FrmOpt2Set(CommonSet.tpOptUIPanel2);
                        GenerateForm(frmOpt2, sender);
                        frmOpt2.Dock = DockStyle.Fill;
                        frmOpt2.Show();

                        bParamSetUI[index] = true;

                        // bShowSuctionUI[iSelectSuctionTabIndex] = true;
                    }
                    break;

                case 2:
                    iSelectParamSetIndex = 2;
                    if (!bParamSetUI[index])
                    {
                        frmAssem1 = new FrmAssem1Set();
                        GenerateForm(frmAssem1, sender);
                        frmAssem1.Dock = DockStyle.Fill;
                        frmAssem1.Show();
                        bParamSetUI[index] = true;
                    }

                    break;
                case 3:
                    iSelectParamSetIndex = 3;
                    if (!bParamSetUI[index])
                    {
                        frmAssem2 = new FrmAssem2Set();
                        GenerateForm(frmAssem2, sender);
                        frmAssem2.Dock = DockStyle.Fill;
                        frmAssem2.Show();
                        bParamSetUI[index] = true;
                    }
                    break;
                case 4:
                    iSelectParamSetIndex = 4;
                    if (!bParamSetUI[index])
                    {
                        barrelUI = new BarrelUI(CommonSet.tpBarrelPanel);
                        barrelUI.Parent = ((TabControl)sender).SelectedTab;
                        barrelUI.Dock = DockStyle.Fill;
                        barrelUI.Show();
                        bParamSetUI[index] = true;
                        
                    }
                    break;
                case 5:
                    iSelectParamSetIndex = 5;
                    if (!bParamSetUI[index])
                    {
                        frmOther = new FrmOtherSet();
                        GenerateForm(frmOther, sender);
                        frmOther.Show();
                        bParamSetUI[index] = true;
                    }
                    break;
                //case 4:
                //    iSelectParamSetIndex = 4;
                //    if (!bParamSetUI[index])
                //    {
                //        //fpc = new FrmPixelCompute();
                //        //GenerateForm(fpc, sender);
                //        //fpc.Show();
                //        //bParamSetUI[index] = true;
                //    }
                //    break;
                //case 5:
                //     iSelectParamSetIndex = 5;
                //    if (!bParamSetUI[index])
                //    {
                //        //frmOther = new FrmOtherSet();
                //        //GenerateForm(frmOther, sender);
                //        //frmOther.Show();
                //        //bParamSetUI[index] = true;
                //    }
                //    break;
                default:
                   
                    break;

            }
        }
        #endregion
        List<Label> lstSuctionName = new List<Label>();
       //添加主界面显示元素
        private void AddMainShowUI()
        {
            lstSuctionName.Clear();
            lstSuctionName.Add(lblSuctionName1);
            lstSuctionName.Add(lblSuctionName2);
            lstSuctionName.Add(lblSuctionName3);
            lstSuctionName.Add(lblSuctionName4);
            lstSuctionName.Add(lblSuctionName5);
            lstSuctionName.Add(lblSuctionName6);
            lstSuctionName.Add(lblSuctionName7);
            lstSuctionName.Add(lblSuctionName8);
            lstSuctionName.Add(lblSuctionName9);
            lstSuctionName.Add(lblSuctionName10);
            lstSuctionName.Add(lblSuctionName11);
            lstSuctionName.Add(lblSuctionName12);
            lstSuctionName.Add(lblSuctionName13);
            lstSuctionName.Add(lblSuctionName14);
            lstSuctionName.Add(lblSuctionName15);
            lstSuctionName.Add(lblSuctionName16);
            lstSuctionName.Add(lblSuctionName17);
            lstSuctionName.Add(lblSuctionName18);
            for (int i = 1; i < 19; i++)
            {
                Label lbl = new Label();
                lbl = new Label();
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblSetAngle" + i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "0";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lstAssembleSetAngle.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 2);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i-9, 2);
                }
                lbl = new Label();
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblSetPressure" + i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "0";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lstAssembleSetPressure.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 3);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 3);
                }

                 lbl = new Label();
                //添加取料当前位
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Blue;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblCurrentNum"+i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "0";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbl.DoubleClick += lbl_DoubleClick;
                lstCurrentNum.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 4);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 4);
                }

                lbl = new Label();
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblStartPos" + i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "1";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbl.DoubleClick +=lblStartPos_DoubleClick;
                lstStartPos.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 5);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 5);
                }

                lbl = new Label();
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblEndPos" + i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "100";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbl.DoubleClick +=lblEndPos_DoubleClick;
                lstEndPos.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 6);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 6);
                }

                Label lblSuctionCheck = new Label();
                lblSuctionCheck.BackColor = System.Drawing.Color.Red;
                lblSuctionCheck.Dock = System.Windows.Forms.DockStyle.Fill;
                lblSuctionCheck.Location = new System.Drawing.Point(110, 166);
                lblSuctionCheck.Margin = new System.Windows.Forms.Padding(10);
                lblSuctionCheck.Name = "lblProductSuctionCheck" + i.ToString();
                lblSuctionCheck.Size = new System.Drawing.Size(39, 22);
                lstProductSuctionCheck.Add(lblSuctionCheck);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lblSuctionCheck, i, 7);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lblSuctionCheck, i - 9, 7);
                }

               

                lbl = new Label();
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lbl.ForeColor = System.Drawing.Color.Black;
                lbl.Location = new System.Drawing.Point(100, 30);
                lbl.Margin = new System.Windows.Forms.Padding(0);
                lbl.Name = "lblImageResult" + i.ToString();
                lbl.Size = new System.Drawing.Size(59, 42);
                lbl.Text = "NG";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lstAssembleImgResult.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 8);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 8);
                }

                lbl = new Label();
                lbl.BackColor = System.Drawing.Color.Red;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Location = new System.Drawing.Point(110, 166);
                lbl.Margin = new System.Windows.Forms.Padding(10);
                lbl.Name = "lblCorrect" + i.ToString();
                lbl.Size = new System.Drawing.Size(39, 22);
                lstCorrect.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 9);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 9);
                }

                lbl = new Label();
                lbl.BackColor = System.Drawing.Color.Red;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Location = new System.Drawing.Point(110, 166);
                lbl.Margin = new System.Windows.Forms.Padding(10);
                lbl.Name = "lblAssembleSuction" + i.ToString();
                lbl.Size = new System.Drawing.Size(39, 22);
                lstAssembleSuctionCheck.Add(lbl);
                if (i < 10)
                {
                    tableLayoutPanel2.Controls.Add(lbl, i, 10);
                }
                else
                {
                    tableLayoutPanel6.Controls.Add(lbl, i - 9, 10);
                }
            }
        
        }

        private void lblEndPos_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            int index = Convert.ToInt32(name.Substring(9));
            FrmSetDialog fsd = new FrmSetDialog(index, "取料结束位");
            fsd.ShowDialog();
            fsd = null;
        }

        private void lblStartPos_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            int index = Convert.ToInt32(name.Substring(11));
            FrmSetDialog fsd = new FrmSetDialog(index, "取料起始位");
            fsd.ShowDialog();
            fsd = null;
        }

        //镜片取料位双击设置
        void lbl_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(index,"当前取料位");
            fsd.ShowDialog();
            fsd = null;
        }
        private void initHwindow()
        {
            lstHWindow.Clear();
            //lstHWindow.Add(hWindowControl1.HalconWindow);
            //lstHWindow.Add(hWindowControl2.HalconWindow);
            //lstHWindow.Add(hWindowControl3.HalconWindow);
            //lstHWindow.Add(hWindowControl4.HalconWindow);
            //lstHWindow.Add(hWindowControl5.HalconWindow);
            //lstHWindow.Add(hWindowControl6.HalconWindow);
            //lstHWindow.Add(hWindowControl7.HalconWindow);
            //lstHWindow.Add(hWindowControl8.HalconWindow);
            //lstHWindow.Add(hWindowControl9.HalconWindow);
            //lstHWindow.Add(hWindowControl10.HalconWindow);
            //lstHWindow.Add(hWindowControl11.HalconWindow);
            //lstHWindow.Add(hWindowControl12.HalconWindow);
            //lstHWindow.Add(hWindowControl13.HalconWindow);
            //lstHWindow.Add(hWindowControl14.HalconWindow);
            //lstHWindow.Add(hWindowControl15.HalconWindow);
            //lstHWindow.Add(hWindowControl16.HalconWindow);
            //lstHWindow.Add(hWindowControl17.HalconWindow);
            //lstHWindow.Add(hWindowControl18.HalconWindow);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //CommonSet.ShowDebug += CommonSet.CommonSet_ShowDebug;
                timeBeginPeriod(1);
                AddMainShowUI();
                CommonSet.showMsg += ShowMsg;
                th_UpdateUI = new Thread(UpdateUI);
                th_UpdateUI.Start();
                th_ShowDialog = new Thread(CommonSet.RunShowDialog);
                th_ShowDialog.Start();

                Action initCam = initCamera;
                initCam.BeginInvoke(null, null);
                mc = MotionCard.getMotionCard();

                mc.ShowInfo += CommonSet.WriteInfo;
               // mc.initCard(Application.StartupPath + "\\");
                mc.SeverOnAll();
                //初始化图像处理
                CommonSet.imageManager = ImageProcessManager.GetInstance();
                CommonSet.imageManager.InitParam(CommonSet.strProductName);//初始化参数
               // initHwindow();
                //初始化相机和光源
                //initCamera();
              
                initLight();
                //initSerialAV();
          
                // LoadTray();
                LoadSuctionSetUI();
                run = Run.getInstance();
                alarmInfo = new Alarminfo();
                if (frmhand == null)
                {
                    frmhand = new FrmHand();
                    frmhand.Show();
                    frmhand.Visible = false;
                }
                frmShowImage.Show();
                frmShowImage.Visible = false;
                CommonSet.lstOtherWin.Add(hWindowControl1.HalconWindow);
                CommonSet.lstOtherWin.Add(hWindowControl2.HalconWindow);

              
                frmhand.Visible = false;
                CommonSet.spMH = SerialPortMeasureHeight.GetInstance();
                CommonSet.spMH.InitParam(CommonSet.strProductParamPath + "SerialPortParam.ini");
                CommonSet.spPre = SerialPortMeasurePressure.GetInstance();
                CommonSet.spPre.InitParam(CommonSet.strProductParamPath + "SerialPortParam.ini");
                //设置初始电压
                mc.WriteOutDA((float)CommonSet.fCommonPressureV1, 0);
                mc.WriteOutDA((float)CommonSet.fCommonPressureV2, 2);
                //打开光源
                mc.setDO(DO.照明灯开启, true);
                mc.setDO(DO.真空泵继电器, true);
                //SerialAV.WriteOutputA(1, SerialAV.GetAByPressure(ProductSuction.dPresureUsual));
                //Thread.Sleep(500);
                //SerialAV.WriteOutputA(2, SerialAV.GetAByPressure(ProductSuction.dPresureUsual));
            }
            catch (Exception ex)
            {

                CommonSet.WriteDebug("初始化异常",ex);
            }       
        }
        #region"更新UI"
        public void UpdateUI()
        {
            while (true)
            {
                try
                {
                    _context.Send(UpdateAll, null);
                    //_context.Send(UpdateModeUI, null);
                    //_context.Send(UpdateShowInfo, null);
                    //_context.Send(UpdateBarrelUI, null);
                    //_context.Send(UpdateSuctionUI, null);
                    //_context.Send(UpdateOtherUI, null);

                    //ClearMemory();
                   
                }
                catch (Exception ex)
                {

                    CommonSet.WriteDebug("更新界面异常：", ex);
                }


                Thread.Sleep(300);

            }

        }
        public void UpdateAll(object o)
        {
            UpdateModeUI(null);
            UpdateShowInfo(null);
            UpdateBarrelUI(null);
            UpdateSuctionUI(null);
            UpdateOtherUI(null);
            ClearMemory();
        }
        public void UpdateModeUI(object o)
        {
            if (Run.bReset)
            {
                btnReset.BackColor = System.Drawing.Color.PaleGreen;
            }
            else
            {
                btnReset.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            if (Run.runMode == RunMode.手动)
            {
                btnHand.BackColor = System.Drawing.Color.PaleGreen;
                btnRun.BackColor = System.Drawing.Color.WhiteSmoke;
                btnStop.Enabled = false;
                btnStop.BackColor = System.Drawing.Color.WhiteSmoke;
                btnAuto.Enabled = false;
                btnAuto.BackColor = System.Drawing.Color.WhiteSmoke;
                btnStepMove.Enabled = false;
                btnNext.Enabled = false;
                btnStepMove.BackColor = System.Drawing.Color.WhiteSmoke;
                btnNext.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else if (Run.runMode == RunMode.运行)
            {

                btnHand.BackColor = System.Drawing.Color.WhiteSmoke;
                btnRun.BackColor = System.Drawing.Color.PaleGreen;
                btnStop.Enabled = true;
                btnStop.BackColor = System.Drawing.Color.WhiteSmoke;
                btnAuto.Enabled = true;
                btnAuto.BackColor = System.Drawing.Color.PaleGreen;
                btnStepMove.Enabled = true;
                if (Run.bRunStep)
                {
                    btnNext.Enabled = true;
                    btnStepMove.BackColor = System.Drawing.Color.PaleGreen;
                }
                else
                {
                    btnNext.Enabled = false;
                    btnStepMove.BackColor = System.Drawing.Color.WhiteSmoke;
                }

            }
            else if (Run.runMode == RunMode.暂停)
            {
                btnHand.BackColor = System.Drawing.Color.WhiteSmoke;
                btnRun.BackColor = System.Drawing.Color.PaleGreen;
                btnStop.Enabled = true;
                btnStop.BackColor = System.Drawing.Color.PaleGreen;
                btnAuto.BackColor = System.Drawing.Color.WhiteSmoke;
                btnAuto.Enabled = true;
                btnStepMove.Enabled = true;
                if (Run.bRunStep)
                {
                    btnNext.Enabled = true;
                    btnStepMove.BackColor = System.Drawing.Color.PaleGreen;
                }
                else
                {
                    btnNext.Enabled = false;
                    btnStepMove.BackColor = System.Drawing.Color.WhiteSmoke;
                }

            }
            bool bFlag = true;
            foreach (KeyValuePair<AXIS, bool> pair in mc.dic_HomeStatus)
            {
                bFlag = bFlag & pair.Value;
            }
            if (bFlag)
            {
                btnHome.BackColor = System.Drawing.Color.PaleGreen;
            }
            else
            {
                btnHome.BackColor = System.Drawing.Color.Yellow;
            }
            

        }
        public void UpdateShowInfo(object o)
        {
            try
            {
                lblCircleTime.Text = CommonSet.swCircleD.Elapsed.TotalSeconds.ToString("0.0");
                bool bFinishFlag = false;
                for (int i = 0; i < 9; i++)
                {
                    try
                    {
                        lstSuctionName[i].Text = CommonSet.dic_OptSuction1[i + 1].Name;
                        lstSuctionName[i + 9].Text = CommonSet.dic_OptSuction2[i + 10].Name;
                        #region"组装1部"
                        if (CommonSet.dic_OptSuction1[i + 1].BUse)
                        {
                            lstCurrentNum[i].Enabled = true;
                            lstStartPos[i].Enabled = true;
                            lstEndPos[i].Enabled = true;
                            lstProductSuctionCheck[i].Enabled = true;
                            lstAssembleSuctionCheck[i].Enabled = true;
                            lstAssembleSetAngle[i].Enabled = true;
                            lstAssembleSetPressure[i].Enabled = true;
                            lstAssembleImgResult[i].Enabled = true;
                            lstCorrect[i].Enabled = true;
                            //Tray.Tray temp = CommonSet.dic_ProductSuction[i + 1].tray;
                            if (CommonSet.dic_OptSuction1[i+1].bFinish)
                            {
                                lstCurrentNum[i].ForeColor = Color.Red;
                                lstCurrentNum[i].Text = "N";
                                bFinishFlag = true;
                            }
                            else
                            {
                                lstCurrentNum[i].ForeColor = Color.Blue;
                                lstCurrentNum[i].Text = CommonSet.dic_OptSuction1[i+1].CurrentPos.ToString();
                            }

                            lstStartPos[i].Text = CommonSet.dic_OptSuction1[i + 1].StartPos.ToString();
                            lstEndPos[i].Text = CommonSet.dic_OptSuction1[i + 1].EndPos.ToString();
                            string strD = "取料真空检测" + (i + 1).ToString();
                            DI d = (DI)Enum.Parse(typeof(DI), strD);
                            lstProductSuctionCheck[i].BackColor = getColor(mc.dic_DI[d]);
                            strD = "中转真空检测" + (i + 1).ToString();
                            d = (DI)Enum.Parse(typeof(DI), strD);
                            lstCorrect[i].BackColor = getColor(mc.dic_DI[d]);
                            strD = "组装真空检测" + (i + 1).ToString();
                             d = (DI)Enum.Parse(typeof(DI), strD);
                            lstAssembleSuctionCheck[i].BackColor = getColor(mc.dic_DI[d]);
                            lstAssembleSetAngle[i].Text = CommonSet.dic_Assemble1[i + 1].dAngleSet.ToString();
                            lstAssembleSetPressure[i].Text = CommonSet.dic_Assemble1[i + 1].dPressureSet.ToString();
                            if (CommonSet.dic_OptSuction1[i + 1].imgResultDown.bImageResult)
                            {
                                lstAssembleImgResult[i].ForeColor = Color.Green;
                                lstAssembleImgResult[i].Text = "OK";

                            }
                            else
                            {
                                lstAssembleImgResult[i].ForeColor = Color.Red;
                                lstAssembleImgResult[i].Text = "NG";
                            }

                        }
                        else
                        {
                            lstCurrentNum[i].Enabled = false;
                            //lstCurrentNum[i].BackColor = Color.Transparent;
                            lstStartPos[i].Enabled = false;
                            lstEndPos[i].Enabled = false;
                            lstProductSuctionCheck[i].Enabled = false;
                            lstAssembleSuctionCheck[i].Enabled = false;
                            lstAssembleSetAngle[i].Enabled = false;
                            lstAssembleSetPressure[i].Enabled = false;
                            lstAssembleImgResult[i].Enabled = false;
                            lstCorrect[i].Enabled = false;
                            lstProductSuctionCheck[i].BackColor = getColor(false);
                            lstCorrect[i].BackColor = getColor(false);
                            lstAssembleSuctionCheck[i].BackColor = getColor(false);
                            
                            //lstAssembleSuctionCheck[i].BackColor = getColor(false);

                        }
                        #endregion
                        #region"组装2部"
                        if (CommonSet.dic_OptSuction2[i + 10].BUse)
                        {
                            lstCurrentNum[i+9].Enabled = true;
                            lstStartPos[i + 9].Enabled = true;
                            lstEndPos[i + 9].Enabled = true;
                            lstProductSuctionCheck[i + 9].Enabled = true;
                            lstAssembleSuctionCheck[i + 9].Enabled = true;
                            lstAssembleSetAngle[i + 9].Enabled = true;
                            lstAssembleSetPressure[i + 9].Enabled = true;
                            lstAssembleImgResult[i + 9].Enabled = true;
                            lstCorrect[i + 9].Enabled = true;
                            //Tray.Tray temp = CommonSet.dic_ProductSuction[i + 1].tray;
                            if (CommonSet.dic_OptSuction2[i + 10].bFinish)
                            {
                                lstCurrentNum[i + 9].ForeColor = Color.Red;
                                lstCurrentNum[i + 9].Text = "N";
                                bFinishFlag = true;
                            }
                            else
                            {
                                lstCurrentNum[i + 9].ForeColor = Color.Blue;
                                lstCurrentNum[i + 9].Text = CommonSet.dic_OptSuction2[i + 10].CurrentPos.ToString();
                            }

                            lstStartPos[i + 9].Text = CommonSet.dic_OptSuction2[i + 10].StartPos.ToString();
                            lstEndPos[i + 9].Text = CommonSet.dic_OptSuction2[i + 10].EndPos.ToString();
                            string strD = "取料真空检测" + (i + 10).ToString();
                            DI d = (DI)Enum.Parse(typeof(DI), strD);
                            lstProductSuctionCheck[i + 9].BackColor = getColor(mc.dic_DI[d]);
                            strD = "中转真空检测" + (i + 10).ToString();
                            d = (DI)Enum.Parse(typeof(DI), strD);
                            lstCorrect[i + 9].BackColor = getColor(mc.dic_DI[d]);
                            strD = "组装真空检测" + (i + 10).ToString();
                            d = (DI)Enum.Parse(typeof(DI), strD);
                            lstAssembleSuctionCheck[i + 9].BackColor = getColor(mc.dic_DI[d]);
                            lstAssembleSetAngle[i + 9].Text = CommonSet.dic_Assemble2[i + 10].dAngleSet.ToString();
                            lstAssembleSetPressure[i + 9].Text = CommonSet.dic_Assemble2[i + 10].dPressureSet.ToString();
                            if (CommonSet.dic_OptSuction2[i + 10].imgResultDown.bImageResult)
                            {
                                lstAssembleImgResult[i + 9].ForeColor = Color.Green;
                                lstAssembleImgResult[i + 9].Text = "OK";

                            }
                            else
                            {
                                lstAssembleImgResult[i + 9].ForeColor = Color.Red;
                                lstAssembleImgResult[i + 9].Text = "NG";
                            }

                        }
                        else
                        {
                            lstCurrentNum[i + 9].Enabled = false;
                            //lstCurrentNum[i].BackColor = Color.Transparent;
                            lstStartPos[i + 9].Enabled = false;
                            lstEndPos[i + 9].Enabled = false;
                            lstProductSuctionCheck[i + 9].Enabled = false;
                            lstAssembleSuctionCheck[i + 9].Enabled = false;
                            lstAssembleSetAngle[i + 9].Enabled = false;
                            lstAssembleSetPressure[i + 9].Enabled = false;
                            lstAssembleImgResult[i + 9].Enabled = false;
                            lstCorrect[i + 9].Enabled = false;
                            lstProductSuctionCheck[i + 9].BackColor = getColor(false);
                           
                            lstCorrect[i+9].BackColor = getColor(false);
                            lstAssembleSuctionCheck[i+9].BackColor = getColor(false);
                            
                            //lstAssembleSuctionCheck[i].BackColor = getColor(false);

                        }
                        #endregion
                        
                    }
                    catch (Exception ex)
                    {
                        CommonSet.WriteInfo("更新组装参数异常！" + ex.ToString());

                    }
                }
                lblGetPos.Text = CommonSet.dic_BarrelSuction[1].CurrentPos.ToString();
                lblPutPos.Text = CommonSet.dic_BarrelSuction[2].CurrentPos.ToString();
                lblStation1Barrel.Text = ParamListerner.station1.iCurrentBarrelNum.ToString();
                lblStation2Barrel.Text = ParamListerner.station2.iCurrentBarrelNum.ToString();
                lblLenStartPos.Text = CommonSet.dic_BarrelSuction[1].StartPos.ToString();
                lblLenEndPos.Text = CommonSet.dic_BarrelSuction[1].EndPos.ToString();
                lblA1Barrel.Text = GetProduct1Module.iCurrentBarrel.ToString();
                lblC1Barrel.Text = Assem1Module.iCurrentLen.ToString();
                lblA1Circle.Text = CommonSet.swCircleA1.Elapsed.TotalSeconds.ToString("0.0");
                lblC1Circle.Text = CommonSet.swCircleC1.Elapsed.TotalSeconds.ToString("0.0");
                lblC1Station.Text = Assem1Module.iCurrentStation.ToString();

                lblA2Barrel.Text = GetProduct2Module.iCurrentBarrel.ToString();
                lblC2Barrel.Text = Assem2Module.iCurrentLen.ToString();
                lblA2Circle.Text = CommonSet.swCircleA2.Elapsed.TotalSeconds.ToString("0.0");
                lblC2Circle.Text = CommonSet.swCircleC2.Elapsed.TotalSeconds.ToString("0.0");
                lblC2Station.Text = Assem2Module.iCurrentStation.ToString();

                lblStation1Barrel.Text = ParamListerner.station1.iCurrentBarrelNum.ToString();
                lblStation2Barrel.Text = ParamListerner.station2.iCurrentBarrelNum.ToString();
                if (bFinishFlag)
                {
                    lblGetSuctionFlag1.BackColor = Color.Red;
                    lblGetSuctionFlag2.BackColor = Color.Red;
                }
                else
                {
                    lblGetSuctionFlag1.BackColor = Color.Gainsboro;
                    lblGetSuctionFlag2.BackColor = Color.Gainsboro;
                }

            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("更新显示数据异常！" + ex.ToString());

            }
            #region"相机异常"
            if (CommonSet.camUpA1 != null)
            {
                if (!CommonSet.camUpA1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.取料1上相机连接异常))
                    {
                        CommonSet.WriteInfo("取料1上相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.取料1上相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.取料1上相机连接异常))
                {
                    CommonSet.WriteInfo("取料1上相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.取料1上相机连接异常);
                }
            }
            if (CommonSet.camDownA1 != null)
            {
                if (!CommonSet.camDownA1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.取料1下相机连接异常))
                    {
                        CommonSet.WriteInfo("取料1下相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.取料1下相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.取料1下相机连接异常))
                {
                    CommonSet.WriteInfo("取料1下相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.取料1下相机连接异常);
                }
            }

            if (CommonSet.camUpA2 != null)
            {
                if (!CommonSet.camUpA2.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.取料2上相机连接异常))
                    {
                        CommonSet.WriteInfo("取料2上相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.取料2上相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.取料2上相机连接异常))
                {
                    CommonSet.WriteInfo("取料2上相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.取料2上相机连接异常);
                }
            }
            if (CommonSet.camDownA2 != null)
            {
                if (!CommonSet.camDownA2.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.取料2下相机连接异常))
                    {
                        CommonSet.WriteInfo("取料2下相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.取料2下相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.取料2下相机连接异常))
                {
                    CommonSet.WriteInfo("取料2下相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.取料2下相机连接异常);
                }
            }

            if (CommonSet.camUpC1 != null)
            {
                if (!CommonSet.camUpC1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.组装1上相机连接异常))
                    {
                        CommonSet.WriteInfo("组装1上相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.组装1上相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.组装1上相机连接异常))
                {
                    CommonSet.WriteInfo("组装1上相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.组装1上相机连接异常);
                }
            }

            if (CommonSet.camDownC1 != null)
            {
                if (!CommonSet.camDownC1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.组装1下相机连接异常))
                    {
                        CommonSet.WriteInfo("组装1下相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.组装1下相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.组装1下相机连接异常))
                {
                    CommonSet.WriteInfo("组装1下相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.组装1下相机连接异常);
                }
            }

            if (CommonSet.camUpC2 != null)
            {
                if (!CommonSet.camUpC2.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.组装2上相机连接异常))
                    {
                        CommonSet.WriteInfo("组装2上相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.组装2上相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.组装2上相机连接异常))
                {
                    CommonSet.WriteInfo("组装2上相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.组装2上相机连接异常);
                }
            }

            if (CommonSet.camDownC2 != null)
            {
                if (!CommonSet.camDownC2.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.组装2下相机连接异常))
                    {
                        CommonSet.WriteInfo("组装2下相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.组装2下相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.组装2下相机连接异常))
                {
                    CommonSet.WriteInfo("组装2下相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.组装2下相机连接异常);
                }
            }

            if (CommonSet.camDownD1 != null)
            {
                if (!CommonSet.camDownD1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.点胶和镜筒下相机连接异常))
                    {
                        CommonSet.WriteInfo("点胶和镜筒下相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.点胶和镜筒下相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.点胶和镜筒下相机连接异常))
                {
                    CommonSet.WriteInfo("点胶和镜筒下相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.点胶和镜筒下相机连接异常);
                }
            }

            if (CommonSet.camUpD1 != null)
            {
                if (!CommonSet.camUpD1.bInit)
                {
                    if (!Alarminfo.GetAlarmList().Contains(Alarm.点胶和镜筒上相机连接异常))
                    {
                        CommonSet.WriteInfo("点胶和镜筒上相机连接异常,请检查设置和硬件连接并重启软件！");
                        Alarminfo.AddAlarm(Alarm.点胶和镜筒上相机连接异常);
                    }
                }

            }
            else
            {
                if (!Alarminfo.GetAlarmList().Contains(Alarm.点胶和镜筒上相机连接异常))
                {
                    CommonSet.WriteInfo("点胶和镜筒上相机连接异常,请检查设置和硬件连接并重启软件！");
                    Alarminfo.AddAlarm(Alarm.点胶和镜筒上相机连接异常);
                }
            }

            #endregion

        }
        public void UpdateBarrelUI(object o)
        {
            //lblGetPos.Text = CommonSet.dic_BarrelSuction[1].CurrentPos.ToString();
            //lblPutPos.Text = CommonSet.dic_BarrelSuction[2.CurrentPos.ToString();
     
            //lblLenStartPos.Text = CommonSet.dic_BarrelSuction[1].tray.StartPos.ToString();
            //lblLenEndPos.Text = CommonSet.dic_BarrelSuction[1].tray.EndPos.ToString();
            if (CommonSet.dic_BarrelSuction[1].bFinish)
            {
                lblLenFlag.BackColor = Color.Red;
            }
            else
            {
                lblLenFlag.BackColor = Color.WhiteSmoke;
            }
            if (CommonSet.dic_BarrelSuction[2].bFinish)
            {

                lblLenPutFlag.BackColor = Color.Red;
            }
            else
            {
                lblLenPutFlag.BackColor = Color.WhiteSmoke;
            }
            if (CommonSet.bForceFinish)
            {
                btnForceFinish.BackColor = Color.Green;
            }
            else
            {
                btnForceFinish.BackColor = Color.WhiteSmoke;
            }
        }
        public void UpdateSuctionUI(object o)
        {
            try {

                if (frmOpt1 != null)
                {
                    frmOpt1.updateUI();
                    //frmOpt1.updateUI();
                }
            }
            catch (Exception ex) { }
            try
            {

                if (frmOpt2 != null)
                {
                    frmOpt2.updateUI();
                    //frmOpt1.updateUI();
                }
            }
            catch (Exception ex) { }
            try
            {
                if (frmAssem1 != null)
                {
                    frmAssem1.updateUI();
                }
            }
            catch (Exception)
            {


            }
            try
            {
                if (frmAssem2 != null)
                {
                    frmAssem2.updateUI();
                }
            }
            catch (Exception)
            {


            }
            try
            {
                if (CommonSet.frmSAndTR != null)
                {
                    CommonSet.frmSAndTR.UpdateUI();
                }
            }
            catch (Exception)
            {

               
            }
            try
            {
                if (CommonSet.frmBAndTR != null)
                {
                    CommonSet.frmBAndTR.UpdateUI();
                }
            }
            catch (Exception)
            {


            }
            
            try
            {
                if (barrelUI != null)
                {
                    barrelUI.updateUI();
                }
            }
            catch (Exception)
            {


            }
            try
            {
                if (frmOther != null)
                {
                    frmOther.UpdateUI();
                }
            }
            catch (Exception)
            {


            }
        }
        public void UpdateOtherUI(object o)
        {
            try
            {
                if (CommonSet.iLevel == 1)
                {
                    toolStripRight.Text = "调试";
                    toolStripHand.Visible = true;
                    toolStripSet.Visible = true;
                   // toolStripShowDebug.Visible = true;
                    toolStripIO.Visible = true;
                    toolStripSave.Visible = true;
                }
                else if (CommonSet.iLevel == 2)
                {
                    toolStripRight.Text = "开发者";
                    toolStripHand.Visible = true;
                    toolStripSet.Visible = true;
                   // toolStripShowDebug.Visible = true;
                    toolStripIO.Visible = true;
                    toolStripSave.Visible = true;
                }
                else {
                    toolStripRight.Text = "生产";
                    toolStripHand.Visible = false;
                    toolStripSet.Visible = false;
                    //toolStripShowDebug.Visible = false;
                    toolStripIO.Visible = false;
                    toolStripSave.Visible = false;
                }

                lblAssembNum.Text = CommonSet.iAssembleNum.ToString();
                lblProductName.Text = CommonSet.strProductName;
                toolStripMeasure.Text = "工位1测高(mm):" + SerialPortMeasureHeight.dStation1.ToString("0.000")
                                        + "工位2测高(mm):" + SerialPortMeasureHeight.dStation2.ToString("0.000") + "采集间隔时间(ms):" + SerialPortMeasureHeight.lTime.ToString()
                                        + "   工位1测压(N):" + SerialPortMeasurePressure.dStation1.ToString("0.000") + "工位2测压(N):" + SerialPortMeasurePressure.dStation2.ToString("0.000") + " 采集间隔时间(ms):" + SerialPortMeasurePressure.lTime.ToString();
                lblLight.BackColor = getColor(mc.dic_DO[DO.照明灯开启]);
                lblSuctionOn.BackColor = getColor(mc.dic_DO[DO.真空泵继电器]);    
               // lblOutP.Text = Assemble1Module.iCurrentSolution.ToString();
                //lblCenterColUp1.Text = CommonSet.dic_BarrelSuction[1].dCenterUpColumn.ToString("0.000");
                //lblCenterRowUp1.Text = CommonSet.dic_BarrelSuction[1].dCenterUpRow.ToString("0.000");
                //lblAngleUp1.Text = CommonSet.dic_BarrelSuction[1].dAngle.ToString("0.0");
                //lblCenterColUp2.Text = CommonSet.dic_BarrelSuction[2].dCenterUpColumn.ToString("0.000");
                //lblCenterRowUp2.Text = CommonSet.dic_BarrelSuction[2].dCenterUpRow.ToString("0.000");
                //lblAngleUp2.Text = CommonSet.dic_BarrelSuction[2].dAngle.ToString("0.0");

                //toolStripAxis.Text = "X1:" + mc.dic_Axis[AXIS.X1轴].dPos.ToString("0.000") + " Y1:" + mc.dic_Axis[AXIS.Y1轴].dPos.ToString("0.000")
                //    + " Z1:" + mc.dic_Axis[AXIS.Z1轴].dPos.ToString("0.000") + " X2:" + mc.dic_Axis[AXIS.X2轴].dPos.ToString("0.0000")
                //     + " Y2:" + mc.dic_Axis[AXIS.Y2轴].dPos.ToString("0.0000")
                //      + " Z2:" + mc.dic_Axis[AXIS.Z2轴].dPos.ToString("0.000") + " C:" + mc.dic_Axis[AXIS.C轴].dPos.ToString("0.000");
                       
            }
            catch (Exception)
            {
                
               
            }
            try
            {
                //if (CommonSet.frmRotate != null)
                //{
                //    CommonSet.frmRotate.UpdateUI();
                //}
                //if (frmOther != null)
                //{
                //    frmOther.updateUI();
                //}

            }
            catch (Exception ex)
            {
            }
            try
            {
                ShowAlarm();
            }
            catch (Exception ex)
            {

                // throw;
            }
          
            
        }
        private Dictionary<Alarm, DateTime> dictAlarm = new Dictionary<Alarm, DateTime>();

        DateTime showAlarmTime = new DateTime();
        private void ShowAlarm()
        {
           
            List<Alarm> lstAlarmAll = Alarminfo.GetAlarmList();
            int count = lstAlarmAll.Count;
            int len = lstAlarm.Items.Count;
            if (count != len)
            {
                lstAlarm.Items.Clear();

                foreach (Alarm alarm in lstAlarmAll)
                {
                    lstAlarm.Items.Add(alarm.ToString());
                    if (dictAlarm.ContainsKey(alarm))
                    {
                        dictAlarm[alarm] = DateTime.Now;
                    }
                    else
                    {
                        txtAllAlarm.AppendText(DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + alarm.ToString() + "\r\n");
                        IniOperate.INIWriteValue(CommonSet.strAlarmFile, "Alarm", DateTime.Now.ToString("yy-MM-dd HH:mm:ss ") + DateTime.Now.Millisecond.ToString(), alarm.ToString());
                        dictAlarm.Add(alarm, DateTime.Now);
                    }
                }
            }
            int iAlarmCount = lstAlarmAll.Count;
            lblAlarmNum.Text = iAlarmCount.ToString();
           
            if (lstAlarmAll.Count > 0)
            {
                lblAlarmFlag.BackColor = Color.Red;
                lblLastAlarm.Text = lstAlarmAll[iAlarmCount - 1].ToString();
                TimeSpan ts = DateTime.Now - showAlarmTime;
                if (ts.TotalSeconds > 0.5)
                {
                    showAlarmTime = DateTime.Now;
                    if (panel1.BackColor == Color.Red)
                    {
                        panel1.BackColor = Color.Yellow;
                    }
                    else
                    {
                        panel1.BackColor = Color.Red;
                    }
                }

            }
            else
            {
                panel1.BackColor = Color.Yellow;
                lblAlarmFlag.BackColor = Color.Green;
                lblLastAlarm.Text = "空";
            }

            foreach (KeyValuePair<Alarm, DateTime> pair in dictAlarm)
            {
                TimeSpan ts = DateTime.Now - pair.Value;
                if (ts.Seconds > 3)
                {
                    dictAlarm.Remove(pair.Key);
                }
            }
        }
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Yellow;

        }
        #endregion

       
        private void ShowMsg(string info)
        {
            _context.Post(ShowText, info);
        }
        private void ShowText(object o)
        {
            txtLog.AppendText(o.ToString());
        }
        //禁止使用快捷方式切换Tabpage
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            { 
                  
                case(Keys.Tab|Keys.Control):
                    return true;
                case(Keys.Left):
                    return true;
                case(Keys.Right):
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void toolStripMain_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageMain;
        }

        private void toolStripSet_Click(object sender, EventArgs e)
        {
            tabParamSet.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.SelectedTab = tabPageSet;
        }
        //在选项卡中生成窗体  
        public void GenerateForm(Form fm, object sender)
        {
            // 反射生成窗体  
            // Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);

            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;

            // s[((TabControl)sender).SelectedIndex] = 1;

        }
        bool[] bLoadFlag = new bool[] { false, false, false, false, false, false, false };//窗体导入标志
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) { 
                case 0:
                    //InitTray();
                    break;
                case 1:
                     // iSelectParamSetIndex = 0;
                    if (!bParamSetUI[0])
                    {
                        //cs = new ControlSuction(1);
                        //cs.Parent = tabParamSet.SelectedTab;
                        //cs.Dock = DockStyle.Fill;
                        //cs.Show();
                        bParamSetUI[0] = true;
                        //dic_SuctionUI.Add(iSelectSuctionTabIndex, cs);
                       // bShowSuctionUI[iSelectSuctionTabIndex] = true;
                    }
                    break;
                case 2:
                    if (!bLoadFlag[2])
                    {
                        TestTray FrmTray = new TestTray();
                        GenerateForm(FrmTray, sender);
                        FrmTray.Show();
                        FrmTray.Dock = DockStyle.Fill;
                        bLoadFlag[2] = true;
                    }

                    break;
            }

        }
       
        private void toolStripOther_Click(object sender, EventArgs e)
        {
          
            //tabControl1.SelectedTab = tabPageTraySet;
        }
        FrmSolutionSet frmSolution = null;
        private void button1_Click(object sender, EventArgs e)
        {
            frmSolution = new FrmSolutionSet();
            frmSolution.ShowDialog();
            frmSolution = null;
            //trayPanel.setStartPos((int)numericUpDown1.Value, Color.Green,Color.Gray);
        }

        FrmStatus frmStatus = null;
        private void toolStripIO_Click(object sender, EventArgs e)
        {
            if (frmStatus == null)
            {
                frmStatus = new FrmStatus();
            }
            frmStatus.Show();
            frmStatus.Activate();
        }

        private void tabParamSet_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fntTab;
            Brush bshBack;
            Brush bshFore;
            if (e.Index == this.tabParamSet.SelectedIndex)
            {
                fntTab = new Font(e.Font, FontStyle.Bold);
                //bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.Control, SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshBack = new SolidBrush(Color.SkyBlue);
                bshFore = Brushes.Black;
            }
            else
            {
                fntTab = e.Font;
                bshBack = new SolidBrush(Color.LightSteelBlue);
                bshFore = new SolidBrush(Color.Black);
            }
            string tabName = this.tabParamSet.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat();
            sftTab.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(bshBack, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X, recTab.Y + 10, recTab.Width, recTab.Height - 4);
            e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出前是否保存参数", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                CommonSet.SaveParam();
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
            {

                e.Cancel = true;
                return;
            }
           
            //DialogResult dr = MessageBox.Show("是否退出程序?", "提示信息", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            //if (dr == DialogResult.No)
            //{
            //    e.Cancel = true;
            //    return;
            //}
            timeEndPeriod(1);
            try
            {
                mc.StopAllAxis();
                mc.releaseCard();

                closeCamera();
                th_UpdateUI.Abort();
                th_UpdateUI = null;

                CommonSet.spMH.Close();
                CommonSet.spPre.Close();
            }
            catch (Exception ex)
            {
            }
            System.Environment.Exit(0);

        }
        public void closeCamera()
        {
            CommonSet.camDownA1.CloseCamera();
            CommonSet.camUpA1.CloseCamera();
            CommonSet.camDownA2.CloseCamera();
            CommonSet.camUpA2.CloseCamera();

            CommonSet.camDownC1.CloseCamera();
            CommonSet.camUpC1.CloseCamera();
            CommonSet.camDownC2.CloseCamera();
            CommonSet.camUpC2.CloseCamera();

            CommonSet.camDownD1.CloseCamera();
            CommonSet.camUpD1.CloseCamera();

            CommonSet.camDownA1.Release();
            CommonSet.camUpA1.Release();
            CommonSet.camDownA2.Release();
            CommonSet.camUpA2.Release();

            CommonSet.camDownC1.Release();
            CommonSet.camUpC1.Release();
            CommonSet.camDownC2.Release();
            CommonSet.camUpC2.Release();

            CommonSet.camDownD1.Release();
            CommonSet.camUpD1.Release();
        }
        private void toolStripExit_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
       // FrmProcess frmProcess = null;
        FrmCameraAndLight frmCamera = null;
        public static FrmDebug frmDebug = null;
        private void toolStripCamera_Click(object sender, EventArgs e)
        {
            if((Run.runMode == RunMode.运行) || (Run.runMode == RunMode.暂停))
            {
                return;
            }
            frmDebug = new FrmDebug();
            frmDebug.ShowDialog();
            frmDebug = null;
            //CommonSet.camUp.RemoveDelegate();
            //CommonSet.camDown.RemoveDelegate();
            //if (frmCamera == null)
            //{
            //    frmCamera = new FrmCameraAndLight();
            //}
            //frmCamera.MdiParent = this;
            //frmCamera.WindowState = FormWindowState.Maximized;
           // toolStripCalib_Click(null,null);
            //frmCamera.ShowDialog();
            //frmCamera.Close();
            //frmCamera = null;
            //CommonSet.camUpA1.RemoveDelegate();
            //CommonSet.camUpA1.ProcessImage += ProcessImageUp1;
            //CommonSet.camUpA1.InitCamera(CameraName.CamUpA1.ToString());

            //CommonSet.camDown1.RemoveDelegate();
            //CommonSet.camDown1.ProcessImage += ProcessImageDown1;
            //CommonSet.camDown1.InitCamera(CameraName.CameraDown1.ToString());

            //CommonSet.camUp2.RemoveDelegate();
            //CommonSet.camUp2.ProcessImage += ProcessImageUp2;
            //CommonSet.camUp2.InitCamera(CameraName.CameraUp2.ToString());

            //CommonSet.camDown2.RemoveDelegate();
            //CommonSet.camDown2.ProcessImage += ProcessImageDown2;
            //CommonSet.camDown2.InitCamera(CameraName.CameraDown2.ToString());
            //initLight();

            //frmProcess = null;
            //if (frmProcess == null)
            //{
            //    frmProcess = new FrmProcess();
            //}
            //frmProcess.Show();
            

        }
        FrmHand frmhand = null;
        private void toolStripCalib_Click(object sender, EventArgs e)
        {
            if (frmhand == null)
            {
                frmhand = new FrmHand();
            }
           
            frmhand.Show();
            frmhand.Activate();
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (CommonSet.SaveParam())
            {
                MessageBox.Show("保存参数成功！");
            }
            else {
                MessageBox.Show("保存参数失败！");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            
            if (Alarminfo.BAlarm)
                return;

            if (Run.runMode == RunMode.手动)
            {
                if (CommonSet.iLevel < 2)
                    CommonSet.iLevel = 0;
                Run.runMode = RunMode.暂停;
                Run.ResetStatus();
                CommonSet.bResetOne = true;
               Run.bReset = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
            {
                return;
            }
            if (Run.runMode == RunMode.暂停)
            {
                if (!Alarminfo.BAlarm)
                    return;
            }
            Run.ResetStatus();
            Run.bReset = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.runMode = RunMode.暂停;
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            //if (Run.productModule1.IsBarrelEmpty() && (Run.productModule1.IStep == 0) && (Run.productModule2.IStep == 0) && (Run.assembleModule1.IStep == 0) && (Run.assembleModule2.IStep == 0) && (Run.flashModule1.IStep == 0) && (Run.flashModule2.IStep == 0))
            //{
            //    MessageBox.Show("料已空，请先换料！");
            //    return;
            //}
            if (Run.bReset)
            {
                MessageBox.Show("正在复位中，请稍候!");
                return;
            }

            Run.runMode = RunMode.运行;
            //CommonSet.swCircleA1.Start();
            //CommonSet.swCircleA2.Start();
            //CommonSet.swCircleC1.Start();
            //CommonSet.swCircleC2.Start();
            //CommonSet.swCircleD.Start();
        }

        private void btnStepMove_Click(object sender, EventArgs e)
        {
            Run.bRunStep = !Run.bRunStep;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Run.bNext = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (Run.runMode != RunMode.手动)
                return;
            if (Run.bHome)
                return;
            mc.setDO(DO.点胶镜筒吸笔下降, false);
            mc.setDO(DO.点胶成品镜头气缸下降, false);
            mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, false);
            Run.swWaitSuction.Restart();
            CommonSet.WriteInfo("开始回原点");
            //添加轴
            string[] values = Enum.GetNames(typeof(AXIS));
            foreach (string value in values)
            {
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                if(mc.dic_HomeStatus.Keys.Contains(axis))
                         mc.dic_HomeStatus[axis] = false;
                if (!mc.dic_Axis[axis].SVON)
                {
                    mc.SeverOnAll();
                }
            }
            List<Alarm> lst = new List<Alarm>();
            lst.AddRange(Alarminfo.GetAlarmList());
            for (int i = 0; i < 6; i++)
            {
                lst.Remove(Alarm.取料X1轴原点丢失 + i);
                //lst.Remove(Alarm.Z1轴原点丢失 + i);
            }
            lst.Remove(Alarm.点胶C轴原点丢失);
            lst.Remove(Alarm.点胶Y轴原点丢失);
            if (lst.Count > 0)
            {
                Run.bHome = false;
                MessageBox.Show("有其它异常报警，请检查并复位清除！");
                return;
            }
            mc.bHome = true;
            Run.bHome = true;
            mc.HomeAbs(AXIS.组装Z1轴);
            mc.HomeAbs(AXIS.组装Z2轴);
            mc.HomeAbs(AXIS.取料Z1轴);
            mc.HomeAbs(AXIS.取料Z2轴);
            mc.HomeAbs(AXIS.点胶Z轴);
           // mc.Home(AXIS.取料X1轴);
           // mc.HomeAbs(AXIS.取料Y1轴);
           // mc.Home(AXIS.取料X2轴);
           // //mc.setDO(DO.C轴夹子打开, false);
            
           // //mc.Home(AXIS.Z1轴);
           // //mc.Home(AXIS.Z2轴);
           //// mc.Home(AXIS.镜筒Z3轴);
        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.手动)
                return;
            DialogResult dr = MessageBox.Show("是否回手动状态？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Run.runMode = RunMode.手动;
                //CommonSet.swCircleTime.Stop();
                //CommonSet.swCircleTime.Reset();
                //CommonSet.bResetOne = false;
                //Run.productModule1.bResetFinish = false;
                //Run.productModule2.bResetFinish = false;
                //Run.assembleModule1.bResetFinish = false;
                //Run.assembleModule2.bResetFinish = false;
                //Run.flashModule1.bResetFinish = false;
                //Run.flashModule2.bResetFinish = false;
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            //tabControlMain.SelectedTab = tabMainAlarm;
        }

        private void lblGetSuctionFlag_Click(object sender, EventArgs e)
        {
            if ((lblGetSuctionFlag1.BackColor == Color.Red))
            {
                lblGetSuctionFlag1.BackColor = Color.Gainsboro;
                lblGetSuctionFlag2.BackColor = Color.Gainsboro;
                foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                {
                    if (pair.Value.bFinish)
                    {
                        pair.Value.bFinish = false;
                        // pair.Value.tray.StartPos = pair.Value.tray.
                        pair.Value.StartPos = 1;
                        pair.Value.CurrentPos = pair.Value.StartPos;
                        pair.Value.EndPos = pair.Value.GetCount();
                    }
                }
                foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                {
                    if (pair.Value.bFinish)
                    {
                        pair.Value.bFinish = false;
                        // pair.Value.tray.StartPos = pair.Value.tray.
                        pair.Value.StartPos = 1;
                        pair.Value.CurrentPos = pair.Value.StartPos;
                        pair.Value.EndPos = pair.Value.GetCount();
                    }
                }
               
            }
        }

        private void lblGetPos_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog("镜筒取料位");
            fsd.ShowDialog();
            fsd = null;
        }

        private void lblLenFlag_Click(object sender, EventArgs e)
        {
            if (lblLenFlag.BackColor == Color.Red)
            {
                if (CommonSet.dic_BarrelSuction[1].bFinish)
                {
                    CommonSet.dic_BarrelSuction[1].bFinish = false;
                    CommonSet.dic_BarrelSuction[1].StartPos = 1;
                    CommonSet.dic_BarrelSuction[1].CurrentPos = CommonSet.dic_BarrelSuction[1].StartPos;
                    CommonSet.dic_BarrelSuction[1].EndPos = CommonSet.dic_BarrelSuction[1].GetCount();
                }
                
            }
          
        }

        private void lblLenStartPos_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog("镜筒起始位");
            fsd.ShowDialog();
            fsd = null;
        }

        private void lblLenEndPos_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
            Label l = (Label)sender;
            string name = l.Name;
            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog("镜筒结束位");
            fsd.ShowDialog();
            fsd = null;
        }

        private void lblLenPutFlag_Click(object sender, EventArgs e)
        {
            if (lblLenPutFlag.BackColor == Color.Red)
            {
                if (CommonSet.dic_BarrelSuction[2].bFinish)
                {
                    CommonSet.dic_BarrelSuction[2].bFinish = false;
                    CommonSet.dic_BarrelSuction[2].StartPos = 1;
                    CommonSet.dic_BarrelSuction[2].CurrentPos = CommonSet.dic_BarrelSuction[2].StartPos;
                    CommonSet.dic_BarrelSuction[2].EndPos = CommonSet.dic_BarrelSuction[2].GetCount();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CommonSet.iAssembleNum = 0;
        }

        private void toolStripProduce_Click(object sender, EventArgs e)
        {
            CommonSet.iLevel = 0;
        }

        private void toolStripDebug_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
            {
                MessageBox.Show("请先切换到手动状态！");
                return;
            }
            if (Run.runMode == RunMode.暂停)
            {
                MessageBox.Show("请先切换到手动状态！");
                return;
            }
            if (CommonSet.iLevel == 0)
            {
                if (!login("11111"))
                {
                    MessageBox.Show("密码错误,登录失败!");
                    return;
                }
            }
            CommonSet.iLevel = 1;
        }

        private void toolStripSuper_Click(object sender, EventArgs e)
        {
            if (CommonSet.iLevel != 2)
            {
                if (!login("admin"))
                {
                    MessageBox.Show("密码错误,登录失败!");
                    return;
                }
            }
            CommonSet.iLevel = 2;
        }

        public bool login(string pwd)
        {
            FrmPasswordDialog frmDlg = new FrmPasswordDialog();
            frmDlg.ShowDialog();

            if (frmDlg.strPassword.Equals(pwd))
                return true;
            else
                return false;
        }

        private void btnY1Put1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01))
            {
                MessageBox.Show("取料Z1轴低于安全高度!");
                return;
            }
           
            mc.AbsMove(AXIS.取料Y1轴, OptSution1.pSafeXYZ.Y, 100);
            if (lblGetSuctionFlag1.BackColor == Color.Red)
            {
                lblGetSuctionFlag1.BackColor = Color.Gainsboro;
                foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                {
                    if (pair.Value.bFinish)
                    {
                        pair.Value.bFinish = false;
                        // pair.Value.tray.StartPos = pair.Value.tray.
                        pair.Value.StartPos = 1;
                        pair.Value.CurrentPos = pair.Value.StartPos;
                        pair.Value.EndPos = pair.Value.GetCount();
                    }
                }
               

            }

           
        }

        private void btnY1Put2_Click(object sender, EventArgs e)
        {
          
            if (mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01))
            {
                MessageBox.Show("取料Z2轴低于安全高度!");
                return;
            }
            mc.AbsMove(AXIS.取料Y2轴, OptSution2.pSafeXYZ.Y, 100);
            if (lblGetSuctionFlag2.BackColor == Color.Red)
            {
                lblGetSuctionFlag2.BackColor = Color.Gainsboro;
              
                foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                {
                    if (pair.Value.bFinish)
                    {
                        pair.Value.bFinish = false;
                        // pair.Value.tray.StartPos = pair.Value.tray.
                        pair.Value.StartPos = 1;
                        pair.Value.CurrentPos = pair.Value.StartPos;
                        pair.Value.EndPos = pair.Value.GetCount();
                    }
                }

            }

        }

        private void lblGetSuctionFlag2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripShowWindow_Click(object sender, EventArgs e)
        {
            frmShowImage.Show();
        }

        private void lblLight_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.照明灯开启, !mc.dic_DO[DO.照明灯开启]);
        }

        private void lblSuctionOn_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.真空泵继电器, !mc.dic_DO[DO.真空泵继电器]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Assem1Module.iPicSum = 0;
            foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
            {
                if (pair.Value.BUse)
                {
                    string strProcessName = CommonSet.dic_Assemble1[pair.Key].strPicDownProduct;
                    CommonSet.dic_Assemble1[pair.Key].InitImageDown(strProcessName);
                   
                }
            }
            Assem1Module.iPicNum = 1;
            HObject image;
            HOperatorSet.ReadImage(out image, "E:\\1.bmp");
            ShowImage.test(image);

            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
            {
                if (pair.Value.BUse)
                {
                    string strProcessName = CommonSet.dic_Assemble2[pair.Key].strPicDownProduct;
                    CommonSet.dic_Assemble2[pair.Key].InitImageDown(strProcessName);

                }
            }
            Assem1Module.iPicNum = 1;
            ShowImage.test2(image);

            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
            {
                if (pair.Value.BUse)
                {
                    string strProcessName = CommonSet.dic_OptSuction1[pair.Key].strPicDownProduct;
                    CommonSet.dic_OptSuction1[pair.Key].InitImageDown(strProcessName);

                }
            }
            Assem1Module.iPicNum = 1;
            ShowImage.testA1(image);
        }

        private void btnForceFinish_Click(object sender, EventArgs e)
        {
            CommonSet.bForceFinish = true;
        }


        /*
private void btnStartSet_Click(object sender, EventArgs e)
{
int endPos = Convert.ToInt32(lblEndPos.Text);
tray.setStartEndPos((int)nudStartPos.Value, endPos, CommonSet.ColorInit, CommonSet.ColorNotUse);
tray.updateColor();
lblStartPos.Text = tray.StartPos.ToString();
nudCurrentPos.Minimum = Convert.ToInt32(lblStartPos.Text);
if (Convert.ToInt32(lblCurrentPos.Text) < Convert.ToInt32(lblStartPos.Text))
{
nudCurrentPos.Value = Convert.ToInt32(lblStartPos.Text);
btnCurrentSet_Click(this, null);
}
}

private void btnEndSet_Click(object sender, EventArgs e)
{
int startPos = Convert.ToInt32(lblStartPos.Text);
tray.setStartEndPos(startPos, (int)nudEndPos.Value, CommonSet.ColorInit, CommonSet.ColorNotUse);
tray.updateColor();
lblEndPos.Text = tray.EndPos.ToString();
nudCurrentPos.Maximum = Convert.ToInt32(lblEndPos.Text);
}

private void btnCurrentSet_Click(object sender, EventArgs e)
{
tray.CurrentPos = (int)nudCurrentPos.Value;
lblCurrentPos.Text = tray.CurrentPos.ToString();
//tray.setNumColor((int)nudCurrentPos.Value, CommonSet.ColorOK);
//tray.updateColor();
}

private void button1_Click_1(object sender, EventArgs e)
{
tray.initTrayValue(CommonSet.ColorInit);
tray.updateColor();
}    
* */
    }
}
