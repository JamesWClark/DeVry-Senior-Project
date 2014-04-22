<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Whelp</title>
    <link href="main.css" rel="stylesheet" type="text/css" />
    <link rel="short cut icon" href="/favicon.ico" type="image/x-icon"/>
</head>

<body>
<div id ="container">
<form id="form1" runat="server">
   <div id="header">
    </div>
     <div id = "wrapper"><div class="Login">
        <div align="center" class="insidelogin">
                <asp:Login ID="Login1"  runat="server" DisplayRememberMe="False"></asp:Login>
        </div></div>
    </div>
    
   
</form>
</div>
</body>
</html>
