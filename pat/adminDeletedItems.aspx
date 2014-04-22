<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminDeletedItems.aspx.cs" Inherits="adminDeletedItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="gvAdminDelete" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames="ITEM_ID_PK" DataSourceID="dsAdminDelete" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" 
            AllowPaging="True" AllowSorting="True">

            <emptydatarowstyle backcolor="LightBlue"  
                forecolor="Red"/>

            <EmptyDataTemplate>
            
            NO DELETED ITEMS FOUND

            </EmptyDataTemplate>

            <AlternatingRowStyle BackColor="LightBlue" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ITEM_ID_PK" HeaderText="Tag #" ReadOnly="True" 
                    SortExpression="ITEM_ID_PK" />
                <asp:TemplateField HeaderText="Item Type" SortExpression="ITEM_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ITEM_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Status" SortExpression="ITEM_STATUS_FK">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="dsStatusEdit" 
                            DataTextField="ITEM_STATUS" DataValueField="ITEM_STATUS" 
                            SelectedValue='<%# Bind("ITEM_STATUS_FK") %>'>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="dsStatusEdit" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
                            SelectCommand="SELECT * FROM [ITEM_STATUS] ORDER BY [ITEM_STATUS] DESC">
                        </asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ITEM_STATUS_FK") %>'></asp:Label>
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
        <asp:SqlDataSource ID="dsAdminDelete" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            
            SelectCommand="SELECT [ITEM_ID_PK], [ITEM_TYPE], [ITEM_STATUS_FK] FROM [ITEM] WHERE ([ITEM_STATUS_FK] = @ITEM_STATUS_FK)" 
            DeleteCommand="DELETE FROM [ITEM] WHERE [ITEM_ID_PK] = @ITEM_ID_PK" 
            InsertCommand="INSERT INTO [ITEM] ([ITEM_ID_PK], [ITEM_TYPE], [ITEM_STATUS_FK]) VALUES (@ITEM_ID_PK, @ITEM_TYPE, @ITEM_STATUS_FK)" 
            UpdateCommand="UPDATE [ITEM] SET [ITEM_TYPE] = @ITEM_TYPE, [ITEM_STATUS_FK] = @ITEM_STATUS_FK WHERE [ITEM_ID_PK] = @ITEM_ID_PK">
            <DeleteParameters>
                <asp:Parameter Name="ITEM_ID_PK" Type="Decimal" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ITEM_ID_PK" Type="Decimal" />
                <asp:Parameter Name="ITEM_TYPE" Type="String" />
                <asp:Parameter Name="ITEM_STATUS_FK" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="DELETED" Name="ITEM_STATUS_FK" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="ITEM_TYPE" Type="String" />
                <asp:Parameter Name="ITEM_STATUS_FK" Type="String" />
                <asp:Parameter Name="ITEM_ID_PK" Type="Decimal" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
