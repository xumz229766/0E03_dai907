using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using HalconDotNet;
namespace ImageProcess0
{
    public partial class FrmProcess : Form
    {
        ProcessFatory pf = null;
        public static string strProjectPath = Application.StartupPath + "\\Project\\";
        public string strProjectName = "";
        HalconDotNet.HObject hImage = null;
        HTuple hv_Width, hv_Height;
       // SynchronizationContext context = null;
        HWindow hwin = null;
        ImageProcessManager manager = null;
        bool bLoadProject = false;//是否直接由其它程序调用导入项目
        public FrmProcess()
        {
            bLoadProject = false;
            InitializeComponent();
        }

        public FrmProcess(string strProjectName,HObject image)
        {
            bLoadProject = true;
            InitializeComponent();
            manager = ImageProcessManager.GetInstance();
            if (Directory.Exists(ImageProcessManager.strFilePath + strProjectName))
            {
                lstItems.Items.Add(strProjectName);
                //return;
            }
            else
            {
                //创建项目目录
                try
                {
                    ProcessFatory fatory = new ProcessFatory(strProjectName);
                    manager.AddImageFactory(strProjectName, fatory);
                    Directory.CreateDirectory(ImageProcessManager.strFilePath + strProjectName);
                    lstItems.Items.Add(strProjectName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("添加项目异常!");
                }
            }
            try
            {
                this.strProjectName = strProjectName;
                string strSelect = strProjectName;
                pf = manager.GetProcessFactory(strSelect);
                hwin = hWindowControl1.HalconWindow;
                manager.SetWindow(strSelect, hwin);
                this.Text = "图像处理--" + pf.strName;
                toolStrip1.Enabled = true;
                cbMakeModel.Checked = pf.bUse[0];
                cbMeasureCircle.Checked = pf.bUse[1];
                cbRegionAngle.Checked = pf.bUse[2];
                cbRegionArea.Checked = pf.bUse[3];

                cmbAngleOutput.Text = pf.eSrcAng.ToString();
                cmbCenterOutput.Text = pf.eSrcCenter.ToString();
                cmbExist.Text = pf.eSrcExist.ToString();

               // HOperatorSet.ReadImage(out hImage, ofd.FileName);
                if (image != null)
                {
                    hImage = image;
                    HOperatorSet.GetImageSize(image, out hv_Width, out hv_Height);
                    pf.hImage = image;
                    pf.fitWindow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入设置异常！"+ex.ToString());
            }
            lstItems.Enabled = false;
            btnNew.Enabled = false;
            btnSelect.Enabled = false;
            btnDelete.Enabled = false;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
          string value = Interaction.InputBox("请输入项目名:", "添加项目", "检测");
          if (value.Trim().Equals(""))
              return;
          if (lstItems.Items.Contains(value))
          {
              MessageBox.Show("不能添加已有项目名！");
              return;
          }
          if (Directory.Exists(ImageProcessManager.strFilePath + value))
          {
              lstItems.Items.Add(value);
              return;
          }
          
           //创建项目目录
          try {
                ProcessFatory fatory = new ProcessFatory(value);
                manager.AddImageFactory(value,fatory);
                Directory.CreateDirectory(ImageProcessManager.strFilePath + value);
              lstItems.Items.Add(value);
          }
          catch (Exception ex) {
              MessageBox.Show("添加项目异常!");
          }
          
        }
        private string[] getProjects()
        {
           //string[] directs = Directory.GetDirectories(strProjectPath);
           //List<string> lst = new List<string>();
           //foreach (string name in directs)
           //{
           //   string item = name.Substring(name.LastIndexOf("\\")+1);
           //   lst.Add(item);
           //}
           return manager.GetFactoryNames();
        }
        private void FrmProcess_Load(object sender, EventArgs e)
        {
            if (!bLoadProject)
            {
                manager = ImageProcessManager.GetInstance();
                hwin = hWindowControl1.HalconWindow;
                lstItems.Items.AddRange(getProjects());
            }
           
            
           
        }
        private void groupBoxFun_Paint(PaintEventArgs e, GroupBox groupBox, Brush br)
        {
            int h = groupBox.Height;
            int w = groupBox.Width;
            e.Graphics.Clear(groupBox.BackColor);


            Pen p1 = new Pen(br, 40);
            Pen p2 = new Pen(br, 10);

            e.Graphics.DrawLine(p1, 0, 0, w, 0);
            //e.Graphics.DrawLine(p1, e.Graphics.MeasureString(groupBox.Text, groupBox.Font).Width + 8 + 2, 7, groupBox.Width - 2, 7);

            e.Graphics.DrawLine(p2, 1, 7, 1, groupBox.Height);
            e.Graphics.DrawLine(p2, 1, groupBox.Height, groupBox.Width, groupBox.Height);
            e.Graphics.DrawLine(p2, groupBox.Width, 7, groupBox.Width, groupBox.Height);

            float sw = e.Graphics.MeasureString(groupBox.Text, groupBox.Font).Width;
            //Font f = new Font(Font.
            e.Graphics.DrawString(groupBox.Text, groupBox.Font, Brushes.Black, (w - sw) / 2, 2);

        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox g = (GroupBox)sender; 
            groupBoxFun_Paint(e, g, Brushes.LightSteelBlue);
        }
        private void groupBoxFun_Paint1(PaintEventArgs e, GroupBox groupBox, Brush br)
        {
            int h = groupBox.Height;
            int w = groupBox.Width;
            e.Graphics.Clear(groupBox.BackColor);


            Pen p1 = new Pen(br, 5);
            Pen p2 = new Pen(br, 5);

            e.Graphics.DrawLine(p1, 0, 0, w, 0);
            //e.Graphics.DrawLine(p1, e.Graphics.MeasureString(groupBox.Text, groupBox.Font).Width + 8 + 2, 7, groupBox.Width - 2, 7);

            e.Graphics.DrawLine(p2, 1, 7, 1, groupBox.Height);
            e.Graphics.DrawLine(p2, 1, groupBox.Height, groupBox.Width, groupBox.Height);
            e.Graphics.DrawLine(p2, groupBox.Width, 7, groupBox.Width, groupBox.Height);

            float sw = e.Graphics.MeasureString(groupBox.Text, groupBox.Font).Width;
            //Font f = new Font(Font.
            e.Graphics.DrawString(groupBox.Text, groupBox.Font, Brushes.Black, (w - sw) / 2, 2);

        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            GroupBox g = (GroupBox)sender;
            groupBoxFun_Paint1(e, g, Brushes.Lavender);
        }
     
        private void btnSelect_Click(object sender, EventArgs e)
        {
            clearResult();
            if (pf != null)
            {
                RemoveControls();
            }
            try
            {
                string strSelect = lstItems.SelectedItem.ToString();
                pf = manager.GetProcessFactory(strSelect);
                manager.SetWindow(strSelect, hwin);
                this.Text = "图像处理--" + pf.strName;
                toolStrip1.Enabled = true;
                cbMakeModel.Checked = pf.bUse[0];
                cbMeasureCircle.Checked = pf.bUse[1];
                cbRegionAngle.Checked = pf.bUse[2];
                cbRegionArea.Checked = pf.bUse[3];

                cmbAngleOutput.Text = pf.eSrcAng.ToString();
                cmbCenterOutput.Text = pf.eSrcCenter.ToString();
                cmbExist.Text = pf.eSrcExist.ToString();
            }
            catch (Exception ex)
            { }
        }

        private void toolReadImg_Click(object sender, EventArgs e)
        {
            clearResult();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BMP文件(*.bmp)|*.bmp";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                HOperatorSet.ReadImage(out hImage, ofd.FileName);
                HOperatorSet.GetImageSize(hImage, out hv_Width, out hv_Height);               
                pf.hImage = hImage;
                pf.fitWindow();
                
            }
            
        }

        #region"缩放"

        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            if (pf == null)
                return;
            try
            {

                HOperatorSet.ClearWindow(hwin);
                int hv_MouseRow, hv_MouseColumn, hv_Button;

                hwin.GetMposition(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                // HOperatorSet.GetMpositionSubPix(mc.hwindow, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                //HOperatorSet.GetMposition(mc.hwindow, out hv_MouseRow, out hv_MouseColumn, out hv_Button);
                if (e.Delta > 0)
                {
                    pf.DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 1.3);                   

                }
                else
                {
                    pf.DisplayPart(hwin, hv_MouseColumn, hv_MouseRow, 0.7); 
                }
                pf.showObj();
               
                //control.DisplayImage(mc.hwindow, ho_Image, startX, startY, endX, endY);
                // displayObj();

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }
        }
        private void hWindowControl1_MouseEnter(object sender, EventArgs e)
        {
            hWindowControl1.Focus();
        }
        #endregion

