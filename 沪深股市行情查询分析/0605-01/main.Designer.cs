namespace _0605_01
{
    partial class main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.TextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.btn_CodeSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.label2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.模糊搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox_Mohu = new System.Windows.Forms.ToolStripTextBox();
            this.股价ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox_sp1 = new System.Windows.Forms.ToolStripTextBox();
            this.TextBox_sp2 = new System.Windows.Forms.ToolStripTextBox();
            this.成交金额ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox_tp1 = new System.Windows.Forms.ToolStripTextBox();
            this.TextBox_tp2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged_1);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("楷体", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1578, 598);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            //this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            //this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(100, 37);
            this.toolStripLabel2.Text = "请输入代码";
            // 
            // TextBox1
            // 
            this.TextBox1.AutoSize = false;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(150, 40);
            // 
            // btn_CodeSearch
            // 
            this.btn_CodeSearch.AutoSize = false;
            this.btn_CodeSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_CodeSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CodeSearch.Name = "btn_CodeSearch";
            this.btn_CodeSearch.Size = new System.Drawing.Size(60, 37);
            this.btn_CodeSearch.Text = "搜索";
            this.btn_CodeSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.AutoSize = false;
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton7.Text = "加入自选股";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.AutoSize = false;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton6.Text = "删除自选股";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(120, 37);
            this.toolStripButton2.Text = "我的自选股";
            this.toolStripButton2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton3.Text = "行情更新";
            this.toolStripButton3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(100, 37);
            this.toolStripLabel1.Text = "-- / --";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.AutoSize = false;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(200, 37);
            // 
            // label2
            // 
            this.label2.AutoSize = false;
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 37);
            this.label2.Text = "-- : -- : --";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.AutoSize = false;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton4.Text = "终止更新";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.AutoSize = false;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton5.Text = "全部股票";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.TextBox1,
            this.btn_CodeSearch,
            this.toolStripButton7,
            this.toolStripButton6,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripLabel1,
            this.ProgressBar1,
            this.label2,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripDropDownButton1,
            this.toolStripButton8});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1578, 60);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "--";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.AutoSize = false;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模糊搜索ToolStripMenuItem,
            this.股价ToolStripMenuItem,
            this.成交金额ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(100, 37);
            this.toolStripDropDownButton1.Text = "条件选股";
            // 
            // 模糊搜索ToolStripMenuItem
            // 
            this.模糊搜索ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextBox_Mohu});
            this.模糊搜索ToolStripMenuItem.Name = "模糊搜索ToolStripMenuItem";
            this.模糊搜索ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.模糊搜索ToolStripMenuItem.Text = "模糊搜索";
            // 
            // TextBox_Mohu
            // 
            this.TextBox_Mohu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_Mohu.Name = "TextBox_Mohu";
            this.TextBox_Mohu.Size = new System.Drawing.Size(100, 27);
            this.TextBox_Mohu.ToolTipText = "请输入模糊股名";
            // 
            // 股价ToolStripMenuItem
            // 
            this.股价ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextBox_sp1,
            this.TextBox_sp2});
            this.股价ToolStripMenuItem.Name = "股价ToolStripMenuItem";
            this.股价ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.股价ToolStripMenuItem.Text = "股价";
            // 
            // TextBox_sp1
            // 
            this.TextBox_sp1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_sp1.Name = "TextBox_sp1";
            this.TextBox_sp1.Size = new System.Drawing.Size(100, 27);
            this.TextBox_sp1.ToolTipText = "请输入最小值";
            // 
            // TextBox_sp2
            // 
            this.TextBox_sp2.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox_sp2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_sp2.Name = "TextBox_sp2";
            this.TextBox_sp2.Size = new System.Drawing.Size(100, 27);
            this.TextBox_sp2.ToolTipText = "请输入最大值";
            // 
            // 成交金额ToolStripMenuItem
            // 
            this.成交金额ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextBox_tp1,
            this.TextBox_tp2});
            this.成交金额ToolStripMenuItem.Name = "成交金额ToolStripMenuItem";
            this.成交金额ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.成交金额ToolStripMenuItem.Text = "成交金额";
            // 
            // TextBox_tp1
            // 
            this.TextBox_tp1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_tp1.Name = "TextBox_tp1";
            this.TextBox_tp1.Size = new System.Drawing.Size(100, 27);
            this.TextBox_tp1.ToolTipText = "请输入最小值";
            // 
            // TextBox_tp2
            // 
            this.TextBox_tp2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_tp2.Name = "TextBox_tp2";
            this.TextBox_tp2.Size = new System.Drawing.Size(100, 27);
            this.TextBox_tp2.ToolTipText = "请输入最大值";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.AutoSize = false;
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(100, 37);
            this.toolStripButton8.Text = "搜索";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 658);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("楷体", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "main";
            this.Text = "main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox TextBox1;
        private System.Windows.Forms.ToolStripButton btn_CodeSearch;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar1;
        private System.Windows.Forms.ToolStripLabel label2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 模糊搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox TextBox_Mohu;
        private System.Windows.Forms.ToolStripMenuItem 股价ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox TextBox_sp1;
        private System.Windows.Forms.ToolStripTextBox TextBox_sp2;
        private System.Windows.Forms.ToolStripMenuItem 成交金额ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox TextBox_tp1;
        private System.Windows.Forms.ToolStripTextBox TextBox_tp2;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
    }
}