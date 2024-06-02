using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;

namespace EShopping.Customer
{
    public partial class Products : System.Web.UI.Page
    {
        MySqlConnection connection;
        int cid = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
           
                string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT p.*, COALESCE(pc.purchaseCount, 0) AS purchaseCount FROM products p LEFT JOIN (SELECT pid, COUNT(*) AS purchaseCount FROM purchase WHERE status=0 GROUP BY pid) pc ON p.pid = pc.pid";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productName = reader.GetString("name");
                        string productQuantity = reader.GetString("quantity");
                        string productPrice = reader.GetString("price");
                        byte[] productImage = null;
                        int blobColumnIndex = reader.GetOrdinal("image"); // Replace 'image' with your actual column name
                        if (!reader.IsDBNull(blobColumnIndex))
                        {
                            long length = reader.GetBytes(blobColumnIndex, 0, null, 0, 0); // Get the length of the data
                            productImage = new byte[length];
                            reader.GetBytes(blobColumnIndex, 0, productImage, 0, (int)length); // Read the data into the byte array
                        }
                        string imageUrl = GetImageUrl(productImage);

                        int purchaseCount = reader.GetInt32("purchaseCount");

                        // Create the card's HTML
                        string productCardHtml = $@"
                            <div class='card'>
                                <img src='{imageUrl}' alt='Product Image' height='50px' width='50px'/>
                                <h5 class='card-title'>{productName}</h5>
                                <p class='card-text'>{productQuantity}</p>
                                <p class='card-text'>Price: {productPrice}</p>";

                        LiteralControl productCard = new LiteralControl(productCardHtml);
                        productContainer.Controls.Add(productCard);

                        if (purchaseCount == 0)
                        {
                            // Create the button dynamically
                            Button btnAddToCart = new Button();
                            btnAddToCart.ID = "btnAddToCart_" + reader.GetInt32("pid");
                            btnAddToCart.Text = "Add to cart";
                            btnAddToCart.CommandArgument = reader.GetInt32("pid").ToString();
                            btnAddToCart.Click += new EventHandler(btnAddToCart_Click);
                            productContainer.Controls.Add(btnAddToCart);
                        }
                        else
                        {
                            LiteralControl purchaseLabel = new LiteralControl($"<label>{purchaseCount} purchase(s)</label>");
                            productContainer.Controls.Add(purchaseLabel);
                        }

                        LiteralControl productCardEnd = new LiteralControl("</div>");
                        productContainer.Controls.Add(productCardEnd);
                    }
                }

                connection.Close();
            
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int productId = Convert.ToInt32(clickedButton.CommandArgument);
                InsertIntoCart(productId);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        private void InsertIntoCart(int productId)
        {
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO purchase (cid, pid, status) VALUES (@cid, @productId, @status)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@cid", cid);
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@status", 0);
            command.ExecuteNonQuery();
            connection.Close();
        }

        protected string GetImageUrl(object imageData)
        {
            if (imageData == DBNull.Value || imageData == null)
            {
                return ""; // Return empty string if image data is null
            }
            else
            {
                byte[] bytes = (byte[])imageData;
                string base64String = Convert.ToBase64String(bytes);
                return "data:image;base64," + base64String; // Return base64 encoded image URL
            }
        }
    }
}
