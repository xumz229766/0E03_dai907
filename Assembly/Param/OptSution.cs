using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using Tray;
using ImageProcess;
using ConfigureFile;
using System.ComponentModel;
namespace Assembly
{
    /// <summary>
    /// 以取料吸笔建立对象，1-9个
    /// </summary>
     public class OptSution1
    {

       // private Tray.Tray tray = null;
        //public Tray.Tray trayCamera = null;//料盘
        private int iStartPos = 1;//工作起始位
        private int iEndPos = 100;//工作结束位
        private int iCurrentPos = 1;//当前位置
        public bool bFinish = false;//所有盘完成标志
        public int StartPos
        {
            set
            {

                iStartPos = value;
            }
            get { return iStartPos; }
        }
        public int EndPos
        {
            set
            {

                iEndPos = value;
            }
            get { return iEndPos; }
        }

        public int CurrentPos
        {
            set
            {
                iCurrentPos = value;
                if (CurrentPos < StartPos)
                    iCurrentPos = StartPos;
                if (iCurrentPos > EndPos)
                {
                    iCurrentPos = EndPos;
                    bFinish = true;
                }
            }
            get { return iCurrentPos; }
        }

        public List<int> lstTrayNum = new List<int>();
        public List<int> lstIndex1 = new List<int>();//对位点编号1
        public List<int> lstIndex2 = new List<int>();//对位点编号2
        public List<int> lstIndex3 = new List<int>();//对位点编号3
         //取料盘拍照位
        public List<Point> lstP1 = new List<Point>();//对位点1，X对应Column,Y对应Row
        public List<Point> lstP2 = new List<Point>();//对位点2
        public List<Point> lstP3 = new List<Point>();//对位点3
        public List<HTuple> lstHomatCamera = new List<HTuple>();//点位关系
         //取料盘吸笔位
        public List<Point> lstP4 = new List<Point>();
        public List<Point> lstP5 = new List<Point>();
        public List<Point> lstP6 = new List<Point>();
        public List<HTuple> lstHomatSuction = new List<HTuple>();//吸笔对应关系
       
        //取料基本参数
        public int SuctionOrder{get;set;}//吸笔序号
        public bool BUse { get; set; }//是否使用该吸笔
        public string Name { get; set; }//吸笔名称
        public bool BUseCameraUp { get; set; }//使用上相机定位取料
        public double DGetPosZ { get; set; }//取料高度
        public double DGetTime { get; set; }//取料吸真空时间
        public double DPosCameraDownX { get; set; }//下相机拍照位，同时是飞拍触发位 
        public static Point pSafeXYZ = new Point();//安全位
        public static double dFlashStartX = 0;//飞拍起始位
        public static double dFlashEndX = 0;//飞拍结束位,无效
        public static double dFlashZ = 0;//飞拍高度
        public static Point pPutPos = new Point();//放料位
        public bool bPut { get; set; }//放料时是否真空
       
        public static double dPutTime = 0;//破真空时间

        public static double DCorrectPosZ { get; set; }//求心高度
        public static double DCorrectTime { get; set; }//求心时间

        public static Point pDiscard = new Point();//抛料位
        public static double dFalshMakeUpX = 0;//飞拍补偿参数

        public static double dExposureTimeUp = 100;//曝光时间(us)
        public static double dGainUp = 1;//增益
        public static double dLightUp = 1;//光源强度
        public static double dExposureTimeDown = 100;//曝光时间(us)
        public static double dGainDown = 1;//增益
        public static double dLightDown = 1;//光源强度

        public static double dExposureTimeSuction = 100;//曝光时间(us)
        public static double dGainSuction = 1;//增益
        public static double dLightSuction = 1;//光源强度

        public static double dCalibGain = 1;//上相机标定九点标定时相机的增益
        public static double dCalibGainUp = 1;//上下标定时上相机的增益
        public static double dCalibGainDown = 1;//上下标定时下相机的增益
        

        //上下相机标定参数
   
        public Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public static Point pGetCalib = new Point();//吸笔取标定板的位置
        public static Point pCalibCameraUpXY = new Point();//上相机拍照标定板位置
        public static Point pCalibCameraUpRC = new Point();//标定板在上相机的位置
        public static double dCalibCameraDownX = 0;//吸笔下相机拍照的位置
        public static Point pCalibCameraDownRC = new Point();//标定板在下相机的位置
        //public static List<Point> lstCalibCameraUpRC = new List<Point>();//9点标定上相机像素位置
        //public static List<Point> lstCalibCameraUpXY = new List<Point>();//9点标定上相机坐标位置

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置
        public static List<Point> lstCalibCameraDownRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraDownXY = new List<Point>();//标定上相机坐标位置

        //图像处理参数
        public string strPicDownSuctionCenterName = "";//下相机检测吸笔中心的方法
        public string strPicDownProduct = "";//下相机检测产品的方法
        public string strPicCameraUpName = "";//上相机检测产品的方法
        public static string strPicDownCalibName = "";//下相机标定方法
        public static string strPicUpCalibName = "";//上相机标定方法

        public HObject hImageUP = null;//图片存储
        public ProcessFatory pfUp = null;//图像处理
        public ImageResult imgResultUp = new ImageResult();//处理结果

        public HObject hImageDown = null;//图片存储
        public ProcessFatory pfDown = null;//图像处理
        public ImageResult imgResultDown = new ImageResult();//处理结果

