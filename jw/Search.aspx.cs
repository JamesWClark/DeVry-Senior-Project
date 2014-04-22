using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class jw_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ReadData();
    }
    protected void ReadData()
    {
        try
        {
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataAdapter sda;
            DataTable dt;

            conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AjjpSqlServerDB"].ConnectionString;
            conn.Open();

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT make, model, serial, typeid "
                + "FROM objects "
                + "WHERE CONTAINS (make, '\"" + txtSearchField.Text + "\"') "
                + "OR CONTAINS (model, '\"" + txtSearchField.Text + "\"') "
                + "OR CONTAINS (serial, '\"" + txtSearchField.Text + "\"') "
                + "OR CONTAINS (typeid, '\"" + txtSearchField.Text + "\"') ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            dt = new DataTable();
            sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dt);

            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();

            sda.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
            lblError.Text = "An error occurding during database connectivity.";
        }
    }
}