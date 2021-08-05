using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Assembly
{
    //两个组装站的运行参数监控
    public class ParamListerner
    {
        public static StationData station1 = new StationData(1);
        public static StationData station2 = new StationData(2);
        public static void InitData()
        {
            station1.initData();
            station2.initData();
        }
        public static double GetLenAngle(int iLen)
        {
            if (station1.iCurrentBarrelNum == iLen)
            {
                return station1.dCurrentBarrelAngle;
            }
            else
            {
                return station2.dCurrentBarrelAngle;
            }
        }
    }
    //运行时工位实际的数据
    public class StationData
    {
        public int iStation = 1;//站号名
        public int iAssembleOrder = 0;//组装方案
        public int iCurrentBarrelNum = 0;//当前工位的镜筒序列号
        public double dCurrentBarrelAngle = 0;//当前镜筒角度
        public bool bGet1 = false; //工位1是否已获取物料
        public bool bGet2 = false;//工位2是否已获取物料
        public bool bFinish1 = false;//工位前半部分组装完成
        public bool bFinish2 = false;//工位后半部分组装完成，当bFinish1和bFinish2都为true时,代表组装完成
        public List<double> lstHeight = new List<double>();//实际高度数据
        public List<double> lstPressure = new List<double>();//实际压力数据
        public List<double> lstZPos = new List<double>();//实际组装Z轴坐标
        private int iNum = 0;
        public StationData(int _iStation)
        {
            iStation = _iStation;
            iAssembleOrder = 0;
            lstHeight.Clear();
            lstPressure.Clear();
            for (int i = 0; i < 19; i++)
            {
                lstHeight.Add(0);
                lstPressure.Add(0);
                lstZPos.Add(0);
            }
        }
        //初始化数据
        public void initData()
        {
            iAssembleOrder = 0;
            bFinish1 = false;
            bFinish2 = false;
            bGet1 = false;
            bGet2 = false;
            iCurrentBarrelNum = 0;
            dCurrentBarrelAngle = 0;
            for (int i = 0; i < 19; i++)
            {
                lstHeight[i]=0;
                lstPressure[i]=0;
                lstZPos[i] = 0;
            }
            
        }

        private void WriteHeight(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                try
                {
                    DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
                }
                catch (Exception)
                {

                }
            }
            string file = strPath + DateTime.Now.ToString("yyMMdd") + ".csv";
            try
            {
                if (!File.Exists(file))
                {
                    FileStream fs = File.Create(file);
                    fs.Close();
                    string strHead = "时间,镜筒,站号,";
                    for (int i = 1; i < 19; i++)
                    {
                        if (i < 10)
                        {
                            strHead += CommonSet.dic_OptSuction1[i].Name + ",";
                            
                        }
                        else {
                            strHead += CommonSet.dic_OptSuction2[i].Name + ",";
                        }

                    }

                    strHead = strHead.Remove(strHead.LastIndexOf(','));
                    StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                    sw.WriteLine(strHead);
                    sw.Close();
                }

            }
            catch (Exception)
            {
            }
            try
            {
                string strContent = "";
                int count = lstHeight.Count;
                for (int i = 0; i < count; i++)
                {
                    strContent += lstHeight[i] + "," ;
                }
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + iNum.ToString() + "," + iStation.ToString() + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WriteHeightResult(string strPath)
        {
            iNum = iCurrentBarrelNum;
            Action<string> dele = WriteHeight;
            dele.BeginInvoke(strPath, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string> dele = (Action<string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }


        private void WritePressure(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                try
                {
                    DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
                }
                catch (Exception)
                {

                }
            }
            string file = strPath + DateTime.Now.ToString("yyMMdd") + ".csv";
            try
            {

                if (!File.Exists(file))
                {
                    FileStream fs = File.Create(file);
                    fs.Close();
                    string strHead = "时间,镜筒,站号,";
                    for (int i = 1; i < 19; i++)
                    {
                        if (i < 10)
                        {
                            strHead += CommonSet.dic_OptSuction1[i].Name + ",";

                        }
                        else
                        {
                            strHead += CommonSet.dic_OptSuction2[i].Name + ",";
                        }

                    }
                    strHead = strHead.Remove(strHead.LastIndexOf(','));
                    StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                    sw.WriteLine(strHead);
                    sw.Close();
                }

            }
            catch (Exception)
            {
            }
            try
            {
                string strContent = "";
                int count = lstPressure.Count;
                for (int i = 0; i < count; i++)
                {
                    strContent += lstPressure[i] + ",";
                }
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + iNum.ToString() + "," + iStation.ToString() + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WritePressureResult(string strPath)
        {
            iNum = iCurrentBarrelNum;
            Action<string> dele = WritePressure;
            dele.BeginInvoke(strPath, WriteCallBack, null);
        }

        private void WriteZPos(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                try
                {
                    DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
                }
                catch (Exception)
                {
                }
            }
            string file = strPath + DateTime.Now.ToString("yyMMdd") + ".csv";
            try
            {

                if (!File.Exists(file))
                {
                    FileStream fs = File.Create(file);
                    fs.Close();
                    string strHead = "时间,镜筒,站号,";
                    for (int i = 1; i < 19; i++)
                    {
                        if (i < 10)
                        {
                            strHead += CommonSet.dic_OptSuction1[i].Name + ",";

                        }
                        else
                        {
                            strHead += CommonSet.dic_OptSuction2[i].Name + ",";
                        }

                    }

                    strHead = strHead.Remove(strHead.LastIndexOf(','));
                    StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                    sw.WriteLine(strHead);
                    sw.Close();
                }

            }
            catch (Exception)
            {
            }
            try
            {
                string strContent = "";
                int count = lstZPos.Count;
                for (int i = 0; i < count; i++)
                {
                    strContent += lstZPos[i] + ",";
                }
                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + iNum.ToString() + "," + iStation.ToString() + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WriteZPosResult(string strPath)
        {
            iNum = iCurrentBarrelNum;
            Action<string> dele = WriteZPos;
            dele.BeginInvoke(strPath, WriteCallBack, null);
        }
    }
}
