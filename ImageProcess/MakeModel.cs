using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using ConfigureFile;
namespace ImageProcess
{
   public class MakeModel:IProcess
    {
       HDrawingObject hv_DrawID = null,hv_DrawID2=null;
       public bool bReduceRegion = false;//使用查找区域标志
       public bool bOutputPoint = true;//指定输出点标志
       public int row1 = 0, col1 = 0, row2 = 100, col2 = 100;
       HTuple hv_ModelID = null;
       public int iAngleStart = 0;
       public int iAngleEnd = 360;
       public double dMinScore = 0.7;
       public double outputRow = 0;
       public double outputCol = 0;
       private HTuple hv_ModelRow = null, hv_ModelColumn = null, hv_ModelScore = null, hv_ModelAngle = null;
       private HTuple hv_OutRow = null, hv_OutCol = null;
       private HObject ho_Erase,ho_OutModelImg;
        public MakeModel(string _name)
        {
            Name = _name;
            HOperatorSet.GenEmptyObj(out ho_Erase);
        }
        HDrawingObject.HDrawingObjectCallback drawCallback;
        public void createDrawCircleObj(HWindow hwin, double row, double column, double radius)
        {

            if (hv_DrawID == null)
            {
                hv_DrawID = new HDrawingObject(row, column, radius);
                // hv_DrawID.CreateDrawingObjectCircle();
                drawCallback += drawOnDrag;
                // HOperatorSet.CreateDrawingObjectCircle(row, column, radius, out hv_DrawID);

                HOperatorSet.AttachDrawingObjectToWindow(hwin, hv_DrawID);
                hv_DrawID.OnDrag(drawCallback);
                // hv_DrawID.OnSelect(drawCallback);
                hv_DrawID.OnResize(drawCallback);
                //HOperatorSet.SetDrawingObjectCallback(hv_DrawID, "on_drag", new HTuple(drawCallback));

            }
        }
        public void createDrawRectObj(HWindow hwin)
        {
            if (hv_DrawID2 == null)
            {
                hv_DrawID2 = new HDrawingObject(row1, col1, row2, col2);
                HOperatorSet.AttachDrawingObjectToWindow(hwin, hv_DrawID2);
                hv_DrawID2.OnDrag(drawCallback);
                hv_DrawID2.OnResize(drawCallback);

            }
        }
        public void drawOnDrag(IntPtr drawID, IntPtr hwinP, Object o)
        {            
            dic_Outputobj.Clear();
            IProcess.DispImage(hImage, hwin);
            showObj(hwin);
        }
        public void getShapeModelContours(int constrast)
        { 
           
            HObject ho_Reduced,ho_Diff,ho_ReducedImg,ho_OutModelRegions;
            HOperatorSet.GetDrawingObjectIconic(out ho_Reduced,hv_DrawID);
            HOperatorSet.Difference(ho_Reduced, ho_Erase, out ho_Diff);
            HOperatorSet.ReduceDomain(hImage, ho_Diff, out ho_ReducedImg);
            HOperatorSet.InspectShapeModel(ho_ReducedImg, out ho_OutModelImg, out ho_OutModelRegions, 1, constrast);
            addOutputObj(OutputObject.Model_InspectContours, ho_OutModelRegions);
            

        }
        public void addErase(HObject erase)
        {
            HOperatorSet.Union2(ho_Erase, erase, out ho_Erase);
            addOutputObj(OutputObject.Model_EraseRegion, ho_Erase);
           
        }
        public void removeErase()
        {
            ho_Erase.Dispose();
            ho_Erase.GenEmptyObj();
            addOutputObj(OutputObject.Model_EraseRegion, ho_Erase);
            
        }
        public bool getOutputPoint(double col,double row)
        {
            try
            {
                HTuple hv_Mat = null,hv_TransCol= null,hv_TransRow = null;
                HObject ho_Cross;
                if (bTestResult)
                {
                    HOperatorSet.VectorAngleToRigid(hv_ModelRow, hv_ModelColumn, hv_ModelAngle, 0, 0, 0, out hv_Mat);
                    HOperatorSet.AffineTransPixel(hv_Mat, row, col, out hv_TransRow, out hv_TransCol);
                    outputCol = hv_TransCol.D;
                    outputRow = hv_TransRow.D;
                    HOperatorSet.GenCrossContourXld(out ho_Cross, row, col, 11, 0);
                    addOutputObj(OutputObject.Model_OutputPoint, ho_Cross);
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                return false;
            }
        }

        public override void action(HalconDotNet.HObject hImage)
        {
            dic_Outputobj.Clear();
            dic_outResult.Clear();
            HObject ho_ReduceImg;
            HObject ho_RectDomain,ho_ShapeContous,ho_AffineTrans,ho_Cross;
            HOperatorSet.GenEmptyObj(out ho_ReduceImg);
            HOperatorSet.GenEmptyObj(out ho_RectDomain);
            HOperatorSet.GenEmptyObj(out ho_ShapeContous);
            HOperatorSet.GenEmptyObj(out ho_AffineTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            try
            {

                if (bReduceRegion)
                {
                    ho_RectDomain.Dispose();
                    HOperatorSet.GenRectangle1(out ho_RectDomain, row1, col1, row2, col2);
                    HOperatorSet.ReduceDomain(hImage, ho_RectDomain, out ho_ReduceImg);
                }
                else
                {
                    ho_ReduceImg = hImage;
                }
                HOperatorSet.FindShapeModel(ho_ReduceImg, hv_ModelID, new HTuple(iAngleStart).TupleRad(), new HTuple(iAngleEnd).TupleRad(), dMinScore, 2,
                    0.5, "least_squares", 0, 0.9, out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScore);
                if (hv_ModelScore.Length > 0)
                {
                    HTuple hv_mat2d=null;
                    ho_ShapeContous.Dispose();
                    HOperatorSet.GetShapeModelContours(out ho_ShapeContous, hv_ModelID,1);
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_ModelRow, hv_ModelColumn, hv_ModelAngle, out hv_mat2d);
                    ho_AffineTrans.Dispose();
                    HOperatorSet.AffineTransContourXld(ho_ShapeContous, out ho_AffineTrans, hv_mat2d);                   
                    HOperatorSet.AffineTransPixel(hv_mat2d, outputRow, outputCol, out hv_OutRow, out hv_OutCol);
                    addOutputObj(OutputObject.Model_Contours, ho_AffineTrans);
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_OutRow, hv_OutCol, 11, 0);
                    addOutputObj(OutputObject.Model_OutputPoint, ho_Cross);
                    bTestResult = true;
                    addOutputResult(OutputResult.Model_Row, hv_ModelRow.D);
                    addOutputResult(OutputResult.Model_Column, hv_ModelColumn.D);
                    double dAngle = hv_ModelAngle.TupleDeg().D;
                    if (dAngle <= 0)
                    {
                        dAngle = 360 + dAngle;
                    }
                   // dAngle = 360 - dAngle;
                   // dAngle = 360 - dAngle;
                    addOutputResult(OutputResult.Model_Angle, dAngle);                    
                    addOutputResult(OutputResult.Model_Score, hv_ModelScore.D);
                    if (bOutputPoint)
                    {
                        addOutputResult(OutputResult.Model_OutputRow, hv_OutRow.D);
                        addOutputResult(OutputResult.Model_OutputCol, hv_OutCol.D);
                    }


                }
                else
                {
                    bTestResult = false;
                    addOutputResult(OutputResult.Model_Row, 0);
                    addOutputResult(OutputResult.Model_Column, 0);
                    addOutputResult(OutputResult.Model_Angle, 0);
                    addOutputResult(OutputResult.Model_Score, 0);
                    if (bOutputPoint)
                    {
                        addOutputResult(OutputResult.Model_OutputRow, 0);
                        addOutputResult(OutputResult.Model_OutputCol, 0);
                    }
                    return;
                }
                
                // HOperatorSet.ReduceDomain(hImage,
            }
            catch (Exception ex) {
                bTestResult = false;
                addOutputResult(OutputResult.Model_Row, 0);
                addOutputResult(OutputResult.Model_Column, 0);
                addOutputResult(OutputResult.Model_Angle, 0);
                addOutputResult(OutputResult.Model_Score, 0);
                if (bOutputPoint)
                {
                    addOutputResult(OutputResult.Model_OutputRow, 0);
                    addOutputResult(OutputResult.Model_OutputCol, 0);
                }
            }
        }

