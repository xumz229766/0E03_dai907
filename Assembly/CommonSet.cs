using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using ConfigureFile;
using log4net;
using ImageProcess;
using Tray;
using CameraSet;
using HalconDotNet;
using System.Runtime.Remoting.Messaging;
using Motion;
namespace Assembly
{
    public class CommonSet
    {
        public static Color ColorInit = Color.Gray;//镜筒未完成颜色
        public static Color ColorOK = Color.LightGreen;//镜筒组装OK颜色
        public static Color ColorNG = Color.Red;//镜筒组装NG颜色
        public static Color ColorNotUse = Color.DeepSkyBlue;//镜筒不组装颜色
       
        public static int SUCTIONNUM = 14;//吸笔个数
        //相机
        public static ICamera camUpA1 = null;//取料上相机
        public static ICamera camDownA1 = null;//取料下相机
        public static ICamera camUpA2 = null;//取料上相机2
        public static ICamera camDownA2 = null;//取料下相机2
        public static ICamera camUpC1 = null;//组装上相机1
        public static ICamera camDownC1 = null;//组装下相机1
        public static ICamera camUpC2 = null;//组装上相机2
        public static ICamera camDownC2 = null;//组装下相机2
        public static ICamera camUpD1 = null;//镜筒上相机
        public static ICamera camDownD1 = null;//镜筒下相机
        public static CameraManager cameraManager = null;
        public static int iCameraDownFlag = 1;//1代表组装1部，2代表组装二部
        public static int iCameraUpFlag = 1;//1代表组装1部，2代表组装二部

        public static bool bCalibFlag = false;

        //光源
        public static LightManager lightManager = null;
        public static LightControl lc = null;

        public static string strProductName = "Test";//产品名称 

        public static float fCommonPressureV1 = (float)2.0;//通常电压 
        public static float fCommonPressureV2 = (float)2.0;//通常电压 
        public static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int iLevel = 0;//权限级别，0代表运行，1代表设置，2代表开发者
        public static string strCameraFile = System.Windows.Forms.Application.StartupPath + "\\Param\\CameraInfo.ini";
        //吸笔参数保存位置
        public static string strProductParamPath = System.Windows.Forms.Application.StartupPath + "\\Param\\";
       //镜筒参数保存位置
       // public static string strBPath = System.Windows.Forms.Application.StartupPath + "\\Param\\";
        //组装方案参数
        public static string strAssemSolutionPath = System.Windows.Forms.Application.StartupPath + "\\Param\\";
        //其它参数
        public static string strSaveFile = System.Windows.Forms.Application.StartupPath + "\\Param\\Parameter.ini";
        public static string strReportPath = System.Windows.Forms.Application.StartupPath + "\\Report\\";
        public static string strSaveImg = System.Windows.Forms.Application.StartupPath + "\\Image\\";
        public static int iCurrentPicNum = 1;
        //报警参数
        public static string strAlarmFile = System.Windows.Forms.Application.StartupPath + "\\Param\\AlarmInfo.ini";

        public static Action<string> showMsg;
        //取料模块1吸笔参数
       // public static Dictionary<int, ProductSuction> dic_ProductSuction = new Dictionary<int, ProductSuction>();
        //取料模块2吸笔参数
       // public static Dictionary<int, ProductSuction> dic_ProductSuction2 = new Dictionary<int, ProductSuction>();
        public static Dictionary<int, OptSution1> dic_OptSuction1 = new Dictionary<int, OptSution1>();
        public static Dictionary<int, OptSution2> dic_OptSuction2 = new Dictionary<int, OptSution2>();
        public static Dictionary<int, AssembleSuction1> dic_Assemble1 = new Dictionary<int, AssembleSuction1>();
        public static Dictionary<int, AssembleSuction2> dic_Assemble2 = new Dictionary<int, AssembleSuction2>();
        public static Dictionary<CalibStation, CalibCamera> dic_Calib = new Dictionary<CalibStation, CalibCamera>();
        //public static Dictionary<int, Tray.Tray> dic_Tray = new Dictionary<int, Tray.Tray>();
       
   
        //镜筒吸笔参数
        public static Dictionary<int, BarrelSuction> dic_BarrelSuction = new Dictionary<int, BarrelSuction>();
        //组装方案参数
        public static AssemSolutionManager asm = null;
        public static ImageProcessManager imageManager = null;//图像处理
        public static FrmProcess frmProcess = null;
        //public static FrmRotate frmRotate = null;
        public static int iFlashTest = 1;
        public static FrmSuctionAndTrayRelation frmSAndTR = null;//设置吸笔和托盘关系
        public static FrmBarrelTrayRelation frmBAndTR = null;//设置镜筒托盘
       
