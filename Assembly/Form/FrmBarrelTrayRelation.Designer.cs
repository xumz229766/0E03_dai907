namespace Assembly
{
    partial class FrmBarrelTrayRelation
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
            this.nudSuction = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.barrelListTray1 = new Assembly.BarrelListTray();
            this.btnTraySet2 = new System.Windows.Forms.Button();
            this.btnTraySet1 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSuction)).BeginInit();
            this.SuspendLayout();
            // 
            // nudSuction
            // 
            this.nudSuction.Location = new System.Drawing.Point(88, 29);
            this.nudSuction.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSuction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSuction.Name = "nudSuction";
            this.nudSuction.Size = new System.Drawing.Size(53, 21);
            this.nudSuction.TabIndex = 5;
            this.nudSuction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "吸笔选择：";
            // 
            // barrelListTray1
            // 
            this.barrelListTray1.Location = new System.Drawing.Point(174, 12);
            this.barrelListTray1.Name = "barrelListTray1";
            this.barrelListTray1.Size = new System.Drawing.Size(179, 317);
            this.barrelListTray1.TabIndex = 6;
            // 
            // btnTraySet2
            // 
            this.btnTraySet2.Location = new System.Drawing.Point(88, 118);
            this.btnTraySet2.Name = "btnTraySet2";
            this.btnTraySet2.Size = new System.Drawing.Size(80, 23);
            this.btnTraySet2.TabIndex = 9;
            this.btnTraySet2.Text = "盘形设定2";
            this.btnTraySet2.UseVisualStyleBackColor = true;
            this.btnTraySet2.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet1
            // 
            this.btnTraySet1.Location = new System.Drawing.Point(88, 76);
            this.btnTraySet1.Name = "btnTraySet1";
            this.btnTraySet1.Size = new System.Drawing.Size(80, 23);
            this.btnTraySet1.TabIndex = 10;
            this.btnTraySet1.Text = "盘形设定1";
            this.btnTraySet1.UseVisualStyleBackColor = true;
            this.btnTraySet1.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(19, 122);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 16);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "托盘2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "托盘1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 49);
            this.label2.TabIndex = 11;
            this.label2.Text = "注:\r\n吸笔1代表取空镜筒的吸笔;\r\n吸笔2代表放成品的吸笔.";
            // 
            // FrmBarrelTrayRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 341);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTraySet2);
            this.Controls.Add(this.btnTraySet1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.barrelListTray1);
            this.Controls.Add(this.nudSuction);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBarrelTrayRelation";
            this.Text = "镜筒托盘设定";
            this.Load += new System.EventHandler(this.FrmBarrelTrayRelation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSuction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudSuction;
        private System.Windows.Forms.Label label1;
        private BarrelListTray barrelListTray1;
        private System.Windows.Forms.Button btnTraySet2;
        private System.Windows.Forms.Button btnTraySet1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
    }
}