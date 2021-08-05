using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace Motion
{
    public partial class Form1 : Form
    {
        private static SynchronizationContext synContext = null;
        public MotionCard mc = null;
        public static string strStartPath = Application.StartupPath;
        public Form1()
        {
            mc = MotionCard.getMotionCard();
            InitializeComponent();
            synContext = SynchronizationContext.Current;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strFilePath = strStartPath + "\\7856.xml";
            
            mc.initCard(strFilePath);
        }
        private void DisplayInfo(object info) {
            try
            {
                if(textBox1 != null)
                     textBox1.AppendText(DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + " " + info.ToString() + "\r\n");
            }
            catch (Exception ex) { }
        }
        public void ShowInfo(string info)
        {
            try
            {
                synContext.Post(DisplayInfo, info);
            }
            catch (Exception) { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mc.ShowInfo += ShowInfo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mc.releaseCard();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                mc.releaseCard();
                mc = null;
                System.Environment.Exit(0);
            }
            catch (Exception ex) { }

        }
        FrmStatus frmIO = null; 
        private void button3_Click(object sender, EventArgs e)
        {
            frmIO = new FrmStatus();
           
            frmIO.Show();
        }
      
        private void button5_Click(object sender, EventArgs e)
        {
           //mc.SeverOn(AXIS.组装C21轴, 1);
           //mc.SeverOn(AXIS.组装C22轴, 1);
            //mc.SeverOn(AXIS.Y轴, 1);
            //mc.SeverOn(AXIS.Z轴, 1);
            //mc.SeverOn(AXIS.C轴, 1);
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //mc.SeverOn(AXIS.组装C22轴, 0);
            //mc.SeverOn(AXIS.组装C21轴, 0);
            //mc.SeverOn(AXIS.Y轴, 0);
            //mc.SeverOn(AXIS.Z轴, 0);
            //mc.SeverOn(AXIS.C轴, 0);
        }

        private void btnAbsMove_Click(object sender, EventArgs e)
        {
            AXIS ax =(AXIS)Enum.Parse(typeof(AXIS),cmdAxis.Text.Trim());
            mc.AbsMove(ax, (int)nudCmdPos.Value, (int)nudSetVel.Value);
        }

        private void btnRelP_Click(object sender, EventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.RelativeMove(ax, (int)nudRelDist.Value, (int)nudSetVel.Value);
        }

        private void btnRelM_Click(object sender, EventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.RelativeMove(ax, -(int)nudRelDist.Value, (int)nudSetVel.Value);
        }

        private void btnJOGM_MouseDown(object sender, MouseEventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.VelMove(ax, -(int)nudSetVel.Value);
        }

        private void btnJOGM_MouseUp(object sender, MouseEventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.StopAxis(ax);
        }

        private void btnJOGP_MouseDown(object sender, MouseEventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.VelMove(ax, (int)nudSetVel.Value);
        }

        private void btnJOGP_MouseUp(object sender, MouseEventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.StopAxis(ax);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
           // AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            //mc.HomeZ(AXIS.组装C22轴);
            //mc.HomeZ(AXIS.组装C21轴);
        }

        private void cbEnTrigger_CheckedChanged(object sender, EventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            if (cbEnTrigger.Checked)
            {
                int[] data = new int[] { (int)nudTrigPos1.Value };
                mc.startCmpTrigger(ax, data);
            }
            else {
                mc.stopCmpTrigger(ax);
            }
        }

        private void btnChangePos_Click(object sender, EventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.AbsMoveOvrd(ax, (int)nudChangePos.Value, (int)nudSetVel.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AXIS ax = (AXIS)Enum.Parse(typeof(AXIS), cmdAxis.Text.Trim());
            mc.SpeedOvrd(ax, (int)nudChangeVel.Value);
        }
    }
}
