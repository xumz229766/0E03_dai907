using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using ImageProcess;
using ConfigureFile;
namespace Assembly
{
    public class AssembleSuction1
    {
       
        //基本参数
        public int SuctionOrder { get; set; }//吸笔序号
        public bool BUse { get; set; }//是否使用该吸笔
        public string Name { get; set; }//吸笔名称
       
       
        //组装参数
    
        public double dMakeUpX{ get;set;}//工位1 X方向补正值
        public double dMakeUpY { get; set; }//工位1 Y方向补正值
        public double dMakeUpX2 { get; set; }//工位2 X方向补正值
        public double dMakeUpY2 { get; set; }//工位2 Y方向补正值
        public double dAssemblePosZ1 { get;set;}//组装高度1
        public double dAssemblePosZ2 { get; set; }//组装高度2
        public double dPreH1 {get; set;}//工位1第1次组装下降高度
        public double dPreH2 { get; set; }//工位2第次组装下降高度 
        
        public double dAssembleH { get; set; }//组装1高度
        public double dAssembleH2 { get; set; }//组装2高度
        public double dAssembleOver { get; set; }//过压量
        public double DPosCameraDownX { get; set; }//下相机拍照位，同时是飞拍触发位 
        public Point pAssemblePos1 { get; set; }
        public Point pAssemblePos2 { get; set; }
        public static Point pCamBarrelPos1 = new Point();//拍摄镜筒位置1，同时也是飞拍位置
        public static Point pCamBarrelPos2 = new Point();//拍摄镜筒位置2，同时也是飞拍位置
        public static double DGetPosZ { get; set; }//取料高度
        public static double DGetTime { get; set; }//取料吸真空时间
        public static double DCorrectPosZ { get; set; }//求心高度
        public static double DCorrectTime { get;set;}//求心时间
       // public static double dDist2 = 1;//二次组装下降高度


        public bool bCam { get; set; }//组装时，是否拍照
        public bool bAutoTestHeight { get; set; }//组装时，是否根据设定压力自动测高

       
        public static Point pSafeXYZ = new Point();//安全位
        public static double dFlashStartX = 0;//飞拍起始位,也是取料位
        public static double dFlashEndX = 0;//飞拍结束位

        public static double dFalshMakeUpX = 0;//飞拍补正

        public static double dExposureTimeUp = 100;//曝光时间(us)
        public static double dGainUp = 1;//增益
        public static double dLightUp = 1;//光源强度
        public static double dExposureTimeDown = 100;//曝光时间(us)
        public static double dGainDown = 1;//增益
        public static double dLightDown = 1;//光源强度

        public static double dExposureTimeSuction = 100;//曝光时间(us)
        public static double dGainSuction = 1;//增益

        public static double dCalibGain = 1;//上相机标定九点标定时相机的增益
        public static double dCalibGainUp = 1;//上下标定时上相机的增益
        public static double dCalibGainDown = 1;//上下标定时下相机的增益

        public static double dResultTestGain = 1;//验证组装精度时的增益
        public static double dResultPutZ = 0;//验证时的高度
        public static string strResultSuctionName = "";//验证组装时标定块的检测方法
        public static string strResultLenName = "";//验证组装时固定块的检测方法

        public static Point pDiscardC1 = new Point();//C1组装抛料位1
        public static Point pDiscardC2 = new Point();//C1组装抛料位2
        //public static Point pDiscardC21 = new Point();//C2组装抛料位1
        //public static Point pDiscardC22 = new Point();//C2组装抛料位2

        public static Point pCheckSuction = new Point();//吸笔1平面校正位，其它吸笔以第一支吸笔X方向-25mm的距离换算出来
        public double dCalibPosZ = 0;//标定块拍照时的高度
     


        //不保存
        public double dAngleSet = 0;//组装设置角度
        public double dPressureSet = 0;//组装设定压力       
        public double dAssemblVel = 5;//组装速度
        public double dPutDelay = 0;//组装破真空时间
        //public double dAssembleH = 0;//组装高度
        //public double dAssembleH2 = 0;//组装二次高度

        public bool bTwo = false;//使用二次高度      
        public double dKeepTime = 0;//组装保压时间
        public Point pAssembly1 = new Point();//手动组装位1
        public Point pAssembly2 = new Point();//手动组装位2 


