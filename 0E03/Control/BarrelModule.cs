using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
namespace _0E03
{
    /// <summary>
    /// 镜筒模块
    /// </summary>
    public class BarrelModule:ActionModule
    {
        private string strOut = ",镜筒拍照模块,Action,";
        
        private Point currentPoint = null;
        private int iCurrentTimes = 1;
        private int iCurrentNum = 1;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static bool bBarrelResult = false;//镜筒拍照OK标志

        public static int iResetStep = 0;
        public static int iCamTimes = 0;
        public BarrelModule()
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
                    bBarrelResult = false;
                    if ((AssemLModule.iResetStep > 1) && (AssemRModule.iResetStep > 1)||(Run.assemLModule.bResetFinish&& Run.assemRModule.bResetFinish))
                    {
                        Action(ActionName._20XY到安全位, ref iResetStep);
                    }
                    break;
                case 1:
                    Action(ActionName._20XY轴到位完成, ref iResetStep);

                    break;
                case 2:
                    BarrelState = ActionState.等待;
                    bResetFinish = true;
                    iResetStep = 0;
                    break;
            }
        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
                switch (action) {
                    case ActionName._20开始拍照:
                        //如果相机模块处理等待状态，且图像结果NG，则开始拍照
                        if ((BarrelState == ActionState.等待) && (!bBarrelResult))
                        {
                            iCamTimes = 0;
                            BarrelState = ActionState.拍照;
                            step = step + 1;
                            WriteOutputInfo(strOut + "开始拍照");
                        }

                        break;


                    case ActionName._20获取拍照位:
                        if (IsBarrelEmpty())
                        {
                            Alarminfo.AddAlarm(Alarm.镜筒盘无空镜筒);
                            //报警
                            Run.runMode = RunMode.暂停;
                            WriteOutputInfo(strOut + "镜筒盘满");
                            dDestPosX = ModelManager.BarrelParam.pSafeXY.X;
                            dDestPosY = ModelManager.BarrelParam.pSafeXY.Y;
                            mc.AbsMove(AXIS.镜筒X轴, dDestPosX, 200, ModelManager.VelBarrelX.ACCAndDec, ModelManager.VelBarrelX.ACCAndDec);
                            mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, 200, ModelManager.VelBarrelY.ACCAndDec, ModelManager.VelBarrelY.ACCAndDec);
                            WriteOutputInfo(strOut + "XY到安全位");
                        }
                        else
                        {
                            currentPoint = ModelManager.BarrelParam.getCameraCoordinateByIndex();
                            step = step + 1;
                        }
                     
                        break;
                    case ActionName._20XY到安全位:
                        dDestPosX = ModelManager.BarrelParam.pSafeXY.X;
                        dDestPosY = ModelManager.BarrelParam.pSafeXY.Y;
                        mc.AbsMove(AXIS.镜筒X轴, dDestPosX, 200, ModelManager.VelBarrelX.ACCAndDec, ModelManager.VelBarrelX.ACCAndDec);
                        mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, 200, ModelManager.VelBarrelY.ACCAndDec, ModelManager.VelBarrelY.ACCAndDec);
                        WriteOutputInfo(strOut + "XY到安全位");
                        step = step + 1;
                        break;
                    case ActionName._20XY轴到拍照位:

                        dDestPosX = currentPoint.X;
                        dDestPosY = currentPoint.Y;
                        mc.AbsMove(AXIS.镜筒X轴, dDestPosX, ModelManager.VelBarrelX.Vel);
                        mc.AbsMove(AXIS.镜筒Y轴, dDestPosY, ModelManager.VelBarrelY.Vel);
                        WriteOutputInfo(strOut + "XY轴到拍照位:" + dDestPosX.ToString("0.000") + "," + dDestPosY.ToString("0.000"));
                        step = step + 1;
                        break;
                    case ActionName._20XY轴到位完成:
                        if (IsAxisINP2(dDestPosX, AXIS.镜筒X轴, 0.01) && IsAxisINP2(dDestPosY, AXIS.镜筒Y轴, 0.01))
                        {
                            WriteOutputInfo(strOut + "XY轴到拍照位");
                            step = step + 1;
                        }

                        break;
                    case ActionName._20相机拍照:
                        iCamTimes++;
                        if(Run.runMode == RunMode.组装标定)
                        {
                            ModelManager.BarrelParam.InitImageUp(CalibrationBL.strPicUpCalibName);
                            ModelManager.CamBarrel.SetExposure(CalibrationBL.ICalibUpExposure);

                        }
                        else
                        {
                            ModelManager.BarrelParam.InitImageUp(ModelManager.BarrelParam.strPicCameraUpName);
                            ModelManager.CamBarrel.SetExposure(ModelManager.BarrelParam.IProductUpExposure);
                        }
                        
                        ModelManager.CamBarrel.SoftTrigger();
                        WriteOutputInfo(strOut + "相机拍照");
                        step = step + 1;
                        break;
                    case ActionName._20相机拍照完成:
                        if (ModelManager.BarrelParam.imgResultUp.bStatus)
                        {
                            int iCurrentTrayIndex = ModelManager.BarrelParam.iCurrentTrayIndex-1;
                            int pos = Barrel.lstTray[iCurrentTrayIndex].CurrentPos;
                            if (ModelManager.BarrelParam.imgResultUp.bImageResult || CommonSet.bTestRun)
                            {

                                Barrel.lstTray[iCurrentTrayIndex].setNumColor(pos, CommonSet.ColorNotUse);
                                Barrel.lstTray[iCurrentTrayIndex].updateColor();
                                //Barrel.lstTray[iCurrentTrayIndex].CurrentPos++;
                                WriteOutputInfo(strOut + "相机拍照检测OK");
                                step = step + 1;
                            }
                            else
                            {
                                Barrel.lstTray[iCurrentTrayIndex].setNumColor(pos, CommonSet.ColorNG);
                                Barrel.lstTray[iCurrentTrayIndex].updateColor();
                                Barrel.lstTray[iCurrentTrayIndex].CurrentPos++;
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
                        bBarrelResult = true;
                        BarrelState = ActionState.等待;
                        WriteOutputInfo(strOut + "镜筒拍照完成");
                        step = step + 1;
                        break;
                }


            }
            catch (Exception ex)
            {

                WriteOutputInfo(strOut + "镜筒拍照模块动作执行异常：" + ex.ToString());
            }

        }
        //判断是否有料
        public bool IsBarrelEmpty()
        {
            if (ModelManager.BarrelParam.iCurrentTrayIndex == 1)
            {
                if (ModelManager.BarrelParam.bExitTray[0])
                {
                    if (Barrel.lstTray[0].bFinish)
                    {
                        //如果第1个托盘不存在，则更改托盘号，
                        ModelManager.BarrelParam.iCurrentTrayIndex = 2;
                        //FrmMain.trayPB.ShowTray(Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1]);
                        return IsBarrelEmpty();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //如果第1个托盘不存在，则更改托盘号，
                    ModelManager.BarrelParam.iCurrentTrayIndex = 2;

                    return IsBarrelEmpty();
                }
            }
            else if (ModelManager.BarrelParam.iCurrentTrayIndex == 2)
            {
                if (ModelManager.BarrelParam.bExitTray[1])
                {
                    if (Barrel.lstTray[1].bFinish)
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
