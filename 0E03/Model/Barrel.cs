using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using HalconDotNet;
using ImageProcess;
using Tray;
using System.ComponentModel;
namespace _0E03
{
    public class Barrel
    {
        public int iCurrentTrayIndex = 1;//当前盘号
        public bool[] bExitTray = new bool[2];//托盘是否存在

      
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

        public Point pSafeXY = new Point();//镜筒待机位

        public static List<Point> lstCalibCameraUpRC = new List<Point>();//标定上相机像素位置
        public static List<Point> lstCalibCameraUpXY = new List<Point>();//标定上相机坐标位置

      

        public int IProductUpExposure { get; set; }//上相机产品检测曝光值
        public string strPicCameraUpName = "";//上相机检测产品的方法
        public static string strPicUpCalibName = "";//上相机标定方法
      

       
        public HObject hImageUP = null;//图片存储
        public ProcessFatory pfUp = null;//图像处理
        public ImageResult imgResultUp = new ImageResult();//处理结果


        public void InitParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Barrel.ini";

            string section = "Barrel";
            CommonSet.StringToList(ref lstIndex1, IniOperate.INIGetStringValue(file, section, "lstIndex1", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex2, IniOperate.INIGetStringValue(file, section, "lstIndex2", "1,1,1,1,1,1,1,1,1"));
            CommonSet.StringToList(ref lstIndex3, IniOperate.INIGetStringValue(file, section, "lstIndex3", "1,1,1,1,1,1,1,1,1"));
            lstP1.Clear();
            lstP2.Clear();
            lstP3.Clear();

            lstCalibCameraUpRC.Clear();
            lstCalibCameraUpXY.Clear();
            //lstCalibCameraDownRC.Clear();
            //lstCalibCameraDownXY.Clear();
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



                p = new Point();
                p.initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                // lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                lstCalibCameraUpRC.Add(p);

                p = new Point();
                p.initParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());
                lstCalibCameraUpXY.Add(p);
                //p = new Point();
                //p.initParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());
                //// lstCalibCameraUpRC[i].initParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());
                //lstCalibCameraDownRC.Add(p);

                //p = new Point();
                //p.initParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());
                //lstCalibCameraDownXY.Add(p);
                lstHomatCamera.Add(null);


            }

            pSafeXY.initParam(file, section, "pSafeXY");
            IProductUpExposure = Convert.ToInt32(IniOperate.INIGetStringValue(file, section, "IProductUpExposure", "100"));


            strPicCameraUpName = IniOperate.INIGetStringValue(file, section, "strPicCameraUpName", "镜筒检测");
            strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "镜筒相机标定");

            ModelManager.VelGetX.initParam(file, section, "VelGetX");
            ModelManager.VelGetY.initParam(file, section, "VelGetY");
            ModelManager.VelAssemX.initParam(file, section, "VelAssemX");
            ModelManager.VelBarrelX.initParam(file, section, "VelBarrelX");
            ModelManager.VelBarrelY.initParam(file, section, "VelBarrelY");
            ModelManager.VelC.initParam(file, section, "VelC");
            ModelManager.VelZ.initParam(file, section, "VelZ");

        }
        public bool SaveParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Barrel.ini";

            string section = "Barrel";
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

                bFlag = bFlag && lstCalibCameraUpRC[i].saveParam(file, section, "CalibCameraUpRC_" + (i + 1).ToString());

                bFlag = bFlag && lstCalibCameraUpXY[i].saveParam(file, section, "CalibCameraUpXY_" + (i + 1).ToString());

                //bFlag = bFlag && lstCalibCameraDownRC[i].saveParam(file, section, "CalibCameraDownRC_" + (i + 1).ToString());

                //bFlag = bFlag && lstCalibCameraDownXY[i].saveParam(file, section, "CalibCameraDownXY_" + (i + 1).ToString());


            }
            bFlag = bFlag && pSafeXY.saveParam(file, section, "pSafeXY");
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "IProductUpExposure", IProductUpExposure.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicCameraUpName", strPicCameraUpName.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());
            bFlag = bFlag && ModelManager.VelGetX.saveParam(file, section, "VelGetX");
            bFlag = bFlag && ModelManager.VelGetY.saveParam(file, section, "VelGetY");
            bFlag = bFlag && ModelManager.VelAssemX.saveParam(file, section, "VelAssemX");
            bFlag = bFlag && ModelManager.VelBarrelX.saveParam(file, section, "VelBarrelX");
            bFlag = bFlag && ModelManager.VelBarrelY.saveParam(file, section, "VelBarrelY");
            bFlag = bFlag && ModelManager.VelC.saveParam(file, section, "VelC");
            bFlag = bFlag && ModelManager.VelZ.saveParam(file, section, "VelZ");
            return bFlag;

        }
        public static void InitStaticParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "Barrel";




            //strPicDownCalibName = IniOperate.INIGetStringValue(file, section, "strPicDownCalibName", "取料2下相机标定");
            //strPicUpCalibName = IniOperate.INIGetStringValue(file, section, "strPicUpCalibName", "取料2上相机标定");


        }
        public static bool SaveStaticParam()
        {
            string file = CommonSet.strProductParamPath + ModelManager.strProductName + "\\Suction.ini";

            string section = "Barrel";
            bool bFlag = true;


            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "VelGetZ", dVelGetZ.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicDownCalibName", strPicDownCalibName.ToString());
            //bFlag = bFlag && IniOperate.INIWriteValue(file, section, "strPicUpCalibName", strPicUpCalibName.ToString());

            return bFlag;
        }
        public static double GetUpResulotion()
        {
            try
            {
                double dx = lstCalibCameraUpXY[0].X - lstCalibCameraUpXY[1].X;
                double dy = lstCalibCameraUpXY[0].Y - lstCalibCameraUpXY[1].Y;
                double dr = lstCalibCameraUpRC[0].X - lstCalibCameraUpRC[1].X;
                double dc = lstCalibCameraUpRC[0].Y - lstCalibCameraUpRC[1].Y;
                double sq = Math.Sqrt(dr * dr + dc * dc);
                double sq1 = Math.Sqrt(dx * dx + dy * dy);
                return Math.Abs(sq1 / sq);
            }
            catch (Exception)
            {

                return 1;
            }
        }
        public static void InitTray()
        {
            lstTray.Clear();
            lstTray.Add(TrayFactory.dic_Tray["5"]);
            lstTray.Add(TrayFactory.dic_Tray["6"]);

        }
        public static void InitTrayPanel(TrayPanel panel, int iTrayIndex, System.Drawing.Color c)
        {

            lstTray[iTrayIndex - 1].RemoveDelegate();
            panel.BShowModel = true;
            lstTray[iTrayIndex - 1].updateColor += panel.UpdateColor;
           // panel.setTrayObj(lstTray[iTrayIndex - 1], CommonSet.ColorInit);
            // trayPanel1.Dock = DockStyle.Fill;
            int start = lstTray[iTrayIndex - 1].StartPos;
            int end = lstTray[iTrayIndex - 1].EndPos;

            lstTray[iTrayIndex - 1].setStartEndPos(start, end, c, c);

          
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
            Tray.Tray temptray = TrayFactory.dic_Tray[iTrayNum.ToString()];
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
        //使用之前调用initImage，进行初化
        public void actionUp(HObject image)
        {
            CommonSet.WriteInfo("镜筒上相机图像处理开始");
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


    }
}