        //上下相机标定参数
       public Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public static Point pGetCalib = new Point();//吸笔取标定板的位置
        public static Point pCalibCameraUpXY = new Point();//吸笔上相机拍照标定板位置
        public static Point pCalibCameraUpXY2 = new Point();//标定板在上相机的位置
        public static double dCalibCameraDownX = 0;//吸笔下相机拍照的位置
        public static Point pCalibCameraDownRC = new Point();//标定板在下相机的位置
        public static List<Point> lstCalibCameraUpRC1 = new List<Point>();//9点标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY1 = new List<Point>();//9点标定上相机坐标位置
        public static List<Point> lstCalibCameraUpRC2 = new List<Point>();//9点标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY2 = new List<Point>();//9点标定上相机坐标位置

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置
        public static List<Point> lstCalibCameraDownRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraDownXY = new List<Point>();//标定上相机坐标位置


        //图像处理参数
        public string strPicDownSuctionCenterName = "";//下相机检测吸笔中心的方法
        public string strPicDownProduct = "";//下相机检测产品的方法
        public static string strPicCameraUpCheckName = "";//上相机检测产品的方法
        public static string strPicDownCalibName = "";//下相机标定方法
        public static string strPicUpCalibName = "";//上相机标定方法

       

        public static HObject hImageUP = null;//图片存储
        public static ProcessFatory pfUp = null;//图像处理
        public static ImageResult imgResultUp = new ImageResult();//处理结果

        public HObject hImageDown = null;//图片存储
        public ProcessFatory pfDown = null;//图像处理
        public ImageResult imgResultDown = new ImageResult();//处理结果
        public double dCamDownX = 0;
        public double dCamDownY = 0;
        public static double dCamUpX = 0;
        public static double dCamUpY = 0;

        public static double dUpAng = 0;
        public static double dDownAng = 0;
        

        public AssembleSuction1(int _order)
        {
            SuctionOrder = _order;
        }
        //使用之前调用initImage，进行初化
        public static void actionUp(HObject image)
        {
            CommonSet.WriteInfo("上相机图像处理开始");
            HOperatorSet.CopyImage(image, out hImageUP);
            if (pfUp != null)
            {
                pfUp.hImage = hImageUP;
                pfUp.Action(hImageUP);
                imgResultUp.StrResult = pfUp.strOutputString;
            }
            imgResultUp.bStatus = true;
        }
        public static void InitImageUp(string strProcessName)
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

        public static double GetUpResulotion()
        {
            try
            {
                //double dx = lstCalibCameraUpXY[0].X - lstCalibCameraUpXY[1].X;
                //double dr = lstCalibCameraUpRC[0].X - lstCalibCameraUpRC[1].X;
                //double dc = lstCalibCameraUpRC[0].Y - lstCalibCameraUpRC[1].Y;
                //double sq = Math.Sqrt(dr*dr+dc*dc);
                //return Math.Abs(dx/sq);

                return 0.0055349685475412;
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
                //return 0.0041320435847957;
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



        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction1_" + SuctionOrder.ToString();
            

            BUse = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUse", "true"));
            bCam = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bCam", "false"));
            bAutoTestHeight = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bAutoTestHeight", "false"));
          
            Name = IniOperate.INIGetStringValue(file, section, "Name", "");
            dAssemblePosZ1 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemblePosZ1", "0"));
            dAssemblePosZ2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemblePosZ2", "0"));
            DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));
            dPutDelay = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPutDelay", "0.1"));
            //DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));

            strPicDownSuctionCenterName = IniOperate.INIGetStringValue(file, section, "strPicDownSuctionCenterName", "OptSuctionC" + SuctionOrder.ToString());
            strPicDownProduct = IniOperate.INIGetStringValue(file, section, "strPicDownProduct", "AssemDown" + SuctionOrder.ToString());


            dMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpX", "0"));
            dMakeUpY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpY", "0"));
            dMakeUpX2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpX2", "0"));
            dMakeUpY2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpY2", "0"));
            dPreH1 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPreH1", "1"));
            dPreH2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPreH2", "1"));

            dAssembleH = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleH", "1"));
            dAssembleH2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleH2", "1"));
            dAssembleOver = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleOver", "1"));

            dCalibPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibPosZ", "0"));

