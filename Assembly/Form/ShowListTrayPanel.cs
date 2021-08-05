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
    public partial class ShowListTrayPanel : UserControl
    {
        int iShowMode = 0;//0代表显示单个吸笔设置时模拟显示,1代表字符显示

        int iStation = 1;
        int iCurrentSuction = 1;
        private int[] sort1 = new int[] {1,2,3,4,5,6,7,8,9 };//工位1排盘顺序
        private int[] sort2 = new int[] {12, 11, 10, 15, 14, 13, 18, 17, 16 };//工位2排盘顺序
       // private Dictionary<int, List<int>> dic_Tray = new Dictionary<int, List<int>>();
        private List<Label> lstLable = new List<Label>();
        public ShowListTrayPanel()
        {
            iShowMode = 0;
            InitializeComponent();
        }
        public void SetShowMode(int mode)
        {
            iShowMode = mode;
        }
        public void SetStaion(int Station)
        {
            iStation = Station;
        }
        public void SetSuction(int suction)
        {
            iCurrentSuction = suction;
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
                        if (iStation == 1)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                if (CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Contains(sort1[i]))
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
                        }
                        else
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                if (CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Contains(sort2[i]))
                                {
                                    lstLable[i].Text = sort2[i].ToString();
                                    lstLable[i].BackColor = Color.Red;
                                }
                                else
                                {
                                    lstLable[i].Text = sort2[i].ToString();
                                    lstLable[i].BackColor = Color.Yellow;
                                }
                            }
                        }


                        break;
                    case 1:
                         tableLayoutPanel1.RowStyles[0].Height = 30;
                         if (iStation == 1)
                         {
                             lblX.Text = "X 轴\r\n 吸笔排列：9  8  7  6  5  4  3  2  1";
                         }
                         else
                         {
                             lblX.Text = "X 轴\r\n 吸笔排列：10  11  12  13  14  15  16  17  18";
                         }
                        for (int i = 0; i < 9; i++)
                        {
                            if (iStation == 1)
                            {
                                string strSuction = "";
                                for (int iSuction = 1; iSuction < 10; iSuction++)
                                {
                                    if (CommonSet.dic_OptSuction1[iSuction].lstTrayNum.Contains(sort1[i]))
                                    {
                                        strSuction += iSuction.ToString() + ",";
                                    }
                                }
                                if (strSuction != "")
                                    strSuction = strSuction.Remove(strSuction.LastIndexOf(','));
                                lstLable[i].Text = "托盘"+sort1[i].ToString();
                                lstLable[i].Refresh();
                            }
                            else
                            {
                                string strSuction = "";
                                for (int iSuction = 10; iSuction < 19; iSuction++)
                                {
                                    if (CommonSet.dic_OptSuction2[iSuction].lstTrayNum.Contains(sort2[i]))
                                    {
                                        strSuction += iSuction.ToString() + ",";
                                    }
                                }
                                if (strSuction != "")
                                    strSuction = strSuction.Remove(strSuction.LastIndexOf(','));
                                lstLable[i].Text = "托盘" + sort2[i].ToString() ;
                                lstLable[i].Refresh();
                            }
                        
                        }

                        break;
                }


            }
            catch (Exception)
            {
                
                
            }
        }

        private void ShowListTrayPanel_Load(object sender, EventArgs e)
        {
            lstLable.Clear();
            lstLable.Add(lbl1);
            lstLable.Add(lbl2);
            lstLable.Add(lbl3);
            lstLable.Add(lbl4);
            lstLable.Add(lbl5);
            lstLable.Add(lbl6);
            lstLable.Add(lbl7);
            lstLable.Add(lbl8);
            lstLable.Add(lbl9);

        }

        private void lbl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {


                Label lab = (Label)sender;
                string str = lab.Text;
                for (int i = 0; i < 9; i++)
                {
                    if (iStation == 1)
                    {
                        string name = "托盘" + sort1[i].ToString();
                        if (name.Equals(str))
                        {

                            string strSuction = "";
                            for (int iSuction = 1; iSuction < 10; iSuction++)
                            {
                                if (CommonSet.dic_OptSuction1[iSuction].lstTrayNum.Contains(sort1[i]))
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
                    else
                    {
                         string name = "托盘" + sort2[i].ToString();
                         if (name.Equals(str))
                         {
                             string strSuction = "";
                             for (int iSuction = 10; iSuction < 19; iSuction++)
                             {
                                 if (CommonSet.dic_OptSuction2[iSuction].lstTrayNum.Contains(sort2[i]))
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
                             //lstLable[i].Text = "托盘" + sort2[i].ToString() + "\n\r" + "当前吸笔：[" + strSuction + "]";
                             //lstLable[i].Refresh();
                         }
                    }

                }

                //int index1 = str.IndexOf('[');
                //int index2 = str.IndexOf(']');
                //if (index2  > index1+1)
                //{
                //    string strA = str.Substring(index1+1, index2 - index1-1);
                //    //SizeF fontSize = e.Graphics.MeasureString(strA, lab.Font);
                //    //System.Drawing.PointF pontF = fontSize.ToPointF();

                //    System.Drawing.Point point = new System.Drawing.Point(lab.Padding.Left, lab.Padding.Top);
                //    //TextRenderer.DrawText(e.Graphics, str, lab.Font, point, Color);
                //    TextRenderer.DrawText(e.Graphics, strA, lab.Font, point, Color.Red);
                //}
            }
            catch (Exception)
            {


            }
        }

       

    }
}
