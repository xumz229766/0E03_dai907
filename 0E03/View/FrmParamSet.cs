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
namespace _0E03
{
    public partial class FrmParamSet:UIPage
    {
        public FrmParamSet()
        {
            InitializeComponent();
           
        }
        
        private void FrmParamSet_Load(object sender, EventArgs e)
        {
            CommonSet.WriteInfo("加载参数设置界面成功！");
            //tabPage1.Controls.Add(new FrmSetLeft());
            frmSetLeft = new FrmSetLeft();
            frmSetLeft.Parent = tabPage1;

            frmSetLeft.Dock = DockStyle.Fill;
            frmSetLeft.Show();
            bParamSetUI[0] = true;

            frmSetRight = new FrmSetRight();
            frmSetRight.Parent = tabPage2;
            frmSetRight.Dock = DockStyle.Fill;
            frmSetRight.Show();
            bParamSetUI[1] = true;

            frmBarrel = new FrmSetBarrel();
            frmBarrel.Parent = tabPage3;
            frmBarrel.Dock = DockStyle.Fill;
            frmBarrel.Show();
            bParamSetUI[2] = true;

            frmOther = new FrmOtherSet();
            frmOther.Parent = tabPage4;
            frmOther.Dock = DockStyle.Fill;
            frmOther.Show();
            bParamSetUI[3] = true;
        }

        bool[] bParamSetUI = new bool[6];
        public static FrmSetLeft frmSetLeft = null;
        public static FrmSetRight frmSetRight = null;
        public static FrmSetBarrel frmBarrel = null;
        public static FrmOtherSet frmOther = null;
        public static int iSelectIndex = 0;
        private void uiTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = uiTabControl1.SelectedIndex;
                iSelectIndex = index;
                switch (index) { 
                    case 0:
                        if (!bParamSetUI[0])
                        {
                            frmSetLeft = new FrmSetLeft();
                            frmSetLeft.Parent = tabPage1;

                            frmSetLeft.Dock = DockStyle.Fill;
                            frmSetLeft.Show();
                            bParamSetUI[0] = true;
                        }
                        break;
                    case 1:
                        if (!bParamSetUI[1])
                        {
                            frmSetRight = new FrmSetRight();
                            frmSetRight.Parent = tabPage2;

                            frmSetRight.Dock = DockStyle.Fill;
                            frmSetRight.Show();
                            bParamSetUI[1] = true;
                        }
                        break;
                    case 2:
                        if (!bParamSetUI[2])
                        {
                            frmBarrel = new FrmSetBarrel();
                            frmBarrel.Parent = tabPage3;

                            frmBarrel.Dock = DockStyle.Fill;
                            frmBarrel.Show();
                            bParamSetUI[2] = true;
                        }
                        break;
                    case 3:
                        if (!bParamSetUI[3])
                        {
                           
                            bParamSetUI[3] = true;
                        }
                        break;
                }

            }
            catch (Exception)
            {
                
               
            }
        }

        public void UpdateUI()
        {
            if (frmSetLeft != null)
            {
                frmSetLeft.UpdateUI();
            }
            if (frmSetRight != null)
            {
                frmSetRight.UpdateUI();
            }
            if (frmBarrel != null)
                frmBarrel.UpdateUI();
            if (frmOther != null)
                frmOther.UpdateUI();
        }
    }
}
