using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using ConfigureFile;
using ImageProcess;
using System.ComponentModel;
namespace _0E03
{
    /// <summary>
    /// 存储点位的类
    /// </summary>
    public class Point {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Theta { get; set; }
        public bool isEmpty()
        {
            if ((X == 0) && (Y == 0) && (Z == 0))
                return true;
            return false;
        }
        public void initParam(string file,string section,string keyHead)
        {
          X =Convert.ToDouble(IniOperate.INIGetStringValue(file,section,keyHead+"_X","0"));
          Y =Convert.ToDouble(IniOperate.INIGetStringValue(file,section,keyHead+"_Y","0"));
          Z =Convert.ToDouble(IniOperate.INIGetStringValue(file,section,keyHead+"_Z","0"));
          Theta =Convert.ToDouble(IniOperate.INIGetStringValue(file,section,keyHead+"_Theta","0"));
        }
        public bool saveParam(string file,string section,string keyHead)
        {
              bool bFlag =true;
              bFlag = bFlag && IniOperate.INIWriteValue(file,section,keyHead+"_X",X.ToString());
              bFlag = bFlag && IniOperate.INIWriteValue(file,section,keyHead+"_Y",Y.ToString());
              bFlag = bFlag && IniOperate.INIWriteValue(file,section,keyHead+"_Z",Z.ToString());
              bFlag = bFlag && IniOperate.INIWriteValue(file,section,keyHead+"_Theta",Theta.ToString());
              return bFlag;
        }
    }
    /// <summary>
    /// 轴速度和加减速
    /// </summary>
    public class VelAxis
    {
        public double Vel { get; set; }//运行速度
        public double ACCAndDec { get; set; }//加减速
        public void initParam(string file, string section, string keyHead)
        {
            Vel = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, keyHead + "_Vel", "10"));
            ACCAndDec = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, keyHead + "_ACCAndDec", "0.1"));
           
        }
        public bool saveParam(string file, string section, string keyHead)
        {
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, keyHead + "_Vel", Vel.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, keyHead + "_ACCAndDec", ACCAndDec.ToString());
           
            return bFlag;
        }
    }

    public class ImageResult : INotifyPropertyChanged
    {


        private int id = 0;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }


        private double dCenterRow = 0;//下相机获取到的中心位置Row

        public double CenterRow
        {
            get { return dCenterRow; }
            set
            {
                dCenterRow = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CenterRow"));
                }
            }
        }
        private double dCenterColumn = 0;//下相机获取到的中心位置Column

        public double CenterColumn
        {
            get { return dCenterColumn; }
            set
            {
                dCenterColumn = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CenterColumn"));
                }
            }
        }

        public double dRadius = 0;//半径
        public bool bStatus = false;//图像处理是否完成
        private string strResult = "";//图像处理结果字符串
        public bool bImageResult = false;//图像处理结果
        public double dAngle = 0; //下相机获取的角度
        public bool bExist = false;//有无检测

        public string StrResult
        {
            get { return strResult; }

            set
            {
                strResult = value;
                if (strResult == "")
                {
                    bImageResult = false;
                    dCenterRow = 0;
                    dCenterColumn = 0;
                    dAngle = 0;
                    dRadius = 0;
                    bExist = false;
                }
                else
                {
                    string[] str = strResult.Split(',');
                    if (str[0].Equals("OK"))
                    {
                        bImageResult = true;
                    }
                    else
                    {
                        bImageResult = false;

                    }
                    dAngle = 360 - Convert.ToDouble(str[1]);
                    dCenterRow = Convert.ToDouble(str[2]);
                    dCenterColumn = Convert.ToDouble(str[3]);
                    bExist = Convert.ToBoolean(str[4]);
                    //dRadius = Convert.ToDouble(str[5]);
                }
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
   
}