        public static double dUpAng = 0;
        public static double dDownAng = 0;
        public OptSution1(int iOrder)
        {
            SuctionOrder = iOrder;
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
                return Math.Abs(dx / dr);
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
                HOperatorSet.TupleAtan(dc / dr, out a);
                HOperatorSet.TupleDeg(a, out da);
                return da;
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
                HOperatorSet.TupleAtan(dc / dr, out a);
                HOperatorSet.TupleDeg(a, out da);
                return da;
            }
            catch (Exception)
            {

                return 0;
            }

        }


        //使用之前调用initImage，进行初化
        public void actionUp(HObject image)
        {
            CommonSet.WriteInfo("上相机图像" + SuctionOrder.ToString() + "处理开始");
            HOperatorSet.CopyImage(image, out hImageUP);
            if (pfUp != null)
            {
                pfUp.hImage = hImageUP;
                pfUp.Action(hImageUP);
                imgResultUp.StrResult = pfUp.strOutputString;
            }
            imgResultUp.bStatus = true;
        }
        public void InitImageUp(string strProcessName)
        {
            imgResultUp.bStatus = false;
            pfUp = CommonSet.imageManager.GetProcessFactory(strProcessName);
            if (pfUp != null)
                pfUp.hImage = null;
            imgResultUp.StrResult = "";
        }


        //使用之前调用initImage，进行初化
        public void actionDown(HObject image)
        {
            CommonSet.WriteInfo("下相机图像" + SuctionOrder.ToString() + "处理开始");
            HOperatorSet.CopyImage(image, out hImageDown);
            if (pfDown != null)
            {
                pfDown.hImage = hImageDown;
                pfDown.Action(hImageDown);
                imgResultDown.StrResult = pfDown.strOutputString;
            }
            imgResultDown.bStatus = true;
        }
        public void InitImageDown(string strProcessName)
        {
            imgResultDown.bStatus = false;
            pfDown = CommonSet.imageManager.GetProcessFactory(strProcessName);
            if (pfDown != null)
                pfDown.hImage = null;
            imgResultDown.StrResult = "";
        }

        public void InitParam()
        {
            string file = CommonSet.strProductParamPath+CommonSet.strProductName+"\\OPTSuction.ini";

            string section = "OptSuction1_" + SuctionOrder.ToString();
            CommonSet.StringToList(ref lstTrayNum, IniOperate.INIGetStringValue(file, section, "lstTrayNum", ""));


            CommonSet.StringToList(ref lstIndex1, IniOperate.INIGetStringValue(file, section, "lstIndex1", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex2, IniOperate.INIGetStringValue(file, section, "lstIndex2", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex3, IniOperate.INIGetStringValue(file, section, "lstIndex3", "1,1,1,1,1,1,1,1,1"));
            lstP1.Clear();
            lstP2.Clear();
            lstP3.Clear();
            lstP4.Clear();
            lstP5.Clear();
            lstP6.Clear();
            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            lstCalibCameraDownRC.Clear();
            lstCalibCameraDownXY.Clear();
            lstHomatCamera.Clear();
            lstHomatSuction.Clear();
            for (int i = 0; i < 9; i++)
            {
                
                Point p = new Point();
                p.initParam(file, section, "p1_"+  (i + 1).ToString());
                lstP1.Add(p);
                p = new Point();
                p.initParam(file, section, "p2_" + (i + 1).ToString());
                lstP2.Add(p);
                p = new Point();
                p.initParam(file, section, "p3_" + (i + 1).ToString());
                lstP3.Add(p);
                p = new Point();
                p.initParam(file, section, "p4_" + (i + 1).ToString());
                lstP4.Add(p);
                p = new Point();
                p.initParam(file, section, "p5_" + (i + 1).ToString());
                lstP5.Add(p);
                p = new Point();
                p.initParam(file, section, "p6_" + (i + 1).ToString());
                lstP6.Add(p);
                //lstP2[i].initParam(file, section, "p2_" + (i + 1).ToString());

                //lstP3[i].initParam(file, section, "p3_" + (i + 1).ToString());

                //lstP4[i].initParam(file, section, "p4_" + (i + 1).ToString());

                //lstP5[i].initParam(file, section, "p5_" + (i + 1).ToString());

                //lstP6[i].initParam(file, section, "p6_" + (i + 1).ToString());
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
                lstHomatCamera.Add(null);
                lstHomatSuction.Add(null);

                EndPos = GetCount();
                //lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                //lstCalibCameraUpXY[i].initParam(file, section, "lstCalibCameraUpXY_" + (i + 1).ToString());
            }

          
            BUse = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUse", "true"));
            bPut = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bPut", "false"));
            BUseCameraUp = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUseCameraUp", "true"));
            Name = IniOperate.INIGetStringValue(file, section, "Name", "吸笔"+SuctionOrder.ToString());
            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0"));
            DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));
            pSuctionCenter.initParam(file, section, "pSuctionCenter");
            strPicDownSuctionCenterName = IniOperate.INIGetStringValue(file, section, "strPicDownSuctionCenterName", "OptSuction" + SuctionOrder.ToString());
            strPicDownProduct = IniOperate.INIGetStringValue(file, section, "strPicDownProduct", "OptDownS" + SuctionOrder.ToString());
            strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "OptS"+SuctionOrder.ToString());

