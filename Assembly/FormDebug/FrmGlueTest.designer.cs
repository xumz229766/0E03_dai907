namespace Assembly
{
    partial class FrmGlueTest
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.nudStartAngle = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.nudStopAngle = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.nudAngleZStop = new System.Windows.Forms.NumericUpDown();
            this.nudDistZStop = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudShotNum = new System.Windows.Forms.NumericUpDown();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngleZStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistZStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShotNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 213);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 41);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始点胶测试";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(164, 213);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 41);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 195);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.91878F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.03046F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.05076F));
            this.tableLayoutPanel2.Controls.Add(this.nudStartAngle, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.nudStopAngle, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label20, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label21, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.nudAngleZStop, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.nudDistZStop, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.nudShotNum, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.nudAngle, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(251, 189);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // nudStartAngle
            // 
            this.nudStartAngle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudStartAngle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudStartAngle.Location = new System.Drawing.Point(75, 34);
            this.nudStartAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudStartAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.nudStartAngle.Name = "nudStartAngle";
            this.nudStartAngle.Size = new System.Drawing.Size(120, 26);
            this.nudStartAngle.TabIndex = 1;
            this.nudStartAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudStartAngle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudStartAngle.ValueChanged += new System.EventHandler(this.nudStartAngle_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.LightGray;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 31);
            this.label11.TabIndex = 0;
            this.label11.Text = "点胶旋转总角度：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.LightGray;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(0, 62);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 31);
            this.label18.TabIndex = 0;
            this.label18.Text = "提前断胶角度：";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudStopAngle
            // 
            this.nudStopAngle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudStopAngle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudStopAngle.Location = new System.Drawing.Point(75, 65);
            this.nudStopAngle.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.nudStopAngle.Name = "nudStopAngle";
            this.nudStopAngle.Size = new System.Drawing.Size(120, 26);
            this.nudStopAngle.TabIndex = 1;
            this.nudStopAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudStopAngle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudStopAngle.ValueChanged += new System.EventHandler(this.nudStopAngle_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.LightGray;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(0, 93);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 31);
            this.label20.TabIndex = 0;
            this.label20.Text = "点胶Z轴提前上升角度：";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.LightGray;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Location = new System.Drawing.Point(0, 124);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 31);
            this.label21.TabIndex = 0;
            this.label21.Text = "Z轴提前上升距离：";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudAngleZStop
            // 
            this.nudAngleZStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudAngleZStop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudAngleZStop.Location = new System.Drawing.Point(75, 96);
            this.nudAngleZStop.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.nudAngleZStop.Name = "nudAngleZStop";
            this.nudAngleZStop.Size = new System.Drawing.Size(120, 26);
            this.nudAngleZStop.TabIndex = 1;
            this.nudAngleZStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAngleZStop.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAngleZStop.ValueChanged += new System.EventHandler(this.nudAngleZStop_ValueChanged);
            // 
            // nudDistZStop
            // 
            this.nudDistZStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudDistZStop.DecimalPlaces = 2;
            this.nudDistZStop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudDistZStop.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDistZStop.Location = new System.Drawing.Point(75, 127);
            this.nudDistZStop.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDistZStop.Name = "nudDistZStop";
            this.nudDistZStop.Size = new System.Drawing.Size(120, 26);
            this.nudDistZStop.TabIndex = 1;
            this.nudDistZStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDistZStop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDistZStop.ValueChanged += new System.EventHandler(this.nudDistZStop_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 155);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 34);
            this.label2.TabIndex = 0;
            this.label2.Text = "点胶方向：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudShotNum
            // 
            this.nudShotNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudShotNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudShotNum.Location = new System.Drawing.Point(75, 159);
            this.nudShotNum.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudShotNum.Name = "nudShotNum";
            this.nudShotNum.Size = new System.Drawing.Size(120, 26);
            this.nudShotNum.TabIndex = 1;
            this.nudShotNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudShotNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudShotNum.ValueChanged += new System.EventHandler(this.nudShotNum_ValueChanged);
            // 
            // nudAngle
            // 
            this.nudAngle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudAngle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudAngle.Location = new System.Drawing.Point(75, 3);
            this.nudAngle.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(120, 26);
            this.nudAngle.TabIndex = 1;
            this.nudAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudAngle.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightGray;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(0, 31);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 31);
            this.label10.TabIndex = 0;
            this.label10.Text = "点胶起始角度：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmGlueTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 279);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmGlueTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "点胶测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCalibTest_FormClosing);
            this.Load += new System.EventHandler(this.FrmCalibTest_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngleZStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistZStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShotNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudStartAngle;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudStopAngle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudAngleZStop;
        private System.Windows.Forms.NumericUpDown nudDistZStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudShotNum;
    }
}