using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ConfigureFile;
using System.Threading;
using Motion;
namespace Assembly
{
    public class ShowImageClass
    {
       
        //private event EventHandler<ImageArgs> onCheckA1;
        //private event EventHandler<ImageArgs> onCheckA2;
        //private event EventHandler<ImageArgs> onCheckC1;
        //private event EventHandler<ImageArgs> onCheckC2;

        public Thread tA1;
        public Thread tA2;
        public Thread tC1;
        public Thread tC2;
        public Queue<ImageArgs> lstImgA1 = new Queue<ImageArgs>();
        public Queue<ImageArgs> lstImgA2 = new Queue<ImageArgs>();
        public Queue<ImageArgs> lstImgC1 = new Queue<ImageArgs>();
        public Queue<ImageArgs> lstImgC2 = new Queue<ImageArgs>();
        private Mutex muA1 = new Mutex();
        private Mutex muA2 = new Mutex();
        private Mutex muC1 = new Mutex();
        private Mutex muC2 = new Mutex();

        MotionCard mc = null;
        private string strShowColor = "magenta";//正常显示信息颜色
        private string strShowNgColor = "red";   //显示NG颜色
        public ShowImageClass()
        {
            mc = MotionCard.getMotionCard();
            tA1 = new Thread(RunA1);
            tA1.Priority = ThreadPriority.Highest;
            tA1.Start();
            tA2 = new Thread(RunA2);
            tA2.Priority = ThreadPriority.Highest;
            tA2.Start();
            tC1 = new Thread(RunC1);
            tC1.Priority = ThreadPriority.Highest;
            tC1.Start();
            tC2 = new Thread(RunC2);
            tC2.Priority = ThreadPriority.Highest;
            tC2.Start();
            //ThreadPool.SetMinThreads(1, 1);
            //ThreadPool.SetMaxThreads(20, 20);

            //onCheckA1 += ShowImageClass_onCheckA1;
            //onCheckA2 += ShowImageClass_onCheckA2;
            //onCheckC1 += ShowImageClass_onCheckC1;
            //onCheckC2 += ShowImageClass_onCheckC2;
        }
        #region"线程"
        public void RunA1()
        {
            while (true)
            {
                try
                {

               
                    if (lstImgA1.Count == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }
                    ImageArgs e = new ImageArgs();
                    muA1.WaitOne();
                    e = lstImgA1.Dequeue();
                    CommonSet.WriteInfo("取料1出列后数目" + lstImgA1.Count);
                    muA1.ReleaseMutex();

                    ActionAsynA1Down(e.Index, e.Image, e.BTest);
                   
                }
                catch (Exception ex)
                {
                    CommonSet.WriteInfo("取料1下线程处理图像异常"+ex.ToString());

                }
                Thread.Sleep(20);
            }
        }
        public void RunA2()
        {
            while (true)
            {
                try
                {


                    if (lstImgA2.Count == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }
                    ImageArgs e = new ImageArgs();
                    muA2.WaitOne();
                    e = lstImgA2.Dequeue();
                    muA2.ReleaseMutex();

                    ActionAsynA2Down(e.Index, e.Image, e.BTest);
                }
                catch (Exception ex)
                {
                    CommonSet.WriteInfo("取料2下线程处理图像异常" + ex.ToString());

                }
                Thread.Sleep(20);
            }
        }

        public void RunC1()
        {
            while (true)
            {
                try
                {


                    if (lstImgC1.Count == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }
                    ImageArgs e = new ImageArgs();
                    muC1.WaitOne();
                    e = lstImgC1.Dequeue();
                    CommonSet.WriteInfo("组装1出列后数目" + lstImgC1.Count);
                    muC1.ReleaseMutex();

                    ActionAsynC1Down(e.Index, e.Image, e.BTest);
                }
                catch (Exception ex)
                {
                    CommonSet.WriteInfo("组装1下线程处理图像异常" + ex.ToString());

                }
                Thread.Sleep(20);
            }
        }
        public void RunC2()
        {
            while (true)
            {
                try
                {
                    if (lstImgC2.Count == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }
                    ImageArgs e = new ImageArgs();
                    muC2.WaitOne();
                    e = lstImgC2.Dequeue();
                    CommonSet.WriteInfo("组装2出列后数目" + lstImgC2.Count);
                    muC2.ReleaseMutex();

                    ActionAsynC2Down(e.Index, e.Image, e.BTest);
                }
                catch (Exception ex)
                {
                    CommonSet.WriteInfo("组装2下线程处理图像异常" + ex.ToString());

                }
                Thread.Sleep(20);
            }
        }


        #endregion

        #region"无效"
        void ShowImageClass_onCheckC2(object sender, ImageArgs e)
        {
            ActionAsynC2Down(e.Index, e.Image, e.BTest);
        }

        void ShowImageClass_onCheckC1(object sender, ImageArgs e)
        {
            ActionAsynC1Down(e.Index, e.Image, e.BTest);
        }
        void testCallBackA1(object args)
        {
            ImageArgs e = (ImageArgs)args;
            ActionAsynA1Down(e.Index, e.Image, e.BTest);
        }
        void testCallBack(object args)
        {
            ImageArgs e = (ImageArgs)args;
            ActionAsynC1Down(e.Index, e.Image, e.BTest);
        }
        
        void testCallBack2(object args)
        {
            ImageArgs e = (ImageArgs)args;
            ActionAsynC2Down(e.Index, e.Image, e.BTest);
        }
        void ShowImageClass_onCheckA2(object sender, ImageArgs e)
        {
            ActionAsynA2Down(e.Index, e.Image, e.BTest);
        }

        void ShowImageClass_onCheckA1(object sender, ImageArgs e)
        {
            ActionAsynA1Down(e.Index, e.Image, e.BTest);
        }
        #endregion

        #region"取料工位1相机"
        //下相机
        public  void ProcessImageA1Down(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -OptSution1.dDownAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.手动:

                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (CommonSet.dic_Calib[CalibStation.取料工位1].hImageDown != null)
                        //        {

                        //            CommonSet.dic_Calib[CalibStation.取料工位1].hImageDown.Dispose();
                        //        }
                        //        HOperatorSet.CopyImage(image, out CommonSet.dic_Calib[CalibStation.取料工位1].hImageDown);
                        //        HOperatorSet.DispImage(CommonSet.dic_Calib[CalibStation.取料工位1].hImageDown, FrmMain.frmOpt1.GetDownWindow());
                        //        HOperatorSet.DispCross(FrmMain.frmOpt1.GetDownWindow(), height.I / 2, width.I / 2, 3000, 0);
                        //        return;
                        //    }
                        //}
                    //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 0)
                        {

                            try
                            {
                                if (CommonSet.dic_OptSuction1[1].hImageDown != null) 
                                     CommonSet.dic_OptSuction1[1].hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[1].hImageDown);
                               // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmOpt1.GetDownWindow();
                                // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageDown, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {
                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            
                            }

                        }
                       
                        

                        break;
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            for (; FlashModule1.iPicNum < 10; FlashModule1.iPicNum++)
                            {
                                int index = FlashModule1.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                OptSution1 ps = CommonSet.dic_OptSuction1[index];
                                if (ps.BUse)
                                {
                                    //swProcessListener.Restart();
                                    if (CommonSet.dic_OptSuction1[index].hImageDown != null) 
                                    CommonSet.dic_OptSuction1[index].hImageDown.Dispose();
                                    HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[index].hImageDown);

                                    // CommonSet.dic_Suction[1].hImage = image;
                                    HOperatorSet.SetPart(FrmShowImage.lstA1Win[index - 1], 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, FrmShowImage.lstA1Win[index - 1]);
                                    FlashModule1.iPicNum++;
                                    FlashModule1.iPicSum--;
                                    ImageArgs args = new ImageArgs();
                                    args.Index = index;
                                    args.Image = CommonSet.dic_OptSuction1[index].hImageDown;
                                    args.BTest = false;
                                    

                                    muA1.WaitOne();
                                    lstImgA1.Enqueue(args);
                                    muA1.ReleaseMutex();
                                    CommonSet.WriteInfo("取料1队列数目" + lstImgA1.Count);
                                   // this.onCheckA1(this, args);
                                    //Task t = Task.Factory.StartNew(() => ActionAsynA1Down(index, CommonSet.dic_OptSuction1[index].hImageDown, false));
                                    //Action<int, HObject,bool> dele = ActionAsynA1Down;
                                    //dele.BeginInvoke(index, CommonSet.dic_OptSuction1[index].hImageDown, false, ActionCallBack, null);
                                    break;
                                }
                                else
                                {
                                    HOperatorSet.SetPart(FrmShowImage.lstA1Win[index - 1], 0, 0, height, width);
                                    HOperatorSet.ClearWindow(FrmShowImage.lstA1Win[index - 1]);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                             CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.飞拍测试:
                        try
                        {

                            for (; TestFlash.iPicNum < 10; )
                            {
                                int index = TestFlash.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                OptSution1 ps = CommonSet.dic_OptSuction1[index];
                               
                                //swProcessListener.Restart();
                                if (CommonSet.dic_OptSuction1[index].hImageDown != null) 
                                     CommonSet.dic_OptSuction1[index].hImageDown.Dispose();
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[index].hImageDown);

                                // CommonSet.dic_Suction[1].hImage = image;
                                HOperatorSet.SetPart(CommonSet.lstFlashWin[index - 1], 0, 0, height.I, width.I);
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, CommonSet.lstFlashWin[index - 1]);
                                TestFlash.iPicNum++;
                                TestFlash.iPicSum--;
                                ImageArgs args = new ImageArgs();
                                args.Index = index;
                                args.Image = CommonSet.dic_OptSuction1[index].hImageDown;
                                args.BTest = true;
                                muA1.WaitOne();
                                lstImgA1.Enqueue(args);
                                muA1.ReleaseMutex();
                                CommonSet.WriteInfo("取料1队列数目" + lstImgA1.Count);
                               // this.onCheckA1(this, args);
                               // Task t = Task.Factory.StartNew(() => ActionAsynA1Down(index, CommonSet.dic_OptSuction1[index].hImageDown, true));
                                //Action<int, HObject, bool> dele = ActionAsynA1Down;
                                //dele.BeginInvoke(index, CommonSet.dic_OptSuction1[index].hImageDown, true, ActionCallBack, null);
                               // dele.Invoke(index, CommonSet.dic_OptSuction1[index].hImageDown, true);
                                if (!TestFlash.bFlash)
                                {
                                    break;
                                }
                                break;
                               
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.组装验证:
                        try
                        {
                            int index = ResultTestModule.iCurrentSuctionOrder;
                            CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");


                            //swProcessListener.Restart();
                            if (CommonSet.dic_OptSuction1[index].hImageDown != null)
                                CommonSet.dic_OptSuction1[index].hImageDown.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[index].hImageDown);

                            HWindow hwin = FrmMain.frmOpt1.GetDownWindow();
                            if (ResultTestModule.bUseOptOnly)
                            {
                                hwin = FrmMain.frmOpt1.GetDownWindow();
                            }
                            else
                            {
                                hwin = FrmMain.frmAssem1.GetUpWindow();
                            }
                            // CommonSet.dic_Suction[1].hImage = image;
                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, hwin);

                            //Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true));

                            CommonSet.dic_OptSuction1[index].pfDown.SetRun(true);

                            CommonSet.dic_OptSuction1[index].pfDown.hwin = hwin;