        public static SerialAV serailAV = null;
        public static SerialPortMeasureHeight spMH = null;
        public static SerialPortMeasurePressure spPre = null;


        public static System.Diagnostics.Stopwatch swCircleA1 = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swCircleA2 = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swCircleC1 = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swCircleC2 = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swCircleD = new System.Diagnostics.Stopwatch();

        public static double dVelRunX = 100;//X轴运行速度
        public static double dVelFlashX = 300;//X轴飞拍速度
        public static double dVelRunY = 100;//Y轴运行速度
        public static double dVelRunZ = 100;//Z轴运行速度
        public static double dVelC = 100;//C轴运行速度
        public static double dVelLenY = 100;//镜筒Y轴速度
        public static double dVelGlueX = 100;//点胶X轴速度
        public static double dVelGlueY = 100;//点胶Y轴速度
        public static double dVelGlueZ = 100;//点胶Z轴速度 
        public static double dVelGlueC = 100;//点胶C轴速度
        public static double dVelAssemble2 = 10;//二次组装速度


        public static int iGetTimes = 3;//取料每个位置最多吸取次数   
        public static int iTimes = 3;//每次连续吸取穴数

        public static bool bUseAutoParam = false;//使用自动参数
        public static DO dOutTest= DO.点胶清洁夹紧;
        public static DO dOutIn;
        public static long dOutTestTime = 0;
      

        public static bool bForceFinish = false;//强制完成
        public static bool bForceOk = false;  //强制OK
        //空跑标志
        public static bool bTest = true ;
        public static bool bUseGlue = true;//使用点胶
        public static bool bUnableCheckSuction = true;//屏蔽吸笔真空检测
        public static bool bUseLensAngle = false;   //使用镜筒角度
        public static bool bUnableFeel = false;//屏蔽气缸感应
        public static bool bUnableCameraLen = false;//屏蔽镜筒相机
        public static bool bAutoTestHeight = false;//是否自动测试组装高度
        //public static bool[] bTestSuction = new bool[18];//每只吸笔是否为测试高度标志
       
        public static bool bDirect = true;//角度设定方向,true为逆时针,false为顺时针
        
        public static int iAssembleNum = 0;//组装计数
        public static bool bSaveNG = false;//是否保存NG图像
        public static bool bSavePic = false; //保存图像
        public static int iSaveNum = 10;//保存图像次数
        public static bool bResetOne = false;//从手动到自动时第一次自动复位
        public static bool bUseTakePhoto2 = false;//使用2次定位拍照
        public static List<HWindow> lstOtherWin = new List<HWindow>();
        public static List<HWindow> lstA1Win = new List<HWindow>();//取料工位1飞拍窗口
        public static List<HWindow> lstA2Win = new List<HWindow>();//取料工位2飞拍窗口
        public static List<HWindow> lstC1Win = new List<HWindow>();//组装工位1飞拍窗口
        public static List<HWindow> lstC2Win = new List<HWindow>();//组装工位2飞拍窗口
        //飞拍显示窗口
        public static List<HWindow> lstFlashWin = new List<HWindow>();
        public static HWindow hPixelToAxisCalibWin = null;
        public static void WriteInfo(string info)
        {
            log.Info(info);
            showMsg(DateTime.Now.ToString("yy-MM-dd HH:mm:ss ") + info + "\r\n");
        }
        public static void WriteDebug(string info, Exception ex)
        {
            log.Debug(info, ex);
           showMsg(DateTime.Now.ToString("yy-MM-dd HH:mm:ss ") + info + "\r\n");
        }
        public static TrayPanel tpSetPanel = null;//设置界面的Panel
        public static TrayPanel tpOptUIPanel1 = null;//OPtUI显示panel
        public static TrayPanel tpOptUIPanel2 = null;//OPtUI显示panel
        public static TrayPanel tpBarrelPanel = null;
        //导入托盘参数，初始化程序时调用,在参数文件初始化后调用
        public static void LoadTray()
        {
            string strFile = strProductParamPath+strProductName+"\\Tray.ini";
            TrayFactory.initTrayFactory(strFile);
            //先初始化，加快界面加载速度
            //tpSetPanel = new TrayPanel();
            //tpOptUIPanel1 = new TrayPanel();
            //tpOptUIPanel2 = new TrayPanel();
            //tpBarrelPanel = new TrayPanel();
           
        }

