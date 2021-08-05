namespace Assembly
{
    partial class FrmPixelToAxis
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudGain = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.nudDist = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudCalibY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCalibX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.btnCheckSet = new System.Windows.Forms.Button();
            this.btnTriggerCam = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.txtCalibNow = new System.Windows.Forms.TextBox();
            this.txtCalib = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnUpdateCalib = new System.Windows.Forms.Button();
            this.btnCheckResult = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCurrentAxisPos = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudPosX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudPosY = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.nudRow = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudCol = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudGain);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.btnGet);
            this.panel1.Controls.Add(this.nudDist);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nudCalibY);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudCalibX);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.radioButton7);
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1399, 118);
            this.panel1.TabIndex = 0;
            // 
            // nudGain
            // 
            this.nudGain.Font = new System.Drawing.Font("宋体", 9F);
            this.nudGain.Location = new System.Drawing.Point(881, 68);
            this.nudGain.Margin = new System.Windows.Forms.Padding(4);
            this.nudGain.Name = "nudGain";
            this.nudGain.Size = new System.Drawing.Size(84, 25);
            this.nudGain.TabIndex = 15;
            this.nudGain.ValueChanged += new System.EventHandler(this.nudGain_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(829, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "增益:";
            // 
            // btnGo
            // 
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGo.Location = new System.Drawing.Point(496, 68);
            this.btnGo.Margin = new System.Windows.Forms.Padding(4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(60, 29);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnGet
            // 
            this.btnGet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGet.Location = new System.Drawing.Point(417, 68);
            this.btnGet.Margin = new System.Windows.Forms.Padding(4);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(60, 29);
            this.btnGet.TabIndex = 7;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // nudDist
            // 
            this.nudDist.DecimalPlaces = 2;
            this.nudDist.Location = new System.Drawing.Point(683, 68);
            this.nudDist.Margin = new System.Windows.Forms.Padding(4);
            this.nudDist.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudDist.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudDist.Name = "nudDist";
            this.nudDist.Size = new System.Drawing.Size(99, 25);
            this.nudDist.TabIndex = 6;
            this.nudDist.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 74);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "移动间距(mm):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y";
            // 
            // nudCalibY
            // 
            this.nudCalibY.DecimalPlaces = 3;
            this.nudCalibY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudCalibY.Location = new System.Drawing.Point(309, 68);
            this.nudCalibY.Margin = new System.Windows.Forms.Padding(4);
            this.nudCalibY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCalibY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudCalibY.Name = "nudCalibY";
            this.nudCalibY.Size = new System.Drawing.Size(96, 25);
            this.nudCalibY.TabIndex = 3;
            this.nudCalibY.ValueChanged += new System.EventHandler(this.nudCalibY_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "X";
            // 
            // nudCalibX
            // 
            this.nudCalibX.DecimalPlaces = 3;
            this.nudCalibX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudCalibX.Location = new System.Drawing.Point(176, 68);
            this.nudCalibX.Margin = new System.Windows.Forms.Padding(4);
            this.nudCalibX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCalibX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudCalibX.Name = "nudCalibX";
            this.nudCalibX.Size = new System.Drawing.Size(103, 25);
            this.nudCalibX.TabIndex = 3;
            this.nudCalibX.ValueChanged += new System.EventHandler(this.nudCalibX_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "标定坐标:";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(880, 19);
            this.radioButton7.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(58, 19);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.Text = "点胶";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(748, 19);
            this.radioButton6.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(104, 19);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.Text = "组装2工位2";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(608, 19);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(104, 19);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "组装2工位1";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(468, 19);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(104, 19);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "组装1工位2";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(237, 19);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(66, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "取料2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(339, 19);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 19);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.Text = "组装1工位1";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(151, 19);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(66, 19);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "取料1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "上相机标定选择:";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 2448, 2048);
            this.hWindowControl1.Location = new System.Drawing.Point(7, 132);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(4);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(515, 417);
            this.hWindowControl1.TabIndex = 1;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(515, 417);
            // 
            // btnCheckSet
            // 
            this.btnCheckSet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckSet.Location = new System.Drawing.Point(363, 557);
            this.btnCheckSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckSet.Name = "btnCheckSet";
            this.btnCheckSet.Size = new System.Drawing.Size(223, 41);
            this.btnCheckSet.TabIndex = 2;
            this.btnCheckSet.Text = "上相机标定方法设定";
            this.btnCheckSet.UseVisualStyleBackColor = true;
            this.btnCheckSet.Click += new System.EventHandler(this.btnCheckSet_Click);
            // 
            // btnTriggerCam
            // 
            this.btnTriggerCam.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTriggerCam.Location = new System.Drawing.Point(16, 557);
            this.btnTriggerCam.Margin = new System.Windows.Forms.Padding(4);
            this.btnTriggerCam.Name = "btnTriggerCam";
            this.btnTriggerCam.Size = new System.Drawing.Size(155, 41);
            this.btnTriggerCam.TabIndex = 2;
            this.btnTriggerCam.Text = "拍 照";
            this.btnTriggerCam.UseVisualStyleBackColor = true;
            this.btnTriggerCam.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTriggerCam_MouseDown);
            this.btnTriggerCam.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTriggerCam_MouseUp);
            // 
            // btnCheck
            // 
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Location = new System.Drawing.Point(183, 557);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(155, 41);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "检 测";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtCalibNow
            // 
            this.txtCalibNow.Location = new System.Drawing.Point(557, 132);
            this.txtCalibNow.Margin = new System.Windows.Forms.Padding(4);
            this.txtCalibNow.Multiline = true;
            this.txtCalibNow.Name = "txtCalibNow";
            this.txtCalibNow.ReadOnly = true;
            this.txtCalibNow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCalibNow.Size = new System.Drawing.Size(243, 416);
            this.txtCalibNow.TabIndex = 3;
            // 
            // txtCalib
            // 
            this.txtCalib.Location = new System.Drawing.Point(864, 134);
            this.txtCalib.Margin = new System.Windows.Forms.Padding(4);
            this.txtCalib.Multiline = true;
            this.txtCalib.Name = "txtCalib";
            this.txtCalib.ReadOnly = true;
            this.txtCalib.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCalib.Size = new System.Drawing.Size(243, 416);
            this.txtCalib.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(16, 606);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(321, 52);
            this.button4.TabIndex = 2;
            this.button4.Text = "开 始 标 定";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnUpdateCalib
            // 
            this.btnUpdateCalib.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateCalib.Location = new System.Drawing.Point(363, 616);
            this.btnUpdateCalib.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateCalib.Name = "btnUpdateCalib";
            this.btnUpdateCalib.Size = new System.Drawing.Size(223, 42);
            this.btnUpdateCalib.TabIndex = 2;
            this.btnUpdateCalib.Text = "更新标定结果";
            this.btnUpdateCalib.UseVisualStyleBackColor = true;
            this.btnUpdateCalib.Click += new System.EventHandler(this.btnUpdateCalib_Click);
            // 
            // btnCheckResult
            // 
            this.btnCheckResult.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckResult.Location = new System.Drawing.Point(1361, 577);
            this.btnCheckResult.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckResult.Name = "btnCheckResult";
            this.btnCheckResult.Size = new System.Drawing.Size(100, 39);
            this.btnCheckResult.TabIndex = 4;
            this.btnCheckResult.Text = "验 证";
            this.btnCheckResult.UseVisualStyleBackColor = true;
            this.btnCheckResult.Click += new System.EventHandler(this.btnCheckResult_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1079, 643);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "验证坐标结果:";
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(1197, 637);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(221, 29);
            this.lblResult.TabIndex = 6;
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(619, 639);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "当前轴坐标:";
            // 
            // lblCurrentAxisPos
            // 
            this.lblCurrentAxisPos.Location = new System.Drawing.Point(732, 629);
            this.lblCurrentAxisPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentAxisPos.Name = "lblCurrentAxisPos";
            this.lblCurrentAxisPos.Size = new System.Drawing.Size(221, 29);
            this.lblCurrentAxisPos.TabIndex = 6;
            this.lblCurrentAxisPos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(645, 585);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "目标位置:";
            // 
            // nudPosX
            // 
            this.nudPosX.DecimalPlaces = 3;
            this.nudPosX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPosX.Location = new System.Drawing.Point(745, 581);
            this.nudPosX.Margin = new System.Windows.Forms.Padding(4);
            this.nudPosX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPosX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudPosX.Name = "nudPosX";
            this.nudPosX.Size = new System.Drawing.Size(83, 25);
            this.nudPosX.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(722, 586);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "X";
            // 
            // nudPosY
            // 
            this.nudPosY.DecimalPlaces = 3;
            this.nudPosY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudPosY.Location = new System.Drawing.Point(859, 582);
            this.nudPosY.Margin = new System.Windows.Forms.Padding(4);
            this.nudPosY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPosY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudPosY.Name = "nudPosY";
            this.nudPosY.Size = new System.Drawing.Size(84, 25);
            this.nudPosY.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(836, 587);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "Y";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(960, 579);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 29);
            this.button1.TabIndex = 7;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1046, 587);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "像素坐标:";
            // 
            // nudRow
            // 
            this.nudRow.DecimalPlaces = 3;
            this.nudRow.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudRow.Location = new System.Drawing.Point(1146, 583);
            this.nudRow.Margin = new System.Windows.Forms.Padding(4);
            this.nudRow.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudRow.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(83, 25);
            this.nudRow.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1123, 588);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 15);
            this.label13.TabIndex = 4;
            this.label13.Text = "R";
            // 
            // nudCol
            // 
            this.nudCol.DecimalPlaces = 3;
            this.nudCol.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudCol.Location = new System.Drawing.Point(1260, 584);
            this.nudCol.Margin = new System.Windows.Forms.Padding(4);
            this.nudCol.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudCol.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(84, 25);
            this.nudCol.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1237, 589);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 15);
            this.label14.TabIndex = 4;
            this.label14.Text = "C";
            // 
            // FrmPixelToAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 719);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblCurrentAxisPos);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnCheckResult);
            this.Controls.Add(this.nudCol);
            this.Controls.Add(this.nudPosY);
            this.Controls.Add(this.txtCalib);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCalibNow);
            this.Controls.Add(this.nudRow);
            this.Controls.Add(this.nudPosX);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnUpdateCalib);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnTriggerCam);
            this.Controls.Add(this.btnCheckSet);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPixelToAxis";
            this.Text = "FrmPixelToAxis";
            this.Load += new System.EventHandler(this.FrmPixelToAxis_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCalibX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudCalibY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCalibX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button btnCheckSet;
        private System.Windows.Forms.Button btnTriggerCam;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.NumericUpDown nudDist;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCalibNow;
        private System.Windows.Forms.TextBox txtCalib;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnUpdateCalib;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnCheckResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCurrentAxisPos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudPosX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudPosY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudGain;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudRow;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudCol;
        private System.Windows.Forms.Label label14;
    }
}