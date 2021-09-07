using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using HalconDotNet;
using ImageProcess0;
using Tray;
using System.ComponentModel;
namespace _0E030
{
    public class SuctionR:INotifyPropertyChanged
    {

        public int iCurrentTrayIndex = 1;//当前盘号
        public bool[] bExitTray = new bool[2];//托盘是否存在

        private bool bUse = false;//使用当前工位
        public bool BUse
        {
            get { return bUse; }
            set
            {
                bUse = value;
                //OnPropertyChanged("BUse");
            }
        }//是否使用该吸笔
        //public List<int> lstTrayNum = new List<int>();
        public static List<Tray.Tray> lstTray = new List<Tray.Tray>();
        public List<int> lstIndex1 = new List<int>();//对位点编号1
        public List<int> lstIndex2 = new List<int>();//对位点编号2
        public List<int> lstIndex3 = new List<int>();//对位点编号3
        //取料盘拍照位
        public List<Point> lstP1 = new List<Point>();//对位点1
        public List<Point> lstP2 = new List<Point>();//对位点2
        public List<Point> lstP3 = new List<Point>();//对位点3
        public List<HTuple> lstHomatCamera = new List<HTuple>();//点位关系

        private double dGetPosX;
        public double DGetPosX { get { return dGetPosX; } set { dGetPosX = value; } }//组装X轴取料位置

        private double dGetPosZ;//取料高度
        public double DGetPosZ
        {
            get { return dGetPosZ; }
            set
            {
                dGetPosZ = value;
                //OnPropertyChanged("DGetPosZ");
            }
        }

        private double dGetTime;//取料吸真空时间
        public double DGetTime
        {
            get
            {
                return dGetTime;
            }
            set
            {
                dGetTime = value;
                //OnPropertyChanged("DGetTime");
            }
        }

        private double dPutPosX;
        public double DPutPosX
        {
            get { return dPutPosX; }
            set
            {
                dPutPosX = value;
                /////OnPropertyChanged("DPutPosX"); 
            }
        }//组装X轴组装位置
        private double dDiscardX;
        public double DDiscardX
        {
            get { return dDiscardX; }
            set
            {
                dDiscardX = value;
                /////OnPropertyChanged("DPutPosX"); 
            }
        }//组装X轴抛料位
        private double dDiscardY;
        public double DDiscardY
        {
            get { return dDiscardY; }
            set
            {
                dDiscardY = value;
                /////OnPropertyChanged("DPutPosX"); 
            }
        }//取料Y轴抛料位


        private double dPutPosZ;
        public double DPutPosZ
        {
            get { return dPutPosZ; }
            set
            {
                dPutPosZ = value;
                // OnPropertyChanged("DPutPosZ"); 
            }
        }//组装高度

        private double dPutTime;
        public double DPutTime
        {
            get { return dPutTime; }
            set
            {
                dPutTime = value;
                // OnPropertyChanged("DPutTime");
            }
        }//组装破真空时间

        private bool bTwo = false;
        public bool BTwo
        {
            get { return bTwo; }
            set
            {
                if (bTwo != value)
                {
                    bTwo = value;
                    //OnPropertyChanged("BTwo");
                }
            }
        }//是否使用二次下降组装

        private double dPutPosZ2;
        public double DPutPosZ2
        {
            get { return dPutPosZ2; }
            set
            {
                dPutPosZ2 = value;
                //OnPropertyChanged("DPutPosZ2");
            }
        }//2次下降位置

        private VelAxis velZ2;
        public VelAxis VelZ2
        {
            get { return velZ2; }
            set
            {
                velZ2 = value;
                //OnPropertyChanged("VelZ2");
            }
        }//2次下降速度

        private double dPosCameraDownX;
        public double DPosCameraDownX
        {
            get { return dPosCameraDownX; }
            set
            {
                dPosCameraDownX = value;
                // OnPropertyChanged("DPosCameraDownX");
            }
        }//下相机拍照位
        public Point pSafeXYZ = new Point();//待机位
        public int IProductUpExposure { get; set; }//上相机产品检测曝光值
        public int IProductDownExposure { get; set; }//上相机产品检测曝光值


    



        public string strPicDownSuctionCenterName = "";//下相机检测吸笔中心的方法
        public string strPicDownProduct = "";//下相机检测产品的方法
        public string strPicCameraUpName = "";//上相机检测产品的方法
   

