<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectUser.aspx.cs" Inherits="Super_SelectUser" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select User</title>
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
                <li><a href ="../User/AboutUs.aspx">About Us</a></li>
                <li><a href ="../User/Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">
            Select the type of user you would like to create:<br />
            <asp:RadioButtonList ID="rblRoles" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="RoleName" DataValueField="RoleName">
            </asp:RadioButtonList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDB %>" 
                SelectCommand="SELECT [RoleName] FROM [vw_aspnet_Roles]">
            </asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                Text="Continue" />
        </div>
    </div>
</form>
</div>
</body>
</html>
