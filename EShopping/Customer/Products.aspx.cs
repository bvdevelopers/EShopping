using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EShopping.Admin;
using static EShopping.Customer.Products;
using Newtonsoft.Json;


namespace EShopping.Customer
{
    public partial class Products : System.Web.UI.Page
    {
        MySqlConnection connection;
        int cid = 2;
        int purchaseCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT p.*, COALESCE(pc.purchaseCount, 0) AS purchaseCount FROM products p LEFT JOIN (SELECT pid, COUNT(*) AS purchaseCount FROM purchase where status=0 GROUP BY pid) pc ON p.pid = pc.pid";
            MySqlCommand command = new MySqlCommand(query, connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                string productCardHtml1 = $@"<div class='maincard'>";
                LiteralControl productCard1 = new LiteralControl(productCardHtml1);
                productContainer.Controls.Add(productCard1);


                while (reader.Read())
                {
                    string productName = reader.GetString("name");
                    string productQuantity = reader.GetString("quantity");
                    string productPrice = reader.GetString("price");
                    // Read the BLOB data
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

                   string productCardHtml = $@"

                    <div class='card'>
                                <img src='{imageUrl}' alt='Product Image' height='50px' width='50px'/>
                       <h5 class='card-title'>{productName}</h5>
                        <p class='card-text'>{productQuantity}</p>
                        <p class='card-text'>Price: {productPrice}</p>";

                    if (purchaseCount == 0)
                    {
                        productCardHtml += @"<asp:Button ID='btnAddToCart_" + reader.GetInt32("pid") + @"' runat='server' Text='Add to cart' />";


                    }
                    else
                    {
                        productCardHtml += $@"<label>{purchaseCount} purchase(s)</label>";


                    }
                    productCardHtml += @"</div>";
                    LiteralControl productCard = new LiteralControl(productCardHtml);
                    productContainer.Controls.Add(productCard);

                    if (purchaseCount == 0)
                    {
                        Button btnAddToCart = new Button();
                        btnAddToCart.ID = "btnAddToCart_" + reader.GetInt32("pid");
                        btnAddToCart.Text = "Add to cart";
                        btnAddToCart.Click += new EventHandler(btnAddToCart_Click);
                        productContainer.Controls.Add(btnAddToCart);
                    }
                }
                string productmainCardHtml = @"</div>";
                LiteralControl pmc = new LiteralControl(productmainCardHtml);
                productContainer.Controls.Add(pmc);

            }

            connection.Close();
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string productId = clickedButton.ID.Split('_')[1];
                // Handle the add to cart logic here
                lbl.Text = "Product ID " + productId + " added to cart.";
                InsertIntoCart(Convert.ToInt16(productId));
                Response.Redirect(Request.Url.AbsoluteUri);

            }
        }
        private void InsertIntoCart(int productId)
        {
            connection.Open();
            string qrey = "insert into purchase (cid,pid,status) values (@cid,@productId,@status)";
            MySqlCommand command3 = new MySqlCommand(qrey, connection);
            command3.Parameters.AddWithValue("@cid", cid);
            command3.Parameters.AddWithValue("@productId", productId);
            command3.Parameters.AddWithValue("@status", 0);
            command3.ExecuteNonQuery();
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