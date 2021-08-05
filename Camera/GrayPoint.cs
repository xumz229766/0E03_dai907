using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Threading;
using FlyCapture2Managed;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Windows.Forms;
//using FlyCapture2Managed.Gui;
using System.ComponentModel;
using System.Diagnostics;
using ConfigureFile;
namespace CameraSet
{
    public class GrayPoint:ICamera
    {
        Thread renderThread = null;         /* 显示线程  */
        bool bGrabImage = false;            /* 线程控制变量 */
        Mutex m_mutex = new Mutex();        /* 锁，保证多线程安全 */
        public HObject hImg;
        //ManagedCamera camera =null;
        ManagedGigECamera camera = null;
        ManagedImage m_rawImage = null;
        static ManagedBusManager busMgr = null;
        bool bContinous = false;
        bool m_grabImages = false;
        private AutoResetEvent m_grabThreadExited;
        private BackgroundWorker m_grabThread;

        public GrayPoint()
        {
            if (null == renderThread)
            {
                renderThread = new Thread(new ThreadStart(ShowThread));
                renderThread.Start();
            }
            //m_grabThreadExited = new AutoResetEvent(false);
        }
        public override void RemoveDelegate()
        {
            try
            {
                if (ProcessImage != null)
                {
                    Delegate[] list = ProcessImage.GetInvocationList();
                    foreach (Delegate d in list)
                    {
                        ProcessImage -= d as Action<HObject>;
                    }
                }
            }
            catch (Exception ex)
            {
                string info = ex.ToString();
            }
        }

        public override bool InitCamera(string DeviceName)
        {
            try
            {

            
                bInit = false;
                if (camera != null)
                {
                    CloseCamera();
                }
                if(busMgr == null)
                 busMgr = new ManagedBusManager();
                uint num = busMgr.GetNumOfCameras();
                camera = new ManagedGigECamera();
                // CameraName name = (CameraName)Enum.Parse(typeof(CameraName), DeviceName);
                //IPAddress ip = IPAddress.Parse(dic_CameraInfo[name].ip_address.Trim());
                string file = System.Windows.Forms.Application.StartupPath + "\\CameraConfig.ini";
                string ipAddress = IniOperate.INIGetStringValue(file, "IP", DeviceName, "");
                IPAddress ip = IPAddress.Parse(ipAddress.Trim());

                ManagedPGRGuid guid = busMgr.GetCameraFromIPAddress(ip);
                
                camera.Connect(guid);
                SetTriggerMode(true);
                SetExposure(dExposure);
                //SetGain(dGain);
                ShowInfo(DeviceName + "初始化相机成功");
                strDeviceName = DeviceName;
                camera.StartCapture();
                bGrabImage = false;
                //m_grabImages = true;
                //StartGrabLoop();
                dExposure = 0;
                bInit = true;
            }
            catch (Exception ex)
            {

                ShowInfo(DeviceName + "打开相机异常" + ex.ToString());
                CloseCamera();
            }
            return false;
        }
        private void StartGrabLoop()
        {
            m_grabThread = new BackgroundWorker();
            //m_grabThread.ProgressChanged += new ProgressChangedEventHandler(UpdateUI);
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.WorkerReportsProgress = true;
            m_grabThread.RunWorkerAsync();
        }
        private void ShowThread()
        {
            while (true)
            {
                try
                {
                    if (!bInit)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    try
                    {
                        //camera.StartCapture();
                        m_rawImage = new ManagedImage();
                     
                        camera.RetrieveBuffer(m_rawImage);
                        
                        //ShowInfo(strDeviceName+"抓取图像成功! ");
                    }
                    catch (FC2Exception ex)
                    {
                        ShowInfo(strDeviceName+"抓取图像错误: " + ex.Message);
                        Thread.Sleep(10);
                        continue;
                    }

                    ManagedImage m_processedImage = new ManagedImage();
                     m_rawImage.Convert(FlyCapture2Managed.PixelFormat.PixelFormatMono8, m_processedImage);
                     Bitmap bitmap = m_processedImage.bitmap;

                     BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                     // Place the pointer to the buffer of the bitmap.

                     IntPtr ptrBmp = bmpData.Scan0;
                     //converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                     //HObject hImg;
                     if (hImg == null)
                         HOperatorSet.GenEmptyObj(out hImg);
                     //hImg.Dispose();
                     //HOperatorSet.GenImageInterleaved(out hImg, ptrBmp, "bgr", bitmap.Width, bitmap.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                    HOperatorSet.GenImage1(out hImg, "byte", bitmap.Width, bitmap.Height, ptrBmp);
                     bitmap.UnlockBits(bmpData);
                     ProcessImage(hImg);
                     //  hImg.Dispose();
                     hImg = null;
                     bitmap.Dispose();
                     m_processedImage = null;
                    m_rawImage = null;
                   
                }
                catch (Exception ex)
                {

                    ShowInfo("图像转换异常: " + ex.Message);
                }
                //if(!bContinous)
                //bGrabImage = false;
                Thread.Sleep(10);
            }
        }
        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (m_grabImages)
            {
                try
                {
                    //if (!bGrabImage)
                    //{
                    //    Thread.Sleep(10);
                    //    continue;
                    //}
                    try
                    {
                        //camera.StartCapture();
                        m_rawImage = new ManagedImage();
                        Thread.Sleep(50);
                        camera.RetrieveBuffer(m_rawImage);

                        //ShowInfo(strDeviceName + "抓取图像成功! ");
                    }
                    catch (FC2Exception ex)
                    {
                        ShowInfo("Error: " + ex.Message);
                        continue;
                    }

                    ManagedImage m_processedImage = new ManagedImage();
                    m_rawImage.Convert(FlyCapture2Managed.PixelFormat.PixelFormatBgr, m_processedImage);
                    Bitmap bitmap = m_processedImage.bitmap;

                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.

                    IntPtr ptrBmp = bmpData.Scan0;
                    //converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                    //HObject hImg;
                    if (hImg == null)
                        HOperatorSet.GenEmptyObj(out hImg);
                    //hImg.Dispose();
                    HOperatorSet.GenImageInterleaved(out hImg, ptrBmp, "bgr", bitmap.Width, bitmap.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                    //HOperatorSet.GenImage1(out hImg, "byte", bitmap.Width, bitmap.Height, ptrBmp);
                    bitmap.UnlockBits(bmpData);
                    ProcessImage(hImg);
                    //  hImg.Dispose();
                    hImg = null;
                    bitmap.Dispose();
                    m_processedImage = null;
                    m_rawImage = null;

                }
                catch (Exception ex)
                {

                    ShowInfo("图像转换异常: " + ex.Message);
                }

                worker.ReportProgress(0);
            }

            m_grabThreadExited.Set();
        }

