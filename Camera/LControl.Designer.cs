namespace CameraSet
{
    partial class LControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOpenUp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudChannelUp = new System.Windows.Forms.NumericUpDown();
            this.nudChannelDown = new System.Windows.Forms.NumericUpDown();
            this.btnOpenDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.cmbPort.Location = new System.Drawing.Point(51, 7);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(66, 20);
            this.cmbPort.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "端口:";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(136, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(67, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOpenUp
            // 
            this.btnOpenUp.Location = new System.Drawing.Point(287, 6);
            this.btnOpenUp.Name = "btnOpenUp";
            this.btnOpenUp.Size = new System.Drawing.Size(79, 23);
            this.btnOpenUp.TabIndex = 3;
            this.btnOpenUp.Text = "打开上光源";
            this.btnOpenUp.UseVisualStyleBackColor = true;
            this.btnOpenUp.Click += new System.EventHandler(this.btnOpenUp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "上光源：";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(113, 23);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(423, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Location = new System.Drawing.Point(543, 38);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(11, 12);
            this.lblValue1.TabIndex = 6;
            this.lblValue1.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "下光源：";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(115, 70);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(423, 45);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(544, 85);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(11, 12);
            this.lblValue2.TabIndex = 6;
            this.lblValue2.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudChannelDown);
            this.panel1.Controls.Add(this.nudChannelUp);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblValue2);
            this.panel1.Controls.Add(this.lblValue1);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(4, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 121);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(529, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "亮度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "通道";
            // 
            // nudChannelUp
            // 
            this.nudChannelUp.Location = new System.Drawing.Point(63, 28);
            this.nudChannelUp.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudChannelUp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelUp.Name = "nudChannelUp";
            this.nudChannelUp.Size = new System.Drawing.Size(30, 21);
            this.nudChannelUp.TabIndex = 9;
            this.nudChannelUp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelUp.ValueChanged += new System.EventHandler(this.nudChannelUp_ValueChanged);
            // 
            // nudChannelDown
            // 
            this.nudChannelDown.Location = new System.Drawing.Point(63, 76);
            this.nudChannelDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudChannelDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelDown.Name = "nudChannelDown";
            this.nudChannelDown.Size = new System.Drawing.Size(30, 21);
            this.nudChannelDown.TabIndex = 9;
            this.nudChannelDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChannelDown.ValueChanged += new System.EventHandler(this.nudChannelDown_ValueChanged);
            // 
            // btnOpenDown
            // 
            this.btnOpenDown.Location = new System.Drawing.Point(406, 5);
            this.btnOpenDown.Name = "btnOpenDown";
            this.btnOpenDown.Size = new System.Drawing.Size(79, 23);
            this.btnOpenDown.TabIndex = 3;
            this.btnOpenDown.Text = "打开下光源";
            this.btnOpenDown.UseVisualStyleBackColor = true;
            this.btnOpenDown.Click += new System.EventHandler(this.btnOpenDown_Click);
            // 
            // LControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOpenDown);
            this.Controls.Add(this.btnOpenUp);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPort);
            this.Name = "LControl";
            this.Size = new System.Drawing.Size(594, 158);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannelDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnOpenUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudChannelDown;
        private System.Windows.Forms.NumericUpDown nudChannelUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenDown;
    }
}
