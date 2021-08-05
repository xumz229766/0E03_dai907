using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigureFile;
using APS168_W32;
using APS_Define_W32;
using log4net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
namespace Motion
{
    public class Adlink7856 : MotionCard
    {
        //private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        private Int32 errorCode = 0;//错误代码
        private Thread t_Update = null;
        bool bMInit = false;
        bool bResetAxisAlarm = false;//复位驱动器报警
        private int[] iSetDO = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };//设置的输出值,数组长度要大于IO板的张数

        public Int32 iErrorCode
        {
            get { return errorCode; }
            set
            {

                errorCode = value;
                // ErrorMessage(errorCode);
            }
        }


        Int16 CardID = 0;	      //Card number for setting.
        Int16 HBusNo = 0;         //HSL BusNo is 0
        Int16 MBusNo = 1;        //Bus number for setting,  Motion Net BusNo is 1.
        Int32 MOD_No = 0;		  //Arrcoding switch On Module. 	
        Int32 Start_Axis_ID = 1500;  //First Axis number in Motion Net bus.
        private static Int16 HIONum = 4;//IO板总数
        private static Int16 HDI16HDO16 = 4;//16进16出的卡的个数
        private static Int16 MCardNum = 2;//轴卡个数

        private Stopwatch sw = new Stopwatch();

        internal Adlink7856()
        {
            bMInit = false;
            //添加输入
            string[] values = Enum.GetNames(typeof(DI));
            foreach (string value in values)
            {
                DI e = (DI)Enum.Parse(typeof(DI), value);
                dic_DI.Add(e, false);
            }
            //添加输出
            values = Enum.GetNames(typeof(DO));
            foreach (string value in values)
            {
                DO e = (DO)Enum.Parse(typeof(DO), value);
                dic_DO.Add(e, false);
            }
            //添加轴
            values = Enum.GetNames(typeof(AXIS));
            foreach (string value in values)
            {
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                AXStatus ax = new AXStatus();
                dic_Axis.Add(axis, ax);
                if (!dic_HomeStatus.ContainsKey(axis))
                    dic_HomeStatus.Add(axis, false);
                if (!dic_HomeZ.ContainsKey(axis))
                {
                    dic_HomeZ.Add(axis, false);
                }
            }
            //设定轴的比率
            //dic_AxisRatio.Add(AXIS.X1轴, 1000);
            //dic_AxisRatio.Add(AXIS.X2轴, 1000);
            //dic_AxisRatio.Add(AXIS.Y1轴, 1000);
            //dic_AxisRatio.Add(AXIS.Y2轴, 1000);
            //dic_AxisRatio.Add(AXIS.Z1轴, 1000);
            //dic_AxisRatio.Add(AXIS.Z2轴, 1000);
            //dic_AxisRatio.Add(AXIS.C轴, 10);


            t_Update = new Thread(updateStatus);
            t_Update.Priority = ThreadPriority.Highest;
            t_Update.Start();
        }

        //处理错误信息
        private void ErrorMessage(Int32 error)
        {
            if (error != 0)
                writeInfo("执行出错，错误代码为:" + error.ToString());
        }
        /// <summary>
        /// 初始化运动控制卡
        /// </summary>
        /// <param name="strXmlFile">参数文件</param>
        public override void initCard(string strXmlFile)
        {
            if (bMInit)
            {
                return;
            }
            Int32 DPAC_ID_Bits = 0;
            Int32 Info = 0;

            if (APS168.APS_initial(ref DPAC_ID_Bits, 0) == 0)
            {
                try
                {
                    iErrorCode += APS168.APS_get_device_info(CardID, 0x10, ref Info);
                    writeInfo("驱动版本：" + Info.ToString());
                    writeInfo("DLL版本：" + APS168.APS_version().ToString());
                    APS168.APS_get_device_info(CardID, 0x20, ref Info);
                    writeInfo("CPLD版本：" + Info.ToString());
                    //设置传输速度
                    iErrorCode += APS168.APS_set_field_bus_param(CardID, MBusNo, (Int32)APS_Define.PRF_TRANSFER_RATE, 2);
                    iErrorCode += APS168.APS_set_field_bus_param(CardID, HBusNo, (Int32)APS_Define.PRF_TRANSFER_RATE, 2);
                    //启动总线
                    iErrorCode += APS168.APS_start_field_bus(CardID, MBusNo, Start_Axis_ID);
                    iErrorCode += APS168.APS_start_field_bus(CardID, HBusNo, 0);
                    //加载轴参数
                    iErrorCode += APS168.APS_load_param_from_file(strXmlFile);

                    if (iErrorCode == 0)
                    {
                        writeInfo("初始化完成！");
                        bMInit = true;
                    }
                    else
                    {
                        writeInfo("初始化失败!");
                        closeCard();

                    }
                    iErrorCode = 0;
                }
                catch (Exception ex)
                {

                    writeDebug("初始化异常!", ex);
                    closeCard();
                }
            }
            else
            {
                APS168.APS_close();
                writeInfo("初始化板卡失败!");
                bMInit = false;
            }

        }
        public override float ReadOutAD(ushort subindex)
        {
            return 0;
        }
        public override void WriteOutDA(float value, ushort subindex)
        {

        }

