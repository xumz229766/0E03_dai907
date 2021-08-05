using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Motion;
using CameraSet;

namespace Assembly
{
   
    public partial class FrmHand : Form
    {
        MotionCard mc = null;
        SynchronizationContext context = null;
        Thread th_UpdateUI = null;
        Dictionary<int, PictureBox> dic_Suction = new Dictionary<int, PictureBox>();//吸笔气缸
        Dictionary<int, Label> dic_Get = new Dictionary<int, Label>();//取料吸真空
        Dictionary<int, Label> dic_Put = new Dictionary<int, Label>();//取料破真空
        Dictionary<int, PictureBox> dic_AssemSuction = new Dictionary<int, PictureBox>();//组装气缸
        Dictionary<int, Label> dic_AssemGet = new Dictionary<int, Label>();//组装吸真空
        Dictionary<int, Label> dic_AssemPut = new Dictionary<int, Label>();//组装破真空
        Dictionary<int, Label> dic_CorrectGet = new Dictionary<int, Label>();//校正吸真空
        Dictionary<int, Label> dic_CorrectPut = new Dictionary<int, Label>();//校正破真空

        private int iVelRunX = 100;
        private int iVelRunY = 100;
        private int iVelRunZ = 20;
        bool bContinue = false;
        private string[] strAxisCSuciton = new string[] { "吸笔1", "吸笔2", "吸笔3", "吸笔4", "吸笔5", "吸笔6", "吸笔7", "吸笔8", "吸笔9", "吸笔10", "吸笔11", "吸笔12", "吸笔13", "吸笔14", "吸笔15", "吸笔16", "吸笔17", "吸笔18" };
        private string[] strAxisName = new string[] { "取料C1轴", "取料C2轴", "取料C3轴", "取料C4轴", "取料C5轴", "取料C6轴", "取料C7轴", "取料C8轴", "取料C9轴", "取料C10轴", "取料C11轴"
        , "取料C12轴", "取料C13轴", "取料C14轴", "取料C15轴", "取料C16轴", "取料C17轴", "取料C18轴"};

        public FrmHand()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

        #region "更新UI"
        private void UpdateUI()
        {
            while (true) {
                try
                {
                   
                    context.Send(UpdateProductAndAssembleUI, null);
                    //context.Post(UpdateGlueUI, null);
                    context.Send(UpdateBarrelUI, null);
                }
                catch (Exception ex)
                { }

                Thread.Sleep(100);
            }

        }

