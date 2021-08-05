using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace _0E03
{
    /// <summary>
    /// 左上相机拍照模块
    /// </summary>
    public class CameraLModule:ActionModule
    {
        private string strOut = ",左拍照模块,Action,";
        //private static GetProduct1Module module = null;
        public static int iCurrentSuction = 1;//当前吸笔序号
     
        public Point currentPoint = null;
        private int iCurrentTimes = 1;
        private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static bool bIsProductEmpty = false;//盘子中有无料，true为无料
        public static bool bImageResult = false;//图像结果
        public static int iCurrentBarrel = 0;//当前镜筒位置
       
        public static int iResetStep = 0;
        public static int iCamTimes = 0;
        public CameraLModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._20开始拍照);
            lstAction.Add(ActionName._20获取拍照位);
            lstAction.Add(ActionName._20XY轴到拍照位);
            lstAction.Add(ActionName._20XY轴到位完成);
            lstAction.Add(ActionName._20相机拍照);
            lstAction.Add(ActionName._20相机拍照完成);
            lstAction.Add(ActionName._20拍照完成);

        }
        public override void Reset()
        {
            switch (iResetStep)
            { 
                case 0:
                   
                    if (bResetFinish)
                        break;
                    IStep = 0;
                    bImageResult = false;
                    iCamTimes = 0;
                    if (AssemLModule.iResetStep > 1)
                    {
                        Action(ActionName._20XY到安全位, ref iResetStep);
                    }
                    break;
                case 1:
                    Action(ActionName._20XY轴到位完成, ref iResetStep);
                   
                    break;
                case 2:
                    CameraLState = ActionState.等待;
                    bResetFinish = true;
                    iResetStep = 0;
                    break;
            }

        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
                switch (action)
                { 
                    case ActionName._20开始拍照:
                        //如果相机模块处理等待状态，且图像结果NG，则开始拍照
                        if((CameraLState == ActionState.等待)&&(!bImageResult)&&ModelManager.SuctionLParam.BUse)
                        {
                            iCamTimes = 0;
                            CameraLState = ActionState.拍照;
                            step = step + 1;
                            WriteOutputInfo(strOut + "开始拍照");
                        }

                        break;

                    case ActionName._20获取拍照位:
                        if (IsBarrelEmpty())
                        {
                            //报警
                            Alarminfo.AddAlarm(Alarm.左取料盘空料);
                            WriteOutputInfo(strOut + "左取料盘空料");
                            Run.runMode = RunMode.暂停;
                            dDestPosX = ModelManager.SuctionLParam.pSafeXYZ.X;
                            dDestPosY = ModelManager.SuctionLParam.pSafeXYZ.Y;
                            mc.AbsMove(AXIS.取料X1轴, dDestPosX, 200, ModelManager.VelGetX.ACCAndDec, ModelManager.VelGetX.ACCAndDec);
                            mc.AbsMove(AXIS.取料Y1轴, dDestPosY, 200, ModelManager.VelGetY.ACCAndDec, ModelManager.VelGetY.ACCAndDec);
                            WriteOutputInfo(strOut + "XY到安全位");
                        }
                        else
                        {
                            currentPoint = ModelManager.SuctionLParam.getCameraCoordinateByIndex();
                            step = step + 1;
                        }
                        break;
                    case ActionName._20XY到安全位:
                        dDestPosX = ModelManager.SuctionLParam.pSafeXYZ.X;
                        dDestPosY = ModelManager.SuctionLParam.pSafeXYZ.Y;
                        mc.AbsMove(AXIS.取料X1轴, dDestPosX, 200, ModelManager.VelGetX.ACCAndDec, ModelManager.VelGetX.ACCAndDec);
                        mc.AbsMove(AXIS.取料Y1轴, dDestPosY, 200, ModelManager.VelGetY.ACCAndDec, ModelManager.VelGetY.ACCAndDec);
                        WriteOutputInfo(strOut + "XY到安全位");
                        step = step + 1;
                        break;
                    case ActionName._20XY轴到拍照位:

                        dDestPosX = currentPoint.X;
                        dDestPosY = currentPoint.Y;
                        mc.AbsMove(AXIS.取料X1轴, dDestPosX, ModelManager.VelGetX.Vel, ModelManager.VelGetX.ACCAndDec, ModelManager.VelGetX.ACCAndDec);
                        mc.AbsMove(AXIS.取料Y1轴, dDestPosY, ModelManager.VelGetY.Vel, ModelManager.VelGetY.ACCAndDec, ModelManager.VelGetY.ACCAndDec);
                        WriteOutputInfo(strOut + "XY轴到拍照位:"+dDestPosX.ToString("0.000")+","+dDestPosY.ToString("0.000"));
                        step = step + 1;
                        break;
                    case ActionName._20XY到标定拍照位:
                        dDestPosX = CalibrationL.pCalibUpPosXY.X;
                        dDestPosY = CalibrationL.pCalibUpPosXY.Y;
                        mc.AbsMove(AXIS.取料X1轴, dDestPosX, ModelManager.VelGetX.Vel, ModelManager.VelGetX.ACCAndDec, ModelManager.VelGetX.ACCAndDec);
                        mc.AbsMove(AXIS.取料Y1轴, dDestPosY, ModelManager.VelGetY.Vel, ModelManager.VelGetY.ACCAndDec, ModelManager.VelGetY.ACCAndDec);
                        WriteOutputInfo(strOut + "XY轴到拍照位:" + dDestPosX.ToString("0.000") + "," + dDestPosY.ToString("0.000"));
                        step = step + 1;
                        break;
                    case ActionName._20XY轴到位完成:
                        if (IsAxisINP2(dDestPosX, AXIS.取料X1轴, 0.01) && IsAxisINP2(dDestPosY, AXIS.取料Y1轴, 0.01))
                        {
                            WriteOutputInfo(strOut + "XY轴到位完成");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20相机拍照:
                        iCamTimes++;
                        if (Run.runMode == RunMode.取料标定)
                        {
                            ModelManager.SuctionLParam.InitImageUp(CalibrationL.strPicUpCalibName);
                            ModelManager.CamUpL.SetExposure(CalibrationL.ICalibUpExposure);
                        }
                        else { 
                            ModelManager.SuctionLParam.InitImageUp(ModelManager.SuctionLParam.strPicCameraUpName);
                            ModelManager.CamUpL.SetExposure(ModelManager.SuctionLParam.IProductUpExposure);
                        }
                        ModelManager.CamUpL.SoftTrigger();
                        WriteOutputInfo(strOut + "相机拍照");
                        step = step + 1;
                        break;
                    case ActionName._20相机拍照完成:
                        if (ModelManager.SuctionLParam.imgResultUp.bStatus)
                        {
                            int iCurrentTrayIndex = ModelManager.SuctionLParam.iCurrentTrayIndex - 1;
                            int pos = SuctionL.lstTray[iCurrentTrayIndex].CurrentPos;
                            if (ModelManager.SuctionLParam.imgResultUp.bImageResult || CommonSet.bTestRun)
                            {

                                SuctionL.lstTray[iCurrentTrayIndex].setNumColor(pos, CommonSet.ColorInit);
                                SuctionL.lstTray[iCurrentTrayIndex].updateColor();
                                SuctionL.lstTray[iCurrentTrayIndex].CurrentPos++;
                                WriteOutputInfo(strOut + "相机拍照检测OK");
                                step = step + 1;
                            }
                            else
                            {
                                SuctionL.lstTray[iCurrentTrayIndex].setNumColor(pos, CommonSet.ColorNG);
                                SuctionL.lstTray[iCurrentTrayIndex].updateColor();
                                SuctionL.lstTray[iCurrentTrayIndex].CurrentPos++;
                                WriteOutputInfo(strOut + "相机拍照检测NG");
                                step = lstAction.IndexOf(ActionName._20获取拍照位);

                            }
                        }
                        else
                        {
                            
                            if (sw.WaitSetTime(500))
                            {
                                if (iCamTimes < 2)
                                {
                                    step = step - 1;
                                    WriteOutputInfo(strOut + "第二次拍照");
                                }
                                else
                                {
                                    step = step + 1;
                                }
                            }
                        }


                        break;
                    case ActionName._20拍照完成:
                        bImageResult = true;
                        CameraLState = ActionState.等待;
                        WriteOutputInfo(strOut + "拍照模块完成");
                        step = step + 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                
                 WriteOutputInfo(strOut + "拍照异常："+ex.ToString());
            }


        }

        //判断是否有料
        public bool IsBarrelEmpty()
        {
            if (ModelManager.SuctionLParam.iCurrentTrayIndex == 1)
            {
                if (ModelManager.SuctionLParam.bExitTray[0])
                {
                    if (SuctionL.lstTray[0].bFinish)
                    {
                        //如果第1个托盘不存在，则更改托盘号，
                        ModelManager.SuctionLParam.iCurrentTrayIndex = 2;
                        //FrmMain.trayPL.ShowTray(SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1]);
                        return IsBarrelEmpty();
                    }
                    else
                    {
                        return false;
                    }
                }else
                {
                    //如果第1个托盘不存在，则更改托盘号，
                    ModelManager.SuctionLParam.iCurrentTrayIndex = 2;
                   
                    return IsBarrelEmpty();
                }
            }
            else if (ModelManager.SuctionLParam.iCurrentTrayIndex == 2)
            {
                if (ModelManager.SuctionLParam.bExitTray[1])
                {
                    if (SuctionL.lstTray[1].bFinish)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            
        }

        public override void Action2()
        {
            
        }
    }
}
