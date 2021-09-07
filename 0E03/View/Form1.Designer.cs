namespace _0E030
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Sunny.UI.UIStyleManager uiStyleManager1;
            this.uiSymbolMain = new Sunny.UI.UISymbolButton();
            this.uiSymbolHand = new Sunny.UI.UISymbolButton();
            this.uiSymbolParamSet = new Sunny.UI.UISymbolButton();
            this.uiSymbolIO = new Sunny.UI.UISymbolButton();
            this.uiSymbolOperator = new Sunny.UI.UISymbolButton();
            this.uiSymbolExit = new Sunny.UI.UISymbolButton();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiAlaramLight = new Sunny.UI.UILight();
            this.btnSaveParam = new Sunny.UI.UISymbolButton();
            uiStyleManager1 = new Sunny.UI.UIStyleManager(this.components);
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // Aside
            // 
            this.Aside.LineColor = System.Drawing.Color.Black;
            this.Aside.Location = new System.Drawing.Point(0, 85);
            this.Aside.Size = new System.Drawing.Size(1, 899);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Header
            // 
            this.Header.Controls.Add(this.uiAlaramLight);
            this.Header.Controls.Add(this.uiLabel1);
            this.Header.Controls.Add(this.uiSymbolExit);
            this.Header.Controls.Add(this.btnSaveParam);
            this.Header.Controls.Add(this.uiSymbolOperator);
            this.Header.Controls.Add(this.uiSymbolIO);
            this.Header.Controls.Add(this.uiSymbolParamSet);
            this.Header.Controls.Add(this.uiSymbolHand);
            this.Header.Controls.Add(this.uiSymbolMain);
            this.Header.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Header.Size = new System.Drawing.Size(1280, 50);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            // 
            // uiStyleManager1
            // 
            uiStyleManager1.Style = Sunny.UI.UIStyle.Office2010Silver;
            // 
            // uiSymbolMain
            // 
            this.uiSymbolMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolMain.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolMain.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolMain.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolMain.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolMain.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolMain.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolMain.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolMain.Location = new System.Drawing.Point(163, 3);
            this.uiSymbolMain.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolMain.Name = "uiSymbolMain";
            this.uiSymbolMain.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolMain.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolMain.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolMain.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolMain.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolMain.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolMain.Symbol = 57353;
            this.uiSymbolMain.SymbolSize = 40;
            this.uiSymbolMain.TabIndex = 0;
            this.uiSymbolMain.Text = "主界面";
            this.uiSymbolMain.Click += new System.EventHandler(this.uiSymbolMain_Click);
            // 
            // uiSymbolHand
            // 
            this.uiSymbolHand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolHand.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolHand.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolHand.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolHand.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolHand.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolHand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolHand.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolHand.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolHand.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolHand.Location = new System.Drawing.Point(294, 3);
            this.uiSymbolHand.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolHand.Name = "uiSymbolHand";
            this.uiSymbolHand.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolHand.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolHand.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolHand.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolHand.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolHand.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolHand.Symbol = 62038;
            this.uiSymbolHand.SymbolSize = 40;
            this.uiSymbolHand.TabIndex = 0;
            this.uiSymbolHand.Text = "手动";
            // 
            // uiSymbolParamSet
            // 
            this.uiSymbolParamSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolParamSet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolParamSet.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolParamSet.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolParamSet.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolParamSet.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolParamSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolParamSet.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolParamSet.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolParamSet.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolParamSet.Location = new System.Drawing.Point(425, 3);
            this.uiSymbolParamSet.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolParamSet.Name = "uiSymbolParamSet";
            this.uiSymbolParamSet.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolParamSet.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolParamSet.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolParamSet.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolParamSet.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolParamSet.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolParamSet.Symbol = 57399;
            this.uiSymbolParamSet.SymbolSize = 40;
            this.uiSymbolParamSet.TabIndex = 0;
            this.uiSymbolParamSet.Text = "参数设置";
            this.uiSymbolParamSet.Click += new System.EventHandler(this.uiSymbolParamSet_Click);
            // 
            // uiSymbolIO
            // 
            this.uiSymbolIO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolIO.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolIO.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolIO.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolIO.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolIO.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolIO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolIO.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolIO.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolIO.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolIO.Location = new System.Drawing.Point(557, 3);
            this.uiSymbolIO.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolIO.Name = "uiSymbolIO";
            this.uiSymbolIO.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolIO.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolIO.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolIO.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolIO.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolIO.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolIO.Symbol = 57351;
            this.uiSymbolIO.SymbolSize = 40;
            this.uiSymbolIO.TabIndex = 0;
            this.uiSymbolIO.Text = "轴和IO";
            this.uiSymbolIO.Click += new System.EventHandler(this.uiSymbolIO_Click);
            // 
            // uiSymbolOperator
            // 
            this.uiSymbolOperator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolOperator.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolOperator.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolOperator.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolOperator.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolOperator.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolOperator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolOperator.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolOperator.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolOperator.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolOperator.Location = new System.Drawing.Point(689, 3);
            this.uiSymbolOperator.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolOperator.Name = "uiSymbolOperator";
            this.uiSymbolOperator.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolOperator.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolOperator.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolOperator.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolOperator.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolOperator.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolOperator.Symbol = 61447;
            this.uiSymbolOperator.SymbolSize = 40;
            this.uiSymbolOperator.TabIndex = 0;
            this.uiSymbolOperator.Text = "操作人";
            // 
            // uiSymbolExit
            // 
            this.uiSymbolExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.uiSymbolExit.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.uiSymbolExit.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolExit.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.uiSymbolExit.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolExit.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolExit.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolExit.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.uiSymbolExit.Location = new System.Drawing.Point(955, 3);
            this.uiSymbolExit.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolExit.Name = "uiSymbolExit";
            this.uiSymbolExit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.uiSymbolExit.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.uiSymbolExit.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolExit.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.uiSymbolExit.Size = new System.Drawing.Size(120, 44);
            this.uiSymbolExit.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolExit.Symbol = 61584;
            this.uiSymbolExit.SymbolSize = 40;
            this.uiSymbolExit.TabIndex = 0;
            this.uiSymbolExit.Text = "退出";
            this.uiSymbolExit.Click += new System.EventHandler(this.uiSymbolExit_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Blue;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(3, 3);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(154, 44);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiAlaramLight
            // 
            this.uiAlaramLight.CenterColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(232)))));
            this.uiAlaramLight.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiAlaramLight.Location = new System.Drawing.Point(1227, 2);
            this.uiAlaramLight.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAlaramLight.Name = "uiAlaramLight";
            this.uiAlaramLight.OffColor = System.Drawing.Color.Gray;
            this.uiAlaramLight.OnColor = System.Drawing.Color.Red;
            this.uiAlaramLight.Radius = 35;
            this.uiAlaramLight.Size = new System.Drawing.Size(47, 47);
            this.uiAlaramLight.State = Sunny.UI.UILightState.Blink;
            this.uiAlaramLight.Style = Sunny.UI.UIStyle.Custom;
            this.uiAlaramLight.TabIndex = 2;
            this.uiAlaramLight.Text = "uiLight1";
            // 
            // btnSaveParam
            // 
            this.btnSaveParam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveParam.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(249)))));
            this.btnSaveParam.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(226)))), ((int)(((byte)(137)))));
            this.btnSaveParam.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.btnSaveParam.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(137)))));
            this.btnSaveParam.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveParam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSaveParam.ForeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSaveParam.ForePressColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSaveParam.ForeSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSaveParam.Location = new System.Drawing.Point(822, 3);
            this.btnSaveParam.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSaveParam.Name = "btnSaveParam";
            this.btnSaveParam.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.btnSaveParam.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(201)))), ((int)(((byte)(88)))));
            this.btnSaveParam.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.btnSaveParam.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(118)))), ((int)(((byte)(43)))));
            this.btnSaveParam.Size = new System.Drawing.Size(120, 44);
            this.btnSaveParam.Style = Sunny.UI.UIStyle.Custom;
            this.btnSaveParam.Symbol = 61639;
            this.btnSaveParam.SymbolSize = 40;
            this.btnSaveParam.TabIndex = 0;
            this.btnSaveParam.Text = "保存参数";
            this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 984);
            this.Name = "Form1";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(144)))), ((int)(((byte)(151)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "组装机";
            this.TitleColor = System.Drawing.Color.Silver;
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISymbolButton uiSymbolMain;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton uiSymbolExit;
        private Sunny.UI.UISymbolButton uiSymbolOperator;
        private Sunny.UI.UISymbolButton uiSymbolIO;
        private Sunny.UI.UISymbolButton uiSymbolParamSet;
        private Sunny.UI.UISymbolButton uiSymbolHand;
        private Sunny.UI.UILight uiAlaramLight;
        private Sunny.UI.UISymbolButton btnSaveParam;


    }
}

