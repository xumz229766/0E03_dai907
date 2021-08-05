using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Assembly
{
    public partial class FrmPasswordDialog : Form
    {
       public string strPassword = "";
        public FrmPasswordDialog()
        {
            
            InitializeComponent();
        }

        private void FrmPasswordDialog_Load(object sender, EventArgs e)
        {

        }

     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            strPassword = txtPassword.Text;
        }
        
    }
}
