using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using ConfigureFile;
using HalconDotNet;
namespace Assembly
{
    /// <summary>
    /// 从串口中读取或写入模拟电压值
    /// </summary>
    public class SerialAV
    {

        static SerialPort sp = new SerialPort();
       public static int iReadAddr = 1;
       public static int iReadChannel = 5;
       public static int iWriteAddr = 2;
       public static List<double> lstOut = new List<double>();
       public static List<double> lstPress = new List<double>();
       public static List<double> lstIn = new List<double>();
       public static HTuple PaToA = null;//输出电流和压力值的关系
      
       public static HTuple VToPa = null;//输出电压和压力值的关系

       public string portName = "COM1";
        static double dSetData = 0;//设置电压值
        public static bool bFlag = false;//开始比较标志
        public static double dGetData = 0;//获取值
       // public static Action dele_Action;//调用的委托
        private static SerialAV av = null;
        Thread th = null;
       
         static Stopwatch sw = new Stopwatch();
         public static long circleTime = 0;
       

        public static bool bWrite = false;//写标志,刚写入时置为false,当接收到对应数据后，置为true
        public static bool bRead = false;//读标志,同bWrite
       
        private static string strSendData = "";
        private SerialAV()
        {
           sp.DataReceived += sp_DataReceived;
           
            //th = new Thread(GetInputV);
            //th.Start();
          
           
        }
     
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string strRecive = ((SerialPort)sender).ReadExisting().Trim();
            ((SerialPort)sender).DiscardInBuffer();
            if (strRecive.Equals(">"))
            {
                bWrite = true;
                //System.Windows.Forms.MessageBox.Show("设置成功！");
                CommonSet.WriteInfo("设定指定电流成功！");
                return;
            }
            if (strRecive.Contains("."))
            {
                int index = strRecive.IndexOf('+');
                dGetData = Convert.ToDouble(strRecive.Remove(0, index + 1).Replace("\r", ""));
                circleTime = sw.ElapsedMilliseconds;
                bRead = true;
                CommonSet.WriteInfo("获取电压成功！读取时间为："+circleTime.ToString()+"ms");
            }           
           
        }

