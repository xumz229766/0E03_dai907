using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
namespace CameraSet0
{
    public partial class Camera : UserControl
    {
        CameraManager manager = null;
        ICamera cam = null;
        HWindow hwin;
        public Camera()
        {
           // HSystem.SetSystem("filename_encoding", "utf8");
            InitializeComponent();
        }
        public void SetControlValue(ICamera _cam)
        {
            cam = _cam;

            if (cam != null)
            {
                panel1.Enabled = true;
                groupBox1.Enabled = true;
                cam.GetMinMaxExposure(ref cam.dMinExposure, ref cam.dMaxExposure);
                cam.GetMinMaxGain(ref cam.dMinGain, ref cam.dMaxGain);
                nudExp.Maximum = (decimal)cam.dMaxExposure;
                nudExp.Minimum = (decimal)cam.dMinExposure;
                nudExp.Value = (decimal)cam.dExposure;
                tBarExp.Maximum = (int)cam.dMaxExposure;
                tBarExp.Minimum = (int)cam.dMinExposure;
                tBarExp.Value = (int)cam.dExposure;

                nudGain.Maximum = (decimal)cam.dMaxGain;
                nudGain.Minimum = (decimal)cam.dMinGain;
                nudGain.Value = (decimal)cam.dGain;
                tBarGain.Maximum = (int)cam.dMaxGain;
                tBarGain.Minimum = (int)cam.dMinGain;
                tBarGain.Value = (int)cam.dGain;

                cbTrigger.Checked = cam.bTrigger;
                btnTrigger.Enabled = cam.bTrigger;
            }
            else
            {
                panel1.Enabled = false;
                groupBox1.Enabled = false;
                lblCamName.Text = "";
            }

        }
        public void ListCameras()
        {
            lstCamera.Items.Clear();

            foreach (CameraInfo cInfo in ICamera.lstCamera)
            {
                lstCamera.Items.Add(cInfo.user_name + ":" + cInfo.ip_address);
            }
        }
      

        private void tBarGain_Scroll(object sender, EventArgs e)
        {
           
            if (tBarGain.Focused)
            {
                cam.dGain = tBarGain.Value;
                nudGain.Value =(decimal) cam.dGain;
                cam.SetGain(cam.dGain);
            }

        }

        private void Camera_Load(object sender, EventArgs e)
        {
            //ICamera.getCameras();
            hwin = hWindowControl1.HalconWindow;
            manager = CameraManager.getCameraManager(CameraManager.strCameraFile);
            
           // ListCameras();
            
        }

        private void nudGain_ValueChanged(object sender, EventArgs e)
        {
           
            if (nudGain.Focused)
            {
                cam.dGain = (double)nudGain.Value;
                tBarGain.Value = (int)cam.dGain;
                cam.SetGain(cam.dGain);
            }
        }

        private void tBarExp_Scroll(object sender, EventArgs e)
        {
            if (tBarExp.Focused)
            {
                cam.dExposure = tBarExp.Value;
                nudExp.Value = (decimal)cam.dExposure;
                cam.SetExposure(cam.dExposure);
            }
        }

        private void nudExp_ValueChanged(object sender, EventArgs e)
        {
            if (nudExp.Focused)
            {
                cam.dExposure = (double)nudExp.Value;
                tBarExp.Value = (int)cam.dExposure;
                cam.SetExposure(cam.dExposure);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lstCamera.Items.Clear();
            lstCamera.Items.AddRange(ICamera.getCameras());
           
            //ListCameras();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstCamera.SelectedIndex <0) return ;
            try
            {
                string name = lstCamera.SelectedItem.ToString();
                if (name.Length > 1)
                {
                    if (cam != null)
                    {
                        cam.RemoveDelegate();
                    }
                    string Device = name.Split(':')[0];
                    lblCamName.Text = Device;
                    CameraName cName = (CameraName)Enum.Parse(typeof(CameraName), Device);
                    cam = manager.getCamera(cName);

                    cam.RemoveDelegate();
                    cam.ProcessImage += ProcessImage;
                    cam.InitCamera(Device);
                    SetControlValue(cam);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("相机名称与指定名称不一致，请设定相机名称！");
            
            }
           

        }
        public void ProcessImage(HObject image)
        {
            if (image != null) {

                HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.SetPart(hwin, 0, 0, height, width);
                HOperatorSet.DispImage(image, hwin);
            }
        
        }

        private void lstCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = lstCamera.SelectedItem.ToString();
                if (name.Length > 1)
                {
                    string Device = name.Split(':')[0];
                    CameraInfo info = ICamera.getCameraInfoByName(Device);
                    lblCameraInfo.Text = "UserName:" + info.user_name + "\r\n\r\n"
                                        + "CameraIP:" + info.ip_address + "\r\n"
                                        + "UniqueName:" + info.unique_name + "\r\n\r\n"
                                        + "Serial:" + info.serial + "\r\n\r\n"
                                        + "ComputerIP:" + info.computerIp + "\r\n\r\n";
                }
            }
            catch (Exception ex) { }
        }

        private void cbTrigger_Click(object sender, EventArgs e)
        {
            if (cam != null)
            {
                cam.SetTriggerMode(cbTrigger.Checked);
                btnTrigger.Enabled = cam.bTrigger;
            }
            else
            {
                cbTrigger.Checked = false;
            }
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {
            if (cam != null)
            {
                cam.SoftTrigger();
            }
        }
    }
}
