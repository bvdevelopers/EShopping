using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EShopping.Admin
{
    public partial class Shiping : System.Web.UI.Page
    {
        int cid = 2;
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
                string query = "SELECT * FROM purchase ORDER BY prgrpid;";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    string currentPrgrpid = null;
                    Table table = new Table();
                    TableRow headerRow = new TableRow();
                    TableCell prgrpidHeaderCell = new TableCell();
                    prgrpidHeaderCell.Text = "prgrpid";
                    headerRow.Cells.Add(prgrpidHeaderCell);
                    TableCell pidHeaderCell = new TableCell();
                    pidHeaderCell.Text = "pid";
                    headerRow.Cells.Add(pidHeaderCell);
                    TableCell cidHeaderCell = new TableCell();
                    cidHeaderCell.Text = "cid";
                    headerRow.Cells.Add(cidHeaderCell);
                    table.Rows.Add(headerRow);

                    while (reader.Read())
                    {
                        string prgrpid = reader["prgrpid"].ToString();
                        string pid = reader["pid"].ToString();
                        string cid = reader["cid"].ToString();

                        if (prgrpid != currentPrgrpid)
                        {
                            if (currentPrgrpid != null)
                            {
                                // Add the previous table to the container
                                productContainer.Controls.Add(table);
                            }

                            // Create a new table for the new prgrpid
                            table = new Table();
                            TableRow prgrpidRow = new TableRow();
                            TableCell prgrpidCell = new TableCell();
                            prgrpidCell.Text = $"prgrpid - {prgrpid}";
                            prgrpidCell.ColumnSpan = 3; // Span all columns
                            prgrpidRow.Cells.Add(prgrpidCell);
                            table.Rows.Add(prgrpidRow);

                            // Add headers to the new table
                            table.Rows.Add(headerRow);

                            currentPrgrpid = prgrpid;
                        }

                        // Add data row to the current table
                        TableRow dataRow = new TableRow();
                        TableCell pidCell = new TableCell();
                        pidCell.Text = pid;
                        dataRow.Cells.Add(pidCell);
                        TableCell cidCell = new TableCell();
                        cidCell.Text = cid;
                        dataRow.Cells.Add(cidCell);
                        table.Rows.Add(dataRow);
                    }

                    // Add the last table to the container
                    productContainer.Controls.Add(table);
                }
            }
        }






    }
}