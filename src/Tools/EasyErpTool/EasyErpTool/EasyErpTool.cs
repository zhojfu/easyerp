using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EasyErpTool
{
    public partial class EasyErpTool : Form
    {
        public EasyErpTool()
        {
            InitializeComponent();
        }

        private void SalesOutput_Click(object sender, EventArgs e)
        {
            string dateTime = this.dateTimePicker.Text;
            //this.dateTimePicker.
            DateTime date = DateTime.Parse(dateTime, CultureInfo.CurrentCulture);
            GetSalesRecord(date); 
        }

        private void GetSalesRecord(DateTime date)
        {
            SqlConnection cnn = new SqlConnection
            {
                ConnectionString =
                    "Initial Catalog=kmjxc;Integrated Security=True;Data Source=localhost;connect Timeout=20"
            };

            cnn.Open();

            var range = GetRangeOfWeek(date);

            const string fields = @"dbo.pos_t_daysum.item_no, dbo.pos_t_daysum.oper_date, dbo.pos_t_daysum.sale_qnty, dbo.pos_t_daysum.sale_price, dbo.pos_t_daysum.sale_amount, dbo.bi_t_item_info.base_price";

            string strSql = string.Format("SELECT {0} FROM dbo.pos_t_daysum, dbo.bi_t_item_info WHERE dbo.pos_t_daysum.oper_date >='{1}' AND dbo.pos_t_daysum.oper_date <='{2}' AND dbo.pos_t_daysum.item_no = dbo.bi_t_item_info.item_no", fields, range.Item1, range.Item2);

            SqlCommand myCommand = new SqlCommand(strSql, cnn);
            var reader = myCommand.ExecuteReader();
           
            var root = new XElement("sale_records");
            while (reader.Read())
            {
                var item = new XElement("sale_item",
                    new XElement("item_number", reader["item_no"].ToString()),
                    new XElement("date", reader["oper_date"].ToString()),
                    new XElement("sale_quantity", reader["sale_qnty"].ToString()),
                    new XElement("sale_price", reader["sale_price"].ToString()),
                    new XElement("sale_amount", reader["sale_amount"].ToString()),
                    new XElement("cost_price", reader["base_price"].ToString())
                    );
               root.Add(item); 
            }
            
            string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            string fileName = string.Format("saleRecord{0}.erp", date.Month);
            string path = Path.Combine(dataDirectory, fileName);
            root.Save(path);
            MessageBox.Show(string.Format("销售记录已经导出在{0}",path));
            cnn.Close();
        }

        private Tuple<string, string> GetRangeOfWeek(DateTime date)
        {
            DateTime start = new DateTime(date.Year, date.Month, 1);
            DateTime end = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            string strStart = string.Format("{0:yyyyMMdd}", start);
            string strEnd = string.Format("{0:yyyyMMdd}", end);
            return new Tuple<string, string>(strStart, strEnd);
        }

        private void InputProducts_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = @"easyerp(*.xml)|*.xml";
            this.openFileDialog.ShowDialog();
            Stream file = this.openFileDialog.OpenFile();

            XDocument doc = XDocument.Load(file);
            
            var products = doc.Descendants("Product");
            var node = doc.Root.Element("Categories");
            List<XElement> categories = new List<XElement>();
            if (node != null)
            {
                categories = node.Descendants("Category").ToList();
            }

            SqlConnection cnn = new SqlConnection
            {
                ConnectionString =
                    "Initial Catalog=kmjxc;Integrated Security=True;Data Source=localhost;connect Timeout=20"
            };

            cnn.Open();

            SqlCommand cmd = new SqlCommand
            {
                Connection = cnn,
                Transaction = cnn.BeginTransaction(),
            };

            cmd.CommandText = "DELETE FROM dbo.bi_t_item_info";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM dbo.bi_t_item_cls";
            cmd.ExecuteNonQuery();

            SqlParameter[] paras =
            {
                new SqlParameter ("@item_no",SqlDbType.Text, 13),
                new SqlParameter ("@item_subno",SqlDbType.Text, 15),
                new SqlParameter ("@item_name",SqlDbType.Text, 40),
                new SqlParameter ("@item_subname",SqlDbType.Text, 20),
                new SqlParameter ("@item_clsno",SqlDbType.Text, 50),
                new SqlParameter ("@price",SqlDbType.Decimal, 14),
                new SqlParameter ("@base_price",SqlDbType.Decimal, 14),
                new SqlParameter ("@sale_price",SqlDbType.Decimal, 14),
                new SqlParameter ("@display_flag",SqlDbType.Char, 1),
                new SqlParameter ("@combine_sta",SqlDbType.Char, 1),
                new SqlParameter ("@status",SqlDbType.Char, 1)
            };
            try
            {
                cmd.CommandText =
                    "INSERT INTO dbo.bi_t_item_info(item_no, item_subno, item_name, item_subname, item_clsno, price, base_price, sale_price, display_flag, combine_sta, status) VALUES(@item_no, @item_subno, @item_name, @item_subname, @item_clsno, @price, @base_price, @sale_price, @display_flag, @combine_sta, @status)";
          
                foreach (var product in products)
                {
                    string itemNo = product.Elements("ItemNo").Select(t => t.Value).FirstOrDefault();
                    string name = product.Elements("Name").Select(t => t.Value).FirstOrDefault();
                    string categoryNo = product.Elements("CategoryNo").Select(t => t.Value).FirstOrDefault();
                    string gtin = product.Elements("Gtin").Select(t => t.Value).FirstOrDefault();
                    double price = product.Elements("Price").Select(t => (double)t).FirstOrDefault();
                    double productCost = product.Elements("ProductCost").Select(t => (double)t).FirstOrDefault();
                    paras[0].Value = itemNo;
                    paras[1].Value = gtin;
                    paras[2].Value = name;
                    paras[3].Value = name;
                    paras[4].Value = categoryNo;
                    paras[5].Value = productCost;
                    paras[6].Value = productCost;
                    paras[7].Value = price;
                    paras[8].Value = 1;
                    paras[9].Value = 0;
                    paras[10].Value = 1;

                    cmd.Parameters.Clear();
                    foreach (SqlParameter para in paras)
                    {
                        cmd.Parameters.Add(para);
                    }
               
                    cmd.ExecuteNonQuery();
                 }

                SqlParameter[] paraCategory =
                {
                    new SqlParameter("@item_clsno", SqlDbType.Char, 6),
                    new SqlParameter("@item_clsname", SqlDbType.VarChar, 20),
                    new SqlParameter("@item_flag", SqlDbType.Char, 1),
                    new SqlParameter("@display_flag", SqlDbType.Char, 1)
                };
                cmd.CommandText = "INSERT INTO dbo.bi_t_item_cls(item_clsno, item_clsname, item_flag, display_flag) VALUES(@item_clsno, @item_clsname, @item_flag, @display_flag)";
               
                foreach (var category in categories)
                {
                    string itemNo = category.Elements("ItemNo").Select(t => t.Value).FirstOrDefault();
                    string name = category.Elements("Name").Select(t => t.Value).FirstOrDefault();
                    paraCategory[0].Value = itemNo;
                    paraCategory[1].Value = name;
                    paraCategory[2].Value = 0;
                    paraCategory[3].Value = 1;
                    cmd.Parameters.Clear();
                    foreach (SqlParameter para in paraCategory)
                    {
                        cmd.Parameters.Add(para);
                    }
                  
                    cmd.ExecuteNonQuery();
                }
                
                cmd.Transaction.Commit();
                MessageBox.Show("产品目录成功导入收银机"); 
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                MessageBox.Show(ex.ToString());
                MessageBox.Show("数据导入失败，请联系管理员");
            }
            finally
            {
               cnn.Close();
            }
        }
    }
}
