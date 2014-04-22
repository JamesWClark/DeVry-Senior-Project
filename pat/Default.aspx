<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="file:///Macintosh HD/Users/aaronbaker/Desktop/main.css" rel="stylesheet" type="text/css" />

</head>
<body><div id="header"></div>
<div class="Login">
    <div class="insidelogin"><form id="form1" runat="server">
  <div>
    
       <asp:LoginStatus ID="LoginStatus1" runat="server" /></div>
    

    <p>
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                Please login.
            </AnonymousTemplate>
            <LoggedInTemplate>
                You have no roles. See a system administrator to get roles.
            </LoggedInTemplate>
            <RoleGroups>
                <asp:RoleGroup Roles="superadmin">
                    <ContentTemplate>
      <div class="links"><a><asp:HyperLink ID="HyperLink1" CssClass="links" runat="server" NavigateUrl="~/Super/Main.aspx">Admin Main</asp:HyperLink></a></div>
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="admin">
                    <ContentTemplate>
                        <div class="links"><asp:HyperLink ID="HyperLink2" CssClass="links" runat="server" NavigateUrl="~/Admin/Main.aspx">Admin Main</asp:HyperLink></div>
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="user">
                    <ContentTemplate>
                        <div class="links"><asp:HyperLink ID="HyperLink3" CssClass="links" runat="server" NavigateUrl="~/User/Main.aspx">User Main</asp:HyperLink></div>
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
        </asp:LoginView>
    </p>
    </form></div> 
</div>
</body>
</html>
