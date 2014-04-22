<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewObject.aspx.cs" Inherits="Super_NewObject" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Object</title>
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
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <p class="Instruction">Create a new master object only after careful planning. This feature of the Whelp application is only suggested for those individuals with a working knowledge of relational data, SQL syntax, and access to this system's database server.</p>
                    What is the name of this new object?
                    <asp:TextBox ID="v1txtName" runat="server" TabIndex="1"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="v1valName" runat="server" 
                        ControlToValidate="v1txtName">* This is a required field. Enter a name for the new object.</asp:RequiredFieldValidator>
                    <br />
                    How many fields will it relate to?
                    <asp:TextBox ID="v1txtFields" runat="server" TabIndex="2"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="v1valFields" runat="server" 
                        ControlToValidate="v1txtFields" 
                        
                        
                        ErrorMessage="* This is a required Field. Enter a number between 1 and 255."></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Button ID="v1next" runat="server" onclick="v1next_Click" Text="Next" />
                    <br />
                    <br />
                    <asp:Label ID="v1err" runat="server"></asp:Label>
                </asp:View>
                <asp:View ID="View2" runat="server" onactivate="View2_Activate">
                    <p class="Instruction">Enter a name and data type for each field.</p>
                    <asp:Table ID="v2table" 
                        runat="server">
                    </asp:Table>
                    <br />
                    <asp:Button ID="v2prev" runat="server" CausesValidation="False" 
                        CommandName="PrevView" Text="Previous" />
                    &nbsp;<asp:Button ID="v2next" runat="server" Text="Next" 
                        onclick="v2next_Click" />
                    <br />
                    <br />
                    <asp:Label ID="v2err" runat="server"></asp:Label>
                </asp:View>
                <asp:View ID="View3" runat="server" onactivate="View3_Activate">
                    <p class="Instruction">Verify the following information is accurate:</p>
                    <asp:Table ID="v3table" runat="server">
                    </asp:Table>
                    <br />
                    <asp:Button ID="v3prev" runat="server" CausesValidation="False" 
                        CommandName="PrevView" Text="Previous" />
                    &nbsp;<asp:Button ID="v3finish" runat="server" onclick="v3finish_Click" 
                        Text="Finish" />
                    <br />
                    <asp:Label ID="v3err" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="v31" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="v32" runat="server"></asp:Label>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    The new object was successfully added.<br />
                    <br />
                    <asp:Label ID="v4err" runat="server"></asp:Label>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</form>
</div>
</body>
</html>
