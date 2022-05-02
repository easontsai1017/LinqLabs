using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            LoadDataToComboBox();
        }

        private void LoadDataToComboBox()
        {
            comboBox1.Text = ("請選擇年份");

            var q = from o in nwDataSet1.Orders
                    select o.OrderDate.Year;

            foreach (var i in q)
            {
                if (comboBox1.Items.Contains(i))
                {
                    continue;
                }
                else
                {
                    comboBox1.Items.Add(i);
                }
            }
        }

        #region//LINQ to FileInfo[]
        private void button14_Click(object sender, EventArgs e)
        {
            //.log檔案
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            //var q = from n in files
            //        where n.Extension == ".log"
            //        select n;
            var q = files.Where(n => n.Extension == ".log");
            this.dataGridView1.DataSource = q.ToList();
            

        }
        private void button2_Click(object sender, EventArgs e)
        {

            //找年分2019
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            //var q = from n in files
            //        where n.CreationTime.Year == 2019
            //        select n;
            var q = files.Where(n => n.CreationTime.Year == 2019);
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //找大檔案
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            //var q = from n in files
            //        where n.Length >= 1000000
            //        select n;
            var q = files.Where(n => n.Length > 5000000);
            this.dataGridView1.DataSource = q.ToList();
        }
        #endregion

        #region order
        private void button6_Click(object sender, EventArgs e)
        {
            //all order
            var q = from o in nwDataSet1.Orders where ! o.IsShippedDateNull()&& ! o.IsShipRegionNull() && ! o.IsShipPostalCodeNull() select o;
            this.dataGridView1.DataSource = q.ToList();

            // oreder to order details
            var x = from od in nwDataSet1.Order_Details
                    where od.OrderID ==(int) dataGridView1.Rows[0].Cells[0].Value
                    select od;
            this.dataGridView2.DataSource = x.ToList();

            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // oreder to order details
            var x = from od in nwDataSet1.Order_Details
                    where od.OrderID == (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value
                    select od;
            this.dataGridView2.DataSource = x.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text== "請選擇年份")
            {
                MessageBox.Show("請選擇年份");
            }
            else
            {
                var x = from o in nwDataSet1.Orders
                        join d in nwDataSet1.Order_Details
                        on o.OrderID equals d.OrderID
                        where o.OrderDate.Year.ToString() == comboBox1.Text
                        select d;
                // select new {o.OrderID,d.ProductID,d.UnitPrice,d.Discount};


                this.dataGridView2.DataSource = x.ToList();
            }
           

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = from o in nwDataSet1.Orders
                    where !o.IsOrderDateNull() && !o.IsShipPostalCodeNull() && !o.IsShippedDateNull() && !o.IsShipRegionNull() && o.OrderDate.Year == int.Parse(comboBox1.Text)
                    select o;
            

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = bindingSource1;
        }
        #endregion

        #region product頁數第一種寫法
        //int skip = 0;
        //int take_new;
        //int take_old;
        //bool flag = true;

        //private void button13_Click(object sender, EventArgs e)
        //{
        //    this.lblMaster.Text= "產品";
        //    take_new = int.Parse(textBox1.Text);
        //    if (skip >= this.nwDataSet1.Products.Rows.Count)
        //    {
        //        return;
        //    }

        //    if (flag == false)
        //    {
        //        flag = true;
        //        skip += take_old;
        //    }

        //    this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_new).ToList();
        //    skip += take_new;
        //    take_old = take_new;
        //}

        //private void button12_Click(object sender, EventArgs e)
        //{
        //    this.lblMaster.Text = "產品";
        //    take_new = int.Parse(textBox1.Text);


        //    if (flag == true)
        //    {
        //        flag = false;
        //        skip -= take_old;
        //    }
        //    skip -= take_new;
        //    take_old = take_new; 
        //    this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_new).ToList();
        //    if (skip < 0)
        //    {
        //        skip = 0;
        //    }

        //}
        #endregion
        #region product p2
        int count = 0;
        private void button13_Click(object sender, EventArgs e)
        {//下一頁
            int pages =int.Parse( textBox1.Text);
            if (count < (nwDataSet1.Products.Rows.Count / pages) + 1)
            {
                var q = from p in nwDataSet1.Products.Skip(count * pages).Take(pages)
                        select p;
                count += 1;
                this.dataGridView1.DataSource = q.ToList();
                dataGridView1.CellClick += DataGridView1_CellClick1;
            }
            else
            {
                MessageBox.Show("已到達最後一頁", "Error");
            }


        }

        private void DataGridView1_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            //選擇product ID to order_detials
            var q = from p in nwDataSet1.Products
                    join od in nwDataSet1.Order_Details
                    on p.ProductID equals od.ProductID
                    where p.ProductID==(int)dataGridView1.Rows[e.RowIndex].Cells[0].Value
                    select od;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //上一頁
            int pages = int.Parse(textBox1.Text);
            if(count> 1)
            {
                var q = from p in nwDataSet1.Products.Skip((count - 2) * pages).Take(pages)
                        select p;
                count -= 1;
                this.dataGridView1.DataSource = q.ToList();
            }
            else
            {
                MessageBox.Show("已到達第一頁", "Error");

            }

        }

        #endregion
    }
}