        public static SerialAV GetInstance()
        {
            if (av == null)
                av = new SerialAV();
            return av;

        }
        public void InitParam(string strFile)
        {
            string section = "RS485";
            portName = IniOperate.INIGetStringValue(strFile, section, "PortName", "COM1");
            iReadAddr = Convert.ToInt32(IniOperate.INIGetStringValue(strFile,section,"ReadAddr","1"));
            iReadChannel = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, section, "ReadChannel", "5"));
            iWriteAddr = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, section, "WriteAddr", "2"));
            lstIn.Clear();
            lstOut.Clear();
            lstPress.Clear();
            StringToList(ref lstIn, IniOperate.INIGetStringValue(strFile, section, "In", ""));
            StringToList(ref lstOut, IniOperate.INIGetStringValue(strFile, section, "Out", ""));
            StringToList(ref lstPress, IniOperate.INIGetStringValue(strFile, section, "Press", ""));
            SetSerialPort(portName);
            //lstIn.Add(IniOperate.INIGetStringValue(strFile, section, "In", "").);
        }
        public bool SaveParam(string strFile)
        {
             string section = "RS485";
            bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(strFile,section,"PortName",portName);
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "ReadAddr", iReadAddr.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "ReadChannel", iReadChannel.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "WriteAddr", iWriteAddr.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "In", ListToString(lstIn));
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "Out", ListToString(lstOut));
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "Press", ListToString(lstPress));

            return bFlag;
        }
        private static string ListToString(List<double> lst)
        { 
          string strReturn="";
          foreach (double d in lst)
          {
              strReturn = strReturn + d.ToString() + ",";
          }
          if (!strReturn.Equals(""))
          {
              strReturn.Remove(strReturn.LastIndexOf(","));
          }
          return strReturn;
        }
        private static void StringToList(ref List<double> lst, string strValue)
        {
            if (strValue.Equals(""))
                return;
            string[] arr = strValue.Split(',');

            foreach (string value in arr)
            {
                if(!value.Equals(""))
                     lst.Add(Convert.ToDouble(value));
            }
        }

        public bool SetSerialPort(string _portName)
        {
            
           
                portName = _portName;
                try
                {
                    if (sp.IsOpen)
                        sp.Close();
                }
                catch (Exception ex)
                {
                    CommonSet.WriteDebug("设置电气比例阀串口参数异常", ex);
                    Alarminfo.AddAlarm(Alarm.打开电气比例阀串口异常);
                    return false;
                }
                sp.PortName = _portName;
                sp.StopBits = StopBits.One;
                sp.BaudRate = 9600;
                sp.DataBits = 8;
                sp.Parity = Parity.None;
                //sp.ReadTimeout = 1000;
           
            return true;

        }
      
        /// <summary>
        /// 设置输出电压
        /// </summary>
        /// <param name="addr">模块地址</param>
        /// <param name="data">电流大小</param>
        public static void WriteOutputA(int addr, double data)
        {
            dSetData = data;
            iWriteAddr = addr;
            //lock (obj)
            //{
            if (!sp.IsOpen)
            {
                try
                {
                    sp.Open();
                }
                catch (Exception ex)
                {
                    Alarminfo.AddAlarm(Alarm.设置组装压力电流异常);
                    CommonSet.WriteInfo("设置组装压力电流异常");
                    return;
                }

            }
            dGetData = 0;
            strSendData = "#" + addr.ToString("00") + data.ToString("00.000") + "\r\n";
            bWrite = false;
            sp.Write(strSendData);
            //sp.ReadTo("\r");
            CommonSet.WriteInfo("设置输出电流为：" + data.ToString());
            
        }
     
        /// <summary>
        /// 获取输入模拟电压
        /// </summary>
        /// <param name="addr">地址</param>
        /// <param name="channel">通道号</param>
        public static void ReadInputV(int addr, int channel)
        {

            if (!sp.IsOpen)
            {
                try
                {
                    sp.Open();
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            bRead = false;
            sw.Restart();
            string strOutput = "#" + addr.ToString("00") + channel.ToString("0")+"\r\n";
            sp.Write(strOutput);
           // Thread.Sleep(500);
           // string strRecive = sp.ReadTo("\r");
            //if (strRecive.Contains("."))
            //{
            //    int index = strRecive.IndexOf('+');
            //    dGetData = Convert.ToDouble(strRecive.Remove(0, index + 1).Replace("\r", ""));
            //    circleTime = sw.ElapsedMilliseconds;

            //}   
        }
        public static void setModeParam( int writeAddr,int readAddr, int channel)
        {
            iWriteAddr = writeAddr;
            iReadAddr = readAddr;
            iReadChannel = channel;
        }

        private void GetInputV()
        {
            while (true)
            {
                try
                {
                   
                    sw.Restart();
                    if (bWrite)
                    {
                        strSendData = "#" + iWriteAddr.ToString("00") + dSetData.ToString("00.000") + "\r\n";
                       
                        sp.Write(strSendData);
                       // Thread.Sleep(200);
                        string str = sp.ReadTo("\r");
                        //校验是否写入成功
                        strSendData = "$" + iWriteAddr.ToString("00") + "6\r\n";
                        Thread.Sleep(1000);
                        sp.Write(strSendData);
                       // Thread.Sleep(200);
                        string strRData = sp.ReadTo("\r"); ;
                        if (strRData.Contains("!"))
                        { 
                           double d = Convert.ToDouble(strRData.Trim().Remove(0,3));
                           if (d == dSetData)
                           {
                               bWrite = false;
                           }
                        }
                    }

                    if (bRead)
                    {
                        ReadInputV(iReadAddr, iReadChannel);
                        if (dGetData != 0)
                        {
                            bRead = false;
                        }
                    }
                    // strResult= sp.ReadLine();                        
                    
                    


                }
                catch (Exception ex)
                {
                    string strEx = ex.ToString();
                    CommonSet.WriteInfo(strEx);
                    Thread.Sleep(100);
                    continue;
                }
                circleTime = sw.ElapsedMilliseconds;
                
                Thread.Sleep(10);

            }
        }

        public static HTuple CreateFun(List<double> lstX, List<double> lstY)
        {
            int len = lstX.Count;
            if (len == 0)
                return null;
            HTuple x, y;
            x = HTuple.TupleGenConst(len, 0);
            y = HTuple.TupleGenConst(len, 0);
            for (int i = 0; i < len; i++)
            {
                x[i] = lstX[i];
                y[i] = lstY[i];
 
            }
            HTuple fun;
            HOperatorSet.CreateFunct1dPairs(x, y, out fun);
            return fun;
        }
        public static double GetAByPressure(double pressure)
        {
            HTuple outA =7;
            try
            {
                if (PaToA == null)
                {
                   PaToA = CreateFun(lstPress, lstOut);
                }
                HOperatorSet.GetYValueFunct1d(PaToA, pressure, new HTuple("constant"), out outA);
            }
            catch (Exception ex)
            {
                return 7;
            }

            return outA.D;
        }
        
    }
}
