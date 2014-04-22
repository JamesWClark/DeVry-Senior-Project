<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="jw_Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../aaron/main.css" rel="stylesheet" type="text/css" />
</head>
<body><body class="twoColLiqLtHdr">

<div id="containershadow"><div id="container"> 
  <div id="header">
    <h1><img src="assets/Whelp_logo.png" width="255" height="124" alt="Whelp Logo" /></h1>
  <!-- end #header --></div>
  <div id="sidebar1">
    <h3>sidebar1 Content</h3>
    <p>The background color on this div will only show for the length of the content. If you'd like a dividing line instead, place a border on the left side of the #mainContent div if the #mainContent div will always contain more content than the #sidebar1 div. </p>
    <p>Donec eu mi sed turpis feugiat feugiat. Integer turpis arcu, pellentesque  eget, cursus et, fermentum ut, sapien. Fusce metus mi, eleifend  sollicitudin, molestie id, varius et, nibh. Donec nec libero.</p>
  <!-- end #sidebar1 --></div>
  <div id="mainContent">
    <h1> Main Content </h1>
    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Praesent aliquam,  justo convallis luctus rutrum, erat nulla fermentum diam, at nonummy quam  ante ac quam. Maecenas urna purus, fermentum id, molestie in, commodo  porttitor, felis. Nam blandit quam ut lacus. Quisque ornare risus quis  ligula. Phasellus tristique purus a augue condimentum adipiscing. Aenean  sagittis. Etiam leo pede, rhoncus venenatis, tristique in, vulputate at,  odio. Donec et ipsum et sapien vehicula nonummy. Suspendisse potenti. Fusce  varius urna id quam. Sed neque mi, varius eget, tincidunt nec, suscipit id,  libero. In eget purus. Vestibulum ut nisl. Donec eu mi sed turpis feugiat  feugiat. Integer turpis arcu, pellentesque eget, cursus et, fermentum ut,  sapien. Fusce metus mi, eleifend sollicitudin, molestie id, varius et, nibh.  Donec nec libero.</p>
    <h2>H2 level heading </h2>
    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Praesent aliquam,  justo convallis luctus rutrum, erat nulla fermentum diam, at nonummy quam  ante ac quam. Maecenas urna purus, fermentum id, molestie in, commodo  porttitor, felis. Nam blandit quam ut lacus. Quisque ornare risus quis  ligula. Phasellus tristique purus a augue condimentum adipiscing. Aenean  sagittis. Etiam leo pede, rhoncus venenatis, tristique in, vulputate at, odio.</p>
	<!-- end #mainContent --></div><br class="clearfloat" />
  <div id="footer">
    <ul>
    <li><a href="">CONTACT</a></li>
    <li><a href="">ABOUT US</a></li>
    <li><a href="">INFO</a></li>
    
    
    </ul>
  <!-- end #footer --></div>
<!-- end #container --></div>
<!-- end #containershadow --></div>

    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="txtSearchField" runat="server" Width="400px"></asp:TextBox>
&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" />
    
    </div>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
