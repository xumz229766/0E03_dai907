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
namespace Assembly
{
    public partial class FrmCalibPressure : Form
    {

        MotionCard mc = null;
        public int iCalibPos = 1;//1代表工位1,2代表工位2

        bool bInit = false;
        AXIS axisX, axisY, axisZ;
        public int iCurrentSuction = 1;//组装1吸笔关系
        public int iCurrentSuction2 = 10;// 组装2吸笔关系

       

        public FrmCalibPressure()
        {
            InitializeComponent();
        }
        BindingList<MeasurePressure> bList1;
        BindingList<MeasurePressure> bList2;
        private void FrmCalibPressure_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();

            bInit = false;
            setNumerialControl(nudX, AssemblePressureRelation.p1.X);
            setNumerialControl(nudY, AssemblePressureRelation.p1.Y);
            setNumerialControl(nudStartPosZ, AssemblePressureRelation.p1.Z);

            setNumerialControl(nudX2, AssemblePressureRelation.p2.X);
            setNumerialControl(nudY2, AssemblePressureRelation.p2.Y);
            setNumerialControl(nudStartPosZ2, AssemblePressureRelation.p2.Z);

            bInit = true;

            bList1 = new BindingList<MeasurePressure>( AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction]);
            bList2 = new BindingList<MeasurePressure>(AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction2]);
           
            int width = 80;
            this.dataGridView1.DataSource = bList1;
            this.dataGridView1.Columns[0].HeaderText = "电压(V)";
            this.dataGridView1.Columns[1].HeaderText = "压力(N)";
            this.dataGridView1.Columns[0].Width = width;
            this.dataGridView1.Columns[1].Width = width;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridView2.DataSource = bList2;
            this.dataGridView2.Columns[0].HeaderText = "电压(V)";
            this.dataGridView2.Columns[1].HeaderText = "压力(N)";
            this.dataGridView2.Columns[0].Width = width;
            this.dataGridView2.Columns[1].Width = width;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
        }

        public void UpdateUI()
        {
            try
            {
                if (bInit)
                    UpdateControl();
            }
            catch (Exception)
            {


            }
        }

        public void UpdateControl()
        {

            try
            {
                if (Run.runMode == RunMode.手动)
                {
                    bList1 = new BindingList<MeasurePressure>(AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction]);
                    bList2 = new BindingList<MeasurePressure>(AssemblePressureRelation.dic_RelationPressAndV2[iCurrentSuction2]);
                    this.dataGridView1.DataSource = bList1;
                    this.dataGridView2.DataSource = bList2;
                }
                else
                {
                    if (CalibPressureModule.iStation == 1)
                    {
                        bList1 = new BindingList<MeasurePressure>(AssemblePressureRelation.dic_RelationPressAndV1[CalibPressureModule.iCurrentSuction]);
                    }
                    else
                    {
                        bList2 = new BindingList<MeasurePressure>(AssemblePressureRelation.dic_RelationPressAndV2[CalibPressureModule.iCurrentSuction]);
                    }
                    this.dataGridView1.DataSource = bList1;
                    this.dataGridView2.DataSource = bList2;
                
                }
                int count = AssemblePressureRelation.dic_RelationPressAndV1[iCurrentSuction].Count;

                for (int i = 0; i < count; i++)
                {
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = (i + 1).ToString();

                }
            }
            catch (Exception)
            {
                
                
            }

          

            if (Run.runMode == RunMode.压力测试)
            {

                btnTest.Text = "测试中。。";
                btnTest2.Text = "测试中。。";

                if (Run.bPressureControlFlag)
                {
                    btnStop.Text = "暂 停";
                    btnStop2.Text = "暂 停";
                }
                else
                {
                    btnStop.Text = "继 续";
                    btnStop2.Text = "继 续";
                }
            }
            else
            {
                btnTest.Text = "开始测试";
                btnStop.Text = "暂 停";
                btnTest2.Text = "开始测试";
                btnStop2.Text = "暂 停";
            }
            if (bInit)
            {
                setNumerialControl(nudX, AssemblePressureRelation.p1.X);
                setNumerialControl(nudY, AssemblePressureRelation.p1.Y);
                setNumerialControl(nudStartPosZ, AssemblePressureRelation.p1.Z);

                setNumerialControl(nudX2, AssemblePressureRelation.p2.X);
                setNumerialControl(nudY2, AssemblePressureRelation.p2.Y);
                setNumerialControl(nudStartPosZ2, AssemblePressureRelation.p2.Z);
            }
            //if (rbtnStation1.Checked)
            //{
            //    iCalibPos = 1;

            //    axisX = AXIS.组装X1轴;
            //    axisY = AXIS.组装Y1轴;
            //    axisZ = AXIS.组装Z1轴;
            //    setNumerialControl(nudX, AssembleHeightRelation.p1.X);
            //    setNumerialControl(nudY, AssembleHeightRelation.p1.X);
            //    setNumerialControl(nudStartPosZ, AssembleHeightRelation.p1.Z);

            //}
            //else if (rbtnStation2.Checked)
            //{
            //    iCalibPos = 2;

            //    axisX = AXIS.组装X1轴;
            //    axisY = AXIS.组装Y2轴;
            //    axisZ = AXIS.组装Z1轴;
            //    setNumerialControl(nudX, AssembleHeightRelation.p2.X);
            //    setNumerialControl(nudY, AssembleHeightRelation.p2.X);
            //    setNumerialControl(nudStartPosZ, AssembleHeightRelation.p2.Z);

            //}
           
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
            if (Run.runMode == RunMode.压力测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }

            if (rbtnSingle.Checked)
            {
                CalibPressureModule.pTest.X = (double)nudX.Value;
                CalibPressureModule.pTest.Y = (double)nudY.Value;
                CalibPressureModule.dDistX = (double)nudDistX.Value;
                CalibPressureModule.dPosZ = (double)nudStartPosZ.Value;
                if (rbtnStation1.Checked)
                {
                    CalibPressureModule.iStation = 1;
                    CalibPressureModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    CalibPressureModule.iStation = 2;
                    CalibPressureModule.axisY = AXIS.组装Y2轴;
                }

                CalibPressureModule.axisX = AXIS.组装X1轴;
               
                CalibPressureModule.axisZ = AXIS.组装Z1轴;
                CalibPressureModule.iCurrentSuction = iCurrentSuction;
                CalibPressureModule.iStartSuction = 1;
                CalibPressureModule.iEndSuction = iCurrentSuction;
                CalibPressureModule.iTimes = 0;

            }
            else
            {
                CalibPressureModule.pTest.X = (double)nudX.Value;
                CalibPressureModule.pTest.Y = (double)nudY.Value;
                CalibPressureModule.dDistX = (double)nudDistX.Value;
                CalibPressureModule.dPosZ = (double)nudStartPosZ.Value;
                if (rbtnStation1.Checked)
                {
                    CalibPressureModule.iStation = 1;
                    CalibPressureModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    CalibPressureModule.iStation = 2;
                    CalibPressureModule.axisY = AXIS.组装Y2轴;
                }

                CalibPressureModule.axisX = AXIS.组装X1轴;
               
                CalibPressureModule.axisZ = AXIS.组装Z1轴;
                CalibPressureModule.iCurrentSuction = 1;
                CalibPressureModule.iStartSuction = 1;
                CalibPressureModule.iEndSuction = 9;
                CalibPressureModule.iTimes = 0;
            }
            Run.bPressureControlFlag = true;
            Run.calibPre.IStep = 0;
            Run.runMode = RunMode.压力测试;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.bPressureControlFlag = !Run.bPressureControlFlag;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            if (rbtnStation1.Checked)
            {
                nudX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
                nudY.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;

            }
            else
            {
                nudX.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
                nudY.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (axisZ == AXIS.组装Z1轴))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (axisZ == AXIS.组装Z2轴))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (rbtnStation1.Checked)
            {
                mc.AbsMove(AXIS.组装X1轴, (double)nudX.Value, (int)50);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudY.Value, (int)50);

            }
            else
            {
                mc.AbsMove(AXIS.组装X1轴, (double)nudX.Value, (int)50);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudY.Value, (int)50);
            }

        }

        private void radioButton9_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtn = (RadioButton)sender;
                iCurrentSuction = Convert.ToInt32(rbtn.Name.Substring(11));

           
            }
            catch (Exception)
            {


            }
        }

        private void radioButton18_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtn = (RadioButton)sender;
                iCurrentSuction2 = Convert.ToInt32(rbtn.Name.Substring(11));
                
            }
            catch (Exception)
            {
                
               
            }
        }

        private void nudStartPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveControl();

        }
        private void SaveControl()
        {
              AssemblePressureRelation.p1.X = (double)nudX.Value;
              AssemblePressureRelation.p1.Y = (double)nudY.Value;
              AssemblePressureRelation.p1.Z = (double)nudStartPosZ.Value;

              AssemblePressureRelation.p2.X = (double)nudX2.Value;
              AssemblePressureRelation.p2.Y = (double)nudY2.Value;
              AssemblePressureRelation.p2.Z = (double)nudStartPosZ2.Value;

        }

        private void btnGet2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            if (rbtnStation1.Checked)
            {
                nudX2.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
                nudY2.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;

            }
            else
            {
                nudX2.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
                nudY2.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            }
        }

        private void nudY2_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveControl();
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            if (Run.runMode == RunMode.压力测试)
            {
                Run.runMode = RunMode.手动;
                return;
            }
            if (rbtnSingle2.Checked)
            {
                CalibPressureModule.pTest.X = (double)nudX2.Value;
                CalibPressureModule.pTest.Y = (double)nudY2.Value;
                CalibPressureModule.dDistX = (double)nudDistX2.Value;
                CalibPressureModule.dPosZ = (double)nudStartPosZ2.Value;
                if (rbtnStation1.Checked)
                {
                    CalibPressureModule.iStation = 1;
                    CalibPressureModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    CalibPressureModule.iStation = 2;
                    CalibPressureModule.axisY = AXIS.组装Y2轴;
                }

                CalibPressureModule.axisX = AXIS.组装X2轴;
              
                CalibPressureModule.axisZ = AXIS.组装Z2轴;
                CalibPressureModule.iCurrentSuction = iCurrentSuction2;
                CalibPressureModule.iStartSuction = 10;
                CalibPressureModule.iEndSuction = iCurrentSuction2;
                CalibPressureModule.iTimes = 0;

            }
            else
            {
                CalibPressureModule.pTest.X = (double)nudX2.Value;
                CalibPressureModule.pTest.Y = (double)nudY2.Value;
                CalibPressureModule.dDistX = (double)nudDistX2.Value;
                CalibPressureModule.dPosZ = (double)nudStartPosZ2.Value;
                if (rbtnStation1.Checked)
                {
                    CalibPressureModule.iStation = 1;
                    CalibPressureModule.axisY = AXIS.组装Y1轴;
                }
                else
                {
                    CalibPressureModule.iStation = 2;
                    CalibPressureModule.axisY = AXIS.组装Y2轴;
                }

                CalibPressureModule.axisX = AXIS.组装X2轴;
             
                CalibPressureModule.axisZ = AXIS.组装Z2轴;
                CalibPressureModule.iCurrentSuction = 10;
                CalibPressureModule.iStartSuction = 10;
                CalibPressureModule.iEndSuction = 18;
                CalibPressureModule.iTimes = 0;
            }
            Run.bPressureControlFlag = true;
            Run.calibPre.IStep = 0;
            Run.runMode = RunMode.压力测试;
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            Run.bPressureControlFlag = !Run.bPressureControlFlag;
        }

        private void btnGo2_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)) && (axisZ == AXIS.组装Z1轴))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }
            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)) && (axisZ == AXIS.组装Z2轴))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }
            if (rbtnStation1.Checked)
            {
                mc.AbsMove(AXIS.组装X2轴, (double)nudX2.Value, (int)50);
                mc.AbsMove(AXIS.组装Y1轴, (double)nudY2.Value, (int)50);

            }
            else
            {
                mc.AbsMove(AXIS.组装X2轴, (double)nudX2.Value, (int)50);
                mc.AbsMove(AXIS.组装Y2轴, (double)nudY2.Value, (int)50);
            }

        }

        private void btnGoZ_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblResult1.Text = AssemblePressureRelation.GetVBySuction1(iCurrentSuction, (double)numericUpDown1.Value).ToString();
            lblResult2.Text = AssemblePressureRelation.GetVBySuction2(iCurrentSuction2, (double)numericUpDown2.Value).ToString();
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = "D:\\";
            sfd.RestoreDirectory = true;
            if (DialogResult.OK == sfd.ShowDialog())
            {
                string direct = sfd.FileName.Remove(sfd.FileName.LastIndexOf("\\")) + "\\"; ;
                AssemblePressureRelation.WriteFile(direct,"吸笔和压力关系");
            }
        }

        private void btnTestPressure_Click(object sender, EventArgs e)
        {
            CalibPressureModule.bTestZPressure = true;
            CalibPressureModule.iTestZTimes = 0;
            CalibPressureModule.iTestZTotal = (int)nudTimes.Value;
            btnTest_Click(null, null);
           
        }

        private void btnTestPressure2_Click(object sender, EventArgs e)
        {
            CalibPressureModule.bTestZPressure = true;
            CalibPressureModule.iTestZTimes = 0;
            CalibPressureModule.iTestZTotal = (int)nudTimes2.Value;
            btnTest2_Click(null, null);
            
        }
    }
}
