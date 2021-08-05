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
    public partial class FrmDebug : Form
    {
        public FrmPixelToAxis fptA = null;
        public FrmCalib frmCalib = null;
        public FrmTestFlash frmTestFlash = null;
        FrmCalibHeight frmCalibHeight = null;
        FrmCalibPressure frmCalibPre = null;
        public FrmRotate frmRotate = null;
        SynchronizationContext _context = null;
        Thread th_UpdateUI = null;
        public FrmDebug()
        {
            InitializeComponent();

            _context = SynchronizationContext.Current;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
           

            Font fntTab;
            Brush bshBack;
            Brush bshFore;
            if (e.Index == this.tabControl1.SelectedIndex)
            {
                fntTab = new Font(e.Font, FontStyle.Bold);
                //bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.Control, SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshBack = new SolidBrush(Color.SkyBlue);
                bshFore = Brushes.Black;
            }
            else
            {
                fntTab = e.Font;
                bshBack = new SolidBrush(Color.LightSteelBlue);
                bshFore = new SolidBrush(Color.Black);
            }
            string tabName = this.tabControl1.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat();
            sftTab.Alignment = StringAlignment.Center;
            sftTab.LineAlignment = StringAlignment.Center;
            e.Graphics.FillRectangle(bshBack, e.Bounds);
            //Rectangle recTab = e.Bounds;
            //recTab = new Rectangle(recTab.X, recTab.Y + 10, recTab.Width, recTab.Height - 4);
            RectangleF tabTextArea;
            tabTextArea = (RectangleF)tabControl1.GetTabRect(e.Index);
            e.Graphics.DrawString(tabName, fntTab, bshFore, tabTextArea, sftTab);


            //Rectangle tabArea;
            //RectangleF tabTextArea;
            //tabArea = tabControl1.GetTabRect(e.Index);
            //tabTextArea = (RectangleF)tabControl1.GetTabRect(e.Index);
            //Graphics g = e.Graphics;
            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            //sf.Alignment = StringAlignment.Center;
            //Font font = this.tabControl1.Font;
            //SolidBrush brush = new SolidBrush(Color.Black);
            //g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);
        }

        private void FrmDebug_Load(object sender, EventArgs e)
        {
            CommonSet.bCalibFlag = true;

            LoadSuctionSetUI();

            th_UpdateUI = new Thread(UpdateUI);
            th_UpdateUI.Start();

        }

        public void UpdateUI()
        {
            while (true)
            {
                try
                {
                       _context.Send(UpdateStatus, null);
                        _context.Send(UpdateStatus2, null);
                   
                }
                catch (Exception ex)
                {

                }

                Thread.Sleep(600);
            }
        }
        public void UpdateStatus(object o)
        {
            try
            {
                if (fptA != null)
                    fptA.updateUI();
            }
            catch (Exception)
            {

            }
            try
            {
                if(frmCalib != null)
                {
                    frmCalib.UpdateControl();
                }

            }
            catch (Exception)
            {
               
            }
            try
            {
                if (frmRotate != null)
                {
                    frmRotate.UpdateUI();
                }
            }
            catch (Exception)
            {
                
               
            }
           

        }

        private void UpdateStatus2(object o)
        {
            try
            {
                if (frmCalibHeight != null)
                    frmCalibHeight.UpdateUI();
            }
            catch (Exception)
            {

            }
            try
            {
                if (frmCalibPre != null)
                    frmCalibPre.UpdateUI();
            }
            catch (Exception)
            {

            }
        }

        bool[] bShowSuctionUI = new bool[15];
        //Dictionary<int, ControlSuction> dic_SuctionUI = new Dictionary<int, ControlSuction>();//镜片吸笔界面
        // BarrelSuctionUI bs = null;//镜筒吸笔界面
       // int iSelectSuctionTabIndex = 1;//当前选中的吸笔索引
        public static int iSelectParamSetIndex = 0;//参数界面索引
        private void LoadSuctionSetUI()
        {
            fptA = new FrmPixelToAxis();
            GenerateForm(fptA,tabControl1);
            fptA.Show();
            bShowSuctionUI[0] = true;
            //addSuctionUI(1, cs);
        }
        //在选项卡中生成窗体  
        public void GenerateForm(Form fm, object sender)
        {
            // 反射生成窗体  
            // Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);

            //设置窗体没有边框 加入到选项卡中  
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;

            // s[((TabControl)sender).SelectedIndex] = 1;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 0:
                    iSelectParamSetIndex = 0;
                    if (!bShowSuctionUI[index])
                    {
                        bShowSuctionUI[0] = true;
                        // bShowSuctionUI[iSelectSuctionTabIndex] = true;
                    }
                    break;

                case 1:
                    iSelectParamSetIndex = 1;
                    if (!bShowSuctionUI[index])
                    {
                        frmCalib = new FrmCalib();
                        GenerateForm(frmCalib, sender);
                        frmCalib.Show();
                        bShowSuctionUI[1] = true;
                    }

                    break;
                case 2:
                    iSelectParamSetIndex = 2;
                    if (!bShowSuctionUI[index])
                    {
                        frmTestFlash = new FrmTestFlash();
                        GenerateForm(frmTestFlash, sender);
                        frmTestFlash.Show();
                        bShowSuctionUI[2] = true;

                    }
                    break;
                case 3:
                    iSelectParamSetIndex = 3;
                    if (!bShowSuctionUI[index])
                    {
                        frmCalibHeight = new FrmCalibHeight();
                        GenerateForm(frmCalibHeight, sender);
                        frmCalibHeight.Show();
                        bShowSuctionUI[3] = true;
                    }
                    break;
                case 4:
                    iSelectParamSetIndex = 4;
                    if (!bShowSuctionUI[index])
                    {
                        frmCalibPre = new FrmCalibPressure();
                        GenerateForm(frmCalibPre, sender);
                        frmCalibPre.Show();
                        bShowSuctionUI[4] = true;
                    }
                    break;
                case 5:
                    iSelectParamSetIndex = 5;
                    if (!bShowSuctionUI[index])
                    {
                        frmRotate = new FrmRotate();
                        GenerateForm(frmRotate, sender);
                        frmRotate.Show();
                        bShowSuctionUI[5] = true;
                    }
                    break;
                default:

                    break;

            }
        }

        private void FrmDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonSet.bCalibFlag = false;
            Run.runMode = RunMode.手动;

            try
            {

                if (th_UpdateUI.IsAlive)
                    th_UpdateUI.Abort();
                th_UpdateUI = null;
            }
            catch (Exception)
            {
                
                
            }
            try
            {
                if (fptA != null)
                    fptA=null;
            }
            catch (Exception)
            {

            }
            try
            {
                if (frmCalib != null)
                {
                    frmCalib=null;
                }

            }
            catch (Exception)
            {

            }
            try
            {
                if (frmRotate != null)
                {
                    frmRotate=null;
                }
            }
            catch (Exception)
            {


            }
            try
            {
                if (frmCalibHeight != null)
                    frmCalibHeight=null;
            }
            catch (Exception)
            {

            }
            try
            {
                if (frmCalibPre != null)
                    frmCalibPre=null;
            }
            catch (Exception)
            {

            }
            try
            {
                if (frmTestFlash != null)
                    frmTestFlash = null;
            }
            catch (Exception)
            {

            }
        }

    }
}
