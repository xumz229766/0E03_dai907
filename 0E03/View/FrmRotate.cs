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
using Motion0;
using ImageProcess0;
namespace _0E030
{
    public partial class FrmRotate : Form
    {
        private AXIS axisC;
        public static HWindow hwin = null;
        HTuple hv_Row =null ;
        HTuple hv_Col = null;
        MotionCard mc = null;

        public FrmRotate()
        {
            InitializeComponent();
        }
        public FrmRotate(AXIS axis)
        {
            axisC = axis;
            InitializeComponent();
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {
            if (axisC == AXIS.C1轴)
            {
                if (ModelManager.CamDownL != null)
                {
                    ModelManager.CamDownL.SoftTrigger();
                }
            }else
            {
                if (ModelManager.CamDownR != null)
                {
                    ModelManager.CamDownR.SoftTrigger();
                }

            }
        }

        private void btnSetProcess_Click(object sender, EventArgs e)
        {
            HObject hoImage;
            string strName = "";

            if (axisC == AXIS.C1轴)
            {
                strName = CalibrationL.strPicRotateName;
                hoImage = ModelManager.SuctionLParam.hImageDown;

                if (strName.Equals(""))
                {
                    strName = "旋转中心标定";
                    CalibrationL.strPicRotateName = strName;
                }
            }
            else
            {
                strName = CalibrationR.strPicRotateName;
                hoImage = ModelManager.SuctionRParam.hImageDown;

                if (strName.Equals(""))
                {
                    strName = "旋转中心标定";
                    CalibrationR.strPicRotateName = strName;
                }
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
            if (axisC == AXIS.C1轴)
                CheckDownL();
            else
                CheckDownR();
        }
        private void CheckDownL()
        {
            // AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = ModelManager.SuctionLParam.hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            string strProcessName = CalibrationL.strPicRotateName;
            ModelManager.SuctionLParam.InitImageDown(strProcessName);

            if (ModelManager.SuctionLParam.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            ModelManager.SuctionLParam.pfDown.SetRun(true);
            ModelManager.SuctionLParam.pfDown.hwin = hWindowControl1.HalconWindow;
            ModelManager.SuctionLParam.actionDown(hImage);
            ModelManager.SuctionLParam.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionLParam.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionLParam.imgResultDown.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");
          

            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }

        private void CheckDownR()
        {
            // AssembleSuction1 suction = CommonSet.dic_Assemble1[iSuctionNum];
            HObject hImage = ModelManager.SuctionRParam.hImageDown;
            if (hImage == null)
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            string strProcessName = CalibrationR.strPicRotateName;
            ModelManager.SuctionRParam.InitImageDown(strProcessName);

            if (ModelManager.SuctionRParam.pfDown == null)
            {
                MessageBox.Show("检测方法为空，请先设定！");
                return;
            }
            ModelManager.SuctionRParam.pfDown.SetRun(true);
            ModelManager.SuctionRParam.pfDown.hwin = hWindowControl1.HalconWindow;
            ModelManager.SuctionRParam.actionDown(hImage);
            ModelManager.SuctionRParam.pfDown.showObj();
            // HOperatorSet.WriteString(hWindowControl1.HalconWindow, CommonSet.dic_AssemblySuction[iSuctionNum].dCenterDownRow);
            string strDispRow = "Row:" + ModelManager.SuctionRParam.imgResultDown.CenterRow.ToString("0.000");
            string strDispCol = "Col:" + ModelManager.SuctionRParam.imgResultDown.CenterColumn.ToString("0.000");

            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispRow, "window", 0, 0, "black", "true");
            CommonSet.disp_message(hWindowControl1.HalconWindow, strDispCol, "window", 20, 0, "black", "true");
            // CommonSet.disp_message(hWindowControl1.HalconWindow, suction.dR.ToString("0.000"), "window", 40, 0, "black", "true");


            HTuple width, height;
            HOperatorSet.GetImageSize(hImage, out width, out height);
            HOperatorSet.DispCross(hWindowControl1.HalconWindow, height.I / 2, width.I / 2, 3000, 0);
        }
        private void btnSaveImg_Click(object sender, EventArgs e)
        {

            if ((ModelManager.SuctionLParam.hImageDown == null)&&(axisC== AXIS.C1轴))
            {
                MessageBox.Show("请先获取图像！");
                return;
            }
            if ((ModelManager.SuctionRParam.hImageDown == null) && (axisC == AXIS.C2轴))
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
                if(axisC == AXIS.C1轴)
                 HOperatorSet.WriteImage(ModelManager.SuctionLParam.hImageDown, "bmp", 0, fileName);
                else
                    HOperatorSet.WriteImage(ModelManager.SuctionRParam.hImageDown, "bmp", 0, fileName);

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            //if (BarrelSuction.imgResultUp.CenterColumn == 0)
            //{
            //    MessageBox.Show("请先确认图像能检测");
            //    return;
            //}
            //if ((Math.Abs(BarrelSuction.pCamGlue.X - mc.dic_Axis[AXIS.点胶X轴].dPos) > 0.01) || (Math.Abs(BarrelSuction.pCamGlue.Y - mc.dic_Axis[AXIS.点胶Y轴].dPos) > 0.01))
            //{
            //    MessageBox.Show("相机不在点胶拍照位");
            //    return;
            //}
            if(axisC== AXIS.C1轴) {
                RotateTestModule.axisC = AXIS.C1轴;
                RotateTestModule.cam = ModelManager.CamDownL;
            }else
            {
                RotateTestModule.axisC = AXIS.C2轴;
                RotateTestModule.cam = ModelManager.CamDownR;

            }
            CalibrationL.iRotateNum = 0;
            CalibrationL.iRotateStep = (int)numericUpDown1.Value;

            CalibrationL.lstRotateColumn.Clear();
            CalibrationL.lstRotateRow.Clear();
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
                if(axisC == AXIS.C1轴)
                {
                    btnTrigger.Enabled = ModelManager.CamDownL.bTrigger;
                    lblCenter.Text = "R:" + CalibrationL.dRoateCenterRow.ToString("0.000") + "\r\n" + "C:" + CalibrationL.dRoateCenterColumn.ToString("0.000");
                }
                else { 
                    btnTrigger.Enabled = ModelManager.CamDownR.bTrigger;
                    lblCenter.Text = "R:" + CalibrationR.dRoateCenterRow.ToString("0.000") + "\r\n" + "C:" + CalibrationR.dRoateCenterColumn.ToString("0.000");
                }
                string strInfo = " " + "    Row    " + "    Column    " + "\r\n";
                StringBuilder sb = new StringBuilder();
                sb.Append(strInfo);
                int len = CalibrationL.lstRotateRow.Count;
                for (int i = 0; i < len; i++)
                { 
                    sb.Append(i.ToString("")+ CalibrationL.lstRotateRow[i].ToString(" 000.000  ")+ CalibrationL.lstRotateColumn[i].ToString(" 000.000  ")+"\r\n");

                }
                txtData.Text = sb.ToString();
                
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
            if (CalibrationL.lstRotateColumn.Count < 3)
                return;
            ComputeCenter();
        }
        private void ComputeCenter()
        {
           int count = CalibrationL.lstRotateColumn.Count;
           hv_Col = new HTuple();
           hv_Row = new HTuple();
           HOperatorSet.ClearWindow(hWindowControl1.HalconWindow);
           HOperatorSet.SetPart(hWindowControl1.HalconWindow, 1024 - 125, 1224 - 150, 1024 + 125, 1224 + 150);
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
           for (int i = 0; i < count; i++)
           {
               hv_Row.Append(CalibrationL.lstRotateRow[i]);
               hv_Col.Append(CalibrationL.lstRotateColumn[i]);
              
               HOperatorSet.DispCross(hWindowControl1.HalconWindow, CalibrationL.lstRotateRow[i], CalibrationL.lstRotateColumn[i], 4, 0);
           }
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "green");
           HObject ho_Contour,ho_Circle;

           // Local control variables 

           HTuple hv_RowCenter = null;
           HTuple hv_ColumnCenter = null, hv_Radius = null, hv_StartPhi = null;
           HTuple hv_EndPhi = null, hv_PointOrder = null;
           // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_Circle);
           ho_Contour.Dispose();
           HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Row,hv_Col);
           HOperatorSet.FitCircleContourXld(ho_Contour, "geotukey", -1, 0, 0, 3, 2, out hv_RowCenter,
               out hv_ColumnCenter, out hv_Radius, out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
          
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "red");
           HOperatorSet.DispCross(hWindowControl1.HalconWindow, hv_RowCenter.D, hv_ColumnCenter.D, 80, 0);
           HOperatorSet.SetColor(hWindowControl1.HalconWindow, "green");
           HOperatorSet.DispCross(hWindowControl1.HalconWindow, 2048 / 2, 2448 / 2, 3000, 0);
            HOperatorSet.GenCircleContourXld(out ho_Circle, hv_RowCenter, hv_ColumnCenter, hv_Radius, hv_StartPhi, hv_EndPhi, hv_PointOrder, 1);
            HOperatorSet.DispObj(ho_Circle, hwin);
            HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, 2048, 2448);

