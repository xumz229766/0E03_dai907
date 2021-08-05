using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly
{
    public partial class FrmSetDialog : Form
    {
        private int iSuctionNum = 1;
        private string strType = "当前取料位";
        public FrmSetDialog()
        {
            InitializeComponent();
        }
        public FrmSetDialog(int _iSuctionNum, string _strType)
        {
            iSuctionNum = _iSuctionNum;
            strType = _strType;
            InitializeComponent();
        }
        public FrmSetDialog( string _strType)
        {
            strType = _strType;
            InitializeComponent();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {

            switch (strType)
            {
                case "当前取料位":
                    if (iSuctionNum < 10)
                    {
                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction1[iSuctionNum].CurrentPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            int len = CommonSet.dic_OptSuction1.Count;
                            for (int i = 1; i < 10; i++)
                            {

                                OptSution1 p = CommonSet.dic_OptSuction1[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction1[i].CurrentPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    else
                    {

                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction2[iSuctionNum].CurrentPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            //int len = CommonSet.dic_ProductSuction.Count;
                            for (int i = 10; i < 19; i++)
                            {

                                OptSution2 p = CommonSet.dic_OptSuction2[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction2[i].CurrentPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    break;
                case "取料起始位":
                    if (iSuctionNum < 10)
                    {
                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction1[iSuctionNum].StartPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            int len = CommonSet.dic_OptSuction1.Count;
                            for (int i = 1; i < 10; i++)
                            {

                                OptSution1 p = CommonSet.dic_OptSuction1[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction1[i].StartPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction2[iSuctionNum].StartPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            //int len = CommonSet.dic_ProductSuction2.Count;
                            for (int i = 10; i < 19; i++)
                            {

                                OptSution2 p = CommonSet.dic_OptSuction2[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction2[i].StartPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    break;
                case "取料结束位":
                    if (iSuctionNum < 10)
                    {
                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction1[iSuctionNum].EndPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            int len = CommonSet.dic_OptSuction1.Count;
                            for (int i = 1; i < 10; i++)
                            {

                                OptSution1 p = CommonSet.dic_OptSuction1[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction1[i].EndPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (rbtnSingle.Checked)
                        {
                            CommonSet.dic_OptSuction2[iSuctionNum].EndPos = (int)nudValue.Value;
                        }
                        if (rbtnAll.Checked)
                        {
                            //int len = CommonSet.dic_ProductSuction2.Count;
                            for (int i = 10; i < 19; i++)
                            {

                                OptSution2 p = CommonSet.dic_OptSuction2[i];

                                if (p.BUse)
                                {
                                    CommonSet.dic_OptSuction2[i].EndPos = (int)nudValue.Value;
                                }
                            }
                        }
                    }
                    break;
                case "镜筒取料位":
                    CommonSet.dic_BarrelSuction[1].CurrentPos = (int)nudValue.Value;
                    break;
                case "镜筒起始位":
                    CommonSet.dic_BarrelSuction[1].StartPos = (int)nudValue.Value;
                    break;
                case "镜筒结束位":
                    CommonSet.dic_BarrelSuction[1].EndPos = (int)nudValue.Value;
                    break;
            }
            this.Close();
        }

        private void FrmSetDialog_Load(object sender, EventArgs e)
        {
            this.Text = "设置--" + strType;
            switch (strType)
            {
                case "当前取料位":
                    if (iSuctionNum < 10)
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction1[iSuctionNum].EndPos;
                        nudValue.Minimum = CommonSet.dic_OptSuction1[iSuctionNum].StartPos;
                    }
                    else
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction2[iSuctionNum].EndPos;
                        nudValue.Minimum = CommonSet.dic_OptSuction2[iSuctionNum].StartPos;
                    }
                    break;
                case "取料起始位":
                    if (iSuctionNum < 10)
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction1[iSuctionNum].EndPos;
                        nudValue.Minimum = 1;
                    }
                    else
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction2[iSuctionNum].EndPos;
                        nudValue.Minimum = 1;
                    }
                    break;
                case "取料结束位":
                    if (iSuctionNum < 10)
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction1[iSuctionNum].GetCount();
                        nudValue.Minimum = CommonSet.dic_OptSuction1[iSuctionNum].StartPos;
                    }
                    else
                    {
                        nudValue.Maximum = CommonSet.dic_OptSuction2[iSuctionNum].GetCount();
                        nudValue.Minimum = CommonSet.dic_OptSuction2[iSuctionNum].StartPos;
                    }
                    break;
                case "镜筒取料位":
                    nudValue.Maximum = CommonSet.dic_BarrelSuction[1].EndPos;
                    nudValue.Minimum = CommonSet.dic_BarrelSuction[1].StartPos;
                    rbtnAll.Visible = false;
                    rbtnSingle.Visible = false;
                    break;
                case "镜筒起始位":
                    nudValue.Maximum = CommonSet.dic_BarrelSuction[1].EndPos;
                    nudValue.Minimum = 1;
                    rbtnAll.Visible = false;
                    rbtnSingle.Visible = false;
                    break;
                case "镜筒结束位":
                    nudValue.Maximum = CommonSet.dic_BarrelSuction[1].GetCount();
                    nudValue.Minimum = CommonSet.dic_BarrelSuction[1].StartPos;
                    rbtnAll.Visible = false;
                    rbtnSingle.Visible = false;
                    break;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