        private void UpdateProductAndAssembleUI(object obj)
        {
            for (int i = 1; i < 19; i++)
            {
                string strName = "取料气缸下降"+i.ToString();
                DO do1 = (DO)Enum.Parse(typeof(DO), strName);
                dic_Suction[i].BackgroundImage = getImage(mc.dic_DO[do1]);


                strName = "取料吸真空" + i.ToString();
                DO do2 = (DO)Enum.Parse(typeof(DO), strName);
                dic_Get[i].BackColor = getColor(mc.dic_DO[do2]);
                strName = null;

                strName = "取料破真空" + i.ToString();
                DO do3 = (DO)Enum.Parse(typeof(DO), strName);
                dic_Put[i].BackColor = getColor(mc.dic_DO[do3]);
                strName = null;

                strName = "组装气缸下降" + i.ToString();
                DO do4 = (DO)Enum.Parse(typeof(DO), strName);
                dic_AssemSuction[i].BackgroundImage = getImage(mc.dic_DO[do4]);
                strName = null;

                strName = "组装吸真空" + i.ToString();
                DO do5 = (DO)Enum.Parse(typeof(DO), strName);
                dic_AssemGet[i].BackColor = getColor(mc.dic_DO[do5]);
                strName = null;

                strName = "组装破真空" + i.ToString();
                DO do6 = (DO)Enum.Parse(typeof(DO), strName);
                dic_AssemPut[i].BackColor = getColor(mc.dic_DO[do6]);
                strName = null;

                strName = "中转吸真空" + i.ToString();
                DO do7 = (DO)Enum.Parse(typeof(DO), strName);
                dic_CorrectGet[i].BackColor = getColor(mc.dic_DO[do7]);
                strName = null;

                strName = "中转破真空" + i.ToString();
                DO do8 = (DO)Enum.Parse(typeof(DO), strName);
                dic_CorrectPut[i].BackColor = getColor(mc.dic_DO[do8]);
                strName = null;

              
            }

            panelProduct.Visible = rbtProductA.Checked;
            panel4.Visible = rbtProductA2.Checked;
            panel7.Visible = rbtProductA3.Checked;

            lblPosGetX1.Text = mc.dic_Axis[AXIS.取料X1轴].dPos.ToString("0.000");
            lblPosGetY1.Text = mc.dic_Axis[AXIS.取料Y1轴].dPos.ToString("0.000");
            lblPosGetZ1.Text = mc.dic_Axis[AXIS.取料Z1轴].dPos.ToString("0.000");
            lblPosGetX2.Text = mc.dic_Axis[AXIS.取料X2轴].dPos.ToString("0.000");
            lblPosGetY2.Text = mc.dic_Axis[AXIS.取料Y2轴].dPos.ToString("0.000");
            lblPosGetZ2.Text = mc.dic_Axis[AXIS.取料Z2轴].dPos.ToString("0.000");

            lblAssemblePosX1.Text = mc.dic_Axis[AXIS.组装X1轴].dPos.ToString("0.000");
            lblAssemblePosY1.Text = mc.dic_Axis[AXIS.组装Y1轴].dPos.ToString("0.000");
            lblAssemblePosZ1.Text = mc.dic_Axis[AXIS.组装Z1轴].dPos.ToString("0.000");
            lblAssemblePosX2.Text = mc.dic_Axis[AXIS.组装X2轴].dPos.ToString("0.000");
            lblAssemblePosY2.Text = mc.dic_Axis[AXIS.组装Y2轴].dPos.ToString("0.000");
            lblAssemblePosZ2.Text = mc.dic_Axis[AXIS.组装Z2轴].dPos.ToString("0.000");

            lblBarrelYPos.Text = mc.dic_Axis[AXIS.镜筒Y轴].dPos.ToString("0.000");
            lblGluePosX.Text = mc.dic_Axis[AXIS.点胶X轴].dPos.ToString("0.000");
            lblGluePosY.Text = mc.dic_Axis[AXIS.点胶Y轴].dPos.ToString("0.000");
            lblGluePosZ.Text = mc.dic_Axis[AXIS.点胶Z轴].dPos.ToString("0.000");
            lblGluePosC.Text = mc.dic_Axis[AXIS.点胶C轴].dPos.ToString("0.000");

            //int channel = LightManager.iLightDownChannel;
            //lblOpenLightDown.BackColor = getColor(CommonSet.lc.bChannelOpen[channel]);
            //if (CommonSet.lc.bChannelOpen[channel])
            //{
            //    lblOpenLightDown.Text = "关闭下光源";
            //}
            //else
            //{
            //    lblOpenLightDown.Text = "打开下光源";
            //}
        }
       
