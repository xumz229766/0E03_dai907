using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0E030
{
    public partial class FrmSetDialog : Form
    {
        private int iTrayNum = 1;
        private string strType = "取料位";
        private string strTray = "左托盘";
        public FrmSetDialog()
        {
            InitializeComponent();
        }
        public FrmSetDialog(int _iTrayNum, string _strType,string _strTray)
        {
            iTrayNum = _iTrayNum;
            strType = _strType;
            strTray = _strTray;
            InitializeComponent();
        }
      
        
        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (strType)
            {
                case "取料位":

                    if (strTray.Equals("左托盘"))
                    {
                       SuctionL.lstTray[iTrayNum-1].CurrentPos = (int)nudValue.Value;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        SuctionR.lstTray[iTrayNum - 1].CurrentPos = (int)nudValue.Value;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                       Barrel.lstTray[iTrayNum-1].CurrentPos = (int)nudValue.Value;
                    }
                    
                    break;
                case "起始位":
                  if (strTray.Equals("左托盘"))
                    {
                       SuctionL.lstTray[iTrayNum-1].StartPos = (int)nudValue.Value;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        SuctionR.lstTray[iTrayNum - 1].StartPos = (int)nudValue.Value;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                        Barrel.lstTray[iTrayNum - 1].StartPos = (int)nudValue.Value;
                    }
                    break;
                case "结束位":
                    if (strTray.Equals("左托盘"))
                    {
                       SuctionL.lstTray[iTrayNum-1].EndPos = (int)nudValue.Value;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        SuctionR.lstTray[iTrayNum - 1].EndPos = (int)nudValue.Value;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                        Barrel.lstTray[iTrayNum - 1].EndPos = (int)nudValue.Value;
                    }
                    break;
               
            }
            this.Close();
        }

        private void FrmSetDialog_Load(object sender, EventArgs e)
        {
            this.Text = "设置--" +strTray+iTrayNum.ToString()+ strType;
            switch (strType)
            {
                case "取料位":

                     if (strTray.Equals("左托盘"))
                    {
                        nudValue.Maximum = SuctionL.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = SuctionL.lstTray[iTrayNum - 1].StartPos;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        nudValue.Maximum = SuctionR.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = SuctionR.lstTray[iTrayNum - 1].StartPos;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                        nudValue.Maximum = Barrel.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = Barrel.lstTray[iTrayNum - 1].StartPos;
                    }
                   
                   
                    break;
                case "起始位":
                    if (strTray.Equals("左托盘"))
                    {
                        nudValue.Maximum = SuctionL.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = 1;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        nudValue.Maximum = SuctionR.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = 1;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                        nudValue.Maximum = Barrel.lstTray[iTrayNum - 1].EndPos;
                        nudValue.Minimum = 1;
                    }
                    break;
                case "结束位":
                     if (strTray.Equals("左托盘"))
                    {
                        nudValue.Maximum = SuctionL.lstTray[iTrayNum - 1].dic_Index.Count;
                        nudValue.Minimum = SuctionL.lstTray[iTrayNum - 1].StartPos;
                    }
                    else if (strTray.Equals("右托盘"))
                    {
                        nudValue.Maximum = SuctionR.lstTray[iTrayNum - 1].dic_Index.Count;
                        nudValue.Minimum = SuctionR.lstTray[iTrayNum - 1].StartPos;
                    }else if (strTray.Equals("镜筒托盘"))
                    {
                        nudValue.Maximum = Barrel.lstTray[iTrayNum - 1].dic_Index.Count;
                        nudValue.Minimum = Barrel.lstTray[iTrayNum - 1].StartPos;
                    }
                   
                    break;
               

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
