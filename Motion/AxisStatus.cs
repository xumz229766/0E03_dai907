using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Motion0
{
    public partial class AxisStatus : UserControl
    {


        public AxisStatus()
        {
            InitializeComponent();
        }
        public void setControlValue(AXStatus axStatus)
        {
            setColor(lblALM, axStatus.ALM);
            setColor(lblPEL, axStatus.PEL);
            setColor(lblMEL, axStatus.MEL);
            setColor(lblORG, axStatus.ORG);
            setColor(lblEMG, axStatus.EMG);
            setColor(lblEZ, axStatus.EZ);
            setColor(lblINP, axStatus.INP);
            setColor(lblSVON, axStatus.SVON);
            setColor(lblRDY, axStatus.RDY);
            lblVel.Text = axStatus.dVel.ToString("0.000");
            lblPos.Text = axStatus.dPos.ToString("0.000");
            lblCmdPos.Text = axStatus.dCmdPos.ToString("0.000");
            
        }
        private void setColor(Label l, bool b)
        {
            if (b)
                l.Image = Properties.Resources.ball_red;
            else
                l.Image = Properties.Resources.ball_gray;
        }
    }
}
