using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class jw_ViewItemIncidents : System.Web.UI.Page
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
        lblErr.Text = String.Empty;
        if (IsValidPageRequest())//testing the session bool hasItem, sent from ViewItem.aspx
        {
            //getting session variables - present when the valid page check passes
            string[] headers = (string[])Session["ViewItems_GridView_Headers"];
            string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"];
            DisplayObjectTable(headers, gvString);

            string objectGuid = (string)Session["SelectedObjectGuid"];
            btnCreate.CommandArgument = objectGuid;
            ShowIncidents(objectGuid);

            /*TEST STUBS
            lblErr.Text += "<br /><br />headers[]: ";
            for (int i = 0; i < headers.Length; i++)
                lblErr.Text += headers[i] + ",<br />";
            lblErr.Text += "<br /><br />gvString[]: ";
            for (int i = 0; i < gvString.Length; i++)
                lblErr.Text += gvString[i] + ",<br />";
            lblErr.Text += "<br /><br />Inbound objectGuid: " + objectGuid;
             * */
        }
        else
        {
            lblErr.Text = "Error - failed to pass data or data was empty.";
        }
    }
    protected bool IsValidPageRequest()
    {
        bool found = false;
        try
        {
            found = (bool)Session["hasItem"];
        }
        catch (Exception)
        {
            lblErr.Text = "You have loaded this page outside of the navigation scope. For this page to load correctly, you must first select a database object from ";
            lblErr.Text += "<a href=\"ViewItems.aspx\">View Items</a> page. ";
        }      
        return found;
    }
    protected void ShowIncidents(string objectGuid)
    {
        //lblErr.Text = "Inbound objectGuid is: " + objectGuid;
        string queryIncidents = "SELECT * FROM ajjp_Incidents WHERE objectguid = '" + objectGuid + "' ORDER BY status ASC";

        //lblErr.Text += "<br /><br />ShowIncidents(string objectGuid): " + queryIncidents;

        int indexIncidentId = 0;
        //int indexObjectGuid = 1;
        //int indexObjectlabel = 2;
        int indexTitle = 3;
        int indexUserId = 4;
        int indexDate = 5;
        int indexDescription = 6;
        int indexStatus = 7;
        //int indexAssignedUserId = 8;
        //int indexAssignedUserName = 10;
        //int indexSolutionId = 11;
        
        //try
        //{
            DataTable dt = DataTypeHandler.GetDataTable(queryIncidents);

            int count = 0;
            if (dt.Rows.Count > 0)
            {
                string[] incidentIds = new string[dt.Rows.Count];
                foreach (DataRow r in dt.Rows)
                {
                    //IncidentId foreach incident row
                    int thisId = (int)r.ItemArray[indexIncidentId];
                    incidentIds[count] = thisId.ToString();
                    bool hasIncidentGuid = true;
                    Session["HasIncidentGuid"] = hasIncidentGuid;

                    Table tblIncident = new Table();
                    tblIncident.CssClass = "ObjectTable";

                    //row: incident, status
                    TableHeaderRow hr = new TableHeaderRow();
                    TableHeaderCell cIncident = new TableHeaderCell(); cIncident.ColumnSpan = 2; cIncident.HorizontalAlign = HorizontalAlign.Left;
                    TableHeaderCell cStatus = new TableHeaderCell(); cStatus.ColumnSpan = 2; cStatus.HorizontalAlign = HorizontalAlign.Right;
                    Int32 incidentNumber = (Int32)r.ItemArray[indexIncidentId];
                    LinkButton btnIncident = new LinkButton();
                    btnIncident.Command += LinkButton_Command;
                    btnIncident.Text = "Incident #" + incidentNumber.ToString();
                    btnIncident.CommandArgument = incidentNumber.ToString();
                    cIncident.Controls.Add(btnIncident);
                    cStatus.Text = GetIncidentStatus(r, indexStatus, indexUserId, thisId);
                    hr.Cells.Add(cIncident); hr.Cells.Add(cStatus);
                    tblIncident.Rows.Add(hr);

                    //row: entered on, entered by
                    TableRow rEntered = new TableRow();
                    TableCell cDate = new TableCell(); cDate.ColumnSpan = 2;
                    TableCell cEnteredBy = new TableCell(); cEnteredBy.ColumnSpan = 2; cEnteredBy.HorizontalAlign = HorizontalAlign.Right;
                    cDate.Text = "<b>Entered On</b>: " + (string)r.ItemArray[indexDate];
                    cEnteredBy.Text = "<b>Entered By</b>: " + Page.User.Identity.Name;
                    rEntered.Cells.Add(cDate); rEntered.Cells.Add(cEnteredBy);
                    tblIncident.Rows.Add(rEntered);

                    //row: title
                    TableRow rTitle = new TableRow();
                    TableCell cTitle = new TableCell(); cTitle.ColumnSpan = 3;
                    cTitle.Text = "<b>Title</b>: " + (string)r.ItemArray[indexTitle];
                    rTitle.Cells.Add(cTitle);
                    tblIncident.Rows.Add(rTitle);

                    //row:description
                    TableRow rDesc = new TableRow();
                    TableCell cDesc = new TableCell(); cDesc.ColumnSpan = 4;
                    cDesc.Text = "<b>Description</b>: " + (string)r.ItemArray[indexDescription];
                    rDesc.Cells.Add(cDesc);
                    tblIncident.Rows.Add(rDesc);

                    //row: blank space
                    Table tblBlank = new Table();
                    TableRow rBlank = new TableRow();
                    TableCell cBlank = new TableCell();
                    cBlank.ColumnSpan = 4;
                    cBlank.Text = "&nbsp;<br />";
                    rBlank.Cells.Add(cBlank);
                    tblBlank.Rows.Add(rBlank);
                    phIncidents.Controls.Add(tblBlank);

                    //add table to placeholder
                    phIncidents.Controls.Add(tblIncident);

                    count++;
                }
                Session["ViewItemIncidents_IncidentIds_Array"] = incidentIds;
            }
        //}
        //catch (Exception ex)
        //{
        //    lblErr.Text += "<br />Failed to proceed: " + ex.Message;
        //}
    }
    protected string GetIncidentStatus(DataRow row, int indexStatus, int indexUser, int incidentId)
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
                    + "AND i.IncidentId = '" + incidentId + "'";

                DataTable userName = DataTypeHandler.GetDataTable(sqlSelect);
                //lblErr.Text += sqlSelect;

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
    protected void DisplayObjectTable(string[] headers, string [] session)
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
    protected void LinkButton_Command(object sender, CommandEventArgs e)
    {
        Session["OneIncidentGuid"] = (string)e.CommandArgument;
        Response.Redirect("ViewIncident.aspx");
    }
    protected void btnCreate_Command(object sender, CommandEventArgs e)
    {
        Session["OneIncidentGuid"] = (string)e.CommandArgument;
        Response.Redirect("AddIncident.aspx");
    }
}