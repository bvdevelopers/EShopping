<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EShopping.Admin.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General page styling */
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }
        h1 {
            font-size: 2rem;
            color: #333;
            margin-bottom: 20px;
            text-align: center;
        }
        hr {
            border: none;
            height: 1px;
            background-color: #ddd;
            margin: 20px 0;
        }

        /* Table styling */
        .table {
            width: 100%;
            margin-bottom: 40px;
            overflow-x: auto;
        }
        .table h1 {
            text-align: left;
        }
        .table table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .table th, .table td {
            padding: 12px 15px;
            border: 1px solid #ddd;
        }
        .table th {
            background-color: #f4f4f4;
            text-align: left;
        }
        .table tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        /* Form styling */
        .add_product {
            background-color: #f4f4f4;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .add_product h1 {
            text-align: center;
        }
        .add_product input[type="text"], 
        .add_product input[type="number"], 
        .add_product textarea {
            width: calc(100% - 22px);
            padding: 10px;
            margin-bottom: 20px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 1rem;
        }
        .add_product textarea {
            height: 100px;
        }
        .add_product input[type="file"] {
            margin-bottom: 20px;
        }
        .add_product button {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 1rem;
        }
        .add_product button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="table">
            <h1>Products</h1>
            <asp:Table ID="myTable" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Price</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Quantity</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>

        <hr />

        <div class="add_product">
            <h1>Add Products</h1>
            <asp:TextBox ID="Name" placeholder="Product name" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox1" TextMode="Number" placeholder="Price" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox2" TextMode="Number" placeholder="Quantity" runat="server"></asp:TextBox>
            <textarea id="dis" placeholder="Description" runat="server"></textarea>
            <asp:FileUpload ID="fileUploadImage" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload Image" OnClick="btnUpload_Click" />
        </div>
    </div>

    <script type="text/javascript">
        function showAlert() {
            alert('Please fill in all the fields.');
            return false; // Prevent postback if needed
        }
    </script>
</asp:Content>
