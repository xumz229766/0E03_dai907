using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tray;
namespace Assembly
{
    public partial class FrmSuctionAndTrayRelation : Form
    {
        int iStaiton = 1;
        int iCurrentSuction = 1;
        List<CheckBox> lstCheckBox = new List<CheckBox>();
        List<Button> lstButton = new List<Button>();
        public FrmSuctionAndTrayRelation()
        {
            InitializeComponent();
        }

        private void FrmSuctionAndTrayRelation_Load(object sender, EventArgs e)
        {
            showListTrayPanel1.SetShowMode(1);
            lstCheckBox.Clear();
            lstCheckBox.Add(checkBox1);
            lstCheckBox.Add(checkBox2);
            lstCheckBox.Add(checkBox3);
            lstCheckBox.Add(checkBox4);
            lstCheckBox.Add(checkBox5);
            lstCheckBox.Add(checkBox6);
            lstCheckBox.Add(checkBox7);
            lstCheckBox.Add(checkBox8);
            lstCheckBox.Add(checkBox9);
            lstButton.Clear();
            lstButton.Add(btnTraySet1);
            lstButton.Add(btnTraySet2);
            lstButton.Add(btnTraySet3);
            lstButton.Add(btnTraySet4);
            lstButton.Add(btnTraySet5);
            lstButton.Add(btnTraySet6);
            lstButton.Add(btnTraySet7);
            lstButton.Add(btnTraySet8);
            lstButton.Add(btnTraySet9);
          

            nudSuction.Minimum = 1;
            nudSuction.Maximum = 9;
        }

        private void rbtnStation2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnStation2.Checked)
            {
                iStaiton = 2;
                nudSuction.Minimum = 10;
                nudSuction.Maximum = 18;
                nudSuction.Value = 10;
            }
            else
            {
                iStaiton = 1;
               
                nudSuction.Minimum = 1;
                nudSuction.Maximum = 9;
                nudSuction.Value = 1;
            }
        }

        public void UpdateUI()
        {
            try
            {
                 iCurrentSuction = (int)nudSuction.Value;
                for (int i = 0; i < 9; i++)
                {
                    if (iStaiton == 1)
                    {
                        lstCheckBox[i].Text = "托盘" + (i + 1).ToString();
                        lstButton[i].Text = "盘形设定"+(i+1).ToString();
                        if (CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Contains(i + 1))
                        {
                            lstCheckBox[i].Checked = true;
                        }
                        else
                        {
                            lstCheckBox[i].Checked = false;
                        }

                    }
                    else
                    {
                        lstCheckBox[i].Text = "托盘" + (i + 10).ToString();
                        lstButton[i].Text = "盘形设定" + (i + 10).ToString();
                        if (CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Contains(i + 10))
                        {
                            lstCheckBox[i].Checked = true;
                        }
                        else
                        {
                            lstCheckBox[i].Checked = false;
                        }
                    }
                }
                showListTrayPanel1.SetStaion(iStaiton);
                showListTrayPanel1.UpdateUI();
            }
            catch (Exception)
            {
                
               
            }
        
        }

        private void checkBox9_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            string name = cb.Text;
            try
            {
                int index = Convert.ToInt32(name.Remove(0,2));
                if (cb.Checked)
                {
                    if (iStaiton == 1)
                    {
                        if (!CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Contains(index))
                            CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Add(index);
                    }
                    else
                    {
                        if (!CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Contains(index))
                            CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Add(index);
                    }
                }
                else
                {
                    if (iStaiton == 1)
                    {
                        if (CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Contains(index))
                            CommonSet.dic_OptSuction1[iCurrentSuction].lstTrayNum.Remove(index);
                    }
                    else
                    {
                        if (CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Contains(index))
                            CommonSet.dic_OptSuction2[iCurrentSuction].lstTrayNum.Remove(index);
                    }
                }
            }
            catch (Exception)
            {
                
               
            }
        }

        private void btnTraySet1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string name = btn.Text;
            try
            {
                  int index = Convert.ToInt32(name.Remove(0,4));
                  List<int> lst = new List<int>();
                  lst.Add(index);
                  TestTray test = new TestTray(lst, CommonSet.strProductParamPath + CommonSet.strProductName + "\\Tray.ini",CommonSet.tpSetPanel);
                  test.ShowDialog();
            }
            catch (Exception)
            {
                
               
            }

           
        }
    }
}
