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
    public partial class FrmSetMeasurePort : Form
    {
        public FrmSetMeasurePort()
        {
            InitializeComponent();
        }

        private void btnSetPort_Click(object sender, EventArgs e)
        {
            if (CommonSet.spMH.SetAndOpenSerialPort(cbPortName.Text, Convert.ToInt32(cbBaudRate.Text)))
            {
                CommonSet.spMH.SaveParam(CommonSet.strProductParamPath + "SerialPortParam.ini");
                MessageBox.Show("设置测高串口成功！");
            }
        }

        private void FrmSetMeasurePort_Load(object sender, EventArgs e)
        {
            cbPortName.Text = CommonSet.spMH.PortName;
            cbBaudRate.Text = CommonSet.spMH.BaudRate.ToString();
            cbPortName1.Text = CommonSet.spPre.PortName1;
            cbBaudRate1.Text = CommonSet.spPre.BaudRate1.ToString();
            cbPortName2.Text = CommonSet.spPre.PortName2;
            cbBaudRate2.Text = CommonSet.spPre.BaudRate2.ToString();

        }

        private void btnSetPot1_Click(object sender, EventArgs e)
        {
            if (CommonSet.spPre.SetAndOpenSerialPort1(cbPortName1.Text, Convert.ToInt32(cbBaudRate1.Text)))
            {
                CommonSet.spPre.SaveParam(CommonSet.strProductParamPath + "SerialPortParam.ini");
                MessageBox.Show("设置测压1串口成功！");
            }
        }

        private void btnSetPot2_Click(object sender, EventArgs e)
        {
            if (CommonSet.spPre.SetAndOpenSerialPort2(cbPortName2.Text, Convert.ToInt32(cbBaudRate2.Text)))
            {
                CommonSet.spPre.SaveParam(CommonSet.strProductParamPath + "SerialPortParam.ini");
                MessageBox.Show("设置测压2串口成功！");
            }
        }
    }
}
