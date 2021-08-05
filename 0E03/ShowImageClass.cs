using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraSet;
using HalconDotNet;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using ImageProcess;
using System.IO;
using System.Windows.Forms;
using ConfigureFile;
using System.Threading;
using Motion;
namespace _0E03
{
   public class ShowImageClass
    {
        static Stopwatch sw1 = new Stopwatch();
        static Stopwatch sw2 = new Stopwatch();
        static Stopwatch sw3 = new Stopwatch();
        public static bool bGrabDataL = false;
        public static bool bGrabDataR = false;
        public static bool bGrabDataB = false;
        /// <summary>
        /// 处理左上相机图像
        /// </summary>
        /// <param name="image"></param>
        public static void ProcessImageUpL(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                if (CalibrationL.bUseUpAngle)
                {
                    HOperatorSet.RotateImage(image, out image, CalibrationL.GetUpCameraAngle(), "constant");
                }
                //HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.取料中心对位测试:
                    case RunMode.手动:
                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp != null)
                        //        {

                        //            CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp.Dispose();
                        //        }
                        //        HOperatorSet.CopyImage(image, out CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp);
                        //        HOperatorSet.DispImage(CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp, FrmMain.frmOpt1.GetUpWindow());
                        //        HOperatorSet.SetColor(FrmMain.frmOpt1.GetUpWindow(), "green");
                        //        HOperatorSet.DispCross(FrmMain.frmOpt1.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);
                        //        return;
                        //    }
                        //}
                        //通过获取吸笔对应的索引显示到相应的窗体
                       

