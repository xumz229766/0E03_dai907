using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using log4net;
namespace ImageProcess0
{
    public enum eMethod { 
        定位_模板匹配,
        测量_圆,
        区域_环,
        区域_面积
          
    }
    /// <summary>
    /// 输出对象名称
    /// </summary>
    public enum OutputObject {
        //测量圆输出对象
        MC_ResultCircle,
        MC_Contours,
        MC_Cross,
        MC_Line,
        MC_Center,
       //模板输出对象
        Model_Contours,
        Model_OutputPoint,
        Model_InspectContours,
        Model_EraseRegion,
        //区域角度输出对象
        RA_PreImg,
        RA_Ring,
        RA_Region,
        RA_Line       

    }
    public enum OutputResult {
        //测量圆输出结果
        MC_Row,
        MC_Col,
        MC_Radius,
        MC_Angle,
        //模板输出结果
        Model_Row,
        Model_Column,
        Model_OutputRow,
        Model_OutputCol,
        Model_Angle,
        Model_Score,
        //区域环形检测
        RA_Angle,
        RA_Area,
        //区域面积
        RArea_Result


        
    }
    public enum PreProcess { 
        膨胀,
        收缩,
        开运算,
        闭运算,
        灰度差        
    }
    public delegate void GetModelCenter(ref bool mResult, ref double mRow, ref double mCol,ref double mOutRow,ref double mOutCol);//传递模板参数的委托
    public delegate void GetMeasureCenter(ref bool mResult, ref double mOutRow, ref double mOutCol);//传递参数的委托
   
    public abstract class IProcess
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _name = "";//名称       

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private HWindow _hwin = null;//显示窗口

        public HWindow hwin
        {
            get { return _hwin; }
            set { _hwin = value; }
        }
        
        private HObject _image = null;//处理的图像

        public HObject hImage
        {
            get { return _image; }
            set {
                _image = value;
                HTuple hv_Width, hv_Height;
                HOperatorSet.GetImageSize(_image, out hv_Width, out hv_Height);
                height = hv_Height.I;
                width = hv_Width.I;
                bTestResult = false;
            }
        }
        int width, height;
        public Dictionary<OutputObject, HObject> dic_Outputobj = new Dictionary<OutputObject, HObject>();//输出对象 

        public Dictionary<OutputResult, double> dic_outResult = new Dictionary<OutputResult, double>();//输出结果参数

        private bool _bRun = true;//运行标志        

        public bool bRun
        {
            get { return _bRun; }
            set { _bRun = value;}
        }
        private bool _bTestResult = false;//测试结果

        public bool bTestResult
        {
            get { return _bTestResult; }
            set { _bTestResult = value; }
        }
        public GetModelCenter getCenter;
        public GetMeasureCenter getMeasureCenter;
        /// <summary>
        /// 添加显示对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void addOutputObj(OutputObject key, HObject obj)
        {
            if (dic_Outputobj.ContainsKey(key))
                dic_Outputobj[key] = obj;
            else
                dic_Outputobj.Add(key, obj);
            
        }
        /// <summary>
        /// 添加结果
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void addOutputResult(OutputResult key, double value)
        {
            if (dic_outResult.ContainsKey(key))
                dic_outResult[key] = value;
            else
                dic_outResult.Add(key, value);
        }
        
      
        /// <summary>
        /// 处理程序
        /// </summary>
        /// <param name="hImage">输入图像</param>
        public abstract void action(HObject hImage);
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="strFile">输入文件,ini形式</param>
        public abstract void initParam(string strFile);
        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="strFile">保存的参数文件(.ini形式)</param>
        /// <returns>返回结果</returns>
        public abstract bool saveParam(string strFile);
       /// <summary>
       /// 显示对象
       /// </summary>
       /// <param name="hwin">窗体</param>
        public abstract void showObj(HWindow hwin);
        /// <summary>
        /// 从窗体中清除绘制句柄
        /// </summary>
        /// <param name="hwin"></param>
        public abstract void clearDrawObj(HWindow hwin);
        

        public static void setPart(HWindow hwin,double startR, double startC, double endR, double endC)
        {
            HOperatorSet.SetPart(hwin, startR, startC, endR, endC);
        }
        public static void DispImage(HObject image, HWindow hwin)
        {
            try
            {
                HOperatorSet.DispImage(image, hwin);
            }
            catch (Exception ex) { }
        }
        public static void DispObj(HObject obj, HWindow hwin, string color)
        {
            HOperatorSet.SetColor(hwin, color);
            HOperatorSet.DispObj(obj, hwin);
        }
        public Action<string> showMsg = null;
        public void showInfo(string strInfo)
        {
            showMsg(strInfo);
        }
     
    }
}