            pSuctionCenter.initParam(file, section, "pSuctionCenter");
            pAssemblePos1 = new Point();
            pAssemblePos2 = new Point();
            pAssemblePos1.initParam(file, section, "pAssemblePos1");
            pAssemblePos2.initParam(file, section, "pAssemblePos2");


        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction1_" + SuctionOrder.ToString();
            bool bFlag = true;

          
           

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUse", BUse.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bCam", bCam.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bAutoTestHeight", bAutoTestHeight.ToString());
           // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUseCameraUp", BUseCameraUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "Name", Name);
            

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPosCameraDownX", DPosCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemblePosZ1", dAssemblePosZ1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemblePosZ2", dAssemblePosZ2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutDelay", dPutDelay.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownProduct", strPicDownProduct.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpCheckName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownSuctionCenterName", strPicDownSuctionCenterName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpX", dMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpY", dMakeUpY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpX2", dMakeUpX2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpY2", dMakeUpY2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPreH1", dPreH1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPreH2", dPreH2.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleH", dAssembleH.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleH2", dAssembleH2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleOver", dAssembleOver.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibPosZ", dCalibPosZ.ToString());

            bFlag = bFlag && pSuctionCenter.saveParam(file, section, "pSuctionCenter");
            bFlag = bFlag && pAssemblePos1.saveParam(file, section, "pAssemblePos1");
            bFlag = bFlag && pAssemblePos2.saveParam(file, section, "pAssemblePos2");
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction1";

            pCamBarrelPos1.initParam(file, section, "pCamBarrelPos1");
            pCamBarrelPos2.initParam(file, section, "pCamBarrelPos2");

            pSafeXYZ.initParam(file, section, "pSafeXYZ");
           
            pGetCalib.initParam(file, section, "pGetCalib");
            pCalibCameraUpXY.initParam(file, section, "pCalibCameraUpXY");
            pCalibCameraUpXY2.initParam(file, section, "pCalibCameraUpXY2");

