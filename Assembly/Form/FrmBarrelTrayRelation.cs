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
    public partial class FrmBarrelTrayRelation : Form
    {
        int iCurrentSuction = 1;
        List<CheckBox> lstCheckBox = new List<CheckBox>();
        List<Button> lstButton = new List<Button>();
        public FrmBarrelTrayRelation()
        {
            InitializeComponent();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            string name = cb.Text;
            try
            {
                int index = Convert.ToInt32(name.Remove(0, 2));
                if (cb.Checked)
                {
                   
                        if (!CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Contains(index+18))
                            CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Add(index + 18);
                    
                }
                else
                {

                    if (CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Contains(index + 18))
                        CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Remove(index + 18);
                   
                }
            }
            catch (Exception)
            {


            }
        }

        private void FrmBarrelTrayRelation_Load(object sender, EventArgs e)
        {
            barrelListTray1.SetShowMode(1);
            lstButton.Clear();
            lstButton.Add(btnTraySet1);
            lstButton.Add(btnTraySet2);
            lstCheckBox.Clear();
            lstCheckBox.Add(checkBox1);
            lstCheckBox.Add(checkBox2);
        }

        public void UpdateUI()
        {
            try
            {
                iCurrentSuction = (int)nudSuction.Value;
                for (int i = 0; i < 2; i++)
                {
                    
                       
                        if (CommonSet.dic_BarrelSuction[iCurrentSuction].lstTrayNum.Contains(i + 19))
                        {
                            lstCheckBox[i].Checked = true;
                        }
                        else
                        {
                            lstCheckBox[i].Checked = false;
                        }

                   
                    
                }
               
                barrelListTray1.UpdateUI();
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
                int index = Convert.ToInt32(name.Remove(0, 4));
                List<int> lst = new List<int>();
                lst.Add(index+18);
                TestTray test = new TestTray(lst, CommonSet.strProductParamPath + CommonSet.strProductName + "\\Tray.ini", CommonSet.tpSetPanel);
                test.ShowDialog();
            }
            catch (Exception)
            {


            }
        }

    }
}
