using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            // Hardcoded credentials (replace with actual authentication logic)
            string expectedUsername = "admin";
            string expectedPassword = "password";

            if (username == expectedUsername && password == expectedPassword)
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
    }
}