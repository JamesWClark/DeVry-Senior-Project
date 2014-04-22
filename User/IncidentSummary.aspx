<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncidentSummary.aspx.cs" Inherits="User_IncidentSummary" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incident Summary</title>
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
            <asp:Label ID="lblErr" runat="server"></asp:Label>
            <p class="Instruction">Select an incident to view its details. Click the check box below to view a table of previously closed incidents.</p>
            <asp:CheckBox ID="cbFilter" runat="server" AutoPostBack="True" 
                Text="Include Closed" oncheckedchanged="cbFilter_CheckedChanged" />
            <br />
            <br />
            <div>
                <asp:GridView ID="gvOpenAssigned" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" CssClass="ObjectTable" DataKeyNames="IncidentId,ObjectLabel,ObjectGuid" 
                    DataSourceID="dsOpenAssigned" EnableModelValidation="True" 
                    onselectedindexchanged="gvOpenAssigned_SelectedIndexChanged" 
                    GridLines="Vertical">
                    <AlternatingRowStyle CssClass="AltRow" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="IncidentId" HeaderText="#" 
                            InsertVisible="False" ReadOnly="True" SortExpression="IncidentId" />
                        <asp:BoundField DataField="ObjectGuid" HeaderText="ObjectGuid" 
                            SortExpression="ObjectGuid" Visible="False" />
                        <asp:BoundField DataField="ObjectLabel" HeaderText="ObjectLabel" 
                            SortExpression="ObjectLabel" Visible="False" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Status" HeaderText="Status" 
                            SortExpression="Status" />
                        <asp:BoundField DataField="AssignedUserName" HeaderText="Assigned To" 
                            SortExpression="AssignedUserName" />
                    </Columns>
                </asp:GridView>
            </div>
            <p>&nbsp;</p>
            <asp:Label ID="lblClosed" runat="server" 
                    Text="Previously closed incidents are listed as follows." 
                    CssClass="Instruction" Visible="False"></asp:Label>
            <br />
            <br />
            <div>
                <asp:GridView ID="gvSummary" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" DataKeyNames="IncidentId,ObjectLabel,ObjectGuid" 
                    DataSourceID="dsClosed" EnableModelValidation="True" 
                    CssClass="ObjectTable" Width="100%" AutoGenerateSelectButton="True" 
                    onselectedindexchanged="gvSummary_SelectedIndexChanged" Visible="False" 
                    GridLines="Vertical">
                    <AlternatingRowStyle CssClass="AltRow" />
                    <Columns>
                        <asp:BoundField DataField="IncidentId" HeaderText="#" 
                            InsertVisible="False" ReadOnly="True" SortExpression="IncidentId" />
                        <asp:BoundField DataField="ObjectGuid" HeaderText="ObjectGuid" 
                            SortExpression="ObjectGuid" Visible="False" />
                        <asp:BoundField DataField="ObjectLabel" HeaderText="ObjectLabel" 
                            SortExpression="ObjectLabel" Visible="False" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Status" HeaderText="Status" 
                            SortExpression="Status" />
                        <asp:BoundField DataField="AssignedUserName" HeaderText="Assigned To" 
                            SortExpression="AssignedUserName" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:SqlDataSource ID="dsClosed" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                
                SelectCommand="SELECT [IncidentId], [ObjectGuid], [ObjectLabel], [Title], [Status], [AssignedUserName] FROM [ajjp_Incidents] WHERE ([Status] = @Status) ORDER BY [IncidentId]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Closed" Name="Status" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="dsOpenAssigned" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                SelectCommand="SELECT [IncidentId], [ObjectGuid], [ObjectLabel], [Title], [Status], [AssignedUserName] FROM [ajjp_Incidents] WHERE ([Status] &lt;&gt; @Status) ORDER BY [IncidentId]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Closed" Name="Status" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</form>
</div>
</body>
</html>
