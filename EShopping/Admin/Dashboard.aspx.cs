using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EShopping.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        MySqlConnection connection;
        byte[] fileBytes;
        string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //lab1.Text = "hai";
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query = "SELECT image FROM products WHERE pid=1";
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                object imageData = command.ExecuteScalar();
                if (imageData != null)
                {
                    byte[] bytes = (byte[])imageData;
                    string base64String = Convert.ToBase64String(bytes);
                    imm.ImageUrl = "data:image/PNG;base64," + base64String;
                    lab1.Text = base64String;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

    }
}