        /// <summary>
        /// 初始化文件参数
        /// </summary>
        public static void InitParam()
        {
            dic_Assemble1.Clear();
            dic_OptSuction1.Clear();
            dic_OptSuction2.Clear();
            dic_Assemble2.Clear();
            for (int i = 1; i < 10; i++)
            {
                OptSution1 ps = new OptSution1(i);
                ps.InitParam();
                dic_OptSuction1.Add(i, ps);
                AssembleSuction1 assem = new AssembleSuction1(i);
                assem.InitParam();
                assem.BUse = ps.BUse;
                dic_Assemble1.Add(i, assem);
             
               
            }
            OptSution1.InitStaticParam();
            AssembleSuction1.InitStaticParam();
            for (int i = 10; i < 19; i++)
            {
                OptSution2 ps = new OptSution2(i);
                ps.InitParam();
                dic_OptSuction2.Add(i, ps);

                AssembleSuction2 assem = new AssembleSuction2(i);
                assem.InitParam();
                assem.BUse = ps.BUse;
                dic_Assemble2.Add(i, assem);
                //AssembleSuction suction = new AssembleSuction(i);
                //suction.InitParam();
                //dic_AssemblySuction.Add(i, suction);

            }
            OptSution2.InitStaticParam();
            AssembleSuction2.InitStaticParam();
            //ProductSuction.InitStaticParam();
            ////AssembleSuction.InitStaticParam();
            dic_BarrelSuction.Clear();
            for (int i = 1; i < 3; i++)
            {
                BarrelSuction bs = new BarrelSuction(i);
                bs.InitParam();
                dic_BarrelSuction.Add(i,bs);
             }
            BarrelSuction.InitStaticParam();
            strAssemSolutionPath = System.Windows.Forms.Application.StartupPath + "\\Param\\" + strProductName + "\\Solution.ini";
            asm = AssemSolutionManager.getInstance();

            asm.initSolutionParam(strAssemSolutionPath);


            dic_Calib.Clear();
            //初始化标定参数
            string[] values = Enum.GetNames(typeof(CalibStation));
          
            foreach (string value in values)
            {
                CalibStation station = (CalibStation)Enum.Parse(typeof(CalibStation), value);
                CalibCamera calib = new CalibCamera(station);
                calib.InitParam();
                dic_Calib.Add(station, calib);
            }
            AssembleHeightRelation.InitParam(CommonSet.strProductParamPath + CommonSet.strProductName + "\\CalibRelation.ini");
            AssemblePressureRelation.InitParam(CommonSet.strProductParamPath + CommonSet.strProductName + "\\CalibRelation.ini");

        }
        
     /// <summary>
     /// 保存参数
     /// </summary>
     /// <returns>返回true代表保存成功</returns>
        public static bool SaveParam()
        {
            bool bFlag = true;
            for (int i = 1; i < 10; i++)
            {
                // bFlag = bFlag && dic_AssemblySuction[i].SaveParam();
                bFlag = bFlag && dic_OptSuction1[i].SaveParam();
                bFlag = bFlag && dic_Assemble1[i].SaveParam();
            }
            bFlag = bFlag && OptSution1.SaveStaticParam();
            bFlag = bFlag && AssembleSuction1.SaveStaticParam();
            for (int i = 10; i < 19; i++)
            {
                // bFlag = bFlag && dic_AssemblySuction[i].SaveParam();
                bFlag = bFlag && dic_OptSuction2[i].SaveParam();
                bFlag = bFlag && dic_Assemble2[i].SaveParam();

            }
            bFlag = bFlag && OptSution2.SaveStaticParam();
            bFlag = bFlag && AssembleSuction2.SaveStaticParam();
            for (int i = 1; i < 3; i++)
            {
                bFlag = bFlag && dic_BarrelSuction[i].SaveParam();
            }
            bFlag = bFlag && BarrelSuction.SaveStaticParam();



            foreach (KeyValuePair<CalibStation, CalibCamera> pair in dic_Calib)
            {
               
                bFlag = bFlag && pair.Value.SaveParam();
                
            }
            bFlag = bFlag && AssembleHeightRelation.SaveParam(CommonSet.strProductParamPath + CommonSet.strProductName + "\\CalibRelation.ini");
            bFlag = bFlag && AssemblePressureRelation.SaveParam(CommonSet.strProductParamPath + CommonSet.strProductName + "\\CalibRelation.ini");
            // //for (int i = 8; i < 15; i++)
           // //{
           // //    // bFlag = bFlag && dic_AssemblySuction[i].SaveParam();
           // //    bFlag = bFlag && dic_ProductSuction2[i].SaveParam();
               
           // //}
           // bFlag = bFlag && ProductSuction.SaveStaticParam();
           //// bFlag = bFlag && AssembleSuction.SaveStaticParam();

            return bFlag;

        }


