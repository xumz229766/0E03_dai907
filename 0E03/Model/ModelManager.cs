using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraSet0;
using ImageProcess0;
namespace _0E030
{
    /// <summary>
    /// 机台数据管理类
    /// </summary>
    public class ModelManager
    {
        public static string strProductName = "Test";//产品名称

        
        /// <summary>
        /// 左吸笔参数
        /// </summary>
        public static SuctionL SuctionLParam { get; set; }
      
        /// <summary>
        /// 右吸笔参数
        /// </summary>
        public static SuctionR SuctionRParam{ get; set; }
  
        /// <summary>
        /// 镜筒参数
        /// </summary>
        public static Barrel BarrelParam{ get; set; }
      
        /// <summary>
        /// 图像处理
        /// </summary>
        public static ImageProcessManager ImageManager { get; set; }

        /// <summary>
        /// 相机参数
        /// </summary>
        public static CameraManager CamManager{get; set;}
        public static ICamera CamUpL = null;
        public static ICamera CamDownL = null;
        public static ICamera CamUpR = null;
        public static ICamera CamDownR = null;
        public static ICamera CamBarrel = null;

        //轴运行速度
        public static VelAxis VelAssemX= new VelAxis();
        public static VelAxis VelGetX = new VelAxis();
        public static VelAxis VelGetY = new VelAxis();
        public static VelAxis VelZ = new VelAxis();
        public static VelAxis VelC = new VelAxis();
        public static VelAxis VelBarrelX = new VelAxis();
        public static VelAxis VelBarrelY = new VelAxis();

        /// <summary>
        /// 
        /// </summary>
        public static AssemParam _AssemParam { get; set; }
        public static void InitParam()
        {
            _AssemParam = new AssemParam();
            _AssemParam.InitParam();
            SuctionLParam = new SuctionL();
            SuctionLParam.InitParam();
            SuctionRParam = new SuctionR();
            SuctionRParam.InitParam();
            BarrelParam = new Barrel();
            BarrelParam.InitParam();
            CalibrationL.InitStaticParam();
            CalibrationR.InitStaticParam();
            CalibrationBL.InitStaticParam();
            CalibrationBR.InitStaticParam();
            ImageManager = ImageProcessManager.GetInstance();
            ImageManager.InitParam(strProductName);
        }
        public static bool SaveParam()
        {
            bool bFlag = true;
            bFlag = bFlag &&_AssemParam.SaveParam();
            bFlag = bFlag && SuctionLParam.SaveParam();
            bFlag = bFlag && SuctionRParam.SaveParam();
            bFlag = bFlag && BarrelParam.SaveParam();
            bFlag = bFlag && CalibrationL.SaveStaticParam();
            bFlag = bFlag && CalibrationR.SaveStaticParam();
            bFlag = bFlag && CalibrationBL.SaveStaticParam();
            bFlag = bFlag && CalibrationBR.SaveStaticParam();
            CamManager.saveCameraParams(CommonSet.strCameraFile);
            return bFlag;
        }
        public static void InitCamera(string strCamFile)
        {
            ICamera.ShowInfo += CommonSet.WriteInfo;
            //初始化相机参数
           // ICamera.getCameras();
            CamManager = CameraManager.getCameraManager(strCamFile);
            CamUpL = CamManager.getCamera(CameraName.CamUpL);
            CamUpL.RemoveDelegate();
            CamUpL.ProcessImage += ShowImageClass.ProcessImageUpL;

            CamDownL = CamManager.getCamera(CameraName.CamDownL);
            CamDownL.RemoveDelegate();
            CamDownL.ProcessImage += ShowImageClass.ProcessImageDownL;

            CamUpR = CamManager.getCamera(CameraName.CamUpR);
            CamUpR.RemoveDelegate();
            CamUpR.ProcessImage += ShowImageClass.ProcessImageUpR;

            CamDownR = CamManager.getCamera(CameraName.CamDownR);
            CamDownR.RemoveDelegate();
            CamDownR.ProcessImage += ShowImageClass.ProcessImageDownR;

            CamBarrel = CamManager.getCamera(CameraName.CamUpBarrel);
            CamBarrel.RemoveDelegate();
            CamBarrel.ProcessImage += ShowImageClass.ProcessImageUpBarrel;

        }
        public static void ReleaseCamera()
        {
            CamUpL.CloseCamera();
            CamUpL.Release();

            CamDownL.CloseCamera();
            CamDownL.Release();

            CamUpR.CloseCamera();
            CamUpR.Release();

            CamDownR.CloseCamera();
            CamDownR.Release();

            CamBarrel.CloseCamera();
            CamBarrel.Release();
        }
        


    }
}
