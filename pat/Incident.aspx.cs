using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class Incident : System.Web.UI.Page
{
    int itemID, count;
    string user, solution, status, date;
    bool validityCheck = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnItem.Click += new EventHandler(this.btnItem_Click);
        btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
    }

    void btnItem_Click(object sender, EventArgs e)
    {
        getItemID();

        lblMessage.Text = "";
        gvIncident.Visible = false;
        lblUser.Visible = false;
        ddUser.Visible = false;
        lblSolution.Visible = false;
        txtSolution.Visible = false;
        lblStatus.Visible = false;
        ddStatus.Visible = false;
        btnSubmit.Visible = false;

        if (validityCheck == false)
        {
            getCount();

            if (count > 0)
            {
                lblMessage.Text = "";
                gvIncident.Visible = true;
                lblUser.Visible = true;
                ddUser.Visible = true;
                lblSolution.Visible = true;
                txtSolution.Visible = true;
                lblStatus.Visible = true;
                ddStatus.Visible = true;
                btnSubmit.Visible = true;
            }

            else
            {
                lblMessage.Text = "Deleted or Non-Existent Tag #, Cannot Create Incident";
                gvIncident.Visible = true;
            }
        }

        else lblMessage.Text = "Invalid Tag # Format, Tag # Must Be Numeric";
    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        getItemID();
        getUser();
        getSolution();
        getStatus();
        getToday();
        count = 0;

        if (validityCheck == false)
        {
            goDatabase();
        }

        else
            lblMessage.Text = "Invalid or Missing Input";
    }

    public void getItemID()
    {
        string strItemID;
        strItemID = txtID.Text.Trim();

        try
        {
            itemID = int.Parse(strItemID);
        }

        catch (Exception)
        {
            validityCheck = true;
            lblMessage.Text = "Invalid Tag # Format, Tag # Must Be Numeric";
        }
    }

    public int setItemID() { return itemID; }

    public void getUser()
    {
        user = ddUser.Text;
    }

    public string setUser() { return user; }

    public void getSolution()
    {
        solution = txtSolution.Text.Trim();
    }

    public string setSolution() { return solution; }

    public void getStatus()
    {
        status = ddStatus.Text;
    }

    public string setStatus() { return status; }

    public void getToday()
    {
        DateTime today = DateTime.Now;
        date = String.Format("{0:MM/dd/yyyy HH:mm}", today);
    }

    public string setToday() { return date; }

    public void getCount()
    {
        string strSQL;

        try
        {
            SqlConnection conn = new SqlConnection(@"Data Source=ajjpsqlserverdb.db.4338448.hostedresource.com; database=ajjpsqlserverdb; 
                                                        User ID=ajjpsqlserverdb; Password= Devry2010;");

            conn.Open();
            SqlCommand command = conn.CreateCommand();

            strSQL = "SELECT COUNT(*) FROM ITEM WHERE ITEM_ID_PK= " + itemID +
                                        " AND ITEM_STATUS_FK = 'ACTIVE'";

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strSQL;
            command.ExecuteNonQuery();

            count = (int)command.ExecuteScalar();

            conn.Close();
        }

        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }


    }

    public int setCount() { return count; }

    public void goDatabase()
    {
        SqlTransaction myTransaction = null;

        try
        {
            SqlConnection conn = new SqlConnection(@"Data Source=ajjpsqlserverdb.db.4338448.hostedresource.com; database=ajjpsqlserverdb; 
                                                        User ID=ajjpsqlserverdb; Password= Devry2010;");

            conn.Open();
            SqlCommand command = conn.CreateCommand();

            string strSQL;

            myTransaction = conn.BeginTransaction();
            command.Transaction = myTransaction;

            strSQL = "INSERT INTO INCIDENT(INCIDENT_ITEM_FK, INCIDENT_DESCRIPTION, INCIDENT_STATUS_FK, INCIDENT_ENTERED_BY, INCIDENT_DATE_OPEN) VALUES" +
            "(" + itemID + ", '" + solution + "', '" + status + "', '" + user + "', '" + date + "')";



            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strSQL;
            command.ExecuteNonQuery();

            myTransaction.Commit();

            conn.Close();

            lblMessage.Text = "Incident Added Successfully";

        }

        catch (Exception ex)
        {
            myTransaction.Rollback();
            lblMessage.Text = ex.Message;
        }

        //return recordSaved;

    }// end goDatabase
}