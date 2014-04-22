/*
 * ViewIncent.aspx calls for the following Session variables:
 * 
 * string[] headers = (string[])Session["ViewItems_GridView_Headers"];
   string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"]; 
   string guid = (string)Session["OneIncidentGuid"]
   found = (bool)Session["HasIncidentGuid"]; 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class User_Main : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            Session["UserName"] = Page.User.Identity.Name;

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
        if (gvUserIncidents.Rows.Count == 0)
        {
            lblErr.Visible = false;
        }
        else
            lblErr.Visible = true;
    }
    protected void gvUserIncidents_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetupSession();
            Response.Redirect("ViewIncident.aspx");
        }
        catch (Exception)
        {
        }
    }
    protected void SetupSession()
    {
        int rowIndex = gvUserIncidents.SelectedIndex;//the clicked row index
        string objectLabel = gvUserIncidents.DataKeys[rowIndex].Values["ObjectLabel"].ToString();
        string selectedObjectGuid = gvUserIncidents.DataKeys[rowIndex].Values["ObjectGuid"].ToString();
        string incidentId = gvUserIncidents.Rows[rowIndex].Cells[1].Text;
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
    }
}