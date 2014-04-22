using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Super_ChangePassword : System.Web.UI.Page
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
        SqlTransaction myTransaction = null;
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=ajjpsqlserverdb.db.4338448.hostedresource.com; database=ajjpsqlserverdb; 
                                                            User ID=ajjpsqlserverdb; Password= Devry2010;");

                conn.Open();
                SqlCommand command = conn.CreateCommand();
                string strSQL;
                string txtBoxText = TextBox1.Text;
                txtBoxText = txtBoxText.Replace("'", "''");

                myTransaction = conn.BeginTransaction();
                command.Transaction = myTransaction;

                strSQL = "UPDATE aspnet_Membership SET Password = '" + txtBoxText + "' WHERE UserID = '" + DropDownList1.SelectedValue + "'";

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = strSQL;
                command.ExecuteNonQuery();

                myTransaction.Commit();

                command.Connection.Close();
                Response.Redirect("~/User/Main.aspx");
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }
    }
}
