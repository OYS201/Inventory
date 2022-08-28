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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        SqlConnection conn = new SqlConnection(myconn);

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {

                // MessageBox.Show("Welcome!");
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from userlog inner join role on userlog.userrole=role.roleid where username=@username and password=@password;"
                 ,conn);
                cmd.Parameters.AddWithValue("@username",textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                SqlDataAdapter adapter=new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string usertype = dt.Rows[0][5].ToString();
                    if (usertype == "admin")
                    {
                        MessageBox.Show("Welcome Admin");
                        Dashboard check = new Dashboard();
                        check.Show();
                        Hide();
                    }
                }
                else
                {
                MessageBox.Show("Wrong Authentication");
                }
                conn.Close();
            }
            else
            {
             MessageBox.Show("Please Fill in the form completely");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Signup check = new Signup();
            check.Show();
            Hide();
        }
    }
}
