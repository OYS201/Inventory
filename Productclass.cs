using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory
{
    public class Productclass
    {
        static string myconn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        public int Product_id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Category { get; set; }
        public int Quantity { get; set; }

        private const string SelectQuery = "Select * from products";
        private const string InsertQuery = "Insert Into products(name, price,category_id,product_quantity) Values (@name,@price,@category_id,@product_quantity)";
        private const string UpdateQuery = "Update products set name=@name,price=@price,category_id=@category_id  where product_id=@product_id";
        private const string DeleteQuery = "Delete from products where product_id=@product_id";

        public DataTable GetProducts()
        {
            var datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(myconn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(SelectQuery, con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(datatable);
                    }
                }
            }
            return datatable;
        }
        public bool InsertProducts(Productclass c)
        {
            int rows;
            int shoerows;
            int bookrow;
            int clothrow;
            int drinkrow;
            using (SqlConnection con = new SqlConnection(myconn))
            {
                con.Open();
                SqlCommand sqlshoes = new SqlCommand("insert into category values('shoes')", con);
                SqlCommand sqlbook = new SqlCommand("insert into category values('book')", con);
                SqlCommand sqlcloathes = new SqlCommand("insert into category values('clothes')", con);
                SqlCommand sqldrinks = new SqlCommand("insert into category values('drinks')", con);
                

                using (SqlCommand com = new SqlCommand(InsertQuery, con))
                {
                    com.Parameters.AddWithValue("@name", c.Name);
                    com.Parameters.AddWithValue("@price", c.Price);
                    com.Parameters.AddWithValue("@category_id", c.Category);
                    //com.Parameters.AddWithValue("@product_quantity",c.Quantity);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }

        public bool UpdateProducts(Productclass c)
        {
            bool isSuccess = false;
            int rows;
            SqlConnection con = new SqlConnection(myconn);
            try {
                SqlCommand com = new SqlCommand(UpdateQuery, con);
                com.Parameters.AddWithValue("@name", c.Name);
                com.Parameters.AddWithValue("@price", c.Price);
                com.Parameters.AddWithValue("@category_id", c.Category);

                con.Open();
                rows = com.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else isSuccess = false;
               


            } catch(Exception ex) { }
            finally {
                con.Close();
            
            }
            return isSuccess;
        }
        public bool DeleteEmployee(Productclass c)
        {
            int rows;
            using (SqlConnection con = new SqlConnection(myconn))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(DeleteQuery, con))
                {
                    com.Parameters.AddWithValue("@product_id", c.Product_id);
                    rows = com.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;
        }

    }
}
