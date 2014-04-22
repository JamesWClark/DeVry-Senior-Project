<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="AddItem.aspx.cs" Inherits="_AddItem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add to Inventory</title>
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
            <p class="Instruction">Select an item from the drop down menu. Enter the required information and click "Submit."</p>
            <p class="Instruction">Fields marked with an asterisk are required.</p>
            <asp:DropDownList ID="ddlObjects" runat="server" AutoPostBack="True" 
                DataSourceID="dsObjects" DataTextField="obj_name" DataValueField="obj_id" 
                onprerender="ddlObjects_PreRender" 
                onselectedindexchanged="ddlObjects_SelectedIndexChanged">
                <asp:ListItem>--</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Table ID="v1table" runat="server" onprerender="v1table_PreRender">
            </asp:Table>
    
            <br />
            <asp:Button ID="bSubmit" runat="server" onclick="bSubmit_Click" Text="Submit" 
                Visible="False" />
    
            <br />
            <br />
            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
    
            <asp:SqlDataSource ID="dsObjects" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                SelectCommand="SELECT * FROM [ajjp_Objects] ORDER BY [obj_name]"></asp:SqlDataSource>
        </div>
    </div>
</form>
</div>
</body>
</html>
