using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Threading;
namespace CameraSet
{
    public class OPT:ICamera
    {
        HTuple hv_AcqHandle = null;
        HObject ho_Image = null;
        Thread th = null;
        bool bOpen = false;//相机初始化标志
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
                if (!bOpen)
                {
                    HOperatorSet.GenEmptyObj(out ho_Image);
                    // this.cameraName = DeviceName;
                    strDeviceName = DeviceName.ToString();
                    HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", -1,
                    "default", -1, "false", "default", DeviceName.ToString(), 0, -1, out hv_AcqHandle);
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "GainAuto", "Off");
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureAuto", "Off");
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "AcquisitionMode", "Continuous");
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "AcquisitionFrameCount", 1);
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "AcquisitionFrameRateEnable", 1);
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "AcquisitionFrameRateAbs", 1000000);
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "grab_timeout", 1000);
                    HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerSelector", "AcquisitionStart");
                    // HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "TriggerSelector", "AcquisitionStart");
                    HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
                    th = new Thread(grabImage);
                    th.Start();
                    bOpen = true;
                }
                bTrigger = true;
                SetTriggerMode(bTrigger);
                SetExposure(dExposure);
                SetGain(dGain);
            }
            catch (Exception ex)
            {
                CloseCamera();
                return false;
            }

            return true;
        }
        private void grabImage()
        {
            while (true)
            {
                // (dev_)set_check ("~give_error")
                try
                {

                    ho_Image.Dispose();
                    HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);

                    if (ProcessImage != null)
                        ProcessImage(ho_Image);


                }
                catch (HalconException e)
                {
                    int error = e.GetErrorCode();
                    //if (error < 0)
                    //    throw e;
                }
                System.Threading.Thread.Sleep(10);
                // (dev_)set_check ("give_error")
                //Image Acquisition 01: Do something
            }

        }
        public override void SetExposure(double value)
        {
           
        }

        public override void GetMinMaxExposure(ref double minExposure, ref double maxExposure)
        {
           
        }

        public override void SetGain(double value)
        {
           
        }

        public override void GetMinMaxGain(ref double minGain, ref double maxGain)
        {
           
        }

        public override void SoftTrigger()
        {
          
        }

        public override void SetTriggerMode(bool bTrigger)
        {
           
        }

        public override void CloseCamera()
        {
           
        }
        public override void Release()
        {
           
        }
    }
}
