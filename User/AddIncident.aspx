<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddIncident.aspx.cs" Inherits="jw_AddIncident" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add a New Incident</title>
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
            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
            <p class="Instruction">The object currently in view has the following attributes:</p>
            <asp:Label ID="lblObject" runat="server" CssClass="SingleObject"></asp:Label>
            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Text="Title:"></asp:Label>
            <br />
            <asp:TextBox ID="txtTitle" runat="server" Width="75%"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                ControlToValidate="txtTitle" ErrorMessage="* Enter a title."></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblDescHeader" runat="server" Font-Bold="True" 
            Text="Description:"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescription" runat="server" Height="200px" 
                TextMode="MultiLine" Width="100%"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                ControlToValidate="txtDescription" ErrorMessage="* Enter a description."></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnSubmit" runat="server" oncommand="btnSubmit_Command" 
                Text="Submit" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" 
                onclick="btnCancel_Click" />
            <br />
            <br />
            <asp:Label ID="submitError" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
</form>
</div>
</body>
</html>
