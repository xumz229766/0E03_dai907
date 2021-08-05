using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Tray;
using System.Threading;
using CameraSet;
using Motion;
using Tray;
using HalconDotNet;
using ConfigureFile;
using System.Diagnostics;
namespace _0E03
{
    public partial class FrmMain : UIPage
    {
        public static int iCurrentTrayIndexL = 1;
        public static Tray.TrayPanel trayPL = null;
        public static int iCurrentTrayIndexR = 1;
        public static Tray.TrayPanel trayPR = null;
        public static int iCurrentTrayIndexB = 1;
        public static Tray.TrayPanel trayPB = null;
        SynchronizationContext _context = null;
        MotionCard mc = null;
        public static HWindow hwinUpL;
        public static HWindow hwinDownL;
        public static HWindow hwinUpR;
        public static HWindow hwinDownR;
        public static HWindow hwinUpB;
        Alarminfo alarm;
        Run run = null;
        public FrmMain()
        {
            InitializeComponent();

            _context = SynchronizationContext.Current;
        }

        private void FrmUIPages_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            CommonSet.showMsg += ShowMsg;

            CommonSet.WriteInfo("加载主界面成功！");
            
            hwinUpL = hWindowControl1.HalconWindow;
            hwinDownL = hWindowControl2.HalconWindow;
            hwinUpB = hWindowControl3.HalconWindow;
            hwinDownR = hWindowControl4.HalconWindow;
            hwinUpR = hWindowControl5.HalconWindow;

            trayPL = trayPanelL;
            trayPR = trayPanelR;
            trayPB = trayPanelBarrel;
            SuctionL.InitTrayPanel(trayPL, 2,CommonSet.ColorInit);
            SuctionL.InitTrayPanel(trayPL, 1,CommonSet.ColorInit);
            trayPL.ShowTray(SuctionL.lstTray[0]);

            
            SuctionR.InitTrayPanel(trayPR, 2,CommonSet.ColorInit);
            SuctionR.InitTrayPanel(trayPR, 1, CommonSet.ColorInit);
            trayPR.ShowTray(SuctionR.lstTray[0]);

            Barrel.InitTrayPanel(trayPB, 2, CommonSet.ColorInit);
            Barrel.InitTrayPanel(trayPB, 1, CommonSet.ColorInit);
            trayPB.ShowTray(Barrel.lstTray[0]);
            alarm = new Alarminfo();
            run = Run.getInstance();
            mc = MotionCard.getMotionCard();


            mc.ShowInfo += CommonSet.WriteInfo;
            mc.initCard(Application.StartupPath + "\\");
            mc.SeverOnAll();

        }
        private void ShowMsg(string info)
        {
            try
            {
                _context.Post(ShowText, info);
            }
            catch (Exception)
            {

               
            }
           
        }
        private void ShowText(object o)
        {
            try
            {
                if (txtInfo != null)
                    txtInfo.AppendText(o.ToString());
            }
            catch (Exception)
            {
            }
          
        }
        bool bForceL = false;//强制有盘
        bool bForceR = false;//强制有盘
        bool bForceB = false;//强制有盘
        Stopwatch swL = new Stopwatch();
        Stopwatch swR = new Stopwatch();
        Stopwatch swB1 = new Stopwatch();
        Stopwatch swB2 = new Stopwatch();
        public void UpdateUI()
        {
            try
            {
                txtProductName.Text = ModelManager.strProductName;
                txtAngleL.Text = ModelManager._AssemParam.dAssemSetAngL.ToString();
                txtAngleR.Text = ModelManager._AssemParam.dAssemSetAngR.ToString();
                txtPressureL.Text = ModelManager._AssemParam.dAssemSetPreL.ToString();
                txtPressureR.Text = ModelManager._AssemParam.dAssemSetPreR.ToString();
                lblRunState.Text = Run.runMode.ToString();
                txtCircleTime.Text = (Run.lAssemTime / 1000.0).ToString("0.00");
                ledStateL.On = ModelManager.SuctionLParam.BUse;
                ledStateR.On = ModelManager.SuctionRParam.BUse;
                if (Run.runMode == RunMode.运行)
                {
                    SetLight(lightUpL, ModelManager.SuctionLParam.imgResultUp.bImageResult);
                    SetLight(lightDownL, ModelManager.SuctionLParam.imgResultDown.bImageResult);
                    SetLight(lightUpR, ModelManager.SuctionRParam.imgResultUp.bImageResult);
                    SetLight(lightDownR, ModelManager.SuctionRParam.imgResultDown.bImageResult);
                    SetLight(lightBarrelL, ModelManager.BarrelParam.imgResultUp.bImageResult);

                    lblAngleUpL.Text = ModelManager.SuctionLParam.imgResultUp.dAngle.ToString();
                    lblAngleDownL.Text = ModelManager.SuctionLParam.imgResultDown.dAngle.ToString();
                    lblAngleUpR.Text = ModelManager.SuctionRParam.imgResultUp.dAngle.ToString();
                    lblAngleDownR.Text = ModelManager.SuctionRParam.imgResultDown.dAngle.ToString();

                    if (nudTrayNumL.Value != ModelManager.SuctionLParam.iCurrentTrayIndex)
                    {
                        trayPL.ShowTray(SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1]);
                    }
                    if (nudTrayNumR.Value != ModelManager.SuctionRParam.iCurrentTrayIndex)
                    {
                        trayPR.ShowTray(SuctionR.lstTray[ModelManager.SuctionRParam.iCurrentTrayIndex - 1]);
                    }
                    if (nudTrayNumBarrel.Value != ModelManager.BarrelParam.iCurrentTrayIndex)
                    {
                        trayPB.ShowTray(Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1]);
                    }
                }

