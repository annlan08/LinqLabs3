using LinqLabs.作業;
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
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };

            MakeCombox();
            
        }

        
        List<Student> students_scores;


        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        #region
        private void button14_Click(object sender, EventArgs e)
        {
            var q = FindFile().Where(f => f.Extension == ".log");            
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = FindFile().Where(f => f.CreationTime.Year == 2019 );
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var q = FindFile().Where(f => f.Length > 50000);
            this.dataGridView1.DataSource = q.ToList();
        }

        private System.IO.FileInfo[] FindFile()
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            return files;
        }
        #endregion

        #region
        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Orders.ToList();
            this.dataGridView2.DataSource = this.nwDataSet1.Order_Details.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") { return; }
            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                    select o;
            var p = from od in this.nwDataSet1.Order_Details
                    where od.OrdersRow.OrderDate.Year == Convert.ToInt32(comboBox1.Text)
                    select od;
            this.dataGridView1.DataSource = q.ToList();
            this.dataGridView2.DataSource = p.ToList();
        }

        private void MakeCombox()
        {        
            var q = from o in this.nwDataSet1.Orders
                    select o.OrderDate.Year;
            foreach (int item in q.Distinct())
            {
                comboBox1.Items.Add(item);
            }
        }
        #endregion

        #region
        private int SkipforBUTT = 0; //----->下一頁起始(加)
        private int SkipforUP = 0; //----->上一頁起始(減)

        private int MAX 
        {
            get { return this.nwDataSet1.Products.ToList().Count(); }
        }

        private void button12_Click(object sender, EventArgs e) //-------減
        {
            int T = Convert.ToInt32(textBox1.Text);

            SkipforBUTT = SkipforUP;
            SkipforUP = SkipforBUTT - T;

            
            if (SkipforBUTT <= 0) { SkipforUP = 0 ; SkipforBUTT = T;}
            Show_DataGrid(SkipforUP, T);
       
        }

        private void button13_Click(object sender, EventArgs e)//++++++++加
        {
            int T = Convert.ToInt32(textBox1.Text);
            if (SkipforUP+T >= MAX ) { return; }

            Show_DataGrid(SkipforBUTT, T);

            
            SkipforUP = SkipforBUTT;
            SkipforBUTT += T;
        }



        private void Show_DataGrid(int S,int T)
        {
            dataGridView2.DataSource = this.nwDataSet1.Products.Skip(S).Take(T).ToList();
        }

        #endregion


        private delegate int Grade(int a, int b, int c); 

        private void button3_Click(object sender, EventArgs e)
        {           
            List<Student> save = new List<Student>();

            var q = students_scores;
            dataGridView2.DataSource = q.ToList();
            

            int q1 = students_scores.Count;
            MessageBox.Show($"共{q1}個學員");

            
            var q2 = students_scores.OrderBy(s => s.Name).Take(3);
            dataGridView1.DataSource = q2.Take(3).ToList();
            MessageBox.Show($"找出 前面三個 的學員所有科目成績");


            var q3 = students_scores.OrderByDescending(s => s.Name).Take(2);
            dataGridView1.DataSource = q3.ToList();
            MessageBox.Show($"找出 後面兩個 的學員所有科目成績	");

            
            var q4 = from s in students_scores
                     where s.Name == "aaa"|| s.Name == "bbb" || s.Name == "ccc"
                     select s;
            dataGridView1.DataSource = q4.ToList();
            MessageBox.Show($"找出 Name 'aaa','bbb','ccc' 的學成績	");

            	                          
            var q5 = students_scores.Where(s => s.Name == "bbb");
            dataGridView1.DataSource = q5.ToList();
            MessageBox.Show($"找出學員 'bbb' 的成績");

            
            var q6 = students_scores.Where(s => s.Name != "bbb");
            dataGridView1.DataSource = q6.ToList();
            MessageBox.Show($"找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)");

            
            var q7 = students_scores.Where(s => s.Math < 60);
            dataGridView1.DataSource = q7.ToList();
            MessageBox.Show($"數學不及格是誰 ");


            Grade Max = delegate (int a, int b, int c)
              {            
                  int[] arr = { a, b, c };
                  for (int i = 1; i < arr.Length; i++) 
                  {
                      if (arr[i] > arr[0]) 
                      {
                          arr[0] = arr[i];
                      }
                  }
                  return arr[0];
              };
            Grade min = delegate (int a, int b, int c)
            {
                int[] arr = { a, b, c };
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] < arr[0])
                    {
                        arr[0] = arr[i];
                    }
                }
                return arr[0];
            };
            Grade sum = delegate (int a, int b, int c)
            {
                return a+b+c;
            };
            var q8 = students_scores.Select(n =>
              new
              {
                  Name = n.Name,
                  總和 = Max(n.Eng, n.Math, n.Chi),
                  平均 = avg(n.Eng, n.Math, n.Chi),
                  最大 = Max(n.Eng, n.Math, n.Chi),
                  最小 = min(n.Eng, n.Math, n.Chi)
              })  ;
            dataGridView1.DataSource = q8.ToList();
            MessageBox.Show($"所有人的總和平均最大最小");

            MakeCombox_NAME();
            MessageBox.Show("可以開始搜尋");

        }

        private double avg(int a,int b,int c)
        {
            return ((double)(a + b + c)) / 3;
        }

        private void MakeCombox_NAME()
        {
            var q = students_scores.Select(s => s.Name);
            foreach (string item in q.Distinct())
            {
                comboBoxName.Items.Add(item);
            }
        }

        //private void MakeCombox_Class()
        //{
        //    var q = students_scores.Select(s=>s.Eng)
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            var q = students_scores.Where(s => s.Name == comboBoxName.Text).Select(s => new { s.Name, s.Chi, s.Eng, s.Math });
            if (q == null) { MessageBox.Show("無資料");return; }
            dataGridView1.DataSource = q.ToList();
        }
    }
}
