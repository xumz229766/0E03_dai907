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
namespace _0E03
{
    public partial class ShowCommon :UserControl
    {
        double velXY = 100;
        double velZ = 50;
        MotionCard mc = null;
        public ShowCommon()
        {
            InitializeComponent();
        }

        private void btnSafeAssemX1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X1轴, ModelManager.SuctionLParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnSafeAssemX2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.组装X2轴, ModelManager.SuctionRParam.DPosCameraDownX, velXY, 0.1, 0.1);
        }

        private void btnSafeAssemZ1_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z1轴, ModelManager.SuctionLParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }

        private void btnSafeAssemZ2_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.组装Z2轴, ModelManager.SuctionRParam.pSafeXYZ.Z, velZ, 0.1, 0.1);
        }

        private void btnSafeGetXY1_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X1轴, ModelManager.SuctionLParam.pSafeXYZ.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y1轴, ModelManager.SuctionLParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnSafeGetXY2_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.取料X2轴, ModelManager.SuctionRParam.pSafeXYZ.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.取料Y2轴, ModelManager.SuctionRParam.pSafeXYZ.Y, velXY, 0.1, 0.1);
        }

        private void btnSafeBarrelXY_Click(object sender, EventArgs e)
        {
            if (mc.dic_Axis[AXIS.组装Z1轴].dPos > ModelManager.SuctionLParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z1轴低于安全位");
                return;
            }
            if (mc.dic_Axis[AXIS.组装Z2轴].dPos > ModelManager.SuctionRParam.pSafeXYZ.Z + 0.01)
            {
                MessageBox.Show("组装Z2轴低于安全位");
                return;
            }
            mc.AbsMove(AXIS.镜筒X轴, ModelManager.BarrelParam.pSafeXY.X, velXY, 0.1, 0.1);
            mc.AbsMove(AXIS.镜筒Y轴, ModelManager.BarrelParam.pSafeXY.Y, velXY, 0.1, 0.1);
        }

       

        private void ShowCommon_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
        } 
    }
}
