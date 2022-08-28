using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventory
{
    public partial class products : Form
    {
        Productclass pro_class = new Productclass();
        int ID = 0;
        public products()
        {
            InitializeComponent();
            DisplayData();

            dataGridView1.DataSource = pro_class.GetProducts();
        }
        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        SqlConnection conn = new SqlConnection(myconn);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into products (product_id,name,price,category_id)  values(@product_id,@name,@price,@category_id);"
                 , conn);
                int i;
                if(Int32.TryParse(textBox4.Text, out i))
                {
                    cmd.Parameters.AddWithValue("@product_id", i);
                }
               
                int k = Convert.ToInt32(comboBox1.Text);
               
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@price", textBox3.Text);
                cmd.Parameters.AddWithValue("@category_id", k);

                dataGridView1.DataSource = pro_class.GetProducts();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                
                      // Clear controls once the employee is inserted successfully
                   MessageBox.Show("Employee has been added successfully");
                    dataGridView1.DataSource = pro_class.GetProducts();
              
                //;





                conn.Close();



            }

            else
            {
                MessageBox.Show("Please Fill in the form completely");
            }
        }
        private void ClearControls()
        {
            //textBox1.Text = "";
            //textBox3.Text = "";
            //textBox2.Text = "";



        }
        private void ClearData()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

          
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
               SqlCommand cmd = new SqlCommand("update products set name=@name,price=@price,category_id=@category_id  where product_id=@product_id", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@product_id", textBox4.Text);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@price", textBox3.Text);
                cmd.Parameters.AddWithValue("@category_id", comboBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                conn.Close();
                DisplayData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }


        }
        private void DisplayData()
        {
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from products", conn);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete products where product_id=@product_id", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@product_id", textBox4.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Record Deleted Successfully!");
            DisplayData();
            ClearData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textBox4.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
        }
    }
    }

