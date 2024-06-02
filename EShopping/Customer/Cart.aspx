<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="EShopping.Customer.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
       table {
           width: 100%;
           max-width: 800px;
           margin: 20px auto;
           border-collapse: collapse;
           background-color: #fff;
           box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
       }
       th, td {
           padding: 12px 15px;
           text-align: left;
       }
       th {
           background-color: #007bff;
           color: #fff;
           text-transform: uppercase;
       }
       tr:nth-child(even) {
           background-color: #f2f2f2;
       }
       tr:hover {
           background-color: #f1f1f1;
       }
       td {
           border-bottom: 1px solid #ddd;
       }
       tfoot td {
           font-weight: bold;
           background-color: #f1f1f1;
       }
       .container {
           text-align: center;
           margin-top: 20px;
       }
       .btn-buy {
           background-color: #007bff;
           border: none;
           color: white;
           padding: 10px 20px;
           border-radius: 5px;
           cursor: pointer;
           transition: background-color 0.3s ease;
           margin-top: 20px;
       }
       .btn-buy:hover {
           background-color: #0056b3;
       }
       .total-amount {
           font-size: 1.2rem;
           font-weight: bold;
           margin-top: 20px;
           display: block;
       }
   </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Panel ID="productContainer" runat="server"></asp:Panel>
        <asp:Button ID="btnCalculateTotal" runat="server" Text="Buy" CssClass="btn-buy" OnClick="CalculateTotal" />
        <asp:Label runat="server" ID="totalAmountLabel" CssClass="total-amount" />
    </div>
</asp:Content>