            CurrentPos = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "CurrentPos", "1"));




           
        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction1_" + SuctionOrder.ToString();
            bool bFlag = true;

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTrayNum", CommonSet.ListToString(lstTrayNum));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex1", CommonSet.ListToString(lstIndex1));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex2", CommonSet.ListToString(lstIndex2));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex3", CommonSet.ListToString(lstIndex3));
            for (int i = 0; i < 9; i++)
            {

                bFlag = bFlag && lstP1[i].saveParam(file, section, "p1_" + (i + 1).ToString());

                bFlag = bFlag && lstP2[i].saveParam(file, section, "p2_" + (i + 1).ToString());

                bFlag = bFlag && lstP3[i].saveParam(file, section, "p3_" + (i + 1).ToString());

                bFlag = bFlag && lstP4[i].saveParam(file, section, "p4_" + (i + 1).ToString());

                bFlag = bFlag && lstP5[i].saveParam(file, section, "p5_" + (i + 1).ToString());

                bFlag = bFlag && lstP6[i].saveParam(file, section, "p6_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());

            
            }

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUse", BUse.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bPut", bPut.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUseCameraUp", BUseCameraUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "Name", Name);
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPosCameraDownX", DPosCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownSuctionCenterName", strPicDownSuctionCenterName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownProduct", strPicDownProduct.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
            bFlag = bFlag && pSuctionCenter.saveParam(file, section, "pSuctionCenter");
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "CurrentPos", CurrentPos.ToString());
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction1" ;

            pSafeXYZ.initParam(file, section, "pSafeXYZ");
            pPutPos.initParam(file, section, "pPutPos");
            pGetCalib.initParam(file, section, "pGetCalib");
            pCalibCameraUpXY.initParam(file, section, "pCalibCameraUpXY");
            pCalibCameraUpRC.initParam(file, section, "pCalibCameraUpRC");
           

            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料1下相机标定");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料1上相机标定");

            dFlashStartX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashStartX", "0"));
            dFlashEndX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashEndX", "10"));
            dPutTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPutTime", "0.1"));
            dCalibCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibCameraDownX", "0"));
            dFlashZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashZ", "10"));

            pDiscard.initParam(file, section, "pDiscard");
            dFalshMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFalshMakeUpX", "0"));
            dExposureTimeUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeUp", "100"));
            dGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainUp", "1"));
            dLightUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightUp", "0"));

            dExposureTimeDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeDown", "100"));
            dGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainDown", "1"));
            dLightDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightDown", "1"));
            dExposureTimeSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeSuction", "100"));
            dGainSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainSuction", "1"));
            dLightSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightSuction", "1"));

            dCalibGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGain", "1"));
            dCalibGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainUp", "1"));
            dCalibGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainDown", "1"));

            DCorrectPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectPosZ", "0"));
            DCorrectTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectTime", "0"));

            dUpAng = GetUpCameraAngle();
            dDownAng = GetDownCameraAngle();
            if (dUpAng > -180 && dUpAng < 180)
            { }
            else
            {
                dUpAng = 0;
            }
            if (dDownAng > -180 && dDownAng < 180)
            { }
            else
            {
                dDownAng = 0;
            }
        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction1";
            bool bFlag = true;
       
            bFlag = bFlag && pSafeXYZ.saveParam(file, section, "pSafeXYZ");
            bFlag = bFlag && pPutPos.saveParam(file, section, "pPutPos");
            bFlag = bFlag && pGetCalib.saveParam(file, section, "pGetCalib");
            bFlag = bFlag && pCalibCameraUpXY.saveParam(file, section, "pCalibCameraUpXY");
            bFlag = bFlag && pCalibCameraUpRC.saveParam(file, section, "pCalibCameraUpRC");

            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashStartX", dFlashStartX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashEndX", dFlashEndX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutTime", dPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibCameraDownX", dCalibCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashZ", dFlashZ.ToString());

            bFlag = bFlag && pDiscard.saveParam(file, section, "pDiscard");
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFalshMakeUpX", dFalshMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeUp", dExposureTimeUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainUp", dGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightUp", dLightUp.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeDown", dExposureTimeDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainDown", dGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightDown", dLightDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeSuction", dExposureTimeSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainSuction", dGainSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightSuction", dLightSuction.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGain", dCalibGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainUp", dCalibGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainDown", dCalibGainDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectPosZ", DCorrectPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectTime", DCorrectTime.ToString());

            return bFlag;
        }

        //通过相机三个对位点，创建整盘的关系
        public bool createPointRelationByCamera(int trayNum)
        {
            //if (tray == null)
            //    return false;
           
            int index = lstTrayNum.IndexOf(trayNum);
              Tray.Tray tray = TrayFactory.dic_Tray[trayNum.ToString()];
            if (lstP1[index].isEmpty() || lstP2[index].isEmpty() || lstP3[index].isEmpty())
                return false;
            Index i1 = tray.dic_Index[lstIndex1[index]];
            Index i2 = tray.dic_Index[lstIndex2[index]];
            Index i3 = tray.dic_Index[lstIndex3[index]];
            HTuple inputX = new HTuple();
            HTuple inputY = new HTuple();
            HTuple inputR = new HTuple();
            HTuple inputC = new HTuple();
            inputX = inputX.TupleConcat(lstP1[index].X).TupleConcat(lstP2[index].X).TupleConcat(lstP3[index].X);
            inputY = inputY.TupleConcat(lstP1[index].Y).TupleConcat(lstP2[index].Y).TupleConcat(lstP3[index].Y);
            inputR = inputR.TupleConcat(i1.Row).TupleConcat(i2.Row).TupleConcat(i3.Row);
            inputC = inputC.TupleConcat(i1.Col).TupleConcat(i2.Col).TupleConcat(i3.Col);
            HTuple outHomat;
            HOperatorSet.VectorToHomMat2d(inputR, inputC, inputY, inputX, out outHomat);
            lstHomatCamera[index] = outHomat;
            return true;
        }
        //通过吸笔三个对位点，创建整盘的关系
        public bool createPointRelationBySuction(int trayNum)
        {
            //if (tray == null)
            //    return false;
            int index = lstTrayNum.IndexOf(trayNum);
            Tray.Tray tray = TrayFactory.dic_Tray[trayNum.ToString()];
            if (lstP4[index].isEmpty() || lstP5[index].isEmpty() || lstP6[index].isEmpty())
                return false;
            Index i1 = tray.dic_Index[lstIndex1[index]];
            Index i2 = tray.dic_Index[lstIndex2[index]];
            Index i3 = tray.dic_Index[lstIndex3[index]];
            HTuple inputX = new HTuple();
            HTuple inputY = new HTuple();
            HTuple inputR = new HTuple();
            HTuple inputC = new HTuple();
            inputX = inputX.TupleConcat(lstP4[index].X).TupleConcat(lstP5[index].X).TupleConcat(lstP6[index].X);
            inputY = inputY.TupleConcat(lstP4[index].Y).TupleConcat(lstP5[index].Y).TupleConcat(lstP6[index].Y);
            inputR = inputR.TupleConcat(i1.Row).TupleConcat(i2.Row).TupleConcat(i3.Row);
            inputC = inputC.TupleConcat(i1.Col).TupleConcat(i2.Col).TupleConcat(i3.Col);
            HTuple outHomat;
            HOperatorSet.VectorToHomMat2d(inputR, inputC, inputY, inputX, out outHomat);
            lstHomatSuction[index] = outHomat;
            return true;
        }




        //设置工作的起始位
        public void SetStartPos(int pos)
        {
            if (pos < 1)
                return;
            if (pos > iEndPos)
                return;
            StartPos = pos;
            iCurrentPos = pos;


        }
        //设置工作的结束位
        public void SetEndPos(int pos)
        {
            if (pos < StartPos)
                return;
            if (pos > GetCount())
                return;
            iEndPos = pos;
            //int len = lstTrayNum.Count;
            //// bool bResult = false;
            //int iTemp = 0;
            //for (int i = 0; i < len; i++)
            //{
            //    int endPos = TrayFactory.dic_Tray[lstTrayNum[i].ToString()].dic_Index.Count;

            //    if (pos > endPos + iTemp)
            //    {
            //        iTemp = iTemp + endPos;
            //        //TrayFactory.dic_Tray[lstTrayNum[i].ToString()].bFinish = true;
            //    }
            //    else
            //    {
            //        TrayFactory.dic_Tray[lstTrayNum[i].ToString()].EndPos = pos - iTemp;

            //       // iCurrentPos = pos;
            //        iEndPos = pos;
            //    }
            //}
        }
        //设置当前位置
        public void SetCurrentPos(int pos)
        {
            if ((pos >= iStartPos) && (pos <= iEndPos))
            {
                iCurrentPos = pos;
            }
        }
        //获取所有盘中料的数量
        public int GetCount()
        {
            int iCount = 0;
            int len = lstTrayNum.Count;
            for (int i = 0; i < len; i++)
            {
                iCount = iCount + TrayFactory.dic_Tray[lstTrayNum[i].ToString()].dic_Index.Count;
            }
            return iCount;
        }
        //通过点位序号，查找LstTrayNum的索引位置，不是值
        public int GetListIndexByPos(int pos)
        {
            int len = lstTrayNum.Count;
            // bool bResult = false;
            int iTemp = 0;
            int iTrayIndex = 0;
            //查找对应的盘
            for (int k = 0; k < len; k++)
            {
                int endPos = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos - iTemp <= endPos)
                {
                    iTrayIndex = k;
                    break;
                }
                else
                {
                    iTemp = iTemp + endPos;
                }
            }
            return iTrayIndex;
        }
        /// <summary>
        /// 初始化盘参数
        /// </summary>
        public void InitTrayParam()
        {
            iStartPos = 1;
            iCurrentPos = 1;
            iEndPos = GetCount();
            bFinish = false;
        }

        /// <summary>
        /// 通过序号获取对应吸笔的坐标
        /// </summary>
        /// <returns>返回实际轴的XY坐标</returns>
        public Point getCoordinateByIndex()
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = CurrentPos;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatSuction[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatSuction[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }
        public Point getCoordinateByIndex(int position)
        {
            int pos = position;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatSuction[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatSuction[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        /// <summary>
        /// 通过序号获取对应相机坐标
        /// </summary>
        /// <returns>返回相机轴的XY坐标</returns>
        public Point getCameraCoordinateByIndex()
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = CurrentPos;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatCamera[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        public Point getCameraCoordinateByIndex(int position)
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = position;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatCamera[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

    }
     /// <summary>
     /// 以取料吸笔建立对象，10-18个
     /// </summary>
    public class OptSution2
    {
        //private Tray.Tray tray = null;
        //public Tray.Tray trayCamera = null;//料盘
        private int iStartPos = 1;//工作起始位
        private int iEndPos = 100;//工作结束位
        private int iCurrentPos = 1;//当前位置
        public bool bFinish = false;//所有盘完成标志
        public int StartPos {
            set { 
                
                iStartPos = value; }
            get { return iStartPos; }
        }
        public int EndPos {
            set
            {

               iEndPos = value;
            }
            get { return iEndPos; }
        }

        public int CurrentPos
        {
            set
            {
                iCurrentPos = value;
                if (CurrentPos < StartPos)
                    iCurrentPos = StartPos;
                if (iCurrentPos > EndPos)
                {
                    iCurrentPos = EndPos;
                    bFinish = true;
                }
            }
            get { return iCurrentPos; }
        }

       
        
        public List<int> lstTrayNum = new List<int>();//存储托盘对应的编号
        public List<int> lstIndex1 = new List<int>();//对位点编号1,在lstTrayNum中所对位的第一个点
        public List<int> lstIndex2 = new List<int>();//对位点编号2
        public List<int> lstIndex3 = new List<int>();//对位点编号3
        //取料盘拍照位
        public List<Point> lstP1 = new List<Point>();//对位点1，X对应Column,Y对应Row
        public List<Point> lstP2 = new List<Point>();//对位点2
        public List<Point> lstP3 = new List<Point>();//对位点3
        public List<HTuple> lstHomatCamera = new List<HTuple>();//点位关系
        //取料盘吸笔位
        public List<Point> lstP4 = new List<Point>();
        public List<Point> lstP5 = new List<Point>();
        public List<Point> lstP6 = new List<Point>();
        public List<HTuple> lstHomatSuction = new List<HTuple>();//吸笔对应关系
       
        //取料基本参数
        public int SuctionOrder { get; set; }//吸笔序号
        public bool BUse { get; set; }//是否使用该吸笔
        public string Name { get; set; }//吸笔名称

        public bool BUseCameraUp { get; set; }//使用上相机定位取料
        public double DGetPosZ { get; set; }//取料高度
        public double DGetTime { get; set; }//取料吸真空时间
        public double DPosCameraDownX { get; set; }//下相机拍照位，同时是飞拍触发位 
        public static Point pSafeXYZ = new Point();//安全位
        public static double dFlashStartX = 0;//飞拍起始位
        public static double dFlashEndX = 0;//飞拍结束位
        public static Point pPutPos = new Point();//放料位
        public bool bPut { get; set; }//放料时是否破真空

        public static double dPutTime = 0;//破真空时间
        public static double dFlashZ = 0;//飞拍高度
        public static Point pDiscard = new Point();//抛料位
        public static double dFalshMakeUpX = 0;//飞拍补偿参数

        public static double DCorrectPosZ { get; set; }//求心高度
        public static double DCorrectTime { get; set; }//求心时间

        public static double dExposureTimeUp = 100;//曝光时间(us)
        public static double dGainUp = 1;//增益
        public static double dLightUp = 1;//光源强度
        public static double dExposureTimeDown = 100;//曝光时间(us)
        public static double dGainDown = 1;//增益
        public static double dLightDown = 1;//光源强度

        public static double dExposureTimeSuction = 100;//吸笔曝光时间(us)
        public static double dGainSuction = 1;//吸笔增益
        public static double dLightSuction = 1;//光源强度

        public static double dCalibGain = 1;//上相机标定九点标定时相机的增益
        public static double dCalibGainUp = 1;//上下标定时上相机的增益
        public static double dCalibGainDown = 1;//上下标定时下相机的增益

        //上下相机标定参数
        public Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public static Point pGetCalib = new Point();//吸笔取标定板的位置
        public static Point pCalibCameraUpXY = new Point();//吸笔上相机拍照标定板位置
        public static Point pCalibCameraUpRC = new Point();//标定板在上相机的位置
        public static double dCalibCameraDownX = 0;//吸笔下相机拍照的位置
        public static Point pCalibCameraDownRC = new Point();//标定板在下相机的位置
        //public static List<Point> lstCalibCameraUpRC = new List<Point>();//9点标定上相机像素位置
        //public static List<Point> lstCalibCameraUpXY = new List<Point>();//9点标定上相机坐标位置

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置
        public static List<Point> lstCalibCameraDownRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraDownXY = new List<Point>();//标定上相机坐标位置


        //图像处理参数
        public string strPicDownSuctionCenterName = "";//下相机检测吸笔中心的方法
        public string strPicDownProduct = "";//下相机检测产品的方法
        public string strPicCameraUpName = "";//上相机检测产品的方法
        public static string strPicDownCalibName = "";//下相机标定方法
        public static string strPicUpCalibName = "";//上相机标定方法

        public HObject hImageUP = null;//图片存储
        public ProcessFatory pfUp = null;//图像处理
        public ImageResult imgResultUp = new ImageResult();//处理结果

        public HObject hImageDown = null;//图片存储
        public ProcessFatory pfDown = null;//图像处理
        public ImageResult imgResultDown = new ImageResult();//处理结果

        public static double dUpAng = 0;
        public static double dDownAng = 0;
         public OptSution2(int iOrder)
        {
            SuctionOrder = iOrder;
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
                 return Math.Abs(dx / dr);
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
                 HOperatorSet.TupleAtan(dc / dr, out a);
                 HOperatorSet.TupleDeg(a, out da);
                 return da;
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
                 HOperatorSet.TupleAtan(dc / dr, out a);
                 HOperatorSet.TupleDeg(a, out da);
                 return da;
             }
             catch (Exception)
             {

                 return 0;
             }

         }


        //使用之前调用initImage，进行初化
        public void actionUp(HObject image)
        {
            CommonSet.WriteInfo("上相机图像" + SuctionOrder.ToString() + "处理开始");
            HOperatorSet.CopyImage(image, out hImageUP);
            if (pfUp != null)
            {
                pfUp.hImage = hImageUP;
                pfUp.Action(hImageUP);
                imgResultUp.StrResult = pfUp.strOutputString;
            }
            imgResultUp.bStatus = true;
        }
        public void InitImageUp(string strProcessName)
        {
            imgResultUp.bStatus = false;
            pfUp = CommonSet.imageManager.GetProcessFactory(strProcessName);
            if (pfUp != null)
                pfUp.hImage = null;
            imgResultUp.StrResult = "";
        }


        //使用之前调用initImage，进行初化
        public void actionDown(HObject image)
        {
            CommonSet.WriteInfo("下相机图像" + SuctionOrder.ToString() + "处理开始");
            HOperatorSet.CopyImage(image, out hImageDown);
            if (pfDown != null)
            {
                pfDown.hImage = hImageDown;
                pfDown.Action(hImageDown);
                imgResultDown.StrResult = pfDown.strOutputString;
            }
            imgResultDown.bStatus = true;
        }
        public void InitImageDown(string strProcessName)
        {
            imgResultDown.bStatus = false;
            pfDown = CommonSet.imageManager.GetProcessFactory(strProcessName);
            if (pfDown != null)
                pfDown.hImage = null;
            imgResultDown.StrResult = "";
        }




        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction2_" + SuctionOrder.ToString();
            CommonSet.StringToList(ref lstTrayNum, IniOperate.INIGetStringValue(file, section, "lstTrayNum", ""));
           
            CommonSet.StringToList(ref lstIndex1, IniOperate.INIGetStringValue(file, section, "lstIndex1", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex2, IniOperate.INIGetStringValue(file, section, "lstIndex2", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex3, IniOperate.INIGetStringValue(file, section, "lstIndex3", "1,1,1,1,1,1,1,1,1"));
            lstP1.Clear();
            lstP2.Clear();
            lstP3.Clear();
            lstP4.Clear();
            lstP5.Clear();
            lstP6.Clear();
            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            lstCalibCameraDownRC.Clear();
            lstCalibCameraDownXY.Clear();
            lstHomatCamera.Clear();
            lstHomatSuction.Clear();
            for (int i = 0; i < 9; i++)
            {
                Point p = new Point();
                p.initParam(file, section, "p1_" + (i + 1).ToString());
                lstP1.Add(p);
                p = new Point();
                p.initParam(file, section, "p2_" + (i + 1).ToString());
                lstP2.Add(p);
                p = new Point();
                p.initParam(file, section, "p3_" + (i + 1).ToString());
                lstP3.Add(p);
                p = new Point();
                p.initParam(file, section, "p4_" + (i + 1).ToString());
                lstP4.Add(p);
                p = new Point();
                p.initParam(file, section, "p5_" + (i + 1).ToString());
                lstP5.Add(p);
                p = new Point();
                p.initParam(file, section, "p6_" + (i + 1).ToString());
                lstP6.Add(p);
                //lstP2[i].initParam(file, section, "p2_" + (i + 1).ToString());

                //lstP3[i].initParam(file, section, "p3_" + (i + 1).ToString());

                //lstP4[i].initParam(file, section, "p4_" + (i + 1).ToString());

                //lstP5[i].initParam(file, section, "p5_" + (i + 1).ToString());

                //lstP6[i].initParam(file, section, "p6_" + (i + 1).ToString());
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
                lstHomatCamera.Add(null);
                lstHomatSuction.Add(null);
                //lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                //lstCalibCameraUpXY[i].initParam(file, section, "lstCalibCameraUpXY_" + (i + 1).ToString());
            }


            BUse = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUse", "true"));
            bPut = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bPut", "false"));
            BUseCameraUp = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUseCameraUp", "true"));
            Name = IniOperate.INIGetStringValue(file, section, "Name", "吸笔" + SuctionOrder.ToString());
            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0"));
            DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));
            pSuctionCenter.initParam(file, section, "pSuctionCenter");
            strPicDownSuctionCenterName = IniOperate.INIGetStringValue(file, section, "strPicDownSuctionCenterName", "");
            strPicDownProduct = IniOperate.INIGetStringValue(file, section, "strPicDownProduct", "OptDownS" + SuctionOrder.ToString());
            strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "");

            CurrentPos = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "CurrentPos", "1"));


            EndPos = GetCount();




        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction2_" + SuctionOrder.ToString();
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTrayNum", CommonSet.ListToString(lstTrayNum));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex1", CommonSet.ListToString(lstIndex1));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex2", CommonSet.ListToString(lstIndex2));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex3", CommonSet.ListToString(lstIndex3));
            for (int i = 0; i < 9; i++)
            {

                bFlag = bFlag && lstP1[i].saveParam(file, section, "p1_" + (i + 1).ToString());

                bFlag = bFlag && lstP2[i].saveParam(file, section, "p2_" + (i + 1).ToString());

                bFlag = bFlag && lstP3[i].saveParam(file, section, "p3_" + (i + 1).ToString());

                bFlag = bFlag && lstP4[i].saveParam(file, section, "p4_" + (i + 1).ToString());

                bFlag = bFlag && lstP5[i].saveParam(file, section, "p5_" + (i + 1).ToString());

                bFlag = bFlag && lstP6[i].saveParam(file, section, "p6_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());


            }

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUse", BUse.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bPut", bPut.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUseCameraUp", BUseCameraUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "Name", Name);
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPosCameraDownX", DPosCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownSuctionCenterName", strPicDownSuctionCenterName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownProduct", strPicDownProduct.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
            bFlag = bFlag && pSuctionCenter.saveParam(file, section, "pSuctionCenter");
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "CurrentPos", CurrentPos.ToString());
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction2";

            pSafeXYZ.initParam(file, section, "pSafeXYZ");
            pPutPos.initParam(file, section, "pPutPos");
            pGetCalib.initParam(file, section, "pGetCalib");
            pCalibCameraUpXY.initParam(file, section, "pCalibCameraUpXY");
            pCalibCameraUpRC.initParam(file, section, "pCalibCameraUpRC");


            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料2下相机标定");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料2上相机标定");

            dFlashStartX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashStartX", "0"));
            dFlashEndX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashEndX", "10"));
            dPutTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPutTime", "0.1"));
            dCalibCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibCameraDownX", "0"));

            dFlashZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashZ", "10"));

            pDiscard.initParam(file, section, "pDiscard");
            dFalshMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFalshMakeUpX", "0"));
            dExposureTimeUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeUp", "100"));
            dGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainUp", "1"));
            dLightUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightUp", "0"));

            dExposureTimeDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeDown", "100"));
            dGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainDown", "1"));
            dLightDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightDown", "1"));
            dExposureTimeSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeSuction", "100"));
            dGainSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainSuction", "1"));
            dLightSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightSuction", "1"));

            dCalibGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGain", "1"));
            dCalibGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainUp", "1"));
            dCalibGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainDown", "1"));

            DCorrectPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectPosZ", "0"));
            DCorrectTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectTime", "0"));

            dUpAng = GetUpCameraAngle();
            dDownAng = GetDownCameraAngle();
            if (dUpAng > -180 && dUpAng < 180)
            { }
            else
            {
                dUpAng = 0;
            }
            if (dDownAng > -180 && dDownAng < 180)
            { }
            else
            {
                dDownAng = 0;
            }
        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\OPTSuction.ini";

            string section = "OptSuction2";
            bool bFlag = true;

            bFlag = bFlag && pSafeXYZ.saveParam(file, section, "pSafeXYZ");
            bFlag = bFlag && pPutPos.saveParam(file, section, "pPutPos");
            bFlag = bFlag && pGetCalib.saveParam(file, section, "pGetCalib");
            bFlag = bFlag && pCalibCameraUpXY.saveParam(file, section, "pCalibCameraUpXY");
            bFlag = bFlag && pCalibCameraUpRC.saveParam(file, section, "pCalibCameraUpRC");

            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashStartX", dFlashStartX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashEndX", dFlashEndX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutTime", dPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibCameraDownX", dCalibCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashZ", dFlashZ.ToString());

            bFlag = bFlag && pDiscard.saveParam(file, section, "pDiscard");
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFalshMakeUpX", dFalshMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeUp", dExposureTimeUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainUp", dGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightUp", dLightUp.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeDown", dExposureTimeDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainDown", dGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightDown", dLightDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeSuction", dExposureTimeSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainSuction", dGainSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightSuction", dLightSuction.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGain", dCalibGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainUp", dCalibGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainDown", dCalibGainDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectPosZ", DCorrectPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectTime", DCorrectTime.ToString());
            return bFlag;
        }

        //通过相机三个对位点，创建整盘的关系
        public bool createPointRelationByCamera(int trayNum)
        {
            //if (tray == null)
            //    return false;
            Tray.Tray tray = TrayFactory.dic_Tray[trayNum.ToString()];
            int index = lstTrayNum.IndexOf(trayNum);
            if (lstP1[index].isEmpty() || lstP2[index].isEmpty() || lstP3[index].isEmpty())
                return false;
            Index i1 = tray.dic_Index[lstIndex1[index]];
            Index i2 = tray.dic_Index[lstIndex2[index]];
            Index i3 = tray.dic_Index[lstIndex3[index]];
            HTuple inputX = new HTuple();
            HTuple inputY = new HTuple();
            HTuple inputR = new HTuple();
            HTuple inputC = new HTuple();
            inputX = inputX.TupleConcat(lstP1[index].X).TupleConcat(lstP2[index].X).TupleConcat(lstP3[index].X);
            inputY = inputY.TupleConcat(lstP1[index].Y).TupleConcat(lstP2[index].Y).TupleConcat(lstP3[index].Y);
            inputR = inputR.TupleConcat(i1.Row).TupleConcat(i2.Row).TupleConcat(i3.Row);
            inputC = inputC.TupleConcat(i1.Col).TupleConcat(i2.Col).TupleConcat(i3.Col);
            HTuple outHomat;
            HOperatorSet.VectorToHomMat2d(inputR, inputC, inputY, inputX, out outHomat);
            lstHomatCamera[index] = outHomat;
            return true;
        }
        //通过吸笔三个对位点，创建整盘的关系
        public bool createPointRelationBySuction(int trayNum)
        {
            //if (tray == null)
            //    return false;
            Tray.Tray tray = TrayFactory.dic_Tray[trayNum.ToString()];
            int index = lstTrayNum.IndexOf(trayNum);
            if (lstP4[index].isEmpty() || lstP5[index].isEmpty() || lstP6[index].isEmpty())
                return false;
            Index i1 = tray.dic_Index[lstIndex1[index]];
            Index i2 = tray.dic_Index[lstIndex2[index]];
            Index i3 = tray.dic_Index[lstIndex3[index]];
            HTuple inputX = new HTuple();
            HTuple inputY = new HTuple();
            HTuple inputR = new HTuple();
            HTuple inputC = new HTuple();
            inputX = inputX.TupleConcat(lstP4[index].X).TupleConcat(lstP5[index].X).TupleConcat(lstP6[index].X);
            inputY = inputY.TupleConcat(lstP4[index].Y).TupleConcat(lstP5[index].Y).TupleConcat(lstP6[index].Y);
            inputR = inputR.TupleConcat(i1.Row).TupleConcat(i2.Row).TupleConcat(i3.Row);
            inputC = inputC.TupleConcat(i1.Col).TupleConcat(i2.Col).TupleConcat(i3.Col);
            HTuple outHomat;
            HOperatorSet.VectorToHomMat2d(inputR, inputC, inputY, inputX, out outHomat);
            lstHomatSuction[index] = outHomat;
            return true;
        }

        

        
        //设置工作的起始位
        public void SetStartPos(int pos)
        {
            if (pos < 1)
                return;
            if (pos > iEndPos)
                return;
            StartPos = pos;
            iCurrentPos = pos;
        
            
        }
        //设置工作的结束位
        public void SetEndPos(int pos)
        {
            if (pos < StartPos)
                return;
            if (pos > GetCount())
                return;
            iEndPos = pos;
            //int len = lstTrayNum.Count;
            //// bool bResult = false;
            //int iTemp = 0;
            //for (int i = 0; i < len; i++)
            //{
            //    int endPos = TrayFactory.dic_Tray[lstTrayNum[i].ToString()].dic_Index.Count;

            //    if (pos > endPos + iTemp)
            //    {
            //        iTemp = iTemp + endPos;
            //        //TrayFactory.dic_Tray[lstTrayNum[i].ToString()].bFinish = true;
            //    }
            //    else
            //    {
            //        TrayFactory.dic_Tray[lstTrayNum[i].ToString()].EndPos = pos - iTemp;
                    
            //       // iCurrentPos = pos;
            //        iEndPos = pos;
            //    }
            //}
        }
        //设置当前位置
        public void SetCurrentPos(int pos)
        {
            if ((pos >= iStartPos) && (pos <= iEndPos))
            {
                iCurrentPos = pos;
            }
        }
        //获取所有盘中料的数量
        public int GetCount()
        {
            int iCount = 0;
            int len = lstTrayNum.Count;
            for (int i = 0; i < len; i++)
            {
                iCount = iCount + TrayFactory.dic_Tray[lstTrayNum[i].ToString()].dic_Index.Count;
            }
            return iCount;
        }
        //通过点位序号，查找LstTrayNum的索引位置，不是值
        public int GetListIndexByPos(int pos)
        {
            int len = lstTrayNum.Count;
            // bool bResult = false;
            int iTemp = 0;
            int iTrayIndex = 0;
            //查找对应的盘
            for (int k = 0; k < len; k++)
            {
                int endPos = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos - iTemp <= endPos)
                {
                    iTrayIndex = k;
                    break;
                }
                else
                {
                    iTemp = iTemp + endPos;
                }
            }
            return iTrayIndex;
        }
        /// <summary>
        /// 初始化盘参数
        /// </summary>
        public void InitTrayParam()
        { 
            iStartPos = 1;
            iCurrentPos = 1;
            iEndPos = GetCount();
            bFinish = false;
        }

        /// <summary>
        /// 通过序号获取对应吸笔的坐标
        /// </summary>
        /// <returns>返回实际轴的XY坐标</returns>
        public Point getCoordinateByIndex()
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = CurrentPos;
             //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatSuction[index]==null)
            {
                createPointRelationBySuction(lstTrayNum[index]);
                
            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }
                
            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatSuction[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }
        public Point getCoordinateByIndex(int position)
        {
            int pos = position;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatSuction[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatSuction[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        /// <summary>
        /// 通过序号获取对应相机坐标
        /// </summary>
        /// <returns>返回相机轴的XY坐标</returns>
        public Point getCameraCoordinateByIndex()
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = CurrentPos;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatCamera[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        public Point getCameraCoordinateByIndex(int position)
        {
            //if (homat == null)
            //    createPointRelation();
            int pos = position;
            //通过位置查盘所在List序号
            int index = GetListIndexByPos(pos);
            if (lstHomatCamera[index] == null)
            {
                createPointRelationBySuction(lstTrayNum[index]);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[lstTrayNum[index].ToString()];
            //从包含的盘中找到所在位置
            int pos1 = pos;
            for (int k = 0; k < index; k++)
            {
                int count = TrayFactory.dic_Tray[lstTrayNum[k].ToString()].dic_Index.Count;
                if (pos1 > count)
                {
                    pos1 = pos1 - count;
                }

            }
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[pos1];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
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
                    dAngle = 360-Convert.ToDouble(str[1]);
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
