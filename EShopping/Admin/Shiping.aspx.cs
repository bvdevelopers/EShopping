using MySql.Data.MySqlClient;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EShopping.Admin
{
    public partial class Shiping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayPurchaseDataGroupedByPrgrpid();
            }
        }

        protected void DisplayPurchaseDataGroupedByPrgrpid()
        {
            string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT 
                        p.prgrpid, p.pid, p.cid, 
                        pr.name AS productname, 
                        c.cname, c.phno
                    FROM 
                        purchase p
                    JOIN 
                        products pr ON p.pid = pr.pid
                    JOIN 
                        customer_data c ON p.cid = c.id
                    ORDER BY 
                        p.prgrpid;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    string currentPrgrpid = null;
                    Table table = null;

                    while (reader.Read())
                    {
                        string prgrpid = reader["prgrpid"].ToString();
                        string pid = reader["pid"].ToString();
                        string productName = reader["productname"].ToString();
                        string cid = reader["cid"].ToString();
                        string customerName = reader["cname"].ToString();
                        string phno = reader["phno"].ToString();

                        if (prgrpid != currentPrgrpid)
                        {
                            if (table != null)
                            {
                                // Add the previous table to the container
                                productContainer.Controls.Add(table);
                            }

                            // Create a new table for the new prgrpid
                            table = new Table();
                            TableRow prgrpidRow = new TableRow();
                            TableCell prgrpidCell = new TableCell();
                            prgrpidCell.Text = $"prgrpid - {prgrpid}";
                            prgrpidCell.ColumnSpan = 6; // Span all columns
                            prgrpidRow.Cells.Add(prgrpidCell);
                            table.Rows.Add(prgrpidRow);

                            // Add headers to the new table
                            TableRow headerRow = new TableRow();
                            headerRow.Cells.Add(new TableCell { Text = "prgrpid" });
                            headerRow.Cells.Add(new TableCell { Text = "pid" });
                            headerRow.Cells.Add(new TableCell { Text = "Product Name" });
                            headerRow.Cells.Add(new TableCell { Text = "cid" });
                            headerRow.Cells.Add(new TableCell { Text = "Customer Name" });
                            headerRow.Cells.Add(new TableCell { Text = "Phone Number" });
                            table.Rows.Add(headerRow);

                            currentPrgrpid = prgrpid;
                        }

                        // Add data row to the current table
                        TableRow dataRow = new TableRow();
                        dataRow.Cells.Add(new TableCell { Text = prgrpid });
                        dataRow.Cells.Add(new TableCell { Text = pid });
                        dataRow.Cells.Add(new TableCell { Text = productName });
                        dataRow.Cells.Add(new TableCell { Text = cid });
                        dataRow.Cells.Add(new TableCell { Text = customerName });
                        dataRow.Cells.Add(new TableCell { Text = phno });
                        table.Rows.Add(dataRow);
                    }

                    // Add the last table to the container
                    if (table != null)
                    {
                        productContainer.Controls.Add(table);
                    }
                }
            }
        }
    }
}