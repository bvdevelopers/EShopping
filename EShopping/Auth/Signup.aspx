<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="EShopping.Auth.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div>

         <center>
            <h2>Sign Up</h2>
            <div>
                <label>Username:</label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            </div>
             <br />
            <div>
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" type="email"></asp:TextBox>
            </div>
                          <br />

         <div>
    <label>phone Number:</label>
    <asp:TextBox ID="txtphno" runat="server" type="text"></asp:TextBox>
</div>
                          <br />

            <div>
                <label>Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
                          <br />

            <div>
                <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
            </div>
             </center>
          <script type="text/javascript">
        function showAlert() {
            alert('Please fill in all the fields.');
            return false; // Prevent postback if needed
        }
          </script>
        </div>
</asp:Content>
