<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="User_Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main</title>
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
            <asp:Label ID="lblWelcome" runat="server" Text="Welcome, "></asp:Label>
            <asp:LoginName ID="LoginName1" runat="server" />
            <asp:Label ID="lblErr" runat="server" 
                Text="&lt;p&gt;Your assigned and currently open incidents:&lt;/p&gt;" 
                CssClass="Instruction"></asp:Label>
            <asp:GridView ID="gvUserIncidents" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" DataKeyNames="IncidentId,ObjectGuid,ObjectLabel" 
                DataSourceID="dsUserIncidents" EnableModelValidation="True" 
                CssClass="ObjectTable" GridLines="Vertical" Width="100%" 
                onselectedindexchanged="gvUserIncidents_SelectedIndexChanged">
                <AlternatingRowStyle CssClass="AltRow" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="IncidentId" HeaderText="#" 
                        InsertVisible="False" ReadOnly="True" SortExpression="IncidentId" />
                    <asp:BoundField DataField="ObjectGuid" HeaderText="ObjectGuid" 
                        SortExpression="ObjectGuid" Visible="False" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        SortExpression="Description" />
                    <asp:BoundField DataField="ObjectLabel" HeaderText="ObjectLabel" 
                        SortExpression="ObjectLabel" Visible="False" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:SqlDataSource ID="dsUserIncidents" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                
                
                SelectCommand="SELECT [IncidentId], [ObjectGuid], [Title], [Description], [ObjectLabel] FROM [ajjp_Incidents] WHERE (([AssignedUserName] = @AssignedUserName) AND ([Status] = @Status)) ORDER BY [IncidentId]">
                <SelectParameters>
                    <asp:SessionParameter Name="AssignedUserName" SessionField="UserName" 
                        Type="String" />
                    <asp:Parameter DefaultValue="Assigned" Name="Status" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
       
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <asp:SqlDataSource ID="dsDeletedItems" runat="server"></asp:SqlDataSource>
       
        </div>
    </div>
</form>
</div>
</body>
</html>