                            try
                            {
                                if (ModelManager.SuctionLParam.hImageUP != null)
                                    ModelManager.SuctionLParam.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageUP);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmParamSet.frmSetLeft.GetUpWindow();
                                // HWindow hwin = FrmMain.optSuctionUI.GetUpWindow();
                                 //HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //HOperatorSet.getpart
                                HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);

                               
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("左上相机显示图像异常" + ex.ToString());
                            }





                        break;
                    case RunMode.暂停:
                  
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";


                            if (ModelManager.SuctionLParam.hImageUP != null)
                                ModelManager.SuctionLParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageUP);

                            hwin = FrmMain.hwinUpL;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageUP, hwin);
                            ModelManager.SuctionLParam.pfUp.SetRun(true);
                            ModelManager.SuctionLParam.pfUp.hwin = hwin;
                            ModelManager.SuctionLParam.actionUp(ModelManager.SuctionLParam.hImageUP);
                            ModelManager.SuctionLParam.pfUp.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("左上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.取料测试:
                    case RunMode.取料标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            if (ModelManager.SuctionLParam.hImageUP != null)
                                ModelManager.SuctionLParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageUP);

                            hwin = FrmParamSet.frmSetLeft.GetUpWindow(); ;

                           // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageUP, hwin);
                            ModelManager.SuctionLParam.pfUp.SetRun(true);
                            ModelManager.SuctionLParam.pfUp.hwin = hwin;
                            ModelManager.SuctionLParam.actionUp(ModelManager.SuctionLParam.hImageUP);
                            ModelManager.SuctionLParam.pfUp.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("左上相机上相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }

        /// <summary>
        /// 处理左下相机图像
        /// </summary>
        /// <param name="image"></param>
        public static void ProcessImageDownL(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                if (CalibrationL.bUseDownAngle)
                {
                    HOperatorSet.RotateImage(image, out image, CalibrationL.GetDownCameraAngle(), "constant");
                }
                //HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.取料中心对位测试:
                    case RunMode.手动:
                    
                        //通过获取吸笔对应的索引显示到相应的窗体
                        try
                        {
                            if (ModelManager.SuctionLParam.hImageDown != null)
                                ModelManager.SuctionLParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageDown);
                            HWindow hwin = FrmParamSet.frmSetLeft.GetDownWindow();
                            if (FrmSetLeft.frmRotate != null)
                                hwin = FrmRotate.hwin;
                            else if (FrmParamSet.iSelectIndex == 2)
                            {
                                if (FrmParamSet.frmBarrel.AxisX == AXIS.组装X1轴)
                                    hwin = FrmParamSet.frmBarrel.GetDownWindow();
                            }
                         
                            HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            if (bGrabDataL)
                            {
                                string strDispRow = "";
                                string strDispCol = "";
                                ModelManager.SuctionLParam.pfDown.SetRun(true);
                                ModelManager.SuctionLParam.pfDown.hwin = hwin;
                                ModelManager.SuctionLParam.actionDown(ModelManager.SuctionLParam.hImageDown);
                                ModelManager.SuctionLParam.pfDown.showObj();
                                strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000");
                                strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                                CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");
                                WriteResult("D:\\数据采集\\左下相机\\", ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000") + "," + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000") + ",");

                            }

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("左下相机显示图像异常" + ex.ToString());
                        }





                        break;
                  
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";


                            if (ModelManager.SuctionLParam.hImageDown != null)
                                ModelManager.SuctionLParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageDown);

                            hwin = FrmMain.hwinDownL;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                            ModelManager.SuctionLParam.pfDown.SetRun(true);
                            ModelManager.SuctionLParam.pfDown.hwin = hwin;
                            ModelManager.SuctionLParam.actionDown(ModelManager.SuctionLParam.hImageDown);
                            ModelManager.SuctionLParam.pfDown.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("左下相机下相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.旋转:
                    case RunMode.取料测试:
                    case RunMode.取料标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            if (ModelManager.SuctionLParam.hImageDown != null)
                                ModelManager.SuctionLParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionLParam.hImageDown);

                            hwin = FrmParamSet.frmSetLeft.GetDownWindow();
                            if (Run.runMode == RunMode.旋转)
                                hwin = FrmRotate.hwin;
                            else if (FrmParamSet.iSelectIndex == 2)
                            {
                                if (FrmParamSet.frmBarrel.AxisX == AXIS.组装X1轴)
                                    hwin = FrmParamSet.frmBarrel.GetDownWindow();
                            }
                            // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionLParam.hImageDown, hwin);
                            ModelManager.SuctionLParam.pfDown.SetRun(true);
                            ModelManager.SuctionLParam.pfDown.hwin = hwin;
                            ModelManager.SuctionLParam.actionDown(ModelManager.SuctionLParam.hImageDown);
                            ModelManager.SuctionLParam.pfDown.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");
                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("左下相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        /// <summary>
        /// 处理右上相机图像
        /// </summary>
        /// <param name="image"></param>
        public static void ProcessImageUpR(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                //HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
                if (CalibrationR.bUseUpAngle)
                {
                    HOperatorSet.RotateImage(image, out image, CalibrationR.GetUpCameraAngle(), "constant");
                }
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.取料中心对位测试:
                    case RunMode.手动:
                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp != null)
                        //        {

                        //            CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp.Dispose();
                        //        }
                        //        HOperatorSet.CopyImage(image, out CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp);
                        //        HOperatorSet.DispImage(CommonSet.dic_Calib[CalibStation.取料工位1].hImageUp, FrmMain.frmOpt1.GetUpWindow());
                        //        HOperatorSet.SetColor(FrmMain.frmOpt1.GetUpWindow(), "green");
                        //        HOperatorSet.DispCross(FrmMain.frmOpt1.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);
                        //        return;
                        //    }
                        //}
                        //通过获取吸笔对应的索引显示到相应的窗体


                        try
                        {
                            if (ModelManager.SuctionRParam.hImageUP != null)
                                ModelManager.SuctionRParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageUP);
                            // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                            HWindow hwin = FrmParamSet.frmSetRight.GetUpWindow();
                            // HWindow hwin = FrmMain.optSuctionUI.GetUpWindow();
                           // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            //HOperatorSet.getpart
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageUP, hwin);
                            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);


                        }
                        catch (Exception ex)
                        {
                           // MessageBox.Show("右上相机显示图像异常" + ex.ToString());
                        }





                        break;
                   
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";


                            if (ModelManager.SuctionRParam.hImageUP != null)
                                ModelManager.SuctionRParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageUP);

                            hwin = FrmMain.hwinUpR;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageUP, hwin);
                            ModelManager.SuctionRParam.pfUp.SetRun(true);
                            ModelManager.SuctionRParam.pfUp.hwin = hwin;
                            ModelManager.SuctionRParam.actionUp(ModelManager.SuctionRParam.hImageUP);
                            ModelManager.SuctionRParam.pfUp.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("右上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.取料测试:
                    case RunMode.取料标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if (ModelManager.SuctionRParam.hImageUP != null)
                                ModelManager.SuctionRParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageUP);

                            hwin = FrmParamSet.frmSetRight.GetUpWindow();

                           // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageUP, hwin);
                            ModelManager.SuctionRParam.pfUp.SetRun(true);
                            ModelManager.SuctionRParam.pfUp.hwin = hwin;
                            ModelManager.SuctionRParam.actionUp(ModelManager.SuctionRParam.hImageUP);
                            ModelManager.SuctionRParam.pfUp.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("右上相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        /// <summary>
        /// 处理右下相机图像
        /// </summary>
        /// <param name="image"></param>
        public static void ProcessImageDownR(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                if (CalibrationR.bUseDownAngle)
                {
                    HOperatorSet.RotateImage(image, out image, CalibrationR.GetDownCameraAngle(), "constant");
                }
                //HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.取料中心对位测试:
                    case RunMode.手动:
                     
                        //通过获取吸笔对应的索引显示到相应的窗体
                        
                        try
                        {
                            if (ModelManager.SuctionRParam.hImageDown != null)
                                ModelManager.SuctionRParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageDown);
                            // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                            HWindow hwin = FrmParamSet.frmSetRight.GetDownWindow();
                            if (FrmSetRight.frmRotate != null)
                                hwin = FrmRotate.hwin;
                            else if (FrmParamSet.iSelectIndex == 2)
                            {
                                if (FrmParamSet.frmBarrel.AxisX == AXIS.组装X2轴)
                                    hwin = FrmParamSet.frmBarrel.GetDownWindow();
                            }
                            // HWindow hwin = FrmMain.optSuctionUI.GetUpWindow();
                            //HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            //HOperatorSet.getpart
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);
                            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            if (bGrabDataR)
                            {
                                string strDispRow = "";
                                string strDispCol = "";

                                ModelManager.SuctionRParam.pfDown.SetRun(true);
                                ModelManager.SuctionRParam.pfDown.hwin = hwin;
                                ModelManager.SuctionRParam.actionDown(ModelManager.SuctionRParam.hImageDown);
                                ModelManager.SuctionRParam.pfDown.showObj();
                                strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000");
                                strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                                CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");
                                WriteResult("D:\\数据采集\\右下相机\\", ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000") + "," + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000") + ",");

                            }

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("右下相机显示图像异常" + ex.ToString());
                        }
                        break;
                   
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";


                            if (ModelManager.SuctionRParam.hImageDown != null)
                                ModelManager.SuctionRParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageDown);

                            hwin = FrmMain.hwinDownR;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);
                            ModelManager.SuctionRParam.pfDown.SetRun(true);
                            ModelManager.SuctionRParam.pfDown.hwin = hwin;
                            ModelManager.SuctionRParam.actionDown(ModelManager.SuctionRParam.hImageDown);
                            ModelManager.SuctionRParam.pfDown.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");
                            
                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("右下相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.旋转:
                    case RunMode.取料测试:
                    case RunMode.取料标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";


                            if (ModelManager.SuctionRParam.hImageDown != null)
                                ModelManager.SuctionRParam.hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.SuctionRParam.hImageDown);

                            hwin = FrmParamSet.frmSetRight.GetDownWindow();
                            if (Run.runMode == RunMode.旋转)
                                hwin = FrmRotate.hwin;
                            else if (FrmParamSet.iSelectIndex == 2)
                            {
                                if (FrmParamSet.frmBarrel.AxisX == AXIS.组装X2轴)
                                    hwin = FrmParamSet.frmBarrel.GetDownWindow();
                            }
                            //HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.SuctionRParam.hImageDown, hwin);
                            ModelManager.SuctionRParam.pfDown.SetRun(true);
                            ModelManager.SuctionRParam.pfDown.hwin = hwin;
                            ModelManager.SuctionRParam.actionDown(ModelManager.SuctionRParam.hImageDown);
                            ModelManager.SuctionRParam.pfDown.showObj();
                            strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("右下相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        static HWindow hwinUp=null;
        /// <summary>
        /// 处理镜筒相机图像
        /// </summary>
        /// <param name="image"></param>
        public static void ProcessImageUpBarrel(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                //HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
                if (CalibrationBL.bUseUpAngle)
                {
                    HOperatorSet.RotateImage(image, out image, CalibrationBL.GetUpCameraAngle(), "constant");
                }
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.取料中心对位测试:
                    case RunMode.手动:

                        //通过获取吸笔对应的索引显示到相应的窗体

                       
                        try
                        {
                            if (hwinUp == null)
                                hwinUp = FrmParamSet.frmBarrel.GetUpWindow();
                            if (ModelManager.BarrelParam.hImageUP != null)
                                ModelManager.BarrelParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.BarrelParam.hImageUP);
                            // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                           
                            // HWindow hwin = FrmMain.optSuctionUI.GetUpWindow();
                            // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            //HOperatorSet.getpart
                            HOperatorSet.DispImage(ModelManager.BarrelParam.hImageUP, hwinUp);
                            HOperatorSet.DispCross(hwinUp, height.I / 2, width.I / 2, 3000, 0);
                            if (bGrabDataB)
                            {
                                string strDispRow = "";
                                string strDispCol = "";
                                string strAngle = "";

                                ModelManager.BarrelParam.pfUp.SetRun(true);
                                ModelManager.BarrelParam.pfUp.hwin = hwinUp;
                                ModelManager.BarrelParam.actionUp(ModelManager.BarrelParam.hImageUP);
                                ModelManager.BarrelParam.pfUp.showObj();
                                strDispRow = "Row:" + ModelManager.BarrelParam.imgResultUp.CenterRow.ToString("0.000");
                                strDispCol = "Col:" + ModelManager.BarrelParam.imgResultUp.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(hwinUp, strDispRow, "window", 0, 0, "green", "false");
                                CommonSet.disp_message(hwinUp, strDispCol, "window", 20, 0, "green", "false");
                                WriteResult("D:\\数据采集\\镜筒相机\\", ModelManager.BarrelParam.imgResultUp.CenterRow.ToString("0.000") + "," + ModelManager.BarrelParam.imgResultUp.CenterColumn.ToString("0.000") + ",");


                            }

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show("镜筒相机显示图像异常" + ex.ToString());
                        }





                        break;
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";


                            if (ModelManager.BarrelParam.hImageUP != null)
                                ModelManager.BarrelParam.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out ModelManager.BarrelParam.hImageUP);

                            hwin = FrmMain.hwinUpB;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(ModelManager.BarrelParam.hImageUP, hwin);
                            ModelManager.BarrelParam.pfUp.SetRun(true);
                            ModelManager.BarrelParam.pfUp.hwin = hwin;
                            ModelManager.BarrelParam.actionUp(ModelManager.BarrelParam.hImageUP);
                            ModelManager.BarrelParam.pfUp.showObj();
                            strDispRow = "Row:" + ModelManager.BarrelParam.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + ModelManager.BarrelParam.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "green", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "green", "false");



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("镜筒相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                           
                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("镜筒相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }

        #region"写入数据到文件"
        private static  void WriteFile(string strPath, string strContent)
        {
            if (!Directory.Exists(strPath))
            {
                try
                {
                    DirectoryInfo dInfo = Directory.CreateDirectory(strPath);
                }
                catch (Exception)
                {

                }
            }
            string file = strPath + DateTime.Now.ToString("yyMMdd") + ".csv";
            try
            {

                if (!File.Exists(file))
                {
                    FileStream fs = File.Create(file);
                    fs.Close();
                    string strHead = "时间,Row,Col,";
                    //for (int i = 1; i < 10; i++)
                    //{

                    //    strHead += "C" + i.ToString() + "_," + "C" + i.ToStrsing() + "_Col,";
                    //}

                    strHead = strHead.Remove(strHead.LastIndexOf(','));
                    StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                    sw.WriteLine(strHead);
                    sw.Close();
                }

            }
            catch (Exception)
            {
            }
            try
            {

                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public static void WriteResult(string strPath, string strContent)
        {
            Action<string, string> dele = WriteFile;
            dele.BeginInvoke(strPath, strContent, WriteCallBack, null);
        }
        private static  void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string, string> dele = (Action<string, string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }
        #endregion
    }
}
