using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Threading;
namespace CameraSet0
{
    public partial class FrmCameraAndLight : Form
    {
       
       // ICamera camera = new Basler();
        HWindow hwin;
        SynchronizationContext context = null;
        CameraManager cManager = null;
        LightManager lManager = null;
        public FrmCameraAndLight()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
           // ICamera.getCameras();//调用manager之前引用
            cManager = CameraManager.getCameraManager(CameraManager.strCameraFile);
            //lManager = LightManager.getLightManager();
            //lControl1.setLightControl(lManager.getControl(0));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CameraManager.CloseCamera();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            cManager.saveCameraParams(CameraManager.strCameraFile);
            //lManager.SetLightControl(0,lControl1.getLightControl());
            //if (lManager.saveLightParam())
            //{
            //    MessageBox.Show("保存参数成功!");
            //}
            //else {
            //    MessageBox.Show("保存参数失败!");
            //}
        }

        private void camera1_Load(object sender, EventArgs e)
        {

        }
       
       
    }
}
