﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class jw_ViewItems : System.Web.UI.Page
{
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
    }
    protected void gvObjects_PreRender(object sender, EventArgs e)
    {
        string query = "SELECT * FROM obj_" + ddlObjects.SelectedItem 
            + " WHERE " + ddlObjects.SelectedItem + "_guid NOT IN (SELECT ObjectGuid FROM ajjp_DeletedObjects)";
        DataTable dt = DataTypeHandler.GetDataTable(query);

        gvObjects.DataSource = dt;
        gvObjects.DataBind();

        if (gvObjects.DataSource == null)
        {
            //stub.Text = "<br /><br />" + query;
            lblErr.Text = "There was no data source available.";
        }
    }
    protected void gvObjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErr.Text = String.Empty;

        //the clicked row index
        int rowIndex = gvObjects.SelectedIndex;
        lblErr.Text += "Selected Index: " + rowIndex.ToString() + "<br>";
        //the number of cells in the gridview
        int numCells = gvObjects.Rows[0].Cells.Count;
        lblErr.Text += "Number of cells: " + numCells.ToString() + "<br>";

        string[] selectedRow = new string[(numCells)];
        string[] headers = new string[numCells - 1];

        selectedRow[0] = ddlObjects.SelectedItem.ToString();//add the table name to the selectedRow string array, so it can pass into the incidents page for further query.
        for (int i = 1; i < numCells - 1; i++)
        {
            headers[i - 1] = gvObjects.HeaderRow.Cells[i + 1].Text;
            selectedRow[i] = gvObjects.Rows[rowIndex].Cells[i + 1].Text;
        }

        Session["SelectedObjectGuid"] = GetObjectGuid(rowIndex);
        Session["ViewItems_GridView_Headers"] = headers;
        Session["hasItem"] = true;
        Session["ViewItems_GridView_SelectedIndex_Row"] = selectedRow;
        Response.Redirect("ViewItemIncidents.aspx");
    }
    protected void ddlObjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErr.Text = String.Empty;
    }
    protected string GetObjectGuid(int rowIndex)
    {
        return gvObjects.Rows[rowIndex].Cells[1].Text;
    }
    protected void ddlObjects_PreRender(object sender, EventArgs e)
    {
        /* THIS METHOD REPLACES _ WITH SPACES, BUT IT WILL THROW ERRORS LATER THAT STILL NEED TO BE FIXED
        for (int i = 0; i < ddlObjects.Items.Count; i++)
        {
            string temp = ddlObjects.Items[i].Text;
            temp = temp.Replace("_", " ");
            ddlObjects.Items[i].Text = temp;
        }
         * */
    }
    protected void cbDeleteMode_CheckedChanged(object sender, EventArgs e)
    {
        if (cbDeleteMode.Checked == true)
            gvObjects.AutoGenerateDeleteButton = true;
        else if (cbDeleteMode.Checked == false)
            gvObjects.AutoGenerateDeleteButton = false;
    }
    protected void gvObjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblErr.Text = String.Empty;
        int rowIndex = e.RowIndex;
        //the selected row's object GUid
        string objectGuid = gvObjects.Rows[rowIndex].Cells[1].Text;

        //check for an open incident
        string queryOpenIncidents = "SELECT " + ddlObjects.SelectedItem + "_guid "
            + "FROM obj_" + ddlObjects.SelectedItem + " "
            + "WHERE " + ddlObjects.SelectedItem + "_guid = '" + objectGuid + "' "
            + "AND " + ddlObjects.SelectedItem + "_guid IN(SELECT ObjectGuid FROM ajjp_Incidents WHERE Status = 'Open' OR Status = 'Assigned')";
        //stub.Text = "<br />" + queryOpenIncidents;
        DataTable dtOpenIncident = DataTypeHandler.GetDataTable(queryOpenIncidents);

        string incidentGuid = String.Empty;
        try
        {
            Guid g = (Guid)dtOpenIncident.Rows[0].ItemArray[0];
            incidentGuid = g.ToString();
            //stub.Text += "<br /><br />Incident GUID: " + incidentGuid;
        }
        catch (Exception)
        {
        }
        //stub.Text += "<br /><br />Object GUID: " + objectGuid;
        if (objectGuid == incidentGuid)
        {
            lblErr.Text = "The item you are trying to delete has open incidents. Resolve and close the incidents first.";
        }
        else
        {
            string sqlInsert = "INSERT INTO ajjp_DeletedObjects(ObjectGuid,TableName,Status,UserName,DateEntered) " 
                + "VALUES('" + objectGuid + "','" + ddlObjects.SelectedItem + "','" + "Deleted" + "','" + Page.User.Identity.Name + "','"
                + DateTime.Now.ToString("MM-dd-yyyy") + "')";
            try
            {
                DataTypeHandler.ExecuteNonQuery(sqlInsert);
                gvObjects.Rows[rowIndex].Visible = false;
            }
            catch (Exception ex)
            {
                lblErr.Text = "Failed to delete the selected item.<br />" + ex.Message;
            }
        }
        //stub.Text = "<br /><br />Row Index: " + rowIndex.ToString();
        //stub.Text += "<br />GUID: + " + objectGuid;
    }
    protected void gvObjects_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}