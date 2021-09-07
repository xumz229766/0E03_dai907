using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ConfigureFile;
namespace ImageProcess0
{
   public class RegionAngle:IProcess
    {
        public double dCircleRow = 100;
        public double dCircleColumn = 100;
        public double dCircleRadius = 50;
        public double dWidth = 10;//环宽
        public bool bModelCenter = false;//使用模板定位
        public bool bMeasureCircle = false;//使用测量圆
        public string strPreMehtod = "无";//预处理图像方法
        public int iSize = 3;//预处理图像尺寸
        public int iMinThreshold = 0;//最小阈值
        public int iMaxThreshold = 255;//最大阈值
        public bool bAngleTest = true;//角度检测，否则为面积检测
        //public bool bDilation = false;//是否使用膨胀
        //public double dDilation = 5;//角度检测膨胀大小
        public double dMaxArea = 0;//最大面积 
        public double dMinArea = 0;//最小面积

        public bool bModelResult = false;//模板检测结果
        public bool bMeasureResult = false;//测量圆结果
        public double dResultAngle = 0;//检测角度结果
        public double dResultArea = 0;//检测面积
        

        HTuple hv_Row1 = null, hv_Column1 = null, hv_ParamValues = null;
        HDrawingObject hv_DrawID = null;
        HObject ho_RingContour;
       public RegionAngle(string _name)
       {
           Name = _name;
           HOperatorSet.GenEmptyObj(out ho_RingContour);
           
       }
       HDrawingObject.HDrawingObjectCallback drawCallback;
       public void createDrawCircleObj(HWindow hwin, double row, double column, double radius)
       {

           if (hv_DrawID == null)
           {
               hv_DrawID = new HDrawingObject(row, column, radius);
               
               // hv_DrawID.CreateDrawingObjectCircle();
               drawCallback = new HDrawingObject.HDrawingObjectCallback(drawOnDrag);
               // HOperatorSet.CreateDrawingObjectCircle(row, column, radius, out hv_DrawID);

               HOperatorSet.AttachDrawingObjectToWindow(hwin, hv_DrawID);
               hv_DrawID.OnDrag(drawCallback);
               // hv_DrawID.OnSelect(drawCallback);
               hv_DrawID.OnResize(drawCallback);
               //HOperatorSet.SetDrawingObjectCallback(hv_DrawID, "on_drag", new HTuple(drawCallback));

           }
           else
           {
               if (bModelCenter)
               {
                   hv_DrawID.SetDrawingObjectParams("row", dCircleRow);
                   hv_DrawID.SetDrawingObjectParams("column", dCircleColumn);
                   hv_DrawID.SetDrawingObjectParams("radius", dCircleRadius);

               }
               else if (bMeasureCircle)
               {
                   hv_DrawID.SetDrawingObjectParams("row", dCircleRow);
                   hv_DrawID.SetDrawingObjectParams("column", dCircleColumn);
                   hv_DrawID.SetDrawingObjectParams("radius", dCircleRadius);
               }

           }
       }
       public void drawOnDrag(IntPtr drawID, IntPtr hwinP, Object o)
       {

           getDrawCircleParam();
           //setMeasureParam();
           dic_Outputobj.Clear();
           IProcess.DispImage(hImage, hwin);
           getPreProcess(hImage, strPreMehtod);
           showObj(hwin);
       }
       /// <summary>
       /// 获取圆的参数
       /// </summary>
       public void getDrawCircleParam()
       {
           try
           {
               HOperatorSet.GetDrawingObjectParams(hv_DrawID, ((new HTuple("radius")).TupleConcat(
              "row")).TupleConcat("column"), out hv_ParamValues);
               dCircleColumn = hv_ParamValues.TupleSelect(2).D;
               dCircleRow = hv_ParamValues.TupleSelect(1).D;
               dCircleRadius = hv_ParamValues.TupleSelect(0).D;

               //HOperatorSet.ClearDrawingObject(hv_DrawID);
           }
           catch (Exception ex)
           {
               log.Debug("获取圆参数异常!", ex);
           }

       }
       public void GenRingContours() {
           try
           {
               double dModelRow = 0, dModelCol = 0, dOutRow = 0, dOutCol = 0;
               bModelResult = false;
               if (bModelCenter)
               {
                   getCenter(ref bModelResult, ref dModelRow, ref dModelCol, ref dOutRow, ref dOutCol);

                   if (!bModelResult)
                       return;
                   dCircleRow = dOutRow;
                   dCircleColumn = dOutCol;

               }
               bMeasureResult = false;
               if (bMeasureCircle)
               {
                   getMeasureCenter(ref bMeasureResult, ref dOutRow, ref dOutCol);
                   if (!bMeasureResult)
                       return;
                   dCircleRow = dOutRow;
                   dCircleColumn = dOutCol;
               }

               HObject ho_Circle, ho_Dilation, ho_Erosion, ho_Diff;
               HOperatorSet.GenCircle(out ho_Circle, dCircleRow, dCircleColumn, dCircleRadius);
               HOperatorSet.ErosionCircle(ho_Circle, out ho_Erosion, dWidth / 2);
               HOperatorSet.DilationCircle(ho_Circle, out ho_Dilation, dWidth / 2);
               HOperatorSet.Difference(ho_Dilation, ho_Erosion, out ho_Diff);
               HOperatorSet.GenContourRegionXld(ho_Diff, out ho_RingContour, "border_holes");
               addOutputObj(OutputObject.RA_Ring, ho_RingContour);
           }
           catch (Exception ex) { }
       }
       public HObject getPreProcess(HObject image, string methodName)
       {
           dic_Outputobj.Clear();
           if (methodName.Equals("")||methodName.Equals("无"))
               return image;
           HObject obj;
           HOperatorSet.GenEmptyObj(out obj);
           try {
               PreProcess p =(PreProcess) Enum.Parse(typeof(PreProcess), methodName);
               switch (p) { 
                   case PreProcess.闭运算:                       
                       HOperatorSet.GrayClosingRect(image, out obj, iSize, iSize);
                       break;
                   case PreProcess.开运算:
                       HOperatorSet.GrayOpeningRect(image, out obj, iSize, iSize);
                       break;
                   case PreProcess.膨胀:
                       HOperatorSet.GrayDilationRect(image, out obj, iSize, iSize);
                       break;
                   case PreProcess.收缩:
                       HOperatorSet.GrayErosionRect(image, out obj, iSize, iSize);
                       break;
                   case PreProcess.灰度差:
                       HObject meanObj,subObj;
                       HOperatorSet.MeanImage(image, out meanObj, iSize, iSize);
                       HOperatorSet.SubImage(meanObj, image, out subObj, 1, 0);
                       HOperatorSet.ScaleImageMax(subObj, out obj);
                       break; 

               }
               addOutputObj(OutputObject.RA_PreImg, obj);

           }
           catch (Exception ex) {
               return image;
           }
           return obj;
       }
        public override void action(HalconDotNet.HObject hImage)
        {
            dic_outResult.Clear();
            try
            {
                HObject ho_Thresh,ho_Dilation,ho_Erosion,ho_Ring,ho_Select,ho_Connect,ho_Diff,ho_Reduced,ho_Line;
                HTuple hv_Row,hv_Col,hv_Area,hv_Angle;
                HObject hPre = getPreProcess(hImage, strPreMehtod);
                GenRingContours();
                if (bModelCenter && !bModelResult) {
                    bTestResult = false;
                    return;
                }
                if (bMeasureCircle && !bMeasureResult)
                {
                    bTestResult = false;
                    return;
                }

                HOperatorSet.GenRegionContourXld(ho_RingContour,out ho_Ring,"filled");
                HOperatorSet.ErosionCircle(ho_Ring, out ho_Erosion, dWidth);
                HOperatorSet.Difference(ho_Ring, ho_Erosion, out ho_Ring);
                HOperatorSet.ReduceDomain(hPre,ho_Ring,out ho_Reduced);
                HOperatorSet.Threshold(ho_Reduced, out ho_Thresh, iMinThreshold, iMaxThreshold);
                addOutputObj(OutputObject.RA_Region, ho_Thresh);
                //如果为角度检测
                if (bAngleTest)
                {
                    HOperatorSet.DilationCircle(ho_Thresh, out ho_Dilation, 5);
                    HOperatorSet.Connection(ho_Dilation, out ho_Connect);
                    HOperatorSet.SelectShapeStd(ho_Connect, out ho_Select, "max_area", 70.0);
                    HOperatorSet.Difference(ho_Ring, ho_Select, out ho_Diff);
                    HOperatorSet.Connection(ho_Diff, out ho_Connect);
                    HOperatorSet.SelectShapeStd(ho_Connect, out ho_Select, "max_area", 70.0);
                    HOperatorSet.AreaCenter(ho_Select, out hv_Area, out hv_Row, out hv_Col);
                    if (hv_Area.L > 0)
                    {
                        HOperatorSet.GenRegionLine(out ho_Line, dCircleRow, dCircleColumn, hv_Row, hv_Col);
                        addOutputObj(OutputObject.RA_Line, ho_Line);
                        HOperatorSet.AngleLx(dCircleRow, dCircleColumn, hv_Row, hv_Col, out hv_Angle);
                        dResultAngle = hv_Angle.TupleDeg().D;
                        if (dResultAngle < 0)
                            dResultAngle = 360 + dResultAngle;
                        bTestResult = true;
                    }
                    else {
                        dResultAngle = 0;
                        bTestResult = false;
                    }
                    addOutputResult(OutputResult.RA_Angle, dResultAngle);
                    dResultArea = 0;
                    return;

                }
                else { 
                   
                    //面积检测
                    HOperatorSet.AreaCenter(ho_Thresh, out hv_Area, out hv_Row, out hv_Col);
                    if (hv_Area.L > 0)
                    {
                        dResultArea = hv_Area.D;                      
                    }
                    else
                    {
                        dResultArea = 0;
                       // bTestResult = false;
                    }
                    if ((dResultArea >= dMinArea) && (dResultArea <= dMaxArea))
                    {
                        bTestResult = true;
                    }
                    else
                    {
                        bTestResult = false;
                    }

                }
            }
            catch (Exception ex) {
                bTestResult = false;
            }
           
        }

