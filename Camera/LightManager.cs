using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using log4net;
namespace CameraSet0
{
   public class LightManager
    {
       private log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       private static LightManager manager = null;
       public static int ControlNum = 1;//控制器个数
       private static Dictionary<int, LightControl> dic_Control = new Dictionary<int, LightControl>();//控制器对象存储
       public static string strLightFile = System.Windows.Forms.Application.StartupPath + "\\Param\\LightConfig.ini";
       public static int iLightUpChannel = 1;//上光源通道
       public static int iLightDownChannel = 2;//下光源通道
       private LightManager()
       {
           for (int i = 0; i < ControlNum; i++)
           {
               try
               {
                   LightControl lc = new LightControl();
                   lc.strPortName = IniOperate.INIGetStringValue(strLightFile, i.ToString(), "PortName", "COM1");
                   if (!lc.OpenPort())
                   {
                       writeInfo("控制器"+(i+1).ToString()+"打开串口失败!");
                       //continue;
                   }
                   if (!lc.OpenChanel(0))
                   {
                       writeInfo("控制器" + (i + 1).ToString() + "打开光源通道失败!");
                      // continue;
                   }
                   iLightUpChannel = Convert.ToInt32(IniOperate.INIGetStringValue(strLightFile, i.ToString(), "LightUpChannel", "1"));
                   iLightDownChannel = Convert.ToInt32(IniOperate.INIGetStringValue(strLightFile, i.ToString(), "LightDownChannel", "2"));
                   //光源控制器的参数以 0,0,0,0的形式保存
                   string strIntensity = IniOperate.INIGetStringValue(strLightFile, i.ToString(), "Intensity", "30");
                   string[] strA = strIntensity.Split(',');
                   for (int k = 0; k < lc.ChannelNum; k++)
                   {
                       if (strA.Length > 1)
                       {
                           lc.iIntensity[k] = Convert.ToInt32(strA[k]);
                       }
                       else
                       {
                           lc.iIntensity[k] = Convert.ToInt32(strIntensity);

                       }
                       lc.SetIntensity(k+1, lc.iIntensity[k]);
                   }
                   dic_Control.Add(i, lc);
               }
               catch (Exception ex) {
                   writeDebug("控制器" + (i + 1).ToString() + "光源初始化异常", ex);
               }
           }
       }
      
       public static LightManager getLightManager()
       {
           if (manager == null)
               manager = new LightManager();
           return manager;
       }
       /// <summary>
       /// 保存参数
       /// </summary>
       /// <returns>如果保存成功,则返回true;否则返回false</returns>
       public bool saveLightParam()
       {
           bool bFlag = true;
            for (int i = 0; i < ControlNum; i++)
           {
               LightControl lc = dic_Control[i];
               bFlag = bFlag && IniOperate.INIWriteValue(strLightFile, i.ToString(), "PortName", lc.strPortName);
               bFlag = bFlag && IniOperate.INIWriteValue(strLightFile, i.ToString(), "LightUpChannel", iLightUpChannel.ToString());
               bFlag = bFlag && IniOperate.INIWriteValue(strLightFile, i.ToString(), "LightDownChannel", iLightDownChannel.ToString());
               string strLight = "";
               for (int k = 0; k < lc.ChannelNum; k++)
               {
                   if (k == lc.ChannelNum - 1)
                   {
                       strLight += lc.iIntensity[k].ToString();
                       break;
                   }
                   strLight = strLight + lc.iIntensity[k].ToString() + ",";
               }
               bFlag = bFlag && IniOperate.INIWriteValue(strLightFile, i.ToString(), "Intensity", strLight);
           
           }
            return bFlag;
       }
       /// <summary>
       /// 按序号获取控制器对象
       /// </summary>
       /// <param name="index">序号从0开始</param>
       /// <returns>如果存在则返回对象,否则返回null</returns>
       public LightControl getControl(int index)
       {
           if (dic_Control.ContainsKey(index))
               return dic_Control[index];
           return null;
       }

       public void SetLightControl(int index,LightControl lc)
       {
            dic_Control[index] = lc;
       }

       public void closeLight()
       {
           foreach (KeyValuePair<int, LightControl> pair in dic_Control)
           {
               pair.Value.ClosePort();
           }
        
       }
       public void writeInfo(string info)
       {
           log.Info(info);
       }
       public void writeDebug(string info,Exception ex)
       {
           log.Debug(info,ex);
       }
    }
}
