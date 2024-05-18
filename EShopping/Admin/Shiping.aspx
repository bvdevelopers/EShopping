<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Shiping.aspx.cs" Inherits="EShopping.Admin.Shiping" %>
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
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <asp:Panel ID="productContainer" runat="server" CssClass="container"></asp:Panel>

</asp:Content>
