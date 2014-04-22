<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditInventoryItem.aspx.cs" Inherits="User_EditInventoryItem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Inventory Item</title>
    <link href="../main.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="container">
<form id="form1" runat="server">
    <div id="header">
        <div id="logout">
            <asp:LoginStatus ID="LoginStatus1" runat="server" 
                LogoutAction="RedirectToLoginPage" />
        </div>
    </div>
    <div id = "wrapper">
        <div id="sidebar1">
            <asp:PlaceHolder ID="phNav" runat="server"></asp:PlaceHolder>
        </div> 
        <div id ="topNav">   
            <ul>
                <li><a href ="AboutUs.aspx">About Us</a></li>
                <li><a href ="Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">

            <asp:Label ID="lblErr" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblObject" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Table ID="v1table" runat="server">
            </asp:Table>
            <br />
            <br />
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" 
                Width="75px" />
&nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="Cancel" 
                Width="75px" CausesValidation="False" />

        </div>
    </div>
</form>
</div>
</body>
</html>
