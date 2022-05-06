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
    public partial class Frm_考試 : Form
    {
        public Frm_考試()
        {
            InitializeComponent();
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }
        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            #region 搜尋 班級學生成績


            // 共幾個 學員成績 ?			
            MessageBox.Show("共幾個學員成績 ", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x = from n in students_scores
                    group n by new { n.Name, n.Eng, n.Chi, n.Math } into g
                    select new
                    {
                        Name = g.Key.Name,
                        英文 = g.Key.Eng,
                        國文 = g.Key.Chi,
                        數學 = g.Key.Math
                    };
             
            foreach(var c in x)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }

            // 找出 前面三個 的學員所有科目成績	
            MessageBox.Show("前面三個的學員所有科目成績 ", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x1 = from n in students_scores.Take(3)
                    group n by new { n.Name, n.Eng, n.Chi, n.Math } into g
                    select new   
                    {
                        Name = g.Key.Name,
                        英文 = g.Key.Eng,
                        國文 = g.Key.Chi,
                        數學 = g.Key.Math
                    };

            foreach (var c in x1)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
            // 找出 後面兩個 的學員所有科目成績					
            MessageBox.Show("後面兩個的學員所有科目成績", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x2 = from n in students_scores.Skip(students_scores.Count-2)
                     group n by new { n.Name, n.Eng, n.Chi, n.Math } into g
                     select new
                     {
                         Name = g.Key.Name,
                         英文 = g.Key.Eng,
                         國文 = g.Key.Chi,
                         數學 = g.Key.Math
                     };

            foreach (var c in x2)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						
            MessageBox.Show("找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x3 = from n in students_scores
                     where n.Name=="aaa"||n.Name=="bbb"||n.Name=="ccc"
                     select new
                     {
                         Name = n.Name,
                         英文 = n.Eng,
                         國文 = n.Chi,
                         數學 = n.Math
                     };

            foreach (var c in x3)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
            // 找出學員 'bbb' 的成績	                          
            MessageBox.Show("找出學員 'bbb' 的成績", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x4 = from n in students_scores
                     where n.Name == "bbb" 
                     select new
                     {
                         Name = n.Name,
                         英文 = n.Eng,
                         國文 = n.Chi,
                         數學 = n.Math
                     };

            foreach (var c in x4)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            MessageBox.Show("找出除了 'bbb' 學員的學員的所有成績", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x5 = from n in students_scores
                     where n.Name != "bbb"
                     select new
                     {
                         Name = n.Name,
                         英文 = n.Eng,
                         國文 = n.Chi,
                         數學 = n.Math
                     };

            foreach (var c in x5)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.英文);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |	
            MessageBox.Show("找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x6 = from n in students_scores
                     where n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc"
                     select new
                     {
                         Name = n.Name,
                         國文 = n.Chi,
                         數學 = n.Math
                     };

            foreach (var c in x6)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.國文);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
            // 數學不及格 ... 是誰 
            MessageBox.Show("數學不及格 ... 是誰 ", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x7 = from n in students_scores
                     where n.Math<60
                     select new
                     {
                         Name = n.Name,
                         數學 = n.Math
                     };

            foreach (var c in x7)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.數學);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }
           
           
        }
 #endregion
        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            MessageBox.Show("個人 sum, min, max, avg ", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var x = from n in students_scores
                    select new
                    {
                        Name = n.Name,
                        Sum = (n.Math+n.Eng+n.Chi),
                        Avg = (n.Math + n.Eng + n.Chi)/3,
                        Max = Math.Max(Math.Max(n.Math,n.Eng),n.Chi),
                        Min = Math.Min(Math.Min(n.Math, n.Eng), n.Chi),
                    };
            this.dataGridView1.DataSource = x.ToList();
            
            foreach (var c in x)
            {
                this.chart1.Series.Add(c.Name);
                this.chart1.Series[c.Name].Points.AddXY("英文", c.Sum);
                this.chart1.Series[c.Name].Points.AddXY("國文", c.Avg);
                this.chart1.Series[c.Name].Points.AddXY("數學", c.Max);
                this.chart1.Series[c.Name].Points.AddXY("平均", c.Min);
                this.chart1.Series[c.Name].IsValueShownAsLabel = true;
            }

            //各科 sum, min, max, avg
            MessageBox.Show("各科 sum, min, max, avg ", "Message", MessageBoxButtons.OK);
            this.chart1.Series.Clear();
            var q = from n in students_scores
                    group n by true into g
                    select new
                    { 
                        國文總和 = g.Sum(n=>n.Chi),
                        國文Max = g.Max(n => n.Chi),
                        國文Min = g.Min(n => n.Chi),
                        國文Avg = $"{g.Average(n => n.Chi):f2}",

                        英文總和 = g.Sum(n => n.Eng),
                        英文Max = g.Max(n => n.Eng),
                        英文Min = g.Min(n => n.Eng),
                        英文Avg = $"{g.Average(n => n.Eng):f2}",

                        數學總和 = g.Sum(n => n.Math),
                        數學Max = g.Max(n => n.Math),
                        數學Min = g.Min(n => n.Math),
                        數學Avg = $"{g.Average(n => n.Math):f2}",

                    };

            this.dataGridView1.DataSource = q.ToList();
            foreach(var c in q)
            {
                this.chart1.Series.Add("國文");
                this.chart1.Series["國文"].Points.AddXY("Sum", c.國文總和);
                this.chart1.Series["國文"].Points.AddXY("Min", c.國文Min);
                this.chart1.Series["國文"].Points.AddXY("Max", c.國文Max);
                this.chart1.Series["國文"].Points.AddXY("Avg", c.國文Avg);

                this.chart1.Series.Add("英文");
                this.chart1.Series["英文"].Points.AddXY("Sum", c.英文總和);
                this.chart1.Series["英文"].Points.AddXY("Min", c.英文Min);
                this.chart1.Series["英文"].Points.AddXY("Max", c.英文Max);
                this.chart1.Series["英文"].Points.AddXY("Avg", c.英文Avg);

                this.chart1.Series.Add("數學");
                this.chart1.Series["數學"].Points.AddXY("Sum", c.數學總和);
                this.chart1.Series["數學"].Points.AddXY("Min", c.數學Min);
                this.chart1.Series["數學"].Points.AddXY("Max", c.數學Max);
                this.chart1.Series["數學"].Points.AddXY("Avg", c.數學Avg);

                this.chart1.Series["國文"].IsValueShownAsLabel = true;
                this.chart1.Series["英文"].IsValueShownAsLabel = true;
                this.chart1.Series["數學"].IsValueShownAsLabel = true;

            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 

            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }
        NorthwindEntities dbcontext = new NorthwindEntities();
        private void button34_Click(object sender, EventArgs e)
        {
            // 年度最高銷售金額 年度最低銷售金額  Max Min

            MessageBox.Show("年度最低銷售金額  Max Min ", "Message", MessageBoxButtons.OK);
            var q = from o in dbcontext.Orders.AsEnumerable()
                    group o by o.OrderDate.Value.Year into g
                    orderby g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) ascending
                    select new
                    {
                        Year = g.Key,
                        Max =$"{g.Max(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}" ,
                        Min =$"{g.Min(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}",
                        Total =$"{g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}"

                    };
            this.dataGridView1.DataSource = q.ToList();

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
          

            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            var q1 = from o in dbcontext.Orders.AsEnumerable()
                     group o by o.OrderDate.Value.Year + "年" + o.OrderDate.Value.Month + "月" into g
                     orderby g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) ascending

                     select new
                     {
                         Year = g.Key,
                         Max = $"{g.Max(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}",
                         Min = $"{g.Min(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}",
                         Total = $"{g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))):c2}"

                     };
            this.dataGridView2.DataSource = q1.ToList();




            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
            MessageBox.Show("每年/月 總銷售分析 圖  ", "Message", MessageBoxButtons.OK);
            this.chart1.DataSource = null;
            
            
            var p = from od in dbcontext.Order_Details.AsEnumerable()
                     group od by od.Order.OrderDate.Value.Year + "年" + od.Order.OrderDate.Value.Month + "月" into g
                     select new
                     {
                         Mykey = g.Key,
                         Total = $"{g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}"
                     };
            this.chart1.DataSource = p.ToList();
            this.dataGridView1.DataSource = p.ToList();
            this.chart1.Series[0].XValueMember = "Mykey";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].YValueMembers = "Total";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;


            

            var p1 = from od in dbcontext.Order_Details.AsEnumerable()
                     group od by od.Order.OrderDate.Value.Year + "年" into g
                     select new
                     {
                         Mykey = g.Key,
                         Total = $"{g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}"
                     };
            this.chart2.DataSource = p1.ToList();
            this.dataGridView2.DataSource = p1.ToList();
            this.chart2.Series[0].XValueMember = "Mykey";
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart2.Series[0].YValueMembers = "Total";
            this.chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }
    }
}