        public override void SetExposure(double value)
        {
            //if (dExposure == value)
            //    return;
            CameraProperty p = new CameraProperty(PropertyType.Shutter);
            p.absControl = true;
            //p.onOff = true;
            p.autoManualMode = false;
            p.absValue = (float)(value/1000);
            //dExposure = value;
            camera.SetProperty(p);
        }

        public override void GetMinMaxExposure(ref double minExposure, ref double maxExposure)
        {
          
        }

        public override void SetGain(double value)
        {
            CameraProperty p = new CameraProperty(PropertyType.Gain);
            p.absValue = (float)value;
        
          //  p.autoManualMode
            camera.SetProperty(p);
        }

        public override void GetMinMaxGain(ref double minGain, ref double maxGain)
        {
           
        }

        public override void SoftTrigger()
        {
            camera.FireSoftwareTrigger(false);
            
            bGrabImage = true;
        }

        public override void SetTriggerMode(bool _bTrigger)
        {
            this.bTrigger = _bTrigger;
            //bContinous = bTrigger;
            TriggerMode mode = new TriggerMode();
            mode.onOff = _bTrigger;
            camera.SetTriggerMode(mode);
        }

        public override void CloseCamera()
        {
            try
            {
                bGrabImage = false;
                if (camera == null)
                {
                    return;
                }

                camera.StopCapture();
               
              
                /* 关闭相机 */
            }
            catch (Exception exception)
            {

            }
            camera = null;
            bInit = false;
        }

        public override void Release()
        {
            try
            {
               
                if (renderThread.IsAlive)
                {
                    renderThread.Abort();
                }

            }
            catch (Exception)
            {


            }
            renderThread = null;
        }
    }
}
