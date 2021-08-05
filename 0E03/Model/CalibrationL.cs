using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using HalconDotNet;
namespace _0E03
{
    public class CalibrationL
    {
        public Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public Point pCalibCameraUpXY = new Point();//上相机拍照标定板位置
        public Point pCalibCameraUpRC = new Point();//标定板在上相机的位置
        public Point pCalibCameraDownRC = new Point();//标定板在下相机的位置

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
        public static List<Point> lstCalibCameraDownRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraDownXY = new List<Point>();//标定上相机坐标位置
        public static bool bUseUpAngle = true;
        public static bool bUseDownAngle = true;

        //旋转中心
        public static string strPicRotateName = "";//上相机旋转标定方法
        public static double dRoateCenterColumn = 0;
        public static double dRoateCenterRow = 0;
        public static int iRotateNum = 10;
        public static int iRotateStep = 10;
        public static List<double> lstRotateRow = new List<double>();
        public static List<double> lstRotateColumn = new List<double>();
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath +  "\\Calib.ini";

            string section = "CalibL";
            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            lstCalibCameraDownRC.Clear();
            lstCalibCameraDownXY.Clear();
          
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
                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraDownRC.Add(p);

                p = new Point();
                p.initParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());
                lstCalibCameraDownXY.Add(p);

              

            }
            pCalibUpPosXY.initParam(file, section, "pCalibUpPosXY");
            DGetCalibPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetCalibPosZ", "0"));
            DCalibDownPosX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCalibDownPosX", "0"));
            DCalibDownPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCalibDownPosZ", "0"));
            ICalibDownExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "ICalibDownExposure", "100"));
            ICalibUpExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "ICalibUpExposure", "100"));


            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "左下相机标定");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "左上相机标定");

            strPicRotateName = IniOperate.INIGetStringValue(file, section, "strPicRotateName", "左下相机旋转中心");
            dRoateCenterColumn = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterColumn", "0"));
            dRoateCenterRow = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterRow", "0"));


            //strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料2下相机标定");
            //strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料2上相机标定");


        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath +  "\\Calib.ini";

            string section = "CalibL";
            bool bFlag = true;
            for (int i = 0; i < 9; i++)
            {

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());


            }

            bFlag = bFlag && pCalibUpPosXY.saveParam(file, section, "pCalibUpPosXY");

          
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetCalibPosZ", DGetCalibPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCalibDownPosX", DCalibDownPosX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCalibDownPosZ", DCalibDownPosZ.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "ICalibDownExposure", ICalibDownExposure.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "ICalibUpExposure", ICalibUpExposure.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicRotateName", strPicRotateName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterColumn", dRoateCenterColumn.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterRow", dRoateCenterRow.ToString());

            return bFlag;
        }
        public static double GetUpResulotion()
        {
            try
            {
                double dx = lstCalibCameraUpXY[0].X - lstCalibCameraUpXY[1].X;
                double dr = lstCalibCameraUpRC[0].X - lstCalibCameraUpRC[1].X;
                double dc = lstCalibCameraUpRC[0].Y - lstCalibCameraUpRC[1].Y;
                double sq = Math.Sqrt(dr * dr + dc * dc);
                return Math.Abs(dx / sq);
            }
            catch (Exception)
            {

                return 1;
            }
        }

        public static double GetDownResulotion()
        {
            try
            {
                double dx = lstCalibCameraDownXY[0].X - lstCalibCameraDownXY[1].X;
                double dr = lstCalibCameraDownRC[0].X - lstCalibCameraDownRC[1].X;
                double dc = lstCalibCameraDownRC[0].Y - lstCalibCameraDownRC[1].Y;
                double sq = Math.Sqrt(dr * dr + dc * dc);
                return Math.Abs(dx / sq);
            }
            catch (Exception)
            {

                return 1;
            }
        }

        public static double GetUpCameraAngle()
        {
            try
            {
                double dr = lstCalibCameraUpRC[0].X - lstCalibCameraUpRC[1].X;
                double dc = lstCalibCameraUpRC[0].Y - lstCalibCameraUpRC[1].Y;
                HTuple a, da;
                HOperatorSet.TupleAtan(dr / dc, out a);
                HOperatorSet.TupleDeg(a, out da);
                if ((da[0] > 180) || (da[0] < -180)||(double.IsNaN(da.D)))
                    da[0] = 0;
                return da[0];
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public static double GetDownCameraAngle()
        {
            try
            {
                double dr = lstCalibCameraDownRC[0].X - lstCalibCameraDownRC[1].X;
                double dc = lstCalibCameraDownRC[0].Y - lstCalibCameraDownRC[1].Y;
                HTuple a, da;
                HOperatorSet.TupleAtan(dr / dc, out a);
                HOperatorSet.TupleDeg(a, out da);
                if ((da[0] > 180) || (da[0] < -180) || (double.IsNaN(da.D)))
                    da[0] = 0;
                return da[0];
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}
