<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyUser.aspx.cs" Inherits="Super_ModifyUser" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modify User</title>
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
            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                <asp:View ID="v1" runat="server">
                    Select User:&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="UserName" 
                        DataValueField="UserId" Height="26px" Width="235px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="Button3" runat="server" onclick="Button1_Click" 
                        Text="Delete User" />
                    <br />
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                        SelectCommand="SELECT [UserName], [UserId] FROM [vw_aspnet_Users]">
                    </asp:SqlDataSource>
                </asp:View>
                <asp:View ID="v2" runat="server">
                    <asp:Label ID="v2lbl" runat="server" CssClass="Instruction"></asp:Label>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</form>
</div>
</body>
</html>
