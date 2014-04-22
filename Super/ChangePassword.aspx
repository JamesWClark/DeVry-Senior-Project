<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Super_ChangePassword" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
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
            <p>Select the user whose password you would like to change:
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="UserName" DataValueField="UserId" 
                    Height="33px"
                    Width="137px">
                </asp:DropDownList>
            </p>
            <p>New Password:         
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <br />
            </p>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
            <br />
            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDB %>" 
                SelectCommand="SELECT [UserId], [UserName] FROM [vw_aspnet_Users]">
            </asp:SqlDataSource>
        </div>
    </div>
</form>
</div>
</body>
</html>
