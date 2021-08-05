using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;

namespace Assembly
{
    //设定所有吸笔的组装角度和压力
   public class AssemSolution
    {
        public int iSolutionNum = 1;//组装方案号
        public List<double> lstAngle = new List<double>();//所有吸笔的角度;
        public List<double> lstPressure = new List<double>();//所有吸笔的组装压力
        public List<double> lstVel = new List<double>();//组装速度
        public List<double> lstTime = new List<double>();//组装时间
        public List<double> lstHieght = new List<double>();//组装高度
        public List<double> lstHeight2 = new List<double>();//组装过压量
        public int iMinNum = 0;//此方案最小组装位
        public int iMaxNum = 0;//此方案最大组装位
        private int SuctionNum = 18;//总吸笔个数

        public AssemSolution(int SolutionNum)
        {
            iSolutionNum = SolutionNum;
            for (int i = 0; i < SuctionNum; i++)
            {
                lstAngle.Clear();
                lstPressure.Clear();
                lstVel.Clear();
                lstTime.Clear();
                lstHieght.Clear();
                lstHeight2.Clear();
                lstAngle.Add(0);
                lstPressure.Add(0);
                lstVel.Add(5);
                lstTime.Add(0.2);
                lstHieght.Add(0.2);
               
                lstHeight2.Add(0);
            }
        }
        /// <summary>
        /// 判断序号是否属于此组装方案
        /// </summary>
        /// <param name="num">穴号</param>
        /// <returns>如果属于此方案，返回true,否则返回false</returns>
        public bool IsThisSolution(int num)
        {
            if ((num >= iMinNum) && (num <= iMaxNum))
            {
                return true;
            }
            return false;
        }
        public void initParam(string file,string section)
        {
            StringToList(ref lstAngle, IniOperate.INIGetStringValue(file, section, "lstAngle", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0"));
            StringToList(ref lstPressure, IniOperate.INIGetStringValue(file, section, "lstPressure", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0"));
            StringToList(ref lstVel, IniOperate.INIGetStringValue(file, section, "lstVel", "5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5"));
            StringToList(ref lstTime, IniOperate.INIGetStringValue(file, section, "lstTime", "0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2"));
            StringToList(ref lstHieght, IniOperate.INIGetStringValue(file, section, "lstHieght", "0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2"));
            StringToList(ref lstHeight2, IniOperate.INIGetStringValue(file, section, "lstHieght2", "0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2,0.2"));
           
            iMinNum = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "iMinNum ", "0"));
            iMaxNum = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "iMaxNum ", "0"));
        }
        public bool saveParam(string file, string section)
        {
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstAngle", ListToString(lstAngle));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstPressure", ListToString(lstPressure));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstVel", ListToString(lstVel));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTime", ListToString(lstTime));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstHieght", ListToString(lstHieght));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstHieght2", ListToString(lstHeight2));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "iMinNum", iMinNum.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "iMaxNum", iMaxNum.ToString());

            return bFlag;
        }

        private  string ListToString(List<double> lst)
        {
            string strReturn = "";
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
        private  void StringToList(ref List<double> lst, string strValue)
        {

            if (strValue.Equals(""))
                return;
            lst.Clear();
            string[] arr = strValue.Split(',');
            foreach (string value in arr)
            {
                if (value.Equals(""))
                    continue;
                lst.Add(Convert.ToDouble(value));
            }
        }

    }
}
