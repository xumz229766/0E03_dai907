namespace Assembly
{
    partial class FrmSetMeasurePort
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetPort = new System.Windows.Forms.Button();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbBaudRate1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetPot1 = new System.Windows.Forms.Button();
            this.cbPortName1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbBaudRate2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSetPot2 = new System.Windows.Forms.Button();
            this.cbPortName2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSetPort);
            this.groupBox1.Controls.Add(this.cbPortName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测高串口设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "端口号：";
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(410, 30);
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
            this.cbPortName.Location = new System.Drawing.Point(66, 34);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(82, 20);
            this.cbPortName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "波特率：";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(261, 33);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(88, 20);
            this.cbBaudRate.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbBaudRate1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnSetPot1);
            this.groupBox2.Controls.Add(this.cbPortName1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 92);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测高1串口设置";
            // 
            // cbBaudRate1
            // 
            this.cbBaudRate1.FormattingEnabled = true;
            this.cbBaudRate1.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudRate1.Location = new System.Drawing.Point(261, 33);
            this.cbBaudRate1.Name = "cbBaudRate1";
            this.cbBaudRate1.Size = new System.Drawing.Size(88, 20);
            this.cbBaudRate1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "波特率：";
            // 
            // btnSetPot1
            // 
            this.btnSetPot1.Location = new System.Drawing.Point(410, 30);
            this.btnSetPot1.Name = "btnSetPot1";
            this.btnSetPot1.Size = new System.Drawing.Size(75, 23);
            this.btnSetPot1.TabIndex = 2;
            this.btnSetPot1.Text = "设置";
            this.btnSetPot1.UseVisualStyleBackColor = true;
            this.btnSetPot1.Click += new System.EventHandler(this.btnSetPot1_Click);
            // 
            // cbPortName1
            // 
            this.cbPortName1.FormattingEnabled = true;
            this.cbPortName1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cbPortName1.Location = new System.Drawing.Point(66, 34);
            this.cbPortName1.Name = "cbPortName1";
            this.cbPortName1.Size = new System.Drawing.Size(82, 20);
            this.cbPortName1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "端口号：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbBaudRate2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnSetPot2);
            this.groupBox3.Controls.Add(this.cbPortName2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(536, 92);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "测高2串口设置";
            // 
            // cbBaudRate2
            // 
            this.cbBaudRate2.FormattingEnabled = true;
            this.cbBaudRate2.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudRate2.Location = new System.Drawing.Point(261, 33);
            this.cbBaudRate2.Name = "cbBaudRate2";
            this.cbBaudRate2.Size = new System.Drawing.Size(88, 20);
            this.cbBaudRate2.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(202, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "波特率：";
            // 
            // btnSetPot2
            // 
            this.btnSetPot2.Location = new System.Drawing.Point(410, 30);
            this.btnSetPot2.Name = "btnSetPot2";
            this.btnSetPot2.Size = new System.Drawing.Size(75, 23);
            this.btnSetPot2.TabIndex = 2;
            this.btnSetPot2.Text = "设置";
            this.btnSetPot2.UseVisualStyleBackColor = true;
            this.btnSetPot2.Click += new System.EventHandler(this.btnSetPot2_Click);
            // 
            // cbPortName2
            // 
            this.cbPortName2.FormattingEnabled = true;
            this.cbPortName2.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cbPortName2.Location = new System.Drawing.Point(66, 34);
            this.cbPortName2.Name = "cbPortName2";
            this.cbPortName2.Size = new System.Drawing.Size(82, 20);
            this.cbPortName2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "端口号：";
            // 
            // FrmSetMeasurePort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 395);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSetMeasurePort";
            this.Text = "测高和测压串口测试";
            this.Load += new System.EventHandler(this.FrmSetMeasurePort_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetPort;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbBaudRate1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetPot1;
        private System.Windows.Forms.ComboBox cbPortName1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbBaudRate2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSetPot2;
        private System.Windows.Forms.ComboBox cbPortName2;
        private System.Windows.Forms.Label label6;
    }
}