        private void UpdateBarrelUI(object obj)
        {

            lblGlueGet.BackColor = getColor(mc.dic_DO[DO.点胶位吸真空]);
            lblGluePut.BackColor = getColor(mc.dic_DO[DO.点胶位破真空]);

            lblGlueOpen.BackColor = getColor(!mc.dic_DO[DO.点胶夹紧]);
            lblGlueClose.BackColor = getColor(mc.dic_DO[DO.点胶夹紧]);
            lblGlueUp.BackColor = getColor(!mc.dic_DO[DO.点胶气缸下降]);
            lblGlueDown.BackColor = getColor(mc.dic_DO[DO.点胶气缸下降]);

            lblBarrelGet.BackColor = getColor(mc.dic_DO[DO.点胶镜筒吸笔真空]);
            lblBarrelPut.BackColor = getColor(false);
            lblBarrelUp.BackColor = getColor(!mc.dic_DO[DO.点胶镜筒吸笔下降]);
            lblBarrelDown.BackColor = getColor(mc.dic_DO[DO.点胶镜筒吸笔下降]);
        
            lblLenGet.BackColor = getColor(mc.dic_DO[DO.点胶成品吸笔真空_夹子]);
            lblLenPut.BackColor = getColor(mc.dic_DO[DO.点胶成品吸笔破真空]);
            lblLenUp.BackColor = getColor(!mc.dic_DO[DO.点胶成品镜头气缸下降]);
            lblLenDown.BackColor = getColor(mc.dic_DO[DO.点胶成品镜头气缸下降]);
            lblGlue.BackColor = getColor(mc.dic_DO[DO.出胶]);
            lblUV.BackColor = getColor(mc.dic_DO[DO.点胶UV]);

            lblPSuctionClose.BackColor = getColor(mc.dic_DO[DO.点胶成品吸笔真空_夹子]);
            lblPSuctionDown.BackColor = getColor(mc.dic_DO[DO.点胶镜筒吸笔破真空_夹子气缸]); 

            //lblCGet.BackColor = getColor(mc.dic_DO[DO.C轴吸真空]);
            //lblCPut.BackColor = getColor(mc.dic_DO[DO.C轴破真空]);
            //lblCOpen.BackColor = getColor(mc.dic_DO[DO.C轴夹子打开]);
            //lblCClose.BackColor = getColor(!mc.dic_DO[DO.C轴夹子打开]);

            lblOpen1.BackColor = getColor(!mc.dic_DO[DO.中转1夹紧]);
            lblClose1.BackColor = getColor(mc.dic_DO[DO.中转1夹紧]);
            lblOpen2.BackColor = getColor(!mc.dic_DO[DO.中转2夹紧]);
            lblClose2.BackColor = getColor(mc.dic_DO[DO.中转2夹紧]);

            lblAssembGet1.BackColor = getColor(mc.dic_DO[DO.组装位1真空吸]);
            lblAssembPut1.BackColor = getColor(mc.dic_DO[DO.组装位1真空破]);
            lblAssembleOpen1.BackColor = getColor(!mc.dic_DO[DO.组装位1夹紧]);
            lblAssembleClose1.BackColor = getColor(mc.dic_DO[DO.组装位1夹紧]);

            lblAssembGet2.BackColor = getColor(mc.dic_DO[DO.组装位2真空吸]);
            lblAssembPut2.BackColor = getColor(mc.dic_DO[DO.组装位2真空破]);
            lblAssembleOpen2.BackColor = getColor(!mc.dic_DO[DO.组装位2夹紧]);
            lblAssembleClose2.BackColor = getColor(mc.dic_DO[DO.组装位2夹紧]);

            lblMSuction1.BackColor = getColor(mc.dic_DO[DO.中转吸真空气源1]);
            lblMSuction2.BackColor = getColor(mc.dic_DO[DO.中转吸真空气源2]);
            //int channel = LightManager.iLightUpChannel;           
            //lblOpenLightUp.BackColor = getColor(CommonSet.lc.bChannelOpen[channel]);
            //if (CommonSet.lc.bChannelOpen[channel])
            //{
            //    lblOpenLightUp.Text = "关闭上光源";
            //}
            //else
            //{
            //    lblOpenLightUp.Text = "打开上光源";
            //}
        }
        private Image getImage(bool value)
        {
            if (value)
                return Properties.Resources.SuctionButtonDown;
            else
                return Properties.Resources.SuctionButtonUp;

        }
        private Color getColor(bool value)
        {
            if (value)
                return Color.Green;
            else
                return Color.Gray;
        }
        #endregion
        private void initControl()
        {
            Control.ControlCollection sonControls1 = tableLayoutPanel1.Controls;
            string strSuction = "ProductSuction";
            string strGet = "lblProductGet";
            string strPut = "lblProductPut";
            
            foreach (Control control in sonControls1)
            {

                if (control.Name.StartsWith(strSuction))
                {
                    int len = strSuction.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Suction.Add(index, (PictureBox)control);
                }else if(control.Name.StartsWith(strGet))
                {
                    int len = strGet.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Get.Add(index, (Label)control);
                }else if (control.Name.StartsWith(strPut))
                {
                    int len = strPut.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Put.Add(index, (Label)control);
                }

            }
            Control.ControlCollection sonControls5 = tableLayoutPanel5.Controls;
            foreach (Control control in sonControls5)
            {

                if (control.Name.StartsWith(strSuction))
                {
                    int len = strSuction.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Suction.Add(index, (PictureBox)control);
                }
                else if (control.Name.StartsWith(strGet))
                {
                    int len = strGet.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Get.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strPut))
                {
                    int len = strPut.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_Put.Add(index, (Label)control);
                }

            }

