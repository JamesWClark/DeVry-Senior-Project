using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.IsInRole("superadmin"))
            Response.Redirect("~/User/Main.aspx");
        else if (Page.User.IsInRole("admin"))
            Response.Redirect("~/User/Main.aspx");
        else if (Page.User.IsInRole("user"))
            Response.Redirect("~/User/Main.aspx");
        else
            Response.Redirect("Login.aspx");
    }
}