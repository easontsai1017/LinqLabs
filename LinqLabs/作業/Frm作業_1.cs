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

       

        

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            var q = from n in files
                    where n.Extension == ".log"
                    select n;
            this.dataGridView1.DataSource = q.ToList();
            

        }
        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.CreationTime.Year == 2020
                    select n;
            this.dataGridView1.DataSource = q.ToList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from n in files
                    where n.Length >= 1000000
                    select n;
            this.dataGridView1.DataSource = q.ToList();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            var q = from o in nwDataSet1.Orders where ! o.IsShippedDateNull()&& ! o.IsShipRegionNull() && ! o.IsShipPostalCodeNull() select o;
            this.dataGridView1.DataSource = q.ToList();
            var x = from od in nwDataSet1.Order_Details  select od;
            this.dataGridView2.DataSource = x.ToList();
                    

        }

        private void button1_Click(object sender, EventArgs e)
        {


            var x = from o in nwDataSet1.Orders
                    join d in nwDataSet1.Order_Details
                    on o.OrderID equals d.OrderID
                    where o.OrderDate.Year.ToString() == comboBox1.Text
                    select o;
            // select new {o.OrderID,d.ProductID,d.UnitPrice,d.Discount};


            this.dataGridView2.DataSource = x.ToList();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = from o in nwDataSet1.Orders
                    where !o.IsOrderDateNull() && !o.IsShipPostalCodeNull() && !o.IsShippedDateNull() && !o.IsShipRegionNull() && o.OrderDate.Year == int.Parse(comboBox1.Text)
                    select o;
            

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = bindingSource1;
        }

        int skip = 0;
        int take_new;
        int take_old;
        bool flag = true;
        
        private void button13_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text= "產品";
            take_new = int.Parse(textBox1.Text);
            if (skip >= this.nwDataSet1.Products.Rows.Count)
            {
                return;
            }

            if (flag == false)
            {
                flag = true;
                skip += take_old;
            }

            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_new).ToList();
            skip += take_new;
            take_old = take_new;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "產品";
            take_new = int.Parse(textBox1.Text);
          

            if (flag == true)
            {
                flag = false;
                skip -= take_old;
            }
            skip -= take_new;
            take_old = take_new; 
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(skip).Take(take_new).ToList();
            if (skip < 0)
            {
                skip = 0;
            }

        }
    }
}
