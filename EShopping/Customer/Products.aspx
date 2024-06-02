<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EShopping.Customer.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* CSS for the product page */
        .maincard {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 20px;
            padding: 20px;
            justify-items: center;
        }

        .card {
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 15px;
            margin: 10px;
            width: 250px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.2s;
            background-color: #fff;
        }

        .card img {
            max-width: 100%;
            height: auto;
            border-radius: 5px;
            margin-bottom: 15px;
        }

        .card h5 {
            margin: 10px 0;
            font-size: 1.2rem;
            color: #333;
        }

        .card p {
            margin: 5px 0;
            font-size: 1rem;
            color: #555;
        }

        .card button {
            background-color: #007bff;
            border: none;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .card button:hover {
            background-color: #0056b3;
        }

        .card:hover {
            transform: translateY(-10px);
        }

        .container {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Products</h1>
    <hr />
    <asp:Label runat="server" ID="lbl" />
    <div class="maincard" runat="server" id="productContainer">
        <!-- Product cards will be dynamically added here -->
    </div>
</asp:Content>
