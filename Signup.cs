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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }
        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        SqlConnection conn = new SqlConnection(myconn);
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into userlog values(@username,@password,@userrole", conn);

                string admin = "admin";
                string attendant = "attendant";
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                if (comboBox1.SelectedItem.ToString() == "admin") {
                cmd.Parameters.AddWithValue("@userrole",1);
                }
                else if(comboBox1.SelectedItem.ToString() == "attendant")
                {
                cmd.Parameters.AddWithValue("@userrole", 2);
                }
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Successful");
                    }
                    else
                    {
                        MessageBox.Show("Error in registring");
                    }
                }
                else
                {
                    MessageBox.Show("Please Fill In the Forms");
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
