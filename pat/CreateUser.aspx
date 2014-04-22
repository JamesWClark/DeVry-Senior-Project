<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="file:///Macintosh HD/Users/aaronbaker/Desktop/main.css" rel="stylesheet" type="text/css" />
</head>
<body><div id="header"></div>
       <div id="container"><div id="sidebar1">
        <ul> 
        <h3>APPLICATION LINKS:</h3>
        <li><a href="../AddItem.aspx">Add Items</a></li>
        <li><a href="../DeleteItem.aspx">Delete Items</a></li>
        <li><a href="../AllItems.aspx">All Items</a></li>
        <li><a href="../NameCheck.aspx">Name Check</a></li>
        </ul>
		<ul>
       <h3> ACCOUNT LINKS:</h3>
        <li><a href="../CreateUser.aspx">Add New User</a></li>
        <li><a href="../DeleteItem.aspx">Remove User</a></li>
        <li><a href="../ModifyUser.aspx">Modify User</a></li>
        <li><a href="../NameCheck.aspx">Change User Password</a></li>
        </ul>
       </div>
   


    <div id="mainContent">
    <form id="form1" runat="server">
    <div>
    
        <br />
    
    </div>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        Width="294px" Height="122px" oncreateduser="CreateUserWizard1_CreatedUser" 
        style="text-align: left" ActiveStepIndex="1">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" >
                <ContentTemplate>
                    <table border="0" style="font-size:100%;height:122px;width:294px;">
                        <tr>
                            <td align="center" colspan="2">
                                Sign Up for Your New Account</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Confirm Password is required." 
                                    ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Security Question:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                    ControlToValidate="Question" ErrorMessage="Security question is required." 
                                    ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Security Answer:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                    ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                                    ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="text-align: center">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                    Display="Dynamic" 
                                    ErrorMessage="The Password and Confirmation Password must match." 
                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:WizardStep runat="server" Title="Choose Role">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="RoleName" 
                    DataValueField="RoleName">
                </asp:RadioButtonList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDB %>" 
                    SelectCommand="SELECT [RoleName] FROM [vw_aspnet_Roles]">
                </asp:SqlDataSource>
            </asp:WizardStep>
            <asp:CompleteWizardStep runat="server" >
                <ContentTemplate>
                    <table border="0" style="font-size:100%;height:122px;width:294px;">
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
                                    CommandName="Continue" PostBackUrl="~/Super/Main.aspx" Text="Continue" 
                                    ValidationGroup="CreateUserWizard1" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
                                <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    </form></div></div>
</body>
</html>
