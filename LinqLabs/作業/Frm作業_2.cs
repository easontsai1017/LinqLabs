using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            
            InitializeComponent();
           
            this.productTableAdapter1.Fill(awDataSet1.Product);
            this.productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            this.productProductPhotoTableAdapter1.Fill(awDataSet1.ProductProductPhoto);
            LoadYearToComboBox();
        }

        void LoadYearToComboBox()
        {
            var q = awDataSet1.ProductPhoto.OrderBy(p => p.ModifiedDate.Year).Select(p => p.ModifiedDate.Year);
            comboBox3.Text = "請選擇年份";
            foreach (int i in q.ToList().Distinct())
            {
                comboBox3.Items.Add(i);
            }

        }
        void loadpicture()
        {
            try
            {
                var bytes = awDataSet1.ProductPhoto.Where(p => p.ProductPhotoID == (int)(dataGridView1.Rows[bindingSource1.Position].Cells["ProductPhotoID"].Value)).Select(p => p.LargePhoto);
                foreach (byte[] b in bytes)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ms.Write(b, 0, Convert.ToInt32(b.Length));
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            catch
            {

            }
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}  錯誤訊尋超出index


        }


        private void button11_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = null;
            pictureBox1.Image = null;
            var q = from p in awDataSet1.ProductPhoto
                    select p;
            
            bindingSource1.DataSource = q.ToList();
            dataGridView1.DataSource = bindingSource1;
            loadpicture();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            loadpicture();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "請選擇年份") 
            {
                MessageBox.Show("請選擇年份");
            }
            else
            {
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
                var q = awDataSet1.ProductPhoto.Where(y => y.ModifiedDate.Year.ToString() == comboBox3.Text);
                bindingSource1.DataSource = q.ToList();
                dataGridView1.DataSource = bindingSource1;
                loadpicture();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = null;
            pictureBox1.Image = null;
            var q = awDataSet1.ProductPhoto.Where(p => p.ModifiedDate > dateTimePicker1.Value && p.ModifiedDate < dateTimePicker2.Value);
            bindingSource1.DataSource = q.ToList();
            dataGridView1.DataSource = bindingSource1;
            loadpicture();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "請選擇年份")
            {
                MessageBox.Show("請選擇年份在選擇季數");
            }
            else
            {
                dataGridView1.DataSource = null;
                pictureBox1.Image = null;
                //var q = awDataSet1.ProductPhoto.Where(m => m.ModifiedDate.Year == int.Parse(comboBox3.Text) && (m.ModifiedDate.Month / 4) == comboBox2.SelectedIndex);
                var q = awDataSet1.ProductPhoto.Where(m => m.ModifiedDate.Month / 4 == comboBox2.SelectedIndex);
                bindingSource1.DataSource = q.ToList();
                dataGridView1.DataSource = bindingSource1;
                loadpicture();
                MessageBox.Show($"共有{dataGridView1.Rows.Count}筆", "此季筆數");
            }

        }
    }
}
