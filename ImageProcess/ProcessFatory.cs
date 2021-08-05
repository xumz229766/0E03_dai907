using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ConfigureFile;
using System.Diagnostics;

namespace ImageProcess
{
   public class ProcessFatory
    {
        private Dictionary<eMethod, IProcess> dic_Process = new Dictionary<eMethod, IProcess>();
        public string strName = "";//名称
        public bool[] bUse = new bool[] { false, false, false, false, false, false, false };//检测方法使用标志
        private int width, height;
        private HWindow _hwin = null;
        public eMethod eSrcAng ;//输出角度的方法
        public eMethod eSrcCenter;//输出中心的方法
        public eMethod eSrcExist;//检测有无的方法
        private bool bTestResult = false;//测试结果
        public string strOutputString = "";//输出结果字符串,格式"结果(bool),角度,中心行,中心列,有无"
        public double dRunTime = 0;
        private Stopwatch sw = new Stopwatch();
        public HWindow hwin
        {
            get { return _hwin; }
            set {
                _hwin = value;
                foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
                {
                    dic.Value.hwin = hwin;
                }
            }
        }

        private HObject _hImage = null;

        public HObject hImage
        {
            get { return _hImage; }
            set { _hImage = value;
            if (_hImage != null)
            {
                HTuple hv_width, hv_height;
                HOperatorSet.GetImageSize(_hImage, out hv_width, out hv_height);
                width = hv_width;
                height = hv_height;
                foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
                {
                    dic.Value.hImage = _hImage;
                    dic.Value.bTestResult = false;
                    dic.Value.dic_Outputobj.Clear();
                    dic.Value.dic_outResult.Clear();
                }
            }
            }
        }

        public ProcessFatory(string _name) {
         
            strName = _name;
            init(ImageProcessManager.strFilePath + _name + "\\config.ini");
            //string[] m = Enum.GetNames(typeof(eMethod));
            eMethod[] m =(eMethod[])Enum.GetValues(typeof(eMethod));
          IProcess p =null;
          foreach (eMethod mth in m)
            {
                switch (mth) { 
                    case eMethod.定位_模板匹配:
                        p= new MakeModel(mth.ToString());
                        p.initParam(ImageProcessManager.strFilePath + _name + "\\config.ini");
                        dic_Process.Add(mth, p);
                        break;
                    case eMethod.测量_圆:
                         p = new MeasureCircle(mth.ToString());
                         p.getCenter += getModelCenter;
                         p.initParam(ImageProcessManager.strFilePath + _name + "\\config.ini");
                        dic_Process.Add(mth, p);
                        break;
                    case eMethod.区域_环:
                        p = new RegionAngle(mth.ToString());
                        p.getCenter += getModelCenter;
                        p.getMeasureCenter += getMeasureCircleCenter;
                        p.initParam(ImageProcessManager.strFilePath + _name + "\\config.ini");
                        dic_Process.Add(mth, p);
                        break;
                    case eMethod.区域_面积:
                        p = new RegionArea(mth.ToString());
                        p.getCenter += getModelCenter;
                        p.initParam(ImageProcessManager.strFilePath + _name + "\\config.ini");
                        dic_Process.Add(mth, p);
                        break;
                    //case eMethod.区域_面积检测:
                    //    break;
                        
                }
                
            }
          //hwin = _h;
         // init(FrmProcess.strProjectPath + _name + "\\config.ini");
        }
      
