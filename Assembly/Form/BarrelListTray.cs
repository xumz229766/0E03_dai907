using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembly
{
    public partial class BarrelListTray : UserControl
    {

        int iShowMode = 0;//0代表显示单个吸笔设置时模拟显示,1代表字符显示

        int iCurrentSuction = 1;
        private int[] sort1 = new int[] { 19, 20 };//工位1排盘顺序
        private List<Label> lstLable = new List<Label>();
        public BarrelListTray()
        {
            iShowMode = 0;
            InitializeComponent();
        }
        public void SetShowMode(int mode)
        {
            iShowMode = mode;
        }
        public void SetSuction(int suction)
        {
            iCurrentSuction = suction;
        }

        private void BarrelListTray_Load(object sender, EventArgs e)
        {
            lstLable.Clear();
            lstLable.Add(lbl1);
            lstLable.Add(lbl2);
        }
        public void UpdateUI()
        {
            try
            {
                switch (iShowMode)
                {
                    case 0:
                        tableLayoutPanel1.RowStyles[0].Height = 15;
                        lblX.Text = "X轴";
                       
                            for (int i = 0; i < 2; i++)
                            {
                                if (CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Contains(sort1[i]))
                                {
                                    lstLable[i].Text = sort1[i].ToString();
                                    lstLable[i].BackColor = Color.Red;
                                }
                                else
                                {
                                    lstLable[i].Text = sort1[i].ToString();
                                    lstLable[i].BackColor = Color.Yellow;
                                }
                            }
                       
                       


                        break;
                    case 1:
                        tableLayoutPanel1.RowStyles[0].Height = 30;
                        lblX.Text = "X轴";
                        for (int i = 0; i < 2; i++)
                        {
                            
                                string strSuction = "";
                                for (int iSuction = 1; iSuction < 3; iSuction++)
                                {
                                    if (CommonSet.dic_BarrelSuction[iSuction].lstTrayNum.Contains(sort1[i]))
                                    {
                                        strSuction += iSuction.ToString() + ",";
                                    }
                                }
                                if (strSuction != "")
                                    strSuction = strSuction.Remove(strSuction.LastIndexOf(','));
                                lstLable[i].Text = "托盘" + sort1[i].ToString();
                                lstLable[i].Refresh();
                            
                           

                        }

                        break;
                }


            }
            catch (Exception)
            {


            }
        }

        private void lbl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {


                Label lab = (Label)sender;
                string str = lab.Text;
                for (int i = 0; i < 2; i++)
                {
                    
                        string name = "托盘" + sort1[i].ToString();
                        if (name.Equals(str))
                        {

                            string strSuction = "";
                            for (int iSuction = 1; iSuction < 3; iSuction++)
                            {
                                if (CommonSet.dic_BarrelSuction[iSuction].lstTrayNum.Contains(sort1[i]))
                                {
                                    strSuction += iSuction.ToString() + ",";
                                }
                            }
                            if (strSuction != "")
                                strSuction = strSuction.Remove(strSuction.LastIndexOf(','));
                            System.Drawing.Point point = new System.Drawing.Point(lab.Padding.Left, lab.Padding.Top);
                            //TextRenderer.DrawText(e.Graphics, str, lab.Font, point, Color);
                            TextRenderer.DrawText(e.Graphics, strSuction, lab.Font, point, Color.Red);
                            break;
                        }

                        //lstLable[i].Text = "托盘" + sort1[i].ToString() + "\n\r" + "当前吸笔：[" + strSuction + "]";
                        //lstLable[i].Refresh();
                   

                }

               
            }
            catch (Exception)
            {


            }
        }
    }
}
