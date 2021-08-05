using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Assembly
{
    public partial class FrmSerial485 : Form
    {
        SerialAV serial = null;
        SynchronizationContext context = null;
        List<NumericUpDown> lstOutA = new List<NumericUpDown>();
        List<NumericUpDown> lstPressure = new List<NumericUpDown>();
        List<NumericUpDown> lstInV = new List<NumericUpDown>();
       
        public FrmSerial485()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

        private void FrmSerial485_Load(object sender, EventArgs e)
        {
            cbPortName.SelectedIndex = 0;
            serial = SerialAV.GetInstance();
            initControl();
            int iLen = SerialAV.lstOut.Count;
            for (int i = 0; i < iLen; i++)
            {
                lstOutA[i].Value = (decimal)SerialAV.lstOut[i];
                lstPressure[i].Value = (decimal)SerialAV.lstPress[i];
                lstInV[i].Value = (decimal)SerialAV.lstIn[i];
            }

            timer1.Start();
        }
        private void initControl()
        {
            int len = SerialAV.lstOut.Count;
            if (len < 1)
                len = 10;
            for (int i = 1; i < len+1; i++)
            {
                //输出控件mA
                NumericUpDown nud = new NumericUpDown();
                nud.Anchor = System.Windows.Forms.AnchorStyles.None;
                nud.DecimalPlaces = 2;
                nud.Increment = new decimal(new int[] {
                1,
                0,
                0,
                196608});
                nud.Location = new System.Drawing.Point(65, 33);
                nud.Maximum = new decimal(new int[] {
                20,
                0,
                0,
                0});
                nud.Minimum = new decimal(new int[] {
                4,
                0,
                0,
                0});
                nud.Name = "nudOutA"+i.ToString();
                nud.Size = new System.Drawing.Size(75, 21);
                nud.TabIndex = 6;
                nud.Value = new decimal(new int[] {
                4,
                0,
                0,
                0});
                this.tableLayoutPanel1.Controls.Add(nud, 1, i);
                lstOutA.Add(nud);
                //压力值
                nud = new NumericUpDown();
                nud.Anchor = System.Windows.Forms.AnchorStyles.None;
                nud.DecimalPlaces = 2;
                nud.Increment = new decimal(new int[] {
                1,
                0,
                0,
                196608});
                nud.Location = new System.Drawing.Point(65, 33);
                nud.Maximum = new decimal(new int[] {
                10,
                0,
                0,
                0});
                nud.Minimum = new decimal(new int[] {
                0,
                0,
                0,
                0});
                nud.Name = "nudPressure" + i.ToString();
                nud.Size = new System.Drawing.Size(75, 21);
                nud.TabIndex = 6;
                nud.Value = new decimal(new int[] {
                0,
                0,
                0,
                0});
                this.tableLayoutPanel1.Controls.Add(nud, 2, i);
                lstPressure.Add(nud);
                //输入电压
                nud = new NumericUpDown();
                nud.Anchor = System.Windows.Forms.AnchorStyles.None;
                nud.DecimalPlaces = 3;
                nud.Increment = new decimal(new int[] {
                1,
                0,
                0,
                196608});
                nud.Location = new System.Drawing.Point(65, 33);
                nud.Maximum = new decimal(new int[] {
                10,
                0,
                0,
                0});
                nud.Minimum = new decimal(new int[] {
                0,
                0,
                0,
                0});
                nud.Name = "nudInV" + i.ToString();
                nud.Size = new System.Drawing.Size(75, 21);
                nud.TabIndex = 6;
                nud.Value = new decimal(new int[] {
                0,
                0,
                0,
                0});
                this.tableLayoutPanel1.Controls.Add(nud, 3, i);
                lstInV.Add(nud);
            }
        }
        private void btnSetPort_Click(object sender, EventArgs e)
        {
            if (serial.SetSerialPort(cbPortName.Text))
            {
                MessageBox.Show("设置端口成功！");
            }
            else
            {
                MessageBox.Show("设置端口失败！");
            }
        }

        private void btnSetParam_Click(object sender, EventArgs e)
        {
            SerialAV.setModeParam((int)nudOutputAddr.Value,(int)nudInputAddr.Value, (int)nudInputChannel.Value);
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SerialAV.WriteOutputA((int)nudOutputAddr.Value, (double)nudOutputData.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblInputData.Text = SerialAV.dGetData.ToString("0.000");
            lblTime.Text ="Circle: " + SerialAV.circleTime.ToString() + "ms";
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            SerialAV.ReadInputV(SerialAV.iReadAddr, SerialAV.iReadChannel);            
           
        }

        private void FrmSerial485_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ilen = lstInV.Count;
            SerialAV.lstIn.Clear();
            SerialAV.lstOut.Clear();
            SerialAV.lstPress.Clear();
            SerialAV.PaToA = null;
            SerialAV.VToPa = null;
            for (int i = 0; i < ilen; i++)
            {
                SerialAV.lstIn.Add((double)lstInV[i].Value);
                SerialAV.lstOut.Add((double)lstOutA[i].Value);
                SerialAV.lstPress.Add((double)lstPressure[i].Value);
            }
           
            if (serial.SaveParam(CommonSet.strSaveFile))
            {
                MessageBox.Show("保存参数成功！");
            }
            else {
                MessageBox.Show("保存参数失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerialAV.WriteOutputA((int)nudOutputAddr.Value, SerialAV.GetAByPressure((double)numericUpDown1.Value));
        }

       
    }
}
