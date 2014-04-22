using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class _AddItem : System.Web.UI.Page 
{
    int itemID;
    string itemType, itemDescription, itemSerialNum, itemModelNum, itemLocation;
    bool validityCheck = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
        txtID.Focus();

    }

    void btnSubmit_Click(object sender, EventArgs e)
    {
        getItemID();
        getItemType();
        getItemDescription();
        getItemSerialNum();
        getItemModelNum();
        getItemLocation();

        if (validityCheck == false)
        {
            goDatabase();
        }

        else
            lblMessage.Text = "Invalid or Missing Input: Tag # must be numeric, Check Required Fields";
    }

    public void getItemID()
    {
        string strItemID = txtID.Text.Trim();

        try
        {
            itemID = int.Parse(strItemID);
        }

        catch (Exception)
        {
            validityCheck = true;
        }

        if (strItemID.Length == 0)
            validityCheck = true;
    }
    public int setItemID() { return itemID; }

    public void getItemType()
    {
        itemType = txtItemType.Text.Trim();
        
            if (itemType.Length == 0)
                validityCheck = true;
    }

    public string setItemType() { return itemType; }

    public void getItemDescription()
    {
        itemDescription = txtItemDescription.Text.Trim();

        if (itemDescription.Length == 0)
            itemDescription = "";
    }

    public string setItemDescription() { return itemDescription; }

    public void getItemSerialNum()
    {
        itemSerialNum = txtSerialNum.Text.Trim();
    }

    public string setItemSerialNum() { return itemSerialNum; }

    public void getItemModelNum()
    {
        itemModelNum = txtModelNum.Text.Trim();
    }

    public string setItemModelNum() { return itemModelNum; }

    public void getItemLocation()
    {
        itemLocation = txtLocation.Text.ToUpper().Trim();

        if (itemLocation.Length == 0)
            validityCheck = true;
    }


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

            strSQL = "INSERT INTO ITEM(ITEM_ID_PK, ITEM_TYPE, ITEM_DESCRIPTION, ITEM_SERIAL_NUM, ITEM_MODEL_NUM, ITEM_LOCATION_ID_FK) VALUES" + 
            "(" + itemID + ", '" + itemType + "', '" + itemDescription + "', '" + itemSerialNum + "', '" + itemModelNum + "', '" + itemLocation + "')";
                        
                        

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = strSQL;
            command.ExecuteNonQuery();

            myTransaction.Commit();

            conn.Close();

            lblMessage.Text = "Item Added Successfully";


        }

        catch (Exception ex) 
        {
            
            myTransaction.Rollback();
            lblMessage.Text = ex.Message;//"Database Write Failed, Check for Duplicate Tag #";
        }

    }// end goDatabase

    



}// end AddItem