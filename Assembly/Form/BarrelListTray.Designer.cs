namespace Assembly
{
    partial class BarrelListTray
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
            this.lblX = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(143, 212);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblX
            // 
            this.lblX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblX.Location = new System.Drawing.Point(0, 0);
            this.lblX.Margin = new System.Windows.Forms.Padding(0);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(143, 20);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X 轴";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.Yellow;
            this.lbl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.Location = new System.Drawing.Point(3, 23);
            this.lbl2.Margin = new System.Windows.Forms.Padding(3);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(137, 89);
            this.lbl2.TabIndex = 0;
            this.lbl2.Text = "2";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl2.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl1_Paint);
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.Yellow;
            this.lbl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl1.Location = new System.Drawing.Point(3, 118);
            this.lbl1.Margin = new System.Windows.Forms.Padding(3);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(137, 91);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "1";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl1.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl1_Paint);
            // 
            // BarrelListTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BarrelListTray";
            this.Size = new System.Drawing.Size(143, 212);
            this.Load += new System.EventHandler(this.BarrelListTray_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
    }
}