        //获取模板参数
        public void getModelCenter(ref bool bResult,ref double row, ref double col,ref double outRow,ref double outCol)
        {
            MakeModel p = dic_Process[eMethod.定位_模板匹配] as MakeModel;
           
            try
            {
                bResult = p.bTestResult && p.bOutputPoint;
                row = p.dic_outResult[OutputResult.Model_Row];
                col = p.dic_outResult[OutputResult.Model_Column];
                if (p.dic_outResult.ContainsKey(OutputResult.Model_OutputCol))
                {
                    outRow = p.dic_outResult[OutputResult.Model_OutputRow];
                    outCol = p.dic_outResult[OutputResult.Model_OutputCol];
                    
                }
            }
            catch (Exception ex)
            {
                row = 0;
                col = 0;
                outRow = 0;
                outCol = 0;
            }
            
        }
        public void getMeasureCircleCenter(ref bool bResult, ref double outRow, ref double outCol)
        {
            MeasureCircle mc = dic_Process[eMethod.测量_圆] as MeasureCircle;
            try
            {
                bResult = mc.bTestResult;
               
                if (mc.dic_outResult.ContainsKey(OutputResult.MC_Col))
                {
                    outRow = mc.dic_outResult[OutputResult.MC_Row];
                    outCol = mc.dic_outResult[OutputResult.MC_Col];

                }
            }
            catch (Exception ex)
            {
               
                outRow = 0;
                outCol = 0;
            }
        }
        //获取要设定的对象
        public IProcess getProcessMethod(eMethod eType)
        {
            //将所有的运行标志都设置为true
            foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
            {
                if (dic.Value != null)
                {
                    dic.Value.bRun = true;
                    dic.Value.clearDrawObj(hwin);
                }
            }
            //将要设置的运行标志设置为false
            dic_Process[eType].bRun = false;
            return dic_Process[eType];
        }
        public void SetRun(bool value)
        {
            //将所有的运行标志都设置为true
            foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
            {
                if (dic.Value != null)
                {
                    dic.Value.bRun = value;
                    dic.Value.clearDrawObj(hwin);
                }
            }
        
        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="file">输入文件名</param>        
        public void init(string file)
        { 
            int iLen = bUse.Length;
            for (int i = 0; i < iLen; i++)
            {
                bUse[i] = Convert.ToBoolean(IniOperate.INIGetStringValue(file,"UseFlag",i.ToString(),"false"));
            }
            eSrcAng = (eMethod)Enum.Parse(typeof(eMethod), IniOperate.INIGetStringValue(file, "UseFlag", "AngleSelect", eMethod.定位_模板匹配.ToString()));
            eSrcCenter = (eMethod)Enum.Parse(typeof(eMethod), IniOperate.INIGetStringValue(file, "UseFlag", "CenterSelect", eMethod.定位_模板匹配.ToString()));
            eSrcExist = (eMethod)Enum.Parse(typeof(eMethod), IniOperate.INIGetStringValue(file, "UseFlag", "ExistSelect", eMethod.定位_模板匹配.ToString()));
        }
        public bool save()
        {
            bool result = true;
            string file = ImageProcessManager.strFilePath + strName + "\\config.ini";
            int iLen = bUse.Length;
            for (int i = 0; i < iLen; i++)
            {
               result = result && IniOperate.INIWriteValue(file, "UseFlag", i.ToString(), bUse[i].ToString());
            }
            result = result && IniOperate.INIWriteValue(file, "UseFlag", "AngleSelect", eSrcAng.ToString());
            result = result && IniOperate.INIWriteValue(file, "UseFlag", "CenterSelect", eSrcCenter.ToString());
            result = result && IniOperate.INIWriteValue(file, "UseFlag", "ExistSelect", eSrcExist.ToString());
            foreach ( KeyValuePair<eMethod,IProcess> mth in dic_Process)
            {
               result = result && mth.Value.saveParam(file);
            }

            return result;
        }
        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="hImage"></param>
        public void Action(HObject hImage)
        {
            sw.Reset();
            sw.Start();
            int i = 0;
            bTestResult = true;
            strOutputString = "";
            foreach(KeyValuePair<eMethod,IProcess> dic in dic_Process)
            {               
                if (bUse[i])
                {
                    IProcess p = dic.Value;
                    p.action(hImage);
                    bTestResult = bTestResult && p.bTestResult;
                }
                i++;
              
            }
            if (bTestResult)
            {
                strOutputString = "OK," + getOutputString();
            }
            else {
                strOutputString = "NG," + getOutputString();
            }
            sw.Stop();
            dRunTime= sw.ElapsedMilliseconds;

        }
        //获取输出结果
        private string getOutputString()
        {
            string strAng = "0";
            string strRadius = "0";
            //获取角度
            if (eSrcAng != null)
            {
                IProcess p = dic_Process[eSrcAng];
                double angle = 0;
                switch (eSrcAng)
                { 
                    case eMethod.测量_圆:
                        if (p.dic_outResult.ContainsKey(OutputResult.MC_Angle))
                        {
                            angle = p.dic_outResult[OutputResult.MC_Angle];
                            
                        }
                        else
                        {
                            angle = 0;
                        }
                        if (p.dic_outResult.ContainsKey(OutputResult.MC_Radius))
                        {
                            strRadius = p.dic_outResult[OutputResult.MC_Radius].ToString("0.000");
                        }
                        break;
                    case eMethod.定位_模板匹配:
                        if (p.dic_outResult.ContainsKey(OutputResult.Model_Angle))
                            angle = p.dic_outResult[OutputResult.Model_Angle];
                        else
                        {
                            angle = 0;
                        }
                        break;
                    case eMethod.区域_环:
                        if (p.dic_outResult.ContainsKey(OutputResult.RA_Angle))
                            angle = p.dic_outResult[OutputResult.RA_Angle];
                        else
                            angle = 0;
                        break;

                }
                strAng = angle.ToString("0.000");
            }
            else {
                strAng = "0";
            }

            string strRow = "0", strCol = "0";
            if (eSrcCenter != null)
            {
                double row = 0, col = 0;
                IProcess p = dic_Process[eSrcCenter];
                switch (eSrcCenter)
                {
                    case eMethod.测量_圆:
                        if (p.dic_outResult.ContainsKey(OutputResult.MC_Row))
                        {
                            row = p.dic_outResult[OutputResult.MC_Row];
                            col = p.dic_outResult[OutputResult.MC_Col];
                        }
                        else
                        {
                            row = 0;
                            col = 0;
                        }
                        if (p.dic_outResult.ContainsKey(OutputResult.MC_Radius))
                        {
                            strRadius = p.dic_outResult[OutputResult.MC_Radius].ToString("0.000");
                        }
                        break;
                    case eMethod.定位_模板匹配:
                        if (p.dic_outResult.ContainsKey(OutputResult.Model_OutputRow))
                        {
                            row = p.dic_outResult[OutputResult.Model_OutputRow];
                            col = p.dic_outResult[OutputResult.Model_OutputCol];
                        }
                        else
                        {
                            row = 0;
                            col = 0;
                        }

                        break;

                }
                strRow = row.ToString("0.000");
                strCol = col.ToString("0.000");
            }
            else {
                strRow = "0";
                strCol = "0";
            }
            bool bExist = false;
            if (eSrcExist != null)
            {
                IProcess p = dic_Process[eSrcExist];
               
                switch (eSrcExist)
                {
                    case eMethod.测量_圆:
                        if ((p.dic_outResult[OutputResult.MC_Row] == 0) && (p.dic_outResult[OutputResult.MC_Col] == 0))
                        {
                            bExist = false;
                        }
                        else
                        {
                           bExist = true;
                        }
                        if (p.dic_outResult.ContainsKey(OutputResult.MC_Radius))
                        {
                            strRadius = p.dic_outResult[OutputResult.MC_Radius].ToString("0.000");
                        }
                        break;
                    case eMethod.定位_模板匹配:
                        if ((p.dic_outResult[OutputResult.Model_Column] == 0) && (p.dic_outResult[OutputResult.Model_Row] == 0))
                            bExist = false;
                        else
                        {
                            bExist = true;
                        }
                        break;
                    case eMethod.区域_环:
                        if (p.dic_outResult.ContainsKey(OutputResult.RA_Angle) || p.dic_outResult.ContainsKey(OutputResult.RA_Area))
                            bExist = true;
                        else
                            bExist = false;
                        break;
                    case eMethod.区域_面积:
                        if(p.dic_outResult.ContainsKey(OutputResult.RArea_Result)){
                            if (p.dic_outResult[OutputResult.RArea_Result] == 1)
                            {
                                bExist = true;
                            }
                            else
                            {
                                bExist = false;
                            }
                        }else
                        {
                          bExist=false;
                        }
                        break;

                }
               
            }
            else
            {
                bExist = false;
            }
            return strAng + "," + strRow + "," + strCol + "," + bExist.ToString()+","+strRadius;

        }
        public IProcess getTestObj(eMethod e)
        {
            return dic_Process[e];
        }
        #region"缩放"
        private int startX, startY, endX, endY;
        public void DisplayPart(HWindow hwin, int iPosX, int iPosY, double multi = 1.01)
        {
            if ((iPosX + (endX - iPosX) / multi) - (iPosX - (iPosX - startX) / multi) > 3 * width)
            {
                return;
            }
            double scale = 4 / 3;
            startX = (int)(iPosX - (iPosX - startX) / multi);
            startY = (int)(iPosY - (iPosY - startY) / (multi * scale));
            endX = (int)(iPosX + (endX - iPosX) / multi);
            endY = (int)(iPosY + (endY - iPosY) / (multi * scale));
            IProcess.setPart(hwin, startY, startX, endY, endX);
        }
        public void fitWindow()
        {
            startX = 0;
            startY = 0;
            endX = width;
            endY = height;
            IProcess.setPart(hwin, startY, startX, endY, endX);
            showObj();

        }    

        #endregion
       public Stopwatch swShow = new Stopwatch();
       public double dShow1 = 0;
       public double dShow2 = 0;
       public double dShow3 = 0;
        public void showObj()
        {
            try
            {
                //HOperatorSet.ClearWindow(hwin);
                swShow.Restart();
                IProcess.DispImage(hImage, hwin);
               // HOperatorSet.DispObj(hImage, hwin);
               
                dShow1 = swShow.ElapsedMilliseconds;
                bool bFlag = true;//运行和还是手动标志
                foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
                {

                    if (dic.Value != null)
                    {
                        bFlag = bFlag && dic.Value.bRun;
                    }

                }
               
                if (bFlag)
                {
                    dShow2 = swShow.ElapsedMilliseconds;
                    int i = 0;
                    foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
                    {

                        if ( (dic.Value != null) && bUse[i])
                        {
                            dic.Value.showObj(hwin);
                        }
                        i++;
                    }
                    dShow3 = swShow.ElapsedMilliseconds;

                }
                else
                {
                    foreach (KeyValuePair<eMethod, IProcess> dic in dic_Process)
                    {

                        if (dic.Value != null)
                        {
                            if (!dic.Value.bRun)
                                dic.Value.showObj(hwin);
                        }

                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
