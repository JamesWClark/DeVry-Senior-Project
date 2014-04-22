<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Incident.aspx.cs" Inherits="Incident" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <asp:Label ID="lblID" runat="server" Text="Enter Tag #, Press &quot;Set Tag #&quot; Button" 
            Width="250px"></asp:Label>
        <asp:TextBox ID="txtID" runat="server" Width="50px"></asp:TextBox>
    
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnItem" runat="server" BackColor="Yellow" Text="Set Tag #" />
    
        <br />
        <asp:GridView ID="gvIncident" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames="INCIDENT_NUM_PK" DataSourceID="dsHistory" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" Visible="false">
            <AlternatingRowStyle BackColor="LightBlue" />

            <emptydatarowstyle backcolor="LightBlue"  
                forecolor="Red"/>
            <EmptyDataTemplate>
            
            NO INCIDENTS FOUND FOR THIS TAG #

            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="INCIDENT_NUM_PK" HeaderText="Incident Number" 
                    InsertVisible="False" ReadOnly="True" SortExpression="INCIDENT_NUM_PK" />
                <asp:BoundField DataField="INCIDENT_ITEM_FK" HeaderText="Tag #" 
                    SortExpression="INCIDENT_ITEM_FK" />
                <asp:BoundField DataField="INCIDENT_DATE_OPEN" HeaderText="Date Entered" 
                    SortExpression="INCIDENT_DATE_OPEN" />
                <asp:BoundField DataField="INCIDENT_DESCRIPTION" HeaderText="Description" 
                    SortExpression="INCIDENT_DESCRIPTION" />
                <asp:BoundField DataField="INCIDENT_STATUS_FK" HeaderText="Incident Status" 
                    SortExpression="INCIDENT_STATUS_FK" />
                <asp:BoundField DataField="INCIDENT_ENTERED_BY" 
                    HeaderText="Incident Entered By" SortExpression="INCIDENT_ENTERED_BY" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <br />
        <asp:SqlDataSource ID="dsHistory" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            
            
            
            SelectCommand="SELECT * FROM [INCIDENT] WHERE ([INCIDENT_ITEM_FK] = @INCIDENT_ITEM_FK)">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtID" Name="INCIDENT_ITEM_FK" 
                    PropertyName="Text" Type="Decimal" />
            </SelectParameters>
        </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="dsEditStatus" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT STATUS FROM STATUS ORDER BY STATUS DESC">
        </asp:SqlDataSource>
    
    </div>

    <div align="center">
    
    
        <asp:Label ID="lblUser" runat="server" Text="Entered By" Width="90px" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddUser" runat="server" DataSourceID="dsUser" 
            DataTextField="USER_NAME" DataValueField="USER_NAME" Visible = "false">
        </asp:DropDownList>
        <asp:SqlDataSource ID="dsUser" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT DISTINCT [USER_NAME] FROM [USER]"></asp:SqlDataSource>
    
    
        <asp:Label ID="lblSolution" runat="server" Text="Description" Width="90px" Visible = "false"></asp:Label>
        <asp:TextBox ID="txtSolution" runat="server" Width="500px" TextMode="MultiLine" Visible = "false"></asp:TextBox>
    
    
    
        <asp:Label ID="lblStatus" runat="server" Text="Status" Width="60px" Visible = "false"></asp:Label>
        <asp:DropDownList ID="ddStatus" runat="server" DataSourceID="dsStatus" Visible = "false"
            DataTextField="STATUS" DataValueField="STATUS">
        </asp:DropDownList>
  
        <asp:Button ID="btnSubmit" runat="server" BackColor="#009933" ForeColor="White" 
            Text="Submit" Visible = "false" />
   
        <asp:Label ID="lblMessage" runat="server" ForeColor = "Red"></asp:Label>
   
        <asp:SqlDataSource ID="dsStatus" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT DISTINCT STATUS FROM STATUS ORDER BY STATUS DESC">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
