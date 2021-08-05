namespace CameraSet
{
    partial class Camera
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudExp = new System.Windows.Forms.NumericUpDown();
            this.nudGain = new System.Windows.Forms.NumericUpDown();
            this.tBarExp = new System.Windows.Forms.TrackBar();
            this.tBarGain = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.cbTrigger = new System.Windows.Forms.CheckBox();
            this.lblCamName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCameraInfo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lstCamera = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarGain)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.hWindowControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.68807F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.31193F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(708, 517);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(150, 30);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(558, 368);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(558, 368);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudExp);
            this.groupBox1.Controls.Add(this.nudGain);
            this.groupBox1.Controls.Add(this.tBarExp);
            this.groupBox1.Controls.Add(this.tBarGain);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(150, 398);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(558, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "增益和曝光";
            // 
            // nudExp
            // 
            this.nudExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudExp.Location = new System.Drawing.Point(431, 71);
            this.nudExp.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudExp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudExp.Name = "nudExp";
            this.nudExp.Size = new System.Drawing.Size(75, 21);
            this.nudExp.TabIndex = 2;
            this.nudExp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudExp.ValueChanged += new System.EventHandler(this.nudExp_ValueChanged);
            // 
            // nudGain
            // 
            this.nudGain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGain.Location = new System.Drawing.Point(431, 28);
            this.nudGain.Name = "nudGain";
            this.nudGain.Size = new System.Drawing.Size(75, 21);
            this.nudGain.TabIndex = 2;
            this.nudGain.ValueChanged += new System.EventHandler(this.nudGain_ValueChanged);
            // 
            // tBarExp
            // 
            this.tBarExp.AutoSize = false;
            this.tBarExp.Location = new System.Drawing.Point(53, 64);
            this.tBarExp.Maximum = 1000000;
            this.tBarExp.Minimum = 1;
            this.tBarExp.Name = "tBarExp";
            this.tBarExp.Size = new System.Drawing.Size(335, 40);
            this.tBarExp.SmallChange = 100;
            this.tBarExp.TabIndex = 1;
            this.tBarExp.TickFrequency = 10;
            this.tBarExp.Value = 25;
            this.tBarExp.Scroll += new System.EventHandler(this.tBarExp_Scroll);
            // 
            // tBarGain
            // 
            this.tBarGain.Enabled = false;
            this.tBarGain.Location = new System.Drawing.Point(53, 23);
            this.tBarGain.Maximum = 1000000;
            this.tBarGain.Minimum = 1;
            this.tBarGain.Name = "tBarGain";
            this.tBarGain.Size = new System.Drawing.Size(335, 45);
            this.tBarGain.SmallChange = 100;
            this.tBarGain.TabIndex = 1;
            this.tBarGain.TickFrequency = 10;
            this.tBarGain.Value = 25;
            this.tBarGain.Scroll += new System.EventHandler(this.tBarGain_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "曝光：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "增益：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTrigger);
            this.panel1.Controls.Add(this.cbTrigger);
            this.panel1.Controls.Add(this.lblCamName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 30);
            this.panel1.TabIndex = 2;
            // 
            // btnTrigger
            // 
            this.btnTrigger.Enabled = false;
            this.btnTrigger.Location = new System.Drawing.Point(452, 3);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(75, 23);
            this.btnTrigger.TabIndex = 5;
            this.btnTrigger.Text = "软触发";
            this.btnTrigger.UseVisualStyleBackColor = false;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // cbTrigger
            // 
            this.cbTrigger.AutoSize = true;
            this.cbTrigger.Location = new System.Drawing.Point(385, 7);
            this.cbTrigger.Name = "cbTrigger";
            this.cbTrigger.Size = new System.Drawing.Size(48, 16);
            this.cbTrigger.TabIndex = 4;
            this.cbTrigger.Text = "触发";
            this.cbTrigger.UseVisualStyleBackColor = true;
            this.cbTrigger.Click += new System.EventHandler(this.cbTrigger_Click);
            // 
            // lblCamName
            // 
            this.lblCamName.AutoSize = true;
            this.lblCamName.Location = new System.Drawing.Point(74, 9);
            this.lblCamName.Name = "lblCamName";
            this.lblCamName.Size = new System.Drawing.Size(29, 12);
            this.lblCamName.TabIndex = 1;
            this.lblCamName.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前相机：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCameraInfo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.lstCamera);
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox2, 3);
            this.groupBox2.Size = new System.Drawing.Size(144, 511);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机列表";
            // 
            // lblCameraInfo
            // 
            this.lblCameraInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCameraInfo.Location = new System.Drawing.Point(2, 354);
            this.lblCameraInfo.Name = "lblCameraInfo";
            this.lblCameraInfo.Size = new System.Drawing.Size(138, 149);
            this.lblCameraInfo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "相机信息：";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(6, 298);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(46, 37);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lstCamera
            // 
            this.lstCamera.FormattingEnabled = true;
            this.lstCamera.ItemHeight = 12;
            this.lstCamera.Location = new System.Drawing.Point(2, 12);
            this.lstCamera.Name = "lstCamera";
            this.lstCamera.Size = new System.Drawing.Size(138, 268);
            this.lstCamera.TabIndex = 0;
            this.lstCamera.SelectedIndexChanged += new System.EventHandler(this.lstCamera_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(58, 298);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 37);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "设定当前相机";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Camera";
            this.Size = new System.Drawing.Size(708, 517);
            this.Load += new System.EventHandler(this.Camera_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarGain)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudExp;
        private System.Windows.Forms.NumericUpDown nudGain;
        private System.Windows.Forms.TrackBar tBarExp;
        private System.Windows.Forms.TrackBar tBarGain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.CheckBox cbTrigger;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblCamName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstCamera;
        private System.Windows.Forms.Label lblCameraInfo;
        private System.Windows.Forms.Label label4;
    }
}
