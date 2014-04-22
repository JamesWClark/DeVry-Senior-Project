using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class User_EditInventoryItem : System.Web.UI.Page
{
    static string prevPage = String.Empty;

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
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
            }
            //getting session variables - present when the valid page check passes
            string[] headers = (string[])Session["ViewItems_GridView_Headers"];
            string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"];
            DisplayObjectTable(headers, gvString);

            string objectGuid = (string)Session["SelectedObjectGuid"];

            PopulateTable(gvString[0]);
            FillTable(gvString);
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
    protected void DisplayObjectTable(string[] headers, string[] session)
    {
        lblObject.Text = "<p><b><u>" + session[0] + "</u></b><br />";
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
    protected void PopulateTable(string objectLabel)
    {
        v1table.Dispose();
        //query for the column name, its data type, and is it nullable
        string queryColumns =
            "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'obj_" + objectLabel + "'";
        DataTable dt = GetDataTable(queryColumns);

        int numRows = dt.Rows.Count;
        string[] insertFields = new string[numRows];

        for (int k = 1; k < numRows; k++)
        {
            TableRow r = new TableRow();
            TableCell cName = new TableCell();
            TableCell cText = new TableCell();
            TableCell cNull = new TableCell();
            TableCell cVali = new TableCell();

            insertFields[k - 1] = (string)dt.Rows[k].ItemArray[0];
            cName.Text = insertFields[k - 1];
            TextBox t = new TextBox();
            t.ID = "box" + k.ToString();
            cText.Controls.Add(t);

            r.Cells.Add(cName); r.Cells.Add(cText); r.Cells.Add(cNull); r.Cells.Add(cVali);
            v1table.Rows.Add(r);
        }

        AddValidators(dt);
        DataTypeHandler.AddTypeValidators(v1table, dt);
    }
    protected void FillTable(string[] session)
    {
        for (int i = 1; i < session.Length - 1; i++)
        {
            TextBox t = (TextBox)v1table.Rows[i - 1].Cells[1].Controls[0];
            t.Text = session[i];
        }
    }
    //these are required field validators for every Not-nullable field in the database table
    protected void AddValidators(DataTable dt)
    {
        int numRows = dt.Rows.Count;
        for (int i = 1; i < numRows; i++)
        {
            if ((string)dt.Rows[i].ItemArray[2] == "NO")
            {
                Label l = new Label();
                l.Text = "*";
                l.ForeColor = Color.Red;
                v1table.Rows[i - 1].Cells[2].Controls.Add(l);
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                TextBox t = (TextBox)v1table.Rows[i - 1].Cells[1].Controls[0];
                rfv.ControlToValidate = t.ID;
                v1table.Rows[i - 1].Cells[2].Controls.Add(rfv);
            }
        }
    }
    protected DataTable GetDataTable(string queryColumns)
    {
        try
        {
            SqlConnection conn; SqlCommand cmd; SqlDataAdapter sda; DataTable dt;

            //create and open a connection to the database
            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AjjpSqlServerDB"].ConnectionString;
            conn.Open();

            //create a new command with the column name and data type query above
            cmd = new SqlCommand();
            cmd.CommandText = queryColumns;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            dt = new DataTable();
            sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
            return null;
        }
    }
    protected void SqlUpdate()
    {
        string[] headers = (string[])Session["ViewItems_GridView_Headers"];
        string[] gvString = (string[])Session["ViewItems_GridView_SelectedIndex_Row"];
        string objectGuid = (string)Session["SelectedObjectGuid"];

        int numRows = v1table.Rows.Count;

        string sqlUpdate = "UPDATE obj_" + gvString[0] + " SET ";
        for (int i = 0; i < numRows; i++)
        {
            string field = v1table.Rows[i].Cells[0].Text;
            TextBox t = (TextBox)v1table.Rows[i].Cells[1].Controls[0];
            string input = t.Text;
            input = input.Replace("'","''");

            sqlUpdate +=  field + " = '" + input + "'";
            if (i < numRows - 1)
            {
                sqlUpdate += ",";
            }
            gvString[i + 1] = t.Text;
        }
        sqlUpdate += " WHERE " + gvString[0] + "_guid = '" + objectGuid + "'";
        Session["ViewItems_GridView_SelectedIndex_Row"] = gvString;

        DataTypeHandler.ExecuteNonQuery(sqlUpdate);
        //lblErr.Text += "<br>" + sqlUpdate;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(prevPage);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SqlUpdate();
            Response.Redirect(prevPage);
        }
        catch (Exception)
        {
        }
    }
}