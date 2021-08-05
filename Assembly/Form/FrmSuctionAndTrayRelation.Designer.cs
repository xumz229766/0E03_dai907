namespace Assembly
{
    partial class FrmSuctionAndTrayRelation
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
            this.showListTrayPanel1 = new Assembly.ShowListTrayPanel();
            this.rbtnStation1 = new System.Windows.Forms.RadioButton();
            this.rbtnStation2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSuction = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.btnTraySet1 = new System.Windows.Forms.Button();
            this.btnTraySet2 = new System.Windows.Forms.Button();
            this.btnTraySet3 = new System.Windows.Forms.Button();
            this.btnTraySet4 = new System.Windows.Forms.Button();
            this.btnTraySet5 = new System.Windows.Forms.Button();
            this.btnTraySet6 = new System.Windows.Forms.Button();
            this.btnTraySet7 = new System.Windows.Forms.Button();
            this.btnTraySet8 = new System.Windows.Forms.Button();
            this.btnTraySet9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSuction)).BeginInit();
            this.SuspendLayout();
            // 
            // showListTrayPanel1
            // 
            this.showListTrayPanel1.Location = new System.Drawing.Point(253, 1);
            this.showListTrayPanel1.Name = "showListTrayPanel1";
            this.showListTrayPanel1.Size = new System.Drawing.Size(421, 420);
            this.showListTrayPanel1.TabIndex = 0;
            // 
            // rbtnStation1
            // 
            this.rbtnStation1.AutoSize = true;
            this.rbtnStation1.Checked = true;
            this.rbtnStation1.Location = new System.Drawing.Point(22, 12);
            this.rbtnStation1.Name = "rbtnStation1";
            this.rbtnStation1.Size = new System.Drawing.Size(53, 16);
            this.rbtnStation1.TabIndex = 1;
            this.rbtnStation1.TabStop = true;
            this.rbtnStation1.Text = "工位1";
            this.rbtnStation1.UseVisualStyleBackColor = true;
            // 
            // rbtnStation2
            // 
            this.rbtnStation2.AutoSize = true;
            this.rbtnStation2.Location = new System.Drawing.Point(106, 12);
            this.rbtnStation2.Name = "rbtnStation2";
            this.rbtnStation2.Size = new System.Drawing.Size(53, 16);
            this.rbtnStation2.TabIndex = 1;
            this.rbtnStation2.Text = "工位2";
            this.rbtnStation2.UseVisualStyleBackColor = true;
            this.rbtnStation2.CheckedChanged += new System.EventHandler(this.rbtnStation2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "吸笔选择：";
            // 
            // nudSuction
            // 
            this.nudSuction.Location = new System.Drawing.Point(91, 57);
            this.nudSuction.Maximum = new decimal(new int[] {
            18,
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
            this.nudSuction.TabIndex = 3;
            this.nudSuction.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(28, 98);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "托盘1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(28, 132);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 16);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "托盘2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(28, 166);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 16);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "托盘3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(28, 199);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(54, 16);
            this.checkBox4.TabIndex = 5;
            this.checkBox4.Text = "托盘4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(28, 232);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(54, 16);
            this.checkBox5.TabIndex = 5;
            this.checkBox5.Text = "托盘5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(28, 266);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(54, 16);
            this.checkBox6.TabIndex = 5;
            this.checkBox6.Text = "托盘6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(28, 299);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(54, 16);
            this.checkBox7.TabIndex = 5;
            this.checkBox7.Text = "托盘7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(28, 332);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(54, 16);
            this.checkBox8.TabIndex = 5;
            this.checkBox8.Text = "托盘8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(28, 366);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(54, 16);
            this.checkBox9.TabIndex = 5;
            this.checkBox9.Text = "托盘9";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.Click += new System.EventHandler(this.checkBox9_Click);
            // 
            // btnTraySet1
            // 
            this.btnTraySet1.Location = new System.Drawing.Point(116, 94);
            this.btnTraySet1.Name = "btnTraySet1";
            this.btnTraySet1.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet1.TabIndex = 6;
            this.btnTraySet1.Text = "盘形设定1";
            this.btnTraySet1.UseVisualStyleBackColor = true;
            this.btnTraySet1.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet2
            // 
            this.btnTraySet2.Location = new System.Drawing.Point(116, 128);
            this.btnTraySet2.Name = "btnTraySet2";
            this.btnTraySet2.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet2.TabIndex = 6;
            this.btnTraySet2.Text = "盘形设定2";
            this.btnTraySet2.UseVisualStyleBackColor = true;
            this.btnTraySet2.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet3
            // 
            this.btnTraySet3.Location = new System.Drawing.Point(116, 162);
            this.btnTraySet3.Name = "btnTraySet3";
            this.btnTraySet3.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet3.TabIndex = 6;
            this.btnTraySet3.Text = "盘形设定3";
            this.btnTraySet3.UseVisualStyleBackColor = true;
            this.btnTraySet3.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet4
            // 
            this.btnTraySet4.Location = new System.Drawing.Point(116, 195);
            this.btnTraySet4.Name = "btnTraySet4";
            this.btnTraySet4.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet4.TabIndex = 6;
            this.btnTraySet4.Text = "盘形设定4";
            this.btnTraySet4.UseVisualStyleBackColor = true;
            this.btnTraySet4.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet5
            // 
            this.btnTraySet5.Location = new System.Drawing.Point(116, 228);
            this.btnTraySet5.Name = "btnTraySet5";
            this.btnTraySet5.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet5.TabIndex = 6;
            this.btnTraySet5.Text = "盘形设定5";
            this.btnTraySet5.UseVisualStyleBackColor = true;
            this.btnTraySet5.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet6
            // 
            this.btnTraySet6.Location = new System.Drawing.Point(116, 262);
            this.btnTraySet6.Name = "btnTraySet6";
            this.btnTraySet6.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet6.TabIndex = 6;
            this.btnTraySet6.Text = "盘形设定6";
            this.btnTraySet6.UseVisualStyleBackColor = true;
            this.btnTraySet6.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet7
            // 
            this.btnTraySet7.Location = new System.Drawing.Point(116, 298);
            this.btnTraySet7.Name = "btnTraySet7";
            this.btnTraySet7.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet7.TabIndex = 6;
            this.btnTraySet7.Text = "盘形设定7";
            this.btnTraySet7.UseVisualStyleBackColor = true;
            this.btnTraySet7.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet8
            // 
            this.btnTraySet8.Location = new System.Drawing.Point(116, 332);
            this.btnTraySet8.Name = "btnTraySet8";
            this.btnTraySet8.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet8.TabIndex = 6;
            this.btnTraySet8.Text = "盘形设定8";
            this.btnTraySet8.UseVisualStyleBackColor = true;
            this.btnTraySet8.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // btnTraySet9
            // 
            this.btnTraySet9.Location = new System.Drawing.Point(116, 362);
            this.btnTraySet9.Name = "btnTraySet9";
            this.btnTraySet9.Size = new System.Drawing.Size(107, 23);
            this.btnTraySet9.TabIndex = 6;
            this.btnTraySet9.Text = "盘形设定9";
            this.btnTraySet9.UseVisualStyleBackColor = true;
            this.btnTraySet9.Click += new System.EventHandler(this.btnTraySet1_Click);
            // 
            // FrmSuctionAndTrayRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 423);
            this.Controls.Add(this.btnTraySet9);
            this.Controls.Add(this.btnTraySet8);
            this.Controls.Add(this.btnTraySet7);
            this.Controls.Add(this.btnTraySet6);
            this.Controls.Add(this.btnTraySet5);
            this.Controls.Add(this.btnTraySet4);
            this.Controls.Add(this.btnTraySet3);
            this.Controls.Add(this.btnTraySet2);
            this.Controls.Add(this.btnTraySet1);
            this.Controls.Add(this.checkBox9);
            this.Controls.Add(this.checkBox8);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.nudSuction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnStation2);
            this.Controls.Add(this.rbtnStation1);
            this.Controls.Add(this.showListTrayPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSuctionAndTrayRelation";
            this.Text = "吸笔和托盘关系建立";
            this.Load += new System.EventHandler(this.FrmSuctionAndTrayRelation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSuction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShowListTrayPanel showListTrayPanel1;
        private System.Windows.Forms.RadioButton rbtnStation1;
        private System.Windows.Forms.RadioButton rbtnStation2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudSuction;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.Button btnTraySet1;
        private System.Windows.Forms.Button btnTraySet2;
        private System.Windows.Forms.Button btnTraySet3;
        private System.Windows.Forms.Button btnTraySet4;
        private System.Windows.Forms.Button btnTraySet5;
        private System.Windows.Forms.Button btnTraySet6;
        private System.Windows.Forms.Button btnTraySet7;
        private System.Windows.Forms.Button btnTraySet8;
        private System.Windows.Forms.Button btnTraySet9;
    }
}