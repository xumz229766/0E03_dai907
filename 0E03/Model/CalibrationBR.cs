using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
namespace _0E03
{
   public class CalibrationBR
    {
        public static Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public static Point pCalibCameraUpXY = new Point();//上相机拍照标定板位置
        public static Point pCalibCameraUpRC = new Point();//标定板在上相机的位置
        public static Point pCalibCameraDownRC = new Point();//标定板在下相机的位置

        public static string strPicDownCalibName = "";//下相机标定方法
        public static string strPicUpCalibName = "";//上相机标定方法
        public static double DGetCalibPosZ { get; set; }//取标定块高度
        public static Point pCalibUpPosXY = new Point();//上相机标定取料位
        public static int ICalibUpExposure { get; set; }//上相机标定曝光值


        public static double DCalibDownPosX { get; set; }//下相机标定位置X
        public static double DCalibDownPosZ { get; set; }//下相机标定高度Z
        public static int ICalibDownExposure { get; set; }//下相机标定曝光值


        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置


        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + "Calib.ini";

            string section = "CalibBR";
            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();


            for (int i = 0; i < 9; i++)
            {
                Point p = new Point();

                p = new Point();
                p.initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC.Add(p);

                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());
                lstCalibCameraUpXY.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

            }
            pCalibUpPosXY.initParam(file, section, "pCalibUpPosXY");
            DGetCalibPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetCalibPosZ", "0"));
            DCalibDownPosX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCalibDownPosX", "0"));
            DCalibDownPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCalibDownPosZ", "0"));
            ICalibDownExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "ICalibDownExposure", "100"));
            ICalibUpExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "ICalibUpExposure", "100"));


            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "右下相机标定");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "镜筒上相机标定");



            //strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料2下相机标定");
            //strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料2上相机标定");


        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath +  "Calib.ini";

            string section = "CalibBR";
            bool bFlag = true;
            for (int i = 0; i < 9; i++)
            {

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());



            }

            bFlag = bFlag && pCalibUpPosXY.saveParam(file, section, "pCalibUpPosXY");


            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetCalibPosZ", DGetCalibPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCalibDownPosX", DCalibDownPosX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCalibDownPosZ", DCalibDownPosZ.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "ICalibDownExposure", ICalibDownExposure.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "ICalibUpExposure", ICalibUpExposure.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());

            return bFlag;
        }
    }
}
