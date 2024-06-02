using MySql.Data.MySqlClient;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EShopping.Customer
{
    public partial class Cart : System.Web.UI.Page
    {
        private MySqlConnection connection;
        private int cid = 2;

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
                string query = "SELECT p.*, COUNT(pu.pid) as quantity FROM purchase pu JOIN products p ON pu.pid = p.pid WHERE pu.cid = @cid AND pu.status=0 GROUP BY pu.pid;";
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
                        decimal price = reader.GetDecimal("price");
                        int quantity = reader.GetInt32("quantity");
                        string productCardHtml = $@"<tr><td>{productName}</td><td>{price:C}</td><td><input type='number' name='qnty_{pid}' min='1' value='{quantity}' /></td></tr>";
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
                    }
                }
            }

            string updateQuery = "UPDATE purchase SET status = 1, prgrpid = @prgrpid WHERE status = 0 AND cid = @cid";
            string connectionString2 = "server=localhost;user=root;password=root;database=eshopping;";
            using (MySqlConnection connection = new MySqlConnection(connectionString2))
            {
                connection.Open();
                Random random = new Random();
                int prgrpid = random.Next(1000, 9999);
                MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
                cmd.Parameters.AddWithValue("@prgrpid", prgrpid);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.ExecuteNonQuery();
            }

            totalAmountLabel.Text = $"Total Amount: {totalAmount:C}";
        }
    }
}