        public override void initParam(string strFile)
        {
            string strSection = Name;
            dCircleRadius = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Radius", "50"));
            dCircleRow = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Row", "100"));
            dCircleColumn = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Column", "100"));
            dWidth = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Width", "10"));
            iMinThreshold = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "MinThreshold", "100"));
            iMaxThreshold = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "MaxThreshold", "255"));
            iSize = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Size", "5"));
            strPreMehtod = IniOperate.INIGetStringValue(strFile, strSection, "Method", "无");
            bAngleTest = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "AngleTest", "true"));
            bModelCenter = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "ModelCenter", "false"));
            bMeasureCircle = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "MeasureCircle", "false"));
            dMaxArea = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MaxArea", "1000"));
            dMinArea = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MinArea", "0"));
            
        }

        public override bool saveParam(string strFile)
        {
            string strSection = Name;
            bool bResult = true;
            bResult = IniOperate.INIWriteValue(strFile, strSection, "Radius", dCircleRadius.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Row", dCircleRow.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Column", dCircleColumn.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Width", dWidth.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MinThreshold", iMinThreshold.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MaxThreshold", iMaxThreshold.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Size", iSize.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Method", strPreMehtod);
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "AngleTest", bAngleTest.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "ModelCenter", bModelCenter.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MeasureCircle", bMeasureCircle.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MaxArea", dMaxArea.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MinArea", dMinArea.ToString());
            return bResult;
        }

        public override void showObj(HalconDotNet.HWindow hwin)
        {
            foreach (KeyValuePair<OutputObject, HObject> pair in dic_Outputobj)
            {
                try
                {
                    switch (pair.Key)
                    {
                        case OutputObject.RA_PreImg:
                            if (!bRun)
                            {
                                DispImage(pair.Value, hwin);
                            }
                            break;
                        case OutputObject.RA_Ring:
                            if (!bRun)
                            {
                                DispObj(pair.Value, hwin, "blue");
                            }
                            break;
                        case OutputObject.RA_Region:
                            if (!bRun)
                            {
                                DispObj(pair.Value, hwin, "red");
                            }
                            break;
                        case OutputObject.RA_Line:
                            DispObj(pair.Value, hwin, "green");
                            break;

                    }
                }
                catch (Exception ex) { }
            }
        }

        public override void clearDrawObj(HalconDotNet.HWindow hwin)
        {
            if (hv_DrawID != null)
            {
                drawCallback = null;
                HOperatorSet.DetachDrawingObjectFromWindow(hwin, hv_DrawID);
                hv_DrawID.Dispose();
                hv_DrawID = null;
            }
        }
    }
}
