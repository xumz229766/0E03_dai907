using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0E03
{
    public class TestGetOptRModule :AssemRModule
    {
        private string strOut = ",取料测试模块,Action,";

        private Point currentPoint = null;
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        private double dDestC = 0;
        private double dDestAssemPosX = 0;
        public static int pos = 1;
        public static int iTray = 1;
        public static string strType = "取料";
        private CameraRModule camModule = new CameraRModule();
        public TestGetOptRModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._50动作选择);
            //取料
            lstAction.Add(ActionName._10Z轴到安全位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10X轴到下相机拍照位);
            lstAction.Add(ActionName._10X轴到位完成);
            lstAction.Add(ActionName._20XY轴到拍照位);
            lstAction.Add(ActionName._20XY轴到位完成);
            lstAction.Add(ActionName._20相机拍照);
            lstAction.Add(ActionName._20相机拍照完成);
            lstAction.Add(ActionName._10X轴到取料位);
            lstAction.Add(ActionName._10X轴到位完成);
            lstAction.Add(ActionName._10Z轴到取料位);
            lstAction.Add(ActionName._10Z轴到位吸真空);
            lstAction.Add(ActionName._10吸笔真空检测);
            lstAction.Add(ActionName._10Z轴到安全位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10X轴到下相机拍照位);
            lstAction.Add(ActionName._10X轴到位完成);
            lstAction.Add(ActionName._50动作完成);
            //放料
            lstAction.Add(ActionName._10Z轴到安全位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10X轴到取料位);
            lstAction.Add(ActionName._10X轴到位完成);
            lstAction.Add(ActionName._10Z轴到取料位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10吸笔破真空);
            lstAction.Add(ActionName._10Z轴到安全位);
            lstAction.Add(ActionName._10Z轴到位);
            lstAction.Add(ActionName._10X轴到下相机拍照位);
            lstAction.Add(ActionName._10X轴到位完成);
            lstAction.Add(ActionName._50动作完成);
        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
                if (action.ToString().Contains("_20"))
                {
                    camModule.currentPoint = ModelManager.SuctionRParam.getCameraCoordinateByIndex(iTray, pos);
                    camModule.Action(action, ref step);

                }
                else if (action.ToString().Contains("_10"))
                    base.Action(action, ref step);
                else
                {
                    switch (action)
                    {
                        case ActionName._50动作选择:
                            if (strType.Equals("取料"))
                                step = step + 1;
                            else
                                //放料
                                step = lstAction.IndexOf(ActionName._50动作完成) + 1;
                            break;
                        case ActionName._50动作完成:
                            IStep = 0;
                            Run.runMode = RunMode.手动;
                            break;
                    }

                }
            }
            catch (Exception)
            {


            }

        }

        public override void Action2()
        {

        }

        public override void Reset()
        {

        }

    }
}
