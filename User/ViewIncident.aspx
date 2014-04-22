<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewIncident.aspx.cs" Inherits="ViewIncident" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Incident</title>
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
            <hr />
            <asp:Panel ID="pnlPageManagement" runat="server" Visible="False">
                <asp:Label ID="lblPageManagement" runat="server" 
                    Text="Page Management" CssClass="NavLabel"></asp:Label>
                <br />
                <asp:Label ID="lblAssignTo" runat="server" Text="Assign Incident To: "></asp:Label>
                <br />
                <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="dsUsers" 
                    DataTextField="UserName" DataValueField="UserName" AutoPostBack="True" 
                    onprerender="ddlUsers_PreRender" 
                    onselectedindexchanged="ddlUsers_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>
        </div> 
        <div id ="topNav">   
            <ul>
                <li><a href ="AboutUs.aspx">About Us</a></li>
                <li><a href ="Help.aspx">Help</a></li>
            </ul>
        </div>
        <div id="mainContent">
            <p class="Instruction">The object currently in view has the following attributes:</p>
            <asp:Label ID="lblObject" runat="server"></asp:Label>
            <asp:Label ID="lblErr" runat="server"></asp:Label>
            <br />
            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                <asp:View ID="v1" runat="server">
                    <div style="vertical-align: middle">
                        <asp:Button ID="btnAssign" runat="server" Visible="False" 
                            onclick="btnAssign_Click" Text="Take Assignment" />
                    </div>
                    <table class="ObjectTable" style="width:100%;">
                        <tr>
                            <th align="left">
                                <asp:Label ID="lblIncidentNumber" runat="server"></asp:Label>
                            </th>
                            <th align="right">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="AltRow" style="font-weight: bold">
                                Description:</td>
                            <td align="right" class="AltRow">
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                &nbsp;<asp:Label ID="lblUser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="AltRow" colspan="2" style="font-weight: bold">
                                Notes:</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblNotes" runat="server"></asp:Label>
                                <asp:TextBox ID="txtNoteInput" runat="server" Height="100px" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="AltRow" style="font-weight: bold">
                                Solution:</td>
                            <td align="right" class="AltRow" style="font-weight: bold">
                                <asp:Label ID="lblSolutionUser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtSolution" runat="server" Height="100px" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="submitError" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" 
                        oncommand="btnSubmit_Command" Width="75px" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Cancel" CausesValidation="False" Width="75px" />
                    <br />
                    <br />
                    <asp:SqlDataSource ID="dsUsers" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AjjpSqlServerDBConnectionString %>" 
                        SelectCommand="SELECT [UserName] FROM [vw_aspnet_Users] ORDER BY [UserName]">
                    </asp:SqlDataSource>
                </asp:View>
                <asp:View ID="v2" runat="server">
                    Your data was successfully stored.<br />
                    <br />
                    <asp:Button ID="btnReturn" runat="server" Text="Return" 
                        onclick="btnReturn_Click" />
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</form>
</div>
</body>
</html>
