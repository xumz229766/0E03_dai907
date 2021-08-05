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
    public partial class FrmSolutionSet : Form
    {

        List<NumericUpDown> lst_nudAng1 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudPre1 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudVel1 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudTime1 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudDelay1 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudHeight21 = new List<NumericUpDown>();

        List<NumericUpDown> lst_nudAng2 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudPre2 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudVel2 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudTime2 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudDelay2 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudHeight22 = new List<NumericUpDown>();

        List<NumericUpDown> lst_nudAng3 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudPre3 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudVel3 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudTime3 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudDelay3 = new List<NumericUpDown>();
        List<NumericUpDown> lst_nudHeight23 = new List<NumericUpDown>();


        public FrmSolutionSet()
        {
            InitializeComponent();
        }

        private void FrmSolutionSet_Load(object sender, EventArgs e)
        {
            initUI();
        }
       
        //初始化UI控件
        private void initUI()
        {
            lst_nudAng1.Clear();
            lst_nudPre1.Clear();
            lst_nudVel1.Clear();
            lst_nudTime1.Clear();
            lst_nudDelay1.Clear();
            lst_nudAng2.Clear();
            lst_nudPre2.Clear();
            lst_nudVel2.Clear();
            lst_nudDelay2.Clear();
            lst_nudTime2.Clear();
            lst_nudAng3.Clear();
            lst_nudPre3.Clear();
            lst_nudVel3.Clear();
            lst_nudTime3.Clear();
            lst_nudDelay3.Clear();
            label2.Text = CommonSet.dic_OptSuction1[1].Name;
            label3.Text = CommonSet.dic_OptSuction1[2].Name;
            label4.Text = CommonSet.dic_OptSuction1[3].Name;
            label5.Text = CommonSet.dic_OptSuction1[4].Name;
            label6.Text = CommonSet.dic_OptSuction1[5].Name;
            label7.Text = CommonSet.dic_OptSuction1[6].Name;
            label8.Text = CommonSet.dic_OptSuction1[7].Name;
            label9.Text = CommonSet.dic_OptSuction1[8].Name;
            label10.Text = CommonSet.dic_OptSuction1[9].Name;

            label11.Text = CommonSet.dic_OptSuction2[10].Name;
            label12.Text = CommonSet.dic_OptSuction2[11].Name;
            label13.Text = CommonSet.dic_OptSuction2[12].Name;
            label14.Text = CommonSet.dic_OptSuction2[13].Name;
            label15.Text = CommonSet.dic_OptSuction2[14].Name;
            label37.Text = CommonSet.dic_OptSuction2[15].Name;
            label38.Text = CommonSet.dic_OptSuction2[16].Name;
            label39.Text = CommonSet.dic_OptSuction2[17].Name;
            label40.Text = CommonSet.dic_OptSuction2[18].Name;

            for (int i = 1; i < 19; i++)
            {
                NumericUpDown nudAng1 = new NumericUpDown();
                nudAng1.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudAng1.Location = new System.Drawing.Point(75, 39);
                nudAng1.Name = "nudAng1"+i.ToString();
                nudAng1.Size = new System.Drawing.Size(60, 21);
                nudAng1.Minimum = 0;
                nudAng1.Maximum = 360;
                nudAng1.Value = (decimal)CommonSet.asm.dic_Solution[1].lstAngle[i - 1];
                nudAng1.ValueChanged += NudAng1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudAng1, i+1, 1);
                lst_nudAng1.Add(nudAng1);

                NumericUpDown nudPre1 = new NumericUpDown();
                nudPre1.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudPre1.Location = new System.Drawing.Point(75, 39);
                nudPre1.Name = "nudPre1" + i.ToString();
                nudPre1.Size = new System.Drawing.Size(60, 21);
                nudPre1.Maximum = 60;
                nudPre1.Minimum = 0;
                nudPre1.DecimalPlaces = 2;
                nudPre1.Value = (decimal)CommonSet.asm.dic_Solution[1].lstPressure[i - 1];
                nudPre1.ValueChanged += NudPre1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudPre1, i + 1, 2);
                lst_nudPre1.Add(nudPre1);

                NumericUpDown nudVel1 = new NumericUpDown();
                nudVel1.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudVel1.Location = new System.Drawing.Point(75, 39);
                nudVel1.Name = "nudVel1" + i.ToString();
                nudVel1.Size = new System.Drawing.Size(60, 21);
                nudVel1.Minimum = 5;
                nudVel1.Maximum = 200;
                nudVel1.Value = (decimal)CommonSet.asm.dic_Solution[1].lstVel[i - 1];
                nudVel1.ValueChanged+=NudVel1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudVel1.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudVel1, i + 1, 3);
                

                lst_nudVel1.Add(nudVel1);

                NumericUpDown nudTime1 = new NumericUpDown();
                nudTime1.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudTime1.Location = new System.Drawing.Point(75, 39);
                nudTime1.Name = "nudTime1" + i.ToString();
                nudTime1.Size = new System.Drawing.Size(60, 21);
                nudTime1.Maximum = 2;
                nudTime1.Minimum = (decimal)0.001;
                nudTime1.DecimalPlaces = 3;
                nudTime1.Value = (decimal)CommonSet.asm.dic_Solution[1].lstTime[i - 1];
                nudTime1.ValueChanged += NudTime1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudTime1.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudTime1, i + 1, 4);
                lst_nudTime1.Add(nudTime1);

                NumericUpDown nudDelay1 = new NumericUpDown();
                nudDelay1.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudDelay1.Location = new System.Drawing.Point(75, 39);
                nudDelay1.Name = "nudDelay1" + i.ToString();
                nudDelay1.Size = new System.Drawing.Size(60, 21);
                nudDelay1.Maximum = 100;
                nudDelay1.Minimum = (decimal)-20;
                nudDelay1.DecimalPlaces = 3;
                nudDelay1.Value = (decimal)CommonSet.asm.dic_Solution[1].lstHieght[i - 1];
                nudDelay1.ValueChanged +=NudDelay1_ValueChanged;
                //if (CommonSet.iLevel == 0)
                    nudDelay1.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudDelay1, i + 1, 5);
                lst_nudDelay1.Add(nudDelay1);

                NumericUpDown nudHeight21 = new NumericUpDown();
                nudHeight21.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudHeight21.Location = new System.Drawing.Point(75, 39);
                nudHeight21.Name = "nudHeight21" + i.ToString();
                nudHeight21.Size = new System.Drawing.Size(60, 21);
                nudHeight21.Maximum = 20;
                nudHeight21.Minimum = (decimal)-20;
                nudHeight21.DecimalPlaces = 3;
                nudHeight21.Value = (decimal)CommonSet.asm.dic_Solution[1].lstHeight2[i - 1];
                nudHeight21.ValueChanged += nudHeight21_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudHeight21.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudHeight21, i + 1, 6);
                lst_nudHeight21.Add(nudHeight21);
                //.Add(nudHeight21);

                NumericUpDown nudAng2 = new NumericUpDown();
                nudAng2.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudAng2.Location = new System.Drawing.Point(75, 39);
                nudAng2.Name = "nudAng2" + i.ToString();
                nudAng2.Size = new System.Drawing.Size(60, 21);
                nudAng2.Minimum = 0;
                nudAng2.Maximum = 360;
                nudAng2.Value = (decimal)CommonSet.asm.dic_Solution[2].lstAngle[i - 1];
                nudAng2.ValueChanged += NudAng1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudAng2, i + 1, 7);
                lst_nudAng2.Add(nudAng2);


                NumericUpDown nudPre2 = new NumericUpDown();
                nudPre2.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudPre2.Location = new System.Drawing.Point(75, 39);
                nudPre2.Name = "nudPre2" + i.ToString();
                nudPre2.Size = new System.Drawing.Size(60, 21);
                nudPre2.Maximum = 60;
                nudPre2.Minimum = 0;
                nudPre2.DecimalPlaces = 2;
                nudPre2.Value = (decimal)CommonSet.asm.dic_Solution[2].lstPressure[i - 1];
                nudPre2.ValueChanged += NudPre1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudPre2, i + 1, 8);
                lst_nudPre2.Add(nudPre2);

                NumericUpDown nudVel2 = new NumericUpDown();
                nudVel2.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudVel2.Location = new System.Drawing.Point(75, 39);
                nudVel2.Name = "nudVel2" + i.ToString();
                nudVel2.Size = new System.Drawing.Size(60, 21);
                nudVel2.Minimum = 5;
                nudVel2.Maximum = 200;
                nudVel2.Value = (decimal)CommonSet.asm.dic_Solution[2].lstVel[i - 1];
                nudVel2.ValueChanged += NudVel1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudVel2.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudVel2, i + 1, 9);
                lst_nudVel2.Add(nudVel2);

                NumericUpDown nudTime2 = new NumericUpDown();
                nudTime2.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudTime2.Location = new System.Drawing.Point(75, 39);
                nudTime2.Name = "nudTime2" + i.ToString();
                nudTime2.Size = new System.Drawing.Size(60, 21);
                nudTime2.Maximum = 2;
                nudTime2.Minimum = (decimal)0.001;
                nudTime2.DecimalPlaces = 3;
                nudTime2.Value = (decimal)CommonSet.asm.dic_Solution[2].lstTime[i - 1];
                nudTime2.ValueChanged += NudTime1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudTime2.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudTime2, i + 1, 10);
                lst_nudTime2.Add(nudTime2);

                NumericUpDown nudDelay2 = new NumericUpDown();
                nudDelay2.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudDelay2.Location = new System.Drawing.Point(75, 39);
                nudDelay2.Name = "nudDelay2" + i.ToString();
                nudDelay2.Size = new System.Drawing.Size(60, 21);
                nudDelay2.Maximum = 100;
                nudDelay2.Minimum = (decimal)-20;
                nudDelay2.DecimalPlaces = 2;
                nudDelay2.Value = (decimal)CommonSet.asm.dic_Solution[2].lstHieght[i - 1];
                nudDelay2.ValueChanged += NudDelay1_ValueChanged;
                //if (CommonSet.iLevel == 0)
                    nudDelay2.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudDelay2, i + 1, 11);
                lst_nudDelay2.Add(nudDelay2);

                NumericUpDown nudHeight22 = new NumericUpDown();
                nudHeight22.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudHeight22.Location = new System.Drawing.Point(75, 39);
                nudHeight22.Name = "nudHeight22" + i.ToString();
                nudHeight22.Size = new System.Drawing.Size(60, 21);
                nudHeight22.Maximum = 20;
                nudHeight22.Minimum = (decimal)-20;
                nudHeight22.DecimalPlaces = 3;
                nudHeight22.Value = (decimal)CommonSet.asm.dic_Solution[2].lstHeight2[i - 1];
                nudHeight22.ValueChanged += nudHeight21_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudHeight22.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudHeight22, i + 1, 12);
                lst_nudHeight22.Add(nudHeight22);

                NumericUpDown nudAng3 = new NumericUpDown();
                nudAng3.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudAng3.Location = new System.Drawing.Point(75, 39);
                nudAng3.Name = "nudAng3" + i.ToString();
                nudAng3.Size = new System.Drawing.Size(60, 21);
                nudAng3.Minimum = 0;
                nudAng3.Maximum = 360;
                nudAng3.Value = (decimal)CommonSet.asm.dic_Solution[3].lstAngle[i - 1];
                nudAng3.ValueChanged += NudAng1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudAng3, i + 1, 13);
                lst_nudAng3.Add(nudAng3);

                NumericUpDown nudPre3 = new NumericUpDown();
                nudPre3.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudPre3.Location = new System.Drawing.Point(75, 39);
                nudPre3.Name = "nudPre3" + i.ToString();
                nudPre3.Size = new System.Drawing.Size(60, 21);
                nudPre3.Maximum = 60;
                nudPre3.Minimum = 0;
                nudPre3.DecimalPlaces = 2;
                nudPre3.Value = (decimal)CommonSet.asm.dic_Solution[3].lstPressure[i - 1];
                nudPre3.ValueChanged += NudPre1_ValueChanged;
                this.tableLayoutPanel1.Controls.Add(nudPre3, i + 1, 14);
                lst_nudPre3.Add(nudPre3);

                NumericUpDown nudVel3 = new NumericUpDown();
                nudVel3.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudVel3.Location = new System.Drawing.Point(75, 39);
                nudVel3.Name = "nudVel3" + i.ToString();
                nudVel3.Size = new System.Drawing.Size(60, 21);
                nudVel3.Minimum = 5;
                nudVel3.Maximum = 200;
                nudVel3.Value = (decimal)CommonSet.asm.dic_Solution[3].lstVel[i - 1];
                nudVel3.ValueChanged += NudVel1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudVel3.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudVel3, i + 1, 15);
                lst_nudVel3.Add(nudVel3);

                NumericUpDown nudTime3 = new NumericUpDown();
                nudTime3.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudTime3.Location = new System.Drawing.Point(75, 39);
                nudTime3.Name = "nudTime3" + i.ToString();
                nudTime3.Size = new System.Drawing.Size(60, 21);
                nudTime3.Maximum = 2;
                nudTime3.Minimum = (decimal)0.001;
                nudTime3.DecimalPlaces = 3;
                nudTime3.Value = (decimal)CommonSet.asm.dic_Solution[3].lstTime[i - 1];
                nudTime3.ValueChanged += NudTime1_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudTime3.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudTime3, i + 1, 16);
                lst_nudTime3.Add(nudTime3);

                NumericUpDown nudDelay3 = new NumericUpDown();
                nudDelay3.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudDelay3.Location = new System.Drawing.Point(75, 39);
                nudDelay3.Name = "nudDelay3" + i.ToString();
                nudDelay3.Size = new System.Drawing.Size(60, 21);
                nudDelay3.Maximum = 100;
                nudDelay3.Minimum = (decimal)-20;
                nudDelay3.DecimalPlaces = 2;
                nudDelay3.Value = (decimal)CommonSet.asm.dic_Solution[3].lstHieght[i - 1];
                nudDelay3.ValueChanged += NudDelay1_ValueChanged;
                //if (CommonSet.iLevel == 0)
                    nudDelay3.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudDelay3, i + 1, 17);
                lst_nudDelay3.Add(nudDelay3);

                NumericUpDown nudHeight23 = new NumericUpDown();
                nudHeight23.Anchor = System.Windows.Forms.AnchorStyles.None;
                nudHeight23.Location = new System.Drawing.Point(75, 39);
                nudHeight23.Name = "nudHeight23" + i.ToString();
                nudHeight23.Size = new System.Drawing.Size(60, 21);
                nudHeight23.Maximum = 20;
                nudHeight23.Minimum = (decimal)-20;
                nudHeight23.DecimalPlaces = 3;
                nudHeight23.Value = (decimal)CommonSet.asm.dic_Solution[3].lstHeight2[i - 1];
                nudHeight23.ValueChanged += nudHeight21_ValueChanged;
                if (CommonSet.iLevel == 0)
                    nudHeight23.Enabled = false;
                this.tableLayoutPanel1.Controls.Add(nudHeight23, i + 1, 18);
                lst_nudHeight23.Add(nudHeight23);

            }
            nudMin1.Value = (decimal)CommonSet.asm.dic_Solution[1].iMinNum;
            nudMax1.Value = (decimal)CommonSet.asm.dic_Solution[1].iMaxNum;
            nudMin2.Value = (decimal)CommonSet.asm.dic_Solution[2].iMinNum;
            nudMax2.Value = (decimal)CommonSet.asm.dic_Solution[2].iMaxNum;
            nudMin3.Value = (decimal)CommonSet.asm.dic_Solution[3].iMinNum;
            nudMax3.Value = (decimal)CommonSet.asm.dic_Solution[3].iMaxNum;
          
            rbtnT.Checked = CommonSet.bDirect;
            rbtnF.Checked = !CommonSet.bDirect;

        }

        void nudHeight21_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(10, 1));
            int num = Convert.ToInt32(nud.Name.Substring(11));
            CommonSet.asm.dic_Solution[index].lstHeight2[num - 1] = (double)nud.Value;
        }

        private void NudTime1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(7, 1));
            int num = Convert.ToInt32(nud.Name.Substring(8));
            CommonSet.asm.dic_Solution[index].lstTime[num - 1] = (double)nud.Value;
        }
        private void NudDelay1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(8, 1));
            int num = Convert.ToInt32(nud.Name.Substring(9));
            CommonSet.asm.dic_Solution[index].lstHieght[num - 1] = (double)nud.Value;
        }

        private void NudVel1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(6, 1));
            int num = Convert.ToInt32(nud.Name.Substring(7));
            CommonSet.asm.dic_Solution[index].lstVel[num - 1] = (double)nud.Value;
        }

        private void NudPre1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32( nud.Name.Substring(6,1));
            int num = Convert.ToInt32(nud.Name.Substring(7));
            CommonSet.asm.dic_Solution[index].lstPressure[num - 1] = (double)nud.Value;
           // int i 
        }

        private void NudAng1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(6, 1));
            int num = Convert.ToInt32(nud.Name.Substring(7));
            CommonSet.asm.dic_Solution[index].lstAngle[num - 1] = (double)nud.Value;
        }

        private void nudMin1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(6, 1));
            CommonSet.asm.dic_Solution[index].iMinNum = (int)nud.Value;
        }

        private void nudMax1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            int index = Convert.ToInt32(nud.Name.Substring(6, 1));
            CommonSet.asm.dic_Solution[index].iMaxNum = (int)nud.Value;
        }

        private void btnSaveAndExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSolutionSet_FormClosed(object sender, FormClosedEventArgs e)
        {
          
            CommonSet.asm.saveSolutionParam(CommonSet.strAssemSolutionPath);

        }

        private void rbtnT_CheckedChanged(object sender, EventArgs e)
        {
            CommonSet.bDirect = rbtnT.Checked;
        }

        private void rbtnF_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
