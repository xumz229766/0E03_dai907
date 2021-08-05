using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
namespace Tray
{
    public partial class TrayPanel : UserControl
    {
        bool bFlag = false;//添加屏蔽标志
        bool bRemoveFlag = false;//移除屏蔽标志
        bool bShowModel = true;//显示模式
        Color backColor = System.Drawing.SystemColors.InactiveBorder;//背景色
        private Point activePoint = new Point(-1, -1);
        private int activeHandleIdx;
        private Point activeHandlePoint = new Point();
        private SynchronizationContext context = null;
        int Row = 3;
        int Col = 3;
        Tray t = null;
        public List<Label> lstLabel = new List<Label>();
        private StringFormat sf = null;
        //public bool bUnregular = false;
        public void setTrayObj(Tray tray,Color initColor)
        {
            t = tray;
            Row = t.TMaxRow;
            Col = t.TMaxCol;
            setLayout(initColor);
        }
        public void ShowTray(Tray tray)
        {
            t = tray;
            Row = t.TMaxRow;
            Col = t.TMaxCol;
            showLayout();
        }
        public bool BRemoveFlag
        {
            get { return bRemoveFlag; }
            set { bRemoveFlag = value;
               
            }
        }

        public bool BFlag
        {
            get { return bFlag; }
            set { bFlag = value; }
        }
        public bool BShowModel
        {
            get { return bShowModel; }
            set { bShowModel = value; }
        }
        public TrayPanel()
        {
           // SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            //lstLabel.Clear();
            //for (int i = 0; i < 600; i++)
            //{
            //   lstLabel.Add(CreateLabel(i));
            //}
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            InitializeComponent();
            context = SynchronizationContext.Current;
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;////用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}
        private Label CreateLabel(int i)
        {
            Label lbl = new Label();
            //Button btn = new Button();
            lbl.Anchor = AnchorStyles.None;
            lbl.Margin = new Padding(0, 0, 0, 0);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
           
            lbl.BackgroundImageLayout = ImageLayout.Stretch;
            lbl.Name = i.ToString();
         

            lbl.Click += new System.EventHandler(this.label_Click);
            return lbl;
        }
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        /// <summary>  
        /// 动态布局  
        /// </summary>  
        /// <param name="layoutPanel">布局面板</param>  
        /// <param name="Row">行</param>  
        /// <param name="Col">列</param>  
        private void DynamicLayout(TableLayoutPanel layoutPanel,Color bColor)
        {
            layoutPanel.SuspendLayout();
            sw.Restart();
            layoutPanel.Controls.Clear();
            layoutPanel.RowStyles.Clear();
            layoutPanel.ColumnStyles.Clear();
            layoutPanel.RowCount = Row+1;    //设置分成几行  
            float pRow = Convert.ToSingle((100 / Row).ToString("0.00"));
            float pColumn = Convert.ToSingle((100 / Col).ToString("0.00"));
            for (int i = 0; i < Row+1; i++)
            {
                if (i == Row)
                {
                    layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 0));
                }else
                layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            }
            layoutPanel.ColumnCount = Col+1;    //设置分成几列  
            for (int i = 0; i < Col+1; i++)
            {
                if (i == Col)
                {
                    layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0));
                }
                else
                {
                    layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                }
            }
            long time1 = sw.ElapsedMilliseconds;
            for (int i = 0; i < Row; i++)
            {

                for (int j = 0; j < Col; j++)
                {
                    if ((Row > 20) || (Col > 20))
                    {
                        lstLabel[i*Col+j].Font = new System.Drawing.Font("宋体", 5.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    }
                    else if ((Row > 15) || (Col > 15))
                    {
                        lstLabel[i * Col + j].Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }
                    else if ((Row > 10) || (Col > 10))
                    {
                        lstLabel[i * Col + j].Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }
                    else
                    {
                        lstLabel[i * Col + j].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    }
                    lstLabel[i * Col + j].Name = i.ToString() + "_" + j.ToString();
                    Index pos = new Index(i, j);
                    int num = t.FindPos(pos);
                    if (num > -1)
                        t.dic_Index[num].setColor(bColor);//初始化颜色
                    //如果是显示模式
                    if (bShowModel)
                    {
                        //屏蔽的点不显示名称
                        if (num < 0)
                        {
                            lstLabel[i * Col + j].Text = "";
                            setBtnColor(lstLabel[i * Col + j], backColor);
                        }
                        else
                        {

                            //不屏蔽的点显示顺序号
                            lstLabel[i * Col + j].Text = num.ToString();
                            setBtnColor(lstLabel[i * Col + j], bColor);
                        }

                    }
                    else
                    {
                        if (num < 0)
                        {
                            setBtnColor(lstLabel[i * Col + j], backColor);
                        }
                        else
                        {

                            setBtnColor(lstLabel[i * Col + j], bColor);
                        }
                        lstLabel[i * Col + j].Text = i.ToString() + "," + j.ToString();

                    }
                   // layoutPanel.Controls.Add(lbl,j,i);
                    layoutPanel.Controls.Add(lstLabel[i * Col + j], j, i);
                }
               
            }
            layoutPanel.ResumeLayout();
            long time2 = sw.ElapsedMilliseconds;

           // MessageBox.Show("Time1:"+time1.ToString() + "Time2:"+time2.ToString());
        }
        private void DynamicLayout2( Color bColor)
        {
           
            long time1 = sw.ElapsedMilliseconds;
            for (int i = 0; i < Row; i++)
            {

                for (int j = 0; j < Col; j++)
                {
                    
                    Index pos = new Index(i, j);
                    int num = t.FindPos(pos);
                    if (num > -1)
                    {
                        //pos.color = bColor;
                        //t.dic_Index[num] = pos;
                        t.dic_Index[num].setColor(bColor);//初始化颜色
                    }
                    this.Invalidate();
                

                  

                }

            }
            
            long time2 = sw.ElapsedMilliseconds;

            // MessageBox.Show("Time1:"+time1.ToString() + "Time2:"+time2.ToString());
        }
        private void DynamicLayoutShow()
        {
            for (int i = 0; i < Row; i++)
            {

                for (int j = 0; j < Col; j++)
                {

                    Index pos = new Index(i, j);
                    int num = t.FindPos(pos);
                    //if (num > -1)
                    //    t.dic_Index[num].setColor(t.dic_Index[num].color);//初始化颜色
                    this.Invalidate();




                }

            }
        }
       
        private void setBtnColor(Button btn,Color color)
        {
            if (color == Color.Yellow)
            {
                btn.BackgroundImage = Properties.Resources.ball_yellow_b;

            }else if (color == Color.Green)
            {
                btn.BackgroundImage = Properties.Resources.ball_green_b;

            }else if (color == Color.Blue)
            {
                btn.BackgroundImage = Properties.Resources.ball_blue_b;
            }
            else if (color == Color.Red)
            {
                btn.BackgroundImage = Properties.Resources.ball_red_b;
            }
            else if (color == Color.Gray)
            {
                btn.BackgroundImage = Properties.Resources.ball_gray_b;
            }
            else
            {
                btn.BackgroundImage = null;
                btn.BackColor = color;
            }
        }
        private void setBtnColor(Label lbl, Color color)
        {
           

            if (color == Color.Yellow)
            {
                lbl.BackgroundImage = Properties.Resources.ball_yellow_b;

            }
            else if (color == Color.Green)
            {
                lbl.BackgroundImage = Properties.Resources.ball_green_b;

            }
            else if (color == Color.Blue)
            {
                lbl.BackgroundImage = Properties.Resources.ball_blue_b;
            }
            else if (color == Color.Red)
            {
                lbl.BackgroundImage = Properties.Resources.ball_red_b;
            }
            else if (color == Color.Gray)
            {
                lbl.BackgroundImage = Properties.Resources.ball_gray_b;
            }
            else
            {
                lbl.BackgroundImage = null;
                lbl.BackColor = color;
            }
        }
        //private;
        public void setLayout(Color bColor)
        {
            t.sortTray(t.eStart,t.eDirect);
           
           // DynamicLayout(tableLayoutPanel1, bColor);
            DynamicLayout2(bColor);
        }
        public void showLayout()
        {
            DynamicLayoutShow();
        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //Graphics g = e.Graphics;
        //    //g.SmoothingMode = SmoothingMode.AntiAlias;
        //    //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        //    //this.PaintMain(g, (float)base.Width, (float)base.Height);
        //    //base.OnPaint(e);
        //}
        private void PaintMain(Graphics g, float width, float height)
        {
            try
            {

           
            float itemWidth = (width - 2) / (float)Col;
            float itemHeight = (height - 2) / (float)Row;
           
            //if (bUnregular)
            //{ 
            //    itemWidth = itemWidth-
            //}
          
            int radius = 10;

            if (itemWidth < itemHeight)
            {
                itemHeight = itemWidth;
                radius = (int)itemWidth / 2;
                
            }
            else {
                itemWidth = itemHeight;
                radius = (int)itemHeight / 2;
               
            }
          
           
            if (t != null)
            {
                float fW = itemWidth;
                float fH = itemHeight;
                 int count = Col > Row ? Row : Col;
                if (t.bUnregular)
                {
                   

                    if (fW < fH)
                        radius = radius + (int)(radius / 4);
                    else
                        radius = radius + (int)(radius / 4);
                    itemHeight = itemHeight - (int)(radius / count);
                    itemWidth = itemWidth - (int)(radius / count);
                }
                using (Brush backbrush = new SolidBrush(backColor))
                {
                    int startPosX = (int)(width - (itemWidth + radius / count) * Col) / 2;
                    int startPosY = (int)(Height - (itemHeight + radius / count) * Row) / 2;
                    if (startPosX < 1)
                        startPosX = 1;
                    if (startPosY < 1)
                        startPosY = 1;
                    var lrect = new Rectangle(startPosX, startPosY, (int)((itemWidth) * Col + 3), (int)((itemHeight) * Row + 3));
                    if (t.bUnregular)
                    {
                        lrect = new Rectangle(startPosX, startPosY, (int)(itemWidth + radius / count) * Col + 3, (int)(itemHeight + +radius / count) * Row + 3);
                    }
                    g.FillRectangle(backbrush, lrect);
                    g.DrawRectangle(new Pen(Color.Gold, 2), lrect);
                    var id = 0;
                    var point = new Point();
                    foreach (var num in t.dic_Index)
                    {
                        if (t.lstEmpty.Contains(num.Value))
                            continue;
                        var x =startPosX+ (int)(itemWidth * (float)num.Value.Col + 4);
                        var y = startPosY+(int)(itemHeight * (float)num.Value.Row + 4);
                        Point center = new Point(x, y);
                      //  double numArray = HMisc.DistancePp((double)(x + radius), y + radius, activePoint.X, activePoint.Y);
                        double numArray = Math.Sqrt((x + radius - activePoint.X) * (x + radius - activePoint.X) + (y + radius - activePoint.Y) * (y + radius - activePoint.Y));
                        using (Brush brush = new SolidBrush(num.Value.color))
                        {
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                            if (radius < 5) return;
                            var rectangle_larger = new Rectangle(center.X, center.Y, 2 * radius, 2 * radius);
                            var rectangle = new Rectangle(center.X + 3, center.Y + 3, 2 * radius - 6, 2 * radius - 6);
                            var rect_text = new Rectangle(center.X + 1, center.Y + 1, 2 * radius -2, 2 * radius - 2);
                            g.FillEllipse(brush, rectangle_larger);
                            g.DrawEllipse(new Pen(num.Value.color), rectangle_larger);
                            if (numArray > radius)
                            {
                                g.DrawEllipse(new Pen(Color.Black), rectangle);
                            }
                            else
                            {
                                g.DrawEllipse(new Pen(Color.White), rectangle);
                                id = num.Key;
                                point.X = num.Value.Col;
                                point.Y = num.Value.Row;
                            }
                            //if (!m_creatView)
                            //    g.DrawString(num.Key.ToString(), Font, Brushes.Blue, rect_text, sf);
                            //else
                            //{
                            Font f = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                            if (!bShowModel)
                                g.DrawString(num.Value.ToString(), Font, Brushes.Blue, rect_text, sf);
                            else
                            {
                                if ((Row > 20) || (Col > 20))
                                {
                                    g.DrawString(num.Key.ToString(), f, Brushes.Blue, rect_text, sf);
                                }
                                else
                                {
                                    g.DrawString(num.Key.ToString(), Font, Brushes.Blue, rect_text, sf);
                                }
                            }
                            //}
                        }
                    }
                    if (!t.bUnregular)
                    foreach (var index in t.lstEmpty)
                    {
                        var x = startPosX+(int)(itemWidth * (float)index.Col + 4);
                        var y = startPosY+(int)(itemHeight * (float)index.Row + 4);
                        Point center = new Point(x, y);
                        double numArray = Math.Sqrt((x + radius - activePoint.X) * (x + radius - activePoint.X) + (y + radius - activePoint.Y) * (y + radius - activePoint.Y));
                        if (numArray <= radius)
                        {
                            point.X = index.Col;
                            point.Y = index.Row;
                        }
                        if (!bShowModel)
                        {
                            using (Brush brush = new SolidBrush(Color.LightSteelBlue))
                            {
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                                if (radius < 5) return;
                                var rectangle_larger = new Rectangle(center.X, center.Y, 2 * radius, 2 * radius);
                                var rectangle = new Rectangle(center.X + 3, center.Y + 3, 2 * radius - 6, 2 * radius - 6);
                                var rect_text = new Rectangle(center.X + 1, center.Y + 1, 2 * radius - 2, 2 * radius - 2);
                                g.FillEllipse(brush, rectangle_larger);
                                g.DrawEllipse(new Pen(Color.DarkGray), rectangle_larger);
                                g.DrawEllipse(new Pen(Color.DarkGray), rectangle);
                                g.DrawString(index.ToString(), Font, Brushes.Blue, rect_text, sf);
                            }
                        }
                        else
                        {
                           
                                
                            using (Brush brush = new SolidBrush(backColor))
                            {
                              
                                var rectangle_larger = new Rectangle(center.X, center.Y, 2 * radius+1, 2 * radius+1);
                           
                                g.FillEllipse(brush, rectangle_larger);
                             
                            }
                        }
                    }
                    activeHandleIdx = id;
                    activeHandlePoint = point;
                }
            }
            }
            catch (Exception)
            {

                
            }
        }
        private void label_Click(object sender, EventArgs e)
        {
            if (bFlag && !bShowModel)
            {
                //Button btn = (Button)sender;
                Label btn = (Label)sender;
                setBtnColor(btn, backColor);
                t.addEmptyPos(btn.Name);
            }
            if (bRemoveFlag && !bShowModel)
            {
               // Button btn = (Button)sender;
                Label btn = (Label)sender;
                setBtnColor(btn, Color.Blue);
                t.removeEmptyPos(btn.Name);
               
            }

        }
        public void CreateUnRegular1()
        {
            t.bUnregular = true;
            if (bShowModel)
                return;
            
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++) {

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            //Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, backColor);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(backColor);
                            t.addEmptyPos(i.ToString() +"_"+j.ToString());
                        }
                        else {
                           // Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, Color.Blue);
                            //t.removeEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(Color.Blue);
                            t.removeEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                    }
                    else {
                        if (j % 2 == 1)
                        {
                           // Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, backColor);
                            //t.addEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(backColor);
                            t.addEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                        else
                        {
                           // Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                           //// Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                           // setBtnColor(btn, Color.Blue);
                           // t.removeEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(Color.Blue);
                            t.removeEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                    }
                
                }
            }
            Refresh();
        }
        public void CreateUnRegular2()
        {
            t.bUnregular = true;
            if (bShowModel)
                return;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 1)
                        {
                            //Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, backColor);
                            //t.addEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(backColor);
                            t.addEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                        else
                        {
                           //// Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                           // Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                           // setBtnColor(btn, Color.Blue);
                           // t.removeEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(Color.Blue);
                            t.removeEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            ////Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, backColor);
                            //t.addEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(backColor);
                            t.addEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                        else
                        {
                           // Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(j, i);
                            //setBtnColor(btn, Color.Blue);
                            //t.removeEmptyPos(btn.Name);
                            Index pos = new Index(i, j);
                            int num = t.FindPos(pos);
                            if (num > -1)
                                t.dic_Index[num].setColor(Color.Blue);
                            t.removeEmptyPos(i.ToString() + "_" + j.ToString());
                        }
                    }

                }
            }
            Refresh();
        }
       
      
        public void UpdateColor()
        {
            context.Post(updateUI, null);
            
        }
        private void updateUI(object obj)
        {
            //foreach (KeyValuePair<int, Index> pair in t.dic_Index)
            //{
            //    int col = pair.Value.Col;
            //    int row = pair.Value.Row;
            //   // Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(col, row);
            //    Label btn = (Label)tableLayoutPanel1.GetControlFromPosition(col, row);
            //    setBtnColor(btn, pair.Value.color);
            //}
            base.Invalidate();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TrayPanel_Load(object sender, EventArgs e)
        {
            base.MouseMove += TrayManagement_MouseMove;
            base.MouseLeave += TrayManagement_MouseLeave;
            base.Click += TrayManagement_Click;
        }

        private void TrayManagement_Click(object sender, EventArgs e)
        {
            if (bShowModel)
                return;
            if (activeHandleIdx == 0 & activeHandlePoint.X == 0 & activeHandlePoint.Y == 0) return;
            if (bFlag && !bShowModel)
            {
                t.addEmptyPos(activeHandlePoint.Y, activeHandlePoint.X);
               
                //t.sortTray();
                t.setNumColor(activeHandleIdx, Color.LightSteelBlue);
            }
            if (bRemoveFlag && !bShowModel)
            {
                t.removeEmptyPos(activeHandlePoint.Y + "_" + activeHandlePoint.X);
                //t.SortTray();
                t.setNumColor(activeHandleIdx,Color.Gray);
            }
            UpdateColor();
        }

        private void TrayManagement_MouseLeave(object sender, EventArgs e)
        {
            if (bShowModel)
                return;
            activePoint = new Point(-1, -1);
           // Refresh();
        }

        private void TrayManagement_MouseMove(object sender, MouseEventArgs e)
        {
            if (bShowModel)
                return;
            activePoint = new Point(e.X, e.Y);
            Refresh();
        }

        private void TrayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            this.PaintMain(g, (float)base.Width, (float)base.Height);
            //base.OnPaint(e);
        }

        private void TrayPanel_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
       
        
    }

    public class CoTableLayoutPanel : TableLayoutPanel
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.CacheText, true);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= NativeMethods.WS_EX_COMPOSITED;
                return cp;
            }
        }

        public void BeginUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        public void EndUpdate()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            Parent.Invalidate(true);
        }
    }

    public static class NativeMethods
    {
        public static int WM_SETREDRAW = 0x000B; //uint WM_SETREDRAW
        public static int WS_EX_COMPOSITED = 0x02000000;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam); //UInt32 Msg
    }

}
