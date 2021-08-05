namespace Tray
{
    partial class TestTray
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRow = new System.Windows.Forms.NumericUpDown();
            this.nudCol = new System.Windows.Forms.NumericUpDown();
            this.btnCreate = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbUnregular2 = new System.Windows.Forms.CheckBox();
            this.cbUnregular1 = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbId = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chShow = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbDirect = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRowType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 468);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "行：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "列：";
            // 
            // nudRow
            // 
            this.nudRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRow.Location = new System.Drawing.Point(16, 159);
            this.nudRow.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudRow.Name = "nudRow";
            this.nudRow.Size = new System.Drawing.Size(94, 21);
            this.nudRow.TabIndex = 3;
            this.nudRow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudCol
            // 
            this.nudCol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudCol.Location = new System.Drawing.Point(16, 212);
            this.nudCol.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudCol.Name = "nudCol";
            this.nudCol.Size = new System.Drawing.Size(94, 21);
            this.nudCol.TabIndex = 3;
            this.nudCol.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(22, 239);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(80, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "创建托盘";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(25, 344);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "添加屏蔽";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(25, 380);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "移除屏蔽";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(702, 523);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbUnregular2);
            this.panel2.Controls.Add(this.cbUnregular1);
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cmbId);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.nudRow);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnCreate);
            this.panel2.Controls.Add(this.nudCol);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(561, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 468);
            this.panel2.TabIndex = 1;
            // 
            // cbUnregular2
            // 
            this.cbUnregular2.AutoSize = true;
            this.cbUnregular2.Location = new System.Drawing.Point(25, 313);
            this.cbUnregular2.Name = "cbUnregular2";
            this.cbUnregular2.Size = new System.Drawing.Size(54, 16);
            this.cbUnregular2.TabIndex = 11;
            this.cbUnregular2.Text = "异形2";
            this.cbUnregular2.UseVisualStyleBackColor = true;
            this.cbUnregular2.CheckedChanged += new System.EventHandler(this.cbUnregular2_CheckedChanged);
            // 
            // cbUnregular1
            // 
            this.cbUnregular1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnregular1.AutoSize = true;
            this.cbUnregular1.Location = new System.Drawing.Point(25, 278);
            this.cbUnregular1.Name = "cbUnregular1";
            this.cbUnregular1.Size = new System.Drawing.Size(54, 16);
            this.cbUnregular1.TabIndex = 11;
            this.cbUnregular1.Text = "异形1";
            this.cbUnregular1.UseVisualStyleBackColor = true;
            this.cbUnregular1.CheckedChanged += new System.EventHandler(this.cbUnregular1_CheckedChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(76, 35);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(48, 23);
            this.btnSelect.TabIndex = 10;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(16, 100);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(94, 21);
            this.txtName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "托盘名称：";
            // 
            // cmbId
            // 
            this.cmbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbId.FormattingEnabled = true;
            this.cmbId.Location = new System.Drawing.Point(16, 37);
            this.cmbId.Name = "cmbId";
            this.cmbId.Size = new System.Drawing.Size(47, 20);
            this.cmbId.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "托盘序号：";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tableLayoutPanel2.Controls.Add(this.chShow, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 470);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(559, 52);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // chShow
            // 
            this.chShow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chShow.AutoSize = true;
            this.chShow.Location = new System.Drawing.Point(10, 18);
            this.chShow.Name = "chShow";
            this.chShow.Size = new System.Drawing.Size(72, 16);
            this.chShow.TabIndex = 0;
            this.chShow.Text = "预览显示";
            this.chShow.UseVisualStyleBackColor = true;
            this.chShow.CheckedChanged += new System.EventHandler(this.chShow_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbRowType);
            this.panel3.Controls.Add(this.cmbDirect);
            this.panel3.Controls.Add(this.btnConfirm);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cmbStart);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(93, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(466, 52);
            this.panel3.TabIndex = 1;
            // 
            // cmbDirect
            // 
            this.cmbDirect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirect.FormattingEnabled = true;
            this.cmbDirect.Items.AddRange(new object[] {
            "行",
            "列"});
            this.cmbDirect.Location = new System.Drawing.Point(192, 18);
            this.cmbDirect.Name = "cmbDirect";
            this.cmbDirect.Size = new System.Drawing.Size(59, 20);
            this.cmbDirect.TabIndex = 7;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(390, 11);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(62, 33);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "起始位置：";
            // 
            // cmbStart
            // 
            this.cmbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStart.FormattingEnabled = true;
            this.cmbStart.Items.AddRange(new object[] {
            "左上角",
            "左下角",
            "右上角",
            "右下角"});
            this.cmbStart.Location = new System.Drawing.Point(75, 18);
            this.cmbStart.Name = "cmbStart";
            this.cmbStart.Size = new System.Drawing.Size(73, 20);
            this.cmbStart.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(154, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "方向：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(593, 477);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 38);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存参数";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "换行：";
            // 
            // cmbRowType
            // 
            this.cmbRowType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRowType.FormattingEnabled = true;
            this.cmbRowType.Items.AddRange(new object[] {
            "Z型",
            "S型"});
            this.cmbRowType.Location = new System.Drawing.Point(310, 18);
            this.cmbRowType.Name = "cmbRowType";
            this.cmbRowType.Size = new System.Drawing.Size(59, 20);
            this.cmbRowType.TabIndex = 7;
            // 
            // TestTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 523);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestTray";
            this.Text = "托盘设置";
            this.Load += new System.EventHandler(this.TestTray_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCol)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRow;
        private System.Windows.Forms.NumericUpDown nudCol;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox chShow;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox cmbDirect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.CheckBox cbUnregular2;
        private System.Windows.Forms.CheckBox cbUnregular1;
        private System.Windows.Forms.ComboBox cmbRowType;
        private System.Windows.Forms.Label label7;


    }
}