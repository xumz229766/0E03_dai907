using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using ConfigureFile;
using ImageProcess;
using Tray;
namespace Assembly
{

    /// <summary>
    /// 镜筒托盘和点胶参数
    /// </summary>
    public class BarrelSuction
    {
        public int SuctionOrder { get; set; }//吸笔序号
        public string Name { get; set; }//吸笔名称

       
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
        public List<HTuple> lstHomatSuction = new List<HTuple>();//点位关系


        public static double dExposureTimeUp = 100;//曝光时间(us)
        public static double dGainUp = 1;//增益
        public static double dLightUp = 1;//光源强度
        public static double dExposureTimeDown = 100;//曝光时间(us)
        public static double dGainDown = 1;//增益
        public static double dLightDown = 1;//光源强度

        public static double dCalibGain = 1;//上相机标定九点标定时相机的增益
        public static double dCalibGainUp = 1;//上下标定时上相机的增益
        public static double dCalibGainDown = 1;//上下标定时下相机的增益

        public static bool bUseAssem1 = true;//组装轴1使用状态
        public static bool bUseAssem2 = true;//组装轴2使用状态

        public static Point pUV = new Point();//UV点位
        public static double dUVTime = 0;//UV时间
        public static double dGetLenTime = 0.5;//取成品吸真空时间
        public static double DGetPosZ { get; set; }//取料高度
        public static double DGetTime { get; set; }//取料吸真空时间
        public static double DPutPosZ { get; set; }//放成品高度
        public static double DPutTime { get; set; }//放料破真空时间
        public static double dCameraPos = 0;//镜筒角度拍照位
        public static Point pSafe = new Point();//待机位，安全位和拍照高度一致
        public static Point pCamGlue = new Point();//点胶拍照位
        public static Point pGlue = new Point();//点胶位
        public static Point pPutBarrel1 = new Point();//工位1放料位
        public static Point pPutBarrel2 = new Point();//工位2放料位
        public static Point pPutLen = new Point();//成品镜头点胶放料位，同时也是点胶后的取料位
        public static Point pGetLen1 = new Point();//工位1成品镜头取料
        public static Point pGetLen2 = new Point();//工位2成品镜头取料
        public static Point pPutProductGlue = new Point();//夹子放成品位置

        public static double dMakeUpR = 0;//点胶补偿半径
        public static double dStartAngle = 0;//C轴旋转起始角度
        public static double dShotAngle = 0;//点胶旋转的角度
        public static bool bUseRotate = true ;//是否使用旋转
        public static double dStopAngle = 0;//提前断胶角度
        public static int iDirect = 1;//点胶方向
        public static double dDstZStop = 20;//点胶Z轴向上提升距离
        public static double dAngleZStop = 20;//点胶Z轴提前角度
        public static double dDelayTime = 0;//延迟点胶时间

        public static Point pPicGlueCalib = new Point();//点胶图像标定像素位

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//9点标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//9点标定上相机坐标位置
        //旋转中心
        public static double dRoateCenterColumn = 0;
        public static double dRoateCenterRow = 0;
        public static int iRotateNum = 10;
        public static int iRotateStep = 10;
        public static List<double> lstRotateRow = new List<double>();
        public static List<double> lstRotateColumn = new List<double>();

        public static Point pCalibLenPos = new Point();//调试下针位时获取的镜筒中心位置
        


        public static string strPicCameraUpName = "";//上相机检测产品的方法
        public static string strPicCameraDownName = "";//下相机检测产品方法
        public static string strPicUpCalibName = "";//上相机标定方法
        public static string strPicRotateName = "";//上相机旋转

        public static HObject hImageUP = null;//图片存储
        public static ProcessFatory pfUp = null;//图像处理
        public static ImageResult imgResultUp = new ImageResult();//处理结果

