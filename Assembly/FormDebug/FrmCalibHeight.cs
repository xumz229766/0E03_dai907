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
using System.IO;
namespace Assembly
{
    public partial class FrmCalibHeight : Form
    {
        MotionCard mc = null;
        public int iCalibPos = 1;//1代表工位1,2代表工位2
      
        bool bInit = false;
        AXIS axisX, axisY, axisZ;
        public FrmCalibHeight()
        {
            InitializeComponent();
        }
        public void UpdateUI()
        {
            try
            {
                if(bInit)
                 UpdateControl();
            }
            catch (Exception)
            {
                
                
            }
        }
        public void UpdateControl()
        {
            if (bList1 != null)
            {
                bList1.ResetBindings();
                bList2.ResetBindings();
                bList3.ResetBindings();
                bList4.ResetBindings();
                for (int i = 0; i < 9; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = "吸笔"+(i + 1).ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = "吸笔" + (i + 1).ToString();
                    dataGridView3.Rows[i].HeaderCell.Value = "吸笔" + (i + 10).ToString();
                    dataGridView4.Rows[i].HeaderCell.Value = "吸笔" + (i + 10).ToString();
                }
            }

            if (Run.runMode == RunMode.高度测试)
            {

                btnTest.Text = "测试中。。";
                if (Run.bHeightControlFlag)
                {
                    btnStop.Text = "暂 停";
                }
                else
                {
                    btnStop.Text = "继 续";
                 }
            }
            else
            {
                btnTest.Text = "开始测试";
                btnStop.Text = "暂 停";
            }
           

            if (radioButton1.Checked)
            {
                iCalibPos = 1;

                axisX = AXIS.组装X1轴;
                axisY = AXIS.组装Y1轴;
                axisZ = AXIS.组装Z1轴;
                setNumerialControl(nudX, AssembleHeightRelation.p1.X);
                setNumerialControl(nudY, AssembleHeightRelation.p1.Y);
                setNumerialControl(nudStartPosZ, AssembleHeightRelation.p1.Z);
                nudSerial.Minimum = 1;
                nudSerial.Maximum = 9;
            }
            else if (radioButton2.Checked)
            {
                iCalibPos = 2;

                axisX = AXIS.组装X1轴;
                axisY = AXIS.组装Y2轴;
                axisZ = AXIS.组装Z1轴;
                setNumerialControl(nudX, AssembleHeightRelation.p2.X);
                setNumerialControl(nudY, AssembleHeightRelation.p2.Y);
                setNumerialControl(nudStartPosZ, AssembleHeightRelation.p2.Z);
                nudSerial.Minimum = 1;
                nudSerial.Maximum = 9;
            }
            else if (radioButton3.Checked)
            {
                iCalibPos = 1;
            
                axisX = AXIS.组装X2轴;
                axisY= AXIS.组装Y1轴;
                axisZ = AXIS.组装Z2轴;
                setNumerialControl(nudX, AssembleHeightRelation.p3.X);
                setNumerialControl(nudY, AssembleHeightRelation.p3.Y);
                setNumerialControl(nudStartPosZ, AssembleHeightRelation.p3.Z);
                nudSerial.Minimum = 10;
                nudSerial.Maximum = 18;
            }
            else if (radioButton4.Checked)
            {
                iCalibPos = 2;
               
                axisX = AXIS.组装X2轴;
                axisY = AXIS.组装Y2轴;
                axisZ = AXIS.组装Z2轴;
                setNumerialControl(nudX, AssembleHeightRelation.p4.X);
                setNumerialControl(nudY, AssembleHeightRelation.p4.Y);
                setNumerialControl(nudStartPosZ, AssembleHeightRelation.p4.Z);
                nudSerial.Minimum = 10;
                nudSerial.Maximum = 18;
            }
        }
        private void SaveControl()
        {
            if (radioButton1.Checked)
            {
                AssembleHeightRelation.p1.X = (double)nudX.Value;
                AssembleHeightRelation.p1.Y = (double)nudY.Value;
                AssembleHeightRelation.p1.Z = (double)nudStartPosZ.Value;
               
            }
            else if (radioButton2.Checked)
            {

                AssembleHeightRelation.p2.X = (double)nudX.Value;
                AssembleHeightRelation.p2.Y = (double)nudY.Value;
                AssembleHeightRelation.p2.Z = (double)nudStartPosZ.Value;
            }
            else if (radioButton3.Checked)
            {


                AssembleHeightRelation.p3.X = (double)nudX.Value;
                AssembleHeightRelation.p3.Y = (double)nudY.Value;
                AssembleHeightRelation.p3.Z = (double)nudStartPosZ.Value;
            }
            else if (radioButton4.Checked)
            {

                AssembleHeightRelation.p4.X = (double)nudX.Value;
                AssembleHeightRelation.p4.Y = (double)nudY.Value;
                AssembleHeightRelation.p4.Z = (double)nudStartPosZ.Value;
            }
        }
        private void setNumerialControl(NumericUpDown nud, double value)
        {
            if (!nud.Focused)
            {
                if (value < (double)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (double)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.高度测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            CalibHeightModule.dTargetN = (double)nudPressure.Value;
            if (rbtnSingle.Checked)
            {
                CalibHeightModule.pTest.X = (double)nudX.Value;
                CalibHeightModule.pTest.Y = (double)nudY.Value;
                CalibHeightModule.dDistX = (double)nudDistX.Value;
                CalibHeightModule.dPosZ = (double)nudStartPosZ.Value;
                CalibHeightModule.dSetMoveDistZ = (double)nudDistZ.Value;
                CalibHeightModule.iStation = iCalibPos;

                CalibHeightModule.axisX = axisX;
                CalibHeightModule.axisY = axisY;
                CalibHeightModule.axisZ = axisZ;
                if (axisX == AXIS.组装X1轴)
                {
                    CalibHeightModule.iStartSuction = 1;
                    CalibHeightModule.iCurrentSuction = (int)nudSerial.Value;
                    CalibHeightModule.iEndSuction = (int)nudSerial.Value;
                }
                else
                {
                    CalibHeightModule.iStartSuction = 10;
                    CalibHeightModule.iCurrentSuction = (int)nudSerial.Value;
                    CalibHeightModule.iEndSuction = (int)nudSerial.Value;
                }
            }
            else
            {
                CalibHeightModule.pTest.X = (double)nudX.Value;
                CalibHeightModule.pTest.Y = (double)nudY.Value;
                CalibHeightModule.dDistX = (double)nudDistX.Value;
                CalibHeightModule.dPosZ = (double)nudStartPosZ.Value;
                CalibHeightModule.dSetMoveDistZ = (double)nudDistZ.Value;
                CalibHeightModule.iStation = iCalibPos;

                CalibHeightModule.axisX = axisX;
                CalibHeightModule.axisY = axisY;
                CalibHeightModule.axisZ = axisZ;
                if (axisX == AXIS.组装X1轴)
                {
                    CalibHeightModule.iStartSuction = 1;
                    CalibHeightModule.iCurrentSuction = 1;
                    CalibHeightModule.iEndSuction = 9;
                }
                else
                {
                    CalibHeightModule.iStartSuction = 10;
                    CalibHeightModule.iCurrentSuction = 10;
                    CalibHeightModule.iEndSuction = 18;
                }
            }
            Run.calibHeight.IStep = 0;
            Run.bHeightControlFlag = true;
            Run.runMode = RunMode.高度测试;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //TestAddData();
          
            Run.bHeightControlFlag = !Run.bHeightControlFlag;
        }
        private void TestAddData()
        {
            for(int i = 0;i<9;i++)
            {
                AssembleHeightRelation.lstRelationAssem1Station1[i].PosZ = 1;
                AssembleHeightRelation.lstRelationAssem1Station1[i].MeasureZ = i;
            }
        }
        BindingList<MeasurePosZ> bList1;
        BindingList<MeasurePosZ> bList2;
        BindingList<MeasurePosZ> bList3;
        BindingList<MeasurePosZ> bList4;
        private void FrmCalibHeight_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            bInit = false;
            UpdateControl();
            bInit = true;
            bList1 = new BindingList<MeasurePosZ>(AssembleHeightRelation.lstRelationAssem1Station1);
            bList2 = new BindingList<MeasurePosZ>(AssembleHeightRelation.lstRelationAssem1Station2);
            bList3 = new BindingList<MeasurePosZ>(AssembleHeightRelation.lstRelationAssem2Station1);
            bList4 = new BindingList<MeasurePosZ>(AssembleHeightRelation.lstRelationAssem2Station2);

            int width =80;
            this.dataGridView1.DataSource = bList1;
            this.dataGridView1.Columns[0].HeaderText = "Z轴位置";
            this.dataGridView1.Columns[1].HeaderText = "测量高度";
            this.dataGridView1.Columns[0].Width = width;
            this.dataGridView1.Columns[1].Width = width;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridView2.DataSource = bList2;
            this.dataGridView2.Columns[0].HeaderText = "Z轴位置";
            this.dataGridView2.Columns[1].HeaderText = "测量高度";
            this.dataGridView2.Columns[0].Width = width;
            this.dataGridView2.Columns[1].Width = width;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridView3.DataSource = bList3;
            this.dataGridView3.Columns[0].HeaderText = "Z轴位置";
            this.dataGridView3.Columns[1].HeaderText = "测量高度";
            this.dataGridView3.Columns[0].Width = width;
            this.dataGridView3.Columns[1].Width = width;
            this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridView4.DataSource = bList4;
            this.dataGridView4.Columns[0].HeaderText = "Z轴位置";
            this.dataGridView4.Columns[1].HeaderText = "测量高度";
            this.dataGridView4.Columns[0].Width = width;
            this.dataGridView4.Columns[1].Width = width;
            this.dataGridView4.EnableHeadersVisualStyles = false;
            this.dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
           
        }

        private void nudDistX_ValueChanged(object sender, EventArgs e)
        {

            if (!bInit)
                return;
            SaveControl();
        }

        private void nudStartPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveControl();
        }
        private void nudY_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveControl();
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudX.Value = (decimal)mc.dic_Axis[axisX].dPos;
            nudY.Value = (decimal)mc.dic_Axis[axisY].dPos;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (axisZ== AXIS.组装Z1轴))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (axisZ == AXIS.组装Z2轴))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            mc.AbsMove(axisX, (double)nudX.Value, (int)50);
            mc.AbsMove(axisY, (double)nudY.Value, (int)50);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            bInit = false;
            UpdateControl();
            bInit = true;
        }

        private void nudX_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveControl();
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = "D:\\";
            sfd.RestoreDirectory = true;
            if (DialogResult.OK == sfd.ShowDialog())
            {
               string direct = sfd.FileName.Remove(sfd.FileName.LastIndexOf("\\"))+"\\";
               AssembleHeightRelation.WriteFile(direct, AssembleHeightRelation.lstRelationAssem1Station1, "组装1部工位1高度数据");
               AssembleHeightRelation.WriteFile(direct, AssembleHeightRelation.lstRelationAssem1Station2, "组装1部工位2高度数据");
               AssembleHeightRelation.WriteFile(direct, AssembleHeightRelation.lstRelationAssem2Station1, "组装2部工位1高度数据");
               AssembleHeightRelation.WriteFile(direct, AssembleHeightRelation.lstRelationAssem2Station2, "组装2部工位2高度数据");
            }
        }

       
    }
}
