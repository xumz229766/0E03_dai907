namespace Assembly
{
    partial class FrmAlarmDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblAlarmInfo = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.gpProcess = new System.Windows.Forms.GroupBox();
            this.nudNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnForceOK = new System.Windows.Forms.Button();
            this.gpProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "报警信息:";
            // 
            // lblAlarmInfo
            // 
            this.lblAlarmInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlarmInfo.Location = new System.Drawing.Point(98, 22);
            this.lblAlarmInfo.Name = "lblAlarmInfo";
            this.lblAlarmInfo.Size = new System.Drawing.Size(315, 23);
            this.lblAlarmInfo.TabIndex = 1;
            this.lblAlarmInfo.Text = "label2";
            this.lblAlarmInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(9, 67);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 38);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "报警复位";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gpProcess
            // 
            this.gpProcess.Controls.Add(this.nudNum);
            this.gpProcess.Controls.Add(this.label3);
            this.gpProcess.Controls.Add(this.lblResult);
            this.gpProcess.Controls.Add(this.label2);
            this.gpProcess.Controls.Add(this.btnNext);
            this.gpProcess.Controls.Add(this.btnForceOK);
            this.gpProcess.Controls.Add(this.btnRepeat);
            this.gpProcess.Location = new System.Drawing.Point(108, 67);
            this.gpProcess.Name = "gpProcess";
            this.gpProcess.Size = new System.Drawing.Size(305, 99);
            this.gpProcess.TabIndex = 3;
            this.gpProcess.TabStop = false;
            this.gpProcess.Text = "报警处理";
            // 
            // nudNum
            // 
            this.nudNum.Location = new System.Drawing.Point(145, 27);
            this.nudNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNum.Name = "nudNum";
            this.nudNum.Size = new System.Drawing.Size(54, 21);
            this.nudNum.TabIndex = 4;
            this.nudNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "间隔：";
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Yellow;
            this.lblResult.Location = new System.Drawing.Point(174, 65);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(106, 23);
            this.lblResult.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "处理结果:";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(205, 20);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 34);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnRepeat
            // 
            this.btnRepeat.Location = new System.Drawing.Point(6, 20);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(75, 38);
            this.btnRepeat.TabIndex = 0;
            this.btnRepeat.Text = "再来一次";
            this.btnRepeat.UseVisualStyleBackColor = false;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(9, 126);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 38);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnForceOK
            // 
            this.btnForceOK.Location = new System.Drawing.Point(6, 61);
            this.btnForceOK.Name = "btnForceOK";
            this.btnForceOK.Size = new System.Drawing.Size(75, 38);
            this.btnForceOK.TabIndex = 0;
            this.btnForceOK.Text = "强制OK";
            this.btnForceOK.UseVisualStyleBackColor = false;
            this.btnForceOK.Click += new System.EventHandler(this.btnForceOK_Click);
            // 
            // FrmAlarmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 189);
            this.Controls.Add(this.gpProcess);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblAlarmInfo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAlarmDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报警提示";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAlarmDialog_FormClosed);
            this.Load += new System.EventHandler(this.FrmAlarmDialog_Load);
            this.gpProcess.ResumeLayout(false);
            this.gpProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAlarmInfo;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox gpProcess;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.NumericUpDown nudNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnForceOK;
    }
}