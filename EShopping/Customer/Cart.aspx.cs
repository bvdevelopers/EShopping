using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EShopping.Customer
{
    public partial class Cart : System.Web.UI.Page
    {
        MySqlConnection connection;
        int cid = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartItems();
            }
        }

        private void LoadCartItems()
        {
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT p.* FROM purchase pu JOIN products p ON pu.pid = p.pid WHERE pu.cid = @cid AND status=0;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@cid", cid);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    string productCardHtml1 = @"<table><thead><tr><th>Name</th><th>Price</th><th>Quantity</th></tr></thead><tbody>";
                    LiteralControl productCard1 = new LiteralControl(productCardHtml1);
                    productContainer.Controls.Add(productCard1);

                    while (reader.Read())
                    {
                        string productName = reader.GetString("name");
                        int pid = reader.GetInt32("pid");
                        string price = reader.GetString("price");
                        string productCardHtml = $@"<tr><td>{productName}</td><td>{price}</td><td><input type='number' name='qnty_{pid}' min='1' value='1' /></td></tr>";
                        LiteralControl productCard = new LiteralControl(productCardHtml);
                        productContainer.Controls.Add(productCard);
                    }

                    string productmainCardHtml = @"</tbody></table>";
                    LiteralControl pmc = new LiteralControl(productmainCardHtml);
                    productContainer.Controls.Add(pmc);
                }
            }

         
          
        }

        protected void CalculateTotal(object sender, EventArgs e)
        {
            decimal totalAmount = 0;
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("qnty_"))
                {
                    int pid = int.Parse(key.Substring(5));
                    int quantity = int.Parse(Request.Form[key]);

                    string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT price FROM products WHERE pid = @pid;";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@pid", pid);

                        decimal price = 0;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                price = reader.GetDecimal("price");
                            }
                        }

                        decimal productTotal = price * quantity;
                        totalAmount += productTotal;
                        connection.Close();

                    }
                }
                string connectionString2 = "server=localhost;user=root;password=root;database=eshopping;";
                using (MySqlConnection connection = new MySqlConnection(connectionString2))
                {
                    connection.Open();
                    Random random = new Random();
                 
                    int prgrpid = random.Next(1000, 9999);
                    string query2 = "update purchase set status=1,prgrpid=@prgrpid where status=0 AND cid=@cid";
                    MySqlCommand cmd = new MySqlCommand(query2, connection);
                    cmd.Parameters.AddWithValue("@prgrpid", prgrpid);
                    cmd.Parameters.AddWithValue("@cid", cid);
                    
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }

            totalAmountLabel.Text = $"Total Amount: {totalAmount:C}";
           
            //Response.Redirect(Request.Url.AbsoluteUri);

        }
    }
}
