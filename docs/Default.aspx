<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="docs_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
&nbsp;<br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;<asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" 
            Text="Upload" />
        <br />
&nbsp;<asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
        <br />
    </div>
    <hr />
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
    </form>
</body>
</html>
