namespace ImageProcess
{
    partial class FrmProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProcess));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolReadImg = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFitWindow = new System.Windows.Forms.ToolStripButton();
            this.toolErase = new System.Windows.Forms.ToolStripButton();
            this.toolStripSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolShowRow = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolShowCol = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolRunTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbExist = new System.Windows.Forms.ComboBox();
            this.cmbCenterOutput = new System.Windows.Forms.ComboBox();
            this.cmbAngleOutput = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gpRegionArea = new System.Windows.Forms.GroupBox();
            this.btn_RAreaTest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRAreaResult = new System.Windows.Forms.Label();
            this.gpRegionAngle = new System.Windows.Forms.GroupBox();
            this.btnRATest = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.lblRAResult = new System.Windows.Forms.Label();
            this.gpMeasureCircle = new System.Windows.Forms.GroupBox();
            this.btnMCircleSet = new System.Windows.Forms.Button();
            this.lblMCResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpModelSet = new System.Windows.Forms.GroupBox();
            this.lblModelResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModelSet = new System.Windows.Forms.Button();
            this.cbRegionArea = new System.Windows.Forms.CheckBox();
            this.cbRegionAngle = new System.Windows.Forms.CheckBox();
            this.cbMeasureCircle = new System.Windows.Forms.CheckBox();
            this.cbMakeModel = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.PanelSet = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblExistResult = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblOutCol = new System.Windows.Forms.Label();
            this.lblResultCMth = new System.Windows.Forms.Label();
            this.lblResultAMth = new System.Windows.Forms.Label();
            this.lblOutRow = new System.Windows.Forms.Label();
            this.lblResultAngle = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpRegionArea.SuspendLayout();
            this.gpRegionAngle.SuspendLayout();
            this.gpMeasureCircle.SuspendLayout();
            this.gpModelSet.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1259, 980);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(200, 980);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnSelect);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.lstItems);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Size = new System.Drawing.Size(200, 980);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试项";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(107, 927);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 45);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Location = new System.Drawing.Point(107, 876);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(89, 45);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(4, 927);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 45);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(4, 876);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(95, 45);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lstItems
            // 
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstItems.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 20;
            this.lstItems.Location = new System.Drawing.Point(8, 25);
            this.lstItems.Margin = new System.Windows.Forms.Padding(0);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(183, 824);
            this.lstItems.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.hWindowControl1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(200, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 660);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(0, 31);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(0);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(792, 604);
            this.hWindowControl1.TabIndex = 1;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(792, 604);
            this.hWindowControl1.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseMove);
            this.hWindowControl1.HMouseDown += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseDown);
            this.hWindowControl1.HMouseWheel += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseWheel);
            this.hWindowControl1.MouseEnter += new System.EventHandler(this.hWindowControl1_MouseEnter);
            this.hWindowControl1.Resize += new System.EventHandler(this.hWindowControl1_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Enabled = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolReadImg,
            this.toolStripSeparator1,
            this.toolFitWindow,
            this.toolErase,
            this.toolStripSize,
            this.toolRemove,
            this.toolStripSeparator2,
            this.toolStripButton1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 28);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolReadImg
            // 
            this.toolReadImg.Image = global::ImageProcess.Properties.Resources.ic_action_picture;
            this.toolReadImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReadImg.Name = "toolReadImg";
            this.toolReadImg.Size = new System.Drawing.Size(89, 24);
            this.toolReadImg.Text = "选择图片";
            this.toolReadImg.Click += new System.EventHandler(this.toolReadImg_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolFitWindow
            // 
            this.toolFitWindow.BackColor = System.Drawing.Color.SkyBlue;
            this.toolFitWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolFitWindow.Image = ((System.Drawing.Image)(resources.GetObject("toolFitWindow.Image")));
            this.toolFitWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFitWindow.Name = "toolFitWindow";
            this.toolFitWindow.Size = new System.Drawing.Size(73, 24);
            this.toolFitWindow.Text = "适应窗口";
            this.toolFitWindow.Click += new System.EventHandler(this.toolFitWindow_Click);
            // 
            // toolErase
            // 
            this.toolErase.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolErase.Enabled = false;
            this.toolErase.Image = ((System.Drawing.Image)(resources.GetObject("toolErase.Image")));
            this.toolErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolErase.Name = "toolErase";
            this.toolErase.Size = new System.Drawing.Size(77, 24);
            this.toolErase.Text = "模板擦除:";
            this.toolErase.Click += new System.EventHandler(this.toolErase_Click);
            // 
            // toolStripSize
            // 
            this.toolStripSize.AutoCompleteCustomSource.AddRange(new string[] {
            "3",
            "5",
            "11",
            "15",
            "21",
            "50",
            "75",
            "100"});
            this.toolStripSize.Enabled = false;
            this.toolStripSize.Items.AddRange(new object[] {
            "3",
            "5",
            "11",
            "15",
            "21",
            "50",
            "75",
            "100"});
            this.toolStripSize.Name = "toolStripSize";
            this.toolStripSize.Size = new System.Drawing.Size(99, 28);
            this.toolStripSize.Text = "3";
            // 
            // toolRemove
            // 
            this.toolRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolRemove.Enabled = false;
            this.toolRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolRemove.Image")));
            this.toolRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRemove.Name = "toolRemove";
            this.toolRemove.Size = new System.Drawing.Size(73, 24);
            this.toolRemove.Text = "清空擦除";
            this.toolRemove.Click += new System.EventHandler(this.toolRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(20, 23);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.Image = global::ImageProcess.Properties.Resources.ic_action_io1;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton1.Text = "运行";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolShowRow,
            this.toolStripStatusLabel2,
            this.toolShowCol,
            this.toolStripStatusLabel3,
            this.toolRunTime,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(792, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 19);
            this.toolStripStatusLabel1.Text = "Row:";
            // 
            // toolShowRow
            // 
            this.toolShowRow.AutoSize = false;
            this.toolShowRow.Name = "toolShowRow";
            this.toolShowRow.Size = new System.Drawing.Size(50, 19);
            this.toolShowRow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolShowRow_MouseMove);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(32, 19);
            this.toolStripStatusLabel2.Text = "Col:";
            // 
            // toolShowCol
            // 
            this.toolShowCol.AutoSize = false;
            this.toolShowCol.Name = "toolShowCol";
            this.toolShowCol.Size = new System.Drawing.Size(50, 19);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(87, 19);
            this.toolStripStatusLabel3.Text = "ElapsedTime:";
            // 
            // toolRunTime
            // 
            this.toolRunTime.AutoSize = false;
            this.toolRunTime.Name = "toolRunTime";
            this.toolRunTime.Size = new System.Drawing.Size(40, 19);
            this.toolRunTime.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(27, 19);
            this.toolStripStatusLabel5.Text = "ms";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbExist);
            this.groupBox2.Controls.Add(this.cmbCenterOutput);
            this.groupBox2.Controls.Add(this.cmbAngleOutput);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.gpRegionArea);
            this.groupBox2.Controls.Add(this.gpRegionAngle);
            this.groupBox2.Controls.Add(this.gpMeasureCircle);
            this.groupBox2.Controls.Add(this.gpModelSet);
            this.groupBox2.Controls.Add(this.cbRegionArea);
            this.groupBox2.Controls.Add(this.cbRegionAngle);
            this.groupBox2.Controls.Add(this.cbMeasureCircle);
            this.groupBox2.Controls.Add(this.cbMakeModel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(992, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(267, 545);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检测方法";
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // cmbExist
            // 
            this.cmbExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExist.FormattingEnabled = true;
            this.cmbExist.Location = new System.Drawing.Point(109, 485);
            this.cmbExist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbExist.Name = "cmbExist";
            this.cmbExist.Size = new System.Drawing.Size(152, 33);
            this.cmbExist.TabIndex = 3;
            // 
            // cmbCenterOutput
            // 
            this.cmbCenterOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCenterOutput.FormattingEnabled = true;
            this.cmbCenterOutput.Location = new System.Drawing.Point(109, 440);
            this.cmbCenterOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCenterOutput.Name = "cmbCenterOutput";
            this.cmbCenterOutput.Size = new System.Drawing.Size(152, 33);
            this.cmbCenterOutput.TabIndex = 3;
            // 
            // cmbAngleOutput
            // 
            this.cmbAngleOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAngleOutput.FormattingEnabled = true;
            this.cmbAngleOutput.Location = new System.Drawing.Point(109, 395);
            this.cmbAngleOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAngleOutput.Name = "cmbAngleOutput";
            this.cmbAngleOutput.Size = new System.Drawing.Size(152, 33);
            this.cmbAngleOutput.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 491);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "有无检测：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 449);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "中心检测：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 404);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "角度检测：";
            // 
            // gpRegionArea
            // 
            this.gpRegionArea.Controls.Add(this.btn_RAreaTest);
            this.gpRegionArea.Controls.Add(this.label3);
            this.gpRegionArea.Controls.Add(this.lblRAreaResult);
            this.gpRegionArea.Enabled = false;
            this.gpRegionArea.Location = new System.Drawing.Point(15, 321);
            this.gpRegionArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpRegionArea.Name = "gpRegionArea";
            this.gpRegionArea.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpRegionArea.Size = new System.Drawing.Size(252, 61);
            this.gpRegionArea.TabIndex = 1;
            this.gpRegionArea.TabStop = false;
            this.gpRegionArea.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // btn_RAreaTest
            // 
            this.btn_RAreaTest.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_RAreaTest.Image = global::ImageProcess.Properties.Resources.ic_action_gear;
            this.btn_RAreaTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_RAreaTest.Location = new System.Drawing.Point(8, 12);
            this.btn_RAreaTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_RAreaTest.Name = "btn_RAreaTest";
            this.btn_RAreaTest.Size = new System.Drawing.Size(101, 38);
            this.btn_RAreaTest.TabIndex = 2;
            this.btn_RAreaTest.Text = "设置";
            this.btn_RAreaTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_RAreaTest.UseVisualStyleBackColor = true;
            this.btn_RAreaTest.Click += new System.EventHandler(this.btn_RAreaTest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label3.Location = new System.Drawing.Point(127, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "结果:";
            // 
            // lblRAreaResult
            // 
            this.lblRAreaResult.AutoSize = true;
            this.lblRAreaResult.Location = new System.Drawing.Point(175, 20);
            this.lblRAreaResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRAreaResult.Name = "lblRAreaResult";
            this.lblRAreaResult.Size = new System.Drawing.Size(49, 25);
            this.lblRAreaResult.TabIndex = 5;
            this.lblRAreaResult.Text = "Null";
            // 
            // gpRegionAngle
            // 
            this.gpRegionAngle.Controls.Add(this.btnRATest);
            this.gpRegionAngle.Controls.Add(this.label19);
            this.gpRegionAngle.Controls.Add(this.lblRAResult);
            this.gpRegionAngle.Enabled = false;
            this.gpRegionAngle.Location = new System.Drawing.Point(11, 225);
            this.gpRegionAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpRegionAngle.Name = "gpRegionAngle";
            this.gpRegionAngle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpRegionAngle.Size = new System.Drawing.Size(252, 61);
            this.gpRegionAngle.TabIndex = 1;
            this.gpRegionAngle.TabStop = false;
            this.gpRegionAngle.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // btnRATest
            // 
            this.btnRATest.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRATest.Image = global::ImageProcess.Properties.Resources.ic_action_gear;
            this.btnRATest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRATest.Location = new System.Drawing.Point(8, 12);
            this.btnRATest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRATest.Name = "btnRATest";
            this.btnRATest.Size = new System.Drawing.Size(101, 38);
            this.btnRATest.TabIndex = 2;
            this.btnRATest.Text = "设置";
            this.btnRATest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRATest.UseVisualStyleBackColor = true;
            this.btnRATest.Click += new System.EventHandler(this.btnRATest_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label19.Location = new System.Drawing.Point(127, 21);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "结果:";
            // 
            // lblRAResult
            // 
            this.lblRAResult.AutoSize = true;
            this.lblRAResult.Location = new System.Drawing.Point(175, 20);
            this.lblRAResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRAResult.Name = "lblRAResult";
            this.lblRAResult.Size = new System.Drawing.Size(49, 25);
            this.lblRAResult.TabIndex = 5;
            this.lblRAResult.Text = "Null";
            // 
            // gpMeasureCircle
            // 
            this.gpMeasureCircle.Controls.Add(this.btnMCircleSet);
            this.gpMeasureCircle.Controls.Add(this.lblMCResult);
            this.gpMeasureCircle.Controls.Add(this.label2);
            this.gpMeasureCircle.Enabled = false;
            this.gpMeasureCircle.Location = new System.Drawing.Point(4, 138);
            this.gpMeasureCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpMeasureCircle.Name = "gpMeasureCircle";
            this.gpMeasureCircle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpMeasureCircle.Size = new System.Drawing.Size(252, 60);
            this.gpMeasureCircle.TabIndex = 1;
            this.gpMeasureCircle.TabStop = false;
            this.gpMeasureCircle.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // btnMCircleSet
            // 
            this.btnMCircleSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMCircleSet.Image = global::ImageProcess.Properties.Resources.ic_action_gear;
            this.btnMCircleSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMCircleSet.Location = new System.Drawing.Point(12, 12);
            this.btnMCircleSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMCircleSet.Name = "btnMCircleSet";
            this.btnMCircleSet.Size = new System.Drawing.Size(101, 40);
            this.btnMCircleSet.TabIndex = 2;
            this.btnMCircleSet.Text = "设置";
            this.btnMCircleSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMCircleSet.UseVisualStyleBackColor = true;
            this.btnMCircleSet.Click += new System.EventHandler(this.btnMCircleSet_Click);
            // 
            // lblMCResult
            // 
            this.lblMCResult.AutoSize = true;
            this.lblMCResult.Location = new System.Drawing.Point(176, 20);
            this.lblMCResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMCResult.Name = "lblMCResult";
            this.lblMCResult.Size = new System.Drawing.Size(49, 25);
            this.lblMCResult.TabIndex = 5;
            this.lblMCResult.Text = "Null";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.Location = new System.Drawing.Point(131, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "结果:";
            // 
            // gpModelSet
            // 
            this.gpModelSet.Controls.Add(this.lblModelResult);
            this.gpModelSet.Controls.Add(this.label1);
            this.gpModelSet.Controls.Add(this.btnModelSet);
            this.gpModelSet.Enabled = false;
            this.gpModelSet.Location = new System.Drawing.Point(4, 49);
            this.gpModelSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpModelSet.Name = "gpModelSet";
            this.gpModelSet.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpModelSet.Size = new System.Drawing.Size(252, 59);
            this.gpModelSet.TabIndex = 1;
            this.gpModelSet.TabStop = false;
            this.gpModelSet.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // lblModelResult
            // 
            this.lblModelResult.AutoSize = true;
            this.lblModelResult.Location = new System.Drawing.Point(176, 21);
            this.lblModelResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModelResult.Name = "lblModelResult";
            this.lblModelResult.Size = new System.Drawing.Size(49, 25);
            this.lblModelResult.TabIndex = 5;
            this.lblModelResult.Text = "Null";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.Location = new System.Drawing.Point(131, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "结果:";
            // 
            // btnModelSet
            // 
            this.btnModelSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModelSet.Image = global::ImageProcess.Properties.Resources.ic_action_gear;
            this.btnModelSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelSet.Location = new System.Drawing.Point(12, 12);
            this.btnModelSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModelSet.Name = "btnModelSet";
            this.btnModelSet.Size = new System.Drawing.Size(101, 40);
            this.btnModelSet.TabIndex = 2;
            this.btnModelSet.Text = "设置";
            this.btnModelSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModelSet.UseVisualStyleBackColor = true;
            this.btnModelSet.Click += new System.EventHandler(this.btnModelSet_Click);
            // 
            // cbRegionArea
            // 
            this.cbRegionArea.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cbRegionArea.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRegionArea.Location = new System.Drawing.Point(7, 294);
            this.cbRegionArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRegionArea.Name = "cbRegionArea";
            this.cbRegionArea.Size = new System.Drawing.Size(256, 30);
            this.cbRegionArea.TabIndex = 0;
            this.cbRegionArea.Text = "区域_面积";
            this.cbRegionArea.UseVisualStyleBackColor = false;
            this.cbRegionArea.CheckedChanged += new System.EventHandler(this.cbRegionArea_CheckedChanged);
            // 
            // cbRegionAngle
            // 
            this.cbRegionAngle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cbRegionAngle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRegionAngle.Location = new System.Drawing.Point(3, 198);
            this.cbRegionAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRegionAngle.Name = "cbRegionAngle";
            this.cbRegionAngle.Size = new System.Drawing.Size(256, 30);
            this.cbRegionAngle.TabIndex = 0;
            this.cbRegionAngle.Text = "区域_环";
            this.cbRegionAngle.UseVisualStyleBackColor = false;
            this.cbRegionAngle.CheckedChanged += new System.EventHandler(this.cbRegionAngle_CheckedChanged);
            // 
            // cbMeasureCircle
            // 
            this.cbMeasureCircle.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cbMeasureCircle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMeasureCircle.Location = new System.Drawing.Point(4, 109);
            this.cbMeasureCircle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMeasureCircle.Name = "cbMeasureCircle";
            this.cbMeasureCircle.Size = new System.Drawing.Size(256, 30);
            this.cbMeasureCircle.TabIndex = 0;
            this.cbMeasureCircle.Text = "测量_圆";
            this.cbMeasureCircle.UseVisualStyleBackColor = false;
            this.cbMeasureCircle.CheckedChanged += new System.EventHandler(this.chMeasureCircle_CheckedChanged);
            // 
            // cbMakeModel
            // 
            this.cbMakeModel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cbMakeModel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbMakeModel.Location = new System.Drawing.Point(4, 24);
            this.cbMakeModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMakeModel.Name = "cbMakeModel";
            this.cbMakeModel.Size = new System.Drawing.Size(256, 30);
            this.cbMakeModel.TabIndex = 0;
            this.cbMakeModel.Text = "定位_模板匹配";
            this.cbMakeModel.UseVisualStyleBackColor = false;
            this.cbMakeModel.CheckedChanged += new System.EventHandler(this.cbMakeModel_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PanelSet);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(200, 660);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(792, 320);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "参数设置";
            this.groupBox5.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // PanelSet
            // 
            this.PanelSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSet.Location = new System.Drawing.Point(4, 28);
            this.PanelSet.Margin = new System.Windows.Forms.Padding(0);
            this.PanelSet.Name = "PanelSet";
            this.PanelSet.Size = new System.Drawing.Size(784, 288);
            this.PanelSet.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblExistResult);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lblOutCol);
            this.panel2.Controls.Add(this.lblResultCMth);
            this.panel2.Controls.Add(this.lblResultAMth);
            this.panel2.Controls.Add(this.lblOutRow);
            this.panel2.Controls.Add(this.lblResultAngle);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblResult);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(996, 664);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(259, 312);
            this.panel2.TabIndex = 4;
            // 
            // lblExistResult
            // 
            this.lblExistResult.BackColor = System.Drawing.Color.Yellow;
            this.lblExistResult.Location = new System.Drawing.Point(104, 282);
            this.lblExistResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistResult.Name = "lblExistResult";
            this.lblExistResult.Size = new System.Drawing.Size(71, 21);
            this.lblExistResult.TabIndex = 6;
            this.lblExistResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 285);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 15);
            this.label11.TabIndex = 5;
            this.label11.Text = "有无检测:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 254);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 15);
            this.label17.TabIndex = 4;
            this.label17.Text = "检测方法:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(33, 220);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "输出列:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 144);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "检测方法:";
            // 
            // lblOutCol
            // 
            this.lblOutCol.AutoSize = true;
            this.lblOutCol.Location = new System.Drawing.Point(103, 220);
            this.lblOutCol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutCol.Name = "lblOutCol";
            this.lblOutCol.Size = new System.Drawing.Size(15, 15);
            this.lblOutCol.TabIndex = 3;
            this.lblOutCol.Text = "0";
            // 
            // lblResultCMth
            // 
            this.lblResultCMth.Location = new System.Drawing.Point(104, 250);
            this.lblResultCMth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultCMth.Name = "lblResultCMth";
            this.lblResultCMth.Size = new System.Drawing.Size(120, 22);
            this.lblResultCMth.TabIndex = 3;
            this.lblResultCMth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResultAMth
            // 
            this.lblResultAMth.Location = new System.Drawing.Point(103, 142);
            this.lblResultAMth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultAMth.Name = "lblResultAMth";
            this.lblResultAMth.Size = new System.Drawing.Size(120, 22);
            this.lblResultAMth.TabIndex = 3;
            this.lblResultAMth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOutRow
            // 
            this.lblOutRow.AutoSize = true;
            this.lblOutRow.Location = new System.Drawing.Point(103, 184);
            this.lblOutRow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutRow.Name = "lblOutRow";
            this.lblOutRow.Size = new System.Drawing.Size(15, 15);
            this.lblOutRow.TabIndex = 3;
            this.lblOutRow.Text = "0";
            // 
            // lblResultAngle
            // 
            this.lblResultAngle.AutoSize = true;
            this.lblResultAngle.Location = new System.Drawing.Point(103, 108);
            this.lblResultAngle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultAngle.Name = "lblResultAngle";
            this.lblResultAngle.Size = new System.Drawing.Size(15, 15);
            this.lblResultAngle.TabIndex = 3;
            this.lblResultAngle.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 184);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "输出行:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 108);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "角度:";
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.SystemColors.Control;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.Location = new System.Drawing.Point(55, 52);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(108, 35);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "NULL";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "检测结果:";
            // 
            // FrmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 980);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmProcess";
            this.Text = "图像处理";
            this.Load += new System.EventHandler(this.FrmProcess_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpRegionArea.ResumeLayout(false);
            this.gpRegionArea.PerformLayout();
            this.gpRegionAngle.ResumeLayout(false);
            this.gpRegionAngle.PerformLayout();
            this.gpMeasureCircle.ResumeLayout(false);
            this.gpMeasureCircle.PerformLayout();
            this.gpModelSet.ResumeLayout(false);
            this.gpModelSet.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbMakeModel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gpModelSet;
        private System.Windows.Forms.ToolStripButton toolReadImg;
        private System.Windows.Forms.Button btnModelSet;
        private System.Windows.Forms.GroupBox gpRegionAngle;
        private System.Windows.Forms.Button btnRATest;
        private System.Windows.Forms.GroupBox gpMeasureCircle;
        private System.Windows.Forms.Button btnMCircleSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbRegionAngle;
        private System.Windows.Forms.CheckBox cbMeasureCircle;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel PanelSet;
        private System.Windows.Forms.ToolStripButton toolFitWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolShowRow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolShowCol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolErase;
        private System.Windows.Forms.ToolStripButton toolRemove;
        private System.Windows.Forms.ToolStripComboBox toolStripSize;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox cmbCenterOutput;
        private System.Windows.Forms.ComboBox cmbAngleOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblOutCol;
        private System.Windows.Forms.Label lblResultCMth;
        private System.Windows.Forms.Label lblResultAMth;
        private System.Windows.Forms.Label lblOutRow;
        private System.Windows.Forms.Label lblResultAngle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolRunTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblRAResult;
        private System.Windows.Forms.Label lblMCResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblModelResult;
        private System.Windows.Forms.ComboBox cmbExist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblExistResult;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gpRegionArea;
        private System.Windows.Forms.Button btn_RAreaTest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRAreaResult;
        private System.Windows.Forms.CheckBox cbRegionArea;
    }
}