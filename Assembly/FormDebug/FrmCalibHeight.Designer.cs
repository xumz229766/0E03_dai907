namespace Assembly
{
    partial class FrmCalibHeight
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.nudDistZ = new System.Windows.Forms.NumericUpDown();
            this.nudDistX = new System.Windows.Forms.NumericUpDown();
            this.nudStartPosZ = new System.Windows.Forms.NumericUpDown();
            this.btnGet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudSerial = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnSingle = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.btnEx = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPressure = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSerial)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPressure)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.nudDistZ);
            this.groupBox1.Controls.Add(this.nudDistX);
            this.groupBox1.Controls.Add(this.nudStartPosZ);
            this.groupBox1.Controls.Add(this.btnGet);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudY);
            this.groupBox1.Controls.Add(this.nudX);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1036, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试选项";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(886, 14);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(122, 44);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "暂 停";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(744, 14);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(120, 44);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "开始测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(530, 77);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(56, 20);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // nudDistZ
            // 
            this.nudDistZ.DecimalPlaces = 3;
            this.nudDistZ.Location = new System.Drawing.Point(928, 76);
            this.nudDistZ.Margin = new System.Windows.Forms.Padding(2);
            this.nudDistZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDistZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudDistZ.Name = "nudDistZ";
            this.nudDistZ.Size = new System.Drawing.Size(64, 21);
            this.nudDistZ.TabIndex = 4;
            this.nudDistZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudDistX
            // 
            this.nudDistX.DecimalPlaces = 3;
            this.nudDistX.Location = new System.Drawing.Point(125, 75);
            this.nudDistX.Margin = new System.Windows.Forms.Padding(2);
            this.nudDistX.Name = "nudDistX";
            this.nudDistX.Size = new System.Drawing.Size(64, 21);
            this.nudDistX.TabIndex = 4;
            this.nudDistX.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudDistX.ValueChanged += new System.EventHandler(this.nudDistX_ValueChanged);
            // 
            // nudStartPosZ
            // 
            this.nudStartPosZ.DecimalPlaces = 3;
            this.nudStartPosZ.Location = new System.Drawing.Point(721, 75);
            this.nudStartPosZ.Margin = new System.Windows.Forms.Padding(2);
            this.nudStartPosZ.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStartPosZ.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudStartPosZ.Name = "nudStartPosZ";
            this.nudStartPosZ.Size = new System.Drawing.Size(64, 21);
            this.nudStartPosZ.TabIndex = 4;
            this.nudStartPosZ.ValueChanged += new System.EventHandler(this.nudStartPosZ_ValueChanged);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(460, 77);
            this.btnGet.Margin = new System.Windows.Forms.Padding(2);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(56, 20);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "Get";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(799, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "测试Z轴移动步长(mm):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "吸笔测试间距(mm):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "测试Z轴起始高度:";
            // 
            // nudY
            // 
            this.nudY.DecimalPlaces = 3;
            this.nudY.Location = new System.Drawing.Point(387, 76);
            this.nudY.Margin = new System.Windows.Forms.Padding(2);
            this.nudY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(60, 21);
            this.nudY.TabIndex = 2;
            this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
            // 
            // nudX
            // 
            this.nudX.DecimalPlaces = 3;
            this.nudX.Location = new System.Drawing.Point(322, 76);
            this.nudX.Margin = new System.Windows.Forms.Padding(2);
            this.nudX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(60, 21);
            this.nudX.TabIndex = 2;
            this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nudSerial);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.rbtnAll);
            this.panel2.Controls.Add(this.rbtnSingle);
            this.panel2.Location = new System.Drawing.Point(425, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 43);
            this.panel2.TabIndex = 2;
            // 
            // nudSerial
            // 
            this.nudSerial.Location = new System.Drawing.Point(240, 14);
            this.nudSerial.Margin = new System.Windows.Forms.Padding(2);
            this.nudSerial.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudSerial.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSerial.Name = "nudSerial";
            this.nudSerial.Size = new System.Drawing.Size(42, 21);
            this.nudSerial.TabIndex = 2;
            this.nudSerial.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "吸笔序号：";
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Location = new System.Drawing.Point(93, 16);
            this.rbtnAll.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(71, 16);
            this.rbtnAll.TabIndex = 0;
            this.rbtnAll.Text = "全部吸笔";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnSingle
            // 
            this.rbtnSingle.AutoSize = true;
            this.rbtnSingle.Checked = true;
            this.rbtnSingle.Location = new System.Drawing.Point(11, 16);
            this.rbtnSingle.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnSingle.Name = "rbtnSingle";
            this.rbtnSingle.Size = new System.Drawing.Size(71, 16);
            this.rbtnSingle.TabIndex = 0;
            this.rbtnSingle.TabStop = true;
            this.rbtnSingle.Text = "单个吸笔";
            this.rbtnSingle.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "吸笔1测试位(X,Y)：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Location = new System.Drawing.Point(4, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 44);
            this.panel1.TabIndex = 1;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 14);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "组装1部工位1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(303, 14);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(95, 16);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.Text = "组装2部工位2";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(104, 14);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "组装1部工位2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(202, 14);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "组装2部工位1";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(9, 134);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(250, 327);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "组装1部工位1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(4, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 70;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(242, 299);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(265, 134);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(250, 327);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "组装1部工位2";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(4, 18);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 70;
            this.dataGridView2.RowTemplate.Height = 27;
            this.dataGridView2.Size = new System.Drawing.Size(242, 299);
            this.dataGridView2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView3);
            this.groupBox4.Location = new System.Drawing.Point(530, 134);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(250, 327);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "组装2部工位1";
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.Location = new System.Drawing.Point(4, 18);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 70;
            this.dataGridView3.RowTemplate.Height = 27;
            this.dataGridView3.Size = new System.Drawing.Size(242, 299);
            this.dataGridView3.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridView4);
            this.groupBox5.Location = new System.Drawing.Point(797, 134);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(250, 327);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "组装2部工位2";
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.EnableHeadersVisualStyles = false;
            this.dataGridView4.Location = new System.Drawing.Point(4, 18);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.RowHeadersWidth = 70;
            this.dataGridView4.RowTemplate.Height = 27;
            this.dataGridView4.Size = new System.Drawing.Size(242, 299);
            this.dataGridView4.TabIndex = 0;
            // 
            // btnEx
            // 
            this.btnEx.Location = new System.Drawing.Point(801, 474);
            this.btnEx.Name = "btnEx";
            this.btnEx.Size = new System.Drawing.Size(111, 40);
            this.btnEx.TabIndex = 2;
            this.btnEx.Text = "数据导出";
            this.btnEx.UseVisualStyleBackColor = true;
            this.btnEx.Click += new System.EventHandler(this.btnEx_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 474);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "测试往返压力判定值:";
            // 
            // nudPressure
            // 
            this.nudPressure.DecimalPlaces = 1;
            this.nudPressure.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPressure.Location = new System.Drawing.Point(144, 472);
            this.nudPressure.Margin = new System.Windows.Forms.Padding(2);
            this.nudPressure.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPressure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudPressure.Name = "nudPressure";
            this.nudPressure.Size = new System.Drawing.Size(64, 21);
            this.nudPressure.TabIndex = 4;
            this.nudPressure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 474);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "N";
            // 
            // FrmCalibHeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 552);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnEx);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.nudPressure);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmCalibHeight";
            this.Text = "高度标定";
            this.Load += new System.EventHandler(this.FrmCalibHeight_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSerial)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPressure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnSingle;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.NumericUpDown nudStartPosZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.NumericUpDown nudDistZ;
        private System.Windows.Forms.NumericUpDown nudDistX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSerial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button btnEx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPressure;
        private System.Windows.Forms.Label label7;
    }
}