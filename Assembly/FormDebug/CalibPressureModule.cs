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
    public class CalibPressureModule:ActionModule
    {
        private string strOut = "CalibPressureModule-Action-";

        private double dDestPosX = 0;
        private double dDestPosY = 0;
        private double dDestPosZ = 0;
        public static Point pTest = new Point();//测试点位
        public static double dDistX = 0;//吸笔的距离
        public static double dPosZ = 0;//Z轴初始位
        public static AXIS axisX, axisY, axisZ;
        public static bool bTestZPressure = false;//测试往返压力重复精度
        public static int iTestZTimes = 0;//测试次数
        public static int iTestZTotal = 1;//测试总次数
        public static int iCurrentSuction = 1;//当前吸笔序号
        public static int iStartSuction = 1;
        public static int iEndSuction = 1;
        public static int iStation = 1;//测试工位
        public static int iTimes = 0;
        public static double[] TestV = new double[]{0.6,0.8,1.0,1.2, 1.4,1.6,1.8,2.0,2.2,2.4,2.6,2.8,3.0,3.2,3.4,3.6,3.8,4.0};
        public CalibPressureModule()
        {
            lstAction.Clear();
            lstAction.Add(ActionName._80Z轴到安全位);
            lstAction.Add(ActionName._80Z轴到位完成);
            lstAction.Add(ActionName._80所有吸笔上升);
            lstAction.Add(ActionName._80XY到测试位);
            lstAction.Add(ActionName._80XY到位完成);
            lstAction.Add(ActionName._80吸笔下降);
            lstAction.Add(ActionName._80Z轴到下压位);
            lstAction.Add(ActionName._80Z轴到位完成);
            lstAction.Add(ActionName._80数据读取);
            lstAction.Add(ActionName._80电压设定);
            lstAction.Add(ActionName._80压力读取);
            lstAction.Add(ActionName._80测压完成);

            
        }



        public override void Reset()
        {
           
        }

        public override void Action(ActionName action, ref int step)
        {
            try
            {
               
                switch (action)
                {

                    case ActionName._80XY到测试位:
                        if (axisX == AXIS.组装X1轴)
                        {
                            if (!CommonSet.dic_OptSuction1[iCurrentSuction].BUse)
                            {
                                step = lstAction.IndexOf(ActionName._80测压完成);
                                break;
                            }

                        }
                        else
                        {
                            if (!CommonSet.dic_OptSuction2[iCurrentSuction].BUse)
                            {
                                step = lstAction.IndexOf(ActionName._80测压完成);
                                break;
                            }
                        }
                        dDestPosX = pTest.X + (iCurrentSuction - iStartSuction) * dDistX;
                        dDestPosY = pTest.Y;
                        mc.AbsMove(axisX, dDestPosX, (int)50);
                        mc.AbsMove(axisY, dDestPosY, (int)50);
                        WriteOutputInfo(strOut + "组装吸笔");
                        step = step + 1;
                        break;
                    case ActionName._80XY到位完成:
                        if (IsAxisINP(dDestPosX, axisX) && IsAxisINP(dDestPosY, axisY))
                        {
                            WriteOutputInfo(strOut + "XY轴到位");
                            step = step + 1;
                        }
                        break;

                  
                    case ActionName._80所有吸笔上升:
                        if (axisX == AXIS.组装X1轴)
                        {
                            mc.WriteOutDA((float)2, (ushort)0);

                        }
                        else
                        {
                            mc.WriteOutDA((float)2, (ushort)2);
                        }
                        AssemblePutSuction1(false);
                        AssemblePutSuction2(false);
                        if (sw.WaitSetTime(500))
                        {
                            WriteOutputInfo(strOut + "所有吸笔上升");
                            step = step + 1;
                        }
                        break;
                    case ActionName._80吸笔下降:
                        string strDo = "组装气缸下降" + iCurrentSuction.ToString();
                        DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                        mc.setDO(dOut, true);
                        WriteOutputInfo(strOut + strDo);
                        step = step + 1;
                        break;
                    case ActionName._80Z轴到位完成:
                        if (IsAxisINP(dDestPosZ, axisZ))
                        {
                            WriteOutputInfo(strOut + "组装Z轴到位");
                            step = step + 1;
                        }
                        break;
                    case ActionName._80Z轴到安全位:
                        dDestPosZ = 0;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "组装Z轴到安全位");
                        step = step + 1;
                        break;
                    case ActionName._80Z轴到下压位:
                        dDestPosZ = dPosZ;
                        mc.AbsMove(axisZ, dDestPosZ, (int)50);
                        WriteOutputInfo(strOut + "Z轴到下压位");
                        step = step + 1;
                        break;
                    case ActionName._80数据读取:
                        if (!bTestZPressure)
                        {
                            step = step + 1;
                            break;
                        }
                        if (sw.WaitSetTime(5000))
                        {
                            //如果是组装工位1
                            if (iStation == 1)
                            {
                                if (axisX == AXIS.组装X1轴)
                                {
                                    double p = SerialPortMeasurePressure.dStation1;
                                    double h = SerialPortMeasureHeight.dStation1;
                                    double Z = mc.dic_Axis[AXIS.组装Z1轴].dPos;
                                    WriteResult(CommonSet.strReportPath + "Z轴重复测试\\", p, h, Z);
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation1.ToString());
                                }
                                else if (axisX == AXIS.组装X2轴)
                                {
                                    double p = SerialPortMeasurePressure.dStation1;
                                    double h = SerialPortMeasureHeight.dStation1;
                                    double Z = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                                    WriteResult(CommonSet.strReportPath + "Z轴重复测试\\", p, h, Z);
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation1.ToString());
                                }
                            }
                            else
                            {
                                if (axisX == AXIS.组装X1轴)
                                {
                                    double p = SerialPortMeasurePressure.dStation2;
                                    double h = SerialPortMeasureHeight.dStation2;
                                    double Z = mc.dic_Axis[AXIS.组装Z1轴].dPos;
                                    WriteResult(CommonSet.strReportPath + "Z轴重复测试\\", p, h, Z);
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation2.ToString());
                                }
                                else if (axisX == AXIS.组装X2轴)
                                {
                                    double p = SerialPortMeasurePressure.dStation2;
                                    double h = SerialPortMeasureHeight.dStation2;
                                    double Z = mc.dic_Axis[AXIS.组装Z2轴].dPos;
                                    WriteResult(CommonSet.strReportPath + "Z轴重复测试\\", p, h, Z);
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation2.ToString());
                                }
                            }
                            iTestZTimes = iTestZTimes + 1;
                            if (iTestZTimes >= iTestZTotal)
                            {
                                step = step + 1;
                            }
                            else
                            {
                                step = 0;
                            }

                        }

                        break;
                    case ActionName._80电压设定:
                        if (bTestZPressure)
                        {
                            step = step + 1;
                            break;
                        }
                        if (axisX == AXIS.组装X1轴)
                        {
                            mc.WriteOutDA((float)TestV[iTimes], (ushort)0);

                        }
                        else
                        {
                            mc.WriteOutDA((float)TestV[iTimes], (ushort)2);
                        }
                        WriteOutputInfo(strOut + "当前电压设定"+TestV[iTimes].ToString());
                        step = step + 1;
                        break;

                    case ActionName._80压力读取:
                        if (bTestZPressure)
                        {
                            step = step + 1;
                            break;
                        }
                        if (sw.WaitSetTime(1000))
                        {
                            //如果是组装工位1
                            if (iStation == 1)
                            {
                                if (axisX == AXIS.组装X1轴)
                                {
                                    AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction][iTimes].Pressure = SerialPortMeasurePressure.dStation1;
                                    AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction][iTimes].V = TestV[iTimes];
                                    iTimes++;
                                    if (iTimes < TestV.Length)
                                    {
                                        step = step - 1;
                                    }
                                    else
                                    {
                                        step = step + 1;
                                    }
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation1.ToString());
                                }
                                else if (axisX == AXIS.组装X2轴)
                                {
                                    AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction][iTimes].Pressure = SerialPortMeasurePressure.dStation1;
                                    AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction][iTimes].V = TestV[iTimes];
                                    iTimes++;
                                    if (iTimes < TestV.Length)
                                    {
                                        step = step - 1;
                                    }
                                    else
                                    {
                                        step = step + 1;
                                    }
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation1.ToString());
                                }
                            }
                            else {
                                if (axisX == AXIS.组装X1轴)
                                {
                                    AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction][iTimes].Pressure = SerialPortMeasurePressure.dStation2;
                                    AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction][iTimes].V = TestV[iTimes];
                                    iTimes++;
                                    if (iTimes < TestV.Length)
                                    {
                                        step = step - 1;
                                    }
                                    else
                                    {
                                        step = step + 1;
                                    }
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation2.ToString());
                                }
                                else if (axisX == AXIS.组装X2轴)
                                {
                                    AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction][iTimes].Pressure = SerialPortMeasurePressure.dStation2;
                                    AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction][iTimes].V = TestV[iTimes];
                                    iTimes++;
                                    if (iTimes < TestV.Length)
                                    {
                                        step = step - 1;
                                    }
                                    else
                                    {
                                        step = step + 1;
                                    }
                                    WriteOutputInfo(strOut + "压力读取" + SerialPortMeasurePressure.dStation2.ToString());
                                }
                            }
                            
                        }

                        break;
                    case ActionName._80测压完成:
                        iCurrentSuction++;
                        iTimes = 0;
                        iTestZTimes = 0;
                        if (iCurrentSuction > iEndSuction)
                        {
                            WriteOutputInfo(strOut + "测压完成" + dDestPosZ.ToString());
                            Run.runMode = RunMode.手动;
                            dDestPosZ = 0;
                            mc.AbsMove(axisZ, dDestPosZ, (int)50);
                            break;
                        }
                        step = step + 1;

                        break;


                }
            }
            catch (Exception ex)
            {
                WriteOutputInfo(strOut + "测高标定模块运行异常" + ToString());

            }
        }

        public override void Action2()
        {
            
        }
        private void WriteFile(string strPath, double p, double h, double z)
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
                    string strHead = "时间,吸笔,压力值,测高值,Z轴坐标,";
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
                string strContent = DateTime.Now.ToString("HH:mm:ss") + "," + iCurrentSuction.ToString() + ",";

                strContent += p.ToString() + "," + h.ToString() + "," + z.ToString() + ",";

                strContent = strContent.Remove(strContent.LastIndexOf(","));
                StreamWriter sw = new StreamWriter(file, true, Encoding.UTF8);
                sw.WriteLine(strContent);
                sw.Close();
            }
            catch (Exception)
            {
            }

        }

        public void WriteResult(string strPath, double p, double h, double z)
        {
            Action<string, double, double, double> dele = WriteFile;
            dele.BeginInvoke(strPath, p, h, z, WriteCallBack, null);
        }
        private void WriteCallBack(IAsyncResult _result)
        {
            AsyncResult result = (AsyncResult)_result;
            Action<string, double, double, double> dele = (Action<string, double, double, double>)result.AsyncDelegate;
            dele.EndInvoke(result);
        }
    }
}
