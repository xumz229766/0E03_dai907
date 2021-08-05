using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;
namespace Assembly
{
    public partial class FrmTestAxiscs : Form
    {

        List<NumericUpDown> lstNud = new List<NumericUpDown>();
        List<Button> lstGet = new List<Button>();
        List<Button> lstGo = new List<Button>();
        MotionCard mc = null;
        int Num = 2;
        public FrmTestAxiscs()
        {
            InitializeComponent();
            lstNud.Clear();
            lstNud.Add(numericUpDown1);
            lstNud.Add(numericUpDown2);
            lstNud.Add(numericUpDown3);
            lstNud.Add(numericUpDown4);
            lstNud.Add(numericUpDown5);
            lstNud.Add(numericUpDown6);
            lstNud.Add(numericUpDown7);
            lstNud.Add(numericUpDown8);
            lstNud.Add(numericUpDown9);
            lstNud.Add(numericUpDown10);
            lstNud.Add(numericUpDown11);
            lstNud.Add(numericUpDown12);
            lstNud.Add(numericUpDown13);
            lstNud.Add(numericUpDown14);
            lstNud.Add(numericUpDown15);
            lstNud.Add(numericUpDown16);
            lstNud.Add(numericUpDown17);
            lstNud.Add(numericUpDown18);
            lstNud.Add(numericUpDown19);
            lstNud.Add(numericUpDown20);
            lstNud.Add(numericUpDown21);
            lstNud.Add(numericUpDown22);
            lstNud.Add(numericUpDown23);
            lstNud.Add(numericUpDown24);
            lstNud.Add(numericUpDown25);
            lstNud.Add(numericUpDown26);
            lstNud.Add(numericUpDown27);
            lstNud.Add(numericUpDown28);
            lstNud.Add(numericUpDown29);
            lstNud.Add(numericUpDown30);

            lstGet.Clear();
            lstGet.Add(btnGet1);
            lstGet.Add(btnGet2);
            lstGet.Add(btnGet3);
            lstGet.Add(btnGet4);
            lstGet.Add(btnGet5);
            lstGet.Add(btnGet6);
            lstGet.Add(btnGet7);
            lstGet.Add(btnGet8);
            lstGet.Add(btnGet9);
            lstGet.Add(btnGet10);
            lstGet.Add(btnGet11);
            lstGet.Add(btnGet12);
            lstGet.Add(btnGet13);
            lstGet.Add(btnGet14);
            lstGet.Add(btnGet15);
            lstGet.Add(btnGet16);
            lstGet.Add(btnGet17);
            lstGet.Add(btnGet18);
            lstGet.Add(btnGet19);
            lstGet.Add(btnGet20);
            lstGet.Add(btnGet21);
            lstGet.Add(btnGet22);
            lstGet.Add(btnGet23);
            lstGet.Add(btnGet24);
            lstGet.Add(btnGet25);
            lstGet.Add(btnGet26);
            lstGet.Add(btnGet27);
            lstGet.Add(btnGet28);
            lstGet.Add(btnGet29);
            lstGet.Add(btnGet30);

            lstGo.Clear();
            lstGo.Add(btnGo1);
            lstGo.Add(btnGo2);
            lstGo.Add(btnGo3);
            lstGo.Add(btnGo4);
            lstGo.Add(btnGo5);
            lstGo.Add(btnGo6);
            lstGo.Add(btnGo7);
            lstGo.Add(btnGo8);
            lstGo.Add(btnGo9);
            lstGo.Add(btnGo10);
            lstGo.Add(btnGo11);
            lstGo.Add(btnGo12);
            lstGo.Add(btnGo13);
            lstGo.Add(btnGo14);
            lstGo.Add(btnGo15);
            lstGo.Add(btnGo16);
            lstGo.Add(btnGo17);
            lstGo.Add(btnGo18);
            lstGo.Add(btnGo19);
            lstGo.Add(btnGo20);
            lstGo.Add(btnGo21);
            lstGo.Add(btnGo22);
            lstGo.Add(btnGo23);
            lstGo.Add(btnGo24);
            lstGo.Add(btnGo25);
            lstGo.Add(btnGo26);
            lstGo.Add(btnGo27);
            lstGo.Add(btnGo28);
            lstGo.Add(btnGo29);
            lstGo.Add(btnGo30);
        }

