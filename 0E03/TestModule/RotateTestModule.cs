using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion0;
using CameraSet0;
namespace _0E030
{
    public class RotateTestModule : ActionModule
    {
        private string strOut = "RotateTest-Action-";

        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        private double dDestPosC = 0;
        public static AXIS axisC;
        public static ICamera cam;

        public RotateTestModule()
        {
            lstAction.Clear();

            lstAction.Add(ActionName._b0C轴到原点位);
            lstAction.Add(ActionName._b0C轴到位);
            lstAction.Add(ActionName._b0C轴定长旋转);
            lstAction.Add(ActionName._b0C轴到位);
            lstAction.Add(ActionName._b0镜筒中心拍照);
            lstAction.Add(ActionName._b0中心拍照完成);

        }
        public override void Action(ActionName action, ref int step)
        {

            try
            {

                switch (action)
                {
                    case ActionName._b0C轴到原点位:
                        dDestPosC = 0;
                        mc.AbsMove(axisC, dDestPosC, (int)ModelManager.VelC.Vel);
                       //cam.SetExposure(BarrelSuction.dExposureTimeDown);
                        //CommonSet.camDownD1.SetGain(BarrelSuction.dGainDown);
                        if(axisC == AXIS.C1轴)
                            ModelManager.SuctionLParam.InitImageDown(CalibrationL.strPicRotateName);
                        else
                            ModelManager.SuctionRParam.InitImageDown(CalibrationR.strPicRotateName);

                        WriteOutputInfo(strOut + "C轴到原点位");
                        step = step + 1;
                        break;
                    case ActionName._b0C轴到位:
                        if (IsAxisINP(dDestPosC, axisC, 1))
                        {

                            WriteOutputInfo(strOut + "点胶C轴到原点位");
                            step = step + 1;

                        }
                        break;
                    case ActionName._b0C轴定长旋转:
                        if (CalibrationL.iRotateNum < (int)(360 / CalibrationL.iRotateStep))
                        {
                            int iTempAngle = CalibrationL.iRotateNum * CalibrationL.iRotateStep;
                            dDestPosC = iTempAngle;
                            mc.AbsMove(axisC, dDestPosC, ModelManager.VelC.Vel);
                            //CommonSet.camDownD1.SetExposure(BarrelSuction.dExposureTimeDown);
                            //CommonSet.camDownD1.SetGain(BarrelSuction.dGainDown);
                            if (axisC == AXIS.C1轴)
                                ModelManager.SuctionLParam.InitImageDown(CalibrationL.strPicRotateName);
                            else
                                ModelManager.SuctionRParam.InitImageDown(CalibrationR.strPicRotateName);

                            CalibrationL.iRotateNum++;
                            WriteOutputInfo(strOut + "C轴旋转到" + iTempAngle.ToString() + "度");
                            step = step + 1;
                        }
                        else
                        {
                            Run.runMode = RunMode.手动;
                        }
                        break;

                    case ActionName._b0镜筒中心拍照:
                      
                        if (sw.WaitSetTime(50))
                        {
                            cam.SoftTrigger();
                            WriteOutputInfo(strOut + "相机拍照");
                            step = step + 1;
                        }
                        break;
                    case ActionName._b0中心拍照完成:
                        if(axisC == AXIS.C1轴) { 
                            if (ModelManager.SuctionLParam.imgResultDown.bStatus)
                            {
                                if (sw.WaitSetTime(1000))
                                {
                                    if (ModelManager.SuctionLParam.imgResultDown.CenterColumn != 0)
                                    {
                                        CalibrationL.lstRotateRow.Add(ModelManager.SuctionLParam.imgResultDown.CenterRow);
                                        CalibrationL.lstRotateColumn.Add(ModelManager.SuctionLParam.imgResultDown.CenterColumn);
                                    }
                                    WriteOutputInfo(strOut + "中心拍照完成");
                                    step = 2;
                                }
                            }
                            else
                            {
                                if (sw.WaitSetTime(500))
                                {
                                    step = 2;
                                }
                            }
                        }else
                        {
                            if (ModelManager.SuctionRParam.imgResultDown.bStatus)
                            {
                                if (sw.WaitSetTime(1000))
                                {
                                    if (ModelManager.SuctionRParam.imgResultDown.CenterColumn != 0)
                                    {
                                        CalibrationL.lstRotateRow.Add(ModelManager.SuctionRParam.imgResultDown.CenterRow);
                                        CalibrationL.lstRotateColumn.Add(ModelManager.SuctionRParam.imgResultDown.CenterColumn);
                                    }
                                    WriteOutputInfo(strOut + "中心拍照完成");
                                    step = 2;
                                }
                            }
                            else
                            {
                                if (sw.WaitSetTime(500))
                                {
                                    step = 2;
                                }
                            }

                        }
                        break;
                }

            }
            catch (Exception)
            {


            }

        }


        public override void Reset()
        {

        }

        public override void Action2()
        {

        }
    }
}
