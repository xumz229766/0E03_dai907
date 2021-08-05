using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion;
using System.IO;
using System.Runtime.Remoting.Messaging;
namespace Assembly
{
    public class TestFlash : ActionModule
    {
        private string strOut = "TestFlash-Action-";
        public static int iStation = 1;//测试工位

        public static AXIS axis;//测试轴
        public static AXIS axisZ;//测试Z轴
        public static double dSafeZ = 0;
        public static List<double> lstPos = new List<double>();//测试点位
        public static List<Point> lstPoint = new List<Point>();//定点拍照存放点位
        public static List<Point> lstPoint1 = new List<Point>();//飞拍点位
        public static double dStartPos = 0;//飞拍起始点位
        public static double dEndPos = 0;//飞拍结束点位
        public static double dMakeup = 0;//补偿值
        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static int iCurrentPos = 0;
        public static DO doCam;
        public static ushort channel = 0;//触发通道
        public static int iPicNum = 1;//拍照顺序
        public static int iPicSum = 0;//拍照总数,飞拍之前赋值确认
        public static bool bFlash = false;//是否为飞拍
        public static bool bNotTestFlash = false;//只有定拍
        public TestFlash()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._70Z轴到安全位);
            lstAction.Add(ActionName._70Z轴到位完成);
            lstAction.Add(ActionName._70XY轴到拍照位);
            lstAction.Add(ActionName._70XY轴到位完成);
            lstAction.Add(ActionName._70上相机拍照);
            lstAction.Add(ActionName._70上相机拍照完成);

            lstAction.Add(ActionName._70X到定位拍照位);
            lstAction.Add(ActionName._70X到位完成);
            lstAction.Add(ActionName._70相机拍照);
            lstAction.Add(ActionName._70相机拍照完成);

