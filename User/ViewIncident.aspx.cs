using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ViewIncident : System.Web.UI.Page
{
    static string prevPage = String.Empty;
    protected override void OnInit(EventArgs e)
    {
        try
        {
            if (Page.User.IsInRole("superadmin"))
            {
                Nav.SetLeftNav(phNav, "superadmin");
                pnlPageManagement.Visible = true;
            }
            else if (Page.User.IsInRole("admin"))
            {
                Nav.SetLeftNav(phNav, "admin");
                pnlPageManagement.Visible = true;
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
        btnAssign.Visible = false;

        if (IsValidPageRequest())//from ViewItemIncidents.ShowIncidents()
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }

            //getting session variables - present when the valid page check passes
            string[] headers = (string[])Session["ViewItems_GridView_Headers"];
            string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"]; //format: table name, cell contents, guid
            DisplayObjectTable(headers, gvString);

            string guid = (string)Session["OneIncidentGuid"];
            btnSubmit.CommandArgument = guid;

            SetTable(guid);

            if (lblStatus.Text == "Status: Open")
            {
                if(!Page.User.IsInRole("user"))
                    pnlPageManagement.Visible = true;
                btnAssign.Text = "Take Assignment";
                btnAssign.Visible = true;
            }
            else if (lblStatus.Text == "Status: Closed")
            {
                pnlPageManagement.Visible = false;
                btnAssign.Text = "Re-Open";
                btnAssign.Visible = true;
            }
            //check for a solution, if yes - turn on close button
            string selSolution = "SELECT COUNT(Solution) FROM ajjp_Incidents WHERE IncidentId = '" + guid + "'";
            DataTable dt = DataTypeHandler.GetDataTable(selSolution);
            Int32 countSolution = (Int32)dt.Rows[0].ItemArray[0];
        }
        else
        {
            lblObject.Text = "Error - failed to pass data or data was empty.";
        }
    }
    protected void SetTable(string guid)
    {
        string queryIncident = "SELECT * FROM ajjp_Incidents WHERE incidentid = '" + guid + "'";

        int indexIncidentId = 0;
        //int indexObjectGuid = 1;
        //int indexObjectLabel = 2;
        int indexTitle = 3;
        int indexUser = 4;
        int indexDate = 5;
        int indexDescription = 6;
        int indexStatus = 7;
        //int indexAssignedUserId = 9;
        //int indexSolutionId = 10;
        DataTable dt = DataTypeHandler.GetDataTable(queryIncident);

        lblIncidentNumber.Text = "Incident #" + ((Int32)dt.Rows[0].ItemArray[indexIncidentId]).ToString();
        lblStatus.Text = GetIncidentStatus(dt.Rows[0], indexStatus, indexUser);
        lblTitle.Text = "<b>Title</b>: " + (string)dt.Rows[0].ItemArray[indexTitle];
        lblDate.Text = "<b>Entered On</b>: " + (string)dt.Rows[0].ItemArray[indexDate];
        lblUser.Text = "<b>Entered By</b>: " + (string)dt.Rows[0].ItemArray[indexUser];
        lblDescription.Text = (string)dt.Rows[0].ItemArray[indexDescription];
        SetNotes(guid);
    }
    protected bool IsValidPageRequest()
    {
        bool found = false;
        try
        {
            found = (bool)Session["HasIncidentGuid"]; //from ViewItemIncidents.ShowIncidents()
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
        lblObject.Text = "<p><b><u>" + session[0] + "</u></b> - <a href=\"EditInventoryItem.aspx\">edit</a><br />";
        for (int i = 1; i < headers.Length; i++)
        {
            lblObject.Text += "<b>" + headers[i - 1] + "</b>: " + session[i];
            if (i < headers.Length - 1)
            {
                lblObject.Text += "<br />";
            }
        }
        lblObject.Text += "</p>";
    }
    protected string GetIncidentStatus(DataRow row, int indexStatus, int indexUser)
    {
        string display = String.Empty;
        string text = (string)row.ItemArray[indexStatus];
        switch (text)
        {
            case "Assigned":
                {
                    //get associated userName from aspnet_Users
                    string sqlSelect = "SELECT u.UserName "
                        + "FROM aspnet_Users u, ajjp_Incidents i "
                        + "WHERE u.UserId = i.AssignedUserId " 
                        + "AND i.IncidentId = '" + (string)Session["OneIncidentGuid"] + "'";

                    DataTable userName = DataTypeHandler.GetDataTable(sqlSelect);

                    display = "Assigned: " + (string)userName.Rows[0].ItemArray[0];
                    break;
                }
            case "Closed":
                {
                    display = "Status: Closed";
                    break;
                }
            case "Open":
                {
                    display = "Status: Open";
                    break;
                }
        }
        return display;
    }
    protected void SetNotes(string incidentId)
    {
        lblNotes.Text = String.Empty;

        string queryNotes = "SELECT n.Note, n.UserName, n.DateEntered "
            + "FROM ajjp_Incidents inc, ajjp_NotesInIncidents nii, ajjp_Notes n "
            + "WHERE inc.IncidentId = '" + incidentId + "' "
            + "AND nii.IncidentId = '" + incidentId + "' "
            + "AND nii.NoteId = n.NoteId "
            + "ORDER BY OrderOfAppearance";

        DataTable dt = DataTypeHandler.GetDataTable(queryNotes);
        foreach (DataRow row in dt.Rows)
        {
            DateTime datetime = (DateTime)row.ItemArray[2];
            string date = datetime.ToString("MM-dd-yyyy");

            lblNotes.Text += "<b>By</b>: " + (string)row.ItemArray[1] + " <b>On</b>: " + date + "<br />" + (string)row.ItemArray[0] + "<hr />";
        }
        try
        {
            string selectSolution = "SELECT Solution,SolutionUser,SolutionDate FROM ajjp_Incidents WHERE IncidentId = '" + incidentId + "'";
            DataTable dtSolution = DataTypeHandler.GetDataTable(selectSolution);
            if (dt.Rows.Count > 0)
            {
                txtSolution.Text = (string)dtSolution.Rows[0].ItemArray[0];
                lblSolutionUser.Text = "<b>Solution Proposed By</b>: " + (string)dtSolution.Rows[0].ItemArray[1]
                    + " <b>On: </b> " + (string)dtSolution.Rows[0].ItemArray[2];
            }
        }
        catch (Exception)
        {//do nothing
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }
    protected void btnSubmit_Command(object sender, CommandEventArgs e)
    {
        try
        {
            if (txtNoteInput.Text.Trim().Length == 0 && txtSolution.Text.Trim().Length == 0)
            {
                string solutionUpdate = "UPDATE ajjp_Incidents SET Solution = NULL,"
                    + "SolutionUser = NULL,"
                    + "SolutionDate = NULL"
                    + "WHERE IncidentId = '" + (string)e.CommandArgument + "'";
                DataTypeHandler.ExecuteNonQuery(solutionUpdate);
                txtSolution.Text = String.Empty;
            }
            else
            {
                //note inserts: ajjp_Notes, ajjp_NotesInIncidents
                if (txtNoteInput.Text.Trim().Length > 0)
                {
                    NoteBuilder.InsertNote(txtNoteInput.Text.Trim(), Page.User.Identity.Name, (string)e.CommandArgument);
                    txtNoteInput.Text = String.Empty;
                }
                //solution update: ajjp_Incidents
                if (txtSolution.Text.Trim().Length > 0)
                {
                    string solution = txtSolution.Text.Trim();
                    solution = solution.Replace("'", "''");
                    string solutionUpdate = "UPDATE ajjp_Incidents SET Status = 'Closed',"
                        + "Solution = '" + solution
                        + "', SolutionUser = '" + Page.User.Identity.Name
                        + "', SolutionDate = '" + DateTime.Now.ToString("MM-dd-yyyy")
                        + "' WHERE IncidentId = '" + (string)e.CommandArgument + "'";
                    DataTypeHandler.ExecuteNonQuery(solutionUpdate);

                    NoteBuilder.InsertNote("Saved for close.", Page.User.Identity.Name, (string)e.CommandArgument);
                    txtSolution.Text = String.Empty;
                    lblStatus.Text = "Status: Closed";
                }
                mv.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            submitError.Text = "Failed to Save:<br />" + ex.Message;
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        mv.ActiveViewIndex = 0;
        Response.Redirect("ViewIncident.aspx");
    }
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUsers.SelectedItem.ToString() != "--")
        {
            string sqlUpdate = "UPDATE ajjp_Incidents SET AssignedUserId = "
                + "(SELECT UserId FROM aspnet_Users WHERE UserName = '" + ddlUsers.SelectedItem + "'),"
                + "AssignedUserName = '" + ddlUsers.SelectedItem + "',"
                + "STATUS = 'Assigned' WHERE IncidentId = '" + (string)Session["OneIncidentGuid"] + "'";

            try
            {
                DataTypeHandler.ExecuteNonQuery(sqlUpdate);
                lblStatus.Text = "Assigned To: " + ddlUsers.SelectedItem;

                string note = "Assigned To: " + ddlUsers.SelectedItem;
                string noteUser = ddlUsers.SelectedItem.Text;
                string incidentId = (string)Session["OneIncidentGuid"];

                NoteBuilder.InsertNote(note, Page.User.Identity.Name, incidentId);
                lblNotes.Text += "<b>By</b>: " + Page.User.Identity.Name + " <b>On</b>: " + DateTime.Now.ToString("MM-dd-yyyy") 
                    + "<br />" + note + "<hr />";
                btnAssign.Visible = false;
            }
            catch (Exception)
            {
            }
        }
    }
    protected void ddlUsers_PreRender(object sender, EventArgs e)
    {
        if(!IsPostBack)
            ddlUsers.Items.Insert(0, "--");
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string text = btnAssign.Text;

        switch (text)
        {
            case "Take Assignment":
            {
                btnAssign.Visible = false;
                if (!Page.User.IsInRole("user"))
                    pnlPageManagement.Visible = true;

                string loggedInUser = Page.User.Identity.Name;

                string sqlUpdate = "UPDATE ajjp_Incidents SET AssignedUserId = "
                    + "(SELECT UserId FROM aspnet_Users WHERE UserName = '" + loggedInUser + "'),"
                    + "AssignedUserName = '" + loggedInUser + "',"
                    + "STATUS = 'Assigned' WHERE IncidentId = '" + (string)Session["OneIncidentGuid"] + "'";

                try
                {
                    DataTypeHandler.ExecuteNonQuery(sqlUpdate);
                    lblStatus.Text = "Assigned To: " + loggedInUser;

                    string note = "Assigned To: " + loggedInUser;
                    string noteUser = loggedInUser;
                    string incidentId = (string)Session["OneIncidentGuid"];

                    NoteBuilder.InsertNote(note, loggedInUser, incidentId);
                    lblNotes.Text += "<b>By</b>: " + loggedInUser + " <b>On</b>: " + DateTime.Now.ToString("MM-dd-yyyy")
                        + "<br />" + note + "<hr />";
                }
                catch (Exception)
                {
                }
                break;
            }
            case "Re-Open":
            {
                try
                {
                    string sqlSelect = "SELECT Solution, SolutionUser, SolutionDate FROM ajjp_Incidents "
                        + "WHERE IncidentId = '" + (string)Session["OneIncidentGuid"] + "'";

                    DataTable dtSolution = DataTypeHandler.GetDataTable(sqlSelect);
                    string[] solution = new string[3];
                    solution[0] = (string)dtSolution.Rows[0].ItemArray[0];
                    solution[1] = (string)dtSolution.Rows[0].ItemArray[1];
                    solution[2] = ((DateTime)dtSolution.Rows[0].ItemArray[2]).ToString("MM-dd-yyyy");

                    string sqlUpdate = "UPDATE ajjp_Incidents SET AssignedUserId = NULL,"
                        + "AssignedUserName = NULL,"
                        + "Solution = NULL,"
                        + "SolutionUser = NULL,"
                        + "SolutionDate = NULL,"
                        + "STATUS = 'Open' WHERE IncidentId = '" + (string)Session["OneIncidentGuid"] + "'";
                    NoteBuilder.InsertNote("Re-opened, previous solution by <i>" + solution[1]
                        + " " + solution[2] + "</i> moved to note: " + solution[0], Page.User.Identity.Name, (string)Session["OneIncidentGuid"]);
                    DataTypeHandler.ExecuteNonQuery(sqlUpdate);
                    lblNotes.Text += "Re-opened, previous solution by <i>" + solution[1]
                        + " " + solution[2] + "</i> moved to note: " + solution[0] + "<hr />";
                    txtSolution.Text = String.Empty;
                    lblStatus.Text = "Status: Open";
                    btnAssign.Text = "Take Assignment";
                    btnAssign.Visible = true;
                    if (!Page.User.IsInRole("user"))
                        pnlPageManagement.Visible = true;

                    //lblObject.Text = "<br /><br />" + sqlSelect;
                    //lblObject.Text += "<br /><br />" + sqlUpdate;
                }
                catch (Exception ex)
                {
                    lblErr.Text = "<br /><br />Failed to re-open:<br />" + ex.Message;
                }
                break;
            }
        }
    }
    protected bool PreviousSolutionExists()
    {
        bool exists = false;

        string selSolution = "SELECT COUNT(Solution) FROM ajjp_Incidents WHERE IncidentId = '" 
            + (string)Session["OneIncidentGuid"] + "'";
        DataTable dt = DataTypeHandler.GetDataTable(selSolution);
        Int32 countSolution = (Int32)dt.Rows[0].ItemArray[0];

        if (countSolution == 1)
        {
            exists = true;
        }
        else
        {
            exists = false;
        }
        return exists;
    }
}