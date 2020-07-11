using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.Data.SqlClient;
using System.Threading;
using System.Drawing;

namespace _0605_01
{
    public partial class Form1 : Form
    {
        //窗口自适应
        AutoSizeFormClass asc = new AutoSizeFormClass();     
        public Form1()
        {
            InitializeComponent();
            button5.Enabled = false;
            button_fst.Focus();
            button4.Enabled = false;
        }
        bool notFst = false;
        //全局变量的股票各数据
        public string stock_Id;
        public string stock_name;
        public string current_price;
        public string highest_price;
        public string lowest_price;
        public string transaction_number;
        public string transaction_price;
        public string open_pricce ;
        public string yesterdayPrice ;
        public string changePrice;
        public string changeFudu;
        //查询是否存于自选股库
        public void inMyStock()
        {
            //sql语句在自选股库中查询该股
            string strSelect = "Select stock_Id From MyStock Where stock_Id='"
                             + stock_Id + "'";
            SQLHelper sh = new SQLHelper();
            //结果显示DataGridView
            DataTable dt = sh.getDataTable(strSelect);
            //若存在则“加入自选股”按钮变灰
            if (dt.Rows.Count == 1)
            {
                button2.Enabled = false;
            }
            //若不存在则“删除自选股”按钮变灰
            if (dt.Rows.Count == 0)
            {
                button1.Enabled = false;
            }
        }
        public Color judgeColor(string a,string b)
        {
            if (double.Parse(a) > double.Parse(b))
            {
                return Color.Red;
            }           
            else if (double.Parse(a) < double.Parse(b))
            {
                return Color.Green;
            }
            else
            {
                return Color.Black;
            }
        }
        //得到Main传来的股票代码，通过新浪股票API获取股票数据，若能获取股票数据，返回1，若不能返回0
        public int getStock(string stockNumber)
        {
            
            stock_Id = stockNumber;
            //查看该股是否存于自选股库中
            inMyStock();
            //新浪股票API
            string url = "http://hq.sinajs.cn/list=" + stock_Id;
            //web的一些请求与响应
            WebRequest request = HttpWebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);

            //返回的样式：var hq_str_sh601006="大秦铁路,6.950,6.930,...,416900,7.000,2020-06-05,14:42:10,00,";
            string strHtml = reader.ReadToEnd();
            reader.Close();
            //用双引号分割strHtml，存放于list
            List<string> list = new List<string>(strHtml.Split('"'));
            //list[1]是股票信息，即：大秦铁路,6.950,6.930,...,416900,7.000,2020-06-05,14:42:10,00,
            string stock_detail = list[1];
            //判断是否该股数据为空，若空说明该股不存在或退市等，返回0
            if (stock_detail == "" || stock_detail=="FAILED")
            {
                return 0;
            }
            //用逗号分隔股票详细数据
            List<string> list_stock_detail = new List<string>(stock_detail.Split(','));
            