        public static string strPicRotateName = "";//上相机旋转标定方法
        //旋转中心
        public static double dRoateCenterColumn = 0;
        public static double dRoateCenterRow = 0;
        public static int iRotateNum = 10;
        public static int iRotateStep = 10;
        public static List<double> lstRotateRow = new List<double>();
        public static List<double> lstRotateColumn = new List<double>();

       

        public HObject hImageUP = null;//图片存储
        public ProcessFatory pfUp = null;//图像处理
        public ImageResult imgResultUp = new ImageResult();//处理结果

        public HObject hImageDown = null;//图片存储
        public ProcessFatory pfDown = null;//图像处理
        public ImageResult imgResultDown = new ImageResult();//处理结果

        public static void InitTray()
        {
            lstTray.Clear();
            lstTray.Add(TrayFactory.dic_Tray["3"]);
            lstTray.Add(TrayFactory.dic_Tray["4"]);

        }
        public static void InitTrayPanel(TrayPanel panel, int iTrayIndex,System.Drawing.Color c)
        {

            lstTray[iTrayIndex - 1].RemoveDelegate();
            panel.BShowModel = true;
            //panel.setTrayObj(lstTray[iTrayIndex - 1], CommonSet.ColorOK);
            lstTray[iTrayIndex - 1].updateColor += panel.UpdateColor;
            int start = lstTray[iTrayIndex - 1].StartPos;
            int end = lstTray[iTrayIndex - 1].EndPos;

            lstTray[iTrayIndex - 1].setStartEndPos(start, end, c, c);
            // trayPanel1.Dock = DockStyle.Fill;

            
            lstTray[iTrayIndex - 1].updateColor();
            // dic_Tray.Add(i, lstTray[i - 1]);


        }
       
        //通过相机三个对位点，创建整盘的关系
        public bool createPointRelationByCamera(int trayNum)
        {
            //if (tray == null)
            //    return false;
            Tray.Tray tray = lstTray[trayNum - 1];
            int index = trayNum - 1;
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

        /// <summary>
        /// 通过序号获取对应相机坐标
        /// </summary>
        /// <returns>返回相机轴的XY坐标</returns>
        public Point getCameraCoordinateByIndex()
        {
            //if (homat == null)
            //    createPointRelation();
            //int pos = CurrentPos;
            //通过位置查盘所在List序号
            int index = iCurrentTrayIndex - 1;
            if (lstHomatCamera[index] == null)
            {
                createPointRelationByCamera(iCurrentTrayIndex);

            }
            //获取盘
            // Tray.Tray temptray = TrayFactory.dic_Tray[iCurrentTrayIndex.ToString()];
            //从包含的盘中找到所在位置
            // int pos1 = pos;
            Tray.Tray temptray = lstTray[iCurrentTrayIndex - 1];
            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[temptray.CurrentPos];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
                p.X = outX;
                p.Y = outY;
                //p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        public Point getCameraCoordinateByIndex(int iTrayNum, int position)
        {
            //if (homat == null)
            //    createPointRelation();
            // int pos = position;
            //通过位置查盘所在List序号
            int index = iTrayNum - 1;
            if (lstHomatCamera[index] == null)
            {
                createPointRelationByCamera(iTrayNum);

            }
            //获取盘
            Tray.Tray temptray = TrayFactory.dic_Tray[(iTrayNum+2).ToString()];
            //从包含的盘中找到所在位置

            //使用仿射变换求出吸笔的位置
            Index i = temptray.dic_Index[position];
            Point p = new Point();
            HTuple outX, outY;
            try
            {
                HOperatorSet.AffineTransPoint2d(lstHomatCamera[index], i.Row, i.Col, out outY, out outX);
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

        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "SuctionR";
            CommonSet.StringToList(ref lstIndex1, IniOperate.INIGetStringValue(file, section, "lstIndex1", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex2, IniOperate.INIGetStringValue(file, section, "lstIndex2", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex3, IniOperate.INIGetStringValue(file, section, "lstIndex3", "1,1,1,1,1,1,1,1,1"));
            lstP1.Clear();
            lstP2.Clear();
            lstP3.Clear();

         
            lstHomatCamera.Clear();
            bExitTray[0] = true;
            bExitTray[1] = true;
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



             
                lstHomatCamera.Add(null);


            }



            BUse = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BUse", "true"));
            BTwo = Convert.ToBoolean(IniOperate.INIGetStringValue(file, section, "BTwo", "false"));

            DGetPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosZ", "1"));
            DGetTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetTime", "0.2"));
            DGetPosX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DGetPosX", "0"));
            DDiscardX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DDiscardX", "0"));
            DDiscardY = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DDiscardY", "0"));
            DPutPosX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutPosX", "0"));
            DPutPosZ = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutPosZ", "0"));
            DPutTime = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutTime", "0"));
            DPutPosZ2 = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPutPosZ2", "0"));
            DPosCameraDownX = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "DPosCameraDownX", "0"));
            VelZ2 = new VelAxis();
            VelZ2.initParam(file, section, "VelZ2");
            pSafeXYZ.initParam(file, section, "pSafeXYZ");
         
            IProductUpExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "IProductUpExposure", "100"));
            IProductDownExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "IProductDownExposure", "100"));

       

