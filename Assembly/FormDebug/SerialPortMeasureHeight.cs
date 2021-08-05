using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using ConfigureFile;
using System.Threading;
namespace Assembly
{

    /// <summary>
    /// 测高的串口,只接收数据
    /// </summary>
    public class SerialPortMeasureHeight
    {
        private static SerialPortMeasureHeight spm = null;
        public static double dStation1 = 0;//组装工位1测高值
        public static double dStation2 = 0;//组装工位2测高值

        public static long lTime = 0;//两个数据获取的间隔时间
        public static Stopwatch sw = new Stopwatch();
        public static SerialPort sp = new SerialPort();
        public string PortName = "COM5";
        public int BaudRate = 38400;
        bool bGet = false;
        Thread th_Data = null;
        private SerialPortMeasureHeight()
        {
            th_Data = new Thread(GetData);
            th_Data.Start();
            //sp.DataReceived += sp_DataReceived;
        }
        public static SerialPortMeasureHeight GetInstance()
        {
            if (spm == null)
                spm = new SerialPortMeasureHeight();
            return spm;

        }
        private void GetData()
        {
            while (true)
            {
                try
                {
                    if (bGet)
                    {
                        string strRecive = sp.ReadTo("@@").Trim();
                        string[] strValue = strRecive.Split(',');
                        int data1 = Convert.ToInt32(strValue[2].Substring(1, 4).Trim());
                        int data2 = Convert.ToInt32(strValue[3].Substring(1, 4).Trim());
                        dStation1 = data1 / 1000.0;
                        dStation2 = data2 / 1000.0;
                        lTime = sw.ElapsedMilliseconds;
                        sw.Restart();
                    }
                   
                }
                catch (Exception)
                {
                    
                    
                }
                Thread.Sleep(10);
            }
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                string strRecive = sp.ReadTo("@@").Trim();
                string[] strValue = strRecive.Split(',');
                int data1 = Convert.ToInt32(strValue[2].Substring(1,4).Trim());
                int data2 = Convert.ToInt32(strValue[3].Substring(1,4).Trim());
                dStation1 = data1 / 1000.0;
                dStation2 = data2 / 1000.0;
                lTime = sw.ElapsedMilliseconds;
                sw.Restart();
           
            }
            catch (Exception ex)
            {
               // CommonSet.WriteInfo("测高串口接收数据异常："+ex.ToString());
              
            }
        }
        public bool SetAndOpenSerialPort(string _portName,int _rate)
        {


            PortName = _portName;
            BaudRate = _rate;
            try
            {
                bGet = false;
                if (sp.IsOpen)
                    sp.Close();
          
                sp.PortName = _portName;
                sp.StopBits = StopBits.One;
                sp.BaudRate = BaudRate;
                sp.DataBits = 8;
                sp.Parity = Parity.None;
                //sp.ReadTimeout = 1000;
                sp.Open();
                bGet = true;
            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("设置测高的串口参数异常", ex);
                Alarminfo.AddAlarm(Alarm.打开测高串口异常);
                bGet = false;
                return false;
            }
            return true;

        }