        private void toolFitWindow_Click(object sender, EventArgs e)
        {
            try
            {
                if (pf != null)
                    pf.fitWindow();
            }
            catch (Exception ex) { }
        }

        private void toolShowRow_MouseMove(object sender, MouseEventArgs e)
        {
             
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            if (pf == null)
                return;
            try
            {
                double hv_MouseRow, hv_MouseColumn; 
                 int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);
               
                toolShowCol.Text = hv_MouseColumn.ToString("0.000");
                toolShowRow.Text = hv_MouseRow.ToString("0.000");
               
                if ((hv_Button == 4) && (toolErase.BackColor == Color.Gray))
                {
                    HObject hoErase;
                    HOperatorSet.GenCircle(out hoErase, hv_MouseRow, hv_MouseColumn,Convert.ToInt32(toolStripSize.Text));
                    if (ucMakeModel != null)
                        ucMakeModel.addErase(hoErase);                    
                }
                //if (hv_Button == 4)
                //    pf.fitWindow();
            }
            catch (Exception ex) { }
        }

        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            if (pf == null)
                return;
            try
            {
                double hv_MouseRow, hv_MouseColumn;
                int hv_Button;
                hwin.GetMpositionSubPix(out hv_MouseRow, out hv_MouseColumn, out hv_Button);

                
                if ((hv_Button == 4) && (toolErase.BackColor == Color.Gray))
                {
                    HObject hoErase;
                    HOperatorSet.GenCircle(out hoErase, hv_MouseRow, hv_MouseColumn, Convert.ToInt32(toolStripSize.Text));
                    if (ucMakeModel != null)
                        ucMakeModel.addErase(hoErase);
                    return;
                }
                //if (hv_Button == 4)
                //    pf.fitWindow();
            }
            catch (Exception ex) { }
        }

        private void hWindowControl1_Resize(object sender, EventArgs e)
        {
            if (pf == null)
                return;
            try
            {
                pf.fitWindow();
            }
            catch (Exception ex) { }
        }

        private void RemoveControls()
        {
            groupBox5.Text = "参数设置";
            if (ucMakeModel != null)
            {
                if (toolErase.BackColor == Color.Gray)
                    toolErase.BackColor = Color.LightSteelBlue;
                toolRemove_Click(this, null);
                ucMakeModel.Release();
                ucMakeModel = null;
                toolErase.Enabled = false;
                toolStripSize.Enabled = false;
                toolRemove.Enabled = false;              
                pf.showObj();
            }
            if (ucMCircle != null)
            {               
                ucMCircle.Release();
                ucMCircle = null;                
                pf.showObj();

            }
            if (ucRA != null)
            {
                ucRA.Release();
                ucRA = null;
                pf.showObj();
            }
            if (ucRArea != null)
            {
                ucRArea.Release();
                ucRArea = null;
                pf.showObj();
            }
           
            PanelSet.Controls.Clear();
        }
        UCMakeModel ucMakeModel = null;
        private void btnModelSet_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveControls();
                toolErase.Enabled = true;
                toolStripSize.Enabled = true;
                toolRemove.Enabled = true;
                MakeModel m = pf.getProcessMethod(eMethod.定位_模板匹配) as MakeModel;
                ucMakeModel = new UCMakeModel(hwin, m);
                ucMakeModel.Dock = DockStyle.Fill;
                Font f = new Font("宋体",10);
                ucMakeModel.Font = f;
                PanelSet.Controls.Add(ucMakeModel);
                pf.showObj();
                groupBox5.Text = "参数设置-" + "定位_模板匹配";
            }
            catch (Exception ex) { }
        }
       
        UCMeasureCircle ucMCircle = null;
        private void btnMCircleSet_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveControls();
                MeasureCircle m = pf.getProcessMethod(eMethod.测量_圆) as MeasureCircle;
                ucMCircle = new UCMeasureCircle(m);
                ucMCircle.Dock = DockStyle.Fill;
                PanelSet.Controls.Add(ucMCircle);
                pf.showObj();
                groupBox5.Text = "参数设置-" + "测量_圆";
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开设置异常:" + ex.ToString());
            }
        }
        UCRegionAngle ucRA = null;
        private void btnRATest_Click(object sender, EventArgs e)
        {
            try {
                RemoveControls();
                RegionAngle ra = pf.getProcessMethod(eMethod.区域_环) as RegionAngle;
                ucRA = new UCRegionAngle(ra);
                ucRA.Dock = DockStyle.Fill;
                ucRA.Font = new Font("宋体", 10);
                PanelSet.Controls.Add(ucRA);
                pf.showObj();
                groupBox5.Text = "参数设置-" + "区域_环";
            }
            catch (Exception ex)
            { }
        }
        UCRegionArea ucRArea = null;
        private void btn_RAreaTest_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveControls();
                RegionArea ra = pf.getProcessMethod(eMethod.区域_面积) as RegionArea;
                ucRArea = new UCRegionArea(ra);
                ucRArea.Dock = DockStyle.Fill;
                ucRArea.Font = new Font("宋体", 10);
                PanelSet.Controls.Add(ucRArea);
                pf.showObj();
                groupBox5.Text = "参数设置-" + "区域_面积";
            }
            catch (Exception ex)
            { }
        }
        private void toolErase_Click(object sender, EventArgs e)
        {
            if (toolErase.BackColor == Color.LightSteelBlue)
                toolErase.BackColor = Color.Gray;
            else {
                toolErase.BackColor = Color.LightSteelBlue;
            }

        }

        private void toolRemove_Click(object sender, EventArgs e)
        {
            if (ucMakeModel != null)
                ucMakeModel.RemoveErase();
        }

        private void cbMakeModel_CheckedChanged(object sender, EventArgs e)
        {
            gpModelSet.Enabled = cbMakeModel.Checked;
            string strMethod = eMethod.定位_模板匹配.ToString();
          
            if (cbMakeModel.Checked)
            {
                if (!cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Add(strMethod);
                }
                if (!cmbCenterOutput.Items.Contains(strMethod))
                {
                    cmbCenterOutput.Items.Add(strMethod);
                }
                if (!cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Add(strMethod);
                }
            }
            else
            {
                if (cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Remove(strMethod);
                }
                if (cmbCenterOutput.Items.Contains(strMethod))
                {
                    cmbCenterOutput.Items.Remove(strMethod);
                }
                if (cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Remove(strMethod);
                }
            }
        }

        private void chMeasureCircle_CheckedChanged(object sender, EventArgs e)
        {
            gpMeasureCircle.Enabled = cbMeasureCircle.Checked;
            string strMethod = eMethod.测量_圆.ToString();
          
            if (cbMeasureCircle.Checked)
            {
                if (!cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Add(strMethod);
                }
                if (!cmbCenterOutput.Items.Contains(strMethod))
                {
                    cmbCenterOutput.Items.Add(strMethod);
                }
                if (!cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Add(strMethod);
                }
            }
            else
            {
                if (cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Remove(strMethod);
                }
                if (cmbCenterOutput.Items.Contains(strMethod))
                {
                    cmbCenterOutput.Items.Remove(strMethod);
                }
                if (cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Remove(strMethod);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (manager.SaveParam(strProjectName))
            {
                MessageBox.Show("保存参数成功!");
            }
            else
            {
                MessageBox.Show("保存参数失败!");
            }
        }

        private void cbRegionAngle_CheckedChanged(object sender, EventArgs e)
        {

            gpRegionAngle.Enabled = cbRegionAngle.Checked;
            string strMethod = eMethod.区域_环.ToString();
           
            if (cbRegionAngle.Checked)
            {
                if (!cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Add(strMethod);
                }
                if (!cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Add(strMethod);
                }
            }
            else
            {
                if (cmbAngleOutput.Items.Contains(strMethod))
                {
                    cmbAngleOutput.Items.Remove(strMethod);
                }
                if (cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Remove(strMethod);
                }
            }
        }
        private void cbRegionArea_CheckedChanged(object sender, EventArgs e)
        {
            gpRegionArea.Enabled = cbRegionArea.Checked;
            string strMethod = eMethod.区域_面积.ToString();

            if (cbRegionArea.Checked)
            {
               
                if (!cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Add(strMethod);
                }
            }
            else
            {
               
                if (cmbExist.Items.Contains(strMethod))
                {
                    cmbExist.Items.Remove(strMethod);
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            clearResult();
            RemoveControls();
            pf.bUse[0] = cbMakeModel.Checked;
            pf.bUse[1] = cbMeasureCircle.Checked;
            pf.bUse[2] = cbRegionAngle.Checked;
            pf.bUse[3] = cbRegionArea.Checked;
            if(cmbCenterOutput.Text.Equals("")||cmbAngleOutput.Text.Equals(""))
            {
                MessageBox.Show("角度输出或中心输出不能为空!");
                return;
            }
            pf.eSrcAng =(eMethod)Enum.Parse(typeof(eMethod), cmbAngleOutput.Text);
            pf.eSrcCenter = (eMethod)Enum.Parse(typeof(eMethod), cmbCenterOutput.Text);
            pf.eSrcExist = (eMethod)Enum.Parse(typeof(eMethod), cmbExist.Text);
            pf.Action(pf.hImage);
            pf.showObj();
            string[] strResult = pf.strOutputString.Split(',');
            if (strResult[0].Equals("OK"))
            {
                lblResult.BackColor = Color.Green;
            }
            else {
                lblResult.BackColor = Color.Red;
            }
            
            #region "检测项结果显示赋值"

            IProcess ip = null;
            if (pf.bUse[0])
            {
                ip = pf.getTestObj(eMethod.定位_模板匹配);
                DisplayColor(lblModelResult, ip.bTestResult);
                //DisplayResult(lblModelAngle, ip, OutputResult.Model_Angle);
                //DisplayResult(lblModelScore, ip, OutputResult.Model_Score);
               
            }
            if (pf.bUse[1])
            {
                ip = pf.getTestObj(eMethod.测量_圆);
                DisplayColor(lblMCResult, ip.bTestResult);
                //DisplayResult(lblMCRow, ip, OutputResult.MC_Row);
                //DisplayResult(lblMCCol, ip, OutputResult.MC_Col);
            }
            if (pf.bUse[2])
            {
                ip = pf.getTestObj(eMethod.区域_环);
                DisplayColor(lblRAResult, ip.bTestResult);
                //DisplayResult(lblRAAngle, ip, OutputResult.RA_Angle);
                //DisplayResult(lblRAArea, ip, OutputResult.RA_Area);
            }
            if (pf.bUse[3])
            {
                ip = pf.getTestObj(eMethod.区域_面积);
                DisplayColor(lblRAreaResult, ip.bTestResult);
            }

            #endregion

            lblResult.Text = strResult[0];          
            lblResultAngle.Text = strResult[1];
            lblOutRow.Text = strResult[2];
            lblOutCol.Text = strResult[3];
            lblResultAMth.Text = pf.eSrcAng.ToString();
            lblResultCMth.Text = pf.eSrcCenter.ToString();
            if (strResult[4].Equals("True"))
            {
                lblExistResult.BackColor = Color.Green;
            }
            else
            {
                lblExistResult.BackColor = Color.Red;
            }
            toolRunTime.Text = pf.dRunTime.ToString("0");

        }
        public void DisplayColor(Label l, bool result)
        {
            if (result)
            {
                l.BackColor = Color.Green;
                l.Text = "OK";
            }
            else {
                l.BackColor = Color.Red;
                l.Text = "NG";
            }
        }
        public void DisplayResult(Label l, IProcess p, OutputResult or)
        {
            if (p.dic_outResult.ContainsKey(or))
            {
                l.Text = p.dic_outResult[or].ToString("0.000");
            }
            else {
                l.Text = "0";
            }
        }
        public void clearResult()
        {
            toolRunTime.Text = "0";
            lblResult.Text = "NULL";
            lblResult.BackColor = System.Drawing.SystemColors.Control;
            lblResultAngle.Text = "0";
            lblOutRow.Text = "0";
            lblOutCol.Text = "0";
            lblResultAMth.Text = "";
            lblResultCMth.Text = "";

            lblModelResult.Text = "NULL";
            lblModelResult.BackColor = System.Drawing.SystemColors.Control;
            //lblModelAngle.Text = "0";
            //lblModelScore.Text = "0";

            lblMCResult.Text = "NULL";
            lblMCResult.BackColor = System.Drawing.SystemColors.Control;
            //lblMCRow.Text = "0";
            //lblMCCol.Text = "0";

            lblRAResult.Text = "NULL";
            lblRAResult.BackColor = System.Drawing.SystemColors.Control;
            //lblRAAngle.Text = "0";
            //lblRAArea.Text = "0";
            lblRAreaResult.Text = "NULL";
            lblRAreaResult.BackColor = System.Drawing.SystemColors.Control;

        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strSelect = lstItems.SelectedItem.ToString();
                DialogResult dr = MessageBox.Show("是否删除-" + strSelect + "-检测项", "删除", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    if (pf.strName == strSelect)
                    {
                        clearResult();
                        if (pf != null)
                        {
                            RemoveControls();
                        }
                        toolStrip1.Enabled = false;
                        cbMakeModel.Checked = false;
                        cbMeasureCircle.Checked = false;
                        cbRegionAngle.Checked = false;
                        pf = null;
                    }
                    try
                    {
                        manager.RemoveImageFactory(strSelect);
                        Directory.Delete(ImageProcessManager.strFilePath + strSelect, true);
                    }
                    catch (Exception ex) { }

                    lstItems.Items.Remove(strSelect);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除异常:"+ex.ToString());
            }


        }

      

       

    }
}