            lstAction.Add(ActionName._70X到飞拍起始位);
            lstAction.Add(ActionName._70X到位完成);
            lstAction.Add(ActionName._70Z轴到安全位);
            lstAction.Add(ActionName._70Z轴到位完成);
            lstAction.Add(ActionName._70开始飞拍);
            lstAction.Add(ActionName._70X到飞拍结束位);
            lstAction.Add(ActionName._70X到位完成);
            lstAction.Add(ActionName._70Z轴到原点位);
            lstAction.Add(ActionName._70Z轴到位完成);
            lstAction.Add(ActionName._70飞拍结束);



        }

        public override void Reset()
        {

        }
        public override void Action2()
        {

        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                {
                    case ActionName._70Z轴到安全位:
                        iPicNum = 1;
                        iPicSum = lstPos.Count;
                        dDestPosZ = dSafeZ;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._70Z轴到原点位:
                        dDestPosZ = 0;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._70Z轴到位完成:
                        if (IsAxisINP2(dDestPosZ, axisZ))
                        {
                            if (axis == AXIS.取料X1轴)
                            {
                                OptPutSuction1(true);
                            }
                            else if (axis == AXIS.取料X2轴)
                            {
                                OptPutSuction2(true);
                            }
                            else if (axis == AXIS.组装X1轴)
                            {
                                AssemblePutSuction1(true);
                            }
                            else if (axis == AXIS.组装X2轴)
                            {
                                AssemblePutSuction2(true);
                            }

                            WriteOutputInfo(strOut + "Z轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._70X到定位拍照位:
                        dDestPosX = lstPos[iCurrentPos];
                        mc.AbsMove(axis, dDestPosX, (int)200);
                        WriteOutputInfo(strOut + "X到定点拍照位" + iCurrentPos.ToString());
                        if (axis == AXIS.取料X1轴)
                        {
                            string strProcessName = CommonSet.dic_OptSuction1[iCurrentPos + 1].strPicDownProduct;
                            CommonSet.dic_OptSuction1[iCurrentPos + 1].InitImageDown(strProcessName);

                        }
                        else if (axis == AXIS.取料X2轴)
                        {
                            string strProcessName = CommonSet.dic_OptSuction2[iCurrentPos + 10].strPicDownProduct;
                            CommonSet.dic_OptSuction2[iCurrentPos + 10].InitImageDown(strProcessName);
                        }
                        else if (axis == AXIS.组装X1轴)
                        {
                            string strProcessName = CommonSet.dic_Assemble1[iCurrentPos + 1].strPicDownProduct;
                            CommonSet.dic_Assemble1[iCurrentPos + 1].InitImageDown(strProcessName);
                        }
                        else if (axis == AXIS.组装X2轴)
                        {
                            string strProcessName = CommonSet.dic_Assemble2[iCurrentPos + 10].strPicDownProduct;
                            CommonSet.dic_Assemble2[iCurrentPos + 10].InitImageDown(strProcessName);

                        }
                        step = step + 1;

                        break;

                    case ActionName._70X到飞拍结束位:
                        dDestPosX = dEndPos;
                        mc.AbsMove(axis, dDestPosX, (int)CommonSet.dVelFlashX);
                        WriteOutputInfo(strOut + "X到飞拍结束位");
                        step = step + 1;
                        break;

                    case ActionName._70X到飞拍起始位:
                        dDestPosX = dStartPos;
                        mc.AbsMove(axis, dDestPosX, (int)CommonSet.dVelRunX);
                        WriteOutputInfo(strOut + "X到飞拍起始位");
                        step = step + 1;
                        break;

                    case ActionName._70X到位完成:
                        if (IsAxisINP(dDestPosX, axis, 0.02))
                        {
                            if (sw.WaitSetTime(500))
                            {
                                WriteOutputInfo(strOut + "X到位完成");
                                step = step + 1;
                            }
                        }
                        break;
                    case ActionName._70XY轴到拍照位:
                        if (axis == AXIS.组装X1轴)
                        {
                            dDestPosX = AssembleSuction1.pCamBarrelPos1.X;
                            dDestPosY = AssembleSuction1.pCamBarrelPos1.Y;
                            mc.AbsMove(AXIS.组装X1轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y1轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());

                            string strProcessName = AssembleSuction1.strPicCameraUpCheckName;
                            AssembleSuction1.InitImageUp(strProcessName);

                            step = step + 1;
                        }
                        else if (axis == AXIS.组装X2轴)
                        {
                            dDestPosX = AssembleSuction2.pCamBarrelPos2.X;
                            dDestPosY = AssembleSuction2.pCamBarrelPos2.Y;
                            mc.AbsMove(AXIS.组装X2轴, dDestPosX, (int)CommonSet.dVelRunX);
                            mc.AbsMove(AXIS.组装Y2轴, dDestPosY, (int)CommonSet.dVelRunY);
                            WriteOutputInfo(strOut + "XY组装拍照位(X,Y):" + dDestPosX.ToString() + "," + dDestPosY.ToString());
                            string strProcessName = AssembleSuction2.strPicCameraUpCheckName;
                            AssembleSuction2.InitImageUp(strProcessName);
                            step = step + 1;

                        }
                        else
                        {
                            step = step + 1;
                        }

                        break;

                    case ActionName._70XY轴到位完成:
                        if (axis == AXIS.组装X1轴)
                        {
                            if (IsAxisINP(dDestPosX, axis, 0.02) && IsAxisINP(dDestPosY, AXIS.组装Y1轴, 0.02))
                            {
                                if (sw.WaitSetTime(500))
                                {
                                    WriteOutputInfo(strOut + "X1Y1到位完成");
                                    step = step + 1;
                                }
                            }
                        }
                        else if (axis == AXIS.组装X2轴)
                        {
                            if (IsAxisINP(dDestPosX, axis, 0.02) && IsAxisINP(dDestPosY, AXIS.组装Y2轴, 0.02))
                            {
                                if (sw.WaitSetTime(500))
                                {
                                    WriteOutputInfo(strOut + "X2Y2到位完成");
                                    step = step + 1;
                                }
                            }

                        }
                        else
                        {
                            step = step + 1;
                        }
                        break;
                    case ActionName._70上相机拍照:
                        if (axis == AXIS.组装X1轴)
                        {
                            mc.setDO(DO.组装上相机1, true);
                            if (sw.WaitSetTime(50))
                            {
                                mc.setDO(DO.组装上相机1, false);
                                WriteOutputInfo(strOut + "相机拍照");
                                step = step + 1;
                            }
                        }
                        else if (axis == AXIS.组装X2轴)
                        {
                            mc.setDO(DO.组装上相机2, true);
                            if (sw.WaitSetTime(50))
                            {
                                mc.setDO(DO.组装上相机2, false);
                                WriteOutputInfo(strOut + "相机拍照");
                                step = step + 1;
                            }
                        }
                        else
                        {
                            step = step + 1;
                        }
                        break;
                    case ActionName._70上相机拍照完成:
                        if (sw.WaitSetTime(200))
                        {
                            step = step + 1;
                            WriteOutputInfo(strOut + "上相机拍照完成" + iCurrentPos.ToString());
                        }
                        break;
                    case ActionName._70相机拍照:
                        mc.setDO(doCam, true);
                        if (sw.WaitSetTime(50))
                        {
                            mc.setDO(doCam, false);
                            WriteOutputInfo(strOut + "相机拍照");
                            step = step + 1;
                        }
                        break;

                    case ActionName._70相机拍照完成:
                        if (iCurrentPos < lstPos.Count - 1)
                        {
                            iCurrentPos++;
                            step = lstAction.IndexOf(ActionName._70X到定位拍照位);
                        }
                        else
                        {
                            if (sw.WaitSetTime(200))
                            {
                                if (bNotTestFlash)
                                {
                                    TestFlash.bFlash = false;
                                    iCurrentPos = 0;
                                    step = 0;

                                }
                                else
                                {
                                    step = step + 1;
                                }
                                string strHead = axis.ToString() + ",";
                                if (axis == AXIS.取料X1轴)
                                {
                                    foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                                    {
                                        strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                        strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                    }
                                    strHead += "0.000,0.000,";
                                }
                                else if (axis == AXIS.取料X2轴)
                                {
                                    foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                                    {
                                        strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                        strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                    }
                                    strHead += "0.000,0.000,";
                                }
                                else if (axis == AXIS.组装X1轴)
                                {
                                    foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
                                    {
                                        strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                        strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                    }
                                    strHead += AssembleSuction1.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000") + ",";
                                }
                                else if (axis == AXIS.组装X2轴)
                                {
                                    foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                                    {
                                        strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                        strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                    }
                                    strHead += AssembleSuction2.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000") + ",";
                                }

                                strHead = strHead.Remove(strHead.LastIndexOf(','));
                                WriteResult(CommonSet.strReportPath + "定拍\\", strHead);


                                WriteOutputInfo(strOut + "相机拍照完成" + iCurrentPos.ToString());
                            }
                        }



                        break;
                    case ActionName._70开始飞拍:
                        TestFlash.bFlash = true;
                        CommonSet.iCameraDownFlag = 1;
                        List<double> lstTestPos = new List<double>();
                        int axisChannel = 0;
                        if (axis == AXIS.取料X1轴)
                        {
                            foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                            {
                                //if (pair.Value.BUse && (pair.Key < 10))
                                //{

                                int index = pair.Value.SuctionOrder;
                                lstTestPos.Add(lstPos[index - 1] + dMakeup);


                                string strProcessName = pair.Value.strPicDownProduct;
                                pair.Value.InitImageDown(strProcessName);
                                //}
                            }
                            axisChannel = 0;
                        }
                        else if (axis == AXIS.取料X2轴)
                        {
                            foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                            {
                                //if (pair.Value.BUse )
                                //{

                                int index = pair.Value.SuctionOrder;
                                lstTestPos.Add(lstPos[index - 10] + dMakeup);


                                string strProcessName = pair.Value.strPicDownProduct;
                                pair.Value.InitImageDown(strProcessName);
                                //}
                            }
                            axisChannel = 1;
                        }
                        else if (axis == AXIS.组装X1轴)
                        {
                            foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
                            {
                                //if (pair.Value.BUse)
                                //{

                                int index = pair.Value.SuctionOrder;
                                lstTestPos.Add(lstPos[index - 1] + dMakeup);


                                string strProcessName = pair.Value.strPicDownProduct;
                                pair.Value.InitImageDown(strProcessName);
                                //}
                            }

                            axisChannel = 0;
                        }
                        else if (axis == AXIS.组装X2轴)
                        {
                            foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                            {
                                //if (pair.Value.BUse)
                                //{

                                int index = pair.Value.SuctionOrder;
                                lstTestPos.Add(lstPos[index - 10] + dMakeup);


                                string strProcessName = pair.Value.strPicDownProduct;
                                pair.Value.InitImageDown(strProcessName);
                                // }
                            }
                            axisChannel = 1;
                        }

                        mc.startCmpTrigger(axis, lstTestPos.ToArray(), (ushort)channel);
                        WriteOutputInfo(strOut + "X轴开始飞拍");
                        iPicNum = 1;
                        iPicSum = lstPos.Count;
                        step = step + 1;
                        break;


                    case ActionName._70飞拍结束:
                        mc.stopCmpTrigger(axis, (ushort)0);

                        //step = lstAction.IndexOf(ActionName._70X到飞拍起始位);
                        if (sw.WaitSetTime(200))
                        {

                            string strHead = axis.ToString() + ",";
                            if (axis == AXIS.取料X1轴)
                            {
                                foreach (KeyValuePair<int, OptSution1> pair in CommonSet.dic_OptSuction1)
                                {
                                    strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                    strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                }
                                strHead += "0.000,0.000,";
                            }
                            else if (axis == AXIS.取料X2轴)
                            {
                                foreach (KeyValuePair<int, OptSution2> pair in CommonSet.dic_OptSuction2)
                                {
                                    strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                    strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                }
                                strHead += "0.000,0.000,";
                            }
                            else if (axis == AXIS.组装X1轴)
                            {
                                foreach (KeyValuePair<int, AssembleSuction1> pair in CommonSet.dic_Assemble1)
                                {
                                    strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                    strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                }
                                // strHead += AssembleSuction1.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction1.imgResultUp.CenterColumn.ToString("0.000") + ",";
                                strHead += "0.000,0.000,";
                            }
                            else if (axis == AXIS.组装X2轴)
                            {
                                foreach (KeyValuePair<int, AssembleSuction2> pair in CommonSet.dic_Assemble2)
                                {
                                    strHead += pair.Value.imgResultDown.CenterRow.ToString("0.000") + ",";
                                    strHead += pair.Value.imgResultDown.CenterColumn.ToString("0.000") + ",";
                                }
                                strHead += "0.000,0.000,";
                                // strHead += AssembleSuction2.imgResultUp.CenterRow.ToString("0.000") + "," + AssembleSuction2.imgResultUp.CenterColumn.ToString("0.000") + ",";
                            }

                            strHead = strHead.Remove(strHead.LastIndexOf(','));
                            WriteResult(CommonSet.strReportPath + "飞拍\\", strHead);
                            iCurrentPos = 0;
                            step = 0;

                        }

                        break;
                }

            }
            catch (Exception ex)
            {

                WriteOutputInfo(strOut + "飞拍测试异常" + ex.ToString());
            }




        }
        private void WriteFile(string strPath, string strContent)
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
                    string strHead = "时间,测试轴,";
                    for (int i = 1; i < 10; i++)
                    {

                        strHead += "S" + i.ToString() + "_Row," + "S" + i.ToString() + "_Col,";
                    }
                    strHead += "B_Row,B_Col,";
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
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "," + strContent);
                sw.Close();
            }
            catch (Exception)
            {


            }

        }

        public void WriteResult(string strPath, string strContent)
        {
            Action<string, string> dele = WriteFile;
            dele.BeginInvoke(strPath, strContent, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string, string> dele = (Action<string, string>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }


    }
}