            string strSuction2 = "AssembleSuction";
            string strGet2 = "lblAssembleGet";
            string strPut2 = "lblAssemblePut";
            string strCorrectGet = "lblCorrectGet";
            string strCorrectPut = "lblCorrectPut";
            Control.ControlCollection sonControls3 = tableLayoutPanel3.Controls;
            foreach (Control control in sonControls3)
            {

                if (control.Name.StartsWith(strSuction2))
                {
                    int len = strSuction2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemSuction.Add(index, (PictureBox)control);
                }
                else if (control.Name.StartsWith(strGet2))
                {
                    int len = strGet2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemGet.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strPut2))
                {
                    int len = strPut2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemPut.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strCorrectGet))
                {
                    int len = strCorrectGet.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_CorrectGet.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strCorrectPut))
                {
                    int len = strCorrectPut.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_CorrectPut.Add(index, (Label)control);
                }
            }
            Control.ControlCollection sonControls6 = tableLayoutPanel6.Controls;
            foreach (Control control in sonControls6)
            {

                if (control.Name.StartsWith(strSuction2))
                {
                    int len = strSuction2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemSuction.Add(index, (PictureBox)control);
                }
                else if (control.Name.StartsWith(strGet2))
                {
                    int len = strGet2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemGet.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strPut2))
                {
                    int len = strPut2.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_AssemPut.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strCorrectGet))
                {
                    int len = strCorrectGet.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_CorrectGet.Add(index, (Label)control);
                }
                else if (control.Name.StartsWith(strCorrectPut))
                {
                    int len = strCorrectPut.Length;
                    int index = Convert.ToInt16(control.Name.Trim().Substring(len));
                    dic_CorrectPut.Add(index, (Label)control);
                }
            }
        }

        private void FrmAxisMove_Load(object sender, EventArgs e)
        {
            cmbC.Text = "吸笔1";
            mc = MotionCard.getMotionCard();
            initControl();
            if (th_UpdateUI == null)
            {
                th_UpdateUI = new Thread(UpdateUI);
                th_UpdateUI.Start();
            }
          

        }

        private void FrmHand_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        #region"取料界面"
        private void btnX1P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtProductA.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            if (name.Contains("X") || name.Contains("Y"))
            {
                if (mc.dic_Axis[AXIS.取料Z1轴].dPos > (OptSution1.pSafeXYZ.Z + 0.01))
                {
                    MessageBox.Show("取料Z1轴低于安全高度!");
                    return;
                }
                if (mc.dic_Axis[AXIS.取料Z2轴].dPos > (OptSution2.pSafeXYZ.Z + 0.01))
                {
                    MessageBox.Show("取料Z2轴低于安全高度!");
                    return;
                }
            }
            int len = name.Length;
            string direct = name.Substring(len-1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed.Value;
            }
            else
            {
                vel = -tBarProductSpeed.Value;
            }           
                mc.VelMove(axis, vel);
           
           
        }

        private void btnX1P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtProductA.Checked)
                return;
            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;            
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.StopAxis(axis);
           
        }

        private void btnX2P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtProductA2.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            if (name.Contains("X") || name.Contains("Y"))
            {
                //if (mc.dic_Axis[AXIS.Z1轴].dPos > (ProductSuction.pSafe1.Z + 0.01))
                //{
                //    MessageBox.Show("Z1轴低于安全高度!");
                //    return;
                //}
                //if (mc.dic_Axis[AXIS.Z2轴].dPos > (ProductSuction.pSafe2.Z + 0.01))
                //{
                //    MessageBox.Show("Z2轴低于安全高度!");
                //    return;
                //}
            }
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed2.Value;
            }
            else
            {
                vel = -tBarProductSpeed2.Value;
            }
            mc.VelMove(axis, vel);


        }

        private void btnX2P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtProductA2.Checked)
                return;
            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.StopAxis(axis);

        }
        private void ProductSuction1_Click(object sender, EventArgs e)
        {
            string strSuction = "ProductSuction";
            PictureBox pic = (PictureBox)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "取料气缸下降"+index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO),strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
            //if (mc.dic_DO[dOut])
            //{
            //    mc.setDO(dOut, false);
            //    //pic.BackgroundImage = Properties.Resources.SuctionButtonDown;
            //}
            //else
            //{
            //    //pic.BackgroundImage = Properties.Resources.SuctionButtonUp;
            //    mc.setDO(dOut, true);
            //}
           
        }

        private void lblProductGet1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblProductGet";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "取料吸真空" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
            //if (mc.dic_DO[dOut])
            //{
            //    mc.setDO(, false);
            //    //pic.BackgroundImage = Properties.Resources.SuctionButtonDown;
            //}
            //else
            //{
            //    //pic.BackgroundImage = Properties.Resources.SuctionButtonUp;
            //    mc.setDO(DO.吸嘴1吸真空 + index - 1, true);
            //}
        }

        private void lblProductPut1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblProductPut";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "取料破真空" + index;
           // DO doPut = (DO)Enum.Parse(typeof(DO), strName);
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            if (CommonSet.bUseAutoParam)
            {
                CommonSet.dOutTest = dOut;
                if(index <10)
                    CommonSet.dOutTestTime = (long)(OptSution1.dPutTime * 1000);
                else
                    CommonSet.dOutTestTime = (long)(OptSution2.dPutTime * 1000);

                string strD = "取料吸真空" + index.ToString();
                DO dO1 = (DO)Enum.Parse(typeof(DO), strD);
                CommonSet.dOutIn = dO1;
            }
            else
            {
                mc.setDO(dOut, !mc.dic_DO[dOut]);
            }
            
        }

        private void X1轴P_Click(object sender, EventArgs e)
        {
            if (rbtProductC.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed.Value;
                double newPos = ((posNow + (double)nudProductSize.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                vel = tBarProductSpeed.Value;
                double newPos = ((posNow - (double)nudProductSize.Value));
                mc.AbsMove(axis, newPos, vel);
            }
          
          
           

        }

        private void X2轴P_Click(object sender, EventArgs e)
        {
            if (rbtProductC2.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed2.Value;
                double newPos = ((posNow + (double)nudProductSize2.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                vel = tBarProductSpeed2.Value;
                double newPos = ((posNow - (double)nudProductSize2.Value));
                mc.AbsMove(axis, newPos, vel);
            }




        }
        private void btnSuctionUp_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {               
                    //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "取料气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnSuctionDown_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i - 1, true);

                string strDo = "取料气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnSuctionGetAll_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {

                string strDo = "取料吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
                    

            }
        }

        private void btnSuctionGetCancel_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                string strDo = "取料吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
                //mc.setDO(DO.吸嘴1吸真空 + i-1, false);
                
            }
        }

        private void btnSuctionPutAll_Click(object sender, EventArgs e)
        {
         
            for (int i = 1; i < 10; i++)
            {
                string strDo = "取料破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);

            }
        }

        private void btnSuctionPutCancel_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                string strDo = "取料破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);

            }
        }

        private void btnProductSuctionUp2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "取料气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnProductSuctionDown2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "取料气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnProductSuctionGetAll2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "取料吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);


            }
        }

        private void btnProductSuctionGetCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "取料吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);


            }
        }

        private void btnProSuctionPutAll2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "取料破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);


            }
        }

        private void btnProSuctionPutCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                string strDo = "取料破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }


     
        #endregion
        #region "组装界面"

        private void btnSuctionUp2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnSuctionDown2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnSuctionGetAll2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnSuctionGetCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnSuctionPutAll2_Click(object sender, EventArgs e)
        {

            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnSuctionPutCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }
        private void AssembleSuction1_Click(object sender, EventArgs e)
        {
            string strSuction = "AssembleSuction";
            PictureBox pic = (PictureBox)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));


            string strDo = "组装气缸下降" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);

        }
       
        private void lblAssembleGet1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblAssembleGet";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));

            string strDo = "组装吸真空" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblAssemblePut1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblAssemblePut";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "组装破真空" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            if (CommonSet.bUseAutoParam)
            {
                CommonSet.dOutTest= dOut;
                CommonSet.dOutTestTime = (long)(CommonSet.asm.dic_Solution[1].lstTime[index-1]*1000);
                string strD = "组装吸真空" + index.ToString();
                DO dO1 = (DO)Enum.Parse(typeof(DO), strD);
                CommonSet.dOutIn = dO1;
            }
            else
            {
                mc.setDO(dOut, !mc.dic_DO[dOut]);
            }
        }


        private void lblCorrectGet1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblCorrectGet";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "中转吸真空" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }

        private void lblCorrectPut1_Click(object sender, EventArgs e)
        {
            string strSuction = "lblCorrectPut";
            Label pic = (Label)sender;
            int index = Convert.ToInt32(pic.Name.Trim().Substring(strSuction.Length));
            string strDo = "中转破真空" + index.ToString();
            DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
            mc.setDO(dOut, !mc.dic_DO[dOut]);
        }
        private void btnCorrentGet1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
               

                string strDo = "中转吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnCorrentGetClose1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                string strDo = "中转吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnCorrectPut1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {

                string strDo = "中转破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnCorrectPutClose1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {

                string strDo = "中转破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }
        private void btnSuctionUp2_Click_1(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnSuctionDown2_Click_1(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装气缸下降" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnAssemSuctionGetAll2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnAssemSuctionGetCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnAssemSuctionPutAll2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnAssemSuctionPutCancel2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {
                //mc.setDO(DO.吸嘴1下降 + i-1, false);

                string strDo = "组装破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnCorrentGet2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "中转吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnCorrentGetClose2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "中转吸真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        private void btnCorrectPut2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "中转破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, true);
            }
        }

        private void btnCorrectPutClose2_Click(object sender, EventArgs e)
        {
            for (int i = 10; i < 19; i++)
            {

                string strDo = "中转破真空" + i.ToString();
                DO dOut = (DO)Enum.Parse(typeof(DO), strDo);
                mc.setDO(dOut, false);
            }
        }

        #endregion

        
        #region"组装位真空"
        private void lblAssemble1Get_Click(object sender, EventArgs e)
        {
            //if (mc.dic_DO[DO.C轴吸真空])
            //{
            //    mc.setDO(DO.C轴吸真空, false);
            //}
            //else
            //{
            //    mc.setDO(DO.C轴吸真空, true);
            //    mc.setDO(DO.C轴破真空, false);
            //}
        }

        private void lblAssemble1Put_Click(object sender, EventArgs e)
        {
            //if (mc.dic_DO[DO.C轴破真空])
            //{
            //    mc.setDO(DO.C轴破真空, false);
            //}
            //else
            //{
            //    mc.setDO(DO.C轴破真空, true);
            //    mc.setDO(DO.C轴吸真空, false);
            //}
        }
        private void lblCOpen_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.C轴夹子打开, true);
        }
        private void lblCClose_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.C轴夹子打开, false);
        }
       
        #endregion
    
   
       
        #region"求心夹子"
        private void lblAssemble1Open_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.求心1闭合, false);
        }

        private void lblAssemble1Close_Click(object sender, EventArgs e)
        {
            //bool flag = false;
            //for (int i = 0; i < 7; i++)
            //{
            //    flag = flag || mc.dic_DO[DO.吸嘴1下降 + i];
            //}
            //if (flag)
            //{
            //    MessageBox.Show("吸笔气缸下降，请先将所有气缸上升！");
            //    return;
            //}
            //mc.setDO(DO.求心1闭合, true);
        }

        private void lblAssemble2Open_Click(object sender, EventArgs e)
        {
            //mc.setDO(DO.求心2闭合, false);
        }

        private void lblAssemble2Close_Click(object sender, EventArgs e)
        {

            //bool flag = false;
            //for (int i = 0; i < 7; i++)
            //{
            //    flag = flag || mc.dic_DO[DO.吸嘴8下降 + i];
            //}
            //if (flag)
            //{
            //    MessageBox.Show("吸笔气缸下降，请先将所有气缸上升！");
            //    return;
            //}
            //mc.setDO(DO.求心2闭合, true);
        }
        #endregion

        #region"镜筒吸笔气缸"
        private void lblBarrelUp1_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒气缸1下降, false);
        }

        private void lblBarrelDown1_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒气缸1下降, true);
        }
        private void lblBarrel1Open_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒夹子1夹紧, false);
        }

        private void lblBarrel1Close_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒夹子1夹紧, true);
        }
        private void lblBarrelUp2_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒气缸2下降, false);
        }

        private void lblBarrelDown2_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒气缸2下降, true);
        }
        private void lblBarrel2Open_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒夹子2夹紧, false);
        }

        private void lblBarrel2Close_Click(object sender, EventArgs e)
        {
           // mc.setDO(DO.镜筒夹子2夹紧, true);
        }
        #endregion
       
       

      

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

        private void btnHomeC1_Click(object sender, EventArgs e)
        {
            //int ang = (int)(CommonSet.dic_BarrelSuction[1].dCPutAngle*MotionCard.dic_AxisRatio[AXIS.组装C21轴]);
           // mc.AbsMove(AXIS.C轴, BarrelSuction.dCPutAngle, 180);
        }

        private void btnHomeC2_Click(object sender, EventArgs e)
        {
            //int ang = (int)(CommonSet.dic_BarrelSuction[2].dCPutAngle * MotionCard.dic_AxisRatio[AXIS.组装C22轴]);
            //mc.AbsMove(AXIS.组装C22轴, ang, (int)AssembleSuction.dVelRunC);
        }

        private void btnSafeZ3_Click(object sender, EventArgs e)
        {
            //int z = (int)(BarrelSuction.pSafe.Z * MotionCard.dic_AxisRatio[AXIS.镜筒Z3轴]);
            //mc.AbsMove(AXIS.镜筒Z3轴, z, iVelRunZ);
        }

        private void btnCLeft_MouseDown(object sender, MouseEventArgs e)
        {
            
            string strSuc = cmbC.Text;
            List<string> lstSuc = new List<string>();
            lstSuc.AddRange(strAxisCSuciton);
            string name = strAxisName[lstSuc.IndexOf(strSuc)];

           // int len = name.Length;

            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), name);
            int vel = 0;
          
            vel = tBarProductSpeed.Value;
            mc.VelMove(axis, vel);
           
        }

        private void btnCLeft_MouseUp(object sender, MouseEventArgs e)
        {
            string strSuc = cmbC.Text;
            List<string> lstSuc = new List<string>();
            lstSuc.AddRange(strAxisCSuciton);
            string name = strAxisName[lstSuc.IndexOf(strSuc)];

        
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), name);
            mc.StopAxis(axis);
        }

        private void btnCRight_MouseDown(object sender, MouseEventArgs e)
        {
            string strSuc = cmbC.Text;
            List<string> lstSuc = new List<string>();
            lstSuc.AddRange(strAxisCSuciton);
            string name = strAxisName[lstSuc.IndexOf(strSuc)];

            // int len = name.Length;

            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), name);
            int vel = 0;

            vel = tBarProductSpeed.Value;
            mc.VelMove(axis, -vel);
        }

        private void btnCRight_MouseUp(object sender, MouseEventArgs e)
        {
            string strSuc = cmbC.Text;
            List<string> lstSuc = new List<string>();
            lstSuc.AddRange(strAxisCSuciton);
            string name = strAxisName[lstSuc.IndexOf(strSuc)];
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), name);
            mc.StopAxis(axis);
        }

        #region"镜筒和点胶"
        private void 镜筒Y轴P_Click(object sender, EventArgs e)
        {
            if (rbtProductC3.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            double posNow = mc.dic_Axis[axis].dPos;
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed3.Value;
                double newPos = ((posNow + (double)nudProductSize3.Value));
                mc.AbsMove(axis, newPos, vel);
            }
            else
            {
                vel = tBarProductSpeed3.Value;
                double newPos = ((posNow - (double)nudProductSize3.Value));
                mc.AbsMove(axis, newPos, vel);
            }


        }

        private void 镜筒Y轴P_MouseDown(object sender, MouseEventArgs e)
        {
            if (rbtProductA3.Checked)
                return;

            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            if (name.Contains("X") || name.Contains("Y"))
            {
                //if (mc.dic_Axis[AXIS.Z1轴].dPos > (ProductSuction.pSafe1.Z + 0.01))
                //{
                //    MessageBox.Show("Z1轴低于安全高度!");
                //    return;
                //}
                //if (mc.dic_Axis[AXIS.Z2轴].dPos > (ProductSuction.pSafe2.Z + 0.01))
                //{
                //    MessageBox.Show("Z2轴低于安全高度!");
                //    return;
                //}
            }
            int len = name.Length;
            string direct = name.Substring(len - 1);
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            int vel = 0;
            if (direct.Equals("P"))
            {
                vel = tBarProductSpeed3.Value;
            }
            else
            {
                vel = -tBarProductSpeed3.Value;
            }
            mc.VelMove(axis, vel);
        }

        private void 镜筒Y轴P_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbtProductA3.Checked)
                return;
            Button btn = (Button)sender;
            string name = btn.Name.Trim();
            int len = name.Length;
            string strAxis = name.Remove(len - 1);
            AXIS axis = (AXIS)Enum.Parse(typeof(AXIS), strAxis);
            mc.StopAxis(axis);
        }

        private void tabGlue_Click(object sender, EventArgs e)
        {

        }

        private void lblGlueGet_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶位吸真空, !mc.dic_DO[DO.点胶位吸真空]);
        }

        private void lblGluePut_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶位破真空, !mc.dic_DO[DO.点胶位破真空]);
        }

        private void lblGlueOpen_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶夹紧, false);
        }

        private void lblGlueClose_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶夹紧, true);
        }

        private void lblGlueUp_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶气缸下降, false);
        }

        private void lblGlueDown_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶气缸下降, true);
        }

        private void lblBarrelGet_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶镜筒吸笔真空, !mc.dic_DO[DO.点胶镜筒吸笔真空]);
        }

        private void lblBarrelPut_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶镜筒吸笔破真空_夹子气缸, !mc.dic_DO[DO.点胶镜筒吸笔破真空_夹子气缸]);
        }

        private void lblBarrelUp_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶镜筒吸笔下降, false);
        }

        private void lblBarrelDown_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶镜筒吸笔下降, true);
        }

        private void lblLenGet_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶成品吸笔真空_夹子, !mc.dic_DO[DO.点胶成品吸笔真空_夹子]);
        }

        private void lblLenPut_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶成品吸笔破真空, !mc.dic_DO[DO.点胶成品吸笔破真空]);
        }

        private void lblLenUp_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶成品镜头气缸下降, false);
        }

        private void lblLenDown_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶成品镜头气缸下降, true);
        }

        private void lblGlue_MouseDown(object sender, MouseEventArgs e)
        {
            mc.setDO(DO.出胶, true);
        }

        private void lblGlue_MouseUp(object sender, MouseEventArgs e)
        {
            mc.setDO(DO.出胶, false);
        }

        private void lblGlue_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.出胶, !mc.dic_DO[DO.出胶]);
        }

        private void lblUV_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.点胶UV, !mc.dic_DO[DO.点胶UV]);
        }
        #endregion

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

        private void lblAssembGet1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位1真空吸,!mc.dic_DO[DO.组装位1真空吸]);
        }

        private void lblAssembPut1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位1真空破, !mc.dic_DO[DO.组装位1真空破]);
        }

        private void lblAssembleOpen1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位1夹紧, false);
        }

        private void lblAssembleClose1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位1夹紧, true);
        }

        private void lblAssembGet2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位2真空吸, !mc.dic_DO[DO.组装位2真空吸]);
        }

        private void lblAssembPut2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位2真空破, !mc.dic_DO[DO.组装位2真空破]);
        }

        private void lblAssembleOpen2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位2夹紧, false);
        }

        private void lblAssembleClose2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.组装位2夹紧, true);
        }

        private void btnAssembZ1_Click(object sender, EventArgs e)
        {
            double z = Assembly.AssembleSuction1.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z1轴, z, iVelRunZ);
        }

        private void btnAssembZ2_Click(object sender, EventArgs e)
        {
            double z =Assembly.AssembleSuction2.pSafeXYZ.Z;
            mc.AbsMove(AXIS.组装Z2轴, z, iVelRunZ);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double z = BarrelSuction.pSafe.Z;
            mc.AbsMove(AXIS.点胶Z轴, z, iVelRunZ);
        }

        private void lblMSuction1_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转吸真空气源1, !mc.dic_DO[DO.中转吸真空气源1]);
        }

        private void lblMSuction2_Click(object sender, EventArgs e)
        {
            mc.setDO(DO.中转吸真空气源2, !mc.dic_DO[DO.中转吸真空气源2]);
        }

        private void cbAuto_Click(object sender, EventArgs e)
        {
            CommonSet.bUseAutoParam = cbAuto.Checked;
        }

















    }
}
