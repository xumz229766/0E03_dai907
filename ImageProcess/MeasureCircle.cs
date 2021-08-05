using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ConfigureFile;
namespace ImageProcess
{
    public class MeasureCircle : IProcess
    {
        public double dCircleRow = 100;
        public double dCircleColumn = 100;
        public double dCircleRadius = 50;
        //圆测量参数
        public double dMinScore = 0.7;
        public double dMinDist = 3;
        public double dLen1 = 21;
        public double dLen2 = 3;
        public string strMTrans = "negative";
        public string strSelect = "last";
        public int iThresh = 30;
        public bool bCheckAngle = true;
        public bool bModelCenter = false;//使用模板中心
        public double dMaxR = 1000;//圆检测半径的上限
        public double dMinR = 0;//圆检测半径的下限
        private double dResultRow, dResultCol, dResultR, dResultAngle = 0;


        HTuple hv_Row1 = null, hv_Column1 = null, hv_Parameter = null;
        HDrawingObject hv_DrawID = null;
        HTuple hv_MetrologyHandle = null;
        HTuple hv_Index = null, hv_ParamValues = null;



        HObject ho_Cross, ho_Contour1, ho_resultCircle, ho_Contours,ho_center;
        HObject ho_MeasureContours;

        public bool bModelResult = false;//模板检测结果

