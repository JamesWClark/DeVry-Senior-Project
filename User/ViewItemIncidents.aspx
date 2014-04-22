<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewItemIncidents.aspx.cs" Inherits="jw_ViewItemIncidents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incidents Per Item</title>
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
            <hr />
            <asp:Label ID="lblPageManagement" runat="server" Text="Page Management" 
                CssClass="NavLabel"></asp:Label>
            <br />
            <asp:LinkButton ID="btnCreate" runat="server" 
                PostBackUrl="~/User/AddIncident.aspx">Add New Incident</asp:LinkButton>
        </div> 
        <div id ="topNav">   
            <ul>
                <li><a href ="AboutUs.aspx">About Us</a></li>
                <li><a href ="Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">
            <asp:Label ID="lblErr" runat="server"></asp:Label>
            <p class="Instruction">The object currently in view has the following attributes:</p>
            <asp:Label ID="lblObject" runat="server"></asp:Label>
            <br />
            <asp:PlaceHolder ID="phIncidents" runat="server"></asp:PlaceHolder>
            <br />
            <br />
            <asp:SqlDataSource ID="dsObject" runat="server"></asp:SqlDataSource>
        </div>
    </div>
</form>
</div>
</body>
</html>
