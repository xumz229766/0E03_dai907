using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
using Motion0;
namespace _0E030
{
    public partial class FrmOtherSet :UIPage
    {
        MotionCard mc = null;
        bool bInit = false;

        public FrmOtherSet()
        {
            InitializeComponent();
        }
        private void FrmOtherSet_Load(object sender, EventArgs e)
        {
            bInit = false;
            mc = MotionCard.getMotionCard();
            UpdateControl();
            string[] values = Enum.GetNames(typeof(AXIS));
            cmbAxis.Items.Clear();
            cmbAxis.Items.AddRange(values);
            cmbAxis.SelectedIndex = 0;
            bInit = true;
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
        private void setNumerialControl(UIDoubleUpDown nud, double value)
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
                nud.Value = value;
            }

        }
        private void setNumerialControl(UIIntegerUpDown nud, int value)
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
                nud.Value = value;
            }

        }

        public void UpdateUI()
        {
            try
            {
              
               
                UpdateControl();
                //InitDataBindings();
                lblAxis.Text = cmbAxis.Text;
                string strAxis = cmbAxis.Text;
                AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
                lblPos.Text = mc.dic_Axis[axis].dPos.ToString("0.000");
                if (mc.dic_Axis[axis].SVON)
                    lblSevon.BackColor = Color.Green;
                else
                    lblSevon.BackColor = Color.LightGray;
            }
            catch (Exception)
            {


            }
        }
        public void UpdateControl()
        {
           
            setNumerialControl(nudVelGetX , ModelManager.VelGetX.Vel);
            setNumerialControl(nudVelGetY, ModelManager.VelGetY.Vel);
            setNumerialControl(nudVelAssemX, ModelManager.VelAssemX.Vel);
            setNumerialControl(nudVelAssemZ, ModelManager.VelZ.Vel);
            setNumerialControl(nudVelBarrelX, ModelManager.VelBarrelX.Vel);
            setNumerialControl(nudVelBarrelY, ModelManager.VelBarrelY.Vel);
            setNumerialControl(nudVelC, ModelManager.VelC.Vel);

            setNumerialControl(nudAccGetX, ModelManager.VelGetX.ACCAndDec);
            setNumerialControl(nudAccGetY, ModelManager.VelGetY.ACCAndDec);
            setNumerialControl(nudAccAssemX, ModelManager.VelAssemX.ACCAndDec);
            setNumerialControl(nudAccAssemZ, ModelManager.VelZ.ACCAndDec);
            setNumerialControl(nudAccBarrelX, ModelManager.VelBarrelX.ACCAndDec);
            setNumerialControl(nudAccBarrelY, ModelManager.VelBarrelY.ACCAndDec);
            setNumerialControl(nudAccC, ModelManager.VelC.ACCAndDec);
            cbUse1.Checked = ModelManager.SuctionLParam.BUse;
            cbUse2.Checked = ModelManager.SuctionRParam.BUse;
        }

        public void SaveUI()
        {
            ModelManager.VelGetX.Vel=(double)nudVelGetX.Value;
            ModelManager.VelGetY.Vel= (double)nudVelGetY.Value;
            ModelManager.VelAssemX.Vel=(double)nudVelAssemX.Value;
            ModelManager.VelZ.Vel=(double)nudVelAssemZ.Value;
            ModelManager.VelBarrelX.Vel=(double)nudVelBarrelX.Value;
            ModelManager.VelBarrelY.Vel=(double)nudVelBarrelY.Value;
            ModelManager.VelC.Vel=(double)nudVelC.Value;

            ModelManager.VelGetX.ACCAndDec = (double)nudAccGetX.Value;
            ModelManager.VelGetY.ACCAndDec = (double)nudAccGetY.Value;
            ModelManager.VelAssemX.ACCAndDec = (double)nudAccAssemX.Value;
            ModelManager.VelZ.ACCAndDec = (double)nudAccAssemZ.Value;
            ModelManager.VelBarrelX.ACCAndDec = (double)nudAccBarrelX.Value;
            ModelManager.VelBarrelY.ACCAndDec = (double)nudAccBarrelY.Value;
            ModelManager.VelC.ACCAndDec = (double)nudAccC.Value;


        }


        private void nudVelC_ValueChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            SaveUI();
        }

        private void cbUse1_Click(object sender, EventArgs e)
        {
            ModelManager.SuctionLParam.BUse = cbUse1.Checked;
        }

        private void cbUse2_Click(object sender, EventArgs e)
        {
            ModelManager.SuctionRParam.BUse = cbUse2.Checked;
        }

        private void 轴P_Click(object sender, EventArgs e)
        {

        }

        private void 轴P_MouseDown(object sender, MouseEventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = 100;

                mc.VelMove(axis, vel);
            }
            else
            {
                vel = 100;

                mc.VelMove(axis, -vel);
            }
        }

        private void 轴P_MouseUp(object sender, MouseEventArgs e)
        {
            UISymbolButton btn = (UISymbolButton)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.StopAxis(axis);
        }

        private void btnGet1_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            nudPos1.Value = (decimal)mc.dic_Axis[axis].dPos;
        }

        private void btnGo1_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.AbsMove(axis, (double)nudPos1.Value, 100);
        }

        private void btnGet2_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            nudPos2.Value = (decimal)mc.dic_Axis[axis].dPos;
        }

        private void btnGo2_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.AbsMove(axis, (double)nudPos2.Value, 100);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            TestAxisModule.axis = axis;
            TestAxisModule.lstPos.Clear();
            TestAxisModule.lstPos.Add((double)nudPos1.Value);
            TestAxisModule.lstPos.Add((double)nudPos2.Value);
            TestAxisModule.stopTime = (long)((double)nudStopTime.Value * 1000);
            if ((axis == AXIS.取料X1轴) || (axis == AXIS.取料X2轴))
            {
                TestAxisModule.dVel = ModelManager.VelGetX.Vel;
                TestAxisModule.dAcc = ModelManager.VelGetX.ACCAndDec;
            }
            else if ((axis == AXIS.取料Y1轴) || (axis == AXIS.取料Y2轴))
            {
                TestAxisModule.dVel = ModelManager.VelGetY.Vel;
                TestAxisModule.dAcc = ModelManager.VelGetY.ACCAndDec;

            }
            else if ((axis == AXIS.组装Z1轴) || (axis == AXIS.组装Z2轴))
            {
                TestAxisModule.dVel = ModelManager.VelZ.Vel;
                TestAxisModule.dAcc = ModelManager.VelZ.ACCAndDec;

            }
            else if ((axis == AXIS.组装X1轴) || (axis == AXIS.组装X2轴))
            {
                TestAxisModule.dVel = ModelManager.VelAssemX.Vel;
                TestAxisModule.dAcc = ModelManager.VelAssemX.ACCAndDec;
            }
            else if ((axis == AXIS.C1轴) || (axis == AXIS.C2轴))
            {
                TestAxisModule.dVel = ModelManager.VelC.Vel;
                TestAxisModule.dAcc = ModelManager.VelC.ACCAndDec;
            }
            else if ((axis == AXIS.镜筒X轴))
            {
                TestAxisModule.dVel = ModelManager.VelBarrelX.Vel;
                TestAxisModule.dAcc = ModelManager.VelBarrelX.ACCAndDec;
            }
            else if ((axis == AXIS.镜筒Y轴))
            {
                TestAxisModule.dVel = ModelManager.VelBarrelY.Vel;
                TestAxisModule.dAcc = ModelManager.VelBarrelY.ACCAndDec;
            }

            FrmTestDialog frm = new FrmTestDialog(Run.testAxisModule,RunMode.单轴测试,0);
            frm.ShowDialog();
            frm = null;
        }
        private void lblSevon_Click(object sender, EventArgs e)
        {
            string strAxis = cmbAxis.Text;
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            if (mc.dic_Axis[axis].SVON)
            {
                mc.SeverOn(axis, 0);
            }
            else
            {
                mc.SeverOn(axis, 1);
            }
        }

        private void cmbAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudPos1.Value = 0;
            nudPos2.Value = 0;
        }
    }
}
