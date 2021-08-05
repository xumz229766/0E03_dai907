using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tray;
using ConfigureFile;
namespace Tray
{
    public partial class TestTray : Form
    {
        Tray t = null;//当前托盘对象
        TrayPanel tp;
        string strFile = Application.StartupPath + "\\Param\\Tray.ini";
        List<int> lstTrayOrder = new List<int>();
        bool bInit = true;
        public TestTray()
        {
            //初始化托盘 
            TrayFactory.initTrayFactory(strFile);
            Tray t = TrayFactory.getTrayFactory("1");
            InitializeComponent();
        }
        public TestTray(List<int> trayOrder,string file,TrayPanel panel = null)
        {
            tp = panel;
            strFile = file;
            if (trayOrder != null)
            {
                lstTrayOrder.AddRange(trayOrder.ToArray());
                //初始化托盘 
                TrayFactory.initTrayFactory(strFile);
                Tray t = TrayFactory.getTrayFactory(lstTrayOrder[0].ToString());
            }
            else
            {
                TrayFactory.initTrayFactory(strFile);
                Tray t = TrayFactory.getTrayFactory("1");
            }
            InitializeComponent();
        }
       
        private void TestTray_Load(object sender, EventArgs e)
        {
            bInit = false;
            this.DoubleBuffered = true;
            int count = TrayFactory.getTrayCount();
            for (int i = 1; i <= count; i++)
            {
                cmbId.Items.Add(i.ToString());
            }
            if(count > 0)
                 cmbId.SelectedIndex = 0;
            cmbStart.SelectedIndex = 0;
            cmbDirect.SelectedIndex = 0;
            cmbRowType.SelectedIndex = 0;
            if(tp == null)
             tp = new TrayPanel();
            tp.Anchor = AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top;
           
            tp.Dock = DockStyle.Fill;
           
            panel1.Controls.Add(tp);
            if (lstTrayOrder.Count > 0)
            {
                cmbId.Text = lstTrayOrder[0].ToString();
                cmbId.Enabled = false;
                btnSelect.Enabled = false;
                txtName.Enabled = false;
                t = TrayFactory.getTrayFactory(cmbId.Text);
                panel3.Enabled = true;
                //t.eStart = (EStartPos)Enum.Parse(typeof(EStartPos), cmbStart.Text);
                //t.eDirect = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), cmbDirect.Text);
                tp.BShowModel = true;
                tp.setTrayObj(t, Color.Gray);
                initControls();
                chShow.Checked = true;
            }
            bInit = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = (int)nudRow.Value;
            int c = (int)nudCol.Value;
            if (r * c > 600)
            {
                MessageBox.Show("数量不能超600");
                return;
            }
            bInit = false;
            cbUnregular1.Checked = false;
            cbUnregular2.Checked = false;
            chShow.Checked = false;
            tp.BShowModel = chShow.Checked;
            t = new Tray(Convert.ToInt32(cmbId.Text), txtName.Text, (int)nudRow.Value, (int)nudCol.Value);
            t.bUnregular = false;
            tp.Dock = DockStyle.Fill;
           
            tp.setTrayObj(t,Color.Blue);
           
            panel3.Enabled = chShow.Checked;
            bInit = true;
           // tp.setLayout((int)nudRow.Value, (int)nudCol.Value);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
            tp.BFlag = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
            tp.BRemoveFlag = checkBox2.Checked;
        }

        private void chShow_CheckedChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            if (t == null)
            {
                MessageBox.Show("当前盘为空，请先创建托盘！");
                return;
            }
            tp.BShowModel = chShow.Checked;
            t.eStart = (EStartPos)Enum.Parse(typeof(EStartPos), cmbStart.Text);
            t.eDirect = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), cmbDirect.Text);
            if (chShow.Checked)
            {
                tp.setLayout(Color.Gray);
            }
            else
            {
                tp.setLayout(Color.Blue);
            }
                panel3.Enabled = chShow.Checked;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            t.eStart = (EStartPos)Enum.Parse(typeof(EStartPos), cmbStart.Text);
            t.eDirect = (EIndexDirect)Enum.Parse(typeof(EIndexDirect), cmbDirect.Text);
            t.strChangeRowType = cmbRowType.Text;
            tp.setLayout(Color.Gray);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {           
            t = TrayFactory.getTrayFactory(cmbId.Text);
            tp.setTrayObj(t,Color.Gray);           
            initControls();
            chShow.Checked = true;
        }
        //根据选择的托盘对象初始化值
        private void initControls()
        {
            if (t != null)
            {
                txtName.Text = t.TName;
                nudCol.Value = t.TMaxCol;
                nudRow.Value = t.TMaxRow;
                cmbStart.Text = t.eStart.ToString();
                cmbDirect.Text = t.eDirect.ToString();
                cmbRowType.Text = t.strChangeRowType;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(lstTrayOrder.Count > 0)
            {
                foreach (int key in lstTrayOrder)
                {
                    TrayFactory.setTray(key.ToString(), t);
                }
            }
            else {
                 TrayFactory.setTray(cmbId.Text, t);
            }
            if (TrayFactory.saveTrayFactoryParam(strFile))
            {
                MessageBox.Show("保存托盘参数成功!");
            }
            else {
                MessageBox.Show("保存托盘参数失败!");
            }
        }

        private void cbUnregular1_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chShow.Checked && cbUnregular1.Checked)
            {
                MessageBox.Show("当前为显示模式，无法更改！");
                cbUnregular1.Checked = false;
                return;
            }
            if (cbUnregular1.Checked)
            {
                cbUnregular2.Checked = false;
                tp.CreateUnRegular1();

            }
        }

        private void cbUnregular2_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chShow.Checked && cbUnregular2.Checked)
            {
                MessageBox.Show("当前为显示模式，无法更改！");
                cbUnregular2.Checked = false;
                return;
            }
            if (cbUnregular2.Checked)
            {
                cbUnregular1.Checked = false;
                tp.CreateUnRegular2();
            }
        }
    }
}
