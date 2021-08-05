using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Assembly
{
    public partial class FrmAlarmDialog : Form
    {
        int iCurrentStep = 0;//报警的步骤
        int iStepLen = 0;//允许调试的长度
        public string strAlarmInfo = "";//报警信息
        //int iAlarmBlock = 0;
        AlarmBlock alarmBlock;
        bool bNext = false;//是否取下一颗料,可选项
        SynchronizationContext _context = null;
        Thread th_updateUI = null;
        public FrmAlarmDialog()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }
        public FrmAlarmDialog(int _iCurrentStep, int _iStepLen, string _strAlarmInfo, AlarmBlock _alarmBlcok, bool _bNext)
        {
            iCurrentStep = _iCurrentStep;
            iStepLen = _iStepLen;
            strAlarmInfo = _strAlarmInfo;
            alarmBlock = _alarmBlcok;
            bNext = _bNext;
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!Alarminfo.BAlarm)
                return;

            Run.iDebugResult = 0;
            Run.bReset = true;
        }

        private void FrmAlarmDialog_Load(object sender, EventArgs e)
        {
            Run.runMode = RunMode.暂停;
            th_updateUI = new Thread(UpdateC);
            th_updateUI.Start();
        }
        public void UpdateC()
        {

            while (true)
            {
                try
                {
                    _context.Post(UpdateUI, null);
                }
                catch (Exception)
                {
                    
                   
                }
                System.Threading.Thread.Sleep(100);
            }
        }
        public void UpdateUI(Object o)
        {
            try
            {

              
                if (Alarminfo.BAlarm||(Run.iDebugResult == 1))
                {
                    gpProcess.Enabled = false;
                   
                }
                else {
                    gpProcess.Enabled = true;
                    if (bNext)
                        btnNext.Enabled = true;
                    else
                        btnNext.Enabled = false;
                }
                if (Run.iDebugResult == 1)
                {
                    lblResult.BackColor = Color.Green;
                    lblResult.Text = "OK";
                }
                else if (Run.iDebugResult == 2)
                {
                    lblResult.BackColor = Color.Red;
                    lblResult.Text = "NG";
                }
                else
                {
                    lblResult.BackColor = Color.Yellow;
                    lblResult.Text = "";
                }
                lblAlarmInfo.Text = alarmBlock.ToString() +"--"+ strAlarmInfo;
            }
            catch (Exception)
            {

               
            }
        }
        private void btnRepeat_Click(object sender, EventArgs e)
        {
           
            Run.iDebugBlock = alarmBlock;
            
            Run.bSetStep = true;
            Run.iDebugFlagStep = iCurrentStep;
            Run.iDebugStartStep = iCurrentStep - iStepLen;
            Run.bDebugException = true;
            Run.iDebugResult = 0;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int iNum =(int)nudNum.Value;

            if (alarmBlock == AlarmBlock.取料1)
            {

                CommonSet.dic_OptSuction1[GetProduct1Module.iCurrentSuction].CurrentPos = CommonSet.dic_OptSuction1[GetProduct1Module.iCurrentSuction].CurrentPos + iNum;

            }
            else if (alarmBlock == AlarmBlock.取料2)
            {
                CommonSet.dic_OptSuction2[GetProduct2Module.iCurrentSuction].CurrentPos = CommonSet.dic_OptSuction2[GetProduct2Module.iCurrentSuction].CurrentPos + iNum;
            }
            else if (alarmBlock == AlarmBlock.镜筒和点胶)
            {
                CommonSet.dic_BarrelSuction[1].CurrentPos = CommonSet.dic_BarrelSuction[1].CurrentPos + iNum;
            }
            Run.iDebugBlock = alarmBlock;
            
            Run.bSetStep = true;
            Run.iDebugFlagStep = iCurrentStep;
            Run.iDebugStartStep = iCurrentStep - iStepLen;
            Run.bDebugException = true;
            Run.iDebugResult = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAlarmDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (th_updateUI.IsAlive)
                    th_updateUI.Abort();
                th_updateUI = null;
            }
            catch (Exception)
            {
                
              
            }
        }

        private void btnForceOK_Click(object sender, EventArgs e)
        {
            CommonSet.bForceOk = true;
            this.Close();
        }
    }
}
