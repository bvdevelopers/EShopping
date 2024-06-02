<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="EShopping.Auth.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
        }
        .signup-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            background-color: #f9f9f9;
        }
        .signup-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .signup-container div {
            margin-bottom: 15px;
        }
        .signup-container label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .signup-container input[type="text"],
        .signup-container input[type="email"],
        .signup-container input[type="password"] {
            width: calc(100% - 20px);
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .signup-container input[type="text"]:focus,
        .signup-container input[type="email"]:focus,
        .signup-container input[type="password"]:focus {
            border-color: #007bff;
            outline: none;
        }
        .signup-container button {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            border: none;
            border-radius: 4px;
            color: white;
            font-size: 16px;
            cursor: pointer;
        }
        .signup-container button:hover {
            background-color: #0056b3;
        }
    </style>
    <div class="signup-container">
        <h2>Sign Up</h2>
        <div>
            <label>Username:</label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" type="email"></asp:TextBox>
        </div>
        <div>
            <label>Phone Number:</label>
            <asp:TextBox ID="txtphno" runat="server" type="text"></asp:TextBox>
        </div>
        <div>
            <label>Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
        </div>
        <script type="text/javascript">
            function showAlert() {
                alert('Please fill in all the fields.');
                return false; // Prevent postback if needed
            }
        </script>
    </div>
</asp:Content>