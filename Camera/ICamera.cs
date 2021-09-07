using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PylonC.NET;
using HalconDotNet;
using ThridLibray;
using CLIDelegate;
namespace CameraSet0
{
   public enum CameraName{
       CamUpL,
        CamDownL,
        CamUpR,
        CamDownR,
        CamUpBarrel,


    }
    public class CameraInfo {
       public string unique_name = "";
       public string user_name = "";
       public string ip_address = "";
       public string computerIp = "";
       public string serial = "";
    
   }
   public abstract class ICamera
    {
       public  static string Brand = "灰点";
       public static uint CameraNum = 0;//相机数量
      // public CameraName cameraName;
       public string strDeviceName = "";
       public CameraInfo cameraInfo = null;
       public bool bTrigger = true;//触发模式
       public  Action<HObject> ProcessImage;//处理图像的委托
       public static List<CameraInfo> lstCamera = new List<CameraInfo>();//相机信息
       public static Dictionary<CameraName, CameraInfo> dic_CameraInfo = new Dictionary<CameraName, CameraInfo>();
       public static Action<string> ShowInfo;
       //曝光
       public double dExposure = 1000;
       public double dMaxExposure = 0;
       public double dMinExposure = 0;
       //增益
       public double dGain = 300;
       public double dMaxGain = 0;
       public double dMinGain = 0;

       public bool bInit = false;//初始化相机状态


       //public static Dictionary<CameraName,
       /// <summary>
       /// 获取所有相机
       /// </summary>
       public static string[] getCameras()
       {
           if (Brand == "DaHua")
           {
               return GetCameraName();
           }
           try
           {

          
              HTuple aa;
               lstCamera.Clear();
               dic_CameraInfo.Clear();
               HTuple info,boardValues;

             // HOperatorSet.SetSystem("filename_encoding", "utf8");
               HOperatorSet.GetSystem("filename_encoding", out aa);
               //HSystem.SetSystem("filename_encoding", "locale");
               HOperatorSet.InfoFramegrabber("GigEVision", "info_boards", out info, out boardValues);

               //string boardValues1 = "";
               //boardValues1 = boardValues.S;
               int count = boardValues.Length;
               for (int i = 0; i < count; i++)
               {
                   string[] msg = boardValues[i].S.Split('|');
                   if (msg.Length > 10)
                   {
                       CameraInfo c = new CameraInfo();
                       c.unique_name = msg[0].Split(':')[1];
                       c.user_name = ToUtf8(msg[1].Split(':')[1].Trim());
                       c.ip_address = msg[2].Split(':')[1];
                       c.computerIp = msg[7].Split(':')[1];
                       c.serial = msg[10].Split(':')[1];
                       lstCamera.Add(c);
                       try
                       {
                           CameraName name = (CameraName)Enum.Parse(typeof(CameraName), c.user_name);
                           dic_CameraInfo.Add(name, c);
                       }
                       catch (Exception)
                       {
                       
                      
                       }
                   }
               
               }
           }
           catch (Exception)
           {

               
           }
           
           return null;
       }
      
         public static string[] GetCameraName()
        {
            List<IDeviceInfo> li = Enumerator.EnumerateDevices();
            
            List<string> lst = new List<string>();
            foreach (IDeviceInfo info in li)
            { 
                lst.Add(info.Name);
                
            }
            return lst.ToArray();
        }
      
       private static string ToUtf8(string str)
       {
          // return str;
          
            //Encoding utf8;  
            //Encoding gb2312;  
            //utf8 = Encoding.GetEncoding("UTF-8");  
            //gb2312 = Encoding.GetEncoding("GB2312"); 
         
            //byte[] gb = gb2312.GetBytes(str);
          // Encoding e = Encoding.Default;
           byte[] b = ASCIIEncoding.GetEncoding("GB2312").GetBytes(str);
          // byte[] b2 = ASCIIEncoding.UTF8.GetBytes(str);
           
          // byte[] b4 = ASCIIEncoding.Unicode.GetBytes(str);
          // byte[] b6 = ASCIIEncoding.GetEncoding("GBK").GetBytes(str);
          // byte[] b8 = ASCIIEncoding.ASCII.GetBytes(str);
          // byte[] b9 = ASCIIEncoding.GetEncoding("iso-8859-1").GetBytes(str);
          // // byte[] b = ASCIIEncoding.Unicode.GetBytes(str);
          // byte[] b1 = ASCIIEncoding.UTF8.GetBytes("相机下1");
          // byte[] b3 = ASCIIEncoding.GetEncoding("gb2312").GetBytes("上相机1");
          // byte[] b5 = ASCIIEncoding.Unicode.GetBytes("上相机1");
          // byte[] b7 = ASCIIEncoding.GetEncoding("GBK").GetBytes("上相机1");
          //// byte[] b5 = ASCIIEncoding.Unicode.GetBytes("上相机1");
          // string str1 = b1.ToString() + b2.ToString() + b3.ToString() + b4.ToString() + b5.ToString() + b6.ToString() + b7.ToString() + b8.ToString()+ b9.ToString();
          // // gb = Encoding.Convert(gb2312,utf8,gb);  
           string strUtf = Encoding.UTF8.GetString(b);

           return strUtf;
         
       }
       public static CameraInfo getCameraInfoByName(string deviceName)
       {
           foreach (CameraInfo info in lstCamera)
           {
               if (info.user_name.Equals(deviceName))
               {
                   return info;
               }
           }
           return null;
       }
       /// <summary>
       /// 移除处理图像的委托
       /// </summary>
       public abstract void RemoveDelegate();
       /// <summary>
       /// 初始化相机
       /// </summary>
       ///  <param name="DeviceName">相机名</param>
       /// <returns>成功返回true,失败返回false</returns>
       public abstract bool InitCamera(string DeviceName);
       /// <summary>
       /// 设置曝光
       /// </summary>
       /// <param name="value">曝光值</param>
       public abstract void SetExposure(double value);
       /// <summary>
       /// 获取最大最小曝光值
       /// </summary>
       /// <param name="minExposure">最小曝光</param>
       /// <param name="maxExposure">最大曝光</param>
       public abstract void GetMinMaxExposure(ref double minExposure, ref double maxExposure);
       /// <summary>
       /// 设置增益
       /// </summary>
       /// <param name="value">增益值</param>
       public abstract void SetGain(double value);
       /// <summary>
       /// 获取最大最小增益值
       /// </summary>
       /// <param name="minGain">最小增益</param>
       /// <param name="maxGain">最大增益</param>
       public abstract void GetMinMaxGain(ref double minGain, ref double maxGain);

       /// <summary>
       /// 软触发
       /// </summary>
       public abstract void SoftTrigger();
       /// <summary>
       /// 设置触发模式
       /// </summary>
       /// <param name="bTrigger">true为触发模式，false为实时模式</param>
       public abstract void SetTriggerMode(bool bTrigger);
       /// <summary>
       /// 关闭相机
       /// </summary>
       public abstract void CloseCamera();

       public abstract void Release();
      
    }
}
