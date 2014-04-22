using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class User_ChangeUserPassword : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            if (Page.User.IsInRole("superadmin"))
            {
                Nav.SetLeftNav(phNav, "superadmin");
            }
            else if (Page.User.IsInRole("admin"))
            {
                Nav.SetLeftNav(phNav, "admin");
            }
            else if (Page.User.IsInRole("user"))
            {
                Nav.SetLeftNav(phNav, "user");
            }
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
}