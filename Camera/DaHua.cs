using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThridLibray;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using HalconDotNet;
namespace CameraSet
{
    public class DaHua:ICamera
    {
       
        public IDevice m_dev;
        //List<IGrabbedRawData> m_frameList = new List<IGrabbedRawData>();        /* 图像缓存列表 */
        Queue<IGrabbedRawData> m_frameList = new Queue<IGrabbedRawData>();
        Thread renderThread = null;         /* 显示线程  */
        bool m_bShowLoop = true;            /* 线程控制变量 */
        Mutex m_mutex = new Mutex();        /* 锁，保证多线程安全 */
        public HObject hImg;
        string strDeviceName = "";

        public DaHua()
        {
            if (null == renderThread)
            {
                renderThread = new Thread(new ThreadStart(ShowThread));
                renderThread.Start();
            }
    
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
                if (m_dev != null)
                {
                    CloseCamera();
                }

                m_dev = Enumerator.GetDeviceByUserID(DeviceName);
                /* 注册链接时间 */
                m_dev.CameraOpened += m_dev_CameraOpened;
                m_dev.ConnectionLost += m_dev_ConnectionLost;
                m_dev.CameraClosed += Camera_CameraClosed;
                
                /* 打开设备 */
                if (!m_dev.Open())
                {
                   
                    return false;
                }

                SetTriggerMode(true);

                /* 设置缓存个数为8（默认值为16） */
                m_dev.StreamGrabber.SetBufferCount(12);

                /* 注册码流回调事件 */
                m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                /* 开启码流 */
                if (!m_dev.GrabUsingGrabLoopThread())
                {
                    ShowInfo(DeviceName+"初始化相机失败");
                }
                ShowInfo(DeviceName + "初始化相机成功");
                strDeviceName = DeviceName;
                bInit = true;
            }
            catch (Exception ex)
            {
                ShowInfo(DeviceName + "打开相机异常"+ex.ToString());
                CloseCamera();
            }
          
            return false;
        }

        void m_dev_ConnectionLost(object sender, EventArgs e)
        {
            ShowInfo(strDeviceName + "相机连接断开!");
            bInit = false;
        }
        private void ShowThread()
        {
            while (true)
            {
               
                if (m_frameList.Count == 0)
                {
                   // ShowInfo(strDeviceName + "等待获取图像！");
                    Thread.Sleep(20);
                    continue;
                }
              
                /* 图像队列取最新帧 */
                m_mutex.WaitOne();
               // IGrabbedRawData frame = m_frameList.ElementAt(m_frameList.Count - 1);
               // m_frameList.Clear();
                IGrabbedRawData frame = m_frameList.Dequeue(); 
                m_mutex.ReleaseMutex();
              
                /* 主动调用回收垃圾 */
                GC.Collect();

                /* 控制显示最高帧率为25FPS */
                //if (false == isTimeToDisplay())
                //{
                //    continue;
                //}

                try
                {
                   
                    /* 图像转码成bitmap图像 */
                    Bitmap bitmap = frame.ToBitmap(false);

                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    
                    IntPtr ptrBmp = bmpData.Scan0;
                    //converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                    //HObject hImg;
                    if (hImg == null)
                        HOperatorSet.GenEmptyObj(out hImg);
                    //hImg.Dispose();
                    //HOperatorSet.GenImageInterleaved(out hImg, ptrBmp, "rgb", bitmap.Width, bitmap.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                     HOperatorSet.GenImage1(out hImg, "byte", bitmap.Width, bitmap.Height, ptrBmp);
                    bitmap.UnlockBits(bmpData);
                    ProcessImage(hImg);
                    //  hImg.Dispose();
                     hImg = null;
                    bitmap.Dispose();
                      
                        //_g.DrawImage(bitmap, new Rectangle(0, 0, pbImage.Width, pbImage.Height),
                        //new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                        //bitmap.Dispose();
                    
                }
                catch (Exception exception)
                {
                   // Catcher.Show(exception);
                }
            }
        }
        private void OnImageGrabbed(Object sender, GrabbedEventArgs e)
        {
            try
            {

          
                m_mutex.WaitOne();
                //m_frameList.Add(e.GrabResult.Clone());
                m_frameList.Enqueue(e.GrabResult.Clone());
                m_mutex.ReleaseMutex();

                ShowInfo(strDeviceName + "获取图像成功！");
            }
            catch (Exception ex)
            {
                ShowInfo(strDeviceName + "获取图像异常！");
                 
            }
        }
        private void Camera_CameraOpened(object sender, EventArgs e)
        {
            
        }

        private void Camera_CameraClosed(object sender, EventArgs e)
        {
           
        }

        void m_dev_CameraOpened(object sender, EventArgs e)
        {
            
        }

        public override void SetExposure(double value)
        {
         

            using (IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ExposureTime])
            {
                p.SetValue(value);
            }
        }

        public override void GetMinMaxExposure(ref double minExposure, ref double maxExposure)
        {
            minExposure = 36;
            maxExposure = 1000000;
        }

        public override void SetGain(double value)
        {
            using (IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.GainRaw])
            {
                p.SetValue(value);
            }
        }

        public override void GetMinMaxGain(ref double minGain, ref double maxGain)
        {
            minGain = 1;
            maxGain = 32;
        }

        public override void SoftTrigger()
        {
            bTrigger = true;
            m_dev.TriggerSet.Open(TriggerSourceEnum.Software);
           
            m_dev.ExecuteSoftwareTrigger();
            m_dev.TriggerSet.Open(TriggerSourceEnum.Line1);
        }

        public override void SetTriggerMode(bool bTrigger)
        {
            if (bTrigger)
            {
                bTrigger = true;
                m_dev.TriggerSet.Open(TriggerSourceEnum.Line1);
            }
            else
            {
                bTrigger = false;
                m_dev.TriggerSet.Close();
            }
        }

        public override void CloseCamera()
        {
            try
            {
                if (m_dev == null)
                {
                    return;
                }

                m_dev.StreamGrabber.ImageGrabbed -= OnImageGrabbed;         /* 反注册回调 */
                m_dev.ShutdownGrab();                                       /* 停止码流 */
                m_dev.Close();
                bInit = false;
                /* 关闭相机 */
            }
            catch (Exception exception)
            {
              
            }
        }
        public override void Release()
        {
            try
            {
                m_bShowLoop = false;
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
