<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteItem.aspx.cs" Inherits="DeleteItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 130px" align="center">
    
        <asp:Label ID="lblDelete" runat="server" 
            Text="Enter Tag # of Item to be Deleted and Click Submit" Width="220px"></asp:Label>
        <asp:TextBox ID="txtDelete" runat="server" Width="60px"></asp:TextBox>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" BackColor="Green" 
            ForeColor="White" />
        <br />
        <br />
        <asp:GridView ID="gvDelete" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ITEM_ID_PK" DataSourceID="dsDeleteItem" 
            EnableModelValidation="True" BackColor="black" ForeColor="White">
        <emptydatarowstyle backcolor="LightBlue"  
                forecolor="Red"/>

            <EmptyDataTemplate>
            
            NO ACTIVE ITEM FOUND, PLEASE VERIFY TAG #

            </EmptyDataTemplate>

            <Columns>
                <asp:BoundField DataField="ITEM_ID_PK" HeaderText="Tag #" ReadOnly="True" 
                    SortExpression="ITEM_ID_PK" />
                <asp:BoundField DataField="ITEM_TYPE" HeaderText="Item Type" 
                    SortExpression="ITEM_TYPE" />
                <asp:BoundField DataField="ITEM_DESCRIPTION" HeaderText="Description" 
                    SortExpression="ITEM_DESCRIPTION" />
                <asp:BoundField DataField="ITEM_SERIAL_NUM" HeaderText="Serial #" 
                    SortExpression="ITEM_SERIAL_NUM" />
                <asp:BoundField DataField="ITEM_MODEL_NUM" HeaderText="Model #" 
                    SortExpression="ITEM_MODEL_NUM" />
                <asp:BoundField DataField="ITEM_LOCATION_ID_FK" HeaderText="Location" 
                    SortExpression="ITEM_LOCATION_ID_FK" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsDeleteItem" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            
            SelectCommand="SELECT [ITEM_ID_PK], [ITEM_TYPE], [ITEM_DESCRIPTION], [ITEM_SERIAL_NUM], [ITEM_MODEL_NUM], [ITEM_LOCATION_ID_FK] FROM [ITEM] WHERE (([ITEM_ID_PK] = @ITEM_ID_PK) AND ([ITEM_STATUS_FK] = @ITEM_STATUS_FK))">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtDelete" Name="ITEM_ID_PK" 
                    PropertyName="Text" Type="Decimal" />
                <asp:Parameter DefaultValue="ACTIVE" Name="ITEM_STATUS_FK" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>

    <div align="center">
    
    
    
        <asp:Button ID="btnDelete" runat="server" BackColor="Red" ForeColor="White" 
            Text="Delete This Item" Visible="true" />
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    
    
    
    </div>


    </form>
</body>
</html>
