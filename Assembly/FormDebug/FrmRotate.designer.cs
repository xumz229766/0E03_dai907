namespace Assembly
{
    partial class FrmRotate
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.btnSetProcess = new System.Windows.Forms.Button();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnSaveImg = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnCompute = new System.Windows.Forms.Button();
            this.lblCenter = new System.Windows.Forms.Label();
            this.btnGlueXY = new System.Windows.Forms.Button();
            this.btnGlueZ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblTestResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 2448, 2048);
            this.hWindowControl1.Location = new System.Drawing.Point(3, 3);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(320, 270);
            this.hWindowControl1.TabIndex = 1;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(320, 270);
            // 
            // btnSetProcess
            // 
            this.btnSetProcess.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetProcess.Location = new System.Drawing.Point(159, 286);
            this.btnSetProcess.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetProcess.Name = "btnSetProcess";
            this.btnSetProcess.Size = new System.Drawing.Size(69, 27);
            this.btnSetProcess.TabIndex = 8;
            this.btnSetProcess.Text = "检测设置";
            this.btnSetProcess.UseVisualStyleBackColor = true;
            this.btnSetProcess.Click += new System.EventHandler(this.btnSetProcess_Click);
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(10, 285);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(59, 29);
            this.btnTrigger.TabIndex = 11;
            this.btnTrigger.Text = "拍照";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTrigger_MouseDown);
            this.btnTrigger.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTrigger_MouseUp);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(84, 286);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(62, 27);
            this.btnCheck.TabIndex = 12;
            this.btnCheck.Text = "检测";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Location = new System.Drawing.Point(245, 286);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(78, 29);
            this.btnSaveImg.TabIndex = 13;
            this.btnSaveImg.Text = "保存图片";
            this.btnSaveImg.UseVisualStyleBackColor = true;
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "转动角度步长:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(106, 370);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 21);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(22, 401);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 28);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "开始旋转";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(158, 401);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(98, 28);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "停止旋转";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtData);
            this.groupBox1.Location = new System.Drawing.Point(329, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 445);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据列表";
            // 
            // txtData
            // 
            this.txtData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtData.Location = new System.Drawing.Point(3, 17);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtData.Size = new System.Drawing.Size(355, 425);
            this.txtData.TabIndex = 0;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(22, 435);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(103, 36);
            this.btnCompute.TabIndex = 20;
            this.btnCompute.Text = "获取旋转中心";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // lblCenter
            // 
            this.lblCenter.Location = new System.Drawing.Point(156, 435);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(141, 36);
            this.lblCenter.TabIndex = 21;
            this.lblCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGlueXY
            // 
            this.btnGlueXY.Location = new System.Drawing.Point(10, 324);
            this.btnGlueXY.Margin = new System.Windows.Forms.Padding(2);
            this.btnGlueXY.Name = "btnGlueXY";
            this.btnGlueXY.Size = new System.Drawing.Size(136, 35);
            this.btnGlueXY.TabIndex = 22;
            this.btnGlueXY.Text = "点胶拍照位";
            this.btnGlueXY.UseVisualStyleBackColor = true;
            this.btnGlueXY.Click += new System.EventHandler(this.btnGlueXY_Click);
            // 
            // btnGlueZ
            // 
            this.btnGlueZ.Location = new System.Drawing.Point(159, 324);
            this.btnGlueZ.Margin = new System.Windows.Forms.Padding(2);
            this.btnGlueZ.Name = "btnGlueZ";
            this.btnGlueZ.Size = new System.Drawing.Size(136, 35);
            this.btnGlueZ.TabIndex = 22;
            this.btnGlueZ.Text = "Z轴安全位";
            this.btnGlueZ.UseVisualStyleBackColor = true;
            this.btnGlueZ.Click += new System.EventHandler(this.btnGlueZ_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 496);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "验证角度:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Location = new System.Drawing.Point(92, 494);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(61, 21);
            this.numericUpDown2.TabIndex = 16;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 489);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 27);
            this.button1.TabIndex = 24;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(245, 489);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 27);
            this.button2.TabIndex = 24;
            this.button2.Text = "获取检验位置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblTestResult
            // 
            this.lblTestResult.Location = new System.Drawing.Point(363, 480);
            this.lblTestResult.Name = "lblTestResult";
            this.lblTestResult.Size = new System.Drawing.Size(198, 36);
            this.lblTestResult.TabIndex = 21;
            this.lblTestResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRotate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 543);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGlueZ);
            this.Controls.Add(this.btnGlueXY);
            this.Controls.Add(this.lblTestResult);
            this.Controls.Add(this.lblCenter);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSaveImg);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnTrigger);
            this.Controls.Add(this.btnSetProcess);
            this.Controls.Add(this.hWindowControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmRotate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "旋转中心获取";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmRotate_FormClosed);
            this.Load += new System.EventHandler(this.FrmRotate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button btnSetProcess;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnSaveImg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Label lblCenter;
        private System.Windows.Forms.Button btnGlueXY;
        private System.Windows.Forms.Button btnGlueZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblTestResult;
    }
}