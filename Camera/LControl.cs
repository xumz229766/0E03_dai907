using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraSet0
{
    public partial class LControl : UserControl
    {
        LightControl lc = null;
        public LControl()
        {
            InitializeComponent();
        }
        public void setLightControl(LightControl _control)
        {
            lc = _control;

            initControl();
        }
        public LightControl getLightControl()
        {
            return lc;
        }
        private void initControl()
        {
            if (lc != null)
            {
               
                //btnOpenUp.Enabled = lc.bOpen;

                panel1.Enabled = lc.bOpen;
                trackBar1.Value = (int)lc.iIntensity[LightManager.iLightUpChannel-1];
                trackBar2.Value = (int)lc.iIntensity[LightManager.iLightDownChannel-1];
               
                lblValue1.Text = trackBar1.Value.ToString();
                lblValue2.Text = trackBar2.Value.ToString();
                nudChannelUp.Value = (decimal)LightManager.iLightUpChannel;
                nudChannelDown.Value = (decimal)LightManager.iLightDownChannel;
                if (lc.bChannelOpen[LightManager.iLightUpChannel])
                {
                    btnOpenUp.Text = "关闭上光源";
                }
                else
                {
                    btnOpenUp.Text = "打开上光源";
                }
                if (lc.bChannelOpen[LightManager.iLightDownChannel])
                {
                    btnOpenDown.Text = "关闭下光源";
                }
                else
                {
                    btnOpenDown.Text = "打开下光源";
                }
                if (lc.bOpen)
                {
                    cmbPort.Text = lc.strPortName;
                    lc.SetIntensity(LightManager.iLightUpChannel, (int)trackBar1.Value);
                    lc.SetIntensity(LightManager.iLightDownChannel, (int)trackBar2.Value);
                }
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
           
            if (lc != null)
            {
                lc.ClosePort();
                panel1.Enabled = false;
                //btnOpenUp.Enabled = false;
                lc.bOpen = false;
                lc.strPortName = cmbPort.Text;
                if (!lc.OpenPort())
                {
                    MessageBox.Show("打开串口失败!");
                    return;
                }
                else
                {
                    panel1.Enabled = true;
                }
               
            }
        }

       

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (lc != null)
            {
                int channel = (int)nudChannelUp.Value;
                lc.SetIntensity(channel, (int)trackBar1.Value);
                lblValue1.Text = trackBar1.Value.ToString();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (lc != null)
            {
                int channel = (int)nudChannelDown.Value;
                lc.SetIntensity(channel, (int)trackBar2.Value);
                lblValue2.Text = trackBar2.Value.ToString();
            }
        }
       
        private void btnOpenUp_Click(object sender, EventArgs e)
        {
            if (lc.bOpen)
            {
                int channel = (int)nudChannelUp.Value;
                if (lc.bChannelOpen[channel])
                {
                    lc.CloseChannel(channel);                   
                }
                else
                {
                    lc.OpenChanel(channel);
                }
                if (lc.bChannelOpen[channel])
                {
                    btnOpenUp.Text = "关闭上光源";
                }
                else
                {
                    btnOpenUp.Text = "打开上光源";
                }
            }
        }

        private void btnOpenDown_Click(object sender, EventArgs e)
        {
            if (lc.bOpen)
            {
                int channel = (int)nudChannelDown.Value;
                if (lc.bChannelOpen[channel])
                {
                    lc.CloseChannel(channel);
                }
                else
                {
                    lc.OpenChanel(channel);
                }
                if (lc.bChannelOpen[channel])
                {
                    btnOpenDown.Text = "关闭下光源";
                }
                else
                {
                    btnOpenDown.Text = "打开下光源";
                }
            }
        }

        private void nudChannelUp_ValueChanged(object sender, EventArgs e)
        {
            LightManager.iLightUpChannel = (int)nudChannelUp.Value;
        }

        private void nudChannelDown_ValueChanged(object sender, EventArgs e)
        {
            LightManager.iLightDownChannel = (int)nudChannelDown.Value;
        }

       
    }
}
