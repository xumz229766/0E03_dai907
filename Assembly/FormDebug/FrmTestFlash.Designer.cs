namespace Assembly
{
    partial class FrmTestFlash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartFlash = new System.Windows.Forms.Button();
            this.btnEndFlash = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.hWindowControl4 = new HalconDotNet.HWindowControl();
            this.hWindowControl5 = new HalconDotNet.HWindowControl();
            this.hWindowControl6 = new HalconDotNet.HWindowControl();
            this.hWindowControl7 = new HalconDotNet.HWindowControl();
            this.hWindowControl8 = new HalconDotNet.HWindowControl();
            this.hWindowControl9 = new HalconDotNet.HWindowControl();
            this.hWindowControl10 = new HalconDotNet.HWindowControl();
            this.hWindowControl11 = new HalconDotNet.HWindowControl();
            this.hWindowControl12 = new HalconDotNet.HWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtn2 = new System.Windows.Forms.RadioButton();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.rbtn4 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtn1 = new System.Windows.Forms.RadioButton();
            this.rbtn3 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbCam = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartFlash
            // 
            this.btnStartFlash.Location = new System.Drawing.Point(29, 3);
            this.btnStartFlash.Name = "btnStartFlash";
            this.btnStartFlash.Size = new System.Drawing.Size(78, 24);
            this.btnStartFlash.TabIndex = 0;
            this.btnStartFlash.Text = "开始飞拍";
            this.btnStartFlash.UseVisualStyleBackColor = true;
            this.btnStartFlash.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnEndFlash
            // 
            this.btnEndFlash.Location = new System.Drawing.Point(193, 1);
            this.btnEndFlash.Name = "btnEndFlash";
            this.btnEndFlash.Size = new System.Drawing.Size(67, 24);
            this.btnEndFlash.TabIndex = 0;
            this.btnEndFlash.Text = "结束飞拍";
            this.btnEndFlash.UseVisualStyleBackColor = true;
            this.btnEndFlash.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 792);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel5, 4);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl3, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl4, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl5, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl6, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl7, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl8, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl9, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl10, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl11, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.hWindowControl12, 3, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel5, 3);
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1184, 732);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(1, 1);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(294, 241);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(294, 241);
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(297, 1);
            this.hWindowControl2.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(294, 241);
            this.hWindowControl2.TabIndex = 0;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(294, 241);
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(593, 1);
            this.hWindowControl3.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(294, 241);
            this.hWindowControl3.TabIndex = 0;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(294, 241);
            // 
            // hWindowControl4
            // 
            this.hWindowControl4.BackColor = System.Drawing.Color.Black;
            this.hWindowControl4.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl4.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl4.Location = new System.Drawing.Point(889, 1);
            this.hWindowControl4.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl4.Name = "hWindowControl4";
            this.hWindowControl4.Size = new System.Drawing.Size(294, 241);
            this.hWindowControl4.TabIndex = 0;
            this.hWindowControl4.WindowSize = new System.Drawing.Size(294, 241);
            // 
            // hWindowControl5
            // 
            this.hWindowControl5.BackColor = System.Drawing.Color.Black;
            this.hWindowControl5.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl5.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl5.Location = new System.Drawing.Point(1, 244);
            this.hWindowControl5.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl5.Name = "hWindowControl5";
            this.hWindowControl5.Size = new System.Drawing.Size(294, 242);
            this.hWindowControl5.TabIndex = 0;
            this.hWindowControl5.WindowSize = new System.Drawing.Size(294, 242);
            // 
            // hWindowControl6
            // 
            this.hWindowControl6.BackColor = System.Drawing.Color.Black;
            this.hWindowControl6.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl6.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl6.Location = new System.Drawing.Point(297, 244);
            this.hWindowControl6.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl6.Name = "hWindowControl6";
            this.hWindowControl6.Size = new System.Drawing.Size(294, 242);
            this.hWindowControl6.TabIndex = 0;
            this.hWindowControl6.WindowSize = new System.Drawing.Size(294, 242);
            // 
            // hWindowControl7
            // 
            this.hWindowControl7.BackColor = System.Drawing.Color.Black;
            this.hWindowControl7.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl7.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl7.Location = new System.Drawing.Point(593, 244);
            this.hWindowControl7.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl7.Name = "hWindowControl7";
            this.hWindowControl7.Size = new System.Drawing.Size(294, 242);
            this.hWindowControl7.TabIndex = 0;
            this.hWindowControl7.WindowSize = new System.Drawing.Size(294, 242);
            // 
            // hWindowControl8
            // 
            this.hWindowControl8.BackColor = System.Drawing.Color.Black;
            this.hWindowControl8.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl8.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl8.Location = new System.Drawing.Point(889, 244);
            this.hWindowControl8.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl8.Name = "hWindowControl8";
            this.hWindowControl8.Size = new System.Drawing.Size(294, 242);
            this.hWindowControl8.TabIndex = 0;
            this.hWindowControl8.WindowSize = new System.Drawing.Size(294, 242);
            // 
            // hWindowControl9
            // 
            this.hWindowControl9.BackColor = System.Drawing.Color.Black;
            this.hWindowControl9.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl9.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl9.Location = new System.Drawing.Point(1, 488);
            this.hWindowControl9.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl9.Name = "hWindowControl9";
            this.hWindowControl9.Size = new System.Drawing.Size(294, 243);
            this.hWindowControl9.TabIndex = 0;
            this.hWindowControl9.WindowSize = new System.Drawing.Size(294, 243);
            // 
            // hWindowControl10
            // 
            this.hWindowControl10.BackColor = System.Drawing.Color.Black;
            this.hWindowControl10.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl10.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl10.Location = new System.Drawing.Point(297, 488);
            this.hWindowControl10.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl10.Name = "hWindowControl10";
            this.hWindowControl10.Size = new System.Drawing.Size(294, 243);
            this.hWindowControl10.TabIndex = 0;
            this.hWindowControl10.WindowSize = new System.Drawing.Size(294, 243);
            // 
            // hWindowControl11
            // 
            this.hWindowControl11.BackColor = System.Drawing.Color.Black;
            this.hWindowControl11.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl11.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl11.Location = new System.Drawing.Point(593, 488);
            this.hWindowControl11.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl11.Name = "hWindowControl11";
            this.hWindowControl11.Size = new System.Drawing.Size(294, 243);
            this.hWindowControl11.TabIndex = 0;
            this.hWindowControl11.WindowSize = new System.Drawing.Size(294, 243);
            // 
            // hWindowControl12
            // 
            this.hWindowControl12.BackColor = System.Drawing.Color.Black;
            this.hWindowControl12.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl12.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl12.Location = new System.Drawing.Point(889, 488);
            this.hWindowControl12.Margin = new System.Windows.Forms.Padding(1);
            this.hWindowControl12.Name = "hWindowControl12";
            this.hWindowControl12.Size = new System.Drawing.Size(294, 243);
            this.hWindowControl12.TabIndex = 0;
            this.hWindowControl12.WindowSize = new System.Drawing.Size(294, 243);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.rbtn2);
            this.panel1.Controls.Add(this.nudDelay);
            this.panel1.Controls.Add(this.rbtn4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbtn1);
            this.panel1.Controls.Add(this.rbtn3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 30);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(514, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "mm";
            // 
            // rbtn2
            // 
            this.rbtn2.AutoSize = true;
            this.rbtn2.Location = new System.Drawing.Point(86, 6);
            this.rbtn2.Name = "rbtn2";
            this.rbtn2.Size = new System.Drawing.Size(71, 16);
            this.rbtn2.TabIndex = 4;
            this.rbtn2.Text = "取料二部";
            this.rbtn2.UseVisualStyleBackColor = true;
            this.rbtn2.CheckedChanged += new System.EventHandler(this.rbtn2_CheckedChanged);
            // 
            // nudDelay
            // 
            this.nudDelay.DecimalPlaces = 3;
            this.nudDelay.Location = new System.Drawing.Point(445, 4);
            this.nudDelay.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudDelay.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(63, 21);
            this.nudDelay.TabIndex = 1;
            this.nudDelay.ValueChanged += new System.EventHandler(this.nudDelay_ValueChanged);
            // 
            // rbtn4
            // 
            this.rbtn4.AutoSize = true;
            this.rbtn4.Location = new System.Drawing.Point(250, 6);
            this.rbtn4.Name = "rbtn4";
            this.rbtn4.Size = new System.Drawing.Size(71, 16);
            this.rbtn4.TabIndex = 5;
            this.rbtn4.Text = "组装二部";
            this.rbtn4.UseVisualStyleBackColor = true;
            this.rbtn4.CheckedChanged += new System.EventHandler(this.rbtn4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(362, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "飞拍补偿值：";
            // 
            // rbtn1
            // 
            this.rbtn1.AutoSize = true;
            this.rbtn1.Checked = true;
            this.rbtn1.Location = new System.Drawing.Point(3, 6);
            this.rbtn1.Name = "rbtn1";
            this.rbtn1.Size = new System.Drawing.Size(71, 16);
            this.rbtn1.TabIndex = 6;
            this.rbtn1.TabStop = true;
            this.rbtn1.Text = "取料一部";
            this.rbtn1.UseVisualStyleBackColor = true;
            this.rbtn1.CheckedChanged += new System.EventHandler(this.rbtn1_CheckedChanged);
            // 
            // rbtn3
            // 
            this.rbtn3.AutoSize = true;
            this.rbtn3.Location = new System.Drawing.Point(167, 6);
            this.rbtn3.Name = "rbtn3";
            this.rbtn3.Size = new System.Drawing.Size(71, 16);
            this.rbtn3.TabIndex = 7;
            this.rbtn3.Text = "组装一部";
            this.rbtn3.UseVisualStyleBackColor = true;
            this.rbtn3.CheckedChanged += new System.EventHandler(this.rbtn3_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStartFlash);
            this.panel2.Controls.Add(this.btnEndFlash);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(592, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(296, 30);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbCam);
            this.panel3.Location = new System.Drawing.Point(890, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 26);
            this.panel3.TabIndex = 4;
            // 
            // cbCam
            // 
            this.cbCam.AutoSize = true;
            this.cbCam.Location = new System.Drawing.Point(28, 6);
            this.cbCam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCam.Name = "cbCam";
            this.cbCam.Size = new System.Drawing.Size(96, 16);
            this.cbCam.TabIndex = 0;
            this.cbCam.Text = "只用定点拍照";
            this.cbCam.UseVisualStyleBackColor = true;
            this.cbCam.Click += new System.EventHandler(this.cbCam_Click);
            // 
            // FrmTestFlash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 792);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmTestFlash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "飞拍测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTestFlash_FormClosing);
            this.Load += new System.EventHandler(this.FrmTestFlash_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartFlash;
        private System.Windows.Forms.Button btnEndFlash;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private HalconDotNet.HWindowControl hWindowControl1;
        private HalconDotNet.HWindowControl hWindowControl2;
        private HalconDotNet.HWindowControl hWindowControl3;
        private HalconDotNet.HWindowControl hWindowControl4;
        private HalconDotNet.HWindowControl hWindowControl5;
        private HalconDotNet.HWindowControl hWindowControl6;
        private HalconDotNet.HWindowControl hWindowControl7;
        private HalconDotNet.HWindowControl hWindowControl8;
        private HalconDotNet.HWindowControl hWindowControl9;
        private HalconDotNet.HWindowControl hWindowControl10;
        private HalconDotNet.HWindowControl hWindowControl11;
        private HalconDotNet.HWindowControl hWindowControl12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtn2;
        private System.Windows.Forms.RadioButton rbtn4;
        private System.Windows.Forms.RadioButton rbtn1;
        private System.Windows.Forms.RadioButton rbtn3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbCam;
    }
}