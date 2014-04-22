<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAdminUser.aspx.cs" Inherits="CreateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Admin User</title>
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
                <li><a href ="../User/AboutUs.aspx">About Us</a></li>
                <li><a href ="../User/Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">
            <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
                Height="152px" oncreateduser="CreateUserWizard1_CreatedUser" Width="361px" 
                LoginCreatedUser="False">
                <WizardSteps>
                    <asp:CreateUserWizardStep runat="server" >
                    </asp:CreateUserWizardStep>
                    <asp:CompleteWizardStep runat="server" >
                        <ContentTemplate>
                            <table border="0" style="font-size:100%;height:152px;width:361px;">
                                <tr>
                                    <td align="center" colspan="2">
                                        Complete</td>
                                </tr>
                                <tr>
                                    <td>
                                        Your account has been successfully created.</td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" 
                                            CommandName="Continue" PostBackUrl="../User/Main.aspx" Text="Continue" 
                                            ValidationGroup="CreateUserWizard1" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:CompleteWizardStep>
                </WizardSteps>
            </asp:CreateUserWizard>
        </div>
    </div>
</form>
</div>
</body>
</html>
