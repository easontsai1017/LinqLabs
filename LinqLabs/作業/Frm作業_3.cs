using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
           
        }

         # region  int[] 分三群 -No LINQ
        private void button4_Click(object sender, EventArgs e)
        {
            //int[] 分三群 -No LINQ
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            treeView1.Nodes.Clear();
             foreach(int i in nums)
            {
                string s = TreeNods(i);
                if (treeView1.Nodes[s] == null)
                {
                    TreeNode node = null;
                    node = treeView1.Nodes.Add(s, s);
                    node.Nodes.Add(i.ToString());
                }
                else
                {
                    treeView1.Nodes[s].Nodes.Add(i.ToString());
                }
            }       
        }

         string TreeNods(int i)
        {
            if (i < 5)
                return "Small";
            if (i < 8)
                return "Medium";
            else
                return "Large";
        }
        #endregion
         #region Size
        private void button38_Click(object sender, EventArgs e)
        {//依 檔案大小 分組檔案 (大=>小) Length 10000  1000000  ...
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            //All
            this.dataGridView1.DataSource = files.OrderByDescending(f => f.Length).ToList();
            //分大小Length 10000  1000000  ...
            var q = from f in files
                    orderby f.Length descending
                    group f by Lengthfiles(f.Length) into g
                    select new 
                    { 
                     MyKey = g.Key,
                     MyCount = g.Count(),
                     MyGroup = g
                     };
            this.dataGridView2.DataSource = q.ToList();
            //TreeView
            treeView1.Nodes.Clear();
            foreach(var group in q)
            {
                string s = $"{group.MyKey}  -  ({group.MyCount})";
                TreeNode nods = treeView1.Nodes.Add(group.MyKey, s);
                foreach(var item in group.MyGroup)
                {
                    string s1 = $"{item} -({item.Length})";
                    nods.Nodes.Add(item.ToString(),s1);
                }
            }

        }

        private string Lengthfiles(long length)
        {
            if (length < 10000)
                return "Small_Files";
            else if (length < 1000000)
                return "Medium_Files";
            else
                return "Large_Files";
        }


        #endregion
         #region Year
        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView2.DataSource = null;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = files.OrderByDescending(f => f.CreationTime.Year).ToList();
            var q = from f in files
                    orderby f.CreationTime descending
                    group f by f.CreationTime.Year into g
                    select new
                    {
                    MyYear = g.Key,
                    MyCount = g.Count(),
                    MyGroup = g
                    };
            this.dataGridView2.DataSource = q.ToList();
            //TreeView
            treeView1.Nodes.Clear();

            foreach (var group in q)
            {
                string s = $"{group.MyYear}({group.MyCount})";
                TreeNode nods =  treeView1.Nodes.Add(group.MyYear.ToString(), s);
                foreach(var item in group.MyGroup)
                {
                    nods.Nodes.Add(item.ToString());
                }
            }

           

        }
        #endregion
         #region LINQ to Northwind Entity
        NorthwindEntities dbcontext = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)
        {//NW Products 低中高 價產品 
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            //UnitPrice 20 50 
            var q = dbcontext.Products.Select(p => p).ToList();
            this.dataGridView1.DataSource = q;
            var q1 = from p in dbcontext.Products.AsEnumerable()
                     orderby p.UnitPrice
                     group p by Unitprice(p.UnitPrice) into g
                     select new
                     {
                         MyKey = g.Key,
                         MyCount = g.Count(),
                         MyGroup = g
                     };
            this.dataGridView2.DataSource = q1.ToList();
            //TreeView
            treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.MyKey}  -  ({group.MyCount})";
                TreeNode nods = treeView1.Nodes.Add(group.MyKey.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    string s1 = $"{item.ProductName} -({item.UnitPrice:c2})";
                    nods.Nodes.Add(item.ToString(), s1);
                }
            }
        }
        private object Unitprice(decimal? unitPrice)
        {
            if (unitPrice < 20)
                return "Low_Price";
            else if (unitPrice < 50)
                return "Medium _Price";
            else
                return "High_Price";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Orders -  Group by 年
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            //All
            var q = dbcontext.Orders.Select(o => o).ToList();
            this.dataGridView1.DataSource = q;
            //year
            var q1= from o in dbcontext.Orders
                    group o by o.OrderDate.Value.Year into g
                    orderby g.Key descending
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView2.DataSource = q1.ToList();
            //TreeView
            treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.Year}  -  ({group.Count})";
                TreeNode nods = treeView1.Nodes.Add(group.Year.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                   string s1 = $"{item.CustomerID}";
                    nods.Nodes.Add(item.ToString(),s1);
                }
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {// Orders -  Group by 年/月
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            //All
            var q = dbcontext.Orders.Select(o => o).ToList();
            this.dataGridView1.DataSource = q;
            //year
            var q1 = from o in dbcontext.Orders
                     group o by new {o.OrderDate.Value.Year,o.OrderDate.Value.Month }  into g
                     orderby g.Key descending
                     select new
                     {
                         YearMonth = g.Key,
                         Count = g.Count(),
                         MyGroup = g

                     };
            this.dataGridView2.DataSource = q1.ToList();
            //TreeView
            treeView1.Nodes.Clear();
            foreach (var group in q1)
            {
                string s = $"{group.YearMonth}  -  ({group.Count})";
                TreeNode nods = treeView1.Nodes.Add(group.YearMonth.ToString(), s);
                foreach (var item in group.MyGroup)
                {
                    string s1 = $"{item.CustomerID}";
                    nods.Nodes.Add(item.ToString(),s1);
                }
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {//總銷售金額
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            //By Year
            var q = from od in dbcontext.Order_Details.AsEnumerable()
                    group od by od.Order.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        總銷售金額 =$"{g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}" 
                    };
            this.dataGridView1.DataSource = q.ToList();
            //All
            var q1 = from od in dbcontext.Order_Details.AsEnumerable()
                    group od by true into g
                    select new
                    {
                      
                        總銷售金額 = $"{g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}"
                    };
            this.dataGridView2.DataSource = q1.ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {//take(5) 銷售最好的top 5業務員
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            var q = (from od in dbcontext.Order_Details.AsEnumerable()
                     group od by new { od.Order.Employee.FirstName, od.Order.Employee.LastName } into g
                     orderby g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)) descending
                     select new
                     {
                         Employee = $"{g.Key.FirstName}{g.Key.LastName}",
                         Count = g.Count(),
                         Total = $"{g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}"
                     }).Take(5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //NW 產品最高單價前 5 筆 (包括類別名稱)
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            var q = (from p in dbcontext.Products.AsEnumerable()
                    orderby p.UnitPrice descending
                    select new 
                    {
                    CategoryName = p.Category.CategoryName,
                    p.ProductName,
                    UnitPrice = $"{p.UnitPrice:c2}"
                    }).Take(5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //     NW 產品有任何一筆單價大於300 ?
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            var q = from p in dbcontext.Products
                    where p.UnitPrice>300
                    select p;
            dataGridView1.DataSource = q.ToList(); //false

            bool result;
            result = dbcontext.Products.Any(p => p.UnitPrice > 300);
            MessageBox.Show("NW 產品有任何一筆單價大於300 : " + result);


        }
        #endregion
    }
}
