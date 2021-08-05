using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using log4net;
using ConfigureFile;
namespace Assembly
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
   
   
}
