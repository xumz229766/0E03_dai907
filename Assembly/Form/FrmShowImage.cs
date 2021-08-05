using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace Assembly
{
    public partial class FrmShowImage : Form
    {
        public static List<HWindow> lstA1Win = new List<HWindow>();
        public static List<HWindow> lstA2Win = new List<HWindow>();
        public static List<HWindow> lstC1Win = new List<HWindow>();
        public static List<HWindow> lstC2Win = new List<HWindow>();

        public FrmShowImage()
        {
           
            InitializeComponent();

            initWindow();

        }
        private void initWindow()
        {
            CommonSet.lstOtherWin.Add(hWinA1Up.HalconWindow);
            CommonSet.lstOtherWin.Add(hWinA2Up.HalconWindow);
            CommonSet.lstOtherWin.Add(hWinC1Up.HalconWindow);
            CommonSet.lstOtherWin.Add(hWinC2Up.HalconWindow);

            lstA1Win.Add(hWinA1Down1.HalconWindow);
            lstA1Win.Add(hWinA1Down2.HalconWindow);
            lstA1Win.Add(hWinA1Down3.HalconWindow);
            lstA1Win.Add(hWinA1Down4.HalconWindow);
            lstA1Win.Add(hWinA1Down5.HalconWindow);
            lstA1Win.Add(hWinA1Down6.HalconWindow);
            lstA1Win.Add(hWinA1Down7.HalconWindow);
            lstA1Win.Add(hWinA1Down8.HalconWindow);
            lstA1Win.Add(hWinA1Down9.HalconWindow);

            lstA2Win.Add(hWinA2Down1.HalconWindow);
            lstA2Win.Add(hWinA2Down2.HalconWindow);
            lstA2Win.Add(hWinA2Down3.HalconWindow);
            lstA2Win.Add(hWinA2Down4.HalconWindow);
            lstA2Win.Add(hWinA2Down5.HalconWindow);
            lstA2Win.Add(hWinA2Down6.HalconWindow);
            lstA2Win.Add(hWinA2Down7.HalconWindow);
            lstA2Win.Add(hWinA2Down8.HalconWindow);
            lstA2Win.Add(hWinA2Down9.HalconWindow);

            lstC1Win.Add(hWinC1Down1.HalconWindow);
            lstC1Win.Add(hWinC1Down2.HalconWindow);
            lstC1Win.Add(hWinC1Down3.HalconWindow);
            lstC1Win.Add(hWinC1Down4.HalconWindow);
            lstC1Win.Add(hWinC1Down5.HalconWindow);
            lstC1Win.Add(hWinC1Down6.HalconWindow);
            lstC1Win.Add(hWinC1Down7.HalconWindow);
            lstC1Win.Add(hWinC1Down8.HalconWindow);
            lstC1Win.Add(hWinC1Down9.HalconWindow);

            lstC2Win.Add(hWinC2Down1.HalconWindow);
            lstC2Win.Add(hWinC2Down2.HalconWindow);
            lstC2Win.Add(hWinC2Down3.HalconWindow);
            lstC2Win.Add(hWinC2Down4.HalconWindow);
            lstC2Win.Add(hWinC2Down5.HalconWindow);
            lstC2Win.Add(hWinC2Down6.HalconWindow);
            lstC2Win.Add(hWinC2Down7.HalconWindow);
            lstC2Win.Add(hWinC2Down8.HalconWindow);
            lstC2Win.Add(hWinC2Down9.HalconWindow);
           
        }
        private int startX, startY, endX, endY;
        public void DisplayPart(HWindow hwin, int iPosX, int iPosY, double multi = 1.01)
        {
            HTuple hWidth, hHeight;
            hWidth = 2448;
            Height = 2048;
            if ((iPosX + (endX - iPosX) / multi) - (iPosX - (iPosX - startX) / multi) > 3 * hWidth)
            {
                return;
            }
            double scale = 4 / 3;
            startX = (int)(iPosX - (iPosX - startX) / multi);
            startY = (int)(iPosY - (iPosY - startY) / (multi * scale));
            endX = (int)(iPosX + (endX - iPosX) / multi);
            endY = (int)(iPosY + (endY - iPosY) / (multi * scale));

        }

        private void FrmShowImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gp = (GroupBox)sender;
            e.Graphics.Clear(gp.BackColor);
            SizeF fontSize = e.Graphics.MeasureString(gp.Text, gp.Font);


            Pen p = new Pen(Brushes.LightSteelBlue, fontSize.Height + 12);
            e.Graphics.DrawLine(p, 1, 1, gp.Width - 2, 1);
            //e.Graphics.DrawLine(p, fontSize.Width + 8 + 2, 1, gp.Width - 2, 1);
            e.Graphics.DrawString(gp.Text, gp.Font, Brushes.Red, (gp.Width - fontSize.Width) / 2, 1);
            p = new Pen(Brushes.LightSteelBlue, 4);
            e.Graphics.DrawLine(p, 1, 1, 1, gp.Height);
            e.Graphics.DrawLine(p, 1, gp.Height - 2, gp.Width - 2, gp.Height - 2);
            e.Graphics.DrawLine(p, gp.Width - 2, 1, gp.Width - 2, gp.Height);
        }
        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
           


        }

        private void FrmShowImage_Load(object sender, EventArgs e)
        {

        }

        private void hWinA1Down1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindowControl h = (HWindowControl)sender;
                string hName = h.Name;
                HWindow hwin = h.HalconWindow;
                int hv_MouseRow, hv_MouseColumn, hv_Button;

                hwin.GetMposition(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMpositionSubPix(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMposition(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                if (e.Delta > 0)
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 1.3);

                }
                else
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 0.7);
                }
                HOperatorSet.ClearWindow(hwin);
                HOperatorSet.SetPart(hwin, startY, startX, endY, endX);

                if (hName.Contains("A1"))
                {
                    int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, hwin);
                    CommonSet.dic_OptSuction1[index].pfDown.showObj();
                }
                else if (hName.Contains("A2"))
                {
                    int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, hwin);
                    CommonSet.dic_OptSuction2[index + 9].pfDown.showObj();
                }
                else if (hName.Contains("C1"))
                {
                    int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, hwin);
                    CommonSet.dic_Assemble1[index].pfDown.showObj();
                }
                else if (hName.Contains("C2"))
                {
                    int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(CommonSet.dic_Assemble2[index+9].hImageDown, hwin);
                    CommonSet.dic_Assemble2[index + 9].pfDown.showObj();
                }

               

            }
            catch (Exception ex) { }
        }

        private void hWinA1Down1_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindowControl h = (HWindowControl)sender;
                string hName = h.Name;
                HWindow hwin = h.HalconWindow;
                double hv_MouseRow, hv_MouseColumn;
                int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);


                if (hv_Button == 4)
                {
                    HTuple hWidth = 2448;
                    HTuple hHeight = 2048;
                    // HOperatorSet.GetImageSize(CommonSet.dic_BarrelSuction[iSuctionNum].hImageCenter, out hWidth, out hHeight);
                    HOperatorSet.SetPart(hwin, 0, 0, hHeight.I, hWidth.I);
                    startX = 0;
                    startY = 0;
                    endX = hWidth.I;
                    endY = hHeight.I;
                    if (hName.Contains("A1"))
                    {
                        int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, hwin);
                        CommonSet.dic_OptSuction1[index].pfDown.showObj();
                    }
                    else if (hName.Contains("A2"))
                    {
                        int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, hwin);
                        CommonSet.dic_OptSuction2[index + 9].pfDown.showObj();
                    }
                    else if (hName.Contains("C1"))
                    {
                        int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(CommonSet.dic_Assemble1[index].hImageDown, hwin);
                        CommonSet.dic_Assemble1[index].pfDown.showObj();
                    }
                    else if (hName.Contains("C2"))
                    {
                        int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(CommonSet.dic_Assemble2[index + 9].hImageDown, hwin);
                        CommonSet.dic_Assemble2[index + 9].pfDown.showObj();
                    }
                  
                    return;
                }

            }
            catch (Exception ex) { }

        }

        private void hWinA1Down1_MouseEnter(object sender, EventArgs e)
        {
            HWindowControl h = (HWindowControl)sender;
            h.Focus();
            HTuple hWidth = 2448;
            HTuple hHeight = 2048;
            startX = 0;
            startY = 0;
            endX = hWidth.I;
            endY = hHeight.I;
        }

        private void hWinC1Up_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindowControl h = (HWindowControl)sender;
                string hName = h.Name;
                HWindow hwin = h.HalconWindow;
                double hv_MouseRow, hv_MouseColumn;
                int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);


                if (hv_Button == 4)
                {
                    HTuple hWidth = 2448;
                    HTuple hHeight = 2048;
                    // HOperatorSet.GetImageSize(CommonSet.dic_BarrelSuction[iSuctionNum].hImageCenter, out hWidth, out hHeight);
                    HOperatorSet.SetPart(hwin, 0, 0, hHeight.I, hWidth.I);
                    startX = 0;
                    startY = 0;
                    endX = hWidth.I;
                    endY = hHeight.I;
                    if (hName.Contains("A1"))
                    {
                        //int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        //HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, hwin);
                        //CommonSet.dic_OptSuction1[index].pfDown.showObj();
                    }
                    else if (hName.Contains("A2"))
                    {
                        //int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        //HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, hwin);
                        //CommonSet.dic_OptSuction2[index].pfDown.showObj();
                    }
                    else if (hName.Contains("C1"))
                    {
                       // int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                        AssembleSuction1.pfUp.showObj();
                    }
                    else if (hName.Contains("C2"))
                    {
                       // int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                        HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                        AssembleSuction2.pfUp.showObj();
                    }

                    return;
                }

            }
            catch (Exception ex) { }
        }

        private void hWinC1Up_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                //if (!cbContinous.Checked)
                //{
                //    return;
                //}

                HWindowControl h = (HWindowControl)sender;
                string hName = h.Name;
                HWindow hwin = h.HalconWindow;
                int hv_MouseRow, hv_MouseColumn, hv_Button;

                hwin.GetMposition(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMpositionSubPix(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMposition(hwin, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                if (e.Delta > 0)
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 1.3);

                }
                else
                {
                    DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 0.7);
                }
                HOperatorSet.ClearWindow(hwin);
                HOperatorSet.SetPart(hwin, startY, startX, endY, endX);

                if (hName.Contains("A1"))
                {
                    //int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    //HOperatorSet.DispImage(CommonSet.dic_OptSuction1[index].hImageDown, hwin);
                    //CommonSet.dic_OptSuction1[index].pfDown.showObj();
                }
                else if (hName.Contains("A2"))
                {
                    //int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    //HOperatorSet.DispImage(CommonSet.dic_OptSuction2[index + 9].hImageDown, hwin);
                    //CommonSet.dic_OptSuction2[index].pfDown.showObj();
                }
                else if (hName.Contains("C1"))
                {
                   // int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(AssembleSuction1.hImageUP, hwin);
                    AssembleSuction1.pfUp.showObj();
                }
                else if (hName.Contains("C2"))
                {
                   // int index = Convert.ToInt32(hName.Substring(hName.Length - 1));
                    HOperatorSet.DispImage(AssembleSuction2.hImageUP, hwin);
                    AssembleSuction2.pfUp.showObj();
                }



            }
            catch (Exception ex) { }
        }
    }
}