        public static HObject hImageDown = null;//图片存储
        public static ProcessFatory pfDown = null;//图像处理
        public static ImageResult imgResultDown = new ImageResult();//处理结果
        public BarrelSuction(int _order)
        {
            SuctionOrder = _order;
        }
        //使用之前调用initImage，进行初化
        public static void actionUp(HObject image)
        {
            CommonSet.WriteInfo("点胶上相机图像处理开始");
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
        public static void actionDown(HObject image)
        {
            CommonSet.WriteInfo("镜筒下相机图像处理开始");
            HOperatorSet.CopyImage(image, out hImageDown);
            if (pfDown != null)
            {
                pfDown.hImage = hImageDown;
                pfDown.Action(hImageDown);
                imgResultDown.StrResult = pfDown.strOutputString;
            }
            imgResultDown.bStatus = true;
        }
        public static void InitImageDown(string strProcessName)
        {
            imgResultDown.bStatus = false;
            pfDown = CommonSet.imageManager.GetProcessFactory(strProcessName);
            if (pfDown != null)
                pfDown.hImage = null;
            imgResultDown.StrResult = "";
        }

        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\BarrelSuction.ini";

            string section = "BarrelSuction_" + SuctionOrder.ToString();
            CommonSet.StringToList(ref lstTrayNum, IniOperate.INIGetStringValue(file, section, "lstTrayNum", ""));

            CommonSet.StringToList(ref lstIndex1, IniOperate.INIGetStringValue(file, section, "lstIndex1", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex2, IniOperate.INIGetStringValue(file, section, "lstIndex2", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex3, IniOperate.INIGetStringValue(file, section, "lstIndex3", "1,1,1,1,1,1,1,1,1"));
            lstP1.Clear();
            lstP2.Clear();
            lstP3.Clear();
            lstHomatSuction.Clear();
            for (int i = 0; i < 2; i++)
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
                lstHomatSuction.Add(null);
            }
            CurrentPos = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "CurrentPos", "1"));
        
            Name = IniOperate.INIGetStringValue(file, section, "Name", "");
            EndPos = GetCount();
            //DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            //DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0"));
          
           // strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "");


        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\BarrelSuction.ini";

