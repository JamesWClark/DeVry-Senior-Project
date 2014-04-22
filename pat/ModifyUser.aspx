<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyUser.aspx.cs" Inherits="Super_ModifyUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify User</title>
<link href="file:///Macintosh HD/Users/aaronbaker/Desktop/main.css" rel="stylesheet" type="text/css" />
</head>
<body><div id="header"></div>
       <div id="container"><div id="sidebar1">
        <ul> 
        <h3>APPLICATION LINKS:</h3>
        <li><a href="../AddItem.aspx">Add Items</a></li>
        <li><a href="../DeleteItem.aspx">Delete Items</a></li>
        <li><a href="../AllItems.aspx">All Items</a></li>
        <li><a href="../NameCheck.aspx">Name Check</a></li>
        </ul>
		<ul>
       <h3> ACCOUNT LINKS:</h3>
        <li><a href="../CreateUser.aspx">Add New User</a></li>
        <li><a href="../DeleteItem.aspx">Remove User</a></li>
        <li><a href="../ModifyUser.aspx">Modify User</a></li>
        <li><a href="../NameCheck.aspx">Change User Password</a></li>
        </ul>
       </div>
   


    <div id="mainContent">
    <form id="form1" runat="server">
    <div>
    
        Select User:&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="SqlDataSource2" DataTextField="UserName" 
            DataValueField="UserName" Height="26px" Width="235px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button1_Click" 
            Text="Delete User" />
&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDB %>" 
            SelectCommand="SELECT [UserName] FROM [vw_aspnet_Users]">
        </asp:SqlDataSource>
&nbsp;</div>
    </form></div></div>
    </body>
</html>
