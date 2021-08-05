using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using Motion;
using ImageProcess;
namespace Assembly
{
    public partial class FrmRotate : Form
    {
        private int iSuctionNum = 1;
        public HWindow hwin = null;
        HTuple hv_Row =null ;
        HTuple hv_Col = null;
        MotionCard mc = null;
        public FrmRotate()
        {
            InitializeComponent();
        }
        public FrmRotate(int _iNum)
        {
            iSuctionNum = _iNum;
            InitializeComponent();
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {

        }

        private void btnSetProcess_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            strName = BarrelSuction.strPicRotateName;
            hoImage = BarrelSuction.hImageUP;

            if (strName.Equals(""))
            {
                strName = "旋转中心标定";
                BarrelSuction.strPicRotateName = strName;
            }
            FrmProcess frmProcess = new FrmProcess(strName, hoImage);
            frmProcess.ShowDialog();
            frmProcess = null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //cmbProcessNameDown.Items.Clear();
            //cmbProcessNameDown.Items.AddRange(CommonSet.imageManager.GetFactoryNames());
            //cmbProcessNameDown.Text = BarrelSuction.strPicUpCalibName;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            CheckUp();
        }
        private void CheckUp()
        {
            // AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = BarrelSuction.hImageUP;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            string strProcessName = BarrelSuction.strPicRotateName;
            BarrelSuction.InitImageUp(strProcessName);

            if (BarrelSuction.pfUp == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            BarrelSuction.pfUp.SetRun(true);
            BarrelSuction.pfUp.hwin = hWindowControl1.HalconWindow;
            BarrelSuction.actionUp(hImage);
            BarrelSuction.pfUp.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + BarrelSuction.imgResultUp.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + BarrelSuction.imgResultUp.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
          

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            if (BarrelSuction.hImageUP == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Len" + DateTime.Now.ToString("yyMMddHHmmss");
            sfd.Filter = "bmp图片(*.bmp)|*.bmp";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                HOperatorSet.WriteImage(BarrelSuction.hImageUP, "bmp", 0, fileName);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (BarrelSuction.imgResultUp.CenterColumn == 0)
            {
                MessageBox.Show("请先确认图像能检测");
                return;
            }
            if ((Math.Abs(BarrelSuction.pCamGlue.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.01) || (Math.Abs(BarrelSuction.pCamGlue.Y - mc.dic_Axis[AXIS.点胶Y轴].dPos) > 0.01))
            {
                MessageBox.Show("相机不在点胶拍照位");
                return;
            }

            BarrelSuction.iRotateNum = 0;
            BarrelSuction.iRotateStep = (int)numericUpDown1.Value;
           
            BarrelSuction.lstRotateColumn.Clear();
            BarrelSuction.lstRotateRow.Clear();
            Run.runMode = RunMode.旋转;
        }

        private void FrmRotate_Load(object sender, EventArgs e)
        {
            hwin = hWindowControl1.HalconWindow;
            mc = MotionCard.getMotionCard();
        }
        public void UpdateUI()
        {
            try
            {
                btnTrigger.Enabled = CommonSet.camUpD1.bTrigger;
                string strInfo = " " + "    Row    " + "    Column    " + "\r\n";
                StringBuilder sb = new StringBuilder();
                sb.Append(strInfo);
                int len = BarrelSuction.lstRotateRow.Count;
                for (int i = 0; i < len; i++)
                { 
                    sb.Append(i.ToString("")+BarrelSuction.lstRotateRow[i].ToString(" 000.000  ")+BarrelSuction.lstRotateColumn[i].ToString(" 000.000  ")+"\r\n");

                }
                txtData.Text = sb.ToString();
                lblCenter.Text = "R:"+BarrelSuction.dRoateCenterRow.ToString("0.000")+"\r\n"+"C:"+BarrelSuction.dRoateCenterColumn.ToString("0.000");
            }
            catch (Exception)
            {

            }
        
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Run.runMode = RunMode.手动;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            if (BarrelSuction.lstRotateColumn.Count < 3)
                return;
            ComputeCenter();
        }
        private void ComputeCenter()
        {
           int count = BarrelSuction.lstRotateColumn.Count;
           hv_Col = new HTuple();
           hv_Row = new HTuple();
           HOperatorSet.ClearWindow(hWindowControl1.HalconWindow);
           HOperatorSet.SetPart(hWindowControl1.HalconWindow, 1024 - 125, 1224 - 150, 1024 + 125, 1224 + 150);
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "blue");
           for (int i = 0; i < count; i++)
           {
               hv_Row.Append(BarrelSuction.lstRotateRow[i]);
               hv_Col.Append(BarrelSuction.lstRotateColumn[i]);
              
               HOperatorSet.DispCross(hWindowControl1.HalconWindow, BarrelSuction.lstRotateRow[i], BarrelSuction.lstRotateColumn[i], 4, 0);
           }
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "green");
           HObject ho_Contour;

           // Local control variables 

           HTuple hv_RowCenter = null;
           HTuple hv_ColumnCenter = null, hv_Radius = null, hv_StartPhi = null;
           HTuple hv_EndPhi = null, hv_PointOrder = null;
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Contour);
           ho_Contour.Dispose();
           HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Row,hv_Col);
           HOperatorSet.FitCircleContourXld(ho_Contour, "geotukey", -1, 0, 0, 3, 2, out hv_RowCenter,
               out hv_ColumnCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
          
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
           HOperatorSet.DispCross(hWindowControl1.HalconWindow, hv_RowCenter.D, hv_ColumnCenter.D, 80, 0);
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "green");
           HOperatorSet.DispCross(hWindowControl1.HalconWindow, 2048 / 2, 2448 / 2, 3000, 0);