        public static HTuple HomatPixelToAxis(List<Point> lstRC, List<Point> lstXY)
        {
            int count = lstRC.Count;
            int count1 = lstXY.Count;
            if (count != count1)
                return null;
            if (count < 9)
                return null;
            HTuple inputX = new HTuple();
            HTuple inputY = new HTuple();
            HTuple inputR = new HTuple();
            HTuple inputC = new HTuple();
            for (int i = 0; i < 9; i++)
            {

                inputX = inputX.TupleConcat(lstXY[i].X);
                inputY = inputY.TupleConcat(lstXY[i].Y);
                inputR = inputR.TupleConcat(lstRC[i].X);
                inputC = inputC.TupleConcat(lstRC[i].Y);

            }
            HTuple outHomat;
            HOperatorSet.VectorToHomMat2d(inputR, inputC, inputX, inputY, out outHomat);
            return outHomat;
        }
       // public static event EventHandler<DebugArgs> ShowDebug;
        public static bool bShowDialog = false;
        public static DebugArgs debugArgs = new DebugArgs();
        public static void AlarmProcessShow(AlarmBlock block, int step, int stepLen, string strAlarmInfo,bool bNext=false)
        {

            debugArgs = new DebugArgs();
            debugArgs.block = block;
            debugArgs.step = step;
            debugArgs.stepLen = stepLen;
            debugArgs.strAlarmInfo = strAlarmInfo;
            debugArgs.bNext = bNext;
            bShowDialog = true;
           // ShowDebug(null, e);
                //Action<AlarmBlock, int, int, string,bool> dele = showForm;
                //dele.BeginInvoke(block, step, stepLen, strAlarmInfo,bNext,showFormCallBack, null);
            
        }

        public static void RunShowDialog()
        {
            while (true)
            {
                try
                {
                    if (bShowDialog)
                    {
                        bShowDialog = false;
                        showForm(debugArgs.block, debugArgs.step, debugArgs.stepLen, debugArgs.strAlarmInfo, debugArgs.bNext);
                       
                    }


                }
                catch (Exception)
                {
                    
                }

                Thread.Sleep(200);
            }
        }
       

        public static FrmAlarmDialog frmAlarmPro = null;
        private static void showForm(AlarmBlock block, int step, int stepLen, string strAlarmInfo,bool bNext)
        {
            frmAlarmPro = new FrmAlarmDialog(step, stepLen, strAlarmInfo, block, bNext);
            
            frmAlarmPro.ShowDialog();
            frmAlarmPro = null;
        }
        private static void showFormCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult _result = (AsyncResult)result;
                Action<AlarmBlock, int, int, string,bool> dele = (Action<AlarmBlock, int, int, string,bool>)_result.AsyncDelegate;
                dele.EndInvoke(result);
            }
            catch (Exception ex)
            { 
            
            }
        }
        public static string ListToString<T>(List<T> lst)
        {
            string strReturn = "";
            foreach (T d in lst)
            {
                strReturn = strReturn + d.ToString() + ",";
            }
            if (!strReturn.Equals(""))
            {
                strReturn.Remove(strReturn.LastIndexOf(","));
            }
            return strReturn;
        }
        public  static void StringToList<T>(ref List<T> lst, string strValue)
        {

            if (strValue.Equals(""))
                return;
            lst.Clear();
            string[] arr = strValue.Split(',');
            foreach (string value in arr)
            {
                if (value.Equals(""))
                    continue;
                lst.Add((T)Convert.ChangeType(value,typeof(T)));
            }
        }

        //显示信息
        public static void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
   HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }
    }
    public class DebugArgs : EventArgs
    {

        public AlarmBlock block;
        public int step;
        public int stepLen;
        public string strAlarmInfo;
        public bool bNext = false;

    }
}
