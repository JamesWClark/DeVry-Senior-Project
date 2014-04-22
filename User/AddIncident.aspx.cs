using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class jw_AddIncident : System.Web.UI.Page
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
        if (IsValidPageRequest())//from ViewItemIncidents.ShowIncidents()
        {
            //getting session variables - present when the valid page check passes
            string[] headers = (string[])Session["ViewItems_GridView_Headers"];
            string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"]; //format: table name, cell contents, guid
            DisplayObjectTable(headers, gvString);

            string incidentId = (string)Session["OneIncidentId"];
            string objectLabel = gvString[0];
            btnSubmit.CommandArgument = incidentId;
            btnSubmit.CommandName = objectLabel;

            //string queryIncident = "SELECT * FROM ajjp_Incidents WHERE incidentid = '" + incidentId + "'";
        }
        else
        {
            lblErr.Text = "Error - failed to pass data or data was empty.";
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            lblDescHeader.Visible = false;
            txtDescription.Visible = false;
        }
    }
    protected bool IsValidPageRequest()
    {
        bool found = false;
        try
        {
            found = (bool)Session["hasItem"]; //from ViewItems.gvObjects_SelectedIndexChanged
        }
        catch (Exception)
        {
            lblErr.Text = "You have loaded this page outside of the navigation scope. For this page to load correctly, you must first select a database object from ";
            lblErr.Text += "<a href=\"ViewItems.aspx\">View Items</a> page.";
        }
        return found;
    }
    protected void DisplayObjectTable(string[] headers, string[] session)
    {
        lblObject.Text = "<p><b><u>" + session[0] + "</u></b><br />";
        for (int i = 1; i < headers.Length - 1; i++)
        {
            lblObject.Text += "<b>" + headers[i] + "</b>: " + session[i];
            if (i < headers.Length - 2)
            {
                lblObject.Text += "<br />";
            }
        }
        lblObject.Text += "</p>";
    }
    protected void btnSubmit_Command(object sender, CommandEventArgs e)
    {
        /* ajjp_Incidents
                            IncidentId	int - seeded in table as auto-generated int, do not try to pass by application
                          0-ObjectGuid	uniqueidentifier
                          1-ObjectLabel	varchar
                          2-Title		varchar
                          3-UserName	nvarchar
                          4-DateEntered	nchar
                          5-Description	varchar
                            NotesId		uniqueidentifier - null until user enters notes later in app
                          6-OpenClosed	varchar
                          7-SolutionId	uniqueidentifier - null until user enter solution later in app
         */
        string[] insertValues = new string[7];
        insertValues[0] = (string)Session["SelectedObjectGuid"];
        insertValues[1] = btnSubmit.CommandName;
        insertValues[2] = txtTitle.Text;
        insertValues[2] = insertValues[2].Replace("'", "''");//SQL Server input should replace ' with '' to display a single ' intead of throwing errors on input
        insertValues[3] = Page.User.Identity.Name;
        insertValues[4] = DateTime.Now.ToString("MM-dd-yyyy");
        insertValues[5] = txtDescription.Text.Trim();
        insertValues[5] = insertValues[5].Replace("'", "''");//SQL Server input should replace ' with '' to display a single ' intead of throwing errors on input
        insertValues[6] = "Open";

        string sqlInsert = "INSERT INTO ajjp_Incidents(ObjectGuid,ObjectLabel,Title,UserName,DateEntered,Description,Status) "
            + "VALUES(";
        for (int i = 0; i < insertValues.Length; i++)
        {
            sqlInsert += "'" + insertValues[i] + "'";
            if (i < insertValues.Length - 1)
                sqlInsert += ",";
            else
                sqlInsert += ")";
        }
        try
        {
            DataTypeHandler.ExecuteNonQuery(sqlInsert);
            Response.Redirect("~/user/ViewItemIncidents.aspx");
        }
        catch (Exception ex)
        {
            submitError.Text = "There was a problem connecting to the database.<br />Error Message: " + ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewItemIncidents.aspx");
    }
}