            stock_name = list_stock_detail[0];
            current_price = list_stock_detail[3]; 
            highest_price = list_stock_detail[4];
            lowest_price = list_stock_detail[5];
            transaction_number = list_stock_detail[8];
            transaction_price = (double.Parse(list_stock_detail[9])/10000.0).ToString();
            open_pricce = list_stock_detail[1];
            yesterdayPrice = list_stock_detail[2];
            changePrice = (double.Parse(current_price) - double.Parse(yesterdayPrice)).ToString("0.00");
            changeFudu = (((double.Parse(changePrice)) / (double.Parse(yesterdayPrice)))).ToString("0.0000");
            //买盘与卖盘
            string buyPrice1 = list_stock_detail[11];
            string buyNumber1 = list_stock_detail[10];
            string buyPrice2 = list_stock_detail[13];
            string buyNumber2 = list_stock_detail[12];
            string buyPrice3 = list_stock_detail[15];
            string buyNumber3 = list_stock_detail[14];
            string buyPrice4 = list_stock_detail[17];
            string buyNumber4 = list_stock_detail[16];
            string buyPrice5 = list_stock_detail[19];
            string buyNumber5 = list_stock_detail[18];
            string sellPrice1 = list_stock_detail[21];
            string sellNumber1 = list_stock_detail[20];
            string sellPrice2 = list_stock_detail[23];
            string sellNumber2 = list_stock_detail[22];
            string sellPrice3 = list_stock_detail[25];
            string sellNumber3 = list_stock_detail[24];
            string sellPrice4 = list_stock_detail[27];
            string sellNumber4 = list_stock_detail[26];
            string sellPrice5 = list_stock_detail[29];
            string sellNumber5 = list_stock_detail[28];
            //数据显示在label，并对数据按涨跌改色
            label_number.Text = stock_Id;
            label_name.Text = stock_name;
            label_finalP.Text = current_price;
            label_finalP.ForeColor = judgeColor(current_price, yesterdayPrice);
            label_tranN.Text = transaction_number+"股";
            label_highP.Text = highest_price;
            label_highP.ForeColor = judgeColor(highest_price, yesterdayPrice);
            label_LowP.Text = lowest_price;
            label_LowP.ForeColor = judgeColor(lowest_price, yesterdayPrice);
            label_openP.Text = open_pricce;
            label_openP.ForeColor = judgeColor(open_pricce, yesterdayPrice);
            label_TranP.Text = transaction_price+"万元";
            label_changeP.Text = changePrice;
            label_changeP.ForeColor = judgeColor(changePrice, "0");
            label_changeF.Text = (double.Parse(changeFudu))*100+"%";
            label_changeF.ForeColor = judgeColor(changeFudu, "0");
            label_bn1.Text = buyNumber1;//买一至买五量            
            label_bn2.Text = buyNumber2;            
            label_bn3.Text = buyNumber3;           
            label_bn4.Text = buyNumber4;           
            label_bn5.Text = buyNumber5;           
            label_bp1.Text = buyPrice1;//买一至买五价
            label_bp1.ForeColor = judgeColor(buyPrice1, yesterdayPrice);//涨红跌绿平黑
            label_bp2.Text = buyPrice2;
            label_bp2.ForeColor = judgeColor(buyPrice2, yesterdayPrice);
            label_bp3.Text = buyPrice3;
            label_bp3.ForeColor = judgeColor(buyPrice3, yesterdayPrice);
            label_bp4.Text = buyPrice4;
            label_bp4.ForeColor = judgeColor(buyPrice4, yesterdayPrice);
            label_bp5.Text = buyPrice5;
            label_bp5.ForeColor = judgeColor(buyPrice5, yesterdayPrice);
            label_sn1.Text = sellNumber1;//卖一至卖五量             
            label_sn2.Text = sellNumber2;           
            label_sn3.Text = sellNumber3;            
            label_sn4.Text = sellNumber4;           
            label_sn5.Text = sellNumber5;
            label_sp1.Text = sellPrice1;//卖一至卖五价
            label_sp1.ForeColor = judgeColor(sellPrice1, yesterdayPrice);//涨红跌绿平黑
            label_sp2.Text = sellPrice2;
            label_sp2.ForeColor = judgeColor(sellPrice2, yesterdayPrice);
            label_sp3.Text = sellPrice3;
            label_sp3.ForeColor = judgeColor(sellPrice3, yesterdayPrice);
            label_sp4.Text = sellPrice4;
            label_sp4.ForeColor = judgeColor(sellPrice4, yesterdayPrice);
            label_sp5.Text = sellPrice5;
            label_sp5.ForeColor = judgeColor(sellPrice5, yesterdayPrice);
 