        public void InitParam(string strFile)
        {
            string section = "RS232-测高";
            PortName = IniOperate.INIGetStringValue(strFile, section, "PortName", "COM5");
            BaudRate = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, section, "BaudRate", "38400"));


            SetAndOpenSerialPort(PortName, BaudRate);
            //lstIn.Add(IniOperate.INIGetStringValue(strFile, section, "In", "").);
        }
        public bool SaveParam(string strFile)
        {
            string section = "RS232-测高";
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "PortName", PortName);
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "BaudRate", BaudRate.ToString());

            return bFlag;
        }
        public void Close()
        {
            try
            {
                bGet = false;
                if (sp.IsOpen)
                    sp.Close();
                if (th_Data.IsAlive)
                {
                    th_Data.Abort();
                }
                th_Data = null;

            }
            catch (Exception)
            {
                
               
            }
        }

    }

    /// <summary>
    /// 测高的串口,只接收数据
    /// </summary>
    public class SerialPortMeasurePressure
    {
        private static SerialPortMeasurePressure spm = null;
        public static double dStation1 = 0;//组装工位1测压值
        public static double dStation2 = 0;//组装工位2测压值

        public static long lTime = 0;//两个数据获取的间隔时间
        public static Stopwatch sw = new Stopwatch();
        public static SerialPort sp1 = new SerialPort();
        public static SerialPort sp2 = new SerialPort();
        public string PortName1 = "COM1";
        public string PortName2 = "COM2";
        public int BaudRate1 = 38400;
        public int BaudRate2 = 38400;
        bool bGet = false;
        bool bGet1 = false;
        Thread th_Data = null;
        private SerialPortMeasurePressure()
        {
            //sp1.DataReceived += sp1_DataReceived;
            //sp2.DataReceived += sp2_DataReceived;
            th_Data = new Thread(GetData);
            th_Data.Start();
        }

      
        public static SerialPortMeasurePressure GetInstance()
        {
            if (spm == null)
                spm = new SerialPortMeasurePressure();
            return spm;

        }
        private void GetData()
        {
            while (true)
            {
                try
                {
                    if (bGet)
                    {
                        string strRecive = sp1.ReadTo(ASCIIEncoding.ASCII.GetString(new byte[] { 0xd })).Trim();
                        if (strRecive.Contains("-"))
                        {
                            dStation1 = -Convert.ToDouble(strRecive.Substring(1));

                        }
                        else
                        {
                            dStation1 = Convert.ToDouble(strRecive);
                        }
                        sp1.DiscardOutBuffer();
                        sp1.DiscardInBuffer();

                        lTime = sw.ElapsedMilliseconds;
                        sw.Restart();

                    }

                }
                catch (Exception)
                {


                }

                try
                {
                    if (bGet1)
                    {
                        string str = ASCIIEncoding.ASCII.GetString(new byte[] { 0x0D });

                        string strRecive = sp2.ReadTo(str).Trim();
                        if (strRecive.Contains("-"))
                        {
                            dStation2 = -Convert.ToDouble(strRecive.Substring(1));

                        }
                        else
                        {
                            dStation2 = Convert.ToDouble(strRecive);
                        }
                        sp2.DiscardOutBuffer();
                        sp2.DiscardInBuffer();
                    }

                }
                catch (Exception)
                {


                }
                Thread.Sleep(10);
            }
        }
        void sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                string strRecive = sp1.ReadTo(ASCIIEncoding.ASCII.GetString(new byte[]{0xd})).Trim();
                if (strRecive.Contains("-"))
                {
                     dStation1 = -Convert.ToDouble(strRecive.Substring(1));

                }
                else
                {
                     dStation1 = Convert.ToDouble(strRecive);
                }
                sp1.DiscardOutBuffer();
                sp1.DiscardInBuffer();
            
                lTime = sw.ElapsedMilliseconds;
                sw.Restart();

            }
            catch (Exception ex)
            {
               // CommonSet.WriteInfo("测压1串口接收数据异常：" + ex.ToString());

            }
        }
        void sp2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string str = ASCIIEncoding.ASCII.GetString(new byte[]{ 0x0D });

                string strRecive = sp2.ReadTo(str).Trim();
                if (strRecive.Contains("-"))
                {
                    dStation2 = -Convert.ToDouble(strRecive.Substring(1));

                }
                else
                {
                    dStation2 = Convert.ToDouble(strRecive);
                }
                sp2.DiscardOutBuffer();
                sp2.DiscardInBuffer();

            }
            catch (Exception ex)
            {
              //  CommonSet.WriteInfo("测压2串口接收数据异常：" + ex.ToString());

            }
        }
        public bool SetAndOpenSerialPort1(string _portName, int _rate)
        {


            PortName1 = _portName;
            BaudRate1 = _rate;
            try
            {
                bGet = false;
                if (sp1.IsOpen)
                    sp1.Close();

                sp1.PortName = _portName;
                sp1.StopBits = StopBits.One;
                sp1.BaudRate = BaudRate1;
                sp1.DataBits = 8;
                sp1.Parity = Parity.None;
               
                //sp.ReadTimeout = 1000;
                sp1.Open();
                bGet = true;
            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("设置测压1的串口参数异常", ex);
                Alarminfo.AddAlarm(Alarm.打开测压串口1异常);
                bGet = false;
                return false;
            }
            return true;

        }
        public bool SetAndOpenSerialPort2(string _portName, int _rate)
        {


            PortName2 = _portName;
            BaudRate2 = _rate;
            try
            {
                bGet1 = false;
                if (sp2.IsOpen)
                    sp2.Close();

                sp2.PortName = _portName;
                sp2.StopBits = StopBits.One;
                sp2.BaudRate = BaudRate2;
                sp2.DataBits = 8;
                sp2.Parity = Parity.None;
               
                //sp.ReadTimeout = 1000;
                sp2.Open();
                bGet1 = true;
            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("设置测压2的串口参数异常", ex);
                Alarminfo.AddAlarm(Alarm.打开测压串口2异常);
                bGet1 = false;
                return false;
            }
            return true;

        }
        public void InitParam(string strFile)
        {
            string section = "RS232-测压";
            PortName1 = IniOperate.INIGetStringValue(strFile, section, "PortName1", "COM1");
            BaudRate1 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, section, "BaudRate1", "38400"));
            PortName2 = IniOperate.INIGetStringValue(strFile, section, "PortName2", "COM2");
            BaudRate2 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, section, "BaudRate2", "38400"));

            SetAndOpenSerialPort1(PortName1, BaudRate1);
            SetAndOpenSerialPort2(PortName2, BaudRate2);
            //lstIn.Add(IniOperate.INIGetStringValue(strFile, section, "In", "").);
        }
        public bool SaveParam(string strFile)
        {
            string section = "RS232-测压";
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "PortName1", PortName1);
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "BaudRate1", BaudRate1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "PortName2", PortName2);
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, section, "BaudRate2", BaudRate2.ToString());
            return bFlag;
        }

        public void Close()
        {
            try
            {
                bGet1 = false;
                bGet = false;
                if (sp1.IsOpen)
                    sp1.Close();
                if (sp2.IsOpen)
                    sp2.Close();
                if (th_Data.IsAlive)
                {
                    th_Data.Abort();
                }
                th_Data = null;

            }
            catch (Exception)
            {


            }
        }

    }
   


}
