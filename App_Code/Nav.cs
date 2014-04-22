using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class Nav
{
	public Nav()
	{
	}
    public static void SetUser(PlaceHolder ph, string username, string role)
    {
        Label lblWelcome = new Label();
        lblWelcome.Text = "Welcome, " + username + "<br />";

        Label lblRole = new Label();
        lblRole.Text = role + "<br />";

        ph.Controls.Add(lblWelcome);
        ph.Controls.Add(lblRole);
    }
    public static void SetLeftNav(PlaceHolder ph, string role)
    {
    //START BUILD NAV QUERY
        string navQuery = "SELECT * FROM ajjp_Navigation";
        switch (role)
        {
            case "superadmin":
            {
                break;
            }
            case "admin":
            {
                navQuery += " WHERE NavRole = 'admin' OR NavRole = 'user'";
                break;
            }
            case "user":
            {
                navQuery += " WHERE NavRole = 'user'";
                break;
            }
        }
        navQuery += " ORDER BY NavOrder ASC";
    //FINISH BUILD NAV QUERY

    //EXECUTE NAV QUERY
        DataTable dt = DataTypeHandler.GetDataTable(navQuery);

    //SET PAGE NAV
        //start with the home page link, applying a unique format
        HyperLink home = new HyperLink();
        home.Text = (string)dt.Rows[0].ItemArray[1];
        home.NavigateUrl = (string)dt.Rows[0].ItemArray[0];
        ph.Controls.Add(home);
        //new line
        Label blank = new Label();
        blank.Text = "<br />";
        ph.Controls.Add(blank);

        for (int i = 1; i < dt.Rows.Count; i++)
        {
            string category = (string)dt.Rows[i].ItemArray[4];
            switch (category)
            {
                case "link":
                {
                    HyperLink h = new HyperLink();
                    h.Text = (string)dt.Rows[i].ItemArray[1];
                    h.NavigateUrl = (string)dt.Rows[i].ItemArray[0];
                    ph.Controls.Add(h);

                    //new line
                    blank = new Label();
                    blank.Text = "<br />";
                    ph.Controls.Add(blank);
                    break;
                }
                case "label":
                {
                    Label lbl = new Label();
                    lbl.CssClass = "NavLabel";
                    lbl.Text = "<br />" + (string)dt.Rows[i].ItemArray[1] + "<br />";

                    ph.Controls.Add(lbl);
                    break;
                }
            }
        }
    }
}
