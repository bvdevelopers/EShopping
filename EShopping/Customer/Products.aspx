<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EShopping.Customer.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style>
        /* CSS for card layout */
        .card {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            margin: 10px;
            width: 200px;
            
            
        }
        .maincard
        {
            display:flex;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h1>Products</h1>
    <hr />
    <asp:Label runat="server" ID="lbl"  />
          <asp:Panel ID="productContainer" runat="server" CssClass="container">
    <!-- Product cards will be dynamically added here -->
</asp:Panel>

</asp:Content>