            strPicDownSuctionCenterName = IniOperate.INIGetStringValue(file, section, "strPicDownSuctionCenterName", "右吸笔中心");
            strPicDownProduct = IniOperate.INIGetStringValue(file, section, "strPicDownProduct", "右下相机产品检测");
            strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "右上相机产品检测");
           
            strPicRotateName = IniOperate.INIGetStringValue(file, section, "strPicRotateName", "右下相机旋转中心");

            dRoateCenterColumn = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterColumn", "0"));
            dRoateCenterRow = Convert.ToDouble(IniOperate.INIGetStringValue(file, section, "dRoateCenterRow", "0"));





        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "SuctionR";
            bool bFlag = true;
            // bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstTrayNum", CommonSet.ListToString(lstTrayNum));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex1", CommonSet.ListToString(lstIndex1));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex2", CommonSet.ListToString(lstIndex2));
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "lstIndex3", CommonSet.ListToString(lstIndex3));
            for (int i = 0; i < 9; i++)
            {

                bFlag = bFlag && lstP1[i].saveParam(file, section, "p1_" + (i + 1).ToString());

                bFlag = bFlag && lstP2[i].saveParam(file, section, "p2_" + (i + 1).ToString());

                bFlag = bFlag && lstP3[i].saveParam(file, section, "p3_" + (i + 1).ToString());

             

            }

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BUse", BUse.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "BTwo", BTwo.ToString());

            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosZ", DGetPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetTime", DGetTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DGetPosX", DGetPosX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DDiscardX", DDiscardX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DDiscardY", DDiscardY.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutPosX", DPutPosX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutPosZ", DPutPosZ.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutTime", DPutTime.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPosCameraDownX", DPosCameraDownX.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "DPutPosZ2", DPutPosZ2.ToString());
            bFlag = bFlag && VelZ2.saveParam(file, section, "VelZ2");
            bFlag = bFlag && pSafeXYZ.saveParam(file, section, "pSafeXYZ");
           
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "IProductUpExposure", IProductUpExposure.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "IProductDownExposure", IProductDownExposure.ToString());
         
             bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownSuctionCenterName", strPicDownSuctionCenterName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownProduct", strPicDownProduct.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
         
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicRotateName", strPicRotateName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterColumn", dRoateCenterColumn.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "dRoateCenterRow", dRoateCenterRow.ToString());


            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "SuctionR";




            //strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料2下相机标定");
            //strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料2上相机标定");


        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "SuctionR";
            bool bFlag = true;


            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());

            return bFlag;
        }
        //使用之前调用initImage，进行初化
        public void actionUp(HObject image)
        {
            CommonSet.WriteInfo("左上相机图像处理开始");
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
            pfUp = ModelManager.ImageManager.GetProcessFactory(strProcessName);
            if (pfUp != null)
                pfUp.hImage = null;
            imgResultUp.StrResult = "";
        }

        //使用之前调用initImage，进行初化
        public void actionDown(HObject image)
        {
            CommonSet.WriteInfo("下相机图像处理开始");
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
            pfDown = ModelManager.ImageManager.GetProcessFactory(strProcessName);
            if (pfDown != null)
                pfDown.hImage = null;
            imgResultDown.StrResult = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