        public override void initParam(string strFile)
        {
            string strDirectory = strFile.Substring(0, strFile.LastIndexOf("\\"));
            string strSection = Name;
            bReduceRegion = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "bRegion", "false"));
            bOutputPoint = Convert.ToBoolean(IniOperate.INIGetStringValue(strFile, strSection, "bOutput", "true"));
            outputRow = Convert.ToDouble(IniOperate.INIGetStringValue(strFile,strSection,"OutputRow","0"));
            outputCol = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "OutputCol", "0"));
            dMinScore = Convert.ToDouble(IniOperate.INIGetStringValue(strFile, strSection, "MinScore", "0.7"));
            row1 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Row1", "0"));
            col1 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Col1", "0"));
            row2 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Row2", "400"));
            col2 = Convert.ToInt32(IniOperate.INIGetStringValue(strFile, strSection, "Col2", "400"));
            try{
                HOperatorSet.ReadShapeModel(strDirectory + "\\shape.shm",out hv_ModelID);
            }catch(Exception ex){}
            
        }

        public override bool saveParam(string strFile)
        {
            string strSection = Name;
            bool bFlag = true;
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "bRegion", bReduceRegion.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "bOutput", bOutputPoint.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "OutputRow", outputRow.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "OutputCol", outputCol.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "MinScore", dMinScore.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "Row1", row1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "Col1", col1.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "Row2", row2.ToString());
            bFlag = bFlag && IniOperate.INIWriteValue(strFile, strSection, "Col2", col2.ToString());
            string strDirectory = strFile.Substring(0, strFile.LastIndexOf("\\"));
            try {
                HOperatorSet.WriteShapeModel(hv_ModelID, strDirectory + "\\shape.shm");
            }
            catch (Exception ex) { }
            return bFlag;

            
        }

        public override void showObj(HalconDotNet.HWindow hwin)
        {
            foreach (KeyValuePair<OutputObject, HObject> pair in dic_Outputobj)
            {
                try
                {
                    switch (pair.Key)
                    {
                        case OutputObject.Model_Contours:
                               if(bRun)
                                   DispObj(pair.Value, hwin, "green");
                           
                            break;
                        case OutputObject.Model_OutputPoint:
                                if(bOutputPoint)
                                    DispObj(pair.Value, hwin, "green");                           
                            break;
                        case OutputObject.Model_InspectContours:
                                if (!bRun)
                                    DispObj(pair.Value, hwin, "green");
                            break;
                        case OutputObject.Model_EraseRegion:
                                if (!bRun)
                                    DispObj(pair.Value, hwin, "gray");
                            break;

                    }
                }
                catch (Exception ex) { }
            }
        }

        public override void clearDrawObj(HalconDotNet.HWindow hwin)
        {
            try
            {
                if (hv_DrawID != null)
                {
                    drawCallback = null;
                    HOperatorSet.DetachDrawingObjectFromWindow(hwin, hv_DrawID);
                    hv_DrawID.Dispose();
                    hv_DrawID = null;
                }
                if (hv_DrawID2 != null)
                {
                    drawCallback = null;
                    HOperatorSet.DetachDrawingObjectFromWindow(hwin, hv_DrawID2);
                    hv_DrawID2.Dispose();
                    hv_DrawID2 = null;
                }
            }
            catch (Exception ex) { }
        }
        public bool createShapeModel(int contrast)
        { 
           
            try{
                HOperatorSet.CreateShapeModel(ho_OutModelImg, "auto", new HTuple(iAngleStart).TupleRad(), new HTuple(iAngleEnd).TupleRad(), "auto", "auto", "use_polarity", contrast, 10, out hv_ModelID);
                return true;
            }catch(Exception ex){
                return false;
            }
        }
       
    }
}
