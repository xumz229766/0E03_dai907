namespace Motion0
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudCmdPos = new System.Windows.Forms.NumericUpDown();
            this.lblTarget = new System.Windows.Forms.Label();
            this.btnAbsMove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdAxis = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudSetVel = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRelM = new System.Windows.Forms.Button();
            this.btnRelP = new System.Windows.Forms.Button();
            this.nudRelDist = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnJOGM = new System.Windows.Forms.Button();
            this.btnJOGP = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbEnTrigger = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudTrigPos2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudTrigPos1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudChangePos = new System.Windows.Forms.NumericUpDown();
            this.btnChangePos = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.nudChangeVel = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCmdPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSetVel)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelDist)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigPos2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigPos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangePos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeVel)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "初始化";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 285);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(712, 109);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "信息栏";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(630, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 42);
            this.button2.TabIndex = 3;
            this.button2.Text = "断开";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(33, 213);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 24);
            this.button3.TabIndex = 4;
            this.button3.Text = "IO";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(33, 87);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "使能on";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(33, 128);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "使能off";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudChangePos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.nudCmdPos);
            this.groupBox1.Controls.Add(this.lblTarget);
            this.groupBox1.Controls.Add(this.btnChangePos);
            this.groupBox1.Controls.Add(this.btnAbsMove);
            this.groupBox1.Location = new System.Drawing.Point(158, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 115);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "绝对运动";
            // 
            // nudCmdPos
            // 
            this.nudCmdPos.Location = new System.Drawing.Point(21, 44);
            this.nudCmdPos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCmdPos.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.nudCmdPos.Name = "nudCmdPos";
            this.nudCmdPos.Size = new System.Drawing.Size(99, 21);
            this.nudCmdPos.TabIndex = 4;
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(17, 24);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(65, 12);
            this.lblTarget.TabIndex = 3;
            this.lblTarget.Text = "目标位置：";
            // 
            // btnAbsMove
            // 
            this.btnAbsMove.Location = new System.Drawing.Point(140, 26);
            this.btnAbsMove.Name = "btnAbsMove";
            this.btnAbsMove.Size = new System.Drawing.Size(44, 39);
            this.btnAbsMove.TabIndex = 2;
            this.btnAbsMove.Text = "GO";
            this.btnAbsMove.UseVisualStyleBackColor = true;
            this.btnAbsMove.Click += new System.EventHandler(this.btnAbsMove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "选择轴:";
            // 
            // cmdAxis
            // 
            this.cmdAxis.FormattingEnabled = true;
            this.cmdAxis.Items.AddRange(new object[] {
            "X轴",
            "Y轴",
            "Z轴",
            "C轴"});
            this.cmdAxis.Location = new System.Drawing.Point(244, 10);
            this.cmdAxis.Name = "cmdAxis";
            this.cmdAxis.Size = new System.Drawing.Size(65, 20);
            this.cmdAxis.TabIndex = 1;
            this.cmdAxis.Text = "X轴";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "速度:";
            // 
            // nudSetVel
            // 
            this.nudSetVel.Location = new System.Drawing.Point(370, 9);
            this.nudSetVel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSetVel.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSetVel.Name = "nudSetVel";
            this.nudSetVel.Size = new System.Drawing.Size(75, 21);
            this.nudSetVel.TabIndex = 9;
            this.nudSetVel.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRelM);
            this.groupBox2.Controls.Add(this.btnRelP);
            this.groupBox2.Controls.Add(this.nudRelDist);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(158, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 82);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相对运动";
            // 
            // btnRelM
            // 
            this.btnRelM.Location = new System.Drawing.Point(121, 43);
            this.btnRelM.Name = "btnRelM";
            this.btnRelM.Size = new System.Drawing.Size(61, 33);
            this.btnRelM.TabIndex = 5;
            this.btnRelM.Text = "反转";
            this.btnRelM.UseVisualStyleBackColor = true;
            this.btnRelM.Click += new System.EventHandler(this.btnRelM_Click);
            // 
            // btnRelP
            // 
            this.btnRelP.Location = new System.Drawing.Point(19, 43);
            this.btnRelP.Name = "btnRelP";
            this.btnRelP.Size = new System.Drawing.Size(61, 33);
            this.btnRelP.TabIndex = 5;
            this.btnRelP.Text = "正转";
            this.btnRelP.UseVisualStyleBackColor = true;
            this.btnRelP.Click += new System.EventHandler(this.btnRelP_Click);
            // 
            // nudRelDist
            // 
            this.nudRelDist.Location = new System.Drawing.Point(72, 14);
            this.nudRelDist.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudRelDist.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.nudRelDist.Name = "nudRelDist";
            this.nudRelDist.Size = new System.Drawing.Size(99, 21);
            this.nudRelDist.TabIndex = 4;
            this.nudRelDist.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "距离：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnJOGM);
            this.groupBox3.Controls.Add(this.btnJOGP);
            this.groupBox3.Location = new System.Drawing.Point(380, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 56);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "JOG";
            // 
            // btnJOGM
            // 
            this.btnJOGM.Location = new System.Drawing.Point(119, 15);
            this.btnJOGM.Name = "btnJOGM";
            this.btnJOGM.Size = new System.Drawing.Size(75, 35);
            this.btnJOGM.TabIndex = 0;
            this.btnJOGM.Text = "JOG-";
            this.btnJOGM.UseVisualStyleBackColor = true;
            this.btnJOGM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJOGM_MouseDown);
            this.btnJOGM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJOGM_MouseUp);
            // 
            // btnJOGP
            // 
            this.btnJOGP.Location = new System.Drawing.Point(6, 15);
            this.btnJOGP.Name = "btnJOGP";
            this.btnJOGP.Size = new System.Drawing.Size(75, 35);
            this.btnJOGP.TabIndex = 0;
            this.btnJOGP.Text = "JOG+";
            this.btnJOGP.UseVisualStyleBackColor = true;
            this.btnJOGP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJOGP_MouseDown);
            this.btnJOGP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJOGP_MouseUp);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(34, 163);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 35);
            this.btnHome.TabIndex = 12;
            this.btnHome.Text = "回原点";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbEnTrigger);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.nudTrigPos2);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.nudTrigPos1);
            this.groupBox4.Location = new System.Drawing.Point(382, 117);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(198, 109);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "比较触发";
            // 
            // cbEnTrigger
            // 
            this.cbEnTrigger.AutoSize = true;
            this.cbEnTrigger.Location = new System.Drawing.Point(27, 87);
            this.cbEnTrigger.Name = "cbEnTrigger";
            this.cbEnTrigger.Size = new System.Drawing.Size(48, 16);
            this.cbEnTrigger.TabIndex = 5;
            this.cbEnTrigger.Text = "启用";
            this.cbEnTrigger.UseVisualStyleBackColor = true;
            this.cbEnTrigger.CheckedChanged += new System.EventHandler(this.cbEnTrigger_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "触发位置2：";
            // 
            // nudTrigPos2
            // 
            this.nudTrigPos2.Location = new System.Drawing.Point(102, 50);
            this.nudTrigPos2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTrigPos2.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.nudTrigPos2.Name = "nudTrigPos2";
            this.nudTrigPos2.Size = new System.Drawing.Size(67, 21);
            this.nudTrigPos2.TabIndex = 4;
            this.nudTrigPos2.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "触发位置1：";
            // 
            // nudTrigPos1
            // 
            this.nudTrigPos1.Location = new System.Drawing.Point(102, 20);
            this.nudTrigPos1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTrigPos1.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.nudTrigPos1.Name = "nudTrigPos1";
            this.nudTrigPos1.Size = new System.Drawing.Size(67, 21);
            this.nudTrigPos1.TabIndex = 4;
            this.nudTrigPos1.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "更改位置：";
            // 
            // nudChangePos
            // 
            this.nudChangePos.Location = new System.Drawing.Point(21, 92);
            this.nudChangePos.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudChangePos.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147483648});
            this.nudChangePos.Name = "nudChangePos";
            this.nudChangePos.Size = new System.Drawing.Size(99, 21);
            this.nudChangePos.TabIndex = 4;
            // 
            // btnChangePos
            // 
            this.btnChangePos.Location = new System.Drawing.Point(140, 72);
            this.btnChangePos.Name = "btnChangePos";
            this.btnChangePos.Size = new System.Drawing.Size(44, 39);
            this.btnChangePos.TabIndex = 2;
            this.btnChangePos.Text = "GO";
            this.btnChangePos.UseVisualStyleBackColor = true;
            this.btnChangePos.Click += new System.EventHandler(this.btnChangePos_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(533, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "更改速度";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // nudChangeVel
            // 
            this.nudChangeVel.Location = new System.Drawing.Point(475, 9);
            this.nudChangeVel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudChangeVel.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudChangeVel.Name = "nudChangeVel";
            this.nudChangeVel.Size = new System.Drawing.Size(51, 21);
            this.nudChangeVel.TabIndex = 9;
            this.nudChangeVel.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 393);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.nudChangeVel);
            this.Controls.Add(this.nudSetVel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.cmdAxis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCmdPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSetVel)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelDist)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigPos2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrigPos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangePos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChangeVel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudCmdPos;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Button btnAbsMove;
        private System.Windows.Forms.ComboBox cmdAxis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudSetVel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRelM;
        private System.Windows.Forms.Button btnRelP;
        private System.Windows.Forms.NumericUpDown nudRelDist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnJOGM;
        private System.Windows.Forms.Button btnJOGP;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudTrigPos2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudTrigPos1;
        private System.Windows.Forms.CheckBox cbEnTrigger;
        private System.Windows.Forms.NumericUpDown nudChangePos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnChangePos;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown nudChangeVel;
    }
}