            string section = "BarrelSuction_" + SuctionOrder.ToString();
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTrayNum", CommonSet.ListToString(lstTrayNum));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex1", CommonSet.ListToString(lstIndex1));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex2", CommonSet.ListToString(lstIndex2));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex3", CommonSet.ListToString(lstIndex3));
            for (int i = 0; i < 2; i++)
            {

                bFlag = bFlag && lstP1[i].saveParam(file, section, "p1_" + (i + 1).ToString());

                bFlag = bFlag && lstP2[i].saveParam(file, section, "p2_" + (i + 1).ToString());

                bFlag = bFlag && lstP3[i].saveParam(file, section, "p3_" + (i + 1).ToString());
          

            }
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "CurrentPos", CurrentPos.ToString());
        
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "Name", Name);
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
       
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
           
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\BarrelSuction.ini";

            string section = "BarrelSuction";

            bUseAssem1 = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bUseAssem1", "true"));
            bUseAssem2 = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bUseAssem2", "true"));
            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "0"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0.1"));
            DPutPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutPosZ", "0.1"));
            DPutTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutTime", "0"));
            dCameraPos = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCameraPos", "0.1"));
           
            pSafe.initParam(file, section, "pSafe");
            pCamGlue.initParam(file, section, "pCamGlue");
            pGlue.initParam(file, section, "pGlue");
            pPutBarrel1.initParam(file, section, "pPutBarrel1");
            pPutBarrel2.initParam(file, section, "pPutBarrel2");
            pPutLen.initParam(file, section, "pPutLen");
            pGetLen1.initParam(file, section, "pGetLen1");
            pGetLen2.initParam(file, section, "pGetLen2");
            pPicGlueCalib.initParam(file, section, "pPicGlueCalib");
            pUV.initParam(file, section, "pUV");
            pCalibLenPos.initParam(file, section, "pCalibLenPos");
            pPutProductGlue.initParam(file, section, "pPutProductGlue");

            strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "BarrelUp");
            strPicCameraDownName = IniOperate.INIGetStringValue(file, section, "strPicCameraDownName", "BarrelDown");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "点胶上相机标定");
            strPicRotateName = IniOperate.INIGetStringValue(file, section, "strPicRotateName", "旋转中心标定");
            dRoateCenterColumn = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterColumn", "0"));
            dRoateCenterRow = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterRow", "0"));

            
            dExposureTimeUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeUp", "100"));
            dGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainUp", "1"));
            dLightUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightUp", "0"));

            dExposureTimeDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dExposureTimeDown", "100"));
            dGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dGainDown", "1"));
            dLightDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dLightDown", "1"));

            dCalibGain = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGain", "1"));
            dCalibGainUp = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainUp", "1"));
            dCalibGainDown = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dCalibGainDown", "1"));
            dGetLenTime = Convert.ToDouble(IniOperate.INIGetStringValue(file,section,"dGetLenTime","0.5"));
            dUVTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dUVTime", "3"));


            dShotAngle = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dShotAngle", "360"));
            dStopAngle = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dStopAngle", "10"));
            dStartAngle = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dStartAngle", "0"));
            iDirect = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "iDirect", "1"));
            dAngleZStop = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dAngleZStop", "360"));
            dDstZStop = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dDstZStop", "1"));

            CommonSet.iGetTimes = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "iGetTimes", "1"));
            CommonSet.iTimes = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "iTimes", "1"));

            CommonSet.dVelRunX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelRunX", "100"));
            CommonSet.dVelFlashX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelFlashX", "300"));
            CommonSet.dVelRunY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelRunY", "100"));
            CommonSet.dVelRunZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelRunZ", "100"));
            CommonSet.dVelC = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelC", "100"));
            CommonSet.dVelLenY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelLenY", "100"));
            CommonSet.dVelGlueX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelGlueX", "100"));
            CommonSet.dVelGlueY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelGlueY", "100"));
            CommonSet.dVelGlueZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelGlueZ", "100"));
            CommonSet.dVelGlueC = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelGlueC", "100"));
            CommonSet.dVelAssemble2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dVelAssemble2", "100"));

            CommonSet.fCommonPressureV1 = Convert.ToSingle(IniOperate.INIGetStringValue(file, section, "fCommonPressureV1", "2"));
            CommonSet.fCommonPressureV2 = Convert.ToSingle(IniOperate.INIGetStringValue(file, section, "fCommonPressureV2", "2"));
            CommonSet.bTest = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bTest", "false"));
            CommonSet.bUseGlue = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bUseGlue", "false"));
            CommonSet.bAutoTestHeight = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "bAutoTestHeight", "false"));

            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();

            for (int i = 0; i < 9; i++)
            {

                Point p = new Point();
                p.initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC.Add(p);
                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());
                lstCalibCameraUpXY.Add(p);

              
            }
        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath + CommonSet.strProductName + "\\BarrelSuction.ini";

            string section = "BarrelSuction";
            bool bFlag = true;

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bUseAssem1", bUseAssem1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bUseAssem2", bUseAssem2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutPosZ", DPutPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutTime", DPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCameraPos", dCameraPos.ToString());

            bFlag = bFlag && pUV.saveParam(file, section, "pUV");
            bFlag = bFlag && pSafe.saveParam(file, section, "pSafe");
            bFlag = bFlag && pCamGlue.saveParam(file, section, "pCamGlue");
            bFlag = bFlag && pGlue.saveParam(file, section, "pGlue");
            bFlag = bFlag && pPutBarrel1.saveParam(file, section, "pPutBarrel1");
            bFlag = bFlag && pPutBarrel2.saveParam(file, section, "pPutBarrel2");

            bFlag = bFlag && pPutLen.saveParam(file, section, "pPutLen");
            bFlag = bFlag && pGetLen1.saveParam(file, section, "pGetLen1");
            bFlag = bFlag && pGetLen2.saveParam(file, section, "pGetLen2");
            bFlag = bFlag && pPicGlueCalib.saveParam(file, section, "pPicGlueCalib");
            bFlag = bFlag && pCalibLenPos.saveParam(file, section, "pCalibLenPos");
            bFlag = bFlag && pPutProductGlue.saveParam(file, section, "pPutProductGlue");
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraDownName", strPicCameraDownName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicRotateName", strPicRotateName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterColumn", dRoateCenterColumn.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterRow", dRoateCenterRow.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeUp", dExposureTimeUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainUp", dGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightUp", dLightUp.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dExposureTimeDown", dExposureTimeDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGainDown", dGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dLightDown", dLightDown.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGain", dCalibGain.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainUp", dCalibGainUp.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dCalibGainDown", dCalibGainDown.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dGetLenTime", dGetLenTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dUVTime", dUVTime.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dShotAngle", dShotAngle.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dStopAngle", dStopAngle.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dStartAngle", dStartAngle.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "iDirect", iDirect.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dAngleZStop", dAngleZStop.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dDstZStop", dDstZStop.ToString());


            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "iGetTimes", CommonSet.iGetTimes.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "iTimes", CommonSet.iTimes.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelRunX", CommonSet.dVelRunX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelFlashX", CommonSet.dVelFlashX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelRunY", CommonSet.dVelRunY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelRunZ", CommonSet.dVelRunZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelGlueC", CommonSet.dVelGlueC.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelC", CommonSet.dVelC.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelLenY", CommonSet.dVelLenY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelGlueX", CommonSet.dVelGlueX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelGlueY", CommonSet.dVelGlueY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelGlueZ", CommonSet.dVelGlueZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dVelAssemble2", CommonSet.dVelAssemble2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "fCommonPressureV1", CommonSet.fCommonPressureV1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "fCommonPressureV2", CommonSet.fCommonPressureV2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bTest", CommonSet.bTest.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bUseGlue", CommonSet.bUseGlue.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "bAutoTestHeight", CommonSet.bAutoTestHeight.ToString());

            for (int i = 0; i < 9; i++)
            {
                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());


            }
            return bFlag;
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
        //通过吸笔三个对位点，创建整盘的关系
        public bool createPointRelationBySuction(int trayNum)
        {
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
            lstHomatSuction[index] = outHomat;
            return true;
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
                // p.Z = DGetPosZ;
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
                // p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }



    }
}
