using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace EShopping.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        MySqlConnection connection;
        byte[] fileBytes;
        string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
             connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            connection = new MySqlConnection(connectionString);

            
                // Fetch data from the database
                DataTable dt = GetDataFromDatabase(); // Implement your own method to fetch data

                foreach (DataRow row in dt.Rows)
                {
                    TableRow tableRow = new TableRow();

                    TableCell cell1 = new TableCell();
                    cell1.Text = row["name"].ToString();
                    tableRow.Cells.Add(cell1);

                    TableCell cell2 = new TableCell();
                    cell2.Text = row["price"].ToString();
                    tableRow.Cells.Add(cell2);

                    TableCell cell3 = new TableCell();
                    cell3.Text = row["quantity"].ToString();
                    tableRow.Cells.Add(cell3);

                    TableCell cell4 = new TableCell();
                    cell4.Text = row["discription"].ToString();
                    tableRow.Cells.Add(cell4);


                    myTable.Rows.Add(tableRow);
                }
            

        }
      
        private DataTable GetDataFromDatabase()
        {
            string query = "SELECT name,price,quantity,discription FROM products";

            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand(query, connection);
                
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dt);
                    connection.Close();
                
            

            return dt;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string name = Name.Text;
            string price = TextBox1.Text;
            string quantitqnty = TextBox2.Text;
            string description = dis.Value.ToString();
            if (fileUploadImage.HasFile && price!="" && quantitqnty!="" && description !="")
            {
                 fileBytes = fileUploadImage.FileBytes;
                //string query = "insert into products (name,price,quantity,discription,image) values ('"+name+"','"+price+"','"+quantitqnty+"','"+description+"','fileBytes')";
                string query = "INSERT INTO products (name,price,quantity,discription,image) VALUES (@name, @price, @quantity, @description, @fileBytes)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@quantity", quantitqnty);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@fileBytes", fileBytes); connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Image Selected to upload');", true);
                Name.Text = "";
                TextBox1.Text = "";
                TextBox2.Text = "";
                dis.Value = "";
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select a image');", true);


            }
        }

    }
}