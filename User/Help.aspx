<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="User_Help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Help</title>
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

            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="../User/WhelpManual.pdf" Target="_blank">Click Here</asp:HyperLink>
&nbsp;- Download the User Manual<br />
        </div>
    </div>
</form>
</div>
</body>
</html>
