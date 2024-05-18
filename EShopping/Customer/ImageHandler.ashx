<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;

public class ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
string connectionString = "server=localhost;user=root;password=root;database=eshopping;";
MySqlConnection connection = new MySqlConnection(connectionString);
string query = "SELECT image FROM products WHERE id=3";

MySqlCommand command = new MySqlCommand(query, connection);

try
{
    connection.Open();
    // Execute the query to fetch the image data
    object imageData = command.ExecuteScalar();

 
}
catch (Exception ex)
{
    // Handle any exceptions
    // For example, you can log the error or display a message to the user
    System.Console.WriteLine(ex.ToString());
}
finally
{
    // Close the database connection
    connection.Close();
}
        // Get the image data from the query string or wherever it's stored
        byte[] imageData = ...; // Retrieve the image data from your database or wherever it's stored

        // Set the response content type
        context.Response.ContentType = "image/jpeg";

        // Write the image data to the response stream
        context.Response.BinaryWrite(imageData);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