            pDiscardC1.initParam(file, section, "pDiscardC1");
            pDiscardC2.initParam(file, section, "pDiscardC2");
            //pDiscardC21.initParam(file, section, "pDiscardC21");
            //pDiscardC22.initParam(file, section, "pDiscardC22");

           

            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "组装1下相机标定");
            strPicCameraUpCheckName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpCheckName", "");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "组装1上相机标定");

            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0"));
            DCorrectPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectPosZ", "0"));
            DCorrectTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectTime", "0"));

           


            dResultTestGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dResultTestGain", "1"));
            dResultPutZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dResultPutZ", "1"));
            strResultLenName = IniOperate.INIGetStringValue(file, section, "strResultLenName", "固定块验证检测设置");
            strResultSuctionName = IniOperate.INIGetStringValue(file, section, "strResultSuctionName", "标定块验证检测设置");

            dFlashStartX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashStartX", "0"));
            dFlashEndX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashEndX", "10"));
           
            dCalibCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibCameraDownX", "0"));

          
            dFalshMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFalshMakeUpX", "0"));
            dExposureTimeUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeUp", "100"));
            dGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainUp", "1"));
            dLightUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightUp", "0"));

            dExposureTimeSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeSuction", "100"));
            dGainSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainSuction", "1"));

            dExposureTimeDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeDown", "100"));
            dGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainDown", "1"));
            dLightDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightDown", "1"));

            dCalibGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGain", "1"));
            dCalibGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainUp", "1"));
            dCalibGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainDown", "1"));

            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            lstCalibCameraDownRC.Clear();
            lstCalibCameraDownXY.Clear();

            lstCalibCameraUpRC1.Clear();
            lstCalibCameraUpXY1.Clear();
            lstCalibCameraUpRC2.Clear();
            lstCalibCameraUpXY2.Clear();
            for (int i = 0; i < 9; i++)
            {

                Point p = new Point();
                p.initParam(file, section, "CalibCameraUpRC1_" + (i + 1).ToString());

                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC1.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY1_" + (i + 1).ToString());
                lstCalibCameraUpXY1.Add(p);

                p = new Point();
                p.initParam(file, section, "CalibCameraUpRC2_" + (i + 1).ToString());

                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC2.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY2_" + (i + 1).ToString());
                lstCalibCameraUpXY2.Add(p);

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
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction1";
            bool bFlag = true;

            bFlag = bFlag && pSafeXYZ.saveParam(file, section, "pSafeXYZ");
           // bFlag = bFlag && pPutPos.saveParam(file, section, "pPutPos");
            bFlag = bFlag && pGetCalib.saveParam(file, section, "pGetCalib");
            bFlag = bFlag && pCalibCameraUpXY.saveParam(file, section, "pCalibCameraUpXY");
            bFlag = bFlag && pCalibCameraUpXY2.saveParam(file, section, "pCalibCameraUpXY2");
            bFlag = bFlag && pCamBarrelPos1.saveParam(file, section, "pCamBarrelPos1");
            bFlag = bFlag && pCamBarrelPos2.saveParam(file, section, "pCamBarrelPos2");

            bFlag = bFlag && pDiscardC1.saveParam(file, section, "pDiscardC1");
            bFlag = bFlag && pDiscardC2.saveParam(file, section, "pDiscardC2");
            //bFlag = bFlag && pDiscardC21.saveParam(file, section, "pDiscardC21");
            //bFlag = bFlag && pDiscardC22.saveParam(file, section, "pDiscardC22");
            
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectPosZ", DCorrectPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectTime", DCorrectTime.ToString());
         

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dResultTestGain", dResultTestGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dResultPutZ", dResultPutZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strResultLenName", strResultLenName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strResultSuctionName", strResultSuctionName.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpCheckName", strPicCameraUpCheckName.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashStartX", dFlashStartX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashEndX", dFlashEndX.ToString());

           // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutTime", dPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibCameraDownX", dCalibCameraDownX.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFalshMakeUpX", dFalshMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeUp", dExposureTimeUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainUp", dGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightUp", dLightUp.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeSuction", dExposureTimeSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainSuction", dGainSuction.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeDown", dExposureTimeDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainDown", dGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightDown", dLightDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGain", dCalibGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainUp", dCalibGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainDown", dCalibGainDown.ToString());

            for (int i = 0; i < 9; i++)
            {
                bFlag = bFlag && lstCalibCameraUpRC1[i].saveParam(file, section, "CalibCameraUpRC1_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY1[i].saveParam(file, section, "CalibCameraUpXY1_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpRC2[i].saveParam(file, section, "CalibCameraUpRC2_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY2[i].saveParam(file, section, "CalibCameraUpXY2_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());

            }
            return bFlag;
        }



 


    }

    public class AssembleSuction2
    {

        //基本参数
        public int SuctionOrder { get; set; }//吸笔序号
        public bool BUse { get; set; }//是否使用该吸笔
        public string Name { get; set; }//吸笔名称


        //组装参数
        public double dMakeUpX { get; set; }//工位1 X方向补正值
        public double dMakeUpY { get; set; }//工位1 Y方向补正值
        public double dMakeUpX2 { get; set; }//工位2 X方向补正值
        public double dMakeUpY2 { get; set; }//工位2 Y方向补正值
        public double dAssemblePosZ1 { get; set; }//组装高度1
        public double dAssemblePosZ2 { get; set; }//组装高度2
        public double dPreH1 { get; set; }//二次组装下降高度
        public double dPreH2 { get; set; }//二次组装下降高度
        public double dAssembleH { get; set; }//组装1高度
        public double dAssembleH2 { get; set; }//组装2高度
        public double dAssembleOver { get; set; }//过压量
        public Point pAssemblePos1 { get; set; }
        public Point pAssemblePos2 { get; set; }
        public double DPosCameraDownX { get; set; }//下相机拍照位，同时是飞拍触发位 
        public static Point pCamBarrelPos1 = new Point();//拍摄镜筒位置1，同时也是飞拍位置
        public static Point pCamBarrelPos2 = new Point();//拍摄镜筒位置2，同时也是飞拍位置
        public static double DGetPosZ { get; set; }//取料高度
        public static double DGetTime { get; set; }//取料吸真空时间
        public static double DCorrectPosZ { get; set; }//求心高度
        public static double DCorrectTime { get; set; }//求心时间

        //public static double dDist2 = 1;//二次组装下降高度

        public bool bCam { get; set; }//组装时，是否拍照
        public bool bAutoTestHeight { get; set; }//组装时，是否根据设定压力自动测高


        public static Point pSafeXYZ = new Point();//安全位
        public static double dFlashStartX = 0;//飞拍起始位,也是取料位
        public static double dFlashEndX = 0;//飞拍结束位
        public static double dFalshMakeUpX = 0;//飞拍补正
        public bool bTwo = false;//使用二次高度 

        public static double dExposureTimeUp = 100;//曝光时间(us)
        public static double dGainUp = 1;//增益
        public static double dLightUp = 1;//光源强度
        public static double dExposureTimeDown = 100;//曝光时间(us)
        public static double dGainDown = 1;//增益
        public static double dLightDown = 1;//光源强度

        public static double dExposureTimeSuction = 100;//吸笔曝光时间(us)
        public static double dGainSuction = 1;//吸笔增益

        public static double dCalibGain = 1;//上相机标定九点标定时相机的增益
        public static double dCalibGainUp = 1;//上下标定时上相机的增益
        public static double dCalibGainDown = 1;//上下标定时下相机的增益

        public static double dResultTestGain = 1;//验证组装精度时的增益
        public static string strResultSuctionName = "";//验证组装时标定块的检测方法
        public static string strResultLenName = "";//验证组装时固定块的检测方法
        public static double dResultPutZ = 0;//验证时的高度
        public double dCalibPosZ = 0;//标定块拍照时的高度


        //不保存
        public double dAngleSet = 0;//组装设置角度
        public double dPressureSet = 0;//组装设定压力       
        public double dAssemblVel = 5;//组装速度
        public double dPutDelay = 0;//组装破真空时间
        
        
          
        public double dKeepTime = 0;//组装保压时间
        public Point pAssembly1 = new Point();//手动组装位1
        public Point pAssembly2 = new Point();//手动组装位2 

        public static Point pDiscardC1 = new Point();//组装抛料位1
        public static Point pDiscardC2 = new Point();//组装抛料位2
        public static Point pCheckSuction = new Point();//吸笔1平面校正位，其它吸笔以第一支吸笔X方向-25mm的距离换算出来

        //上下相机标定参数
         public Point pSuctionCenter = new Point();//吸笔中心在下相机的像素位置
        public static Point pGetCalib = new Point();//吸笔取标定板的位置
        public static Point pCalibCameraUpXY = new Point();//吸笔上相机拍照标定板位置
        public static Point pCalibCameraUpXY2 = new Point();//标定板在上相机的位置
        public static double dCalibCameraDownX = 0;//吸笔下相机拍照的位置
        public static Point pCalibCameraDownRC = new Point();//标定板在下相机的位置
        public static List<Point> lstCalibCameraUpRC1 = new List<Point>();//9点标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY1 = new List<Point>();//9点标定上相机坐标位置
        public static List<Point> lstCalibCameraUpRC2 = new List<Point>();//9点标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY2 = new List<Point>();//9点标定上相机坐标位置

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置
        public static List<Point> lstCalibCameraDownRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraDownXY = new List<Point>();//标定上相机坐标位置


        //图像处理参数
        public string strPicDownSuctionCenterName = "";//下相机检测吸笔中心的方法
        public string strPicDownProduct = "";//下相机检测产品的方法
        public static string strPicCameraUpCheckName = "";//上相机检测产品的方法
        public static string strPicDownCalibName = "";//下相机标定方法
        public static string strPicUpCalibName = "";//上相机标定方法
        public static string strPicUpCameraLastCheckName = "";//最后一个镜片对应的镜筒中心检测方法

        public static HObject hImageUP = null;//图片存储
        public static ProcessFatory pfUp = null;//图像处理
        public static ImageResult imgResultUp = new ImageResult();//处理结果

        public HObject hImageDown = null;//图片存储
        public ProcessFatory pfDown = null;//图像处理
        public ImageResult imgResultDown = new ImageResult();//处理结果

        public double dCamDownX = 0;
        public double dCamDownY = 0;
        public static double dCamUpX = 0;
        public static double dCamUpY = 0;

        public static double dUpAng = 0;
        public static double dDownAng = 0;


         public AssembleSuction2(int _order)
        {
            SuctionOrder = _order;
        }
        //使用之前调用initImage，进行初化
        public static void actionUp(HObject image)
        {
            CommonSet.WriteInfo("上相机图像处理开始");
            HOperatorSet.CopyImage(image, out hImageUP);
            if (pfUp != null)
            {
                pfUp.hImage = hImageUP;
                pfUp.Action(hImageUP);
                imgResultUp.StrResult = pfUp.strOutputString;
            }
            imgResultUp.bStatus = true;
        }
        public static  void InitImageUp(string strProcessName)
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

        public static double GetUpResulotion()
        {
            try
            {
                double dx = lstCalibCameraUpXY[0].X - lstCalibCameraUpXY[1].X;
                double dr = lstCalibCameraUpRC[0].X - lstCalibCameraUpRC[1].X;
                double dc = lstCalibCameraUpRC[0].Y - lstCalibCameraUpRC[1].Y;
                double sq = Math.Sqrt(dr*dr+dc*dc);
                return Math.Abs(dx/sq);
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
                HTuple a,da;
                HOperatorSet.TupleAtan(dc/dr,out a);
                HOperatorSet.TupleDeg(a,out da);
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


        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction2_" + SuctionOrder.ToString();

          

            BUse = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUse", "true"));
            bCam = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bCam", "false"));
            bAutoTestHeight = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bAutoTestHeight", "false"));

            Name = IniOperate.INIGetStringValue(file, section, "Name", "");
            dAssemblePosZ1 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemblePosZ1", "0"));
            dAssemblePosZ2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssemblePosZ2", "0"));
            DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));
           

           // DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));

            dPutDelay = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPutDelay", "0.1"));
           
            strPicDownSuctionCenterName = IniOperate.INIGetStringValue(file, section, "strPicDownSuctionCenterName", "OptSuctionC" + SuctionOrder.ToString());
            strPicDownProduct = IniOperate.INIGetStringValue(file, section, "strPicDownProduct", "AssemDown" + SuctionOrder.ToString());

            dMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpX", "0"));
            dMakeUpY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpY", "0"));
            dMakeUpX2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpX2", "0"));
            dMakeUpY2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dMakeUpY2", "0"));
            dPreH1 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPreH1", "0"));
            dPreH2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dPreH2", "0"));
            dAssembleH = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleH", "1"));
            dAssembleH2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleH2", "1"));
            dAssembleOver = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAssembleOver", "1"));

            dCalibPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibPosZ", "0"));

            pSuctionCenter.initParam(file, section, "pSuctionCenter");
            pAssemblePos1 = new Point();
            pAssemblePos2 = new Point();
            pAssemblePos1.initParam(file, section, "pAssemblePos1");
            pAssemblePos2.initParam(file, section, "pAssemblePos2");


        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction2_" + SuctionOrder.ToString();
            bool bFlag = true;


        
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUse", BUse.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bCam", bCam.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bAutoTestHeight", bAutoTestHeight.ToString());
            // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUseCameraUp", BUseCameraUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "Name", Name);

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPosCameraDownX", DPosCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemblePosZ1", dAssemblePosZ1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssemblePosZ2", dAssemblePosZ2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutDelay", dPutDelay.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownProduct", strPicDownProduct.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpCheckName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownSuctionCenterName", strPicDownSuctionCenterName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpX", dMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpY", dMakeUpY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpX2", dMakeUpX2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dMakeUpY2", dMakeUpY2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPreH1", dPreH1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPreH2", dPreH2.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleH", dAssembleH.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleH2", dAssembleH2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAssembleOver", dAssembleOver.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibPosZ", dCalibPosZ.ToString());

            bFlag = bFlag && pSuctionCenter.saveParam(file, section, "pSuctionCenter");
            bFlag = bFlag && pAssemblePos1.saveParam(file, section, "pAssemblePos1");
            bFlag = bFlag && pAssemblePos2.saveParam(file, section, "pAssemblePos2");
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction2";

            pCamBarrelPos1.initParam(file, section, "pCamBarrelPos1");
            pCamBarrelPos2.initParam(file, section, "pCamBarrelPos2");

            pSafeXYZ.initParam(file, section, "pSafeXYZ");

            pGetCalib.initParam(file, section, "pGetCalib");
            pCalibCameraUpXY.initParam(file, section, "pCalibCameraUpXY");
            pCalibCameraUpXY2.initParam(file, section, "pCalibCameraUpXY2");

            pDiscardC1.initParam(file, section, "pDiscardC1");
            pDiscardC2.initParam(file, section, "pDiscardC2");
            pCheckSuction.initParam(file, section, "pCheckSuction");

           // dDist2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dDist2", "1"));

            strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "组装2下相机标定");
            strPicCameraUpCheckName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpCheckName", "");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "组装2上相机标定");

            strPicUpCameraLastCheckName = IniOperate.INIGetStringValue(file, section, "strPicUpCameraLastCheckName", "组装2上相机最后镜筒中心检测");

            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0"));
            DCorrectPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectPosZ", "0"));
            DCorrectTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DCorrectTime", "0"));
           


            dResultTestGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dResultTestGain", "1"));
            dResultPutZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dResultPutZ", "1"));
            strResultLenName = IniOperate.INIGetStringValue(file, section, "strResultLenName", "固定块验证检测设置");
            strResultSuctionName = IniOperate.INIGetStringValue(file, section, "strResultSuctionName", "标定块验证检测设置");

            dFlashStartX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashStartX", "0"));
            dFlashEndX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFlashEndX", "10"));

            dCalibCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibCameraDownX", "0"));

            dFalshMakeUpX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dFalshMakeUpX", "0"));
            dExposureTimeUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeUp", "100"));
            dGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainUp", "1"));
            dLightUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightUp", "0"));

            dExposureTimeSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeSuction", "100"));
            dGainSuction = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainSuction", "1"));

            dExposureTimeDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeDown", "100"));
            dGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainDown", "1"));
            dLightDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightDown", "1"));

            dCalibGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGain", "1"));
            dCalibGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainUp", "1"));
            dCalibGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainDown", "1"));

            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            lstCalibCameraDownRC.Clear();
            lstCalibCameraDownXY.Clear();

            lstCalibCameraUpRC1.Clear();
            lstCalibCameraUpXY1.Clear();
            lstCalibCameraUpRC2.Clear();
            lstCalibCameraUpXY2.Clear();
            for (int i = 0; i < 9; i++)
            {



                Point p = new Point();
                p.initParam(file, section, "CalibCameraUpRC1_" + (i + 1).ToString());
                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC1.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY1_" + (i + 1).ToString());
                lstCalibCameraUpXY1.Add(p);

                p = new Point();
                p.initParam(file, section, "CalibCameraUpRC2_" + (i + 1).ToString());
                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC2.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY2_" + (i + 1).ToString());
                lstCalibCameraUpXY2.Add(p);

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
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\AssembleSuction.ini";

            string section = "AssembleSuction2";
            bool bFlag = true;

            bFlag = bFlag && pCamBarrelPos1.saveParam(file, section, "pCamBarrelPos1");
            bFlag = bFlag && pCamBarrelPos2.saveParam(file, section, "pCamBarrelPos2");
            bFlag = bFlag && pSafeXYZ.saveParam(file, section, "pSafeXYZ");
            // bFlag = bFlag && pPutPos.saveParam(file, section, "pPutPos");
            bFlag = bFlag && pGetCalib.saveParam(file, section, "pGetCalib");
            bFlag = bFlag && pCalibCameraUpXY.saveParam(file, section, "pCalibCameraUpXY");
            bFlag = bFlag && pCalibCameraUpXY2.saveParam(file, section, "pCalibCameraUpXY2");

            bFlag = bFlag && pDiscardC1.saveParam(file, section, "pDiscardC1");
            bFlag = bFlag && pDiscardC2.saveParam(file, section, "pDiscardC2");
            bFlag = bFlag && pCheckSuction.saveParam(file, section, "pCheckSuction");
         

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectPosZ", DCorrectPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DCorrectTime", DCorrectTime.ToString());
           

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dResultTestGain", dResultTestGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dResultPutZ", dResultPutZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strResultLenName", strResultLenName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strResultSuctionName", strResultSuctionName.ToString());

            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpCheckName", strPicCameraUpCheckName.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCameraLastCheckName", strPicUpCameraLastCheckName);

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashStartX", dFlashStartX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFlashEndX", dFlashEndX.ToString());

           // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dPutTime", dPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibCameraDownX", dCalibCameraDownX.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dFalshMakeUpX", dFalshMakeUpX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeUp", dExposureTimeUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainUp", dGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightUp", dLightUp.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeSuction", dExposureTimeSuction.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainSuction", dGainSuction.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeDown", dExposureTimeDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainDown", dGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightDown", dLightDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGain", dCalibGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainUp", dCalibGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainDown", dCalibGainDown.ToString());
            for (int i = 0; i < 9; i++)
            {
                bFlag = bFlag && lstCalibCameraUpRC1[i].saveParam(file, section, "CalibCameraUpRC1_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY1[i].saveParam(file, section, "CalibCameraUpXY1_" + (i + 1).ToString());


                bFlag = bFlag && lstCalibCameraUpRC2[i].saveParam(file, section, "CalibCameraUpRC2_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY2[i].saveParam(file, section, "CalibCameraUpXY2_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());

            }

            return bFlag;
        }






    }
}
