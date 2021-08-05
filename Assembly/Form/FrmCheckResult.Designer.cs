namespace Assembly
{
    partial class FrmCheckResult
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbUseAssem = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(271, 34);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 52);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "暂 停";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(55, 34);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(160, 52);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开 始 验 证";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbUseAssem
            // 
            this.cbUseAssem.AutoSize = true;
            this.cbUseAssem.Location = new System.Drawing.Point(55, 108);
            this.cbUseAssem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbUseAssem.Name = "cbUseAssem";
            this.cbUseAssem.Size = new System.Drawing.Size(119, 19);
            this.cbUseAssem.TabIndex = 4;
            this.cbUseAssem.Text = "使用组装参数";
            this.cbUseAssem.UseVisualStyleBackColor = true;
            this.cbUseAssem.Click += new System.EventHandler(this.cbUseAssem_Click);
            // 
            // FrmCheckResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 152);
            this.Controls.Add(this.cbUseAssem);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmCheckResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "组装验证";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCheckResult_FormClosed);
            this.Load += new System.EventHandler(this.FrmCheckResult_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbUseAssem;
    }
}