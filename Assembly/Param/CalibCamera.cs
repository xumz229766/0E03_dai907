using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using ConfigureFile;
namespace Assembly
{
    public enum CalibStation
    { 
        取料工位1=1,
        取料工位2,
        组装工位1,
        组装工位2,
        
        点胶工位,
    }
    /// <summary>
    /// 标定相机类，与产品无关；1-5，代表取料工位1，取料工位2，组装工位1，组装工位2,点胶工位
    /// </summary>
    public class CalibCamera
    {
        public CalibStation Station ;
        public HObject hImageUp;
        public HObject hImageDown;
        public Point pCalibPos = new Point();//上下标定位置
        public List<double> lstUpR = new List<double>();//上相机标定Row
        public List<double> lstUpC = new List<double>();//上相机标定Column      
        public List<double> lstDownR = new List<double>();//下相机标定Row
        public List<double> lstDownC = new List<double>();//下相机标定Column
        public HTuple homCamDownToUp = null;//下相机像素到上相机像素关系

        public List<double> lstCalibUpX = new List<double>();//上相机与实际像素标定X
        public List<double> lstCalibUpY = new List<double>();//上相机与实际像素标定Y
        public List<double> lstCalibUpR = new List<double>();//上相机与实际像素标定R
        public List<double> lstCalibUpC = new List<double>();//上相机与实际像素标定C

        public List<double> lstCalibDownX = new List<double>();//下相机与实际像素标定X
        public List<double> lstCalibDownC = new List<double>();//下相机与实际像素标定C

        public CalibCamera(CalibStation iStation)
        {
            Station = iStation;
        }
        //创建下相机到上相机像素关系
        public bool createCamDownToUp()
        {
            try
            {

                if ((lstDownC.Count < 4) || (lstUpC.Count < 4))
                    return false;

                HTuple DownR = new HTuple();
                HTuple DownC = new HTuple();
                HTuple UpR = new HTuple();
                HTuple UpC = new HTuple();
                int iLen = lstDownR.Count;
                for (int i = 0; i < iLen; i++)
                {
                    DownR = DownR.TupleConcat(lstDownR[i]);
                    DownC = DownC.TupleConcat(lstDownC[i]);
                    UpR = UpR.TupleConcat(lstUpR[i]);
                    UpC = UpC.TupleConcat(lstUpC[i]);
                }
              
                // HTuple homat = null;
                HOperatorSet.VectorToHomMat2d(DownR, DownC, UpR, UpC, out homCamDownToUp);
               // HOperatorSet.HomMat2dTranslate(homCamDownToUp, 0.5, 0.5, out homCamDownToUp);
            }
            catch (Exception ex)
            {
                homCamDownToUp = null;
                return false;
            }
            return true;
        }

        public void InitParam()
        {
            string file = CommonSet.strProductParamPath+"Calib.ini";
            string section = "Calib"+Station.ToString() ;
            pCalibPos.initParam(file, section, "pCalibPos");
           CommonSet.StringToList(ref lstDownC, IniOperate.INIGetStringValue(file, section, "lstDownC", "0,0,0,0,0"));
           CommonSet.StringToList(ref lstDownR, IniOperate.INIGetStringValue(file, section, "lstDownR", "0,0,0,0,0"));
           CommonSet.StringToList(ref lstUpC, IniOperate.INIGetStringValue(file, section, "lstUpC", "0,0,0,0,0"));
           CommonSet.StringToList(ref lstUpR, IniOperate.INIGetStringValue(file, section, "lstUpR", "0,0,0,0,0"));

           CommonSet.StringToList(ref lstCalibUpX, IniOperate.INIGetStringValue(file, section, "lstCalibUpX", "0,0,0,0,0,0,0,0,0"));
           CommonSet.StringToList(ref lstCalibUpY, IniOperate.INIGetStringValue(file, section, "lstCalibUpY", "0,0,0,0,0,0,0,0,0"));
           CommonSet.StringToList(ref lstCalibUpR, IniOperate.INIGetStringValue(file, section, "lstCalibUpR", "0,0,0,0,0,0,0,0,0"));
           CommonSet.StringToList(ref lstCalibUpC, IniOperate.INIGetStringValue(file, section, "lstCalibUpC", "0,0,0,0,0,0,0,0,0"));

           CommonSet.StringToList(ref lstCalibDownX, IniOperate.INIGetStringValue(file, section, "lstCalibDownX", "0,0,0,0,0,0,0,0,0"));
           CommonSet.StringToList(ref lstCalibDownC, IniOperate.INIGetStringValue(file, section, "lstCalibDownC", "0,0,0,0,0,0,0,0,0"));
         
        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + "Calib.ini";
            string section = "Calib" + Station.ToString();

            bool bFlag = true;
            bFlag = bFlag && pCalibPos.saveParam(file, section, "pCalibPos");

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstDownC", CommonSet.ListToString(lstDownC));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstDownR", CommonSet.ListToString(lstDownR));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstUpC", CommonSet.ListToString(lstUpC));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstUpR", CommonSet.ListToString(lstUpR));

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibUpX", CommonSet.ListToString(lstCalibUpX));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibUpY", CommonSet.ListToString(lstCalibUpY));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibUpR", CommonSet.ListToString(lstCalibUpR));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibUpC", CommonSet.ListToString(lstCalibUpC));

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibDownX", CommonSet.ListToString(lstCalibDownX));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstCalibDownC", CommonSet.ListToString(lstCalibDownC));
            return bFlag;
        }
    }
}
