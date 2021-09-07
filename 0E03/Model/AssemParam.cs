using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;

namespace _0E030
{
    /// <summary>
    /// 组装设定参数
    /// </summary>
    public class AssemParam
    {
        public double dAssemSetAngL = 0;//左组装设定角度
        public double dAssemSetAngR = 0;//右组装设定角度
        public double dAssemSetPreL = 1;//左组装设定压力
        public double dAssemSetPreR = 1;//右组装设定压力
        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\AssemParam.ini";

            string section = "Param";

            dAssemSetAngL = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemAngL", "0"));
            dAssemSetAngR = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemAngR", "0"));
            dAssemSetPreL = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemPreL", "1"));
            dAssemSetPreR = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemPreR", "1"));
          




        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\AssemParam.ini";

            string section = "Param";
            bool bFlag = true;
            // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTrayNum", CommonSet.ListToString(lstTrayNum));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemAngL", dAssemSetAngL.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemAngR", dAssemSetAngR.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemPreL", dAssemSetPreL.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemPreR", dAssemSetPreR.ToString());
           ;

            return bFlag;

        }
    }
}
