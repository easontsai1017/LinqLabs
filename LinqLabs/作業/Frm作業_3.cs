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

            foreach (var group in q)
            {
                string s = $"{group.MyYear}({group.MyCount})";
                TreeNode nods =  treeView1.Nodes.Add(group.MyYear.ToString(), s);
                foreach(var item in group.MyGroup)
                {
                    nods.Nodes.Add(item.ToString());
                }
            }

            #endregion
        }
    }
}