            try
            {
                if(notFst==false)
                {
                    //通过新浪股票API获取分时图
                    string fstUrl = "http://image.sinajs.cn/newchart/min/n/" + stock_Id + ".gif";
                    WebRequest request1 = HttpWebRequest.Create(fstUrl);
                    //request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response1 = request1.GetResponse();
                    Stream stream1 = response1.GetResponseStream();
                    pictureBox1.Image = Image.FromStream(stream1);
                }   
            }
            catch (Exception ex)
            {
                //如果没有该股分时图或日K线，则说明该股退市，不显示该股，即返回0
                return 0;
            }
            //显示实时数据获取时间
            label2.Text = DateTime.Now.ToString("HH:mm:ss");
            //数据可以完全显示，即该股处于上市流通状态。则返回1
            return 1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            
        }
        //加入自选股方法
        private void button2_Click(object sender, EventArgs e)
        {
            //sql语句加入自选股数据库
            string strInsert = "Insert Into MyStock (stock_Id,stock_name,current_Price,changeFudu,open_Price,highest_Price,lowest_Price,transaction_Number,transaction_Price) Values ('"
                      + stock_Id + "','"
                      + stock_name + "','"
                      + current_price + "','"
                      + changeFudu + "','"
                      + open_pricce + "','"
                      + highest_price + "','"
                      + lowest_price + "','"
                      + transaction_number + "','"
                      + transaction_price + "')";
            try
            {
                SQLHelper sh = new SQLHelper();
                int i = sh.operateTable(strInsert);
                if (i == 1)
                {
                    //加入成功，则加入按钮变灰，开放删除按钮
                    MessageBox.Show("success");
                    button2.Enabled = false;
                    button1.Enabled = true;
                }
                else {
                    MessageBox.Show("Fail");
                }
            }
            catch (Exception ex)
            {
                //如果加入了重复自选股，则通过获取sql报错语句，显示存有状态（通过上述使按钮变灰的手段，已经防止了加入重复股情况，此处所写方法作为备用）
                if (ex.Message== "违反了 PRIMARY KEY 约束 'PK_MyStock'。不能在对象 'dbo.MyStock' 中插入重复键。\r\n语句已终止。")
                {
                    MessageBox.Show("已存有");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            
        }
        //删除自选股方法
        private void button1_Click_2(object sender, EventArgs e)
        {
            string strDelete = "Delete From MyStock Where stock_Id='"+stock_Id+"'";
            try
            {
                SQLHelper sh = new SQLHelper();
                int r = sh.operateTable(strDelete);
                if (r == 1)
                {
                    //删除成功，删除按钮变灰，开放加入按钮
                    MessageBox.Show("delete success!");
                    button2.Enabled = true;
                    button1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
        //窗体自适应
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize1(this);
        }
        //单次刷新
        private void button3_Click(object sender, EventArgs e)
        {
            getStock(stock_Id);
        }
        //等待方法
        public  void Delay(int mm)
        {    
            DateTime current = DateTime.Now;
            //将现在时间加10秒，若增加后的时间-现在时间>10s，则继续执行While语句，同时在等待期间可以使用自由使用其他功能，避免假死
            //若增加后的时间-现在时间=10s，则退出该方法，继续执行自动刷新方法
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                //当点击了停止刷新，即刻停止
                if (addHour == 24)
                {
                    return;
                }
                //防假死
                Application.DoEvents();
            }
            return;
        }
        //全局变量，为了方便停止刷新的一个增加量，使系统时间变为非交易时间即可停止刷新
        public int addHour = 0;
        //全局变量，点击自动刷新按钮时的小时数据
        int systemHour;
        //每秒刷新1方法
        public void F5_way()
        {
            addHour = 0;
            //记录点击按钮的小时数据
            systemHour = int.Parse(DateTime.Now.ToString("HH"));
            //MessageBox.Show("自动刷新启动");
            button5.Enabled = true;
            //测试时，注释下面判断
            //if (systemHour >= 9 && systemHour <= 15)
            //{
            //    MessageBox.Show("自动刷新启动");
            //}
            //else
            //{
            //    MessageBox.Show("不处于交易时间段");
            //    return;
            //}
            //当系统时间+增加时间在9-15时，即交易时间段内才可以刷新数据
            //若不点击停止刷新，增加时间=0，若点击停止，会通过加大增加时间使程序认为处于非交易时间，从而停止
            while (systemHour + addHour >= 9 && systemHour + addHour <= 20)
            {
                //获取实时数据
                getStock(stock_Id);
                //显示更新时间
                label2.Text = DateTime.Now.ToString("HH:mm:ss");
                //调用等待1秒方法（Thread会让程序假死，故用了自写方法）
                Delay(1000);
                //获取当前小时数，供下一次判断
                systemHour = int.Parse(DateTime.Now.ToString("HH"));
            }
        }
        //自动刷新方法
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("自动刷新启动");
            button4.Enabled = false;
            F5_way();
        }
        //停止方法，通过增大系统时间来实现
        private void button5_Click(object sender, EventArgs e)
        {
            addHour = 24;
            MessageBox.Show("已停止");
            button5.Enabled = false;
            button4.Enabled = true;
        }
        //显示分时图
        private void button_fst_Click(object sender, EventArgs e)
        {
            string fstUrl = "http://image.sinajs.cn/newchart/min/n/" + stock_Id + ".gif";
            WebRequest request1 = HttpWebRequest.Create(fstUrl);
            //request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response1 = request1.GetResponse();
            Stream stream1 = response1.GetResponseStream();
            pictureBox1.Image = Image.FromStream(stream1);
            notFst = false;
        }
        //显示日K
        private void button_rk_Click(object sender, EventArgs e)
        {
            //通过新浪股票API获取日k线
            string KUrl = "http://image.sinajs.cn/newchart/daily/n/" + stock_Id + ".gif";
            WebRequest request2 = HttpWebRequest.Create(KUrl);
            //request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response2 = request2.GetResponse();
            Stream stream2 = response2.GetResponseStream();
            pictureBox1.Image = Image.FromStream(stream2);
            notFst = true;
        }
        //显示周K
        private void button_zk_Click(object sender, EventArgs e)
        {
            string KUrl = "http://image.sinajs.cn/newchart/weekly/n/" + stock_Id + ".gif";
            WebRequest request2 = HttpWebRequest.Create(KUrl);
            //request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response2 = request2.GetResponse();
            Stream stream2 = response2.GetResponseStream();
            pictureBox1.Image = Image.FromStream(stream2);
            notFst = true;
        }
        //显示月K
        private void button_yk_Click(object sender, EventArgs e)
        {
            string KUrl = "http://image.sinajs.cn/newchart/monthly/n/" + stock_Id + ".gif";
            WebRequest request2 = HttpWebRequest.Create(KUrl);
            //request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response2 = request2.GetResponse();
            Stream stream2 = response2.GetResponseStream();
            pictureBox1.Image = Image.FromStream(stream2);
            notFst = true;
        }
        //窗口关闭时，必须停止刷新
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            addHour = 24;
        }
    }
}
