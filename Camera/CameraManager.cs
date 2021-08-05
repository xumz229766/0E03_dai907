using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using ConfigureFile;
namespace CameraSet
{
   public class CameraManager
    {
       private static Dictionary<CameraName, ICamera> dic_Camera = new Dictionary<CameraName, ICamera>();
       public static string strCameraFile = System.Windows.Forms.Application.StartupPath + "\\Param\\CameraInfo.ini";
       private List<ICamera> lstCamera = new List<ICamera>();
       private static CameraManager manager = null;
       private CameraManager(string _strCameraFile)
       {
          // HOperatorSet.SetSystem("filename_encoding", "utf8");
          // ICamera.getCameras();
           initCameraParams(_strCameraFile);
       }
       public void initCameraParams(string file)
       { 
           string[] cameras = Enum.GetNames(typeof(CameraName));
           foreach (string name in cameras)
           {
               CameraName cam = (CameraName)Enum.Parse(typeof(CameraName), name);
               ICamera camera = new GrayPoint();
               camera.bTrigger = true;
               camera.dExposure = Convert.ToDouble(IniOperate.INIGetStringValue(file,name,"Exposure","1000"));
               camera.dGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, name, "Gain", "1"));
               camera.strDeviceName = IniOperate.INIGetStringValue(file, name, "DeviceName", name);
               camera.cameraInfo = ICamera.getCameraInfoByName(camera.strDeviceName);
               setCamera(cam, camera);
               camera.InitCamera(camera.strDeviceName);

           }       
       
       }
       
       public void saveCameraParams(string file)
       {
          
           foreach (KeyValuePair<CameraName, ICamera> pair in dic_Camera)
           {
               IniOperate.INIWriteValue(file, pair.Key.ToString(), "Exposure", pair.Value.dExposure.ToString());
               IniOperate.INIWriteValue(file, pair.Key.ToString(), "Gain", pair.Value.dGain.ToString());
               IniOperate.INIWriteValue(file, pair.Key.ToString(), "DeviceName", pair.Value.strDeviceName.ToString());
           }
       
       }
       
       public static CameraManager getCameraManager(string strCamFile)
       {
           if (manager == null)
               manager = new CameraManager(strCamFile);

           return manager;
       }
       public void setCamera(CameraName cam,ICamera camera)
       {
           if (dic_Camera.ContainsKey(cam))
           {
               dic_Camera[cam] = camera;
           }
           else {
               dic_Camera.Add(cam, camera);
           }
       }
       public ICamera getCamera(CameraName cam)
       {
           return dic_Camera[cam];
       }
       public static void CloseCamera()
       {
           foreach (KeyValuePair<CameraName, ICamera> pair in dic_Camera)
           {
               pair.Value.CloseCamera();
               pair.Value.Release();
           }
       
       }

    }
}