        private void btnGet1_Click(object sender, EventArgs e)
        {

             AXIS axis = (AXIS)Enum.Parse(typeof(AXIS),cmbAxis.Text);
            Button btn = (Button)sender;
            string name = btn.Name;
            int i = Convert.ToInt32(name.Substring(6));
            lstNud[i - 1].Value = (decimal)mc.dic_Axis[axis].dPos;
        }

        private void btnGo1_Click(object sender, EventArgs e)
        {

            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), cmbAxis.Text);
            Button btn = (Button)sender;
            string name = btn.Name;
            int i = Convert.ToInt32(name.Substring(5));
            mc.AbsMove(axis, (double)lstNud[i - 1].Value, 100);

        }

        private void FrmTestAxiscs_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            cmbAxis.Items.Clear();
            cmbAxis.Items.Add(AXIS.组装X1轴.ToString());
            cmbAxis.Items.Add(AXIS.组装X2轴.ToString());
            cmbAxis.Items.Add(AXIS.组装Y1轴.ToString());
            cmbAxis.Items.Add(AXIS.组装Y2轴.ToString());
            cmbAxis.Items.Add(AXIS.取料X1轴.ToString());
            cmbAxis.Items.Add(AXIS.取料X2轴.ToString());

            cmbAxis.Text = AXIS.取料X1轴.ToString();
            timer1.Start();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Num = (int)nudNum.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS),cmbAxis.Text);
                lblPos.Text = mc.dic_Axis[axis].dPos.ToString("0.000");

                for (int i = 0; i < 30; i++)
                {

                    if (i > Num - 1)
                    {
                        lstNud[i].Enabled = false;
                        lstGet[i].Enabled = false;
                        lstGo[i].Enabled = false;
                    }
                    else
                    {
                        lstNud[i].Enabled = true;
                        lstGet[i].Enabled = true;
                        lstGo[i].Enabled = true;
                    }
                    if (Run.runMode == RunMode.单轴测试)
                    {
                        btnStart.Text = "测试中。。";
                    }
                    else
                    {
                        btnStart.Text = "开始";
                    }
                }

            }
            catch (Exception)
            {
                
               
            }
        }

        private void 取料X1轴P_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            string name = btn.Name.Trim();
           
            int len = name.Length;
            string direct = name.Substring(len - 1);

            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), cmbAxis.Text);
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = 50;
            }
            else
            {
                vel = -50;
            }
            mc.VelMove(axis, vel);
           
        }

        private void 取料X1轴P_MouseUp(object sender, MouseEventArgs e)
        {
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), cmbAxis.Text);
            mc.StopAxis(axis);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if ((RunMode.单轴测试!=Run.runMode))
            {
                SingleAxisTest.iCurrentNum = 0;
                SingleAxisTest.NUM = Num;
                SingleAxisTest.lstPos.Clear();
                for (int i = 0; i < Num; i++)
                {
                    SingleAxisTest.lstPos.Add((double)lstNud[i].Value);
                }
                SingleAxisTest.stopTime = (double)nudStopTime.Value;
                SingleAxisTest.axis = (AXIS)Enum.Parse(typeof(AXIS), cmbAxis.Text);
                Run.runMode = RunMode.单轴测试;
            }
            else
            {
                Run.runMode = RunMode.手动;
            }
        }

        private void FrmTestAxiscs_FormClosed(object sender, FormClosedEventArgs e)
        {
            Run.runMode = RunMode.手动;
        }
    }
}
