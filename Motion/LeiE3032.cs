using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using ConfigureFile;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;

namespace Motion
{
    /// <summary>
    /// 雷赛E3020，轴站号0-31,输入卡32-63,输出卡64-127
    /// </summary>
    public class LeiE3032:MotionCard
    {
        [DllImport("winmm")]
        static extern void timeBeginPeriod(int t);
        [DllImport("winmm")]
        static extern void timeEndPeriod(int t);
        private Thread t_Update = null;
        private Thread t_UpdateIO = null;
       // ushort _CardNo = 7;
        bool bMInit = false;
        short res = 0;
        ushort err = 0;
        uint[] AXISNum = new uint[] { 12, 0 };//轴数
        ushort[] IN = new ushort[]{0,0};//IO输入卡
        ushort[] OUT = new ushort[]{0,0};//IO输出卡
        ushort[] cardids = new ushort[8];
        uint[] cardtypes = new uint[8];
        ushort uCardNum = 0;//控制卡的数目
        ushort SectionNum = 2000;//区分轴所在卡的因素
        bool bSVON = false;
        //private static Int16 HIONum = 4;//IO板总数
        private static Int32 HDI16HDO16 = 3;//16进16出的卡的个数
        private static Int32 HDI32 = 0;//输入端子板个数
        private static Int32 HDO32 = 0;//输出端子板个数
           // private static Int16 MCardNum = 2;//轴卡个数
        ushort DIStartNO = 0;//输入起始站号
        ushort DOStartNo = 0;//输出起始站号
        short[] AxisStartNumInCard = new short[]{-1001,-1001};
       
      
        private uint[] iSetDO = new uint[] { 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0, 0 };//保存输出状态,数组长度要大于IO板的张数
        private uint[] iSetDOX16 = new uint[] { 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0xffffffff, 0, 0 };//保存输出状态,数组长度要大于IO板的张数
        private Stopwatch sw = new Stopwatch();
        private Dictionary<AXIS, bool> dic_StartFlag = new Dictionary<AXIS, bool>(); 
        internal LeiE3032()
        {
            timeBeginPeriod(1);
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

                if (value.Contains("C"))
                {
                    continue;
                }
                if (!dic_HomeStatus.ContainsKey(axis))
                    dic_HomeStatus.Add(axis, false);
                 if(!dic_StartFlag.ContainsKey(axis))
                    dic_StartFlag.Add(axis, false);

                //if (!dic_HomeZ.ContainsKey(axis))
                //{
                //    dic_HomeZ.Add(axis, false);
                //}
            }
           // dic_HomeStatus.Remove(AXIS.组装Y1轴);
            //设定轴的比率
            dic_AxisRatio.Add(AXIS.取料Y1轴, 1000);
            dic_AxisRatio.Add(AXIS.取料Y2轴, 1000);
           
            dic_AxisRatio.Add(AXIS.取料X1轴, 1000);
            dic_AxisRatio.Add(AXIS.取料X2轴, 1000);
            dic_AxisRatio.Add(AXIS.组装X1轴, 1000);
            dic_AxisRatio.Add(AXIS.组装X2轴, 1000);
            
            dic_AxisRatio.Add(AXIS.组装Z1轴, 1000);
            dic_AxisRatio.Add(AXIS.组装Z2轴, 1000);
          
            dic_AxisRatio.Add(AXIS.镜筒Y轴, 1000);
            dic_AxisRatio.Add(AXIS.镜筒X轴, 10000);

            dic_AxisRatio.Add(AXIS.C1轴, 100);
            dic_AxisRatio.Add(AXIS.C2轴, 100);





            t_Update = new Thread(updateStatus);
            t_Update.Priority = ThreadPriority.Highest;
            t_Update.Start();

