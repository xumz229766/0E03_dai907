using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace Motion
{
    public partial class IOControl : UserControl
    {
        private Dictionary<DI, IOStatus> dic_ShowDI = new Dictionary<DI, IOStatus>();
        private Dictionary<DO, IOStatus> dic_ShowDO = new Dictionary<DO, IOStatus>();
        SynchronizationContext synContext = null;
        bool bHand = true;//手动状态，允许手动输出
        MotionCard mc = null;
        int NUM = 16;
        public IOControl( )
        {
           
            InitializeComponent();
            synContext = SynchronizationContext.Current;
        }
        private void initControl()
        {
            dic_ShowDI.Clear();
            flowLayoutPanel1.Controls.Clear();
            //添加输入
            string[] strNamesDI = Enum.GetNames(typeof(DI));
            foreach (string name in strNamesDI)
            {
               
                DI e = (DI)Enum.Parse(typeof(DI),name);
                int value = (int)e-8;
                int mode = value / NUM+1;
                int pos = value % NUM;
                string show = mode.ToString() + "-" + pos.ToString();
                IOStatus input = new IOStatus(show, name, false);
                dic_ShowDI.Add(e, input);
                flowLayoutPanel1.Controls.Add(input);
            }

            dic_ShowDO.Clear();
            flowLayoutPanel2.Controls.Clear();
            //添加输出
            string[] strNamesDO = Enum.GetNames(typeof(DO));
            foreach (string name in strNamesDO)
            {
               
                DO e = (DO)Enum.Parse(typeof(DO), name);
                int value = (int)e-8;
                int mode = value / NUM  +1;
                int pos = value % NUM;
                string show = mode.ToString() + "-" + pos.ToString();
                IOStatus output = new IOStatus(show,name, false);
                output.clickProcess += output_Click;
                dic_ShowDO.Add(e, output);
                flowLayoutPanel2.Controls.Add(output);
            }
            
        }
        public void setStatus(bool _value)
        {
            bHand = _value;
        }
        private void output_Click(object sender, EventArgs e)
        {
            if (bHand)
            {
                try
                {
                    IOStatus outObj = (IOStatus)sender;
                    string name = outObj.getName();
      
                    DO de = (DO)Enum.Parse(typeof(DO), name);
                    mc.setDO(de, !outObj.getValue());
                }
                catch (Exception ex) { }
            }
        }
        private void IOControl_Load(object sender, EventArgs e)
        {
            mc = MotionCard.getMotionCard();
            initControl();
            //timer1.Start();
        }
        public void updateIOStatus()
        {
            try
            {
                if (mc != null)
                {
                    foreach (KeyValuePair<DI, IOStatus> pair in dic_ShowDI)
                    {
                        pair.Value.setValue(mc.dic_DI[pair.Key]);
                    }
                    foreach (KeyValuePair<DO, IOStatus> pair in dic_ShowDO)
                    {
                        pair.Value.setValue(mc.dic_DO[pair.Key]);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }


    }
}