        /// <summary>
        /// 电机使能
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="status">状态,0代表关,1代表开</param>
        public override void SeverOn(AXIS axis, int status)
        {
            iErrorCode += APS168.APS_set_servo_on((int)axis, status);
        }
        public override void ResetAxisAlarm()
        {
            bool bFlag = false;
            int count = dic_Axis.Count;
            for (int i = 0; i < count; i++)
            {

                // bFlag = bFlag || dic_Axis[AXIS.Z1轴 + i].ALM;
            }
            //foreach (KeyValuePair<AXIS, AXStatus> pair in dic_Axis)
            //{
            //    bFlag = bFlag || pair.Value.ALM;
            //}
            bResetAxisAlarm = bFlag;

        }

        private void ResetAllAxisAlarm()
        {
            if (bResetAxisAlarm)
            {
                int i1 = 1;
                int i2 = 1;
                int i3 = 1;
                APS168.APS_get_field_bus_d_output(CardID, MBusNo, 1, ref i1);
                APS168.APS_get_field_bus_d_output(CardID, MBusNo, 2, ref i2);
                // APS168.APS_get_field_bus_d_output(CardID, MBusNo, 3, ref i3);
                if ((i1 == 0) && (i2 == 0))
                {
                    bResetAxisAlarm = false;
                }

                APS168.APS_set_field_bus_d_output(CardID, MBusNo, 1, 0x0);
                APS168.APS_set_field_bus_d_output(CardID, MBusNo, 2, 0x0);
                // APS168.APS_set_field_bus_d_output(CardID, MBusNo, 3, 0x0);
            }
            else
            {
                bool bFlag = false;
                int count = dic_Axis.Count;
                for (int i = 0; i < count; i++)
                {

                    // bFlag = bFlag || dic_Axis[AXIS.Z1轴 + i].ALM;
                }
                if (bFlag)
                    return;

                //int i1 = 1;
                //int i2 = 1;
                //int i3 = 1;
                //APS168.APS_get_field_bus_d_output(CardID, MBusNo, 1, ref i1);
                //APS168.APS_get_field_bus_d_output(CardID, MBusNo, 2, ref i2);
                //APS168.APS_get_field_bus_d_output(CardID, MBusNo, 3, ref i3);



                APS168.APS_set_field_bus_d_output(CardID, MBusNo, 1, 0xff);
                APS168.APS_set_field_bus_d_output(CardID, MBusNo, 2, 0xff);
                // APS168.APS_set_field_bus_d_output(CardID, MBusNo, 3, 0xff);
            }

        }
        public override void SeverOnAll()
        {
            string[] axisNames = Enum.GetNames(typeof(AXIS));
            foreach (string axis in axisNames)
            {
                AXIS a = (AXIS)(Enum.Parse(typeof(AXIS), axis));
                APS168.APS_set_servo_on((int)a, 1);
            }
        }
        public override void SeverOffAll()
        {
            string[] axisNames = Enum.GetNames(typeof(AXIS));
            foreach (string axis in axisNames)
            {
                AXIS a = (AXIS)(Enum.Parse(typeof(AXIS), axis));
                APS168.APS_set_servo_on((int)a, 0);
            }
        }
        /// <summary>
        /// 绝对运动，轴以指定最大速度运行
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        public override void AbsMove(AXIS axis, int pos, int vel)
        {
            iErrorCode += APS168.APS_absolute_move((int)axis, pos, (int)(vel * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        /// <summary>
        /// 绝对运动，轴以指定最大速度运行
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        public override void AbsMove(AXIS axis, double pos, int vel)
        {
            int iPulse = (int)(pos * dic_AxisRatio[axis]);
            iErrorCode += APS168.APS_absolute_move((int)axis, iPulse, (int)(vel * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        public override void AbsMove(AXIS axis, double pos, double vel)
        {
            int iPulse = (int)(pos * dic_AxisRatio[axis]);
            iErrorCode += APS168.APS_absolute_move((int)axis, iPulse, (int)(vel * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        public override void AbsMove(AXIS axis, double pos, double vel, double acc = 0.1, double dec = 0.1)
        {
            
        }
        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="len">运动距离</param>
        /// <param name="vel">最大运行速度</param>
        public override void RelativeMove(AXIS axis, int len, double vel)
        {
            iErrorCode += APS168.APS_relative_move((int)axis, len, (int)(vel * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        /// <summary>
        /// 速度运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">最大运行速度</param>
        public override void VelMove(AXIS axis, int vel)
        {
            iErrorCode += APS168.APS_velocity_move((int)axis, (int)(vel * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        /// <summary>
        /// 停止运动
        /// </summary>
        /// <param name="axis">轴号</param>
        public override void StopAxis(AXIS axis)
        {
            iErrorCode += APS168.APS_stop_move((int)axis);
            iErrorCode = 0;
        }
        public override void StopAllAxis()
        {
            Array arr = Enum.GetValues(typeof(AXIS));
            foreach (AXIS axis in arr)
            {
                StopAxis(axis);
            }
        }
        public override void HomeAbs(AXIS axis)
        {

        }
        /// <summary>
        /// 回原点
        /// </summary>
        /// <param name="axis">轴号</param>

        public override void Home(AXIS axis)
        {
            dic_HomeStatus[axis] = false;
            iErrorCode = APS168.APS_home_move((int)axis);
            if (iErrorCode != 0)
            {
                writeInfo(axis.ToString() + "回原点执行异常,错误代码:" + iErrorCode.ToString());
            }
            if (iErrorCode == 0)
            {
                Action<AXIS> delegateHome = HomeHandle;
                delegateHome.BeginInvoke(axis, HomeCallBack, axis);

            }
            iErrorCode = 0;
        }
        private void HomeHandle(AXIS axis)
        {
            int iStatus = APS168.APS_motion_status((int)axis);
            uint a = 1;
            dic_HomeStatus[axis] = ((iStatus & (a << 6)) == 0);

        }
        private void HomeCallBack(IAsyncResult result)
        {
            AsyncResult _Result = (AsyncResult)result;
            Action<AXIS> delegateObj = (Action<AXIS>)_Result.AsyncDelegate;
            AXIS _axis = (AXIS)_Result.AsyncState;
            delegateObj.EndInvoke(_Result);
            if (!dic_HomeStatus[_axis])
            {
                delegateObj.BeginInvoke(_axis, HomeCallBack, _axis);
            }

        }
        /// <summary>
        /// 启动比较触发
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="data">数据列表</param>
        public override void startCmpTrigger(AXIS axis, int[] data)
        {
            int index = (int)axis - 1504;
            int ModeNum = 2;//模块的拨码号
            int len = data.Length;

            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_CMP0_SRC + index, 1);//Position Counter
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_CMP0_TYPE + index, 0);//Table
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_TRG0_PWD + index, 1000);//Trigger Pulse width
            int outputC = (int)Math.Pow(2, index);
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_TRG0_SRC + index, outputC);
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_TRG0_CFG + index, 0);
            iErrorCode += APS168.APS_set_field_bus_trigger_table(CardID, MBusNo, ModeNum, index, ref data[0], len);
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_CMP0_EN + index, 3);
            iErrorCode = 0;

        }
        public override void startCmpTrigger(AXIS axis, double[] data, ushort cmpPort = 0)
        {

        }
        public override void startCmpTrigger2(AXIS axis, double[] data, ushort cmpPort = 0, ushort cmpPort2 = 1)
        {

        }
        public override void stopCmpTrigger(AXIS axis)
        {

        }
        public override void stopCmpTrigger(AXIS axis, ushort cmpPort = 0)
        {
            int index = (int)axis - 1504;
            int ModeNum = 2;
            iErrorCode += APS168.APS_set_field_bus_trigger_param(CardID, MBusNo, ModeNum, (Int32)APS_Define.TG_CMP0_EN + index, 0);
            iErrorCode = 0;
        }
        public override void stopCmpTrigger(AXIS axis, ushort cmpPort = 0, ushort cmpPort1 = 1)
        {

        }
        /// <summary>
        /// 在运动过程中重置速度
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="newSpeed">设置速度</param>
        public override void SpeedOvrd(AXIS axis, int newSpeed)
        {
            iErrorCode += APS168.APS_speed_override((int)axis, (int)(newSpeed * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        /// <summary>
        /// 绝对运动过程中更改目标位置和速度
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="newPos">更改的目标位置</param>
        /// <param name="newSpeed">更改的速度</param>
        public override void AbsMoveOvrd(AXIS axis, int newPos, int newSpeed)
        {
            iErrorCode += APS168.APS_absolute_move_ovrd((int)axis, newPos, (int)(newSpeed * dic_AxisRatio[axis]));
            iErrorCode = 0;
        }
        /// <summary>
        /// 查询轴的到位信号 
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>返回结果</returns>
        public override bool IsAxisINP(AXIS axis)
        {
            return dic_Axis[axis].INP;
        }

        /// <summary>
        /// 更新数据，包括IO状态和轴数据
        /// </summary>
        public override void updateStatus()
        {
            while (true)
            {
                if (bMInit)
                {
                    sw.Reset();
                    sw.Start();
                    try
                    {
                        //设置输出
                        for (int i = 1; i < HIONum + 1; i++)
                        {
                            iErrorCode += APS168.APS_set_field_bus_d_output(CardID, HBusNo, i, iSetDO[i - 1]);
                        }
                        // iErrorCode += APS168.APS_set_field_bus_d_output(CardID, HBusNo, 8, iSetDO[HIONum-1]);  
                        Thread.Sleep(10);
                        //更新输入输出状态
                        updateDI();
                        updateDO();

                        //更新轴状态 
                        updateAxis();
                        //复位驱动器报警
                        ResetAllAxisAlarm();
                        //如果轴在回原点（手动回原点程序）
                        int len = dic_HomeStatus.Count;
                        for (int i = 1500; i < 1500 + len; i++)
                        {
                            AXIS axis = (AXIS)i;
                            if (dic_HomeZ[axis])
                            {
                                if (dic_Axis[axis].EZ)
                                {
                                    StopAxis(axis);
                                    APS168.APS_set_command((int)axis, 0);
                                    APS168.APS_set_position((int)axis, 0);
                                    dic_HomeStatus[axis] = true;
                                    dic_HomeZ[axis] = false;
                                    StopAxis(axis);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        writeDebug("更新板卡状态异常！", ex);
                    }
                    sw.Stop();
                    lRuntime = sw.ElapsedMilliseconds;
                }
                else
                {
                    Thread.Sleep(500);
                }
                iErrorCode = 0;
            }

        }
        //获取输入状态
        private void updateDI()
        {
            Int32[] di = new Int32[HIONum];
            if (bMInit)
            {
                for (int iCard = 1; iCard < HIONum + 1; iCard++)
                {
                    iErrorCode += APS168.APS_get_field_bus_d_input(CardID, HBusNo, iCard, ref di[iCard - 1]);
                }

                Array values = Enum.GetValues(typeof(DI));
                int inputNum = values.Length;
                for (int i = 0; i < inputNum; i++)
                {
                    int value = (int)values.GetValue(i);
                    DI ei = (DI)Enum.Parse(typeof(DI), value.ToString());
                    int index = 0;
                    int diff = 0;
                    //如果是16进16出IO卡
                    if (value < HDI16HDO16 * 16)
                    {
                        index = value / 16;
                        diff = value % 16;
                    }
                    else
                    {
                        index = (value - HDI16HDO16 * 16) / 32 + HDI16HDO16;
                        diff = value % 32;
                    }
                    uint a = 1;
                    dic_DI[ei] = ((di[index] & (a << diff)) != 0);


                }

            }
            iErrorCode = 0;
        }
        //获取输出状态
        private void updateDO()
        {
            Int32[] dOut = new Int32[HIONum];
            if (bMInit)
            {
                //获取1-6张卡的
                for (int iCard = 1; iCard < HIONum + 1; iCard++)
                {
                    iErrorCode += APS168.APS_get_field_bus_d_output(CardID, HBusNo, iCard, ref dOut[iCard - 1]);
                }
                //因最后一张32输出卡号不连续，单独获取
                // iErrorCode += APS168.APS_get_field_bus_d_output(CardID, HBusNo, 8, ref dOut[HIONum-1]);

                Array values = Enum.GetValues(typeof(DO));
                int outputNum = values.Length;

                for (int i = 0; i < outputNum; i++)
                {
                    int value = (int)values.GetValue(i);
                    DO eo = (DO)Enum.Parse(typeof(DO), value.ToString());
                    int index = 0;
                    int diff = 0;
                    if (value < HDI16HDO16 * 16)
                    {
                        index = value / 16;
                        diff = value % 16;
                    }
                    else
                    {
                        index = (value - HDI16HDO16 * 16) / 32 + HDI16HDO16;
                        diff = (value - HDI16HDO16 * 16) % 32;
                    }
                    uint a = 1;
                    dic_DO[eo] = ((dOut[index] & (a << diff)) != 0);

                }

            }
            iErrorCode = 0;
        }
        /// <summary>
        /// 设置输出
        /// </summary>
        /// <param name="d">设置输出的点，枚举类型DO</param>
        /// <param name="bValue">输出的值</param>
        public override void setDO(DO d, bool bValue)
        {
            if (bMInit)
            {
                int value = (int)d;


                int index = 0;
                int diff = 0;
                if (value < HDI16HDO16 * 16)
                {
                    index = value / 16;
                    diff = value % 16;
                }
                else
                {
                    index = HDI16HDO16 + (value - HDI16HDO16 * 16) / 32;
                    diff = (value - HDI16HDO16 * 16) % 32;
                }

                if (bValue)
                {
                    uint a = 1;
                    uint o = (uint)iSetDO[index];
                    iSetDO[index] = (int)(o | (a << diff));
                }
                else
                {
                    uint b = 1;
                    uint o = (uint)iSetDO[index];
                    iSetDO[index] = (int)(o & (0xffffffff ^ (b << diff)));
                }
            }
            iErrorCode = 0;
        }
        /// <summary>
        /// 获取指定轴的状态
        /// </summary>
        /// <param name="id">轴号</param>
        public override void getAxisStatus(AXIS id)
        {
            if (bMInit)
            {
                int axID = (int)id;

                int ioStatus = APS168.APS_motion_io_status(axID);

                AXStatus ax = dic_Axis[id];
                ax.ALM = (ioStatus & 0x0001) > 0;
                ax.PEL = (ioStatus & 0x0002) > 0;
                ax.MEL = (ioStatus & 0x0004) > 0;
                ax.ORG = (ioStatus & 0x0008) > 0;
                ax.EMG = ((ioStatus & 0x0010) > 0);
                ax.EZ = (ioStatus & 0x0020) > 0;
                ax.INP = (ioStatus & 0x0040) > 0;
                ax.SVON = (ioStatus & 0x0080) > 0;
                ax.RDY = (ioStatus & 0x0100) > 0;

                //获取实际位置
                int fbkPos = 0;
                iErrorCode += APS168.APS_get_position(axID, ref fbkPos);
                ax.dPos = fbkPos / dic_AxisRatio[id];
                //获取实际速度
                int vel = 0;
                APS168.APS_get_feedback_velocity(axID, ref vel);
                ax.dVel = vel / dic_AxisRatio[id];
                int cmdPos = 0;
                //获取目标位置
                iErrorCode += APS168.APS_get_target_position(axID, ref cmdPos);
                ax.dCmdPos = cmdPos / dic_AxisRatio[id];

                dic_Axis[id] = ax;
            }
            iErrorCode = 0;
        }
        public override void SetEncoderValue(AXIS axis, double dSetValue)
        {
            
        }
        /// <summary>
        /// 更新所有轴的状态
        /// </summary>
        public override void updateAxis()
        {
            if (bMInit)
            {
                string[] names = Enum.GetNames(typeof(AXIS));
                foreach (string name in names)
                {
                    AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), name);
                    getAxisStatus(axis);
                }
            }

        }
        public override void Interpolation_2D_Arc_Move(AXIS axis1, AXIS axis2, double centerX, double centerY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1)
        {
            //double TransPara = 0;
            //double PI = 3.14159;
            //Int32[] Axis_ID_Array = { (int)axis1, (int)axis2 };
            //double[] CenterArray = { centerX * dic_AxisRatio[axis1], centerY * dic_AxisRatio[axis2] };
            //double[] NormalArray = { 0, 0, 1 };
            //ASYNCALL p = new ASYNCALL();
            //double time = Math.Abs(angle) / vel;//旋转所需要的时间
            //double rad = angle * PI / 180;//角度转弧度
            //double dRotateVel = 2 * PI * Math.Abs(radius) * (angle / 360) * dic_AxisRatio[AXIS.X2轴] / time;
            //double dDestC = dic_Axis[AXIS.C轴].dPos + angle;
            ////iErrorCode += APS168.APS_arc3_ca_v(Axis_ID_Array, (Int32)APS_Define.OPT_RELATIVE
            ////    , CenterArray, NormalArray,angle, ref TransPara, vel * dic_AxisRatio[AXIS.C轴],ref p);
            //iErrorCode += APS168.APS_arc2_ca_v(Axis_ID_Array, (Int32)APS_Define.OPT_ABSOLUTE, CenterArray, rad * direct, ref TransPara, dRotateVel, ref p);
            //if (bUseRotate)
            //    AbsMove(AXIS.C轴, dDestC, (int)vel);
            //if (iErrorCode != 0)
            //{
            //    writeInfo(axis1.ToString() + "," + axis2.ToString() + " 圆弧插补执行异常" + iErrorCode.ToString());
            //}
            //iErrorCode = 0;

        }
        public override void Interpolation_2D_Arc_Move2(AXIS axis1, AXIS axis2, double centerX, double centerY, double endX, double endY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1)
        {
           
        }
        private Dictionary<AXIS, bool> dic_HomeZ = new Dictionary<AXIS, bool>();//Z相回原点标志
        /// <summary>
        /// 通过Z相回原点
        /// </summary>
        /// <param name="axis">轴号</param>       
        public override void HomeZ(AXIS axis)
        {

            //如果轴正在回原点，则返回
            if (dic_HomeZ[axis])
            {
                return;
            }
            dic_HomeZ[axis] = true;
            // dic_HomeStatus[axis] = true;
            RelativeMove(axis, 44000, 1);
            // AbsMove(axis, 10000, 5);
        }
        /// <summary>
        /// 释放卡片资源
        /// </summary>
        public override void releaseCard()
        {
            try
            {
                if (t_Update.IsAlive)
                {
                    t_Update.Abort();
                }
            }
            catch (Exception ex) { }
            t_Update = null;

            if (bMInit)
            {

                closeCard();

            }

        }
        private void closeCard()
        {
            iErrorCode = APS168.APS_stop_field_bus(CardID, MBusNo);
            iErrorCode = APS168.APS_stop_field_bus(CardID, HBusNo);
            iErrorCode = APS168.APS_close();

            bMInit = false;
        }
        public override void writeInfo(string info)
        {
            ShowInfo(info + "\r\n");
            log.Info(info);
        }
        public override void writeDebug(string info, Exception ex)
        {
            ShowInfo(info + ex.ToString() + "\r\n");
            log.Debug(info, ex);
        }




    }
}
