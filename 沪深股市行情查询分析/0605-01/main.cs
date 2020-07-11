using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _0605_01
{
    public partial class main : Form
    {
        //用来显示股票详情页
        Form1 frm1;
        //系统现在时间
        System.DateTime nowTime;
        bool button4_state = false;
        int _index = 0;
        int blank = -2;
        //实例化自适应窗口类的对象
        //AutoSizeFormClass asc = new AutoSizeFormClass();
        private void main_Load(object sender, EventArgs e)
        {
            //加入/删除自选股初始不可点击
            toolStripButton7.Enabled = false;
            toolStripButton6.Enabled = false;
            TextBox1.Text = string.Empty;
            Form.CheckForIllegalCrossThreadCalls = false;
            // 设置可以通告进度
            backgroundWorker1.WorkerReportsProgress = true;
            //终止更新按钮初始不可点击
            toolStripButton4.Enabled = false;
        }
        //dgv的列名自定义
        public void dtChangeName(DataTable dt)
        {
            dt.Columns["stock_Id"].ColumnName = "股票代码";
            dt.Columns["stock_name"].ColumnName = "股票名称";
            dt.Columns["current_Price"].ColumnName = "成交价";
            dt.Columns["changeFudu"].ColumnName = "涨跌幅";
            dt.Columns["open_Price"].ColumnName = "开盘价";
            dt.Columns["highest_Price"].ColumnName = "最高价";
            dt.Columns["lowest_Price"].ColumnName = "最低价";
            dt.Columns["transaction_Number"].ColumnName = "成交量（股）";
            dt.Columns["transaction_Price"].ColumnName = "成交额（万）";
        }
        public void dtFormat()
        {
            dataGridView1.Columns["成交价"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["开盘价"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["最高价"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["最低价"].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns["成交价"].DefaultCellStyle.Format = "0.00";
            //涨跌幅那一列数据要以百分比显示
            dataGridView1.Columns["涨跌幅"].DefaultCellStyle.Format = "0.00%";
        }
        public main()
        {
            InitializeComponent();
            //初始化DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dataGridView1.BackgroundColor = Color.Gray; 
            dataGridView1.AllowUserToAddRows = false; 
            dataGridView1.AllowUserToOrderColumns = false;
        }
        //代码搜股
        private void button1_Click(object sender, EventArgs e)
        {
            //保证只能打开一只个股详情页
            if (frm1 != null)
            {
                frm1.Close();
            }
            //隐藏dgv
            dataGridView1.Visible = false;
            //调用Form1的获股方法，传TextBox1的值
            frm1=new Form1();
            int n=frm1.getStock(TextBox1.Text);
            //返回0则无此股，或退市等情况
            if (n == 0)
            {
                MessageBox.Show("无");
                return;
            }
            frm1.Text = frm1.stock_Id + frm1.stock_name;
            frm1.MdiParent = this;
            frm1.Show();
            //主窗口的加入删除自选股不可点击，使用子窗口的
            toolStripButton7.Enabled = false;
            toolStripButton6.Enabled = false;
            TextBox1.Text = string.Empty;
            //每秒刷新（可能受网络波动使窗体卡住）
            frm1.F5_way();
        }
        bool judgeZxg = false;
        //用在为自选股返回数据的
        Form1 frm2 = new Form1();
        //自选股
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            judgeZxg = true;
            //自选股只显示删除自选股按钮
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = false;
            if (frm1 != null)
            {
                frm1.Close();
            }           
            //Select语句获得自选股数据库的股票，显示在DataGriView
            string sqlSelect ="Select * From MyStock";
            try
            {
                SQLHelper sh = new SQLHelper();
                DataTable dt = sh.getDataTable(sqlSelect);
                dtChangeName(dt);
                dataGridView1.DataSource = dt;
                dtFormat();
                int n = dt.Rows.Count;
                //循环调用API，显示实时数据
                for (int i = 0; i < n; i++)
                {
                    //调用API时避免窗体假死
                    Application.DoEvents();
                    string stockId = dataGridView1.Rows[i].Cells["股票代码"].Value.ToString().Trim();
                    frm2.getStock(stockId);
                    //从API获得数据先存入数据库再显示在dgv
                    string strUpd = "Update MyStock Set stock_name='" + frm2.stock_name + "',current_Price='"
                       + frm2.current_price + "',changeFudu='"
                       + frm2.changeFudu + "',open_Price='"
                       + frm2.open_pricce + "',highest_Price='"
                       + frm2.highest_price + "',lowest_Price='"
                       + frm2.lowest_price + "',transaction_Number='"
                       + frm2.transaction_number + "',transaction_Price='"
                       + frm2.transaction_price + "' Where stock_Id='" + stockId + "'";
                    sh.operateTable(strUpd);
                    dataGridView1.Rows[i].Cells["成交价"].Value = frm2.current_price;
                    dataGridView1.Rows[i].Cells["涨跌幅"].Value = frm2.changeFudu;
                    dataGridView1.Rows[i].Cells["开盘价"].Value = frm2.open_pricce;
                    dataGridView1.Rows[i].Cells["最高价"].Value = frm2.highest_price;
                    dataGridView1.Rows[i].Cells["最低价"].Value = frm2.lowest_price;
                    dataGridView1.Rows[i].Cells["成交量（股）"].Value = frm2.transaction_number;
                    dataGridView1.Rows[i].Cells["成交额（万）"].Value = frm2.transaction_price;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        int index;
        //单击自选股DataGridView中某行，获得该行的代码，显示在搜索框，便于加入/删除自选股
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                index = dataGridView1.CurrentRow.Index;
                TextBox1.Text = dataGridView1.Rows[index].Cells["股票代码"].Value.ToString().Trim();
                //这个是用来判断是否在自选股库中而更改按钮可点击属性的，这可能会影响电脑性能
                string strSelect = "Select stock_Id From MyStock Where stock_Id='"
                             + TextBox1.Text + "'";
                SQLHelper shh = new SQLHelper();
                //结果显示DataGridView
                DataTable dt = shh.getDataTable(strSelect);
                //若存在则“加入自选股”按钮变灰
                if (dt.Rows.Count == 1)
                {
                    toolStripButton7.Enabled = false;
                    toolStripButton6.Enabled = true;
                }
                //若不存在则“删除自选股”按钮变灰
                if (dt.Rows.Count == 0)
                {
                    toolStripButton6.Enabled = false;
                    toolStripButton7.Enabled = true;
                }
            }
            //空白行标记值设为-2
            blank = -2;
        }        
        //双击自选股DataGridView中某行，显示个股详情（推荐此方法）
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //如果点在了空白行，则双击无效果
            if (blank == -2)
            {
                return;
            }
            //如果没点在标题行，则执行双击操作
            if (_index != -1)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Hide();
                    int index = dataGridView1.CurrentRow.Index;
                    string stockId = dataGridView1.Rows[index].Cells["股票代码"].Value.ToString().Trim();
                    //调用Form1的getStock方法，传入获取值
                    frm1 = new Form1();
                    int n = frm1.getStock(stockId);
                    if (n == 0)
                    {
                        MessageBox.Show("无");
                        return;
                    }
                    frm1.Text = frm1.stock_Id + frm1.stock_name;
                    frm1.MdiParent = this;
                    frm1.Show();
                    toolStripButton7.Enabled = false;
                    toolStripButton6.Enabled = false;
                    TextBox1.Text = string.Empty;
                    frm1.F5_way();
                }
            }
            blank = -2;
        }
        //行情更新
        private void button3_Click(object sender, EventArgs e)
        {
            toolStripButton4.Enabled = true;
            button4_state = false;
            //启动timer，获取系统时间，方便之后计时
            timer1.Start();
            nowTime = System.DateTime.Now;
            //进度条容量
            ProgressBar1.Maximum = 8101;
            //开始执行BackGroundWorker
            backgroundWorker1.RunWorkerAsync();
        }
        //用在行情更新返回数据
        string stock_Id;
        Form1 frm = new Form1();
        //沪市增股入库方法
        public void addStockTable(int i)
        {
            //代码前+sh表示沪市股票
            stock_Id = "sh" + i.ToString();
            //通过Select语句选择股票数据显示在DataTable
            string strSelect = "Select stock_Id From " + tableName + " Where stock_Id='"
                     + stock_Id + "'";
            SQLHelper sh = new SQLHelper();
            DataTable dt = sh.getDataTable(strSelect);
            //若没有找到该股数据，插入（可能是新股，改名股等）
            if (dt.Rows.Count == 0)
            {
                if (frm.getStock(stock_Id) == 1)
                {
                    string strInsert = "Insert Into " + tableName + " (stock_Id,stock_name,current_Price,changeFudu,open_Price,highest_Price,lowest_Price,transaction_Number,transaction_Price) Values ('"
                       + stock_Id + "','"
                       + frm.stock_name + "','"
                       + frm.current_price + "','"
                       + frm.changeFudu + "','"
                       + frm.open_pricce + "','"
                       + frm.highest_price + "','"
                       + frm.lowest_price + "','"
                       + frm.transaction_number + "','"
                       + frm.transaction_price + "')";
                    int r = sh.operateTable(strInsert);
                }
            }
            //若在DATaTable有Select到的数据，即数据库有该条股票数据，那就对其更新
            else
            {
                if (frm.getStock(stock_Id) == 1)
                {
                    string strOperate = "Update " + tableName + " Set stock_Id='" + stock_Id
                    + "', stock_name='" + frm.stock_name + "',current_Price='"
                       + frm.current_price + "',changeFudu='"
                       + frm.changeFudu + "',open_Price='"
                       + frm.open_pricce + "',highest_Price='"
                       + frm.highest_price + "',lowest_Price='"
                       + frm.lowest_price + "',transaction_Number='"
                       + frm.transaction_number + "',transaction_Price='"
                       + frm.transaction_price + "' Where stock_Id='" + stock_Id + "'";
                    int r = sh.operateTable(strOperate);                   
                }               
            }            
        }
        //深市增股入库方法
        public void addStockTable2(int i)
        { 
            //判断前加几个0
            if (i < 10)
            {
                stock_Id = "sz00000" + i.ToString();
            }
            else if (i < 100)
            {
                stock_Id = "sz0000" + i.ToString();
            }
            else if (i < 1000)
            {
                stock_Id = "sz000" + i.ToString();
            }
            else if (i < 4000)
            {
                stock_Id = "sz00" + i.ToString();
            }
            //创业板300开头，不用前加0
            else
            {
                stock_Id = "sz" + i.ToString();
            }
            //查询股票信息，显示在DataTable
            string strSelect = "Select stock_Id From " + tableName + " Where stock_Id='"
                     + stock_Id + "'";
            SQLHelper sh = new SQLHelper();
            DataTable dt = sh.getDataTable(strSelect);
            //有则更新，无则插入
            if (dt.Rows.Count == 0)
            {
                if (frm.getStock(stock_Id) == 1)
                {
                    string strInsert = "Insert Into " + tableName + " (stock_Id,stock_name,current_Price,changeFudu,open_Price,highest_Price,lowest_Price,transaction_Number,transaction_Price) Values ('"
                      + stock_Id + "','"
                      + frm.stock_name + "','"
                      + frm.current_price + "','"
                      + frm.changeFudu + "','"
                      + frm.open_pricce + "','"
                      + frm.highest_price + "','"
                      + frm.lowest_price + "','"
                      + frm.transaction_number + "','"
                      + frm.transaction_price + "')";
                    int r = sh.operateTable(strInsert);
                }
            }
            else
            {
                if (frm.getStock(stock_Id) == 1)
                {
                    string strOperate = "Update " + tableName + " Set stock_Id='" + stock_Id
                    + "', stock_name='" + frm.stock_name + "',current_Price='"
                       + frm.current_price + "',changeFudu='"
                       + frm.changeFudu + "',open_Price='"
                       + frm.open_pricce + "',highest_Price='"
                       + frm.highest_price + "',lowest_Price='"
                       + frm.lowest_price + "',transaction_Number='"
                       + frm.transaction_number + "',transaction_Price='"
                       + frm.transaction_price + "' Where stock_Id='" + stock_Id + "'";
                    int r = sh.operateTable(strOperate);
                }
            }
        }
        string tableName;        
        //股票行情更新方法，更新一条报告出去一次
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //为每日行情建一张表，做这一步是为了在以后复杂股票数据分析所用
                tableName = "tb_" + DateTime.Now.ToString("yyyyMMdd");
                string strCreate = "CREATE TABLE " + tableName + "" +
                "(stock_Id NCHAR(10) PRIMARY KEY," +
                "stock_Name NCHAR(10), current_Price float, changeFudu float, open_Price float,highest_Price float, lowest_Price float, transaction_Number decimal(18, 0), transaction_Price decimal(18, 6))";

                SQLHelper sh = new SQLHelper();
                int n = sh.operateTable(strCreate);
                if (n == -1)//我很诧异建表成功居然返回的是-1
                {
                    MessageBox.Show(tableName + " 建表成功");
                }
            }
            catch (Exception ex)
            {   
                //已存表则覆盖数据
                string error = ex.Message;//数据库中已存在名为 'tb_20200624' 的对象。
                if (error.Substring(0, 9) == "数据库中已存在名为" && error.Substring(error.Length - 4) == "的对象。")
                {
                    MessageBox.Show("覆盖今日数据中");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //点击了行情更新，停止按钮设为不可点
            toolStripButton3.Enabled = false;
            try
            {
                //i表示渐增的股票六位代码
                int i = 0;
                //m表示报告出去的数字
                int m = 0;
                //从上海股票主板开始（60开头）
                for (i = 600000; i < 602000; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    //调用上证股票添入数据库方法
                    addStockTable(i);
                    //报告数加1
                    m++;
                    //报告数传出去
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);
                }
                //同上，沪市主板
                for (i = 603000; i < 604000; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    addStockTable(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);
                }
                //同上，沪市主板
                for (i = 605000; i < 605200; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    addStockTable(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);

                }
                //沪市科创板
                for (i = 688000; i < 689000; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    addStockTable(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);

                }
                //深市主板（由于深市主板及中小板代码开头为00，为防止程序认为前面0为无效数，故从0开始计数，后作为形参传入getStock前在前面加相应数目的0）
                for (i = 0; i < 3000; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    addStockTable2(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);
                }
                for (i = 3816; i <= 3816; i++)
                {
                    if (button4_state == true)
                    {
                        return;
                    }
                    addStockTable2(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);
                }
                //深市创业板
                for (i = 300000; i < 300900; i++)
                {
                    if (button4_state == true)
                    {
                        button4_state = false;
                        return;
                    }
                    addStockTable2(i);
                    m++;
                    BackgroundWorker bw = sender as BackgroundWorker;
                    bw.ReportProgress(m);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //所有股票入库后，按钮恢复，计时停止
                toolStripButton3.Enabled = true;
                timer1.Stop();
            }
            
        }
        //获取报告出来的数，更改进度条进度
        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            int n = e.ProgressPercentage;            
            toolStripLabel1.Text = n + "/" + 8101 ;
            ProgressBar1.Value = n;
        }
        //计时功能，用于行情更新计时
        private void timer1_Tick(object sender, EventArgs e)
        {
            //获取实时时间，将其减去点击按钮时保存的时间，获取运行时间，并显示在label2上
            System.DateTime time = System.DateTime.Now;
            TimeSpan timeCount=time-nowTime;
            label2.Text = timeCount.Hours+":"+timeCount.Minutes+":"+timeCount.Seconds;
        }
        //导入完成后，即backgroundworker运行结束后，显示导入完成
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ProgressBar1.Value == ProgressBar1.Maximum)
            {
                toolStripLabel1.Text = "导入完成";
                toolStripButton4.Enabled = false;
            }
        }
        //终止更新方法，再次启动从零开始更新（此处本应该设置一个停止更新，但思考后认为更新状态不影响窗体操作，没必要中途停止，而终止可以使用户退出窗体而不破坏数据库导入过程）
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //防误触
            if ((int)MessageBox.Show("确定要终止吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != 1)
            {
                return;
            }
            else
            {
                button4_state = true;
                toolStripButton4.Enabled = false;
            }
        }
        //获取当日所有股票信息
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            judgeZxg = false;
            //默认情况下开加入删除自选股
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = true;
            if (frm1 != null)
            {
                frm1.Close();
            }
            tableName = "tb_" + DateTime.Now.ToString("yyyyMMdd");
            dataGridView1.Visible = true;
            //Select语句获得今日自选股数据库的股票，显示在DataGriView
            string sqlSelect = "Select * From " + tableName + "";
            try
            {
                SQLHelper sh = new SQLHelper();
                DataTable dt = sh.getDataTable(sqlSelect);
                dtChangeName(dt);
                dataGridView1.DataSource = dt;
                dtFormat();
            }
            catch (Exception ex)
            {
                //若无今日数据，问用户要不要导入前一个交易日数据
                string error = ex.Message;//对象名  'tb_20200624' 无效。
                if (error.Substring(0, 3) == "对象名" && error.Substring(error.Length - 3) == "无效。")
                {
                    if ((int)MessageBox.Show("您还未导入今日行情，是否使用上个交易日的", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
                    {
                        int i = 1;
                        //循环调前一天数据，直至调到有数据的表（周末休两天，法定假日长则七天）
                        while (true)
                        {
                            tableName = "tb_" + DateTime.Now.AddDays(-i).ToString("yyyyMMdd");
                            string sqlSelect1 = "Select * From " + tableName + "";
                            try
                            {
                                SQLHelper sh = new SQLHelper();
                                DataTable dt = sh.getDataTable(sqlSelect1);
                                dtChangeName(dt);
                                dataGridView1.DataSource = dt;
                                dataGridView1.Columns["涨跌幅"].DefaultCellStyle.Format = "0.00%";
                                break;
                            }
                            catch (Exception ex1)
                            {
                                error = ex1.Message;
                                if (error.Substring(0, 3) == "对象名" && error.Substring(error.Length - 3) == "无效。")
                                {
                                    i++;
                                }
                                else
                                {
                                    MessageBox.Show(ex1.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //关闭窗口时，结束所有进程
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
        //主窗体的删除自选股方法
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string strDelete = "Delete From MyStock Where stock_Id='" + TextBox1.Text + "'";
            try
            {
                SQLHelper sh = new SQLHelper();
                int r = sh.operateTable(strDelete);
                if (r == 1)
                {
                    //删除成功，删除按钮变灰，开放加入按钮
                    MessageBox.Show("delete success!");
                    toolStripButton6.Enabled = false;
                    toolStripButton7.Enabled = true;
                    //删除自选股库的自选股和dgv里的自选股
                    if (judgeZxg == true)
                    {
                        DataGridViewRow row = dataGridView1.Rows[index];
                        dataGridView1.Rows.Remove(row);
                        if (dataGridView1.Rows.Count > 0)
                        {
                            index = dataGridView1.CurrentRow.Index;
                            TextBox1.Text = dataGridView1.Rows[index].Cells["股票代码"].Value.ToString().Trim();
                        }                
                    }  
                }
                else
                {
                    MessageBox.Show("自选股中无该股!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //加入自选股时调用API的窗体对象
        Form1 frm3=new Form1();
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //sql语句加入自选股数据库
            //string strInsert = "Insert Into MyStock (stock_Id,stock_name,current_Price) Values ('"
            //           + stock_Id + "','"
            //           + stock_name + "','"
            //           +current_price+"')";
            if (TextBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Input StockId");
                return;
            }
            frm3.getStock(TextBox1.Text);
            string strInsert = "Insert Into MyStock (stock_Id,stock_name,current_Price,changeFudu,open_Price,highest_Price,lowest_Price,transaction_Number,transaction_Price) Values ('"
                      + TextBox1.Text + "','"
                      + frm3.stock_name + "','"
                      + frm3.current_price + "','"
                      + frm3.changeFudu + "','"
                      + frm3.open_pricce + "','"
                      + frm3.highest_price + "','"
                      + frm3.lowest_price + "','"
                      + frm3.transaction_number + "','"
                      + frm3.transaction_price + "')";
            try
            {
                SQLHelper sh = new SQLHelper();
                int i = sh.operateTable(strInsert);
                if (i == 1)
                {
                    //加入成功，则加入按钮变灰，开放删除按钮
                    toolStripButton7.Enabled = false;
                    toolStripButton6.Enabled = true;
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //点击dgv里的单元格事件
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //空白行标记，默认给值-2，如果rowindex没有获取到行号，该标记就是-2
            blank = e.RowIndex;
            //点击行标题时，textbox为空
            _index=e.RowIndex;
            //若点击了标题
            if (_index == -1)
            {
                TextBox1.Text = string.Empty;
                //dataGridView1.ClearSelection();
                //dataGridView1.CurrentCell = null;
                //dataGridView1.Rows[0].Selected = false;
                toolStripButton6.Enabled = false;
            }
            
        }
        //条件选股
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            judgeZxg = false;
            if (frm1 != null)
            {
                frm1.Close();
            }
            tableName = "tb_" + DateTime.Now.ToString("yyyyMMdd");
            dataGridView1.Visible = true;
            //Select语句获得自选股数据库的股票，显示在DataGriView
            //scp成交价最小最大值，给个默认值防止填入空值，stp是成交额
            string stockMohu = TextBox_Mohu.Text;
            float scp1 = 0;
            float scp2 = 9999;
            double stp1 = 0;
            double stp2 = 9999999;
            if (TextBox_sp1.Text != string.Empty)
            { 
                scp1 = float.Parse(TextBox_sp1.Text);
            }
            if (TextBox_sp2.Text != string.Empty)
            {
                scp2 = float.Parse(TextBox_sp2.Text);
            }
            if (TextBox_tp1.Text != string.Empty)
            {
                stp1 = double.Parse(TextBox_tp1.Text);
            }
            if (TextBox_tp2.Text != string.Empty)
            {
                stp2 = double.Parse(TextBox_tp2.Text);
            }     
            string sqlSelect = "Select * From " + tableName + " Where stock_Name like '%"+stockMohu+"%' and current_Price between '"
                                +scp1 +"' and '"+scp2+"' and transaction_Price between '"
                                +stp1+"' and '"+stp2+"'";
            try
            {
                SQLHelper sh = new SQLHelper();
                DataTable dt = sh.getDataTable(sqlSelect);
                dtChangeName(dt);
                dataGridView1.DataSource = dt;
                dtFormat();
            }
            catch (Exception ex)
            {
                //如果没有当日数据，循环寻找上一个交易日有数据的数据表
                string error = ex.Message;//对象名  'tb_20200624' 无效。
                if (error.Substring(0, 3) == "对象名" && error.Substring(error.Length - 3) == "无效。")
                {
                    if ((int)MessageBox.Show("您还未导入今日行情，是否使用上个交易日的", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == 1)
                    {
                        int i = 1;
                        while (true)
                        {
                            tableName = "tb_" + DateTime.Now.AddDays(-i).ToString("yyyyMMdd");
                            string sqlSelect1 = "Select * From " + tableName + " Where stock_Name like '%" + stockMohu + "%' and current_Price between '"
                                    + scp1 + "' and '" + scp2 + "' and transaction_Price between '"
                                    + stp1 + "' and '" + stp2 + "'";
                            try
                            {
                                SQLHelper sh = new SQLHelper();
                                DataTable dt = sh.getDataTable(sqlSelect1);
                                dtChangeName(dt);
                                dataGridView1.DataSource = dt;
                                dataGridView1.Columns["涨跌幅"].DefaultCellStyle.Format = "0.00%";
                                break;
                            }
                            catch (Exception ex1)
                            {
                                error = ex1.Message;
                                if (error.Substring(0, 3) == "对象名" && error.Substring(error.Length - 3) == "无效。")
                                {
                                    i++;
                                }
                                else
                                {
                                    MessageBox.Show(ex1.Message);
                                }
                            }
                        }
                    }
                    else {
                        return;
                    }  
                }
            }
        }
    }
}
