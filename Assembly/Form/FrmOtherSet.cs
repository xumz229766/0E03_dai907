using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CameraSet;
using Motion;
namespace Assembly
{
    public partial class FrmOtherSet : Form
    {

        MotionCard mc = null;
        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        bool bInit = false;//代表是否初始化完成

        public FrmTestFlash frmTestFlash = null;
        FrmCalibHeight frmCalibHeight = null;
        FrmCalibPressure frmCalibPre = null;
        public FrmOtherSet()
        {
            InitializeComponent();
        }

        private void btnSingleTest_Click(object sender, EventArgs e)
        {
            FrmTestAxiscs frmTest = new FrmTestAxiscs();
            frmTest.ShowDialog();
            frmTest = null;
        }


        private void btnLightSet_Click(object sender, EventArgs e)
        {

            FrmSetLight frmLight = new FrmSetLight();
            frmLight.ShowDialog();
            frmLight = null;
        }

        private void FrmOtherSet_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();

            SetControl();

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
        private void setNumerialControl(NumericUpDown nud, int value)
        {
            if (!nud.Focused)
            {
                if (value < (int)nud.Minimum)
                {
                    nud.Value = nud.Minimum;
                    return;
                }
                if (value > (int)nud.Maximum)
                {
                    nud.Value = nud.Maximum;
                    return;
                }
                nud.Value = (decimal)value;
            }

        }
        public void SetControl()
        {
            bInit = false;
            InitControl();
            bInit = true;
        }
        public void UpdateUI()
        {
            try
            {
                InitControl();

                lblDA1.Text = mc.ReadOutAD(1).ToString();
                lblDA2.Text = mc.ReadOutAD(2).ToString();
                //lblDA2.Text = LeiE3032.ReadOutAD2(0x3002, 2).ToString();
                if (CommonSet.iLevel == 2)
                {
                    groupBox5.Visible = true;
                }
                else
                {
                    groupBox5.Visible = false;
                }
            }
            catch (Exception)
            {
                
                
            }

            try
            {
                if (frmCalibHeight != null)
                    frmCalibHeight.UpdateUI();
            }
            catch (Exception)
            {

            }
            try
            {
                if (frmCalibPre != null)
                    frmCalibPre.UpdateUI();
            }
            catch (Exception)
            {

            }
            
        }
        public void InitControl()
        {
            setNumerialControl(nudSafeOpt1X, OptSution1.pSafeXYZ.X);
            setNumerialControl(nudSafeOpt1Y, OptSution1.pSafeXYZ.Y);
            setNumerialControl(nudSafeOpt1Z, OptSution1.pSafeXYZ.Z);

            setNumerialControl(nudSafeOpt2X, OptSution2.pSafeXYZ.X);
            setNumerialControl(nudSafeOpt2Y, OptSution2.pSafeXYZ.Y);
            setNumerialControl(nudSafeOpt2Z, OptSution2.pSafeXYZ.Z);


            setNumerialControl(nudSafeAssem1X, AssembleSuction1.pSafeXYZ.X);
            setNumerialControl(nudSafeAssem1Y, AssembleSuction1.pSafeXYZ.Y);
            setNumerialControl(nudSafeAssem1Z, AssembleSuction1.pSafeXYZ.Z);


            setNumerialControl(nudSafeAssem2X, AssembleSuction2.pSafeXYZ.X);
            setNumerialControl(nudSafeAssem2Y, AssembleSuction2.pSafeXYZ.Y);
            setNumerialControl(nudSafeAssem2Z, AssembleSuction2.pSafeXYZ.Z);

            setNumerialControl(nudSafeGlueX, BarrelSuction.pSafe.X);
            setNumerialControl(nudSafeGlueY, BarrelSuction.pSafe.Y);
            setNumerialControl(nudSafeGlueZ, BarrelSuction.pSafe.Z);

            setNumerialControl(nudVelX, CommonSet.dVelRunX);
            setNumerialControl(nudVelY, CommonSet.dVelRunY);
            setNumerialControl(nudVelZ, CommonSet.dVelRunZ);
            setNumerialControl(nudVelC, CommonSet.dVelC);
            setNumerialControl(nudVelFlashX, CommonSet.dVelFlashX);
            setNumerialControl(nudVelLenY, CommonSet.dVelLenY);
            setNumerialControl(nudVelGlueX, CommonSet.dVelGlueX);
            setNumerialControl(nudVelGlueY, CommonSet.dVelGlueY);
            setNumerialControl(nudVelGlueZ, CommonSet.dVelGlueZ);
            setNumerialControl(nudVelZ2, CommonSet.dVelAssemble2);
            setNumerialControl(nudVelGlueC, CommonSet.dVelGlueC);
            cbUseAssem1.Checked = BarrelSuction.bUseAssem1;
            cbUseAssem2.Checked = BarrelSuction.bUseAssem2;

            checkBox1.Checked = CommonSet.dic_OptSuction1[1].BUse;
            checkBox2.Checked = CommonSet.dic_OptSuction1[2].BUse;
            checkBox3.Checked = CommonSet.dic_OptSuction1[3].BUse;
            checkBox4.Checked = CommonSet.dic_OptSuction1[4].BUse;
            checkBox5.Checked = CommonSet.dic_OptSuction1[5].BUse;
            checkBox6.Checked = CommonSet.dic_OptSuction1[6].BUse;
            checkBox7.Checked = CommonSet.dic_OptSuction1[7].BUse;
            checkBox8.Checked = CommonSet.dic_OptSuction1[8].BUse;
            checkBox9.Checked = CommonSet.dic_OptSuction1[9].BUse;

            checkBox11.Checked = CommonSet.dic_OptSuction2[10].BUse;
            checkBox12.Checked = CommonSet.dic_OptSuction2[11].BUse;
            checkBox13.Checked = CommonSet.dic_OptSuction2[12].BUse;
            checkBox14.Checked = CommonSet.dic_OptSuction2[13].BUse;
            checkBox15.Checked = CommonSet.dic_OptSuction2[14].BUse;
            checkBox16.Checked = CommonSet.dic_OptSuction2[15].BUse;
            checkBox17.Checked = CommonSet.dic_OptSuction2[16].BUse;
            checkBox18.Checked = CommonSet.dic_OptSuction2[17].BUse;
            checkBox19.Checked = CommonSet.dic_OptSuction2[18].BUse;

            cbTest.Checked = CommonSet.bTest;
            cbUseGlue.Checked = CommonSet.bUseGlue;
            cbAutoTestMode.Checked = CommonSet.bAutoTestHeight;

            setNumerialControl(nudDA1, CommonSet.fCommonPressureV1);
            setNumerialControl(nudDA2, CommonSet.fCommonPressureV2);

            setNumerialControl(nudTimes, CommonSet.iTimes);
            setNumerialControl(nudGetTimes, CommonSet.iGetTimes);
        }

