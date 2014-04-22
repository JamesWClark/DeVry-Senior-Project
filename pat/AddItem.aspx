<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="AddItem.aspx.cs" Inherits="_AddItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Item</title>
<link href="../main.css" rel="stylesheet" type="text/css" />
</head>
<body><div id="header"></div>    <div id="container"><div id="sidebar1">
        <ul> 
        <h3>APPLICATION LINKS:</h3>
        <li><a href="AddItem.aspx">Add Items</a></li>
        <li><a href="DeleteItem.aspx">Delete Items</a></li>
        <li><a href="AllItems.aspx">All Items</a></li>
        <li><a href="NameCheck.aspx">Name Check</a></li>
        </ul>
		<ul>
       <h3> ACCOUNT LINKS:</h3>
        <li><a href="CreateUser.aspx">Add New User</a></li>
        <li><a href="DeleteItem.aspx">Remove User</a></li>
        <li><a href="ModifyUser.aspx">Modify User</a></li>
        <li><a href="NameCheck.aspx">Change User Password</a></li>
        </ul>
     </div><div id="mainContent"><form id="form1" runat="server">

       
    
        <asp:Label ID="lblId" runat="server" Text="*Tag #" Width="60px" BackColor="Yellow"></asp:Label>
        <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
    
        <asp:Label ID="lblItemType" runat="server" Text="*Item Type" Width="120px" 
                BackColor="Yellow"></asp:Label>
        <asp:TextBox ID="txtItemType" runat="server"></asp:TextBox>
        <asp:Label ID="lblDescription" runat="server" Text="Description" Width="80px"></asp:Label>
        <asp:TextBox ID="txtItemDescription" runat="server" Width="363px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblSerialNum" runat="server" Text="Serial Number" Width="120px" 
                ></asp:Label>
        <asp:TextBox ID="txtSerialNum" runat="server"></asp:TextBox>
        <asp:Label ID="lblModelNum" runat="server" Text="   Model Number"></asp:Label>
        <asp:TextBox ID="txtModelNum" runat="server"></asp:TextBox>
        <asp:Label ID="lblLocation" runat="server" Text="*Location" Width="100px" 
                BackColor="Yellow"></asp:Label>
            <asp:TextBox ID="txtLocation" runat="server" Width="65px"></asp:TextBox>
        <br />
    
        <br />
            <asp:Label ID="lblRequired" runat="server" BackColor="Yellow" ForeColor="Black" 
                Text="* = Required Field"></asp:Label>
        <br />
        <br />
        <br />
    
       <div align="center">
        <asp:Button ID="btnSubmit" runat="server" BackColor="#999999" ForeColor="White" 
            Text="Submit" />
&nbsp;&nbsp;
      <!--  <asp:Button ID="btnCancel" runat="server" BackColor="Red" ForeColor="White" Text="Cancel" /> -->
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ajjpsqlserverdb %>" 
            SelectCommand="SELECT * FROM [ITEM]"></asp:SqlDataSource>
        <br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="RED" Width="250px"></asp:Label>
        <br />
    
    </form></div></div>
</body>
</html>
