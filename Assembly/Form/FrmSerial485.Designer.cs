namespace Assembly
{
    partial class FrmSerial485
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetPort = new System.Windows.Forms.Button();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudOutputAddr = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetParam = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudInputChannel = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudInputAddr = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudOutputData = new System.Windows.Forms.NumericUpDown();
            this.btnSend = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInputData = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputAddr)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputAddr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputData)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSetPort);
            this.groupBox1.Controls.Add(this.cbPortName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本设置";
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(244, 24);
            this.btnSetPort.Name = "btnSetPort";
            this.btnSetPort.Size = new System.Drawing.Size(75, 23);
            this.btnSetPort.TabIndex = 2;
            this.btnSetPort.Text = "设置";
            this.btnSetPort.UseVisualStyleBackColor = true;
            this.btnSetPort.Click += new System.EventHandler(this.btnSetPort_Click);
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cbPortName.Location = new System.Drawing.Point(119, 27);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(100, 20);
            this.cbPortName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输出模块地址：";
            // 
            // nudOutputAddr
            // 
            this.nudOutputAddr.Location = new System.Drawing.Point(119, 34);
            this.nudOutputAddr.Name = "nudOutputAddr";
            this.nudOutputAddr.Size = new System.Drawing.Size(75, 21);
            this.nudOutputAddr.TabIndex = 3;
            this.nudOutputAddr.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetParam);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.nudInputChannel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nudInputAddr);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudOutputAddr);
            this.groupBox2.Location = new System.Drawing.Point(4, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 142);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输入输出设置";
            // 
            // btnSetParam
            // 
            this.btnSetParam.Location = new System.Drawing.Point(244, 57);
            this.btnSetParam.Name = "btnSetParam";
            this.btnSetParam.Size = new System.Drawing.Size(75, 44);
            this.btnSetParam.TabIndex = 4;
            this.btnSetParam.Text = "设定";
            this.btnSetParam.UseVisualStyleBackColor = true;
            this.btnSetParam.Click += new System.EventHandler(this.btnSetParam_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "输入模块通道：";
            // 
            // nudInputChannel
            // 
            this.nudInputChannel.Location = new System.Drawing.Point(119, 110);
            this.nudInputChannel.Name = "nudInputChannel";
            this.nudInputChannel.Size = new System.Drawing.Size(75, 21);
            this.nudInputChannel.TabIndex = 3;
            this.nudInputChannel.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "输入模块地址：";
            // 
            // nudInputAddr
            // 
            this.nudInputAddr.Location = new System.Drawing.Point(119, 71);
            this.nudInputAddr.Name = "nudInputAddr";
            this.nudInputAddr.Size = new System.Drawing.Size(75, 21);
            this.nudInputAddr.TabIndex = 3;
            this.nudInputAddr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "设定电流(mA)：";
            // 
            // nudOutputData
            // 
            this.nudOutputData.DecimalPlaces = 3;
            this.nudOutputData.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudOutputData.Location = new System.Drawing.Point(108, 258);
            this.nudOutputData.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudOutputData.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudOutputData.Name = "nudOutputData";
            this.nudOutputData.Size = new System.Drawing.Size(75, 21);
            this.nudOutputData.TabIndex = 6;
            this.nudOutputData.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(194, 258);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "获取电压值(V)：";
            // 
            // lblInputData
            // 
            this.lblInputData.Location = new System.Drawing.Point(117, 303);
            this.lblInputData.Name = "lblInputData";
            this.lblInputData.Size = new System.Drawing.Size(100, 23);
            this.lblInputData.TabIndex = 9;
            this.lblInputData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(366, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 289);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据标定";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 10);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.9991F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0021F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0021F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0021F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 269);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(43, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "输出电流(mA)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(168, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "压力值";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "1";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "2";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "3";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "4";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "5";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "6";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 173);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "7";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 196);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "8";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 219);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "9";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 247);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 1;
            this.label18.Text = "10";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(241, 304);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 23);
            this.lblTime.TabIndex = 9;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(275, 258);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 11;
            this.btnGetData.Text = "获取数据";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(645, 295);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 39);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(372, 309);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(119, 12);
            this.label19.TabIndex = 5;
            this.label19.Text = "设定压力测试(Mpa)：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(483, 303);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 21);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(564, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmSerial485
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 339);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblInputData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.nudOutputData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSerial485";
            this.Text = "电气比例阀设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSerial485_FormClosing);
            this.Load += new System.EventHandler(this.FrmSerial485_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputAddr)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInputAddr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputData)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudOutputAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSetParam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudInputChannel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudInputAddr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudOutputData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInputData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
    }
}