            t_UpdateIO = new Thread(UpdateIOStatus);
            t_UpdateIO.Priority = ThreadPriority.Highest;
            t_UpdateIO.Start();

        }
        public override void initCard(string strPath)
        {
            if (bMInit)
            {
                return;
            }
              uint totalAxis = 0;
            ushort totalIn = 0;
            ushort totalOut = 0;
           
            short num = LTDMC.dmc_board_init();
            if (num <= 0 || num > 8)
            {
                //ShowInfo("初始卡失败!");
                bMInit = false;
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_get_CardInfList(ref uCardNum, cardtypes, cardids);//获取控制卡信息
            if (res != 0)
            {
                ShowInfo("获取卡信息失败!");
                bMInit = false;
                return;
            }
            bool EtherCardFlag = false;
            for (ushort i = 0; i < uCardNum; i++)
            {
                int _cardtype = (int)cardtypes[i];
                _cardtype = _cardtype & 0xfffff;
                if (_cardtype == 0x15032 || _cardtype == 0x13032)  //查找E35032或E3032卡
                {
                   // numericUpDown_cardNo.Value = new decimal(cardids[i]);
                    //_CardNo = cardids[i];
                    EtherCardFlag = true;


                    res = LTDMC.nmc_get_errcode(cardids[i], 2, ref err);//获取总线状态
                    if (res == 0)
                    {
                        if (err != 0)//总线报错
                        {
                            DateTime dt_start = DateTime.Now;
                            res = LTDMC.dmc_cool_reset(cardids[i]);//复位总线错误
                            if (res != 0)
                            {
                                ShowInfo("dmc_soft_reset == " + res.ToString());
                                // button_connect.Enabled = true;
                                return;
                            }
                            ShowInfo("总线复位中... ");
                            while (true)
                            {
                                if ((DateTime.Now - dt_start).TotalMilliseconds >= 15000.0)//超时退出循环
                                {
                                    ShowInfo("总线复位失败！");
                                    //button_connect.Enabled = true;
                                    return;
                                }
                                res = LTDMC.nmc_get_errcode(cardids[i], 2, ref err);
                                if (res == 0)
                                {
                                    if (err == 0)//总线复位正常完成
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    ShowInfo("nmc_get_errcode == " + res.ToString());
                                    // button_connect.Enabled = true;
                                    return;
                                }
                                // Application.DoEvents();
                            }
                        }
                    }
                    else
                    {
                        ShowInfo("nmc_get_errcode == " + res.ToString());
                        // button_connect.Enabled = true;
                        return;
                    }

                    res = LTDMC.nmc_get_total_axes(cardids[i], ref totalAxis);
                    if (res != 0)
                    {
                        ShowInfo("nmc_get_total_axes == " + res.ToString());
                        //button_connect.Enabled = true;
                        return;
                    }

                    if (totalAxis != AXISNum[i])
                    {
                        ShowInfo("控制卡未连接轴！");
                        //button_connect.Enabled = true;
                        return;
                    }
                    res = LTDMC.nmc_get_total_ionum(cardids[i], ref IN[i], ref OUT[i]);

                    //if (totalIn != IN[i])
                    //{
                    //    ShowInfo("检测IO输入卡数目与设定不一致！");
                    //    //button_connect.Enabled = true;
                    //    return;
                    //}

                    //if (totalOut != OUT[i])
                    //{
                    //    ShowInfo("检测IO输出卡数目与设定不一致！");
                    //    //button_connect.Enabled = true;
                    //    return;
                    //}

                    // comboBox_axis.Items.Clear();

                    string fileName = strPath + "E3032-"+cardids[i].ToString() + ".ini";
                    res = LTDMC.dmc_download_configfile(cardids[i],fileName );//下载轴参数文件
                    if (res != 0)
                    {
                        ShowInfo("加载配置文件失败, dmc_download_configfile == " + res.ToString());
                        return;
                    }
                    //加载成功后，打开模拟量模块
                    //InitE1DA();
                    //InitDA(1);
                    //InitDA(2);
                }
            }
            if (!EtherCardFlag)
            {
                ShowInfo("不存在EtherCAT总线卡!");
                //button_connect.Enabled = true;
                return;
            }
            foreach (KeyValuePair<AXIS, ushort> pair in dic_AbsAxis)
            {
                LTDMC.nmc_set_offset_pos(0, pair.Value, 0);
            }
            bMInit = true;
            ShowInfo("初始化卡成功！");
        }
        //初始化指定模拟输出通道
        private void InitDA(ushort subindex)
        {
            ushort uCardId = 0;
            ushort nodenum = 1013;
            //ushort subindex = 1;
            int enable = 1;
            int mode = 4;
           // uint value = 0;
            LTDMC.nmc_set_node_od(uCardId, 2, nodenum, 0x3010, subindex, 8, enable);
            LTDMC.nmc_set_node_od(uCardId, 2, nodenum, 0x3009, subindex, 8, mode);
        }
        //打开E1的模拟量模块输出通道
        private void InitE1DA()
        {
            res=LTDMC.nmc_write_rxpdo_extra(0, 2, 4, 1, 1);
            if (res != 0)
            {
                ShowInfo("nmc_write_rxpdo_extra == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }

        }
        public override void WriteOutDA(float outV, ushort address)
        {
            byte[] a = BitConverter.GetBytes(outV);
            uint value = BitConverter.ToUInt32(a, 0);
            LTDMC.nmc_write_rxpdo_extra_uint((ushort)0, 2, address, 2, value);
        }
        /// <summary>
        /// 设置对应通道的输出电压
        /// </summary>
        /// <param name="value">电压值</param>
        /// <param name="subindex">输出通道，0，2</param>
        //public override void WriteOutDA(float value, ushort subindex)
        //{
        //    int outputV = (int)( value * 10000);
        //      res= LTDMC.nmc_write_rxpdo_extra(0, 2, subindex, 2, outputV);
        //      if (res != 0)
        //      {
        //          ShowInfo("nmc_write_rxpdo_extra == " + res.ToString());
        //          //button_connect.Enabled = true;
        //          return;
        //      }

        //}
        //public override float ReadOutAD(ushort subindex)
        //{
        //    uint value=0;
        //    res = LTDMC.nmc_read_rxpdo_extra_uint(0, 2, subindex, 2,ref value);
        //    if (res != 0)
        //    {
        //        ShowInfo("nmc_read_rxpdo_extra_uint == " + res.ToString());
        //        //button_connect.Enabled = true;
                
        //    }
        //    return (float)( value/10000.0);
        //}
       
        public override float ReadOutAD(ushort subindex)
        {
            ushort uCardId = 0;
            ushort nodenum = 1013;
            //ushort subindex = 1;
            //int enable = 1;
            //int mode = 4;
            ushort index = 1;
           

            int value = 0;
            LTDMC.nmc_get_node_od(uCardId, 2, nodenum, (ushort)(0x3006+(subindex-index)), index, 32, ref value);
            byte[] a = BitConverter.GetBytes(value);
            float d = BitConverter.ToSingle(a, 0);
            return d;
        }
        public static float ReadOutAD2(ushort mainIndex, ushort subindex)
        {
            ushort uCardId = 0;
            ushort nodenum = 1013;
            //ushort subindex = 1;
            //int enable = 1;
            //int mode = 4;
           


            int value = 0;
            LTDMC.nmc_get_node_od(uCardId, 2, nodenum, mainIndex, subindex, 32, ref value);
            byte[] a = BitConverter.GetBytes(value);
            float d = BitConverter.ToSingle(a, 0);
            return d;
        }

        /// <summary>
        /// 总线卡加载INI格式
        /// </summary>
        /// <param name="CardNo">控制卡号</param>
        /// <param name="IniFilePath">总线INI文件放置路径</param>
        public bool LoadINI(ushort CardNo, string IniFilePath)
        {
            short ret = -1;
            bool result = false;
            ushort myCardNo = CardNo;
            FileStream fs = new FileStream(IniFilePath, FileMode.Open);
            //【2】创建读取器
            StreamReader sr = new StreamReader(fs);
            //【3】以流的方式读取数据
            string str = sr.ReadToEnd();
            //【4】关闭读取器
            sr.Close();
            //【5】关闭文件流
            fs.Close();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] fileincontrol = Encoding.UTF8.GetBytes("");
            ushort filetype = 201;

            //   ret = LTDMC.nmc_set_cycletime(myCardNo, 2, 500);      //设置总线周期
            ret = LTDMC.dmc_download_memfile(myCardNo, buffer, (uint)buffer.Length, fileincontrol, filetype);     //下载配置文件
            if (ret == 0)
            {
                result = true;
            }
            else
            {
                ShowInfo("总线配置文件ENI下载失败卡!"+CardNo.ToString());
               // MessageBox.Show("总线配置文件ini下载失败！");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 总线卡加载ENI文件格式
        /// </summary>
        /// <param name="CardNo">控制卡号</param>
        /// <param name="EniFilePath">ENI文件路径</param>
        /// <returns></returns>
        public bool LoadENI(ushort CardNo, string EniFilePath)
        {
            short ret = -1;
            bool result = false;
            ushort myCardNo = CardNo;
            FileStream fs = new FileStream(EniFilePath, FileMode.Open);
            //【2】创建读取器
            StreamReader sr = new StreamReader(fs);
            //【3】以流的方式读取数据
            string str = sr.ReadToEnd();
            //【4】关闭读取器
            sr.Close();
            //【5】关闭文件流
            fs.Close();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] fileincontrol = Encoding.UTF8.GetBytes("");
            ushort filetype = 201;

            //  ret = LTDMC.nmc_set_cycletime(myCardNo, 2, 500);      //设置总线周期
            ret = LTDMC.dmc_download_memfile(myCardNo, buffer, (uint)buffer.Length, fileincontrol, filetype);     //下载配置文件
            if (ret == 0)
            {
                result = true;
            }
            else
            {
                ShowInfo("总线配置文件ENI下载失败！" + CardNo.ToString());
               // MessageBox.Show("总线配置文件ENI下载失败！");
                result = false;
            }
            return result;
        }

        public override void SeverOn(AXIS axis, int status)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            LTDMC.dmc_set_sevon_enable(cardID, axisId, (ushort)status);

        }

        public override void ResetAxisAlarm()
        {
            try
            {
                foreach (KeyValuePair<AXIS, AXStatus> pair in dic_Axis)
                {
                    if (pair.Value.ALM)
                    {
                        ushort cardID = GetCardIdByAxis(pair.Key);
                        ushort axisId = (ushort)pair.Key;
                        if (axisId < 1000)
                            axisId = (ushort)(1000 + axisId);
                        axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
                        LTDMC.nmc_clear_axis_errcode(cardID, axisId);
                    }
                }
            }
            catch (Exception)
            {

               
            }
         
        }
        private ushort GetCardIdByAxis(AXIS axis)
        {
            ushort axisID = (ushort)axis;
            ushort card = (ushort)(axisID / (SectionNum + 1));
            return card;
        }

        public override void SeverOnAll()
        {
            if (bMInit)
            {
                for (int i = 0; i < uCardNum; i++)
                {
                    res =LTDMC.nmc_set_axis_enable(cardids[i], 255);
                    if (res != 0)
                    {
                        ShowInfo("nmc_set_axis_enable == " + res.ToString());
                        //button_connect.Enabled = true;
                        bSVON = false;
                        return;
                      
                    }
                }
                bSVON = true;
            }

        }

        public override void SeverOffAll()
        {
            if (bMInit)
            {
                for (int i = 0; i < uCardNum; i++)
                {
                   res = LTDMC.nmc_set_axis_disable(cardids[i], 255);
                   if (res != 0)
                   {
                       ShowInfo("nmc_set_axis_disable == " + res.ToString());
                       //button_connect.Enabled = true;
                       return;
                   }
                }
            }
            bSVON = false;
        }

        public override void AbsMove(AXIS axis, int pos, int vel)
        {
           
           
        }

        public override void AbsMove(AXIS axis, double pos, int vel)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);

            double dMaxVel = vel * dic_AxisRatio[axis];
            double dStartVel = dMaxVel / 3;
            double dEndVel = dMaxVel / 3;
            double dAcc = vel / 3000.0;

            res = LTDMC.dmc_set_profile_unit(cardID, axisId, (int)dStartVel, (int)(dMaxVel), dAcc, dAcc, (int)dEndVel);
            if (res != 0)
            {
                ShowInfo("dmc_set_profile_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_set_s_profile(cardID, axisId, 0, dAcc/3);
            if (res != 0)
            {
                ShowInfo("dmc_set_s_profile == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_pmove_unit(cardID, axisId, pos * dic_AxisRatio[axis], 1);
            if (res != 0)
            {
                ShowInfo("dmc_pmove_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
        }
        public override void AbsMove(AXIS axis, double pos, double vel)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);

            double dMaxVel = vel * dic_AxisRatio[axis];
            double dStartVel = dMaxVel / 3;
            double dEndVel = dMaxVel / 3;
            double dAcc = vel / 3000.0;

            res = LTDMC.dmc_set_profile_unit(cardID, axisId, (int)dStartVel, (int)(dMaxVel), dAcc, dAcc, (int)dEndVel);
            if (res != 0)
            {
                ShowInfo("dmc_set_profile_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_set_s_profile(cardID, axisId, 0, dAcc / 3);
            if (res != 0)
            {
                ShowInfo("dmc_set_s_profile == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_pmove_unit(cardID, axisId, pos * dic_AxisRatio[axis], 1);
            if (res != 0)
            {
                ShowInfo("dmc_pmove_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
        }

        public override void AbsMove(AXIS axis, double pos, double vel, double acc = 0.1, double dec = 0.1)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);

            double dMaxVel = vel * dic_AxisRatio[axis];
            double dStartVel = dMaxVel / 3;
            double dEndVel = dMaxVel / 3;
           // double dAcc = vel / 3000.0;

            res = LTDMC.dmc_set_profile_unit(cardID, axisId, (int)dStartVel, (int)(dMaxVel), acc, dec, (int)dEndVel);
            if (res != 0)
            {
                ShowInfo("dmc_set_profile_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_set_s_profile(cardID, axisId, 0, acc / 3);
            if (res != 0)
            {
                ShowInfo("dmc_set_s_profile == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            res = LTDMC.dmc_pmove_unit(cardID, axisId, pos * dic_AxisRatio[axis], 1);
            if (res != 0)
            {
                ShowInfo("dmc_pmove_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
        }
        public override void RelativeMove(AXIS axis, int len, double vel)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;

            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = LTDMC.dmc_set_profile_unit(cardID, axisId, 0, (int)(vel * dic_AxisRatio[axis]), 0.05, 0.05, 0);
            res += LTDMC.dmc_set_s_profile(0, 0, 0, 0.05);
            res += LTDMC.dmc_pmove_unit(cardID, axisId, len, 0);
            if (res != 0)
            {
                ShowInfo("dmc_pmove_unit == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
        }

        public override void VelMove(AXIS axis, int vel)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            ushort direct = 0;
            if (vel > 0)
            {
                direct = 1;
            }
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = LTDMC.dmc_set_profile_unit(cardID, axisId, 0, Math.Abs((int)(vel * dic_AxisRatio[axis])), 0.05, 0.05, 0);
            res += LTDMC.dmc_set_s_profile(0, 0, 0, 0.05);
            res += LTDMC.dmc_vmove(cardID, axisId, direct);
            if (res != 0)
            {
                ShowInfo("dmc_vmove == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
        }

        public override void StopAxis(AXIS axis)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = LTDMC.dmc_stop(cardID, axisId, 0);//制动方式，0：减速停止，1：紧急停止
            if (res != 0)
            {
                ShowInfo("dmc_stop == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
           
        }

        public override void StopAllAxis()
        {
            Array arr = Enum.GetValues(typeof(AXIS));
            foreach (AXIS axis in arr)
            {
                StopAxis(axis);
            }
        }
        private void HomeTrack()
        {
            try
            {

           
                if (!bHome)
                    return;
                //string[] values = Enum.GetNames(typeof(AXIS));
                //foreach (string value in values)
                //{

                //    AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                //    if (!dic_HomeStatus.ContainsKey(axis))
                //        continue;
                //    if (dic_AbsAxis.Keys.Contains(axis))
                //    {
                //        if (!dic_HomeStatus[axis])
                //        {
                //            HomeAbsHandle(axis);
                //        }
                //    }
                //    else
                //    {
                //       // Thread.Sleep(500);
                //        if (!dic_HomeStatus[axis])
                //        {
                //            HomeHandle(axis);
                //        }
                //    }
                //}
                foreach (KeyValuePair<AXIS, bool> pair in dic_HomeStatus)
                {
                    if (dic_AbsAxis.Keys.Contains(pair.Key))
                    {
                        if (!pair.Value)
                        {
                            HomeAbsHandle(pair.Key);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                        if (!pair.Value)
                        {
                            HomeHandle(pair.Key);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                ShowInfo("回原点异常"+ex.ToString());
            }
        }
        public override void HomeAbs(AXIS axis)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            dic_HomeStatus[axis] = false;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);

            AbsMove(axis, 0.0, 50);
            //Action<AXIS> delegateHome = HomeAbsHandle;
            //delegateHome.BeginInvoke(axis, HomeCallBack, axis);
        }
        private void HomeAbsHandle(AXIS axis)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            if ((LTDMC.dmc_check_done(cardID, axisId)) == 0)
            {
               // Thread.Sleep(50);

                return;
            }
            else
            {
                dic_HomeStatus[axis] = true;
            }
        }
        #region"Home"
        //public override void Home(AXIS axis)
        //{
        //    ushort cardID = GetCardIdByAxis(axis);
        //    ushort axisId = (ushort)axis;
        //    dic_HomeStatus[axis] = false;

        //    if (axisId < 1000)
        //        axisId = (ushort)(1000 + axisId);
        //    axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
        //    res = LTDMC.nmc_home_move(cardID,axisId);//启动回零   
        //    if (res != 0)
        //    {
        //        ShowInfo("nmc_home_move == " + res.ToString());
        //        //button_connect.Enabled = true;
        //        return;
        //    }
        //    //if (res == 0)
        //    //{
        //    //    //Action<AXIS> delegateHome = HomeHandle;
        //    //    //delegateHome.BeginInvoke(axis, HomeCallBack, axis);

        //    //}

        //}
        #endregion
        public override void Home(AXIS axis)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            dic_HomeStatus[axis] = false;

            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = LTDMC.nmc_home_move(cardID, axisId);//启动回零   
            if (res != 0)
            {
                ShowInfo("nmc_home_move == " + res.ToString());
                //button_connect.Enabled = true;
                return;
            }
            Task t = new Task(() => {
                Thread.Sleep(100);
                while(LTDMC.dmc_check_done(cardID, axisId) == 0)
                {
                    Thread.Sleep(100);
                    //ShowInfo(axis.ToString() + "回零中-------------!");
                    HomeHandle(axis);
                   
                }
                

            });
            t.Start();

        }
        private void HomeHandle(AXIS axis)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            if ((LTDMC.dmc_check_done(cardID, axisId)) == 0)
            {
                //Thread.Sleep(50);

                return;
            }
            ushort state=0;
            res = LTDMC.dmc_get_home_result(cardID, axisId, ref state);//判断回零结果
             
            if (res == 0)
            {
                if (state == 1)
                {
                    if (dic_Axis[axis].EMG)
                    {
                        dic_HomeStatus[axis] = false;
                        //dic_StartFlag[axis] = false;
                        ShowInfo(axis.ToString() + "回零时急停按下!");
                    }
                    else {
                        //dic_StartFlag[axis] = false;
                        dic_HomeStatus[axis] = true;
                        ShowInfo(axis.ToString() + "回零成功!");
                    #region"清除辅助编码器"
                    ////清除辅助编码器的值
                    //if (axis == AXIS.取料X1轴)
                    //{
                    //    double dEncodPos = 0;
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res = LTDMC.dmc_set_extra_encoder(0, 0, (int)dEncodPos);
                    //    if (res != 0)
                    //    {
                    //        ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //        //button_connect.Enabled = true;

                    //    }
                    //    Thread.Sleep(10);

                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res = LTDMC.dmc_set_extra_encoder(0, 0, (int)dEncodPos);
                    //    if (res != 0)
                    //    {
                    //        ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //        //button_connect.Enabled = true;

                    //    }
                    //    ShowInfo(axis.ToString() + "置位!");
                    //}
                    //else if (axis == AXIS.取料X2轴)
                    //{
                    //    double dEncodPos = 0;
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res = LTDMC.dmc_set_extra_encoder(0, 1, (int)dEncodPos);
                    //   if (res != 0)
                    //   {
                    //       ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //       //button_connect.Enabled = true;

                    //   }
                    //    Thread.Sleep(10);
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res=LTDMC.dmc_set_extra_encoder(0, 1, (int)dEncodPos);
                    //    if (res != 0)
                    //    {
                    //        ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //        //button_connect.Enabled = true;

                    //    }

                    //    ShowInfo(axis.ToString() + "置位!");
                    //}
                    //else if (axis == AXIS.组装X1轴)
                    //{
                    //    double dEncodPos = 0;
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res = LTDMC.dmc_set_extra_encoder(1, 0, (int)dEncodPos);
                    //    if (res != 0)
                    //    {
                    //        ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //        //button_connect.Enabled = true;

                    //    }
                    //    Thread.Sleep(10);
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //    res = LTDMC.dmc_set_extra_encoder(1, 0, (int)dEncodPos);
                    //    if (res != 0)
                    //    {
                    //        ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //        //button_connect.Enabled = true;

                    //    }
                    //    ShowInfo(axis.ToString() + "置位!");
                    //}
                    //else if (axis == AXIS.组装X2轴)
                    //{
                    //    double dEncodPos = 0;
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //   res= LTDMC.dmc_set_extra_encoder(1, 1, (int)dEncodPos);
                    //   if (res != 0)
                    //   {
                    //       ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //       //button_connect.Enabled = true;

                    //   }
                    //    Thread.Sleep(10);
                    //    LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
                    //   res= LTDMC.dmc_set_extra_encoder(1, 1, (int)dEncodPos);
                    //   if (res != 0)
                    //   {
                    //       ShowInfo("dmc_set_extra_encoder == " + res.ToString());
                    //       //button_connect.Enabled = true;

                    //   }
                    //    ShowInfo(axis.ToString() + "置位!");
                    //}
                    #endregion
                         }
                }
                else
                {
                     dic_HomeStatus[axis] = false;
                    //dic_StartFlag[axis] = false;
                     ShowInfo(axis.ToString() + "回零失败!");
                }
            }
            else
            {
                ShowInfo("dmc_get_home_result == " + res.ToString());
            }
            
           
        }
        public void ResetExtraEncoder()
        {
            Thread.Sleep(1000);
            int res = 0;

            res = LTDMC.dmc_set_extra_encoder(0, 0, (int)(dic_Axis[AXIS.取料X1轴].dPos * dic_AxisRatio[AXIS.取料X1轴]));
            if (res != 0)
            {
                ShowInfo("取料X1轴 dmc_set_extra_encoder == " + res.ToString());
                //button_connect.Enabled = true;

            }
            Thread.Sleep(100);
            res = LTDMC.dmc_set_extra_encoder(0, 1, (int)(dic_Axis[AXIS.取料X2轴].dPos * dic_AxisRatio[AXIS.取料X2轴]));
            if (res != 0)
            {
                ShowInfo("取料X2轴 dmc_set_extra_encoder == " + res.ToString());
                //button_connect.Enabled = true;

            }
            res = LTDMC.dmc_set_extra_encoder(1, 0, (int)(dic_Axis[AXIS.组装X1轴].dPos * dic_AxisRatio[AXIS.组装X1轴]));
            if (res != 0)
            {
                ShowInfo("组装X1轴 dmc_set_extra_encoder == " + res.ToString());
                //button_connect.Enabled = true;

            }
            Thread.Sleep(100);
            res = LTDMC.dmc_set_extra_encoder(1, 1, (int)(dic_Axis[AXIS.组装X2轴].dPos * dic_AxisRatio[AXIS.组装X2轴]));
            if (res != 0)
            {
                ShowInfo("组装X2轴 dmc_set_extra_encoder == " + res.ToString());
                //button_connect.Enabled = true;

            }
          
        }
        private void HomeCallBack(IAsyncResult result)
        {
            AsyncResult _Result = (AsyncResult)result;
            Action<AXIS> delegateObj = (Action<AXIS>)_Result.AsyncDelegate;
            AXIS _axis = (AXIS)_Result.AsyncState;
            delegateObj.EndInvoke(_Result);
            if (!dic_HomeStatus[_axis])
            {
                if (!dic_Axis[_axis].EMG)
                {
                    delegateObj.BeginInvoke(_axis, HomeCallBack, _axis);
                }
            }

        }
        public override void startCmpTrigger(AXIS axis, int[] data)
        {
            
        }
        public override void startCmpTrigger(AXIS axis, double[] data, ushort cmpPort = 0)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
           // res = LTDMC.dmc_set_extra_encoder(cardID, cmpPort, 0);
            //清除辅助编码器的值
            //if (axis == AXIS.取料X1轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 0, (int)(dic_Axis[AXIS.取料X1轴].dPos * dic_AxisRatio[AXIS.取料X1轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.取料X2轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 1, (int)(dic_Axis[AXIS.取料X2轴].dPos * dic_AxisRatio[AXIS.取料X2轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.组装X1轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 0, (int)(dic_Axis[AXIS.组装X1轴].dPos * dic_AxisRatio[AXIS.组装X1轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.组装X2轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 1, (int)(dic_Axis[AXIS.组装X2轴].dPos * dic_AxisRatio[AXIS.组装X2轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            if (cardID == 0)
            {
                axisId = (ushort)(axisId - 9);
            }
           
           
            
            res = HighSpedCmp(cardID, axisId, 4, cmpPort, 1, 0, 5000); //cmpPort号高速比较口
            if (res != 0)
            {
                ShowInfo(axis.ToString()+" HighSpedCmp == " + res.ToString());
                //button_connect.Enabled = true;

            }

            int count = data.Length;
            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                   
                   
                  res = LTDMC.dmc_hcmp_add_point(cardID, cmpPort, (int)(data[i] * dic_AxisRatio[axis]/2));   //cmpPort号高速比较输出口触发
                  if (res != 0)
                  {
                      ShowInfo(axis.ToString() + " dmc_hcmp_add_point == " + res.ToString());
                      //button_connect.Enabled = true;

                  }

                }
            }
        }
        public override void startCmpTrigger2(AXIS axis, double[] data,ushort cmpPort =0,ushort cmpPort1 = 1 )
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            //清除辅助编码器的值
            //if (axis == AXIS.取料X1轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 0, (int)(dic_Axis[AXIS.取料X1轴].dPos * dic_AxisRatio[AXIS.取料X1轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.取料X2轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 1, (int)(dic_Axis[AXIS.取料X2轴].dPos * dic_AxisRatio[AXIS.取料X2轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.组装X1轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 0, (int)(dic_Axis[AXIS.组装X1轴].dPos * dic_AxisRatio[AXIS.组装X1轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            //else if (axis == AXIS.组装X2轴)
            //{
            //    LTDMC.dmc_set_extra_encoder(cardID, 1, (int)(dic_Axis[AXIS.组装X2轴].dPos * dic_AxisRatio[AXIS.组装X2轴]));
            //    ShowInfo(axis.ToString() + "置位!");
            //}
            res = HighSpedCmp(cardID, axisId, 4, cmpPort, 1, 0, 1000); //cmpPort号高速比较口
            res = HighSpedCmp(cardID, axisId, 4, cmpPort1, 1, 0, 1000); //cmpPort1号高速比较口
            if (res != 0)
            {
                ShowInfo(axis.ToString() + " HighSpedCmp == " + res.ToString());
                //button_connect.Enabled = true;

            }
            int count = data.Length;
            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == count - 1)
                    {
                        res = LTDMC.dmc_hcmp_add_point(cardID, cmpPort1, (int)(data[i] * dic_AxisRatio[axis]/2));   //cmpPort1号高速比较输出口触发
                    }
                    else
                    {
                        res = LTDMC.dmc_hcmp_add_point(cardID, cmpPort, (int)(data[i] * dic_AxisRatio[axis]/2));   //cmpPort号高速比较输出口触发
                    }
                    if (res != 0)
                    {
                        ShowInfo(axis.ToString() + " dmc_hcmp_add_point == " + res.ToString());
                        //button_connect.Enabled = true;

                    }
                }
            }
        }
        public override void stopCmpTrigger(AXIS axis)
        {
            
        }
        public override void stopCmpTrigger(AXIS axis, ushort cmpPort = 0)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = HighSpedCmp(cardID, axisId, 0, cmpPort, 1, 0, 500); //禁止cmpPort号高速比较口
          
        }

        public override void stopCmpTrigger(AXIS axis,ushort cmpPort = 0,ushort cmpPort1 = 1)
        {
            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            res = HighSpedCmp(cardID, axisId, 0, cmpPort, 1, 0, 500); //禁止cmpPort号高速比较口
            res = HighSpedCmp(cardID, axisId, 0, cmpPort1, 1, 0, 500);  //禁止cmpPort1号高速比较口
        }
        //高速比较器
        /// <summary>
        /// 高速比较器配置
        /// </summary>
        /// <param name="CardNo">控制卡号</param>
        /// <param name="Axis">对应的轴号</param>
        /// <param name="cmp_mode">比较器模式 0 禁止 1等于 2 小于 3 大于  4列队 5 线性</param>
        /// <param name="hcmp">高速比较器对应高速比较口（0-5,对应OUT2-OUT7）</param>
        /// <param name="cmp_source">比较源 0为指令位置，1 编码器反馈位置</param>
        /// <param name="cmp_logic">有效电平 0 低电平 1高电平</param>
        /// <param name="time">输出脉冲的宽度  us  0-20s</param>
        /// <returns></returns>
        public short HighSpedCmp(ushort CardNo, ushort Axis, ushort cmp_mode, ushort hcmp, ushort cmp_source, ushort cmp_logic, int time)
        {
            short ret = 0;
            ushort myCardNo = CardNo;
            ushort myaxis = Axis;
            ushort mycmp_mode = cmp_mode;
            ushort myhcmp = hcmp;
            ushort mycmp_source = cmp_source;
            ushort mycmp_logic = cmp_logic;
            int mytime = time;
            ret += LTDMC.dmc_hcmp_set_mode(myCardNo, myhcmp, mycmp_mode);
            ret += LTDMC.dmc_hcmp_set_config(myCardNo, myhcmp, myaxis, mycmp_source, mycmp_logic, mytime);
            ret += LTDMC.dmc_hcmp_clear_points(myCardNo, myhcmp);
           
            return ret;

        }

        public override void SpeedOvrd(AXIS axis, int newSpeed)
        {
           
        }

        public override void AbsMoveOvrd(AXIS axis, int newPos, int newSpeed)
        {
           
        }
        public override void Interpolation_2D_Arc_Move(AXIS axis1, AXIS axis2, double endX, double endY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1)
        {
            ushort cardID = GetCardIdByAxis(axis1);
            ushort axisId1 = (ushort)axis1;
            if (axisId1 < 1000)
                axisId1 = (ushort)(1000 + axisId1);
            axisId1 = (ushort)(AxisStartNumInCard[cardID] + axisId1);

           
            ushort axisId2 = (ushort)axis2;
            if (axisId2 < 1000)
                axisId2 = (ushort)(1000 + axisId2);
            axisId2 = (ushort)(AxisStartNumInCard[cardID] + axisId2);

            double TransPara = 0;
            double PI = 3.14159;
            ushort[] Axis_ID_Array = { (ushort)axisId1, (ushort)axisId2 };
            double[] EndArray = { endX * dic_AxisRatio[axis1], endY * dic_AxisRatio[axis2] };

           
            double time = Math.Abs(angle) / vel;//旋转所需要的时间
            double rad = angle * PI / 180;//角度转弧度
            double dRotateVel = 2 * PI * Math.Abs(radius) * (angle / 360)  / time;
            res = LTDMC.dmc_set_profile_unit(cardID, axisId1, (int)0, (int)(dRotateVel * dic_AxisRatio[axis1]), 0.05, 0.05, 0);

            res = LTDMC.dmc_set_s_profile(cardID, axisId1, 0, 0.01);

            res = LTDMC.dmc_set_profile_unit(cardID, axisId2, (int)0, (int)(dRotateVel * dic_AxisRatio[axis2]), 0.05, 0.05, 0);

            res = LTDMC.dmc_set_s_profile(cardID, axisId2, 0, 0.01);

            //double dDestC = dic_Axis[AXIS.点胶C轴].dPos + angle;
            ////iErrorCode += APS168.APS_arc3_ca_v(Axis_ID_Array, (Int32)APS_Define.OPT_RELATIVE
            ////    , CenterArray, NormalArray,angle, ref TransPara, vel * dic_AxisRatio[AXIS.C轴],ref p);
            //res = LTDMC.dmc_arc_move_radius_unit(cardID, 0, 2, Axis_ID_Array, EndArray, radius * dic_AxisRatio[axis1], (ushort)direct, 1, 1);
            //if (bUseRotate)
            //    AbsMove(AXIS.点胶C轴, dDestC, (int)vel);
            //if (res != 0)
            //{
            //    writeInfo(axis1.ToString() + "," + axis2.ToString() + " 圆弧插补执行异常" + res.ToString());
            //}
           
        }
        public override void Interpolation_2D_Arc_Move2(AXIS axis1, AXIS axis2, double centerX, double centerY, double endX, double endY, double vel, double angle, bool bUseRotate, double radius = 0, int direct = 1)
        {
            ushort cardID = GetCardIdByAxis(axis1);
            ushort axisId1 = (ushort)axis1;
            if (axisId1 < 1000)
                axisId1 = (ushort)(1000 + axisId1);
            axisId1 = (ushort)(AxisStartNumInCard[cardID] + axisId1);


            ushort axisId2 = (ushort)axis2;
            if (axisId2 < 1000)
                axisId2 = (ushort)(1000 + axisId2);
            axisId2 = (ushort)(AxisStartNumInCard[cardID] + axisId2);

            double TransPara = 0;
            double PI = 3.14159;
            ushort[] Axis_ID_Array = { (ushort)axisId1, (ushort)axisId2 };
            double[] CenterArray = { centerX * dic_AxisRatio[axis1], centerY * dic_AxisRatio[axis2] };
            double[] EndArray = { endX * dic_AxisRatio[axis1], endY * dic_AxisRatio[axis2] };

            double time = Math.Abs(angle) / vel;//旋转所需要的时间
            double rad = angle * PI / 180;//角度转弧度
            double dRotateVel = 2 * PI * Math.Abs(radius) * (angle / 360) / time;
            res = LTDMC.dmc_set_vector_profile_unit(cardID, 0, 0, (int)(dRotateVel * dic_AxisRatio[axis2]), 0.05, 0.05, 0);

            res = LTDMC.dmc_set_vector_s_profile(cardID, 0, 0, 0.01);

            //res = LTDMC.dmc_set_profile_unit(cardID, axisId2, (int)0, (int)(dRotateVel * dic_AxisRatio[axis2]), 0.05, 0.05, 0);

            //res = LTDMC.dmc_set_s_profile(cardID, axisId2, 0, 0.01);

            //if (direct == 0)
            //    angle = -angle;
           // double dDestC = dic_Axis[AXIS.点胶C轴].dPos -angle;
           // //iErrorCode += APS168.APS_arc3_ca_v(Axis_ID_Array, (Int32)APS_Define.OPT_RELATIVE
           // //    , CenterArray, NormalArray,angle, ref TransPara, vel * dic_AxisRatio[AXIS.C轴],ref p);
           // short circleNum =(short)(( Math.Abs(angle) / 360.5));
           // if (circleNum < 0)
           //     circleNum = 0;
           // writeInfo("圆弧圈数：" + circleNum.ToString());
           // res = LTDMC.dmc_arc_move_center_unit(cardID, 0, 2, Axis_ID_Array, EndArray, CenterArray, (ushort)direct, circleNum, 1);
           //// res = LTDMC.dmc_arc_move_radius_unit(cardID, 0, 2, Axis_ID_Array, EndArray, radius, (ushort)direct, circleNum, 1);
           // if (bUseRotate)
           //     AbsMove(AXIS.点胶C轴, dDestC, (int)vel);
            if (res != 0)
            {
                writeInfo(axis1.ToString() + "," + axis2.ToString() + " 圆弧插补执行异常" + res.ToString());
            }
           
        }
        public override bool IsAxisINP(AXIS axis)
        {
              ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;

            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            return  1 ==  LTDMC.dmc_check_done(cardID, axisId);
            
        }

        public override void updateStatus()
        {
            while (true)
            {
                if (bMInit)
                {
                    //sw.Restart();
                    try
                    {
                        //WriteOutput();
                        //Action writeOut = WriteOutput;
                        //writeOut.BeginInvoke(null, null);
                        // iErrorCode += APS168.APS_set_field_bus_d_output(CardID, HBusNo, 8, iSetDO[HIONum-1]);  
                        Thread.Sleep(5);
                        //Action updatedi = updateDI;
                        //updatedi.BeginInvoke(null, null);
                        //Action updatedo = updateDO;
                        //updatedo.BeginInvoke(null, null);
                        //Action updateAx = updateAxis;
                        //updateAx.BeginInvoke(null, null);
                        //HomeTrack();
                        //更新输入输出状态
                        //updateDI();
                        //updateDO();

                        //更新轴状态 
                        updateAxis();
                        
                      

                    }
                    catch (Exception ex)
                    {
                        writeDebug("更新板卡状态异常！", ex);
                    }
                    //sw.Stop();
                    lRuntime = sw.ElapsedMilliseconds;
                }
                else
                {
                    Thread.Sleep(100);
                }
               
            }

          

        }
        private void UpdateIOStatus()
        {
            while (true)
            {
                if (bMInit)
                {
                    sw.Restart();
                    try
                    {
                        WriteOutput();
                        //Action writeOut = WriteOutput;
                        //writeOut.BeginInvoke(null, null);
                        // iErrorCode += APS168.APS_set_field_bus_d_output(CardID, HBusNo, 8, iSetDO[HIONum-1]);  
                        Thread.Sleep(10);
                       
                        updateDIX16();
                        updateDOX16();


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

            }

          
        }
        private void WriteOutput()
        {
            //设置输出
            for (int i = 0; i < HDO32; i++)
            {
                LTDMC.dmc_write_outport(0, (ushort)(DOStartNo + i), iSetDO[i]);
            }
            for (int i = 0; i < HDI16HDO16; i++)
            {
                LTDMC.dmc_write_outport(0, (ushort)(DOStartNo + i), iSetDO[i]);
             
            }
        }
        Array valuesDI;
              //获取输入状态
        private void updateDI()
            {
             
                UInt32[] di = new UInt32[HDI32];
               
           
                if (bMInit)
                {
                    for (int iCard = 0; iCard < HDI32; iCard++)
                    {
                        di[iCard]= LTDMC.dmc_read_inport(0,(ushort)(DIStartNO+iCard));
                       
                    }
                    if(valuesDI==null)
                        valuesDI = Enum.GetValues(typeof(DI));
                   
                    int inputNum = valuesDI.Length;
                    for (int i = 0; i < inputNum; i++)
                    {
                        int value = (int)valuesDI.GetValue(i);
                        DI ei = (DI)value;
                       // DI ei = (DI)Enum.Parse(typeof(DI), value.ToString());
                        int index = 0;
                        int diff = 0;
                       
                      
                        index = value / 32;
                        diff = value % 32;
                       
                        uint a = 1;
                        
                        dic_DI[ei] = ((di[index] & (a << diff)) == 0);


                    }

                }
              
            }

        private void updateDIX16()
        {
            int len = HDI16HDO16 / 2+ HDI16HDO16%2;
            UInt32[] di = new UInt32[len];


            if (bMInit)
            {
                for (int iCard = 0; iCard < len; iCard++)
                {
                    di[iCard] = LTDMC.dmc_read_inport(0, (ushort)(DIStartNO + iCard));
                    

                }
                if (valuesDI == null)
                    valuesDI = Enum.GetValues(typeof(DI));

                int inputNum = valuesDI.Length;
                for (int i = 0; i < inputNum; i++)
                {
                    try
                    {
                        
                      int value = (int)valuesDI.GetValue(i);
                    DI ei = (DI)value;
                    // DI ei = (DI)Enum.Parse(typeof(DI), value.ToString());
                    int index = 0;
                    int diff = 0;


                    index = value / 32;
                    diff = value % 32;

                    uint a = 1;

                    dic_DI[ei] = ((di[index] & (a << diff)) == 0);
                     
                    }
                    catch (Exception ex)
                    {

                        ShowInfo("------------");
                    }

                }

            }

        }
        Array valuesDo;
            //获取输出状态
        private void updateDO()
            {
                UInt32[] dOut = new UInt32[HDO32];
              
                if (bMInit)
                {
                    
                    for (int iCard = 0; iCard < HDO32; iCard++)
                    {
                        dOut[iCard] = LTDMC.dmc_read_outport(0, (ushort)(DOStartNo + iCard));
                    }
                    if (valuesDo == null)
                        valuesDo = Enum.GetValues(typeof(DO));

                    int outputNum = valuesDo.Length;

                    for (int i = 0; i < outputNum; i++)
                    {

                            int value = (int)valuesDo.GetValue(i);
                            DO eo = (DO)value;
                           // DO eo = (DO)Enum.Parse(typeof(DO), value.ToString());
                            if (value < 192)
                            {
                                int index = 0;
                                int diff = 0;

                                index = value / 32;
                                diff = value % 32;

                                uint a = 1;
                                dic_DO[eo] = ((dOut[index] & (a << diff)) == 0);
                            }
                            else
                            {
                                dic_DO[eo] = (LTDMC.dmc_read_outbit(0, (ushort)value) == 0);
                            }
                    }

                }
               
            }
        private void updateDOX16()
        {
            int len = HDI16HDO16 / 2 + HDI16HDO16 % 2;
            UInt32[] dOut = new UInt32[len];

            if (bMInit)
            {

                for (int iCard = 0; iCard < len; iCard++)
                {
                    dOut[iCard] = LTDMC.dmc_read_outport(0, (ushort)(DOStartNo + iCard));
                }
                if (valuesDo == null)
                    valuesDo = Enum.GetValues(typeof(DO));

                int outputNum = valuesDo.Length;

                for (int i = 0; i < outputNum; i++)
                {

                    int value = (int)valuesDo.GetValue(i);
                    DO eo = (DO)value;
                    // DO eo = (DO)Enum.Parse(typeof(DO), value.ToString());
                   
                        int index = 0;
                        int diff = 0;

                        index = value / 32;
                        diff = value % 32;

                        uint a = 1;
                        dic_DO[eo] = ((dOut[index] & (a << diff)) == 0);
                   
                }

            }

        }
        public override void setDO(DO d, bool bValue)
        {
            if (bMInit)
            {
                int value = (int)d;

                if (value >= 192)
                {
                    ushort outb = 1;
                    if (bValue)
                        outb = 0;

                    LTDMC.dmc_write_outbit(0, (ushort)value, outb);
                    return;
                }

                int index = 0;
                int diff = 0;

                //index = value / 32;
                //diff = value % 32;
                index = value / 32;
                diff = value % 32;
                
                if (bValue)
                {
                    uint b = 1;
                    uint o = (uint)iSetDO[index];
                    iSetDO[index] = (uint)(o & (~(b << diff)));
                }
                else
                {
                    uint a = 1;
                    uint o = (uint)iSetDO[index];
                    iSetDO[index] = (uint)(o | (a << diff));
                   
                }
            }
        }

        public override void getAxisStatus(AXIS id)
        {
            if(!bMInit)
                return;

            ushort cardID = GetCardIdByAxis(id);
            ushort axisId = (ushort)id;
            uint ioStatus = 0;
         
            AXStatus ax = dic_Axis[id];
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            ioStatus =  LTDMC.dmc_axis_io_status(cardID, axisId );
            uint a= 1;
            ax.ALM = (ioStatus & a) > 0;
                if (dic_AbsAxis.Keys.Contains(id)) {
                    ax.PEL = (ioStatus & (a << 6)) > 0;
                    ax.MEL = (ioStatus & (a << 7)) > 0;
                }
                else
                {
                    ax.PEL = (ioStatus & (a << 1)) > 0;
                    ax.MEL = (ioStatus & (a << 2)) > 0;
                }
                ax.ORG = (ioStatus & (a << 4)) > 0;
                ax.EMG = ((ioStatus & (a << 3)) > 0);
               
                //ax.EZ = (ioStatus & 0x0020) > 0;
                ax.INP = IsAxisINP(id);
            short enable = LTDMC.dmc_get_sevon_enable(cardID, axisId);
                ax.SVON = (enable==1);
                //ax.RDY = (ioStatus & 0x0100) > 0;
           
            double dEncodPos = 0;
            LTDMC.dmc_get_encoder_unit(cardID, axisId, ref dEncodPos);
            ax.dPos = dEncodPos/dic_AxisRatio[id];
            double dPos = 0;
            LTDMC.dmc_get_position_unit(cardID, axisId, ref dPos);
            ax.dCmdPos = dPos / dic_AxisRatio[id];

            dic_Axis[id] = ax;
        }

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
        public override void SetEncoderValue(AXIS axis, double dSetValue)
        {
            if (!bMInit)
                return;

            ushort cardID = GetCardIdByAxis(axis);
            ushort axisId = (ushort)axis;
            

            AXStatus ax = dic_Axis[axis];
            if (axisId < 1000)
                axisId = (ushort)(1000 + axisId);
            axisId = (ushort)(AxisStartNumInCard[cardID] + axisId);
            double value = dSetValue * dic_AxisRatio[axis];
            LTDMC.dmc_set_encoder_unit(cardID, axisId, value);
            LTDMC.dmc_set_position_unit(cardID, axisId, value);

        }

        public override void HomeZ(AXIS axis)
        {
            throw new NotImplementedException();
        }

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
              
                short res = LTDMC.dmc_board_close();
                bMInit = false;
                bSVON = false;
            }
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
