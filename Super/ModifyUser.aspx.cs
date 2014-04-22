using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

public partial class Super_ModifyUser : System.Web.UI.Page
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
    public void Button1_Click(object sender, EventArgs e)
    {
        string selectUserInAssignment = "SELECT COUNT(AssignedUserId) " 
            + "FROM ajjp_Incidents " 
            + "WHERE AssignedUserId = '" + DropDownList1.SelectedValue + "'";



        string Username = DropDownList1.SelectedItem.Text;

        try
        {
            DataTable dtCount = DataTypeHandler.GetDataTable(selectUserInAssignment);
            Int32 count = (Int32)dtCount.Rows[0].ItemArray[0];

            if (count > 0)
            {
                v2lbl.Text = "<p class=\"Instruction\">The selected user is actively assigned in open incidents. The incidents must be reassigned or closed before the user can be deleted.</p>";
                v2lbl.Text += "<p><a href=\"../User/IncidentSummary.aspx\">View Incident Summary</a></p>";
                mv.ActiveViewIndex = 1;
            }
            else if (count == 0)
            {
                Membership.DeleteUser(Username);
                v2lbl.Text = "User, " + Username + ", was successfully deleted.";
                mv.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            mv.ActiveViewIndex = 1;
            v2lbl.Text = "Failed to delete user, " + Username + ". Error:<br />" + ex.Message;
        }
    }
}
