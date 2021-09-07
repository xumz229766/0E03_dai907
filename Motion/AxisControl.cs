using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Motion0
{
    public partial class AxisControl : UserControl
    {
        //添加AxisStatus控件时，Name属性需要与枚举Axis中的名称一致，否则会更新状态异常

        List<AxisStatus> list = new List<AxisStatus>();
        public AxisControl()
        {
            InitializeComponent();
        }
        
        private void addList()
        {
            list.Clear();
            list.Add(镜筒Y轴);
            list.Add(取料Y1轴);
            list.Add(取料Y2轴);
            list.Add(组装X2轴);
            list.Add(组装X1轴);
            list.Add(取料X2轴);
            list.Add(镜筒X轴);
            list.Add(取料X1轴);
            list.Add(组装Z2轴);
            list.Add(组装Z1轴);
            list.Add(C1轴);
            list.Add(C2轴);
           

        }
        private void AxisControl_Load(object sender, EventArgs e)
        {
            addList();
        }
        /// <summary>
        /// 更新所有轴的状态
        /// </summary>
        public void updateAxis(Dictionary<AXIS, AXStatus> dic)
        {
            foreach (AxisStatus axis in list)
            {
                try {
                    AXIS e = (AXIS)Enum.Parse(typeof(AXIS), axis.Name.Trim());
                    axis.setControlValue(dic[e]);

                }
                catch (Exception ex) { }
            }
        }

    }
}
