<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EShopping.Admin.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <hr />
    <div class="table">
        <h1>Products</h1>
<asp:Table ID="myTable" runat="server">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell>Name</asp:TableHeaderCell>
        <asp:TableHeaderCell>Price</asp:TableHeaderCell>
                <asp:TableHeaderCell>Quantity</asp:TableHeaderCell>
        <asp:TableHeaderCell>Discription</asp:TableHeaderCell>

    </asp:TableHeaderRow>
</asp:Table>


    </div>
    <hr />

    <div class="add_product">
        <h1>
            Add Products
        </h1>
       
            <asp:TextBox ID="Name" placeholder ="Product name" runat="server"> </asp:TextBox>
            <asp:TextBox ID="TextBox1" TextMode="Number" placeholder ="Price" runat="server"> </asp:TextBox>
            <asp:TextBox ID="TextBox2" TextMode="Number" placeholder ="quantity" runat="server"> </asp:TextBox>
        <br />
        <br />
        <textarea Id="dis" placeholder="discription" runat="server"></textarea>
       
        <asp:FileUpload ID="fileUploadImage" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload Image" OnClick="btnUpload_Click" />

    </div>
      <script type="text/javascript">
function showAlert() {
    alert('Please fill in all the fields.');
    return false; // Prevent postback if needed
}
      </script>
</asp:Content>
