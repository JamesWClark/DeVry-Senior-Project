using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Super_SelectUser : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            if (Page.User.IsInRole("superadmin"))
                Nav.SetLeftNav(phNav, "superadmin");
            else if (Page.User.IsInRole("admin"))
                Nav.SetLeftNav(phNav, "admin");
            else if (Page.User.IsInRole("user"))
                Nav.SetLeftNav(phNav, "user");
        }
        catch (Exception)
        {
            Label l = new Label();
            l.Text = "DB Connection Failed On Page Load.";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String UserRole = rblRoles.Text;
        {
            if (UserRole == "superadmin")
            {
                Response.Redirect("../super/CreateSuperUser.aspx");
            }
            else if (UserRole == "admin")
            {
                Response.Redirect("../admin/CreateAdminUser.aspx");
            }
            else if (UserRole == "user")
            {
                Response.Redirect("../admin/createbasicuser.aspx");
            }
        }
    }
}
