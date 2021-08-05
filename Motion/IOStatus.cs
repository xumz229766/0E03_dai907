using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace Motion
{
    public partial class IOStatus : UserControl
    {
        string strName = "";
        bool bValue = false;
        string serial = "";
        public Action<object, EventArgs> clickProcess;
        public IOStatus(string strSerial,string name,bool _bValue)
        {
            InitializeComponent();
            setSerial(strSerial);
            setValue(_bValue);
            setName(name);
        }
        public void setValue(bool _bValue) {
            bValue = _bValue;
            if (bValue)
            {
                lblStatus.Image = Properties.Resources.state_green;
            }
            else {
                lblStatus.Image = Properties.Resources.state_grey;
            }
        }
        public bool getValue()
        {
            return bValue;
        }
        public void setSerial(string s)
        {
            serial = s;
            lblSerial.Text = serial;
            
        }
        public void setName(string _name)
        {
            strName = _name;
            lblText.Text = strName;
        }
        public string getName()
        {
            return strName;
        }

        private void IOStatus_Click(object sender, EventArgs e)
        {
            if (clickProcess != null)
                clickProcess(sender,e);
        }

        private void lblText_Click(object sender, EventArgs e)
        {
            if(clickProcess != null)
                 clickProcess(this, e);
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            if(clickProcess !=null)
                 clickProcess(this, e);
        }
    }
}