            string strCenter = "当前旋转中心为\r\n"+"R:"+hv_RowCenter.D.ToString("0.000")+"\r\n"+"C:"+hv_ColumnCenter.D.ToString("0.000");
           if (MessageBox.Show(strCenter, "是否更新旋转中心", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
           {
                if (axisC == AXIS.C1轴)
                {
                    CalibrationL.dRoateCenterColumn = hv_ColumnCenter.D;
                    CalibrationL.dRoateCenterRow = hv_RowCenter.D;
                }
                else {
                    CalibrationR.dRoateCenterColumn = hv_ColumnCenter.D;
                    CalibrationR.dRoateCenterRow = hv_RowCenter.D;
                }
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

       

        private void btnGlueXY_Click(object sender, EventArgs e)
        {
           
        }

        private void btnGlueZ_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(axisC == AXIS.C1轴) { 
                HTuple hmat,outR,outC;
                HOperatorSet.HomMat2dIdentity(out hmat);

                HOperatorSet.HomMat2dRotate(hmat, new HTuple(-(double)numericUpDown2.Value).TupleRad().D, CalibrationL.dRoateCenterRow, CalibrationL.dRoateCenterColumn, out hmat);
            
                HOperatorSet.AffineTransPoint2d(hmat, ModelManager.SuctionLParam.imgResultUp.CenterRow, ModelManager.SuctionLParam.imgResultUp.CenterColumn, out outR, out outC);
                string strCenter = "校验中心为:" + "R:" + outR.D.ToString("0.000") + "\r\n" + "C:" + outC.D.ToString("0.000");
                lblTestResult.Text = strCenter;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mc.AbsMove(axisC, mc.dic_Axis[axisC].dPos+(double)numericUpDown2.Value, (int)30);
        }
    }
}
