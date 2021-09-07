using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_OPTControllerAPI;
namespace CameraSet0
{
   public class LightControl
    {
       public  int ChannelNum = 4;//通道数量
       private OPTControllerAPI OPTController = null;
       public string strPortName = "COM1";//串口号
       public int[] iIntensity = new int[4];
       public bool bOpen = false;//串口打开状态
       public bool[] bChannelOpen = new bool[5];//通道打开状态
       public LightControl()
       {
           OPTController = new OPTControllerAPI();
       }
       public bool OpenPort()
       {
           bOpen = (0 == OPTController.InitSerialPort(strPortName));
           return bOpen;
       }
       /// <summary>
       /// 打开通道
       /// </summary>
       /// <param name="channel">通道号,0代表全部通道</param>
       /// <returns>打开成功返回ture,否则返回false</returns>
       public bool OpenChanel(int channel)
       {
           if (bOpen)
           {
               bChannelOpen[channel] =( OPTController.TurnOnChannel(channel) == 0);
               if (channel == 0)
               {
                   for (int i = 1; i < ChannelNum + 1; i++)
                   {
                       bChannelOpen[i] = bChannelOpen[0];
                   }
               }
               return bChannelOpen[channel];
           }
           return false;
       }
       /// <summary>
       /// 关闭通道
       /// </summary>
       /// <param name="channel">通道号,0代表全部通道</param>
       /// <returns>打开成功返回ture,否则返回false</returns>
       public bool CloseChannel(int channel)
       {
           if (bOpen)
           {
               int result = OPTController.TurnOffChannel(channel);
                bChannelOpen[channel] =!(result == 0);
                if (channel == 0)
                {
                    for (int i = 1; i < ChannelNum + 1; i++)
                    {
                        bChannelOpen[i] = bChannelOpen[0];
                    }
                }
                return bChannelOpen[channel];
           }
           return false;
              
       }
       public bool ClosePort()
       {
          return 0 == OPTController.ReleaseSerialPort();
       }
       /// <summary>
       /// 设置通道的亮度
       /// </summary>
       /// <param name="channel">通道号,0代表全部通道</param>
       /// <param name="intensity">光的强度</param>
       /// <returns>打开成功返回ture,否则返回false</returns>
       public bool SetIntensity(int channel, int intensity) {
            if (channel == 0)
            {
                for (int i = 0; i < ChannelNum; i++)
                {
                    iIntensity[i] = intensity;
                }
                return OPTController.SetIntensity(channel, intensity) == 0;
            }
            else
            {
                if (bChannelOpen[channel])
                {
               
                    iIntensity[channel - 1] = intensity;
                }
                return OPTController.SetIntensity(channel, intensity) == 0;
           
            }
           return false;
       }

    }
}
