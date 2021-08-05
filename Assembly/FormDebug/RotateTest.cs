using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;

namespace Assembly
{
    public class RotateTest:ActionModule
    {
        private string strOut = "RotateTest-Action-";
    
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        private double dDestPosC = 0;

      
        public RotateTest()
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
                        mc.AbsMove(AXIS.点胶C轴, dDestPosC, (int)CommonSet.dVelGlueC);
                         CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                          CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                          BarrelSuction.InitImageUp(BarrelSuction.strPicRotateName);
                        WriteOutputInfo(strOut + "C轴到原点位");
                        step = step + 1;
                        break;
                    case ActionName._b0C轴到位:
                        if (IsAxisINP(dDestPosC,AXIS.点胶C轴,1))
                        {
                            
                                WriteOutputInfo(strOut + "点胶C轴到原点位");
                                step = step + 1;
                            
                        }
                        break;
                    case ActionName._b0C轴定长旋转:
                        if (BarrelSuction.iRotateNum < (int)(360 / BarrelSuction.iRotateStep))
                        {
                            int iTempAngle = BarrelSuction.iRotateNum * BarrelSuction.iRotateStep;
                            dDestPosC = iTempAngle;
                            mc.AbsMove(AXIS.点胶C轴, dDestPosC, (int)CommonSet.dVelGlueC);
                            CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                            CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                            BarrelSuction.InitImageUp(BarrelSuction.strPicRotateName);
                            BarrelSuction.iRotateNum++;
                            WriteOutputInfo(strOut + "C轴旋转到" + iTempAngle.ToString() + "度");
                            step = step + 1;
                        }
                        else
                        {
                            Run.runMode = RunMode.手动;
                        }
                        break;

                    case ActionName._b0镜筒中心拍照:
                        mc.setDO(DO.点胶上相机, true);
                        if (sw.WaitSetTime(5))
                        {
                            mc.setDO(DO.点胶上相机, false);
                            WriteOutputInfo(strOut + "点胶上相机拍照");
                            step = step + 1;
                        }
                        break;
                    case ActionName._b0中心拍照完成:
                        if (BarrelSuction.imgResultUp.bStatus)
                        {
                            if (sw.WaitSetTime(1000))
                            {
                                if (BarrelSuction.imgResultUp.CenterColumn != 0)
                                {
                                    BarrelSuction.lstRotateRow.Add(BarrelSuction.imgResultUp.CenterRow);
                                    BarrelSuction.lstRotateColumn.Add(BarrelSuction.imgResultUp.CenterColumn);
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