                lblProductGetPosL.Text = SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1].CurrentPos.ToString();
                lblStartPosL.Text = SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1].StartPos.ToString();
                lblEndPosL.Text = SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1].EndPos.ToString();

                lblProductGetPosBarrel.Text = Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1].CurrentPos.ToString();
                lblStartPosBarrel.Text = Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1].StartPos.ToString();
                lblEndPosBarrel.Text = Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1].EndPos.ToString();

                lblProductGetPosR.Text = SuctionR.lstTray[ModelManager.SuctionRParam.iCurrentTrayIndex - 1].CurrentPos.ToString();
                lblStartPosR.Text = SuctionR.lstTray[ModelManager.SuctionRParam.iCurrentTrayIndex - 1].StartPos.ToString();
                lblEndPosR.Text = SuctionR.lstTray[ModelManager.SuctionRParam.iCurrentTrayIndex - 1].EndPos.ToString();

                //nudTrayNumL.Value = ModelManager.SuctionLParam.iCurrentTrayIndex;
                //nudTrayNumBarrel.Value = ModelManager.BarrelParam.iCurrentTrayIndex;
                //nudTrayNumR.Value = ModelManager.SuctionRParam.iCurrentTrayIndex;

                setNumerialControl(nudTrayNumL, ModelManager.SuctionLParam.iCurrentTrayIndex);
                setNumerialControl(nudTrayNumBarrel, ModelManager.BarrelParam.iCurrentTrayIndex);
                setNumerialControl(nudTrayNumR, ModelManager.SuctionRParam.iCurrentTrayIndex);
                int iAlarmCount = Alarminfo.GetAlarmList().Count;
                panelAlarm.Text = "报警信息("+iAlarmCount.ToString()+"条)" ;

                #region"托盘操作和显示按钮"
                if (mc.dic_DI[DI.镜片盘1有料感应] || bForceL)
                {
                    ModelManager.SuctionLParam.bExitTray[0] = true;
                    ModelManager.SuctionLParam.bExitTray[1] = true;
                    //感应器断开1秒以上，再次放盘时,自动初始化托盘
                    if (swL.ElapsedMilliseconds > 1000)
                    {
                        InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorOK, 0);
                        InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorOK, 0);
                        ModelManager.SuctionLParam.iCurrentTrayIndex = 1;
                        SuctionL.lstTray[0].bFinish = false;
                        SuctionL.lstTray[1].bFinish = false;
                    }
                    swL.Stop();
                    swL.Reset();
                   
                }
                else
                {
                    swL.Start();
                    if (swL.ElapsedMilliseconds > 1000)
                    {
                        ModelManager.SuctionLParam.bExitTray[0] = false;
                        ModelManager.SuctionLParam.bExitTray[1] = false;
                        uiTrayFlagL1.BackColor = Color.Gray;
                        uiTrayFlagL2.BackColor = Color.Gray;

                        if (swL.ElapsedMilliseconds < 1500) { 
                            InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorInit, 0);
                            InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorInit, 0);
                        }
                    }
                  
                }

                if (mc.dic_DI[DI.镜片盘2有料感应] || bForceR)
                {
                    ModelManager.SuctionRParam.bExitTray[0] = true;
                    ModelManager.SuctionRParam.bExitTray[1] = true;

                    //感应器断开1秒以上，再次放盘时,自动初始化托盘
                    if (swR.ElapsedMilliseconds > 1000)
                    {
                        InitTrayPanel(trayPR, SuctionR.lstTray[0], 1, CommonSet.ColorOK, 1);
                        InitTrayPanel(trayPR, SuctionR.lstTray[1], 2, CommonSet.ColorOK, 1);
                        ModelManager.SuctionRParam.iCurrentTrayIndex = 1;
                        SuctionR.lstTray[0].bFinish = false;
                        SuctionR.lstTray[1].bFinish = false;
                    }
                    swR.Stop();
                    swR.Reset();
                }
                else
                {
                    swR.Start();
                    if (swR.ElapsedMilliseconds > 1000)
                    { 
                        ModelManager.SuctionRParam.bExitTray[0] = false;
                        ModelManager.SuctionRParam.bExitTray[1] = false;
                        uiTrayFlagR1.BackColor = Color.Gray;
                        uiTrayFlagR2.BackColor = Color.Gray;

                        if (swR.ElapsedMilliseconds < 1500)
                        {
                            InitTrayPanel(trayPR, SuctionR.lstTray[0], 1, CommonSet.ColorInit, 1);
                            InitTrayPanel(trayPR, SuctionR.lstTray[1], 2, CommonSet.ColorInit, 1);
                        }
                    }


                }
                if (mc.dic_DI[DI.镜筒盘1有料感应] || bForceB)
                {
                    ModelManager.BarrelParam.bExitTray[0] = true;
                    //感应器断开1秒以上，再次放盘时,自动初始化托盘
                    if (swB1.ElapsedMilliseconds > 1000)
                    {
                        InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorInit, 2);
                        //ModelManager.BarrelParam.iCurrentTrayIndex = 1;
                        Barrel.lstTray[0].bFinish = false;
                       
                      }
                    swB1.Stop();
                    swB1.Reset();
                }
                else
                {
                    swB1.Start();
                    if (swB1.ElapsedMilliseconds > 1000)
                    {
                        ModelManager.BarrelParam.bExitTray[0] = false;
                        uiTrayFlagB1.BackColor = Color.Gray;
                        if(swB1.ElapsedMilliseconds<1500)
                            InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorInit, 2);
                    }
                }
                if (mc.dic_DI[DI.镜筒盘2有料感应] || bForceB)
                {
                   
                    ModelManager.BarrelParam.bExitTray[1] = true;
                    //感应器断开1秒以上，再次放盘时,自动初始化托盘
                    if (swB2.ElapsedMilliseconds > 1000)
                    {
                        InitTrayPanel(trayPB, Barrel.lstTray[1], 2, CommonSet.ColorInit, 2);
                        //ModelManager.BarrelParam.iCurrentTrayIndex = 1;
                        Barrel.lstTray[1].bFinish = false;

                    }
                    swB2.Stop();
                    swB2.Reset();
                }
                else
                {
                    swB2.Start();
                    if (swB2.ElapsedMilliseconds > 1000)
                    {
                        ModelManager.BarrelParam.bExitTray[1] = false;
                        uiTrayFlagB2.BackColor = Color.Gray;
                        if (swB2.ElapsedMilliseconds < 1500)
                            InitTrayPanel(trayPB, Barrel.lstTray[1], 1, CommonSet.ColorInit, 2);
                    }
                }
                //左托盘
                if (ModelManager.SuctionLParam.bExitTray[0])
                {

                    uiTrayFlagL1.BackColor = GetColor(SuctionL.lstTray[0].bFinish);
                    uiTrayFlagL2.BackColor = GetColor(SuctionL.lstTray[1].bFinish);
                    if (SuctionL.lstTray[1].bFinish)
                    {
                        bForceL = false;
                    }

                }
                //右托盘
                if (ModelManager.SuctionRParam.bExitTray[0])
                {
                    uiTrayFlagR1.BackColor = GetColor(SuctionR.lstTray[0].bFinish);
                    uiTrayFlagR2.BackColor = GetColor(SuctionR.lstTray[1].bFinish);

                    if ( SuctionR.lstTray[1].bFinish)
                    {
                        bForceR = false;
                    }
                }
                //镜筒
                if (ModelManager.BarrelParam.bExitTray[0])
                {
                    uiTrayFlagB1.BackColor = GetColor(Barrel.lstTray[0].bFinish);
                    
                }
                if (ModelManager.BarrelParam.bExitTray[1])
                {
                  
                    uiTrayFlagB2.BackColor = GetColor(Barrel.lstTray[1].bFinish);
                }
                if (Barrel.lstTray[0].bFinish && Barrel.lstTray[1].bFinish)
                {
                    bForceB = false;
                }
                #endregion

                #region"操作按钮显示"
                if (Run.runMode == RunMode.运行)
                {
                    btnHand.Style = UIStyle.Office2010Silver;
                    btnStart.Style = UIStyle.Green;
                    if (Run.bReset)
                        btnReset.Style = UIStyle.Green;
                    else
                        btnReset.Style = UIStyle.Office2010Silver;
                    btnStop.Enabled = true;
                    btnStep.Enabled = true;
                    btnNext.Enabled = Run.bRunStep;
                    btnStop.Style = UIStyle.Office2010Silver;

                    if (Run.bRunStep)
                    {
                        btnNext.Style = UIStyle.Office2010Silver;
                        btnStep.Style = UIStyle.Green;
                    }
                    else
                    {
                        btnNext.Style = UIStyle.LightGray;
                        btnStep.Style = UIStyle.Office2010Silver;
                    }
                }
                else if (Run.runMode == RunMode.暂停)
                {
                    btnHand.Style = UIStyle.Office2010Silver;
                    btnStart.Style = UIStyle.Office2010Silver;
                    if(Run.bReset)
                        btnReset.Style = UIStyle.Green;
                    else
                         btnReset.Style = UIStyle.Office2010Silver;
                    btnStop.Enabled = true;
                    btnStep.Enabled = true;
                    btnNext.Enabled = Run.bRunStep;
                    btnStop.Style = UIStyle.Green;
                    btnStep.Style = UIStyle.Office2010Silver;
                    if (Run.bRunStep)
                        btnNext.Style = UIStyle.Office2010Silver;
                    else
                        btnNext.Style = UIStyle.LightGray;
                }
                else
                {
                    btnHand.Style = UIStyle.Green;
                    btnStart.Style = UIStyle.Office2010Silver;
                    btnReset.Style = UIStyle.Office2010Silver;
                    btnStop.Enabled = false;
                    btnStep.Enabled = false;
                    btnNext.Enabled = false;
                    btnStop.Style = UIStyle.LightGray;
                    btnStep.Style = UIStyle.LightGray;
                    btnNext.Style = UIStyle.LightGray;
                }
                if (Run.bHome)
                {
                    btnHome.Style = UIStyle.LightOrange;
                    btnHome.Text = "回原点中";
                }
                else
                {
                    //添加轴
                    string[] values = Enum.GetNames(typeof(AXIS));
                    bool bHomeFlag = true;
                    foreach (string value in values)
                    {
                        AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                        if (mc.dic_HomeStatus.Keys.Contains(axis))
                            bHomeFlag = bHomeFlag && mc.dic_HomeStatus[axis];
                        
                    }
                    if (bHomeFlag)
                    {
                        btnHome.Style = UIStyle.LightGreen;
                        
                    }
                    else
                    {
                        btnHome.Style = UIStyle.LightRed;
                    }
                    btnHome.Text = "回原点";
                }


                #endregion
                try
                {
                    ShowAlarm();
                }
                catch (Exception ex)
                {

                    CommonSet.WriteDebug("更新baojing", ex);
                }
                
              
            }
            catch (Exception ex)
            {

                CommonSet.WriteDebug("更新主界面异常", ex);
            }
        }
       
        private Dictionary<Alarm, DateTime> dictAlarm = new Dictionary<Alarm, DateTime>();

        DateTime showAlarmTime = new DateTime();
        private void ShowAlarm()
        {
           
            List<Alarm> lstAlarmAll = Alarminfo.GetAlarmList();
           
            int count = lstAlarmAll.Count;
            //CommonSet.WriteInfo("获取报警数"+ count.ToString());
            int len = lstAlarmInfo.Items.Count;
            if (count != len)
            {
                lstAlarmInfo.Items.Clear();
                foreach (Alarm alarm in lstAlarmAll)
                {
                    try
                    {

                   
                        lstAlarmInfo.Items.Add(alarm.ToString());
                        if (dictAlarm.ContainsKey(alarm))
                        {
                            dictAlarm[alarm] = DateTime.Now;
                        }
                        else
                        {
                            //txtAllAlarm.AppendText(DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + alarm.ToString() + "\r\n");
                            IniOperate.INIWriteValue(CommonSet.strAlarmFile, "Alarm", DateTime.Now.ToString("yy-MM-dd HH:mm:ss ") + DateTime.Now.Millisecond.ToString(), alarm.ToString());
                            dictAlarm.Add(alarm, DateTime.Now);
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonSet.WriteDebug("更新baojing1", ex);

                    }
                }
            }
           
            //List<Alarm> lst = dictAlarm.Keys.ToList();
            //foreach (Alarm l in lst)
            //{
            //    if (dictAlarm.ContainsKey(l)) {
            //        TimeSpan ts = DateTime.Now - dictAlarm[l];
            //        if (ts.Seconds > 3)
            //        {
            //            dictAlarm.Remove(l);
            //        }
            //      }
            //}
            foreach (KeyValuePair<Alarm, DateTime> pair in dictAlarm)
            {
                try
                {
                    TimeSpan ts = DateTime.Now - pair.Value;
                    if (ts.Seconds > 5)
                    {
                        dictAlarm.Remove(pair.Key);
                        break;
                    }
                }
                catch (Exception ex)
                {

                    CommonSet.WriteDebug("更新baojing2", ex);
                }
                
            }
        }
        private void SetLight(UILight light, bool value)
        {
            if (value)
            {
                light.State = UILightState.On;
            }
            else
            {
                light.State = UILightState.Off;
            }
        }

        private Color GetColor(bool value)
        {
            if (value)
                return Color.Red;
            else
                return Color.Green;
        }
        private void setNumerialControl(NumericUpDown nud, double value)
        {
            if (!nud.Focused)
            {
                if (value < (double)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (double)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        private void setNumerialControl(NumericUpDown nud, int value)
        {
            if (!nud.Focused)
            {
                if (value < (int)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (int)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        private void setNumerialControl(UIDoubleUpDown nud, double value)
        {
            if (!nud.Focused)
            {
                if (value < (double)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (double)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = value;
            }

        }
        private void setNumerialControl(UIIntegerUpDown nud, int value)
        {
            if (!nud.Focused)
            {
                if (value < (int)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (int)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = value;
            }

        }
        private void nudTrayNumL_ValueChanged(object sender, int value)
        {
           
            if (Run.runMode == RunMode.运行)
                return;
            ModelManager.SuctionLParam.iCurrentTrayIndex = nudTrayNumL.Value;
            trayPL.ShowTray(SuctionL.lstTray[ModelManager.SuctionLParam.iCurrentTrayIndex - 1]);
            //SuctionL.lstTray[iCurrentTrayIndexL - 1].updateColor();
        }

        private void nudTrayNumR_ValueChanged(object sender, int value)
        {
           
            if (Run.runMode == RunMode.运行)
                return;

            ModelManager.SuctionRParam.iCurrentTrayIndex = nudTrayNumR.Value;
            trayPR.ShowTray(SuctionR.lstTray[ModelManager.SuctionRParam.iCurrentTrayIndex - 1]);
            //SuctionR.InitTrayPanel(trayPR, iCurrentTrayIndexR);

        }

        private void nudTrayNumBarrel_ValueChanged(object sender, int value)
        {
          
            if (Run.runMode == RunMode.运行)
                return;
            ModelManager.BarrelParam.iCurrentTrayIndex = nudTrayNumBarrel.Value;
            trayPB.ShowTray(Barrel.lstTray[ModelManager.BarrelParam.iCurrentTrayIndex - 1]);
            // Barrel.InitTrayPanel(trayPB, iCurrentTrayIndexB);

        }
   
        private void btnHome_Click(object sender, EventArgs e)
        {
            
            if (Run.runMode != RunMode.手动)
                return;
            if (Run.bHome) {
                if (MessageBox.Show("是否停止回原点", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mc.bHome = false;
                    Run.bHome = false;
                    mc.StopAllAxis();
                 
                }
                return;

            }
               
         
            CommonSet.WriteInfo("开始回原点");
            //添加轴
            string[] values = Enum.GetNames(typeof(AXIS));
            foreach (string value in values)
            {
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), value);
                if (mc.dic_HomeStatus.Keys.Contains(axis))
                    mc.dic_HomeStatus[axis] = false;
                if (!mc.dic_Axis[axis].SVON)
                {
                    mc.SeverOnAll();
                }
            }
            List<Alarm> lst = new List<Alarm>();
            lst.AddRange(Alarminfo.GetAlarmList());
            for (int i = 0; i < 10; i++)
            {
                lst.Remove(Alarm.镜筒Y轴原点丢失 + i);
                //lst.Remove(Alarm.Z1轴原点丢失 + i);
            }
           
            if (lst.Count > 0)
            {
                Run.bHome = false;
                MessageBox.Show("有其它异常报警，请检查并复位清除！");
                return;
            }
            mc.bHome = true;
            Run.bHome = true;
            mc.Home(AXIS.组装Z1轴);
            mc.Home(AXIS.组装Z2轴);
           
        }

        private void btnProductSet_Click(object sender, EventArgs e)
        {

        }

        private void lblProductGetPosL_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }
          
            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumL.Value, "取料位","左托盘");
            fsd.ShowDialog();
            fsd = null;
            int index = ModelManager.SuctionLParam.iCurrentTrayIndex - 1;
            int start = SuctionL.lstTray[index].CurrentPos;
            int end = SuctionL.lstTray[index].EndPos;

            SuctionL.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionL.lstTray[index].updateColor();
        }

        private void lblStartPosL_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumL.Value, "起始位", "左托盘");
            fsd.ShowDialog();
            fsd = null;
            int index = ModelManager.SuctionLParam.iCurrentTrayIndex - 1;
            int start = SuctionL.lstTray[index].StartPos;
            int end = SuctionL.lstTray[index].EndPos;
            SuctionL.lstTray[index].CurrentPos = start;
            SuctionL.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionL.lstTray[index].updateColor();
        }

        private void lblEndPosL_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumL.Value, "结束位", "左托盘");
            fsd.ShowDialog();
            fsd = null;
            int index = ModelManager.SuctionLParam.iCurrentTrayIndex - 1;
            int start = SuctionL.lstTray[index].StartPos;
            int end = SuctionL.lstTray[index].EndPos;

            SuctionL.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionL.lstTray[index].updateColor();
        }

        private void lblProductGetPosBarrel_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumBarrel.Value, "取料位", "镜筒托盘");
            fsd.ShowDialog();
            fsd = null;

            int index = ModelManager.BarrelParam.iCurrentTrayIndex - 1;
            int start = Barrel.lstTray[index].CurrentPos;
            int end = Barrel.lstTray[index].EndPos;
            
            Barrel.lstTray[index].setStartEndPos(start, end, CommonSet.ColorInit, CommonSet.ColorNotUse);
            Barrel.lstTray[index].updateColor();
        }

        private void lblStartPosBarrel_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumBarrel.Value, "起始位", "镜筒托盘");
            fsd.ShowDialog();
            fsd = null;
            int index = ModelManager.BarrelParam.iCurrentTrayIndex - 1;
            int start = Barrel.lstTray[index].StartPos;
            int end = Barrel.lstTray[index].EndPos;
            Barrel.lstTray[index].CurrentPos = start;
            Barrel.lstTray[index].setStartEndPos(start, end, CommonSet.ColorInit, CommonSet.ColorNotUse);
            Barrel.lstTray[index].updateColor();
        }

        private void lblEndPosBarrel_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumBarrel.Value, "结束位", "镜筒托盘");
            fsd.ShowDialog();
            fsd = null;
            int index = ModelManager.BarrelParam.iCurrentTrayIndex - 1;
            int start = Barrel.lstTray[index].StartPos;
            int end = Barrel.lstTray[index].EndPos;

            Barrel.lstTray[index].setStartEndPos(start, end, CommonSet.ColorInit, CommonSet.ColorNotUse);
            Barrel.lstTray[index].updateColor();
        }

        private void lblProductGetPosR_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumR.Value, "取料位", "右托盘");
            fsd.ShowDialog();
            fsd = null;

            int index = ModelManager.SuctionRParam.iCurrentTrayIndex - 1;
            int start = SuctionR.lstTray[index].CurrentPos;
            int end = SuctionR.lstTray[index].EndPos;

            SuctionR.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionR.lstTray[index].updateColor();
        }

        private void lblStartPosR_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumR.Value, "起始位", "右托盘");
            fsd.ShowDialog();
            fsd = null;

            int index = ModelManager.SuctionRParam.iCurrentTrayIndex - 1;
            int start = SuctionR.lstTray[index].StartPos;
            int end = SuctionR.lstTray[index].EndPos;

            SuctionR.lstTray[index].CurrentPos = start;

            SuctionR.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionR.lstTray[index].updateColor();
        }

        private void lblEndPosR_DoubleClick(object sender, EventArgs e)
        {
            if (RunMode.运行 == Run.runMode)
            {
                MessageBox.Show("请停止运行！");
                return;
            }

            //int index = Convert.ToInt32(name.Substring(13));
            FrmSetDialog fsd = new FrmSetDialog(nudTrayNumR.Value, "结束位", "右托盘");
            fsd.ShowDialog();
            fsd = null;

            int index = ModelManager.SuctionRParam.iCurrentTrayIndex - 1;
            int start = SuctionR.lstTray[index].StartPos;
            int end = SuctionR.lstTray[index].EndPos;

            SuctionR.lstTray[index].setStartEndPos(start, end, CommonSet.ColorOK, CommonSet.ColorInit);
            SuctionR.lstTray[index].updateColor();
        }

        public void InitTrayPanel(TrayPanel tp, Tray.Tray t,int index,Color c,int pos = 0)
        {
            t.CurrentPos = 1;
            t.StartPos = 1;
            t.EndPos = t.dic_Index.Count;
            if (pos == 0)
            {
                SuctionL.InitTrayPanel(tp, index, c);
                
            }
            else if (pos == 1)
            {
                SuctionR.InitTrayPanel(tp, index, c);

            }
            else if (pos == 2)
                Barrel.InitTrayPanel(tp, index, c);
        }

        private void btnInitTrayL_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
            {
                return;
            }
            bForceL = true;
            //if (nudTrayNumL.Value == 1)
            //{
            //    InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorOK,0);

            //    InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorOK, 0);

            //}
            //else
            //{
            //    InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorOK, 0);
            //    InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorOK, 0);

            //}
            //ModelManager.SuctionLParam.iCurrentTrayIndex = 1;
            //SuctionL.lstTray[0].bFinish = false;
            //SuctionL.lstTray[1].bFinish = false;
        }
        private void btnInitTrayBarrel_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
            {
                return;
            }
            bForceB = true;
            ModelManager.BarrelParam.iCurrentTrayIndex = 1;
            //if (nudTrayNumBarrel.Value == 1)
            //{

            //    InitTrayPanel(trayPB, Barrel.lstTray[1], 2, CommonSet.ColorInit, 2);

            //    InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorInit, 2);



            //}
            //else
            //{



            //    InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorInit, 2);
            //    InitTrayPanel(trayPB, Barrel.lstTray[1], 2, CommonSet.ColorInit, 2);

            //}

            //Barrel.lstTray[0].bFinish = false;
            //Barrel.lstTray[1].bFinish = false;
        }

        private void btnInitTrayR_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
            {
                return;
            }
            bForceR = true;
            //if (nudTrayNumR.Value == 1)
            //{
            //    InitTrayPanel(trayPR, SuctionR.lstTray[1], 2, CommonSet.ColorOK,1);

            //    InitTrayPanel(trayPR, SuctionR.lstTray[0], 1, CommonSet.ColorOK, 1);

            //}
            //else
            //{
            //    InitTrayPanel(trayPR, SuctionR.lstTray[0], 1, CommonSet.ColorOK, 1);
            //    InitTrayPanel(trayPR, SuctionR.lstTray[1], 2, CommonSet.ColorOK, 1);

            //}
            //ModelManager.SuctionRParam.iCurrentTrayIndex = 1;
            //SuctionR.lstTray[0].bFinish = false;
            //SuctionR.lstTray[1].bFinish = false;
        }

        private void uiTrayFlagL1_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.SuctionLParam.bExitTray[0])
                return;
            if (SuctionL.lstTray[0].bFinish)
            {
                InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorOK,0);
                SuctionL.lstTray[0].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPL, SuctionL.lstTray[0], 1, CommonSet.ColorInit,0);
                SuctionL.lstTray[0].bFinish = true;
            }

        }

        private void uiTrayFlagL2_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.SuctionLParam.bExitTray[0])
                return;
            if (SuctionL.lstTray[1].bFinish)
            {
                InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorOK, 0);
                SuctionL.lstTray[1].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPL, SuctionL.lstTray[1], 2, CommonSet.ColorInit,0);
                SuctionL.lstTray[1].bFinish = true;
            }
        }

        private void uiTrayFlagB1_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.BarrelParam.bExitTray[0])
                return;
            if (Barrel.lstTray[0].bFinish)
            {
                InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorInit, 2);
                Barrel.lstTray[0].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPB, Barrel.lstTray[0], 1, CommonSet.ColorOK, 2);
                Barrel.lstTray[0].bFinish = true;
            }
        }

        private void uiTrayFlagB2_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.BarrelParam.bExitTray[1])
                return;
            if (Barrel.lstTray[1].bFinish)
            {
                InitTrayPanel(trayPB, Barrel.lstTray[1], 2, CommonSet.ColorInit, 2);
                Barrel.lstTray[1].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPB, Barrel.lstTray[1], 2, CommonSet.ColorOK, 2);
                Barrel.lstTray[1].bFinish = true;
            }
        }

        private void uiTrayFlagR1_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.SuctionRParam.bExitTray[0])
                return;
            if (SuctionR.lstTray[0].bFinish)
            {
                InitTrayPanel(trayPL, SuctionR.lstTray[0], 1, CommonSet.ColorOK, 1);
                SuctionR.lstTray[0].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPR, SuctionR.lstTray[0], 1, CommonSet.ColorInit, 1);
                SuctionR.lstTray[0].bFinish = true;
            }
        }

        private void uiTrayFlagR2_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
            if (!ModelManager.SuctionRParam.bExitTray[0])
                return;
            if (SuctionR.lstTray[1].bFinish)
            {
                InitTrayPanel(trayPL, SuctionR.lstTray[1], 2, CommonSet.ColorOK, 1);
                SuctionR.lstTray[1].bFinish = false;
            }
            else
            {
                InitTrayPanel(trayPR, SuctionR.lstTray[1], 2, CommonSet.ColorInit, 1);
                SuctionR.lstTray[1].bFinish = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.运行)
                return;
           
            if (Run.runMode == RunMode.暂停)
            {
                if (!Alarminfo.BAlarm)
                    return;
            }
            Run.camLModule.bResetFinish = false;
            Run.camRModule.bResetFinish = false;
            Run.assemLModule.bResetFinish = false;
            Run.assemRModule.bResetFinish = false;
            Run.barrelModule.bResetFinish = false;
            // Run.ResetStatus();
            Run.bReset = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Alarminfo.BAlarm)
                return;
            if(Run.runMode == RunMode.暂停)
            {
                Run.runMode = RunMode.运行;
                return;
            }

            if (Run.runMode == RunMode.手动)
            {
                Run.camLModule.bResetFinish = false;
                Run.camRModule.bResetFinish = false;
                Run.assemLModule.bResetFinish = false;
                Run.assemRModule.bResetFinish = false;
                Run.barrelModule.bResetFinish = false;
                Run.runMode = RunMode.运行;
                // Run.ResetStatus();
                //CommonSet.bResetOne = true;
                //Run.bReset = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.runMode = RunMode.暂停;
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            Run.bRunStep = !Run.bRunStep;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Run.bNext = true;
        }

        private void btnHand_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.手动)
                return;
            DialogResult dr = MessageBox.Show("是否回手动状态？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Run.runMode = RunMode.手动;
                
            }
        }
    }
}

