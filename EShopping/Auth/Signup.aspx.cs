using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace EShopping.Auth
{
    public partial class Signup : System.Web.UI.Page
    {
        //String s;
        MySqlConnection connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            connection = new MySqlConnection(connectionString);

        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string phno = txtphno.Text;
            string password = txtPassword.Text;

            if (username != "" && email != "" && phno != "" && password != "")
            {
                connection.Open();

                string query2 = "SELECT count(*) FROM customer_data as count  where email='" + email + "' or phno='" + phno + "';";
                MySqlCommand command1 = new MySqlCommand(query2, connection);
                command1.ExecuteNonQuery();
                int count = Convert.ToInt32(command1.ExecuteScalar());
                if (count == 0)
                {
                    string query = "Insert into customer_data (cname,email,phno,password) values ('" + username + "','" + email + "','" + phno + "','" + password + "')";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.ExecuteNonQuery();
                    connection.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Registered successfully');", true);

                    Response.Redirect("./Signin.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('email or phone number already exist');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('fill all the field');", true);
            }
        }
    }
}