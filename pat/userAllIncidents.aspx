<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userAllIncidents.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="gvAllIncidents" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataKeyNames="INCIDENT_NUM_PK" 
            DataSourceID="dsAllIncidents" EnableModelValidation="True" CellPadding="4" 
            ForeColor="#333333" GridLines="None" AllowSorting="True">
            <AlternatingRowStyle BackColor="LightBlue" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Edit" Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                            CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="INCIDENT_NUM_PK" HeaderText="Incident Number" 
                    InsertVisible="False" ReadOnly="True" SortExpression="INCIDENT_NUM_PK" />
                <asp:TemplateField HeaderText="Item ID" SortExpression="INCIDENT_ITEM_FK">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("INCIDENT_ITEM_FK") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Opened" SortExpression="INCIDENT_DATE_OPEN">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("INCIDENT_DATE_OPEN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" 
                    SortExpression="INCIDENT_DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" 
                            Text='<%# Bind("INCIDENT_DESCRIPTION") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                            Text='<%# Bind("INCIDENT_DESCRIPTION") %>' TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle Width="400px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Incident Status" 
                    SortExpression="INCIDENT_STATUS_FK">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("INCIDENT_STATUS_FK") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="dsDropDown" 
                            DataTextField="STATUS" DataValueField="STATUS" 
                            SelectedValue='<%# Bind("INCIDENT_STATUS_FK") %>'>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Incident Entered By" 
                    SortExpression="INCIDENT_ENTERED_BY">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("INCIDENT_ENTERED_BY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:SqlDataSource ID="dsAllIncidents" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT * FROM [INCIDENT]" 
            DeleteCommand="DELETE FROM [INCIDENT] WHERE [INCIDENT_NUM_PK] = @INCIDENT_NUM_PK" 
            InsertCommand="INSERT INTO [INCIDENT] ([INCIDENT_ITEM_FK], [INCIDENT_DATE_OPEN], [INCIDENT_DESCRIPTION], [INCIDENT_STATUS_FK], [INCIDENT_ENTERED_BY]) VALUES (@INCIDENT_ITEM_FK, @INCIDENT_DATE_OPEN, @INCIDENT_DESCRIPTION, @INCIDENT_STATUS_FK, @INCIDENT_ENTERED_BY)" 
            UpdateCommand="UPDATE [INCIDENT] SET [INCIDENT_ITEM_FK] = @INCIDENT_ITEM_FK, [INCIDENT_DATE_OPEN] = @INCIDENT_DATE_OPEN, [INCIDENT_DESCRIPTION] = @INCIDENT_DESCRIPTION, [INCIDENT_STATUS_FK] = @INCIDENT_STATUS_FK, [INCIDENT_ENTERED_BY] = @INCIDENT_ENTERED_BY WHERE [INCIDENT_NUM_PK] = @INCIDENT_NUM_PK">
            <DeleteParameters>
                <asp:Parameter Name="INCIDENT_NUM_PK" Type="Decimal" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="INCIDENT_ITEM_FK" Type="Decimal" />
                <asp:Parameter Name="INCIDENT_DATE_OPEN" Type="String" />
                <asp:Parameter Name="INCIDENT_DESCRIPTION" Type="String" />
                <asp:Parameter Name="INCIDENT_STATUS_FK" Type="String" />
                <asp:Parameter Name="INCIDENT_ENTERED_BY" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="INCIDENT_ITEM_FK" Type="Decimal" />
                <asp:Parameter Name="INCIDENT_DATE_OPEN" Type="String" />
                <asp:Parameter Name="INCIDENT_DESCRIPTION" Type="String" />
                <asp:Parameter Name="INCIDENT_STATUS_FK" Type="String" />
                <asp:Parameter Name="INCIDENT_ENTERED_BY" Type="String" />
                <asp:Parameter Name="INCIDENT_NUM_PK" Type="Decimal" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="dsDropDown" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT * FROM [STATUS] ORDER BY [STATUS] DESC">
        </asp:SqlDataSource>
    
    </div>

   
    </form>
</body>
</html>
