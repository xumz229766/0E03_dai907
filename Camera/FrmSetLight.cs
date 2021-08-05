using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraSet
{
    public partial class FrmSetLight : Form
    {

        LightManager lManager = null;
        public FrmSetLight()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lManager.SetLightControl(0, lControl1.getLightControl());
            if (lManager.saveLightParam())
            {
                MessageBox.Show("保存参数成功!");
            }
            else
            {
                MessageBox.Show("保存参数失败!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSetLight_Load(object sender, EventArgs e)
        {
            lManager = LightManager.getLightManager();
            lControl1.setLightControl(lManager.getControl(0));
           
        }
    }
}