        private void SaveUI()
        {
            OptSution1.pSafeXYZ.X = (double)nudSafeOpt1X.Value;
            OptSution1.pSafeXYZ.Y = (double)nudSafeOpt1Y.Value;
            OptSution1.pSafeXYZ.Z = (double)nudSafeOpt1Z.Value;

            OptSution2.pSafeXYZ.X = (double)nudSafeOpt2X.Value;
            OptSution2.pSafeXYZ.Y = (double)nudSafeOpt2Y.Value;
            OptSution2.pSafeXYZ.Z = (double)nudSafeOpt2Z.Value;

            AssembleSuction1.pSafeXYZ.X = (double)nudSafeAssem1X.Value;
            AssembleSuction1.pSafeXYZ.Y = (double)nudSafeAssem1Y.Value;
            AssembleSuction1.pSafeXYZ.Z = (double)nudSafeAssem1Z.Value;

            AssembleSuction2.pSafeXYZ.X = (double)nudSafeAssem2X.Value;
            AssembleSuction2.pSafeXYZ.Y = (double)nudSafeAssem2Y.Value;
            AssembleSuction2.pSafeXYZ.Z = (double)nudSafeAssem2Z.Value;

            BarrelSuction.pSafe.X = (double)nudSafeGlueX.Value;
            BarrelSuction.pSafe.Y = (double)nudSafeGlueY.Value;
            BarrelSuction.pSafe.Z = (double)nudSafeGlueZ.Value;

            CommonSet.dVelRunX = (double)nudVelX.Value;
            CommonSet.dVelRunY= (double)nudVelY.Value;
            CommonSet.dVelRunZ = (double)nudVelZ.Value;
            CommonSet.dVelC = (double)nudVelC.Value;
            CommonSet.dVelFlashX = (double)nudVelFlashX.Value;
            CommonSet.dVelLenY = (double)nudVelLenY.Value;
            CommonSet.dVelGlueX = (double)nudVelGlueX.Value;
            CommonSet.dVelGlueY = (double)nudVelGlueY.Value;
            CommonSet.dVelGlueZ = (double)nudVelGlueZ.Value;
            CommonSet.dVelAssemble2 = (double)nudVelZ2.Value;
            CommonSet.dVelGlueC = (double)nudVelGlueC.Value;

            BarrelSuction.bUseAssem1 = cbUseAssem1.Checked;
            BarrelSuction.bUseAssem2 = cbUseAssem2.Checked;

            CommonSet.dic_OptSuction1[1].BUse = checkBox1.Checked;
            CommonSet.dic_OptSuction1[2].BUse = checkBox2.Checked;
            CommonSet.dic_OptSuction1[3].BUse = checkBox3.Checked;
            CommonSet.dic_OptSuction1[4].BUse = checkBox4.Checked;
            CommonSet.dic_OptSuction1[5].BUse = checkBox5.Checked;
            CommonSet.dic_OptSuction1[6].BUse = checkBox6.Checked;
            CommonSet.dic_OptSuction1[7].BUse = checkBox7.Checked;
            CommonSet.dic_OptSuction1[8].BUse = checkBox8.Checked;
            CommonSet.dic_OptSuction1[9].BUse = checkBox9.Checked;

            CommonSet.dic_OptSuction2[10].BUse = checkBox11.Checked;
            CommonSet.dic_OptSuction2[11].BUse = checkBox12.Checked;
            CommonSet.dic_OptSuction2[12].BUse = checkBox13.Checked;
            CommonSet.dic_OptSuction2[13].BUse = checkBox14.Checked;
            CommonSet.dic_OptSuction2[14].BUse = checkBox15.Checked;
            CommonSet.dic_OptSuction2[15].BUse = checkBox16.Checked;
            CommonSet.dic_OptSuction2[16].BUse = checkBox17.Checked;
            CommonSet.dic_OptSuction2[17].BUse = checkBox18.Checked;
            CommonSet.dic_OptSuction2[18].BUse = checkBox19.Checked;
            CommonSet.bTest = cbTest.Checked;
            CommonSet.bUseGlue = cbUseGlue.Checked;
            CommonSet.bAutoTestHeight = cbAutoTestMode.Checked;

            CommonSet.fCommonPressureV1 = (float)nudDA1.Value;
            CommonSet.fCommonPressureV2 = (float)nudDA2.Value;

            CommonSet.iGetTimes = (int)nudGetTimes.Value;
            CommonSet.iTimes = (int)nudTimes.Value;



        }

       

