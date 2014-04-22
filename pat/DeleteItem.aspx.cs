using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class DeleteItem : System.Web.UI.Page
{
    int itemID, count;
    bool validityCheck = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Click += new EventHandler(this.btnDelete_Click);
        btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
        btnDelete.Visible = false;
        gvDelete.Visible = false;
        txtDelete.Focus();
    }

    void btnDelete_Click(object sender, EventArgs e)
    {
        getItemID();

        if(validityCheck == false)
        goDatabase();
        txtDelete.Text = "";
    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        getItemID();

        if (validityCheck == false)
        {
            getCount();

            if (count > 0)
            {
                lblMessage.Text = "";
                gvDelete.Visible = true;
                btnDelete.Visible = true;
            }

            else
            {
                lblMessage.Text = "";
                gvDelete.Visible = true;
            }
        }

        else lblMessage.Text = "Invalid Tag #, Tag Number Must Be Numeric";
        
    }


    public void getItemID()
    {
        string strItemID = txtDelete.Text.Trim();

        try
        {
            itemID = int.Parse(strItemID);
        }

        catch(Exception)
        {
            validityCheck = true;
            lblMessage.Text = "Invalid Tag #";
        }
    }

    public int setItemID() { return itemID; }

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

        catch (SqlException)
        {
            
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

            strSQL = "UPDATE ITEM SET ITEM_STATUS_FK = 'DELETED' WHERE ITEM_ID_PK = " + itemID;



            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strSQL;
            command.ExecuteNonQuery();

            myTransaction.Commit();

            conn.Close();

            string deleteMessage = "Item " + itemID + " Deleted Successfully";
            lblMessage.Text = deleteMessage;


        }

        catch (SqlException)
        {
            myTransaction.Rollback();
            lblMessage.Text = "Database Error, Item not Deleted";
        }

    } // end goDatabase

} // end DeleteItem