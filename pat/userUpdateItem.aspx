<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userUpdateItem.aspx.cs" Inherits="frmGrid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <div align="center">
        <asp:Label ID="lblID" runat="server" Text="Enter Item ID, Press Enter" 
            Width="180px"></asp:Label>
    
    <asp:TextBox ID="txtID" runat="server" Width="50px"></asp:TextBox>
    
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
        EnableModelValidation="True" AutoGenerateColumns="False" BackColor="White" 
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
            CellSpacing="1" GridLines="None" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ITEM_ID_PK" HeaderText="Tag #" ReadOnly="True" 
                SortExpression="ITEM_ID_PK" />
            <asp:BoundField DataField="ITEM_TYPE" HeaderText="Item Type" 
                SortExpression="ITEM_TYPE" />
            <asp:TemplateField HeaderText="Description" SortExpression="ITEM_DESCRIPTION">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"
                        Text='<%# Bind("ITEM_DESCRIPTION") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ITEM_DESCRIPTION") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" Wrap="True" />
            </asp:TemplateField>
            <asp:BoundField DataField="ITEM_SERIAL_NUM" HeaderText="Serial #" 
                SortExpression="ITEM_SERIAL_NUM" />
            <asp:BoundField DataField="ITEM_MODEL_NUM" HeaderText="Model #" 
                SortExpression="ITEM_MODEL_NUM" />
            <asp:TemplateField HeaderText="Location" SortExpression="ITEM_LOCATION_ID_FK">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("ITEM_LOCATION_ID_FK") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ITEM_LOCATION_ID_FK") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
        
            SelectCommand="SELECT [ITEM_ID_PK], [ITEM_TYPE], [ITEM_DESCRIPTION], [ITEM_SERIAL_NUM], [ITEM_MODEL_NUM], [ITEM_LOCATION_ID_FK] FROM [ITEM] WHERE ([ITEM_ID_PK] = @ITEM_ID_PK)" 
            ConflictDetection="CompareAllValues" 
            DeleteCommand="DELETE FROM [ITEM] WHERE [ITEM_ID_PK] = @original_ITEM_ID_PK AND [ITEM_TYPE] = @original_ITEM_TYPE AND (([ITEM_DESCRIPTION] = @original_ITEM_DESCRIPTION) OR ([ITEM_DESCRIPTION] IS NULL AND @original_ITEM_DESCRIPTION IS NULL)) AND (([ITEM_SERIAL_NUM] = @original_ITEM_SERIAL_NUM) OR ([ITEM_SERIAL_NUM] IS NULL AND @original_ITEM_SERIAL_NUM IS NULL)) AND (([ITEM_MODEL_NUM] = @original_ITEM_MODEL_NUM) OR ([ITEM_MODEL_NUM] IS NULL AND @original_ITEM_MODEL_NUM IS NULL)) AND (([ITEM_LOCATION_ID_FK] = @original_ITEM_LOCATION_ID_FK) OR ([ITEM_LOCATION_ID_FK] IS NULL AND @original_ITEM_LOCATION_ID_FK IS NULL))" 
            InsertCommand="INSERT INTO [ITEM] ([ITEM_ID_PK], [ITEM_TYPE], [ITEM_DESCRIPTION], [ITEM_SERIAL_NUM], [ITEM_MODEL_NUM], [ITEM_LOCATION_ID_FK]) VALUES (@ITEM_ID_PK, @ITEM_TYPE, @ITEM_DESCRIPTION, @ITEM_SERIAL_NUM, @ITEM_MODEL_NUM, @ITEM_LOCATION_ID_FK)" 
            OldValuesParameterFormatString="original_{0}" 
            UpdateCommand="UPDATE [ITEM] SET [ITEM_TYPE] = @ITEM_TYPE, [ITEM_DESCRIPTION] = @ITEM_DESCRIPTION, [ITEM_SERIAL_NUM] = @ITEM_SERIAL_NUM, [ITEM_MODEL_NUM] = @ITEM_MODEL_NUM, [ITEM_LOCATION_ID_FK] = @ITEM_LOCATION_ID_FK WHERE [ITEM_ID_PK] = @original_ITEM_ID_PK AND [ITEM_TYPE] = @original_ITEM_TYPE AND (([ITEM_DESCRIPTION] = @original_ITEM_DESCRIPTION) OR ([ITEM_DESCRIPTION] IS NULL AND @original_ITEM_DESCRIPTION IS NULL)) AND (([ITEM_SERIAL_NUM] = @original_ITEM_SERIAL_NUM) OR ([ITEM_SERIAL_NUM] IS NULL AND @original_ITEM_SERIAL_NUM IS NULL)) AND (([ITEM_MODEL_NUM] = @original_ITEM_MODEL_NUM) OR ([ITEM_MODEL_NUM] IS NULL AND @original_ITEM_MODEL_NUM IS NULL)) AND (([ITEM_LOCATION_ID_FK] = @original_ITEM_LOCATION_ID_FK) OR ([ITEM_LOCATION_ID_FK] IS NULL AND @original_ITEM_LOCATION_ID_FK IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_ITEM_ID_PK" Type="Decimal" />
            <asp:Parameter Name="original_ITEM_TYPE" Type="String" />
            <asp:Parameter Name="original_ITEM_DESCRIPTION" Type="String" />
            <asp:Parameter Name="original_ITEM_SERIAL_NUM" Type="String" />
            <asp:Parameter Name="original_ITEM_MODEL_NUM" Type="String" />
            <asp:Parameter Name="original_ITEM_LOCATION_ID_FK" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ITEM_ID_PK" Type="Decimal" />
            <asp:Parameter Name="ITEM_TYPE" Type="String" />
            <asp:Parameter Name="ITEM_DESCRIPTION" Type="String" />
            <asp:Parameter Name="ITEM_SERIAL_NUM" Type="String" />
            <asp:Parameter Name="ITEM_MODEL_NUM" Type="String" />
            <asp:Parameter Name="ITEM_LOCATION_ID_FK" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="txtID" Name="ITEM_ID_PK" PropertyName="Text" 
                Type="Decimal" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ITEM_TYPE" Type="String" />
            <asp:Parameter Name="ITEM_DESCRIPTION" Type="String" />
            <asp:Parameter Name="ITEM_SERIAL_NUM" Type="String" />
            <asp:Parameter Name="ITEM_MODEL_NUM" Type="String" />
            <asp:Parameter Name="ITEM_LOCATION_ID_FK" Type="String" />
            <asp:Parameter Name="original_ITEM_ID_PK" Type="Decimal" />
            <asp:Parameter Name="original_ITEM_TYPE" Type="String" />
            <asp:Parameter Name="original_ITEM_DESCRIPTION" Type="String" />
            <asp:Parameter Name="original_ITEM_SERIAL_NUM" Type="String" />
            <asp:Parameter Name="original_ITEM_MODEL_NUM" Type="String" />
            <asp:Parameter Name="original_ITEM_LOCATION_ID_FK" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </div>

    <div style="height: 60px">
    
    
    </div>
    </form>
</body>
</html>
