using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class User_IncidentSummary : System.Web.UI.Page
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
    protected void gvSummary_SelectedIndexChanged(object sender, EventArgs e)
    {  
        lblErr.Text = String.Empty;//just in case ?

        int rowIndex = gvSummary.SelectedIndex;//the clicked row index
        string objectLabel = gvSummary.DataKeys[rowIndex].Values["ObjectLabel"].ToString();
        string selectedObjectGuid = gvSummary.DataKeys[rowIndex].Values["ObjectGuid"].ToString();
        string incidentId = gvSummary.Rows[rowIndex].Cells[1].Text;
        string selObjectFromTable = "SELECT * FROM obj_" + objectLabel + " "
            + "WHERE " + objectLabel + "_guid = '" + selectedObjectGuid + "'";

        DataTable dt = DataTypeHandler.GetDataTable(selObjectFromTable);
        int numCells = dt.Columns.Count;//the number of cells in the gridview to pass

        /*TEST STUBS
        lblErr.Text += "<br />objectLabel: " + objectLabel;
        lblErr.Text += "<br />selectedObjectGuid: " + selectedObjectGuid;
        lblErr.Text += "<br />incidentId: " + incidentId;
        lblErr.Text += "<br /><br />selObjectFromTable: " + selObjectFromTable;
         * */
        
        string[] selectedRow = new string[(numCells + 1)];
        string[] headers = new string[numCells];

        selectedRow[0] = objectLabel;//add the table name to the selectedRow string array, so it can pass into the incidents page for further query.
        for (int i = 1; i < numCells; i++)// += 2 because two array indexes are being assigned per loop iteration
        {
            headers[i - 1] = dt.Columns[i].ColumnName;
            selectedRow[i] = dt.Rows[0].ItemArray[i].ToString();
        }

        bool hasIncidentGuid = true;
        Session["HasIncidentGuid"] = hasIncidentGuid;
        Session["OneIncidentGuid"] = incidentId;
        Session["SelectedObjectGuid"] = selectedObjectGuid;
        Session["ViewItems_GridView_Headers"] = headers;
        Session["hasItem"] = true;
        Session["ViewItems_GridView_SelectedIndex_Row"] = selectedRow;
        Response.Redirect("ViewIncident.aspx");
    }
    protected void gvOpenAssigned_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErr.Text = String.Empty;//just in case ?

        int rowIndex = gvOpenAssigned.SelectedIndex;//the clicked row index
        string objectLabel = gvOpenAssigned.DataKeys[rowIndex].Values["ObjectLabel"].ToString();
        string selectedObjectGuid = gvOpenAssigned.DataKeys[rowIndex].Values["ObjectGuid"].ToString();
        string incidentId = gvOpenAssigned.Rows[rowIndex].Cells[1].Text;
        string selObjectFromTable = "SELECT * FROM obj_" + objectLabel + " "
            + "WHERE " + objectLabel + "_guid = '" + selectedObjectGuid + "'";

        DataTable dt = DataTypeHandler.GetDataTable(selObjectFromTable);
        int numCells = dt.Columns.Count;//the number of cells in the gridview to pass

        /*TEST STUBS
        lblErr.Text += "<br />objectLabel: " + objectLabel;
        lblErr.Text += "<br />selectedObjectGuid: " + selectedObjectGuid;
        lblErr.Text += "<br />incidentId: " + incidentId;
        lblErr.Text += "<br /><br />selObjectFromTable: " + selObjectFromTable;
         * */

        string[] selectedRow = new string[(numCells + 1)];
        string[] headers = new string[numCells];

        selectedRow[0] = objectLabel;//add the table name to the selectedRow string array, so it can pass into the incidents page for further query.
        for (int i = 1; i < numCells; i++)// += 2 because two array indexes are being assigned per loop iteration
        {
            headers[i - 1] = dt.Columns[i].ColumnName;
            selectedRow[i] = dt.Rows[0].ItemArray[i].ToString();
        }

        bool hasIncidentGuid = true;
        Session["HasIncidentGuid"] = hasIncidentGuid;
        Session["OneIncidentGuid"] = incidentId;
        Session["SelectedObjectGuid"] = selectedObjectGuid;
        Session["ViewItems_GridView_Headers"] = headers;
        Session["hasItem"] = true;
        Session["ViewItems_GridView_SelectedIndex_Row"] = selectedRow;
        Response.Redirect("ViewIncident.aspx");
    }
    protected void cbFilter_CheckedChanged(object sender, EventArgs e)
    {
        if (cbFilter.Checked)
        {
            lblClosed.Visible = true;
            gvSummary.Visible = true;
        }
        else
        {
            lblClosed.Visible = false;
            gvSummary.Visible = false;
        }
    }
}