        private void checkBox1_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int index = Convert.ToInt32(cb.Name.Substring(8));
            if (index > 9)
            {
                CommonSet.dic_OptSuction2[index-1].BUse = cb.Checked;
                CommonSet.dic_Assemble2[index - 1].BUse = cb.Checked;
            }
            else
            {
                CommonSet.dic_OptSuction1[index].BUse = cb.Checked;
                CommonSet.dic_Assemble1[index].BUse = cb.Checked;
            }


        }

        private void cbUseAssem1_Click(object sender, EventArgs e)
        {
            BarrelSuction.bUseAssem1 = cbUseAssem1.Checked;
        }

        private void cbUseAssem2_Click(object sender, EventArgs e)
        {
            BarrelSuction.bUseAssem2 = cbUseAssem2.Checked;
        }

        private void btnGetSafeOpt1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudSafeOpt1X.Value = (decimal)mc.dic_Axis[AXIS.取料X1轴].dPos;
            nudSafeOpt1Y.Value = (decimal)mc.dic_Axis[AXIS.取料Y1轴].dPos;
            nudSafeOpt1Z.Value = (decimal)mc.dic_Axis[AXIS.取料Z1轴].dPos;

        }

        private void btnGetSafeOpt2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudSafeOpt2X.Value = (decimal)mc.dic_Axis[AXIS.取料X2轴].dPos;
            nudSafeOpt2Y.Value = (decimal)mc.dic_Axis[AXIS.取料Y2轴].dPos;
            nudSafeOpt2Z.Value = (decimal)mc.dic_Axis[AXIS.取料Z2轴].dPos;


        }

        private void btnGoSafeOpt1XY_Click(object sender, EventArgs e)
        {

            if ((mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("取料Z1轴低于安全位！");
                return;
            }

            double x = OptSution1.pSafeXYZ.X;
            double y = OptSution1.pSafeXYZ.Y;
            mc.AbsMove(AXIS.取料X1轴, x, iVelRunX);
            mc.AbsMove(AXIS.取料Y1轴, y, iVelRunY);

        }

        private void btnGoSafeOpt2XY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("取料Z2轴低于安全位！");
                return;
            }
          
            double x = OptSution2.pSafeXYZ.X;
            double y = OptSution2.pSafeXYZ.Y;
            mc.AbsMove(AXIS.取料X2轴, x, iVelRunX);
            mc.AbsMove(AXIS.取料Y2轴, y, iVelRunY);



        }

        private void btnGetSafeAssem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudSafeAssem1X.Value = (decimal)mc.dic_Axis[AXIS.组装X1轴].dPos;
            nudSafeAssem1Y.Value = (decimal)mc.dic_Axis[AXIS.组装Y1轴].dPos;
            nudSafeAssem1Z.Value = (decimal)mc.dic_Axis[AXIS.组装Z1轴].dPos;
        }

        private void btnGetSafeAssem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudSafeAssem2X.Value = (decimal)mc.dic_Axis[AXIS.组装X2轴].dPos;
            nudSafeAssem2Y.Value = (decimal)mc.dic_Axis[AXIS.组装Y2轴].dPos;
            nudSafeAssem2Z.Value = (decimal)mc.dic_Axis[AXIS.组装Z2轴].dPos;
        }

        private void btnGoSafeAssem1XY_Click(object sender, EventArgs e)
        {

            if ((mc.dic_Axis[AXIS.组装X1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }

            double x = AssembleSuction1.pSafeXYZ.X;
            double y = AssembleSuction1.pSafeXYZ.Y;
            mc.AbsMove(AXIS.组装X1轴, x, iVelRunX);
            mc.AbsMove(AXIS.组装Y1轴, y, iVelRunY);
        }

        private void btnGoSafeAssem2XY_Click(object sender, EventArgs e)
        {

            if ((mc.dic_Axis[AXIS.组装X2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }

            double x = AssembleSuction2.pSafeXYZ.X;
            double y = AssembleSuction2.pSafeXYZ.Y;
            mc.AbsMove(AXIS.组装X2轴, x, iVelRunX);
            mc.AbsMove(AXIS.组装Y2轴, y, iVelRunY);
        }

        private void btnGetGlue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否获取当前位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;
            nudSafeGlueX.Value = (decimal)mc.dic_Axis[AXIS.点胶X轴].dPos;
            nudSafeGlueY.Value = (decimal)mc.dic_Axis[AXIS.镜筒Y轴].dPos;
            nudSafeGlueZ.Value = (decimal)mc.dic_Axis[AXIS.点胶Z轴].dPos;

        }

        private void btnGoGlue_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }

            double x = BarrelSuction.pSafe.X;
            double y = BarrelSuction.pSafe.Y;
            mc.AbsMove(AXIS.点胶X轴, x, iVelRunX);
            mc.AbsMove(AXIS.镜筒Y轴, y, iVelRunY);
        }

        private void btnSafeOpt1Z_Click(object sender, EventArgs e)
        {
            double z = OptSution1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.取料Z1轴, z, iVelRunZ);
        }

        private void btnSafeOpt2Z_Click(object sender, EventArgs e)
        {
            double z = OptSution2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.取料Z2轴, z, iVelRunZ);

        }

        private void btnGoSafeAssem1Z_Click(object sender, EventArgs e)
        {
            double z = AssembleSuction1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z1轴, z, iVelRunZ);
        }

        private void btnGoSafeAssem2Z_Click(object sender, EventArgs e)
        {
            double z = AssembleSuction2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z2轴, z, iVelRunZ);
        }

        private void btnGlueSafeZ_Click(object sender, EventArgs e)
        {
            double z = BarrelSuction.pSafe.Z;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void nudVelX_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void nudDA1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                mc.WriteOutDA((float)nudDA1.Value, 0);
                SaveUI();
            }
            catch (Exception)
            {
                
              
            }
        }

        private void nudDA2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bInit)
                    return;
                mc.WriteOutDA((float)nudDA2.Value, 2);
                SaveUI();
            }
            catch (Exception)
            {


            }
        }

        private void btnSetPort_Click(object sender, EventArgs e)
        {
            FrmSetMeasurePort fmp = new FrmSetMeasurePort();
            fmp.ShowDialog();
            fmp = null;
        }

        private void cbTest_Click(object sender, EventArgs e)
        {
            CommonSet.bTest = cbTest.Checked;
        }

        private void cbUseGlue_Click(object sender, EventArgs e)
        {
            CommonSet.bUseGlue = cbUseGlue.Checked;
        }

        private void cbAutoTestMode_Click(object sender, EventArgs e)
        {
            CommonSet.bAutoTestHeight = cbAutoTestMode.Checked;
        }

        private void btnAutoHeight_Click(object sender, EventArgs e)
        {
            frmCalibHeight = new FrmCalibHeight();
            frmCalibHeight.ShowDialog();
            frmCalibHeight = null;
            
        }

        private void btnAutoPressure_Click(object sender, EventArgs e)
        {
            frmCalibPre = new FrmCalibPressure();
            frmCalibPre.ShowDialog();
            frmCalibPre = null;
        }

        private void btnFlashTest_Click(object sender, EventArgs e)
        {
            frmTestFlash = new FrmTestFlash();
            frmTestFlash.ShowDialog();
            frmTestFlash = null;

        }

    }
}
