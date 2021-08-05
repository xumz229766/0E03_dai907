using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;

namespace Assembly
{
    public partial class BtnControls : UserControl
    {
        MotionCard mc = null;
        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        public BtnControls()
        {
            InitializeComponent();
        }

        private void BtnControls_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();

        }
        public void UpdateBtnControlUI()
        {
            try
            {
                lblOpen1.BackColor = getColor(!mc.dic_DO[DO.中转1夹紧]);
                lblClose1.BackColor = getColor(mc.dic_DO[DO.中转1夹紧]);
                lblOpen2.BackColor = getColor(!mc.dic_DO[DO.中转2夹紧]);
                lblClose2.BackColor = getColor(mc.dic_DO[DO.中转2夹紧]);
            }
            catch (Exception)
            {
            }
        }
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Gray;
        }

        private void btnSafeZ1_Click(object sender, EventArgs e)
        {
            double z = OptSution1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.取料Z1轴, z, iVelRunZ);
        }

        private void btnSafeZ2_Click(object sender, EventArgs e)
        {
            double z = OptSution2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.取料Z2轴, z, iVelRunZ);
        }

        private void btnAssembZ1_Click(object sender, EventArgs e)
        {
            double z = Assembly.AssembleSuction1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z1轴, z, iVelRunZ);
        }

        private void btnAssembZ2_Click(object sender, EventArgs e)
        {
            double z = Assembly.AssembleSuction2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z2轴, z, iVelRunZ);
        }

        private void btnSafeGlueZ_Click(object sender, EventArgs e)
        {
            double z = BarrelSuction.pSafe.Z;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void btnOptSafeX1_Click(object sender, EventArgs e)
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

        private void btnOptSafeX2_Click(object sender, EventArgs e)
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

        private void btnAssemSafeX1_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.组装Z1轴].dPos > (AssembleSuction1.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z1轴低于安全位！");
                return;
            }

            double x = AssembleSuction1.pSafeXYZ.X;
          //  double y = AssembleSuction1.pSafeXYZ.Y;
            mc.AbsMove(AXIS.组装X1轴, x, iVelRunX);
           
        }

        private void btnAssemSafeX2_Click(object sender, EventArgs e)
        {

            if ((mc.dic_Axis[AXIS.组装Z2轴].dPos > (AssembleSuction2.pSafeXYZ.Z + 0.01)))
            {
                MessageBox.Show("组装Z2轴低于安全位！");
                return;
            }

            double x = AssembleSuction2.pSafeXYZ.X;
            //double y = AssembleSuction2.pSafeXYZ.Y;
            mc.AbsMove(AXIS.组装X2轴, x, iVelRunX);
        }

        private void lblOpen1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转1夹紧, false);
        }

        private void lblClose1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转1夹紧, true);
        }

        private void lblOpen2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转2夹紧, false);
        }

        private void lblClose2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转2夹紧, true);
        }
    }
}
