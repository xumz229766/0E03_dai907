namespace ImageProcess
{
    partial class UCMakeModel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSet = new System.Windows.Forms.Button();
            this.nudOutputCol = new System.Windows.Forms.NumericUpDown();
            this.nudOutputRow = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lblOrginCol = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblOrginRow = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbOutput = new System.Windows.Forms.CheckBox();
            this.nudMinScore = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.lblCol2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCol1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRow2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRow1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtnRegion = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateModel = new System.Windows.Forms.Button();
            this.btnGetContour = new System.Windows.Forms.Button();
            this.nudContrast = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblModelResult = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblOutputCol = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblOutputRow = new System.Windows.Forms.Label();
            this.lblModelCol = new System.Windows.Forms.Label();
            this.lblModelRow = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblModelAngle = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblModelScore = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.51064F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.48936F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 200);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.cbOutput);
            this.groupBox1.Controls.Add(this.nudMinScore);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnCreateModel);
            this.groupBox1.Controls.Add(this.btnGetContour);
            this.groupBox1.Controls.Add(this.nudContrast);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(322, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板设置";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSet);
            this.panel1.Controls.Add(this.nudOutputCol);
            this.panel1.Controls.Add(this.nudOutputRow);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblOrginCol);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.lblOrginRow);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(162, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 92);
            this.panel1.TabIndex = 13;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(104, 14);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(39, 37);
            this.btnSet.TabIndex = 10;
            this.btnSet.Text = "设定";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // nudOutputCol
            // 
            this.nudOutputCol.DecimalPlaces = 3;
            this.nudOutputCol.Location = new System.Drawing.Point(36, 36);
            this.nudOutputCol.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudOutputCol.Minimum = new decimal(new int[] {
            3000,
            0,
            0,
            -2147483648});
            this.nudOutputCol.Name = "nudOutputCol";
            this.nudOutputCol.Size = new System.Drawing.Size(62, 21);
            this.nudOutputCol.TabIndex = 9;
            // 
            // nudOutputRow
            // 
            this.nudOutputRow.DecimalPlaces = 3;
            this.nudOutputRow.Location = new System.Drawing.Point(36, 9);
            this.nudOutputRow.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudOutputRow.Minimum = new decimal(new int[] {
            3000,
            0,
            0,
            -2147483648});
            this.nudOutputRow.Name = "nudOutputRow";
            this.nudOutputRow.Size = new System.Drawing.Size(62, 21);
            this.nudOutputRow.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "Row:";
            // 
            // lblOrginCol
            // 
            this.lblOrginCol.AutoSize = true;
            this.lblOrginCol.Location = new System.Drawing.Point(85, 67);
            this.lblOrginCol.Name = "lblOrginCol";
            this.lblOrginCol.Size = new System.Drawing.Size(11, 12);
            this.lblOrginCol.TabIndex = 8;
            this.lblOrginCol.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 8;
            this.label13.Text = "Col:";
            // 
            // lblOrginRow
            // 
            this.lblOrginRow.AutoSize = true;
            this.lblOrginRow.Location = new System.Drawing.Point(22, 67);
            this.lblOrginRow.Name = "lblOrginRow";
            this.lblOrginRow.Size = new System.Drawing.Size(11, 12);
            this.lblOrginRow.TabIndex = 8;
            this.lblOrginRow.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "R:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "C:";
            // 
            // cbOutput
            // 
            this.cbOutput.AutoSize = true;
            this.cbOutput.Location = new System.Drawing.Point(164, 83);
            this.cbOutput.Name = "cbOutput";
            this.cbOutput.Size = new System.Drawing.Size(84, 16);
            this.cbOutput.TabIndex = 12;
            this.cbOutput.Text = "设置输出点";
            this.cbOutput.UseVisualStyleBackColor = true;
            this.cbOutput.CheckedChanged += new System.EventHandler(this.cbOutput_CheckedChanged);
            // 
            // nudMinScore
            // 
            this.nudMinScore.DecimalPlaces = 2;
            this.nudMinScore.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudMinScore.Location = new System.Drawing.Point(225, 54);
            this.nudMinScore.Name = "nudMinScore";
            this.nudMinScore.Size = new System.Drawing.Size(69, 21);
            this.nudMinScore.TabIndex = 10;
            this.nudMinScore.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnAll);
            this.groupBox2.Controls.Add(this.lblCol2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblCol1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblRow2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblRow1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rbtnRegion);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(3, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 152);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "搜索区域设定";
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(6, 20);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(59, 16);
            this.rbtnAll.TabIndex = 7;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "全图像";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // lblCol2
            // 
            this.lblCol2.AutoSize = true;
            this.lblCol2.Location = new System.Drawing.Point(102, 121);
            this.lblCol2.Name = "lblCol2";
            this.lblCol2.Size = new System.Drawing.Size(11, 12);
            this.lblCol2.TabIndex = 8;
            this.lblCol2.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Col2:";
            // 
            // lblCol1
            // 
            this.lblCol1.AutoSize = true;
            this.lblCol1.Location = new System.Drawing.Point(102, 57);
            this.lblCol1.Name = "lblCol1";
            this.lblCol1.Size = new System.Drawing.Size(11, 12);
            this.lblCol1.TabIndex = 8;
            this.lblCol1.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Col1:";
            // 
            // lblRow2
            // 
            this.lblRow2.AutoSize = true;
            this.lblRow2.Location = new System.Drawing.Point(102, 89);
            this.lblRow2.Name = "lblRow2";
            this.lblRow2.Size = new System.Drawing.Size(11, 12);
            this.lblRow2.TabIndex = 8;
            this.lblRow2.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Row2:";
            // 
            // lblRow1
            // 
            this.lblRow1.AutoSize = true;
            this.lblRow1.Location = new System.Drawing.Point(103, 21);
            this.lblRow1.Name = "lblRow1";
            this.lblRow1.Size = new System.Drawing.Size(11, 12);
            this.lblRow1.TabIndex = 8;
            this.lblRow1.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Row1:";
            // 
            // rbtnRegion
            // 
            this.rbtnRegion.Location = new System.Drawing.Point(6, 55);
            this.rbtnRegion.Name = "rbtnRegion";
            this.rbtnRegion.Size = new System.Drawing.Size(59, 52);
            this.rbtnRegion.TabIndex = 7;
            this.rbtnRegion.Text = "区域设定";
            this.rbtnRegion.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "最小得分：";
            // 
            // btnCreateModel
            // 
            this.btnCreateModel.Location = new System.Drawing.Point(231, 13);
            this.btnCreateModel.Name = "btnCreateModel";
            this.btnCreateModel.Size = new System.Drawing.Size(75, 23);
            this.btnCreateModel.TabIndex = 3;
            this.btnCreateModel.Text = "创建模板";
            this.btnCreateModel.UseVisualStyleBackColor = true;
            this.btnCreateModel.Click += new System.EventHandler(this.btnCreateModel_Click);
            // 
            // btnGetContour
            // 
            this.btnGetContour.Location = new System.Drawing.Point(120, 13);
            this.btnGetContour.Name = "btnGetContour";
            this.btnGetContour.Size = new System.Drawing.Size(62, 23);
            this.btnGetContour.TabIndex = 2;
            this.btnGetContour.Text = "生成轮廓";
            this.btnGetContour.UseVisualStyleBackColor = true;
            this.btnGetContour.Click += new System.EventHandler(this.btnGetContour_Click);
            // 
            // nudContrast
            // 
            this.nudContrast.Location = new System.Drawing.Point(59, 15);
            this.nudContrast.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudContrast.Name = "nudContrast";
            this.nudContrast.Size = new System.Drawing.Size(55, 21);
            this.nudContrast.TabIndex = 1;
            this.nudContrast.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "对比度:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.lblModelResult);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.lblOutputCol);
            this.groupBox3.Controls.Add(this.btnCheck);
            this.groupBox3.Controls.Add(this.lblOutputRow);
            this.groupBox3.Controls.Add(this.lblModelCol);
            this.groupBox3.Controls.Add(this.lblModelRow);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.lblModelAngle);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.lblModelScore);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(322, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox3, 2);
            this.groupBox3.Size = new System.Drawing.Size(148, 200);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "模板检测";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(7, 154);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(77, 12);
            this.label27.TabIndex = 9;
            this.label27.Text = "输出点位置：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 110);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 12);
            this.label20.TabIndex = 9;
            this.label20.Text = "模板中心位置：";
            // 
            // lblModelResult
            // 
            this.lblModelResult.AutoSize = true;
            this.lblModelResult.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblModelResult.Location = new System.Drawing.Point(68, 58);
            this.lblModelResult.Name = "lblModelResult";
            this.lblModelResult.Size = new System.Drawing.Size(31, 14);
            this.lblModelResult.TabIndex = 2;
            this.lblModelResult.Text = "NUL";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "检测结果：";
            // 
            // lblOutputCol
            // 
            this.lblOutputCol.AutoSize = true;
            this.lblOutputCol.Location = new System.Drawing.Point(85, 170);
            this.lblOutputCol.Name = "lblOutputCol";
            this.lblOutputCol.Size = new System.Drawing.Size(11, 12);
            this.lblOutputCol.TabIndex = 8;
            this.lblOutputCol.Text = "0";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(21, 20);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(94, 30);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "检测";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblOutputRow
            // 
            this.lblOutputRow.AutoSize = true;
            this.lblOutputRow.Location = new System.Drawing.Point(22, 170);
            this.lblOutputRow.Name = "lblOutputRow";
            this.lblOutputRow.Size = new System.Drawing.Size(11, 12);
            this.lblOutputRow.TabIndex = 8;
            this.lblOutputRow.Text = "0";
            // 
            // lblModelCol
            // 
            this.lblModelCol.AutoSize = true;
            this.lblModelCol.Location = new System.Drawing.Point(85, 127);
            this.lblModelCol.Name = "lblModelCol";
            this.lblModelCol.Size = new System.Drawing.Size(11, 12);
            this.lblModelCol.TabIndex = 8;
            this.lblModelCol.Text = "0";
            // 
            // lblModelRow
            // 
            this.lblModelRow.AutoSize = true;
            this.lblModelRow.Location = new System.Drawing.Point(22, 127);
            this.lblModelRow.Name = "lblModelRow";
            this.lblModelRow.Size = new System.Drawing.Size(11, 12);
            this.lblModelRow.TabIndex = 8;
            this.lblModelRow.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(75, 82);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 8;
            this.label19.Text = "角度：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "得分：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(69, 170);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 8;
            this.label24.Text = "C:";
            // 
            // lblModelAngle
            // 
            this.lblModelAngle.AutoSize = true;
            this.lblModelAngle.Location = new System.Drawing.Point(114, 83);
            this.lblModelAngle.Name = "lblModelAngle";
            this.lblModelAngle.Size = new System.Drawing.Size(11, 12);
            this.lblModelAngle.TabIndex = 8;
            this.lblModelAngle.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(9, 170);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 8;
            this.label23.Text = "R:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(69, 127);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 12);
            this.label21.TabIndex = 8;
            this.label21.Text = "C:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 8;
            this.label16.Text = "R:";
            // 
            // lblModelScore
            // 
            this.lblModelScore.AutoSize = true;
            this.lblModelScore.Location = new System.Drawing.Point(46, 82);
            this.lblModelScore.Name = "lblModelScore";
            this.lblModelScore.Size = new System.Drawing.Size(11, 12);
            this.lblModelScore.TabIndex = 8;
            this.lblModelScore.Text = "0";
            // 
            // UCMakeModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCMakeModel";
            this.Size = new System.Drawing.Size(470, 200);
            this.Load += new System.EventHandler(this.UCMakeModel_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOutputRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetContour;
        private System.Windows.Forms.NumericUpDown nudContrast;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateModel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.NumericUpDown nudOutputCol;
        private System.Windows.Forms.NumericUpDown nudOutputRow;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbOutput;
        private System.Windows.Forms.NumericUpDown nudMinScore;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.Label lblCol2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCol1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRow2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRow1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnRegion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblModelResult;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblOutputCol;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblOutputRow;
        private System.Windows.Forms.Label lblModelCol;
        private System.Windows.Forms.Label lblModelRow;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblModelAngle;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblModelScore;
        private System.Windows.Forms.Label lblOrginCol;
        private System.Windows.Forms.Label lblOrginRow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