        public MeasureCircle(string _name)
        {
            Name = _name;
            HOperatorSet.GenEmptyObj(out ho_resultCircle);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Contour1);
            HOperatorSet.GenEmptyObj(out ho_MeasureContours);
            HOperatorSet.GenEmptyObj(out ho_center);
            HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
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
            else {
                if (bModelCenter)
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
                dCircleColumn = hv_ParamValues.TupleSelect(2).ToDArr()[0];
                dCircleRow = hv_ParamValues.TupleSelect(1).ToDArr()[0];
                dCircleRadius = hv_ParamValues.TupleSelect(0).ToDArr()[0];


                //HOperatorSet.ClearDrawingObject(hv_DrawID);
            }
            catch (Exception ex)
            {
                log.Debug("获取圆参数异常!", ex);
            }

        }
        /// <summary>
        /// 设置圆测量参数
        /// </summary>
        public void setMeasureParam()
        {
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
                if (hv_Index != null)
                {
                    HOperatorSet.ClearMetrologyObject(hv_MetrologyHandle, hv_Index);
                }
                HOperatorSet.AddMetrologyObjectCircleMeasure(hv_MetrologyHandle, dCircleRow, dCircleColumn, dCircleRadius, 20, 5,
                1, 30, new HTuple(), new HTuple(), out hv_Index);

                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "min_score",
                dMinScore);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_distance",
                    dMinDist);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_length1",
                    dLen1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_length2",
                    dLen2);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_transition",
                    strMTrans);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_select",
                    strSelect);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_threshold",
                    iThresh);
            }
            catch (Exception ex) { }
            //if (!bRun)
            //{
            //    HOperatorSet.GetMetrologyObjectMeasures(out ho_MeasureContours, hv_MetrologyHandle,
            //  hv_Index, "all", out hv_Row1, out hv_Column1);
            //    addOutputObj(OutputObject.MC_Contours, ho_MeasureContours);
            //}

        }

        public override void action(HalconDotNet.HObject hImage)
        {

            try
            {
                dic_Outputobj.Clear();
                dic_outResult.Clear();

                if (bModelCenter)
                {
                    setMeasureParam();
                    //如果模板没有找到
                    if (!bModelResult)
                    {
                        dResultRow = 0;
                        dResultCol = 0;
                        dResultR = 0;
                        addOutputResult(OutputResult.MC_Row, dResultRow);
                        addOutputResult(OutputResult.MC_Col, dResultCol);
                        addOutputResult(OutputResult.MC_Radius, dResultR);
                        bTestResult = false;
                        return;
                    }
                }
                HOperatorSet.ApplyMetrologyModel(hImage, hv_MetrologyHandle);
                ho_MeasureContours.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_MeasureContours, hv_MetrologyHandle,
                hv_Index, "all", out hv_Row1, out hv_Column1);
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, hv_Index, "all", "result_type",
                    "all_param", out hv_Parameter);

                ho_resultCircle.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_resultCircle, hv_MetrologyHandle,
                    hv_Index, "all", 1.5);
                ho_Contours.Dispose();
                ho_Cross.Dispose();
                HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row1, hv_Column1, 6, 0);

                addOutputObj(OutputObject.MC_ResultCircle, ho_resultCircle);
               
                if (!bRun)
                {
                    addOutputObj(OutputObject.MC_Contours, ho_MeasureContours);
                    addOutputObj(OutputObject.MC_Cross, ho_Cross);
                }
                if (hv_Parameter.Length > 1)
                {
                    dResultRow = hv_Parameter[0].D;
                    dResultCol = hv_Parameter[1].D;
                    dResultR = hv_Parameter[2].D;
                    addOutputResult(OutputResult.MC_Row, dResultRow);
                    addOutputResult(OutputResult.MC_Col, dResultCol);
                    addOutputResult(OutputResult.MC_Radius, dResultR);
                    ho_center.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_center, dResultRow, dResultCol, 50, 0);
                    addOutputObj(OutputObject.MC_Center, ho_center);

                    if ((dResultR < dMinR) || (dResultR > dMaxR))
                    {
                        bTestResult = false;
                        return;
                    }
                }
                else
                {
                    dResultRow = 0;
                    dResultCol = 0;
                    dResultR = 0;
                    addOutputResult(OutputResult.MC_Row, dResultRow);
                    addOutputResult(OutputResult.MC_Col, dResultCol);
                    addOutputResult(OutputResult.MC_Radius, dResultR);
                    addOutputResult(OutputResult.MC_Angle, 0);
                    bTestResult = false;
                    return;
                }
                if (!bCheckAngle)
                {
                    addOutputResult(OutputResult.MC_Angle, 0);
                    bTestResult = true;
                    return;
                }
                HTuple hv_Area = null, hv_Row = null, hv_Column = null, hv_Angle = null;
                HObject ho_Region1, ho_RegionDilation;
                HObject ho_ContCircle, ho_Region2, ho_RegionDilationCircle;
                HObject ho_RegionDifference, ho_ConnectedRegions, ho_SelectedRegions;
                HObject ho_RegionLines=null;
                HOperatorSet.GenEmptyObj(out ho_Region1);
                HOperatorSet.GenEmptyObj(out ho_RegionDilation);
                HOperatorSet.GenEmptyObj(out ho_ContCircle);
                HOperatorSet.GenEmptyObj(out ho_Region2);
                HOperatorSet.GenEmptyObj(out ho_RegionDilationCircle);
                HOperatorSet.GenEmptyObj(out ho_RegionDifference);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                HOperatorSet.GenEmptyObj(out ho_RegionLines);
                //检测角度
                ho_Contour1.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour1, hv_Row1, hv_Column1);
                ho_Region1.Dispose();
                HOperatorSet.GenRegionContourXld(ho_Contour1, out ho_Region1, "margin");
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_Region1, out ho_RegionDilation, 5.5);
                ho_ContCircle.Dispose();
                HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Parameter.TupleSelect(
                    0), hv_Parameter.TupleSelect(1), hv_Parameter.TupleSelect(2), 0, 6.28318,
                    "positive", 1);
                ho_Region2.Dispose();
                HOperatorSet.GenRegionContourXld(ho_ContCircle, out ho_Region2, "margin");
                ho_RegionDilationCircle.Dispose();
                HOperatorSet.DilationCircle(ho_Region2, out ho_RegionDilationCircle, 2.5);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionDilationCircle, ho_RegionDilation, out ho_RegionDifference
                    );
                //HOperatorSet.DilationCircle(ho_RegionDifference, out ho_RegionDifference, 10);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);
                ho_SelectedRegions.Dispose();
                if (hv_Area.Length > 0)
                {
                    HOperatorSet.AngleLx(hv_Parameter.TupleSelect(
                        0), hv_Parameter.TupleSelect(1), hv_Row, hv_Column, out hv_Angle);
                    dResultAngle = hv_Angle.TupleDeg().D;
                    if (dResultAngle < 0)
                        dResultAngle = 360 + dResultAngle;
                    HOperatorSet.GenRegionLine(out ho_RegionLines, hv_Parameter.TupleSelect(
                        0), hv_Parameter.TupleSelect(1), hv_Row, hv_Column);
                    addOutputObj(OutputObject.MC_Line, ho_RegionLines);
                }
                else
                {
                    dResultAngle = 0;
                    addOutputObj(OutputObject.MC_Line, null);
                }
                addOutputResult(OutputResult.MC_Angle, dResultAngle);
                bTestResult = true;
            }
            catch (Exception ex) {
                bTestResult = false;
                return;
            }
           

        }
        
        public override void initParam(string strFile)
        {
            string strSection = Name;
            dCircleRadius = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Radius", "50"));
            dCircleRow = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Row", "100"));
            dCircleColumn = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Column", "100"));
            dMinScore = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MinScore", "0.7"));
            dMinDist = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MinDistance", "3"));
            dLen1 = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Len1", "21"));
            dLen2 = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "Len2", "3"));
            iThresh = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Threshod", "21"));
            strMTrans = IniOperate.INIGetStringValue(strFile, strSection, "Transition", "negative");
            // = IniOperate.INIGetStringValue(strFile, sid, "Direction", "行");
            strSelect = IniOperate.INIGetStringValue(strFile, strSection, "Select", "last");
            bCheckAngle = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "CheckAngle", "true"));
            bModelCenter = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "ModelCenter", "false"));
            dMaxR = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MaxR", "1000"));
            dMinR = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MinR", "0"));
            setMeasureParam();
        }
        public override bool saveParam(string strFile)
        {
            string strSection = Name;
            bool bResult = true;
            bResult = IniOperate.INIWriteValue(strFile, strSection, "Radius", dCircleRadius.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Row", dCircleRow.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Column", dCircleColumn.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MinScore", dMinScore.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MinDistance", dMinDist.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Len1", dLen1.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Len2", dLen2.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Threshod", iThresh.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Transition", strMTrans.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "Select", strSelect.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "CheckAngle", bCheckAngle.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "ModelCenter", bModelCenter.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MaxR", dMaxR.ToString());
            bResult = bResult && IniOperate.INIWriteValue(strFile, strSection, "MinR", dMinR.ToString());
            return bResult;
        }
        public override void showObj(HWindow hwin)
        {
            //HOperatorSet.ClearWindow(hwin);
            //DispImage(hImage,hwin);
            foreach (KeyValuePair<OutputObject, HObject> pair in dic_Outputobj)
            {
                try
                {
                    switch (pair.Key)
                    {
                        case OutputObject.MC_Contours:
                            if (!bRun)
                            {
                                DispObj(pair.Value, hwin, "red");
                            }
                            break;
                        case OutputObject.MC_Cross:
                            if (!bRun)
                            {
                                DispObj(pair.Value, hwin, "blue");
                            }
                            break;
                        case OutputObject.MC_ResultCircle:

                            DispObj(pair.Value, hwin, "green");
                            break;
                        case OutputObject.MC_Line:
                            DispObj(pair.Value, hwin, "green");
                            break;
                        case OutputObject.MC_Center:
                            DispObj(pair.Value, hwin, "green");
                            break;
                    }
                }
                catch (Exception ex) { }
            }

        }
        public override void clearDrawObj(HWindow hwin)
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
