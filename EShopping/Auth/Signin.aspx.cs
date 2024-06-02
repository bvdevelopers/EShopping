using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;

namespace EShopping.Auth
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear session on initial load
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (ValidateUser(username, password))
            {
                // Authentication successful
                Session["username"] = username; // Store user session
                Response.Redirect("../Customer/Products.aspx"); // Redirect to home page or dashboard
            }
            else
            {
                // Authentication failed
                Response.Write("<script>alert('Invalid username or password.');</script>");
            }
        }

        private bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            // Connection string (update with your actual database details)
            string connString = "server=localhost;user=root;password=root;database=eshopping;";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM customer_data WHERE email = @Email AND password = @Password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    isValid = result > 0;
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Response.Write("<script>alert('Error connecting to database.');</script>");
                }
            }

            return isValid;
        }
    }
}