           string strCenter = "当前旋转中心为\r\n"+"R:"+hv_RowCenter.D.ToString("0.000")+"\r\n"+"C:"+hv_ColumnCenter.D.ToString("0.000");
           if (MessageBox.Show(strCenter, "是否更新旋转中心", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
           {
               BarrelSuction.dRoateCenterColumn = hv_ColumnCenter.D;
               BarrelSuction.dRoateCenterRow = hv_RowCenter.D;
           }
        }

        private void cmbProcessNameDown_SelectedIndexChanged(object sender, EventArgs e)
        {
           /// BarrelSuction.strRotateProcessName = cmbProcessNameDown.Text;
        }

        private void FrmRotate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Run.runMode = RunMode.手动;
        }

        private void btnTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                if (CommonSet.camUpD1 != null)
                {
                    CommonSet.camUpD1.SetExposure(BarrelSuction.dExposureTimeUp);
                    CommonSet.camUpD1.SetGain(BarrelSuction.dGainUp);
                    mc.setDO(DO.点胶上相机, true);
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnTrigger_MouseUp(object sender, MouseEventArgs e)
        {
            mc.setDO(DO.点胶上相机, false);
        }

        private void btnGlueXY_Click(object sender, EventArgs e)
        {
            if ((mc.dic_Axis[AXIS.点胶Z轴].dPos > (BarrelSuction.pSafe.Z + 0.01)))
            {
                MessageBox.Show("点胶Z轴低于安全位！");
                return;
            }
            double x = (BarrelSuction.pCamGlue.X);
            double y = (BarrelSuction.pCamGlue.Y);
            mc.AbsMove(AXIS.点胶X轴, x, 100);
            mc.AbsMove(AXIS.点胶Y轴, y, 20);
        }

        private void btnGlueZ_Click(object sender, EventArgs e)
        {
            double z = (BarrelSuction.pCamGlue.Z);
            mc.AbsMove(AXIS.点胶Z轴, z, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HTuple hmat,outR,outC;
            HOperatorSet.HomMat2dIdentity(out hmat);

            HOperatorSet.HomMat2dRotate(hmat, new HTuple(-(double)numericUpDown2.Value).TupleRad().D, BarrelSuction.dRoateCenterRow, BarrelSuction.dRoateCenterColumn, out hmat);
            
            HOperatorSet.AffineTransPoint2d(hmat, BarrelSuction.imgResultUp.CenterRow, BarrelSuction.imgResultUp.CenterColumn, out outR, out outC);
            string strCenter = "校验中心为:" + "R:" + outR.D.ToString("0.000") + "\r\n" + "C:" + outC.D.ToString("0.000");
            lblTestResult.Text = strCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mc.AbsMove(AXIS.点胶C轴, mc.dic_Axis[AXIS.点胶C轴].dPos+(double)numericUpDown2.Value, (int)30);
        }
    }
}
