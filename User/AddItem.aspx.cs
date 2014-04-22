using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class _AddItem : System.Web.UI.Page 
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
        if (IsPostBack)
        {
            PopulateTable();
            bSubmit.Visible = true;
        }
    }
    protected void PopulateTable()
    {
        v1table.Dispose();
        //query for the column name, its data type, and is it nullable
        string queryColumns =
            "SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'obj_" + ddlObjects.SelectedItem + "'";
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

            insertFields[k-1] = (string)dt.Rows[k].ItemArray[0];
            cName.Text = insertFields[k-1];
            TextBox t = new TextBox();
            t.ID = "box" + k.ToString();
            cText.Controls.Add(t);

            r.Cells.Add(cName); r.Cells.Add(cText); r.Cells.Add(cNull); r.Cells.Add(cVali);
            v1table.Rows.Add(r);
        }

        AddValidators(dt);
        DataTypeHandler.AddTypeValidators(v1table, dt);
    }
    protected void bSubmit_Click(object sender, EventArgs e)
    {
        lblErr.Text = String.Empty;

        try
        {
            SqlInsert();
        }
        catch (Exception ex)
        {
            lblErr.Text = "Failed to submit:<br />" + ex.Message;
        }
        for (int i = 0; i < v1table.Rows.Count; i++)
        {
            TextBox t = (TextBox)v1table.Rows[i].Cells[1].Controls[0];
            t.Text = String.Empty;
        }
    }
    protected void SqlInsert()
    {
        //get the names of the columns we are inserting into
        string queryColumns =
            "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'obj_" + ddlObjects.SelectedItem + "'";
        DataTable dt = GetDataTable(queryColumns);

        int numRows = v1table.Rows.Count;

        string sqlInsert = "INSERT INTO obj_" + ddlObjects.SelectedItem + "(" + ddlObjects.SelectedItem + "_guid,";
        for (int i = 0; i < numRows; i++)
        {
            sqlInsert += (string)dt.Rows[i + 1].ItemArray[0];
            if (i < numRows - 1)
            {
                sqlInsert += ",";
            }
        }
        string guid = Guid.NewGuid().ToString();
        sqlInsert += ") VALUES ('" + guid + "',";

        //get values for the insert from text boxes in v1table
        for (int i = 0; i < v1table.Rows.Count; i++)
        {
            TextBox t = (TextBox)v1table.Rows[i].Cells[1].Controls[0];

            if (t.Text.Trim() == String.Empty)
                sqlInsert += "NULL";
            else
                sqlInsert += "'" + t.Text + "'";

            //last item in the insert, so this stops the comma from generating syntax error
            if (i < v1table.Rows.Count - 1)
            {
                sqlInsert += ",";
            }
        }
        sqlInsert += ")";

        InsertData(sqlInsert);

        //lblErr.Text += "<br>" + sqlInsert;
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
    protected void InsertData(string sqlCommand)
    {
        try
        {
            SqlConnection conn;
            SqlCommand cmd;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AjjpSqlServerDB"].ConnectionString;
            conn.Open();

            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            //run the insert
            cmd.CommandText = sqlCommand;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            lblErr.Text = "Item successfully added.";
        }
        catch (Exception ex)
        {
            lblErr.Text = ex.Message;
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
                v1table.Rows[i-1].Cells[2].Controls.Add(l);
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                TextBox t = (TextBox)v1table.Rows[i-1].Cells[1].Controls[0];
                rfv.ControlToValidate = t.ID;
                v1table.Rows[i-1].Cells[2].Controls.Add(rfv);
            }
        }
    }
    protected void ddlObjects_PreRender(object sender, EventArgs e)
    {
        if(!IsPostBack)
            ddlObjects.Items.Insert(0, "--");
    }
    protected void ddlObjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErr.Text = String.Empty;
        if (ddlObjects.SelectedValue == "--")
            bSubmit.Visible = false;
    }
    protected void v1table_PreRender(object sender, EventArgs e)
    {
    }
}// end AddItem