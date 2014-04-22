<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewItems.aspx.cs" Inherits="jw_ViewItems" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Inventory Items</title>
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
            <asp:Panel ID="pnlPageManagement" runat="server" Visible="False">
                <hr />
                <asp:Label ID="lblPageManagement" runat="server" 
                    Text="Page Management" CssClass="NavLabel"></asp:Label>
                 <br />
                <asp:CheckBox ID="cbDeleteMode" runat="server" 
                    AutoPostBack="True" oncheckedchanged="cbDeleteMode_CheckedChanged" 
                    Text="Delete Mode" />
            </asp:Panel>
        </div> 
        <div id ="topNav">   
            <ul>
                <li><a href ="AboutUs.aspx">About Us</a></li>
                <li><a href ="Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">
            <p class="Instruction">Select an item from the drop down menu to view the active data members for that category.</p>
            <p class="Instruction">To view details about a particular inventory item, click the "Select" link corresponding to that item.</p>   
            <asp:DropDownList ID="ddlObjects" CssClass = "mainContent" runat="server" AutoPostBack="True" 
                DataSourceID="dsObjects" DataTextField="obj_name" DataValueField="obj_id" 
                onselectedindexchanged="ddlObjects_SelectedIndexChanged" 
                onprerender="ddlObjects_PreRender">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lblErr"  runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="stub" runat="server" ForeColor="Red"></asp:Label>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                <asp:GridView ID="gvObjects" runat="server" AutoGenerateSelectButton="True" 
                    onprerender="gvObjects_PreRender" 
                    onselectedindexchanged="gvObjects_SelectedIndexChanged" 
                    CssClass="ObjectTable" CellPadding="5" 
                    onrowdeleting="gvObjects_RowDeleting" onrowcreated="gvObjects_RowCreated">
                    <AlternatingRowStyle CssClass="AltRow" />
                </asp:GridView>
            </asp:Panel>
            <asp:SqlDataSource ID="dsObjects" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDB %>" 
                SelectCommand="SELECT * FROM [ajjp_Objects] ORDER BY [obj_name]">
            </asp:SqlDataSource>
        </div>
    </div>
</form>
</div>
</body>
</html>
