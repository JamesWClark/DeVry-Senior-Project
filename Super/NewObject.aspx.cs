using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Super_NewObject : System.Web.UI.Page
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
        v1txtName.Focus();

        //count is used to populate the number of rows in the view2 table
        //it is based on the number entered in the v1txtFields TextBox
        int count;
        try
        {
            if (v1txtFields.Text.Trim().Length > 0)//if a value is present at all
            {
                count = int.Parse(v1txtFields.Text.Trim());//get the number from v1txtFields TextBox

                //set the table columns and headers
                TableHeaderRow thr = new TableHeaderRow();
                TableHeaderCell thFieldName = new TableHeaderCell();
                TableHeaderCell thDataType = new TableHeaderCell();
                TableHeaderCell thRequired = new TableHeaderCell();
                thFieldName.Text = "Field Name";
                thDataType.Text = "Data Type";
                thRequired.Text = "Required";
                thr.Cells.Add(thFieldName);
                thr.Cells.Add(thDataType);
                thr.Cells.Add(thRequired);
                v2table.Rows.Add(thr);

                //populate the table with new text boxes, drop lists, and validators
                for (int i = 0; i < count; i++)
                {
                    TableRow tr = new TableRow();
                    TableCell c1 = new TableCell();//text box
                    TableCell c2 = new TableCell();//droplist
                    TableCell c3 = new TableCell();//checkbox
                    DropDownList ddl = SetDataTypeList();
                    TextBox t = new TextBox();
                    t.TabIndex = (short)(i + 1);
                    t.ID = "box" + i.ToString(); // can't validate without an ID

                    c1.Controls.Add(t);
                    c2.Controls.Add(ddl);
                    c3.Controls.Add(new CheckBox());

                    tr.Cells.Add(c1);
                    tr.Cells.Add(c2);
                    tr.Cells.Add(c3);
                    tr.Cells.Add(new TableCell()); //add a blank cell for the validator later
                    v2table.Rows.Add(tr);
                }
            }
            else //nothing is in the view 1 textbox at page load
            {
                count = 0;
            }
        }
        catch (Exception)
        {
        }
    }
    //fill the drop lists with data
    protected DropDownList SetDataTypeList()
    {
        DropDownList ddl = new DropDownList();

        string[] types = { "--", "number", "currency", "text", "date" };
        Array.Sort(types);
        ddl.DataSource = types;
        ddl.DataBind();

        return ddl;
    }
    //this method validates v1txtFields data
    //validate integer between 1 and 255
    protected void v1next_Click(object sender, EventArgs e)
    {
        try
        {
            int entry = int.Parse(v1txtFields.Text.Trim());
            if (entry > 0 && entry < 256)
            {
                //v1err.Text = "parse passed";
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                v1valFields.IsValid = false;
                //v1err.Text = "parse fail";
            }
        }
        catch (Exception)
        {
            v1valFields.IsValid = false;
        }
    }
    protected void View3_Activate(object sender, EventArgs e)
    {
        int count = v2table.Rows.Count - 1; //this is -1 because the first row is header cells
        string[] fields = new string[count];
        string[] types = new string[count];
        bool[] checks = new bool[count];

        //get the values entered in v2table controls, ignoring headers
        for (int i = 1; i <= count; i++) //start at 1 to ignore headers, and include the last row with <=
        {
            TextBox t = (TextBox)v2table.Rows[i].Cells[0].Controls[0];
            DropDownList d = (DropDownList)v2table.Rows[i].Cells[1].Controls[0];
            CheckBox c = (CheckBox)v2table.Rows[i].Cells[2].Controls[0];

            fields[i - 1] = t.Text; //-1 for both these because arrays uses zero index
            types[i - 1] = d.SelectedValue;
            if (c.Checked)
                checks[i - 1] = true;
            else
                checks[i - 1] = false;
        }
        //v3table header
        TableHeaderRow rh = new TableHeaderRow();
        TableHeaderCell hc = new TableHeaderCell();
        hc.ColumnSpan = 3;
        hc.Text = v1txtName.Text;
        rh.Cells.Add(hc);
        v3table.Rows.Add(rh);

        //put values in a v3 summary table
        for (int i = 1; i <= count; i++)//start at 1 to ignore headers, and include the last row with <=
        {
            TableRow r = new TableRow();
            TableCell c1 = new TableCell();
            TableCell c2 = new TableCell();
            TableCell c3 = new TableCell();
            c1.Text = "<b>" + fields[i-1].ToString() + "</b>: "; //-1 because of previous zero index
            c2.Text = types[i-1].ToString(); //-1 same reason

            if (checks[i - 1] == true)
            {
                c3.Text = " * Required";
            }

            r.Cells.Add(c1); r.Cells.Add(c2); r.Cells.Add(c3);
            v3table.Rows.Add(r);
        }
        Session["fields"] = fields;
        Session["types"] = types;
        Session["checks"] = checks;
    }
    protected void v3finish_Click(object sender, EventArgs e)
    {
        string[] fields = (string[])Session["fields"];
        string[] types = (string[])Session["types"];
        bool[] checks = (bool[])Session["checks"];
        Session["fields"] = String.Empty; //dump the session variables
        Session["types"] = String.Empty;
        Session["checks"] = String.Empty;

        //the value entered in view 1 textbox for object name
        //used to create obj_xx table and 1 record in objects table
        string objName = v1txtName.Text.Trim();
        objName = objName.Replace(" ", "_");

        //begin the SQL command - create table
        string create = "CREATE TABLE obj_" + objName + " (" + objName + "_guid UNIQUEIDENTIFIER PRIMARY KEY,";

        //for some reason, the string arrays when passed in session acquire an extra null value
        //i fixed this in the following iterations by subtracting 1 from the length in the following two for loops
        for (int i = 0; i < fields.Length; i++ )
        {
            //clean the field entry to remove spaces and replace them with underscores - this prevents syntax errors in create statement
            string validString = fields[i];
            validString = validString.Replace(" ", "_");

            create += validString + " ";

            switch (types[i])
            {
                case "number":
                {
                    create += "FLOAT";
                    break;
                }
                case "text":
                {
                    create += "VARCHAR(8000)";
                    break;
                }
                case "currency":
                {
                    create += "MONEY";
                    break;
                }
                case "date":
                {
                    create += "DATETIME";
                    break;
                }
            }
            if (checks[i] == true)
            {
                create += " NOT NULL";
            }
            else
            {
                create += " NULL";
            }
            //this expression indicates we are on the last line in the construction of our SQL command, where a comma would throw a syntax error, so we omit the comma
            if (i < fields.Length - 1)
            {
                create += ",";
            }
        }
        create +=  ")";

        //TEST STUB: v31.Text = create;

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
            //create the table for this new object
            cmd.CommandText = create;
            cmd.ExecuteNonQuery();
            //add to the objects table
            cmd.CommandText = "INSERT INTO ajjp_Objects(obj_name) VALUES('" + objName + "')";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            MultiView1.ActiveViewIndex = 3;
            //stub: v4err.Text = create;
        }
        catch (Exception ex)
        {
            v3err.Text = ex.Message;
        }
    }
    protected void v2next_Click(object sender, EventArgs e)
    {
        bool valid = true;

        //validate v2table controls
        for (int i = 1; i < v2table.Rows.Count; i++)
        {
            DropDownList ddl = (DropDownList)v2table.Rows[i].Cells[1].Controls[0];
            TextBox t = (TextBox)v2table.Rows[i].Cells[0].Controls[0];

            RequiredFieldValidator rfv = new RequiredFieldValidator();
            rfv.ErrorMessage = "Fill the text box and choose from the drop list";
            rfv.ControlToValidate = v2table.Rows[i].Cells[0].Controls[0].ID; //the ID of the textbox in the first cell
            v2table.Rows[i].Cells[3].Controls.Add(rfv);

            if (t.Text.Trim().Length < 1 || ddl.SelectedIndex == 0)
            {
                rfv.IsValid = false;
                valid = false;
            }
        }
        if (valid)
        {
            MultiView1.ActiveViewIndex = 2;
        }
    }
    protected void View2_Activate(object sender, EventArgs e)
    {
        v2table.Rows[1].Cells[0].Controls[0].Focus();
    }
}