                            CommonSet.dic_OptSuction1[index].actionDown(CommonSet.dic_OptSuction1[index].hImageDown);
                            CommonSet.dic_OptSuction1[index].pfDown.showObj();
                            string strDispRow = "Row:" + CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn.ToString("0.000");
                            double dDiffDownX = (CommonSet.dic_OptSuction1[index].pSuctionCenter.X - CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow) * OptSution1.GetDownResulotion();
                            double dDiffDownY = (CommonSet.dic_OptSuction1[index].pSuctionCenter.Y - CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn) * OptSution1.GetDownResulotion();
                            // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "true");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "true");
                            CommonSet.disp_message(hwin, CommonSet.dic_OptSuction1[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "true");
                            CommonSet.disp_message(hwin, "和吸笔中心差X:" + dDiffDownX.ToString("0.000") + "mm", "window", 60, 0, strShowColor, "true");
                            CommonSet.disp_message(hwin, "和吸笔中心差Y:" + dDiffDownY.ToString("0.000") + "mm", "window", 80, 0, strShowColor, "true");
                            HOperatorSet.DispCross(hwin, CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow, CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn, 100, 0);



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("取料1下相机图像处理异常", ex);
                        }
                        break;
                    default:
                        break;
                }

            }
            image.Dispose();
        }
        //上相机
        public  void ProcessImageA1Up(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -OptSution1.dUpAng, "constant");
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
                        if (FrmMain.iSelectParamSetIndex == 0)
                        {

                            try
                            {
                                if (CommonSet.dic_OptSuction1[1].hImageUP != null) 
                                 CommonSet.dic_OptSuction1[1].hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[1].hImageUP);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmOpt1.GetUpWindow();
                               // HWindow hwin = FrmMain.optSuctionUI.GetUpWindow();
                                // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);

                                if (Run.runMode == RunMode.取料中心对位测试)
                                {
                                  
                                    string strDispRow = "";
                                    string strDispCol = "";
                                    string strAngle = "";


                                    int iSuction = AutoGetCenterPosTestModule.iSuctionNum;

                                    if (CommonSet.dic_OptSuction1[iSuction].hImageUP != null)
                                        CommonSet.dic_OptSuction1[iSuction].hImageUP.Dispose();
                                    HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[iSuction].hImageUP);

                                  

                                    HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(CommonSet.dic_OptSuction1[iSuction].hImageUP, hwin);
                                    CommonSet.dic_OptSuction1[iSuction].pfUp.SetRun(true);
                                    CommonSet.dic_OptSuction1[iSuction].pfUp.hwin = hwin;
                                    CommonSet.dic_OptSuction1[iSuction].actionUp(CommonSet.dic_OptSuction1[iSuction].hImageUP);
                                    CommonSet.dic_OptSuction1[iSuction].pfUp.showObj();
                                    strDispRow = "Row:" + CommonSet.dic_OptSuction1[iSuction].imgResultUp.CenterRow.ToString("0.000");
                                    strDispCol = "Col:" + CommonSet.dic_OptSuction1[iSuction].imgResultUp.CenterColumn.ToString("0.000");
                                    CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                                    CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                                }
                            }
                            catch (Exception ex) {
                                MessageBox.Show("显示图像异常"+ex.ToString());
                            }


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
                            

                            int iSuction= GetProduct1Module.iCurrentSuction;
                            if (CommonSet.dic_OptSuction1[iSuction].hImageUP != null) 
                                  CommonSet.dic_OptSuction1[iSuction].hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[iSuction].hImageUP);

                            hwin = CommonSet.lstOtherWin[0];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction1[iSuction].hImageUP, hwin);
                            CommonSet.dic_OptSuction1[iSuction].pfUp.SetRun(true);
                            CommonSet.dic_OptSuction1[iSuction].pfUp.hwin = hwin;
                            CommonSet.dic_OptSuction1[iSuction].actionUp(CommonSet.dic_OptSuction1[iSuction].hImageUP);
                            CommonSet.dic_OptSuction1[iSuction].pfUp.showObj();
                            strDispRow = "Row:" + CommonSet.dic_OptSuction1[iSuction].imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + CommonSet.dic_OptSuction1[iSuction].imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                           


                        }
                        catch (Exception ex)
                        {
                             CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if (CommonSet.dic_OptSuction1[1].hImageUP!=null)
                                  CommonSet.dic_OptSuction1[1].hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[1].hImageUP);

                            hwin = CommonSet.hPixelToAxisCalibWin;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction1[1].hImageUP, hwin);
                            CommonSet.dic_OptSuction1[1].pfUp.SetRun(true);
                            CommonSet.dic_OptSuction1[1].pfUp.hwin = hwin;
                            CommonSet.dic_OptSuction1[1].actionUp(CommonSet.dic_OptSuction1[1].hImageUP);
                            CommonSet.dic_OptSuction1[1].pfUp.showObj();
                            strDispRow = "Row:" + CommonSet.dic_OptSuction1[1].imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + CommonSet.dic_OptSuction1[1].imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        #endregion
        #region"取料工位2相机"
        //下相机
        public  void ProcessImageA2Down(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -OptSution2.dDownAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.手动:

                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (CommonSet.dic_Calib[CalibStation.取料工位2].hImageDown != null)
                        //        {

                        //            CommonSet.dic_Calib[CalibStation.取料工位2].hImageDown.Dispose();
                        //        }
                        //        HOperatorSet.CopyImage(image, out CommonSet.dic_Calib[CalibStation.取料工位2].hImageDown);
                        //        HOperatorSet.DispImage(CommonSet.dic_Calib[CalibStation.取料工位2].hImageDown, FrmMain.frmOpt2.GetDownWindow());
                        //        HOperatorSet.DispCross(FrmMain.frmOpt2.GetDownWindow(), height.I / 2, width.I / 2, 3000, 0);
                        //        return;
                        //    }
                        //}
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 1)
                        {

                            try
                            {
                                if( CommonSet.dic_OptSuction2[10].hImageDown!=null)
                                     CommonSet.dic_OptSuction2[10].hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[10].hImageDown);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmOpt2.GetDownWindow();
                                // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageDown, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) { }

                        }



                        break;
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            for (; FlashModule2.iPicNum < 10; FlashModule2.iPicNum++)
                            {
                                int index = FlashModule2.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                OptSution2 ps = CommonSet.dic_OptSuction2[index+9];
                                if (ps.BUse)
                                {
                                    if(CommonSet.dic_OptSuction2[index + 9].hImageDown!=null)
                                    CommonSet.dic_OptSuction2[index + 9].hImageDown.Dispose();
                                    //swProcessListener.Restart();
                                    HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[index+9].hImageDown);

                                    // CommonSet.dic_Suction[1].hImage = image;
                                    HOperatorSet.SetPart(FrmShowImage.lstA2Win[index - 1], 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, FrmShowImage.lstA2Win[index - 1]);
                                    FlashModule2.iPicNum++;
                                    FlashModule2.iPicSum--;
                                    ImageArgs args = new ImageArgs();
                                    args.Index = index+9;
                                    args.Image = CommonSet.dic_OptSuction2[index+9].hImageDown;
                                    args.BTest = false;
                                   
                                    muA2.WaitOne();
                                    lstImgA2.Enqueue(args);
                                    muA2.ReleaseMutex();
                                    CommonSet.WriteInfo("取料2队列数目" + lstImgA2.Count);
                                   // this.onCheckA2(this, args);
                                  //  Task t = Task.Factory.StartNew(() => ActionAsynA2Down(index+9, CommonSet.dic_OptSuction2[index+9].hImageDown, false));
                                    //Action<int, HObject, bool> dele = ActionAsynA2Down;
                                    //dele.BeginInvoke(index + 9, CommonSet.dic_OptSuction2[index + 9].hImageDown, false, ActionCallBack, null);
                                    break;
                                }
                                else
                                {
                                    HOperatorSet.SetPart(FrmShowImage.lstA2Win[index - 1], 0, 0, height, width);
                                    HOperatorSet.ClearWindow(FrmShowImage.lstA2Win[index - 1]);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("工位2取料下相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.飞拍测试:
                        try
                        {

                            for (; TestFlash.iPicNum < 10; )
                            {
                                int index = TestFlash.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                OptSution2 ps = CommonSet.dic_OptSuction2[index+9];
                                if (CommonSet.dic_OptSuction2[index + 9].hImageDown != null) 
                                CommonSet.dic_OptSuction2[index + 9].hImageDown.Dispose();
                                //swProcessListener.Restart();
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[index+9].hImageDown);

                                // CommonSet.dic_Suction[1].hImage = image;
                                HOperatorSet.SetPart(CommonSet.lstFlashWin[index - 1], 0, 0, height.I, width.I);
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, CommonSet.lstFlashWin[index - 1]);
                                TestFlash.iPicNum++;
                                TestFlash.iPicSum--;
                                ImageArgs args = new ImageArgs();
                                args.Index = index + 9;
                                args.Image = CommonSet.dic_OptSuction2[index + 9].hImageDown;
                                args.BTest = true;

                                muA2.WaitOne();
                                lstImgA2.Enqueue(args);
                                muA2.ReleaseMutex();
                                CommonSet.WriteInfo("取料2队列数目" + lstImgA2.Count);
                                //Action<int, HObject, bool> dele = ActionAsynA2Down;
                                //dele.BeginInvoke(index + 9, CommonSet.dic_OptSuction2[index + 9].hImageDown, true, ActionCallBack, null);
                                if (!TestFlash.bFlash)
                                {
                                    break;
                                }
                                break;

                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.组装验证:
                        try
                        {
                            int index = ResultTestModule.iCurrentSuctionOrder;
                            CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");


                            //swProcessListener.Restart();
                            if (CommonSet.dic_OptSuction2[index].hImageDown != null)
                                CommonSet.dic_OptSuction2[index].hImageDown.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[index].hImageDown);

                            HWindow hwin = FrmMain.frmOpt2.GetDownWindow();
                            if (ResultTestModule.bUseOptOnly)
                            {
                                hwin = FrmMain.frmOpt2.GetDownWindow();
                            }
                            else
                            {
                                hwin = FrmMain.frmAssem2.GetUpWindow();
                            }
                            // CommonSet.dic_Suction[1].hImage = image;
                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index].hImageDown, hwin);

                            //Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true));

                            CommonSet.dic_OptSuction2[index].pfDown.SetRun(true);

                            CommonSet.dic_OptSuction2[index].pfDown.hwin = hwin;

                            CommonSet.dic_OptSuction2[index].actionDown(CommonSet.dic_OptSuction2[index].hImageDown);
                            CommonSet.dic_OptSuction2[index].pfDown.showObj();
                            string strDispRow = "Row:" + CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn.ToString("0.000");
                            double dDiffDownX = (CommonSet.dic_OptSuction2[index].pSuctionCenter.X - CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow) * OptSution2.GetDownResulotion();
                            double dDiffDownY = (CommonSet.dic_OptSuction2[index].pSuctionCenter.Y - CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn) * OptSution2.GetDownResulotion();
                            // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, CommonSet.dic_OptSuction2[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "true");
                            CommonSet.disp_message(hwin, "和吸笔中心差X:" + dDiffDownX.ToString("0.000") + "mm", "window", 60, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, "和吸笔中心差Y:" + dDiffDownY.ToString("0.000") + "mm", "window", 80, 0, strShowColor, "false");
                            HOperatorSet.DispCross(hwin, CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow, CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn, 100, 0);



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("取料2下相机图像处理异常", ex);
                        }
                        break;
                    default:
                        break;
                }

            }
            image.Dispose();
        }
        //上相机
        public  void ProcessImageA2Up(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -OptSution2.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.手动:

                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (CommonSet.dic_Calib[CalibStation.取料工位2].hImageUp != null)
                        //        {

                        //            CommonSet.dic_Calib[CalibStation.取料工位2].hImageUp.Dispose();
                        //        }
                        //        HOperatorSet.CopyImage(image, out CommonSet.dic_Calib[CalibStation.取料工位2].hImageUp);
                        //        HOperatorSet.DispImage(CommonSet.dic_Calib[CalibStation.取料工位2].hImageUp, FrmMain.frmOpt2.GetUpWindow());
                        //        HOperatorSet.SetColor(FrmMain.frmOpt2.GetUpWindow(), "green");
                        //        HOperatorSet.DispCross(FrmMain.frmOpt2.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);
                        //        return;
                        //    }
                        //}
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 1)
                        {

                            try
                            {
                                if(CommonSet.dic_OptSuction2[10].hImageUP != null)
                                 CommonSet.dic_OptSuction2[10].hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[10].hImageUP);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmOpt2.GetUpWindow();
                                // HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) { }

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

                            int iSuction = GetProduct2Module.iCurrentSuction;
                            if( CommonSet.dic_OptSuction2[iSuction].hImageUP!=null)
                             CommonSet.dic_OptSuction2[iSuction].hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[iSuction].hImageUP);

                            hwin = CommonSet.lstOtherWin[1];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction2[iSuction].hImageUP, hwin);
                            CommonSet.dic_OptSuction2[iSuction].pfUp.SetRun(true);
                            CommonSet.dic_OptSuction2[iSuction].pfUp.hwin = hwin;
                            CommonSet.dic_OptSuction2[iSuction].actionUp(CommonSet.dic_OptSuction2[iSuction].hImageUP);
                            CommonSet.dic_OptSuction2[iSuction].pfUp.showObj();
                            strDispRow = "Row:" + CommonSet.dic_OptSuction2[iSuction].imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + CommonSet.dic_OptSuction2[iSuction].imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            HOperatorSet.DispCross(hwin, CommonSet.dic_OptSuction2[iSuction].imgResultUp.CenterRow, CommonSet.dic_OptSuction2[iSuction].imgResultUp.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if(CommonSet.dic_OptSuction2[10].hImageUP!=null)
                                CommonSet.dic_OptSuction2[10].hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction2[10].hImageUP);

                            hwin = CommonSet.hPixelToAxisCalibWin;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_OptSuction2[10].hImageUP, hwin);
                            CommonSet.dic_OptSuction2[10].pfUp.SetRun(true);
                            CommonSet.dic_OptSuction2[10].pfUp.hwin = hwin;
                            CommonSet.dic_OptSuction2[10].actionUp(CommonSet.dic_OptSuction2[10].hImageUP);
                            CommonSet.dic_OptSuction2[10].pfUp.showObj();
                            strDispRow = "Row:" + CommonSet.dic_OptSuction2[10].imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + CommonSet.dic_OptSuction2[10].imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;


                    default:
                        break;
                }

            }
        }
        #endregion
        #region"组装工位1相机"
        //下相机
        public  void ProcessImageC1Down(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -AssembleSuction1.dDownAng , "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.自动标定:
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {
                            if (CommonSet.dic_Assemble1[1].hImageDown != null)
                                CommonSet.dic_Assemble1[1].hImageDown.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[1].hImageDown);
                            HOperatorSet.DispImage(CommonSet.dic_Assemble1[1].hImageDown, FrmMain.frmAssem1.GetDownWindow());
                            HOperatorSet.DispCross(FrmMain.frmAssem1.GetDownWindow(), height.I / 2, width.I / 2, 3000, 0);

                            HWindow hw = FrmMain.frmAssem1.GetDownWindow();
                            CommonSet.dic_Assemble1[1].pfDown.SetRun(true);

                            CommonSet.dic_Assemble1[1].pfDown.hwin = hw;

                            CommonSet.dic_Assemble1[1].actionDown(CommonSet.dic_Assemble1[1].hImageDown);
                            CommonSet.dic_Assemble1[1].pfDown.showObj();
                            string strDispRow = "Row:" + CommonSet.dic_Assemble1[1].imgResultDown.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn.ToString("0.000");
                            // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                            CommonSet.disp_message(hw, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hw, strDispCol, "window", 20, 0, strShowColor, "false");
                            HOperatorSet.DispCross(hw, CommonSet.dic_Assemble1[1].imgResultDown.CenterRow, CommonSet.dic_Assemble1[1].imgResultDown.CenterColumn, 100, 0);
                            HOperatorSet.DispCross(hw, height.I / 2, width.I / 2, 3000, 0);


                            return;
                        }
                        break;
                    case RunMode.手动:
                     
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {

                            try
                            {
                               
                                if( CommonSet.dic_Assemble1[1].hImageDown!=null)
                                    CommonSet.dic_Assemble1[1].hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[1].hImageDown);
                               
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmAssem1.GetDownWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_Assemble1[1].hImageDown, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {

                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

                        }



                        break;
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            for (; Assem1Module.iPicNum < 10; Assem1Module.iPicNum++)
                            {
                                int index = Assem1Module.iPicNum;
                              
                                AssembleSuction1 ps = CommonSet.dic_Assemble1[index];
                                if (ps.BUse)
                                {
                                    CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                    if(CommonSet.dic_Assemble1[index].hImageDown!=null)
                                         CommonSet.dic_Assemble1[index].hImageDown.Dispose();
                                    //swProcessListener.Restart();
                                    HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[index].hImageDown);

                                    // CommonSet.dic_Suction[1].hImage = image;
                                    HOperatorSet.SetPart(FrmShowImage.lstC1Win[index - 1], 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, FrmShowImage.lstC1Win[index - 1]);
                                    Assem1Module.iPicNum++;
                                    Assem1Module.iPicSum--;
                                    ImageArgs args = new ImageArgs();
                                    args.Index = index ;
                                    args.Image = CommonSet.dic_Assemble1[index].hImageDown;
                                    args.BTest = false;
                                    muC1.WaitOne();
                                    lstImgC1.Enqueue(args);
                                    muC1.ReleaseMutex();
                                    CommonSet.WriteInfo("组装1队列数目" + lstImgC1.Count);
                                   // this.onCheckC1(this, args);
                                   // Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, false));
                                    //Action<int, HObject, bool> dele = ActionAsynC1Down;
                                    //dele.BeginInvoke(index, CommonSet.dic_Assemble1[index].hImageDown, false, ActionCallBack, null);
                                    break;
                                }
                                else
                                {
                                    HOperatorSet.SetPart(FrmShowImage.lstC1Win[index - 1], 0, 0, height, width);
                                    HOperatorSet.ClearWindow(FrmShowImage.lstC1Win[index - 1]);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
                        }
                        break;
                   
                    case RunMode.飞拍测试:
                        try
                        {
                            for (; TestFlash.iPicNum < 10; )
                            {
                                int index = TestFlash.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                AssembleSuction1 ps = CommonSet.dic_Assemble1[index];

                                //swProcessListener.Restart();
                                if( CommonSet.dic_Assemble1[index].hImageDown!=null)
                                    CommonSet.dic_Assemble1[index].hImageDown.Dispose();
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[index].hImageDown);

                                // CommonSet.dic_Suction[1].hImage = image;
                                HOperatorSet.SetPart(CommonSet.lstFlashWin[index - 1], 0, 0, height.I, width.I);
                                HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, CommonSet.lstFlashWin[index - 1]);
                                TestFlash.iPicNum++;
                                TestFlash.iPicSum--;
                                ImageArgs args = new ImageArgs();
                                args.Index = index;
                                args.Image = CommonSet.dic_Assemble1[index].hImageDown;
                                args.BTest = true;
                                muC1.WaitOne();
                                lstImgC1.Enqueue(args);
                                muC1.ReleaseMutex();
                                CommonSet.WriteInfo("组装1队列数目" + lstImgC1.Count);
                               // this.onCheckC1(this, args);
                               // Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true));
                              
                                //ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true);
                               // Action<int, HObject, bool> dele = ActionAsynC1Down;
                               
                               // dele.BeginInvoke(index, CommonSet.dic_Assemble1[index].hImageDown, true, ActionCallBack, null);
                                if (!TestFlash.bFlash)
                                {
                                    break;
                                }
                                break;
                                
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.组装取料测试:
                    case RunMode.组装验证:
                        try
                        {
                                int index = ResultTestModule.iCurrentSuctionOrder;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                AssembleSuction1 ps = CommonSet.dic_Assemble1[index];
                             
                                //swProcessListener.Restart();
                                if (CommonSet.dic_Assemble1[index].hImageDown != null)
                                    CommonSet.dic_Assemble1[index].hImageDown.Dispose();
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[index].hImageDown);

                                HWindow hwin = FrmMain.frmAssem1.GetDownWindow();
                                // CommonSet.dic_Suction[1].hImage = image;
                                HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, hwin);
                              
                                //Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true));

                                CommonSet.dic_Assemble1[index].pfDown.SetRun(true);
                               
                                CommonSet.dic_Assemble1[index].pfDown.hwin = hwin;
                               
                                CommonSet.dic_Assemble1[index].actionDown(CommonSet.dic_Assemble1[index].hImageDown);
                                CommonSet.dic_Assemble1[index].pfDown.showObj();
                                string strDispRow = "Row:" + CommonSet.dic_Assemble1[index].imgResultDown.CenterRow.ToString("0.000");
                                string strDispCol = "Col:" + CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn.ToString("0.000");
                                double dDiffDownX = (1024 - CommonSet.dic_Assemble1[index].imgResultDown.CenterRow) * AssembleSuction1.GetDownResulotion();
                                double dDiffDownY = (1224 - CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn) * AssembleSuction1.GetDownResulotion();
                                // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                                CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, CommonSet.dic_Assemble1[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, "中心差X:" + dDiffDownX.ToString("0.000") + "mm", "window", 60, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, "中心差Y:" + dDiffDownY.ToString("0.000") + "mm", "window", 80, 0, strShowColor, "false");
                                // CommonSet.disp_message(CommonSet.lstA1Win[index - 1], strRadius, "window", 60, 0, "cyan", "false");
                                HOperatorSet.DispCross(hwin, CommonSet.dic_Assemble1[index].imgResultDown.CenterRow, CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn, 100, 0);

                                double dDiffDownX1 = (CommonSet.dic_Assemble1[index].pSuctionCenter.X - CommonSet.dic_Assemble1[index].imgResultDown.CenterRow) * AssembleSuction1.GetDownResulotion();
                                double dDiffDownY1 = (CommonSet.dic_Assemble1[index].pSuctionCenter.Y - CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn) * AssembleSuction1.GetDownResulotion();
                                CommonSet.disp_message(hwin, "和吸笔中心差X:" + dDiffDownX1.ToString("0.000") + "mm", "window", 180, 0, "black", "false");
                                CommonSet.disp_message(hwin, "和吸笔中心差Y:" + dDiffDownY1.ToString("0.000") + "mm", "window", 200, 0, "black", "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装1下相机图像处理异常", ex);
                        }
                        break;
                    default:
                        break;
                }

            }
            image.Dispose();
        }
    
        //上相机
        public  void ProcessImageC1Up(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -AssembleSuction1.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.自动标定:
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {
                            if (AssembleSuction1.hImageUP != null)
                                AssembleSuction1.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HWindow hwin = FrmMain.frmAssem1.GetUpWindow();
                            HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);
                            HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);

                            AssembleSuction1.pfUp.SetRun(true);
                            AssembleSuction1.pfUp.hwin = hwin;
                            AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                            AssembleSuction1.pfUp.showObj();
                            string strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);

                            break;
                        }
                        break;
                    case RunMode.手动:
                        #region "无效"
                        //if (CommonSet.bCalibFlag)
                        //{
                        //    if (FrmDebug.iSelectParamSetIndex == 0)
                        //    {
                        //        if (AssembleSuction1.hImageUP != null)
                        //            AssembleSuction1.hImageUP.Dispose();
                        //        //手动情况下，将图像存入第一个吸笔的图像变量中
                        //        HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);
                        //        //// HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                        //        HWindow hwin = CommonSet.hPixelToAxisCalibWin;
                        //        //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                        //        ////HOperatorSet.getpart
                        //        HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                        //        HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                        //        break;
                        //    }
                        //    else if (FrmDebug.iSelectParamSetIndex == 1)
                        //    {
                        //        if (AssembleSuction1.hImageUP != null)
                        //            AssembleSuction1.hImageUP.Dispose();
                        //        //手动情况下，将图像存入第一个吸笔的图像变量中
                        //        HWindow hwin = FrmMain.frmAssem1.GetUpWindow();
                        //        HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);
                        //        HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                        //        HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                
                        //            AssembleSuction1.pfUp.SetRun(true);
                        //            AssembleSuction1.pfUp.hwin = hwin;
                        //            AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                        //            AssembleSuction1.pfUp.showObj();
                        //            string strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                        //            string strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                        //            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                        //            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                        //            HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                        //            HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);
                                
                        //        break;
                        //    }
                        //}
                        #endregion
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {

                            try
                            {
                               
                                if(AssembleSuction1.hImageUP!=null)
                                    AssembleSuction1.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);
                                //// HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmAssem1.GetUpWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {
                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

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
                            if (AssembleSuction1.hImageUP != null)
                                 AssembleSuction1.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);

                            hwin = CommonSet.lstOtherWin[2];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                            AssembleSuction1.pfUp.SetRun(true);
                            AssembleSuction1.pfUp.hwin = hwin;
                            AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                            AssembleSuction1.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            ShowResult(hwin, AssembleSuction1.imgResultUp.bImageResult, 60);
                            HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装1上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.飞拍测试:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";
                            if (AssembleSuction1.hImageUP != null)
                                AssembleSuction1.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);

                            hwin = CommonSet.lstFlashWin[10];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                            AssembleSuction1.pfUp.SetRun(true);
                            AssembleSuction1.pfUp.hwin = hwin;
                            AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                            AssembleSuction1.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            ShowResult(hwin, AssembleSuction1.imgResultUp.bImageResult, 60);
                            HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装1上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if (AssembleSuction1.hImageUP != null)
                                 AssembleSuction1.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);

                            hwin = CommonSet.hPixelToAxisCalibWin;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                            AssembleSuction1.pfUp.SetRun(true);
                            AssembleSuction1.pfUp.hwin = hwin;
                            AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                            AssembleSuction1.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.组装验证:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";
                            if (AssembleSuction1.hImageUP != null)
                                AssembleSuction1.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction1.hImageUP);

                            hwin = FrmMain.frmAssem1.GetUpWindow();

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                            if (ResultTestModule.iPos == 1)
                            {
                                AssembleSuction1.pfUp.SetRun(true);
                                AssembleSuction1.pfUp.hwin = hwin;
                                AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                                AssembleSuction1.pfUp.showObj();
                                strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                                strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                                HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);
                                try
                                {

                                    double dDiffUpX = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                                    double dDiffUpY = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();

                                    strDispRow = "中心差DX:" + dDiffUpX.ToString("0.000") + "mm";
                                    strDispCol = "中心差DY:" + dDiffUpY.ToString("0.000") + "mm";
                                    CommonSet.disp_message(hwin, strDispRow, "window", 40, 0, strShowNgColor, "false");
                                    CommonSet.disp_message(hwin, strDispCol, "window", 60, 0, strShowNgColor, "false");
                                   



                                   
                                    // p.Z = DGetPosZ;
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                AssembleSuction1.pfUp.SetRun(true);
                                AssembleSuction1.pfUp.hwin = hwin;
                                AssembleSuction1.actionUp(AssembleSuction1.hImageUP);
                                AssembleSuction1.pfUp.showObj();
                                //strDispRow = "Row:" + AssembleSuction1.imgResultUp.CenterRow.ToString("0.000");
                                //strDispCol = "Col:" + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000");
                                //CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                                //CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");
                                HOperatorSet.DispCross(hwin, AssembleSuction1.imgResultUp.CenterRow, AssembleSuction1.imgResultUp.CenterColumn, 100, 0);
                                HOperatorSet.DispCross(hwin, ResultTestModule.dRow, ResultTestModule.dCol, 200, 0);
                                try
                                {

                                    double dDiffUpX = (AssembleSuction1.imgResultUp.CenterRow - 1024) * AssembleSuction1.GetUpResulotion();
                                    double dDiffUpY = (AssembleSuction1.imgResultUp.CenterColumn - 1224) * AssembleSuction1.GetUpResulotion();

                                    strDispRow = "中心差DX:" + dDiffUpX.ToString("0.000") + "mm";
                                    strDispCol = "中心差DY:" + dDiffUpY.ToString("0.000") + "mm";
                                    CommonSet.disp_message(hwin, strDispRow, "window", 40, 0, strShowNgColor, "false");
                                    CommonSet.disp_message(hwin, strDispCol, "window", 60, 0, strShowNgColor, "false");
                                    // p.Z = DGetPosZ;
                                }
                                catch (Exception ex)
                                {

                                }
                                
                            
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装1上相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        #endregion
        #region"组装工位2相机"
        //下相机
        public  void ProcessImageC2Down(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -AssembleSuction2.dDownAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.自动标定:
                       
                            if (FrmDebug.iSelectParamSetIndex == 3)
                            {
                                if (CommonSet.dic_Assemble2[10].hImageDown != null)
                                    CommonSet.dic_Assemble2[10].hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[10].hImageDown);
                                HOperatorSet.DispImage(CommonSet.dic_Assemble2[10].hImageDown, FrmMain.frmAssem2.GetDownWindow());
                                HOperatorSet.DispCross(FrmMain.frmAssem2.GetDownWindow(), height.I / 2, width.I / 2, 3000, 0);


                                HWindow hw = FrmMain.frmAssem2.GetDownWindow();
                                CommonSet.dic_Assemble2[10].pfDown.SetRun(true);

                                CommonSet.dic_Assemble2[10].pfDown.hwin = hw;

                                CommonSet.dic_Assemble2[10].actionDown(CommonSet.dic_Assemble2[10].hImageDown);
                                CommonSet.dic_Assemble2[10].pfDown.showObj();
                                string strDispRow = "Row:" + CommonSet.dic_Assemble2[10].imgResultDown.CenterRow.ToString("0.000");
                                string strDispCol = "Col:" + CommonSet.dic_Assemble2[10].imgResultDown.CenterColumn.ToString("0.000");
                                // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                                CommonSet.disp_message(hw, strDispRow, "window", 0, 0, "cyan", "false");
                                CommonSet.disp_message(hw, strDispCol, "window", 20, 0, "cyan", "false");
                                HOperatorSet.DispCross(hw, CommonSet.dic_Assemble2[10].imgResultDown.CenterRow, CommonSet.dic_Assemble2[10].imgResultDown.CenterColumn, 100, 0);
                                HOperatorSet.DispCross(hw, height.I / 2, width.I / 2, 3000, 0);

                                return;
                            }
                            break;
                    case RunMode.手动:
                       
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 3)
                        {

                            try
                            {
                                if(CommonSet.dic_Assemble2[10].hImageDown!=null)
                                    CommonSet.dic_Assemble2[10].hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[10].hImageDown);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmAssem2.GetDownWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(CommonSet.dic_Assemble2[10].hImageDown, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                
                            }
                            catch (Exception ex) {
                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

                        }



                        break;
                    case RunMode.暂停:
                    case RunMode.运行:
                        try
                        {
                            for (; Assem2Module.iPicNum < 10; Assem2Module.iPicNum++)
                            {
                                int index = Assem2Module.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                AssembleSuction2 ps = CommonSet.dic_Assemble2[index+9];
                                if (ps.BUse)
                                {
                                    if (CommonSet.dic_Assemble2[index + 9].hImageDown != null)
                                         CommonSet.dic_Assemble2[index + 9].hImageDown.Dispose();
                                    //swProcessListener.Restart();
                                    HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[index+9].hImageDown);

                                    // CommonSet.dic_Suction[1].hImage = image;
                                    HOperatorSet.SetPart(FrmShowImage.lstC2Win[index - 1], 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(CommonSet.dic_Assemble2[index + 9].hImageDown, FrmShowImage.lstC2Win[index - 1]);
                                    Assem2Module.iPicNum++;
                                    Assem2Module.iPicSum--;
                                    ImageArgs args = new ImageArgs();
                                    args.Index = index + 9;
                                    args.Image = CommonSet.dic_Assemble2[index + 9].hImageDown;
                                    args.BTest = false;
                                    muC2.WaitOne();
                                    lstImgC2.Enqueue(args);
                                    muC2.ReleaseMutex();
                                    CommonSet.WriteInfo("组装2队列数目" + lstImgC2.Count);
                                   // this.onCheckC2(this, args);
                                    //Task t = Task.Factory.StartNew(() => ActionAsynC2Down(index+9, CommonSet.dic_Assemble2[index+9].hImageDown, false));
                                    //Action<int, HObject, bool> dele = ActionAsynC2Down;
                                    //dele.BeginInvoke(index + 9, CommonSet.dic_Assemble2[index + 9].hImageDown, false, ActionCallBack, null);
                                    break;
                                }
                                else
                                {
                                    HOperatorSet.SetPart(FrmShowImage.lstC2Win[index - 1], 0, 0, height, width);
                                    HOperatorSet.ClearWindow(FrmShowImage.lstC2Win[index - 1]);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装2下相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.飞拍测试:
                        try
                        {

                            for (; TestFlash.iPicNum < 10; )
                            {
                                int index = TestFlash.iPicNum;
                                CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                                AssembleSuction2 ps = CommonSet.dic_Assemble2[index+9];
                                if (CommonSet.dic_Assemble2[index + 9].hImageDown != null)
                                     CommonSet.dic_Assemble2[index + 9].hImageDown.Dispose();
                                //swProcessListener.Restart();
                                HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[index + 9].hImageDown);

                                // CommonSet.dic_Suction[1].hImage = image;
                                HOperatorSet.SetPart(CommonSet.lstFlashWin[index - 1], 0, 0, height.I, width.I);
                                HOperatorSet.DispImage(CommonSet.dic_Assemble2[index + 9].hImageDown, CommonSet.lstFlashWin[index - 1]);
                                TestFlash.iPicNum++;
                                TestFlash.iPicSum--;
                                ImageArgs args = new ImageArgs();
                                args.Index = index + 9;
                                args.Image = CommonSet.dic_Assemble2[index + 9].hImageDown;
                                args.BTest = true;
                                muC2.WaitOne();
                                lstImgC2.Enqueue(args);
                                muC2.ReleaseMutex();
                                CommonSet.WriteInfo("组装2队列数目" + lstImgC2.Count);
                              //  Task t = Task.Factory.StartNew(() => ActionAsynC2Down(index+9, CommonSet.dic_Assemble2[index+9].hImageDown, true));
                                //Action<int, HObject, bool> dele = ActionAsynC2Down;
                                //dele.BeginInvoke(index + 9, CommonSet.dic_Assemble2[index + 9].hImageDown, true, ActionCallBack, null);
                                if (!TestFlash.bFlash)
                                {
                                    break;
                                }
                                
                                break;

                            }

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装2下相机飞拍图像处理异常", ex);
                        }
                        break;
                    case RunMode.组装验证:
                        try
                        {
                            int index = ResultTestModule.iCurrentSuctionOrder;
                            CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                         

                            //swProcessListener.Restart();
                            if (CommonSet.dic_Assemble2[index].hImageDown != null)
                                CommonSet.dic_Assemble2[index].hImageDown.Dispose();
                            HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[index].hImageDown);

                            HWindow hwin = FrmMain.frmAssem2.GetDownWindow();
                            // CommonSet.dic_Suction[1].hImage = image;
                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(CommonSet.dic_Assemble2[index].hImageDown, hwin);

                            //Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, true));

                            CommonSet.dic_Assemble2[index].pfDown.SetRun(true);

                            CommonSet.dic_Assemble2[index].pfDown.hwin = hwin;

                            CommonSet.dic_Assemble2[index].actionDown(CommonSet.dic_Assemble2[index].hImageDown);
                            CommonSet.dic_Assemble2[index].pfDown.showObj();
                            string strDispRow = "Row:" + CommonSet.dic_Assemble2[index].imgResultDown.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn.ToString("0.000");
                            double dDiffDownX = (1024 - CommonSet.dic_Assemble2[index].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                            double dDiffDownY = (1224 - CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                            // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, CommonSet.dic_Assemble2[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, "中心差X:" + dDiffDownX.ToString("0.000") + "mm", "window", 60, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, "中心差Y:" + dDiffDownY.ToString("0.000") + "mm", "window", 80, 0, strShowColor, "false");
                            HOperatorSet.DispCross(hwin, CommonSet.dic_Assemble2[index].imgResultDown.CenterRow, CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn, 100, 0);

                      
                            double dDiffDownX1 = (CommonSet.dic_Assemble2[index].pSuctionCenter.X - CommonSet.dic_Assemble2[index].imgResultDown.CenterRow) * AssembleSuction2.GetDownResulotion();
                            double dDiffDownY1 = (CommonSet.dic_Assemble2[index].pSuctionCenter.Y- CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn) * AssembleSuction2.GetDownResulotion();
                            CommonSet.disp_message(hwin, "和吸笔中心差X:" + dDiffDownX1.ToString("0.000") + "mm", "window", 180, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, "和吸笔中心差Y:" + dDiffDownY1.ToString("0.000") + "mm", "window", 200, 0, strShowColor, "false");
                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装2下相机图像处理异常", ex);
                        }
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        //上相机
        public  void ProcessImageC2Up(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                HOperatorSet.RotateImage(image, out image, -AssembleSuction2.dUpAng, "constant");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.自动标定:
                        if (FrmMain.iSelectParamSetIndex == 3)
                        {
                            if (AssembleSuction2.hImageUP != null)
                                AssembleSuction2.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);
                            HOperatorSet.DispImage(AssembleSuction2.hImageUP, FrmMain.frmAssem2.GetUpWindow());
                            HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);


                            AssembleSuction2.pfUp.SetRun(true);
                            AssembleSuction2.pfUp.hwin = FrmMain.frmAssem2.GetUpWindow();
                            AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                            AssembleSuction2.pfUp.showObj();
                            string strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                            string strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(FrmMain.frmAssem2.GetUpWindow(), strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(FrmMain.frmAssem2.GetUpWindow(), strDispCol, "window", 20, 0, strShowColor, "false");
                            HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);
                            HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);


                            break;
                        }
                        break;
                    case RunMode.手动:
                        #region"无效"
                        if (CommonSet.bCalibFlag)
                        {
                            if (FrmDebug.iSelectParamSetIndex == 0)
                            {
                                if (AssembleSuction2.hImageUP != null)
                                    AssembleSuction2.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);
                                //// HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = CommonSet.hPixelToAxisCalibWin;
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                break;
                            }else if (FrmDebug.iSelectParamSetIndex == 1)
                            {
                                if (AssembleSuction2.hImageUP != null)
                                    AssembleSuction2.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);
                                HOperatorSet.DispImage(AssembleSuction2.hImageUP, FrmMain.frmAssem2.GetUpWindow());
                                HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);

                               
                                AssembleSuction2.pfUp.SetRun(true);
                                AssembleSuction2.pfUp.hwin = FrmMain.frmAssem2.GetUpWindow();
                                AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                                AssembleSuction2.pfUp.showObj();
                                string strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                                string strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(FrmMain.frmAssem2.GetUpWindow(), strDispRow, "window", 0, 0, strShowColor, "false");
                                CommonSet.disp_message(FrmMain.frmAssem2.GetUpWindow(), strDispCol, "window", 20, 0, strShowColor, "false");
                                HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), height.I / 2, width.I / 2, 3000, 0);
                                HOperatorSet.DispCross(FrmMain.frmAssem2.GetUpWindow(), AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);
                                
                                
                                break;
                            }
                        }
                    #endregion
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 3)
                        {

                            try
                            {
                                if(AssembleSuction2.hImageUP!=null)
                                     AssembleSuction2.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);
                                //// HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.frmAssem2.GetUpWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {
                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

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
                            if (AssembleSuction2.hImageUP != null)
                                 AssembleSuction2.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);

                            hwin = CommonSet.lstOtherWin[3];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                            AssembleSuction2.pfUp.SetRun(true);
                            AssembleSuction2.pfUp.hwin = hwin;
                            AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                            AssembleSuction2.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            ShowResult(hwin, AssembleSuction2.imgResultUp.bImageResult, 60);
                            HOperatorSet.DispCross(hwin, AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装2上相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.飞拍测试:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";
                            if (AssembleSuction2.hImageUP != null)
                                AssembleSuction2.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);

                            hwin = CommonSet.lstFlashWin[10];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                            AssembleSuction2.pfUp.SetRun(true);
                            AssembleSuction2.pfUp.hwin = hwin;
                            AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                            AssembleSuction2.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                            ShowResult(hwin, AssembleSuction2.imgResultUp.bImageResult, 60);
                            HOperatorSet.DispCross(hwin, AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装2上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if (AssembleSuction2.hImageUP != null)
                                 AssembleSuction2.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);

                            hwin = CommonSet.hPixelToAxisCalibWin;

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                            AssembleSuction2.pfUp.SetRun(true);
                            AssembleSuction2.pfUp.hwin = hwin;
                            AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                            AssembleSuction2.pfUp.showObj();
                            strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.组装验证:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";
                            if (AssembleSuction2.hImageUP != null)
                                AssembleSuction2.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out AssembleSuction2.hImageUP);

                            hwin = FrmMain.frmAssem2.GetUpWindow();

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                            if (ResultTestModule.iPos == 1)
                            {
                                AssembleSuction2.pfUp.SetRun(true);
                                AssembleSuction2.pfUp.hwin = hwin;
                                AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                                AssembleSuction2.pfUp.showObj();
                                strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                                strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                                CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, strShowColor, "false");
                                CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, strShowColor, "false");
                                HOperatorSet.DispCross(hwin, AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);
                                try
                                {

                                    double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                    double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();
                                  


                                    strDispRow = "中心差DX:" + dDiffUpX.ToString("0.000") + "mm";
                                    strDispCol = "中心差DY:" + dDiffUpY.ToString("0.000") + "mm";
                                    CommonSet.disp_message(hwin, strDispRow, "window", 40, 0, strShowNgColor, "false");
                                    CommonSet.disp_message(hwin, strDispCol, "window", 60, 0, strShowNgColor, "false");
                                    // p.Z = DGetPosZ;
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                AssembleSuction2.pfUp.SetRun(true);
                                AssembleSuction2.pfUp.hwin = hwin;
                                AssembleSuction2.actionUp(AssembleSuction2.hImageUP);
                                AssembleSuction2.pfUp.showObj();
                                //strDispRow = "Row:" + AssembleSuction2.imgResultUp.CenterRow.ToString("0.000");
                                //strDispCol = "Col:" + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000");
                                //CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                                //CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");
                                HOperatorSet.DispCross(hwin, AssembleSuction2.imgResultUp.CenterRow, AssembleSuction2.imgResultUp.CenterColumn, 100, 0);
                                HOperatorSet.DispCross(hwin, ResultTestModule.dRow, ResultTestModule.dCol, 200, 0);
                                try
                                {

                                    double dDiffUpX = (AssembleSuction2.imgResultUp.CenterRow - 1024) * AssembleSuction2.GetUpResulotion();
                                    double dDiffUpY = (AssembleSuction2.imgResultUp.CenterColumn - 1224) * AssembleSuction2.GetUpResulotion();

                                    strDispRow = "中心差DX:" + dDiffUpX.ToString("0.000") + "mm";
                                    strDispCol = "中心差DY:" + dDiffUpY.ToString("0.000") + "mm";
                                    CommonSet.disp_message(hwin, strDispRow, "window", 40, 0, strShowNgColor, "false");
                                    CommonSet.disp_message(hwin, strDispCol, "window", 60, 0, strShowNgColor, "false");
                                    // p.Z = DGetPosZ;
                                }
                                catch (Exception ex)
                                {

                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("组装1下相机图像处理异常", ex);
                        }
                        break;
                    default:
                        break;
                }

            }
            image.Dispose();
        }
        #endregion

        #region"镜筒和点胶工位相机"
        //下相机
        public  void ProcessImageDDown(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                HOperatorSet.MirrorImage(image, out image, "column");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.手动:
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {

                            try
                            {
                                if (BarrelSuction.hImageDown != null)
                                    BarrelSuction.hImageDown.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out BarrelSuction.hImageDown);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.barrelUI.GetDownWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(BarrelSuction.hImageDown, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {
                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

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
                            if (BarrelSuction.hImageDown != null)
                                  BarrelSuction.hImageDown.Dispose();
                            HOperatorSet.CopyImage(image, out BarrelSuction.hImageDown);

                            hwin = CommonSet.lstOtherWin[4];

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(BarrelSuction.hImageDown, hwin);
                            BarrelSuction.pfDown.SetRun(true);
                            BarrelSuction.pfDown.hwin = hwin;
                            BarrelSuction.actionDown(BarrelSuction.hImageDown);
                            BarrelSuction.pfDown.showObj();
                            strDispRow = "Row:" + BarrelSuction.imgResultDown.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + BarrelSuction.imgResultDown.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");
                            CommonSet.disp_message(hwin, "Angle:" + BarrelSuction.imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, "cyan", "false");
                            HOperatorSet.DispCross(hwin, BarrelSuction.imgResultDown.CenterRow, BarrelSuction.imgResultDown.CenterColumn, 100, 0);


                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("点胶工位下相机图像处理异常", ex);
                        }
                        break;



                    default:
                        break;
                }

            }
            image.Dispose();
        }
        //上相机
        public  void ProcessImageDUp(HObject image)
        {
            if (image != null)
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                //HOperatorSet.MirrorImage(image, out image, "row");
                //HOperatorSet.MirrorImage(image, out image, "column");
                //如果相机在手动状态下
                switch (Run.runMode)
                {
                    case RunMode.手动:
                        if (CommonSet.bCalibFlag)
                        {
                            
                            try
                            {
                                if (FrmDebug.iSelectParamSetIndex == 0)
                                {

                                    if (BarrelSuction.hImageUP != null)
                                        BarrelSuction.hImageUP.Dispose();
                                    //手动情况下，将图像存入第一个吸笔的图像变量中
                                    HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);
                                    // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                    HWindow hwin = CommonSet.hPixelToAxisCalibWin;
                                    //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                    ////HOperatorSet.getpart
                                    HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                                    HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                }
                                if (FrmDebug.iSelectParamSetIndex == 5)
                                {
                                    if (BarrelSuction.hImageUP != null)
                                        BarrelSuction.hImageUP.Dispose();
                                    //手动情况下，将图像存入第一个吸笔的图像变量中
                                    HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);
                                    HWindow hwin = FrmMain.frmDebug.frmRotate.hwin;
                                    HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                    HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                                    HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                    
                                    break;
                                      
                                    
                                }
                            }
                            catch (Exception ex)
                            {

                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }
                            break;
                        }
                      
                        //通过获取吸笔对应的索引显示到相应的窗体
                        if (FrmMain.iSelectParamSetIndex == 2)
                        {

                            try
                            {
                                if (BarrelSuction.hImageUP != null)
                                 BarrelSuction.hImageUP.Dispose();
                                //手动情况下，将图像存入第一个吸笔的图像变量中
                                HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);
                                // HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[1].hImageAngle);
                                HWindow hwin = FrmMain.barrelUI.GetUpWindow();
                                //// HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                ////HOperatorSet.getpart
                                HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                                HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                            }
                            catch (Exception ex) {

                                CommonSet.WriteInfo("显示图像异常:" + ex.ToString());
                            }

                        }
                        else if (FrmMain.iSelectParamSetIndex == 1)
                        {
                            try
                            {
                                //if (bs.iSuctionNum == 2)
                                //{

                                //    HOperatorSet.CopyImage(image, out CommonSet.dic_BarrelSuction[2].hImageCenter);
                                //    HWindow hwin = bs.getHwindow();
                                //    HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                                //    //HOperatorSet.getpart
                                //    HOperatorSet.DispImage(image, hwin);
                                //    HOperatorSet.DispCross(hwin, height.I / 2, width.I / 2, 3000, 0);
                                //}
                            }
                            catch (Exception ex) { }
                        }


                        break;
                    case RunMode.暂停:
                    case RunMode.点胶测试:
                    case RunMode.运行:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";
                            string strAngle = "";
                            if (BarrelSuction.hImageUP != null)
                                 BarrelSuction.hImageUP.Dispose();
                           // int iSuction = GetProduct1Module.iCurrentSuction;
                            HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);
                            if (Run.runMode == RunMode.点胶测试)
                            {
                                 hwin = FrmMain.barrelUI.GetUpWindow();
                            }
                            else
                            {
                                hwin = CommonSet.lstOtherWin[5];
                            }

                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                            BarrelSuction.pfUp.SetRun(true);
                            BarrelSuction.pfUp.hwin = hwin;
                            BarrelSuction.actionUp(BarrelSuction.hImageUP);
                            BarrelSuction.pfUp.showObj();
                            strDispRow = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");
                            HOperatorSet.DispCross(hwin, BarrelSuction.pCalibLenPos.X, BarrelSuction.pCalibLenPos.Y, 200, 0);
                            HOperatorSet.DispCross(hwin, BarrelSuction.dRoateCenterRow, BarrelSuction.dRoateCenterColumn, 300, 0);



                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("点胶上相机图像处理异常", ex);
                        }
                        break;
                    case RunMode.像素标定:
                        try
                        {
                            HWindow hwin;
                            string strDispRow = "";
                            string strDispCol = "";

                            if (BarrelSuction.hImageUP != null)
                                    BarrelSuction.hImageUP.Dispose();
                            HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);

                           // hwin = CommonSet.hPixelToAxisCalibWin;
                            hwin = FrmMain.barrelUI.GetUpWindow();
                            HOperatorSet.SetPart(hwin, 0, 0, height.I, width.I);
                            HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin);
                            BarrelSuction.pfUp.SetRun(true);
                            BarrelSuction.pfUp.hwin = hwin;
                            BarrelSuction.actionUp(BarrelSuction.hImageUP);
                            BarrelSuction.pfUp.showObj();
                            strDispRow = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
                            strDispCol = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");
                            CommonSet.disp_message(hwin, strDispRow, "window", 0, 0, "cyan", "false");
                            CommonSet.disp_message(hwin, strDispCol, "window", 20, 0, "cyan", "false");

                        }
                        catch (Exception ex)
                        {
                            CommonSet.WriteDebug("上相机图像处理异常", ex);
                        }
                        break;

                    case RunMode.旋转:
                             if (BarrelSuction.hImageUP != null)
                                BarrelSuction.hImageUP.Dispose();
                            //手动情况下，将图像存入第一个吸笔的图像变量中
                            HOperatorSet.CopyImage(image, out BarrelSuction.hImageUP);
                            HWindow hwin1 = FrmMain.barrelUI.frmRotate.hwin;
                            HOperatorSet.SetPart(hwin1, 0, 0, height.I, width.I);
                            BarrelSuction.pfUp.SetRun(true);
                            BarrelSuction.pfUp.hwin = hwin1;
                            BarrelSuction.actionUp(BarrelSuction.hImageUP);
                            BarrelSuction.pfUp.showObj();
                            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
                            string strDispRow1 = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
                            string strDispCol1 = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");

                            CommonSet.disp_message(hwin1, strDispRow1, "window", 0, 0, "black", "true");
                            CommonSet.disp_message(hwin1, strDispCol1, "window", 20, 0, "black", "true");

                            HOperatorSet.DispImage(BarrelSuction.hImageUP, hwin1);
                            HOperatorSet.DispCross(hwin1, height.I / 2, width.I / 2, 3000, 0);
                                  
                        break;

                    default:
                        break;
                }

            }
            image.Dispose();
        }
        #endregion
        //  Stopwatch swProcessListener = new Stopwatch();
        //下相机
    
        private  void ActionAsynA1Down(int index, HObject image, bool bTest)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                CommonSet.WriteInfo("取料1处理图像" + index.ToString() + "开始");
              
                sw.Start();
                CommonSet.dic_OptSuction1[index].pfDown.SetRun(true);
                if (bTest)
                {
                    CommonSet.dic_OptSuction1[index].pfDown.hwin = CommonSet.lstFlashWin[index - 1];
                }
                else
                {
                    CommonSet.dic_OptSuction1[index].pfDown.hwin = FrmShowImage.lstA1Win[index - 1];
                }
                CommonSet.dic_OptSuction1[index].actionDown(CommonSet.dic_OptSuction1[index].hImageDown);
                CommonSet.dic_OptSuction1[index].pfDown.showObj();
                if (bTest)
                {
                  
                    //double dx = (CommonSet.dic_OptSuction1[index].dFlashCalibColumn - CommonSet.dic_OptSuction1[index].dCenterDownColumn) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);
                    //double dy = (CommonSet.dic_OptSuction1[index].dFlashCalibRow - CommonSet.dic_OptSuction1[index].dCenterDownRow) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);

                  
                    if (TestFlash.bFlash)
                    {
                        TestFlash.lstPoint1[index - 1].X = CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint1[index - 1].Y = CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow;
                    }
                    else
                    {
                        TestFlash.lstPoint[index - 1].X = CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint[index - 1].Y = CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow;
                    }
                    string strDispRow1 = "定点R:" + TestFlash.lstPoint[index-1].Y.ToString("0.000");
                    string strDispCol1 = "定点C:" + TestFlash.lstPoint[index - 1].X.ToString("0.000");

                    string strDispRow2 = "飞拍R:" + TestFlash.lstPoint1[index - 1].Y.ToString("0.000");
                    string strDispCol2 = "飞拍C:" + TestFlash.lstPoint1[index - 1].X.ToString("0.000");

                    double diffR = TestFlash.lstPoint1[index - 1].Y - TestFlash.lstPoint[index - 1].Y;
                    double diffC = TestFlash.lstPoint1[index - 1].X - TestFlash.lstPoint[index - 1].X;

                    string strDispRow3 = "差R:" + diffR.ToString("0.000");
                    string strDispCol3 = "差C:" + diffC.ToString("0.000");

                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow1, "window", 0, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol1, "window", 20, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow2, "window", 40, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol2, "window", 60, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow3, "window", 80, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol3, "window", 100, 0, "black", "true");
                    CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");

                    return;
                }

                double dx = 0, dy = 0;
                //GetCenterOffset1(index, ref dx, ref dy);
                dx = OptSution1.GetDownResulotion() * (CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow - CommonSet.dic_OptSuction1[index].pSuctionCenter.X);
                dy = OptSution1.GetDownResulotion() * (CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn - CommonSet.dic_OptSuction1[index].pSuctionCenter.Y);

                //int iCurrentSolution = CommonSet.asm.getSolution(GetProduct1Module.iCurrentBarrel);
                //double dAngle = CommonSet.asm.dic_Solution[iCurrentSolution].lstAngle[index - 1];
                //double dRotateAngle = CommonSet.dic_OptSuction1[index].imgResultDown.dAngle - dAngle;
             
                string strDispRow = "Row:" + CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow.ToString("0.000");
                string strDispCol = "Col:" + CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn.ToString("0.000");
               // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                HWindow hShowWin = FrmShowImage.lstA1Win[index - 1];
                CommonSet.disp_message(hShowWin, strDispRow, "window", 0, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, strDispCol, "window", 20, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, CommonSet.dic_OptSuction1[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                ShowResult(hShowWin, CommonSet.dic_OptSuction1[index].imgResultDown.bImageResult, 60);
                string colorX = strShowNgColor, colorY = strShowNgColor;
                if (Math.Abs(dx) < 0.2)
                {
                    colorX = strShowColor;
                }
                if (Math.Abs(dy) < 0.2)
                {
                    colorY = strShowColor;
                }
                CommonSet.disp_message(hShowWin, "dx:" + dx.ToString("0.000") + "mm", "window", 110, 0, colorX, "false");
                CommonSet.disp_message(hShowWin, "dy:" + dy.ToString("0.000") + "mm", "window", 130, 0, colorY, "false");
               // CommonSet.disp_message(CommonSet.lstA1Win[index - 1], strRadius, "window", 60, 0, "cyan", "false");
                HOperatorSet.DispCross(hShowWin, CommonSet.dic_OptSuction1[index].imgResultDown.CenterRow, CommonSet.dic_OptSuction1[index].imgResultDown.CenterColumn, 100, 0);
                sw.Stop();
                CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");
                CommonSet.WriteInfo("图像上料1处理时间" + sw.ElapsedMilliseconds.ToString() + "ms");
                if (CommonSet.bSavePic)
                {
                    SaveImg(image, CommonSet.strSaveImg + "S" + index.ToString(), CommonSet.iCurrentPicNum.ToString());
                    if (index >= 9)
                    {
                        CommonSet.iCurrentPicNum++;
                    }

                    if (CommonSet.iCurrentPicNum > CommonSet.iSaveNum)
                    {
                        CommonSet.bSavePic = false;
                    }
                }
                if (CommonSet.bSaveNG)
                {
                    if (!CommonSet.dic_OptSuction1[index].imgResultDown.bImageResult)
                    {
                        SaveImg(image, CommonSet.strSaveImg + "NG", "S" + index.ToString() + DateTime.Now.ToString("_yy_MM_dd_HH_mm_ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("取料1处理图像" + index.ToString() + "异常");
                sw.Stop();

                CommonSet.WriteInfo("取料1处理图像" + index.ToString() + "时间" + sw.ElapsedMilliseconds.ToString() + "ms");
            }
        }
        private  void ActionAsynA2Down(int index, HObject image, bool bTest)
        {
            try
            {
                CommonSet.WriteInfo("取料2处理图像" + index.ToString() + "开始");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                CommonSet.dic_OptSuction2[index].pfDown.SetRun(true);
                if (bTest)
                {
                    CommonSet.dic_OptSuction2[index].pfDown.hwin = CommonSet.lstFlashWin[index - 10];
                }
                else
                {
                    CommonSet.dic_OptSuction2[index].pfDown.hwin = FrmShowImage.lstA2Win[index - 10];
                }
                CommonSet.dic_OptSuction2[index].actionDown(CommonSet.dic_OptSuction2[index].hImageDown);
                CommonSet.dic_OptSuction2[index].pfDown.showObj();
                if (bTest)
                {

                    //double dx = (CommonSet.dic_OptSuction1[index].dFlashCalibColumn - CommonSet.dic_OptSuction1[index].dCenterDownColumn) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);
                    //double dy = (CommonSet.dic_OptSuction1[index].dFlashCalibRow - CommonSet.dic_OptSuction1[index].dCenterDownRow) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);


                    if (TestFlash.bFlash)
                    {
                        TestFlash.lstPoint1[index - 10].X = CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint1[index - 10].Y = CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow;
                    }
                    else
                    {
                        TestFlash.lstPoint[index - 10].X = CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint[index - 10].Y = CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow;
                    }
                    string strDispRow1 = "定点R:" + TestFlash.lstPoint[index - 10].Y.ToString("0.000");
                    string strDispCol1 = "定点C:" + TestFlash.lstPoint[index - 10].X.ToString("0.000");

                    string strDispRow2 = "飞拍R:" + TestFlash.lstPoint1[index - 10].Y.ToString("0.000");
                    string strDispCol2 = "飞拍C:" + TestFlash.lstPoint1[index - 10].X.ToString("0.000");

                    double diffR = TestFlash.lstPoint1[index - 10].Y - TestFlash.lstPoint[index - 10].Y;
                    double diffC = TestFlash.lstPoint1[index - 10].X - TestFlash.lstPoint[index - 10].X;

                    string strDispRow3 = "差R:" + diffR.ToString("0.000");
                    string strDispCol3 = "差C:" + diffC.ToString("0.000");

                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow1, "window", 0, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol1, "window", 20, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow2, "window", 40, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol2, "window", 60, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow3, "window", 80, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol3, "window", 100, 0, "black", "true");
                    CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");

                    return;
                }
                //旋转


                double dx = 0, dy = 0;
                //GetCenterOffset1(index, ref dx, ref dy);
                dx = OptSution2.GetDownResulotion() * (CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow - CommonSet.dic_OptSuction2[index].pSuctionCenter.X);
                dy = OptSution2.GetDownResulotion() * (CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn - CommonSet.dic_OptSuction2[index].pSuctionCenter.Y);

               
                
                string strDispRow = "Row:" + CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow.ToString("0.000");
                string strDispCol = "Col:" + CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn.ToString("0.000");
                // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                HWindow hShowWin = FrmShowImage.lstA2Win[index - 10];
                CommonSet.disp_message(hShowWin, strDispRow, "window", 0, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, strDispCol, "window", 20, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, CommonSet.dic_OptSuction2[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                ShowResult(hShowWin, CommonSet.dic_OptSuction2[index].imgResultDown.bImageResult, 60);
                string colorX = strShowNgColor, colorY = strShowNgColor;
                if (Math.Abs(dx) < 0.2)
                {
                    colorX = strShowColor;
                }
                if (Math.Abs(dy) < 0.2)
                {
                    colorY = strShowColor;
                }
                CommonSet.disp_message(hShowWin, "dx:" + dx.ToString("0.000") + "mm", "window", 110, 0, colorX, "false");
                CommonSet.disp_message(hShowWin, "dy:" + dy.ToString("0.000") + "mm", "window", 130, 0, colorY, "false");
                // CommonSet.disp_message(CommonSet.lstA1Win[index - 1], strRadius, "window", 60, 0, "cyan", "false");
                HOperatorSet.DispCross(hShowWin, CommonSet.dic_OptSuction2[index].imgResultDown.CenterRow, CommonSet.dic_OptSuction2[index].imgResultDown.CenterColumn, 100, 0);
                sw.Stop();
                CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");
                CommonSet.WriteInfo("图像上料2处理时间" + sw.ElapsedMilliseconds.ToString() + "ms");
                if (CommonSet.bSavePic)
                {
                    SaveImg(image, CommonSet.strSaveImg + "S" + index.ToString(), CommonSet.iCurrentPicNum.ToString());
                    if (index >=18)
                    {
                        CommonSet.iCurrentPicNum++;
                    }

                    if (CommonSet.iCurrentPicNum > CommonSet.iSaveNum)
                    {
                        CommonSet.bSavePic = false;
                    }
                }
                if (CommonSet.bSaveNG)
                {
                    if (!CommonSet.dic_OptSuction2[index].imgResultDown.bImageResult)
                    {
                        SaveImg(image, CommonSet.strSaveImg + "NG", "S" + index.ToString() + DateTime.Now.ToString("_yy_MM_dd_HH_mm_ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("取料2处理图像" + index.ToString() + "异常" + ex.ToString());

            }

               
        }
        private  void ActionAsynC1Down(int index, HObject image, bool bTest)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                CommonSet.WriteInfo("组装1处理图像" + index.ToString() + "开始");
               
                sw.Start();
                CommonSet.dic_Assemble1[index].pfDown.SetRun(true);
                if (bTest)
                {
                    CommonSet.dic_Assemble1[index].pfDown.hwin = CommonSet.lstFlashWin[index - 1];
                }
                else
                {
                    CommonSet.dic_Assemble1[index].pfDown.hwin = FrmShowImage.lstC1Win[index - 1];
                }
                CommonSet.dic_Assemble1[index].actionDown(CommonSet.dic_Assemble1[index].hImageDown);
              
                CommonSet.dic_Assemble1[index].pfDown.showObj();

                CommonSet.WriteInfo("图像组装1显示" + index.ToString() + "时间" + CommonSet.dic_Assemble1[index].pfDown.dShow1.ToString() + "ms  "
                    + CommonSet.dic_Assemble1[index].pfDown.dShow2.ToString() + "ms  " + CommonSet.dic_Assemble1[index].pfDown.dShow3.ToString() + "ms  ");
                if (bTest)
                {

                    //double dx = (CommonSet.dic_OptSuction1[index].dFlashCalibColumn - CommonSet.dic_OptSuction1[index].dCenterDownColumn) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);
                    //double dy = (CommonSet.dic_OptSuction1[index].dFlashCalibRow - CommonSet.dic_OptSuction1[index].dCenterDownRow) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);


                    if (TestFlash.bFlash)
                    {
                        TestFlash.lstPoint1[index - 1].X = CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint1[index - 1].Y = CommonSet.dic_Assemble1[index].imgResultDown.CenterRow;
                    }
                    else
                    {
                        TestFlash.lstPoint[index - 1].X = CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint[index - 1].Y = CommonSet.dic_Assemble1[index].imgResultDown.CenterRow;
                    }
                    string strDispRow1 = "定点R:" + TestFlash.lstPoint[index - 1].Y.ToString("0.000");
                    string strDispCol1 = "定点C:" + TestFlash.lstPoint[index - 1].X.ToString("0.000");

                    string strDispRow2 = "飞拍R:" + TestFlash.lstPoint1[index - 1].Y.ToString("0.000");
                    string strDispCol2 = "飞拍C:" + TestFlash.lstPoint1[index - 1].X.ToString("0.000");

                    double diffR = TestFlash.lstPoint1[index - 1].Y - TestFlash.lstPoint[index - 1].Y;
                    double diffC = TestFlash.lstPoint1[index - 1].X - TestFlash.lstPoint[index - 1].X;

                    string strDispRow3 = "差R:" + diffR.ToString("0.000");
                    string strDispCol3 = "差C:" + diffC.ToString("0.000");

                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow1, "window", 0, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol1, "window", 20, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow2, "window", 40, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol2, "window", 60, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispRow3, "window", 80, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 1], strDispCol3, "window", 100, 0, "black", "true");
                    CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");

                    return;
                }
                double dx = 0,dy = 0;
                //GetCenterOffset1(index, ref dx, ref dy);
                dx = AssembleSuction1.GetDownResulotion() * (CommonSet.dic_Assemble1[index].imgResultDown.CenterRow - CommonSet.dic_Assemble1[index].pSuctionCenter.X);
                dy = AssembleSuction1.GetDownResulotion() * (CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn - CommonSet.dic_Assemble1[index].pSuctionCenter.Y);

                string strDispRow = "Row:" + CommonSet.dic_Assemble1[index].imgResultDown.CenterRow.ToString("0.000");
                string strDispCol = "Col:" + CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn.ToString("0.000");
                // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                HWindow hShowWin = FrmShowImage.lstC1Win[index - 1];
                CommonSet.disp_message(hShowWin, strDispRow, "window", 0, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, strDispCol, "window", 20, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, CommonSet.dic_Assemble1[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                ShowResult(hShowWin, CommonSet.dic_Assemble1[index].imgResultDown.bImageResult, 60);
                string colorX = strShowNgColor, colorY = strShowNgColor;
                if (Math.Abs(dx) < 0.05)
                {
                    colorX = strShowColor;
                }
                if (Math.Abs(dy) < 0.05)
                {
                    colorY = strShowColor;
                }
                CommonSet.disp_message(hShowWin, "dx:" + dx.ToString("0.000") + "mm", "window", 80, 0, colorX, "false");
                CommonSet.disp_message(hShowWin, "dy:" + dy.ToString("0.000") + "mm", "window", 100, 0, colorY, "false");
                CommonSet.disp_message(hShowWin, "dR:" + (CommonSet.dic_Assemble1[index].imgResultDown.CenterRow - CommonSet.dic_Assemble1[index].pSuctionCenter.X).ToString("0.000"), "window", 120, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, "dC:" + (CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn - CommonSet.dic_Assemble1[index].pSuctionCenter.Y).ToString("0.000"), "window", 140, 0, strShowColor, "false");
                // CommonSet.disp_message(CommonSet.lstA1Win[index - 1], strRadius, "window", 60, 0, "cyan", "false");
                HOperatorSet.DispCross(hShowWin, CommonSet.dic_Assemble1[index].imgResultDown.CenterRow, CommonSet.dic_Assemble1[index].imgResultDown.CenterColumn, 100, 0);
                sw.Stop();
                CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");
                CommonSet.WriteInfo("图像组装1处理" + index.ToString() + "时间" + sw.ElapsedMilliseconds.ToString() + "ms");
                if (CommonSet.bSavePic)
                {
                    SaveImg(image, CommonSet.strSaveImg + "S" + index.ToString(), CommonSet.iCurrentPicNum.ToString());
                    if (index >= 9)
                    {
                        CommonSet.iCurrentPicNum++;
                    }

                    if (CommonSet.iCurrentPicNum > CommonSet.iSaveNum)
                    {
                        CommonSet.bSavePic = false;
                    }
                }
                if (CommonSet.bSaveNG)
                {
                    if (!CommonSet.dic_Assemble1[index].imgResultDown.bImageResult)
                    {
                        SaveImg(image, CommonSet.strSaveImg + "NG", "S" + index.ToString() + DateTime.Now.ToString("_yy_MM_dd_HH_mm_ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("组装1处理图像" + index.ToString() + "异常");
                sw.Stop();

                CommonSet.WriteInfo("组装1处理图像" + index.ToString() + "时间" + sw.ElapsedMilliseconds.ToString() + "ms");

            }
        }
        private  void ActionAsynC2Down(int index, HObject image, bool bTest)
        {
            Stopwatch sw = new Stopwatch();
            try
            {
                CommonSet.WriteInfo("组装2处理图像" + index.ToString() + "开始");
               
                sw.Start();
                CommonSet.dic_Assemble2[index].pfDown.SetRun(true);
                if (bTest)
                {
                    CommonSet.dic_Assemble2[index].pfDown.hwin = CommonSet.lstFlashWin[index - 10];
                }
                else
                {
                    CommonSet.dic_Assemble2[index].pfDown.hwin = FrmShowImage.lstC2Win[index - 10];
                }
                CommonSet.dic_Assemble2[index].actionDown(CommonSet.dic_Assemble2[index].hImageDown);
                CommonSet.dic_Assemble2[index].pfDown.showObj();
                CommonSet.WriteInfo("图像组装2显示" + index.ToString() + "时间" + CommonSet.dic_Assemble2[index].pfDown.dShow1.ToString() + "ms  "
                   + CommonSet.dic_Assemble2[index].pfDown.dShow2.ToString() + "ms  " + CommonSet.dic_Assemble2[index].pfDown.dShow3.ToString() + "ms  ");
                if (bTest)
                {

                    //double dx = (CommonSet.dic_OptSuction1[index].dFlashCalibColumn - CommonSet.dic_OptSuction1[index].dCenterDownColumn) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);
                    //double dy = (CommonSet.dic_OptSuction1[index].dFlashCalibRow - CommonSet.dic_OptSuction1[index].dCenterDownRow) * Math.Abs(ProductSuction.lstCalibX1[0] - ProductSuction.lstCalibX1[1]) / Math.Abs(ProductSuction.lstCalibC1[0] - ProductSuction.lstCalibC1[1]);


                    if (TestFlash.bFlash)
                    {
                        TestFlash.lstPoint1[index - 10].X = CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint1[index - 10].Y = CommonSet.dic_Assemble2[index].imgResultDown.CenterRow;
                    }
                    else
                    {
                        TestFlash.lstPoint[index - 10].X = CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn;
                        TestFlash.lstPoint[index - 10].Y = CommonSet.dic_Assemble2[index].imgResultDown.CenterRow;
                    }
                    string strDispRow1 = "定点R:" + TestFlash.lstPoint[index - 10].Y.ToString("0.000");
                    string strDispCol1 = "定点C:" + TestFlash.lstPoint[index - 10].X.ToString("0.000");

                    string strDispRow2 = "飞拍R:" + TestFlash.lstPoint1[index - 10].Y.ToString("0.000");
                    string strDispCol2 = "飞拍C:" + TestFlash.lstPoint1[index - 10].X.ToString("0.000");

                    double diffR = TestFlash.lstPoint1[index - 10].Y - TestFlash.lstPoint[index - 10].Y;
                    double diffC = TestFlash.lstPoint1[index - 10].X - TestFlash.lstPoint[index - 10].X;

                    string strDispRow3 = "差R:" + diffR.ToString("0.000");
                    string strDispCol3 = "差C:" + diffC.ToString("0.000");

                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow1, "window", 0, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol1, "window", 20, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow2, "window", 40, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol2, "window", 60, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispRow3, "window", 80, 0, "black", "true");
                    CommonSet.disp_message(CommonSet.lstFlashWin[index - 10], strDispCol3, "window", 100, 0, "black", "true");
                    CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");

                    return;
                }
                double dx = 0, dy = 0;
               // GetCenterOffset2(index, ref dx, ref dy);
                dx = AssembleSuction2.GetDownResulotion() * (CommonSet.dic_Assemble2[index].imgResultDown.CenterRow - CommonSet.dic_Assemble2[index].pSuctionCenter.X);
                dy = AssembleSuction2.GetDownResulotion() * (CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn - CommonSet.dic_Assemble2[index].pSuctionCenter.Y);

                string strDispRow = "Row:" + CommonSet.dic_Assemble2[index].imgResultDown.CenterRow.ToString("0.000");
                string strDispCol = "Col:" + CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn.ToString("0.000");
                // string strRadius = "R:" + CommonSet.dic_OptSuction1[index].imgResultDown.dRadius.ToString("0.000");
                HWindow hShowWin = FrmShowImage.lstC2Win[index - 10];
                CommonSet.disp_message(hShowWin, strDispRow, "window", 0, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, strDispCol, "window", 20, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, CommonSet.dic_Assemble2[index].imgResultDown.dAngle.ToString("0.0"), "window", 40, 0, strShowColor, "false");
                ShowResult(hShowWin, CommonSet.dic_Assemble2[index].imgResultDown.bImageResult, 60);
                string colorX = strShowNgColor, colorY = strShowNgColor;
                if (Math.Abs(dx) < 0.05)
                {
                    colorX = strShowColor;
                }
                if (Math.Abs(dy) < 0.05)
                {
                    colorY = strShowColor;
                }

                CommonSet.disp_message(hShowWin, "dx:" + dx.ToString("0.000") + "mm", "window", 80, 0, colorX, "false");
                CommonSet.disp_message(hShowWin, "dy:" + dy.ToString("0.000") + "mm", "window", 100, 0, colorY, "false");
                CommonSet.disp_message(hShowWin, "dR:" + (CommonSet.dic_Assemble2[index].imgResultDown.CenterRow - CommonSet.dic_Assemble2[index].pSuctionCenter.X).ToString("0.000"), "window", 120, 0, strShowColor, "false");
                CommonSet.disp_message(hShowWin, "dC:" + (CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn - CommonSet.dic_Assemble2[index].pSuctionCenter.Y).ToString("0.000"), "window", 140, 0, strShowColor, "false");
               
                // CommonSet.disp_message(CommonSet.lstA1Win[index - 1], strRadius, "window", 60, 0, "cyan", "false");
                HOperatorSet.DispCross(hShowWin, CommonSet.dic_Assemble2[index].imgResultDown.CenterRow, CommonSet.dic_Assemble2[index].imgResultDown.CenterColumn, 100, 0);
                sw.Stop();
                CommonSet.WriteInfo("处理图像" + index.ToString() + "完成");
                CommonSet.WriteInfo("图像组装2处理" + index.ToString() + "时间" + sw.ElapsedMilliseconds.ToString() + "ms");
                if (CommonSet.bSavePic)
                {
                    SaveImg(image, CommonSet.strSaveImg + "S" + index.ToString(), CommonSet.iCurrentPicNum.ToString());
                    if (index >= 18)
                    {
                        CommonSet.iCurrentPicNum++;
                    }

                    if (CommonSet.iCurrentPicNum > CommonSet.iSaveNum)
                    {
                        CommonSet.bSavePic = false;
                    }
                }
                if (CommonSet.bSaveNG)
                {
                    if (!CommonSet.dic_Assemble2[index].imgResultDown.bImageResult)
                    {
                        SaveImg(image, CommonSet.strSaveImg + "NG", "S" + index.ToString() + DateTime.Now.ToString("_yy_MM_dd_HH_mm_ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                CommonSet.WriteInfo("组装2处理图像" + index.ToString() + "异常");
                sw.Stop();

                CommonSet.WriteInfo("图像组装2处理" + index.ToString() + "时间" + sw.ElapsedMilliseconds.ToString() + "ms");

            }


        }
        private void ShowResult(HWindow hwin, bool bResult,int iPosRow)
        {
            if (bResult)
            {
                CommonSet.disp_message(hwin, "OK", "window", iPosRow, 0, "green", "false");
            }
            else
            {
                CommonSet.disp_message(hwin, "NG", "window", iPosRow, 0, "red", "false");
            }
        }
        private  void ActionCallBack(IAsyncResult result)
        {
            AsyncResult _result = (AsyncResult)result;
            Action<int, HObject, bool> deleObj = (Action<int, HObject, bool>)_result.AsyncDelegate;
            deleObj.EndInvoke(_result);
        }
        private  void SaveImg(HObject hoImage, string strPath, string name)
        {
            try
            {
                if (!Directory.Exists(strPath))
                {
                    DirectoryInfo info = Directory.CreateDirectory(strPath);
                }
            }
            catch (Exception)
            {
            }

            HOperatorSet.WriteImage(hoImage, "bmp", 0, strPath + "\\" + name + ".bmp");
        }

        public void test(HObject image)
        {

            try
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                for (; Assem1Module.iPicNum < 10; Assem1Module.iPicNum++)
                {
                    int index = Assem1Module.iPicNum;

                    AssembleSuction1 ps = CommonSet.dic_Assemble1[index];
                    if (ps.BUse)
                    {
                        CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                        if (CommonSet.dic_Assemble1[index].hImageDown != null)
                            CommonSet.dic_Assemble1[index].hImageDown.Dispose();
                        //swProcessListener.Restart();
                        HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble1[index].hImageDown);

                        // CommonSet.dic_Suction[1].hImage = image;
                        HOperatorSet.SetPart(CommonSet.lstC1Win[index - 1], 0, 0, height.I, width.I);
                        HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, CommonSet.lstC1Win[index - 1]);
                        //Assem1Module.iPicNum++;
                        //Assem1Module.iPicSum--;
                        ImageArgs args = new ImageArgs();
                        args.Index = index;
                        args.Image = CommonSet.dic_Assemble1[index].hImageDown;
                        args.BTest = false;
                        //onCheckC1(this, args);
                        // Task t = Task.Factory.StartNew(() => ActionAsynC1Down(index, CommonSet.dic_Assemble1[index].hImageDown, false));
                        //Action<int, HObject, bool> dele = ActionAsynC1Down;
                        //dele.BeginInvoke(index, CommonSet.dic_Assemble1[index].hImageDown, false, ActionCallBack, null);
                        // ThreadPool.QueueUserWorkItem(new WaitCallback(testCallBack),args);
                        muC1.WaitOne();
                        lstImgC1.Enqueue(args);
                        muC1.ReleaseMutex();
                        CommonSet.WriteInfo("组装1队列数目" + lstImgC1.Count);

                    }
                    else
                    {
                        HOperatorSet.SetPart(CommonSet.lstC1Win[index - 1], 0, 0, height, width);
                        HOperatorSet.ClearWindow(CommonSet.lstC1Win[index - 1]);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
            }
        }
        public void test2(HObject image)
        {
            try
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                for (; Assem1Module.iPicNum < 10; Assem1Module.iPicNum++)
                {
                    int index = Assem1Module.iPicNum;
                    CommonSet.WriteInfo("获取图像" + (index + 9).ToString() + "完成");
                    AssembleSuction2 ps = CommonSet.dic_Assemble2[index + 9];
                    if (ps.BUse)
                    {
                        if (CommonSet.dic_Assemble2[index + 9].hImageDown != null)
                            CommonSet.dic_Assemble2[index + 9].hImageDown.Dispose();
                        //swProcessListener.Restart();
                        HOperatorSet.CopyImage(image, out CommonSet.dic_Assemble2[index + 9].hImageDown);

                        // CommonSet.dic_Suction[1].hImage = image;
                        HOperatorSet.SetPart(CommonSet.lstC2Win[index - 1], 0, 0, height.I, width.I);
                        HOperatorSet.DispImage(CommonSet.dic_Assemble2[index + 9].hImageDown, CommonSet.lstC2Win[index - 1]);
                        //Assem2Module.iPicNum++;
                        //Assem2Module.iPicSum--;
                        ImageArgs args = new ImageArgs();
                        args.Index = index + 9;
                        args.Image = CommonSet.dic_Assemble2[index + 9].hImageDown;
                        args.BTest = false;
                        muC2.WaitOne();
                        lstImgC2.Enqueue(args);
                        muC2.ReleaseMutex();
                        CommonSet.WriteInfo("组装2队列数目" + lstImgC2.Count);
                        // ThreadPool.QueueUserWorkItem(new WaitCallback(testCallBack2), args);
                        //this.onCheckC2(this, args);
                        //Task t = Task.Factory.StartNew(() => ActionAsynC2Down(index+9, CommonSet.dic_Assemble2[index+9].hImageDown, false));
                        //Action<int, HObject, bool> dele = ActionAsynC2Down;
                        //dele.BeginInvoke(index + 9, CommonSet.dic_Assemble2[index + 9].hImageDown, false, ActionCallBack, null);
                        //break;
                    }
                    else
                    {
                        HOperatorSet.SetPart(CommonSet.lstC2Win[index - 1], 0, 0, height, width);
                        HOperatorSet.ClearWindow(CommonSet.lstC2Win[index - 1]);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("工位2取料下相机图像处理异常", ex);
            }
        }
        public void testA1(HObject image)
        {
            try
            {
                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                for (; Assem1Module.iPicNum < 10; Assem1Module.iPicNum++)
                {
                    int index = Assem1Module.iPicNum;

                    OptSution1 ps = CommonSet.dic_OptSuction1[index];
                    if (ps.BUse)
                    {
                        CommonSet.WriteInfo("获取图像" + index.ToString() + "完成");
                        //swProcessListener.Restart();
                        if (CommonSet.dic_OptSuction1[index].hImageDown != null)
                            CommonSet.dic_OptSuction1[index].hImageDown.Dispose();
                        HOperatorSet.CopyImage(image, out CommonSet.dic_OptSuction1[index].hImageDown);

                        // CommonSet.dic_Suction[1].hImage = image;
                        HOperatorSet.SetPart(CommonSet.lstA1Win[index - 1], 0, 0, height.I, width.I);
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, CommonSet.lstA1Win[index - 1]);
                        //FlashModule1.iPicNum++;
                        //FlashModule1.iPicSum--;
                        ImageArgs args = new ImageArgs();
                        args.Index = index;
                        args.Image = CommonSet.dic_OptSuction1[index].hImageDown;
                        args.BTest = false;
                        muA1.WaitOne();
                        lstImgA1.Enqueue(args);
                        muA1.ReleaseMutex();
                        CommonSet.WriteInfo("取料1队列数目" + lstImgA1.Count);
                        // ThreadPool.QueueUserWorkItem(new WaitCallback(testCallBackA1), args);
                        //this.onCheckA1(this, args);
                        //Task t = Task.Factory.StartNew(() => ActionAsynA1Down(index, CommonSet.dic_OptSuction1[index].hImageDown, false));
                        //Action<int, HObject,bool> dele = ActionAsynA1Down;
                        //dele.BeginInvoke(index, CommonSet.dic_OptSuction1[index].hImageDown, false, ActionCallBack, null);
                        //  break;
                    }
                    else
                    {
                        HOperatorSet.SetPart(CommonSet.lstA1Win[index - 1], 0, 0, height, width);
                        HOperatorSet.ClearWindow(CommonSet.lstA1Win[index - 1]);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonSet.WriteDebug("工位1取料下相机图像处理异常", ex);
            }
        }
        public void testA2(HObject image)
        {

        }
        public void GetCenterOffset1(int sution, ref double dx,ref double dy)
        {
            //下相机镜片的中心换算到上相机
            CommonSet.dic_Calib[CalibStation.组装工位1].createCamDownToUp();

            HTuple outRow, outCol;//算出的镜片中心在上相机的像素坐标
            HOperatorSet.AffineTransPoint2d(CommonSet.dic_Calib[CalibStation.组装工位1].homCamDownToUp, CommonSet.dic_Assemble1[sution].imgResultDown.CenterRow, CommonSet.dic_Assemble1[sution].imgResultDown.CenterColumn, out outRow, out outCol);
            HTuple outRow1, outCol1;
            HOperatorSet.AffineTransPoint2d(CommonSet.dic_Calib[CalibStation.组装工位1].homCamDownToUp, CommonSet.dic_Assemble1[sution].pSuctionCenter.X, CommonSet.dic_Assemble1[sution].pSuctionCenter.Y, out outRow1, out outCol1);


            HTuple hom = CommonSet.HomatPixelToAxis(AssembleSuction1.lstCalibCameraUpRC1, AssembleSuction1.lstCalibCameraUpXY1);
           
            try
            {
                //吸笔中心坐标对应的9点标定的轴坐标
                HTuple outBarrelX, outBarrelY;
                HOperatorSet.AffineTransPoint2d(hom, outRow1, outCol1, out outBarrelX, out outBarrelY);
                //镜片中心对应的轴坐标    
                HTuple outLenX, outLenY;
                HOperatorSet.AffineTransPoint2d(hom, outRow, outCol, out outLenX, out outLenY);

                dx =   (outBarrelX.D - outLenX.D);
                dy =  (outBarrelY.D - outLenY.D) ;
                // p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {

            }
        }
        public void GetCenterOffset2(int sution, ref double dx, ref double dy)
        {
            //下相机镜片的中心换算到上相机
            CommonSet.dic_Calib[CalibStation.组装工位2].createCamDownToUp();

            HTuple outRow, outCol;//算出的镜片中心在上相机的像素坐标
            HOperatorSet.AffineTransPoint2d(CommonSet.dic_Calib[CalibStation.组装工位2].homCamDownToUp, CommonSet.dic_Assemble2[sution].imgResultDown.CenterRow, CommonSet.dic_Assemble2[sution].imgResultDown.CenterColumn, out outRow, out outCol);
            HTuple outRow1, outCol1;
            HOperatorSet.AffineTransPoint2d(CommonSet.dic_Calib[CalibStation.组装工位2].homCamDownToUp, CommonSet.dic_Assemble2[sution].pSuctionCenter.X, CommonSet.dic_Assemble2[sution].pSuctionCenter.Y, out outRow1, out outCol1);


            HTuple hom = CommonSet.HomatPixelToAxis(AssembleSuction2.lstCalibCameraUpRC1, AssembleSuction2.lstCalibCameraUpXY1);
            try
            {
                //吸笔中心坐标对应的9点标定的轴坐标
                HTuple outBarrelX, outBarrelY;
                HOperatorSet.AffineTransPoint2d(hom, outRow1, outCol1, out outBarrelX, out outBarrelY);
                //镜片中心对应的轴坐标    
                HTuple outLenX, outLenY;
                HOperatorSet.AffineTransPoint2d(hom, outRow, outCol, out outLenX, out outLenY);

                dx = (outBarrelX.D - outLenX.D);
                dy = (outBarrelY.D - outLenY.D);
                // p.Z = DGetPosZ;
            }
            catch (Exception ex)
            {

            }
        }

    }
    public class ImageArgs : EventArgs
    {
        public int Index { get; set; }
        public HObject Image { get; set; }
        public bool BTest { get; set; }
    }
    public class RotateAngleArgs : EventArgs
    {
        public int Index { get; set; }
        public bool bRotate = false;
        public double DAngle { get; set; }
    }
}
