using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmGrid : System.Web.UI.Page
{
    int itemID;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void getItemID()
    {
        string strItemID;

        strItemID = txtID.Text.Trim();

        itemID = int.Parse(strItemID);
    }

    public int setItemID() { return itemID; }



    protected void gvItem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}