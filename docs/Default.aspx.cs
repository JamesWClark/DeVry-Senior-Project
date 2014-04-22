using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class docs_Default : System.Web.UI.Page
{
    //constant - absolute directory path
    protected static DirectoryInfo di = new DirectoryInfo("D:\\Hosting\\4338448\\html\\docs\\index");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            buildTable();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        FileInfo[] rgFiles = getFileInfo();
        bool exists = false;
        bool isValidFileExtension = false;

        //this is to prevent upload abuse - change the limit as needed
        if (rgFiles.Length > 20)
        {
            lblStatus.Text = "Directory is full. Contact administrator.";
        }
        //as long as there aren't too many files, upload more
        else
        {
            //this loop checks to see if the file already exists to prevent an overwrite
            for(int i = 0; i < rgFiles.Length; i++)
            {
                if (rgFiles[i].Name == FileUpload1.FileName)
                {
                    exists = true;
                    break;
                }
            }
            if(exists == true)
            {
                lblStatus.Text = "A file with the same name already exists.";
            }
            else if (FileUpload1.HasFile)
            {
                //check file extension
                string[] exts = getValidExtensions();
                for (int i = 0; i < exts.Length; i++)
                {
                    if (Path.GetExtension(FileUpload1.FileName) == "." + exts[i])
                    {
                        isValidFileExtension = true;
                    }
                }
                try
                {
                    if (!isValidFileExtension)
                    {
                        lblStatus.Text = "File format not approved. Contact administrator.";
                    }
                        //for this next step to work, I had to edit the permissions in GoDaddy
                        //use the GoDaddy Hosting Login file manager, check the box next to a directory, 
                        //then click the permissions button. choose to enable write permissions. 
                        //You will probably have to turn off the inheritance first.
                    else if (FileUpload1.PostedFile.ContentLength < 10240000)
                    {
                        string filename = Path.GetFileName(FileUpload1.FileName);
                        FileUpload1.SaveAs(Server.MapPath("index\\") + filename);
                        lblStatus.Text = "Upload succeeded.";
                    }
                    else
                        lblStatus.Text = "File must be less than 10 mb.";
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Upload failed. The following error occurred: " + ex.Message;
                }
            }
            else if(!FileUpload1.HasFile)
            {
                lblStatus.Text = "Browse to a file.";
            }
        }
        buildTable();
    }
    protected void buildTable()
    {
        FileInfo[] rgFiles = getFileInfo();
        /***********************************
        //generate header row and cells
        TableHeaderRow rh;
        TableHeaderCell ch;
        rh = new TableHeaderRow();
        ch = new TableHeaderCell();
        ch.Controls.Add(new LiteralControl("File"));
        rh.Cells.Add(ch);
        ch = new TableHeaderCell();
        ch.Controls.Add(new LiteralControl("Download"));
        rh.Cells.Add(ch);
        ch = new TableHeaderCell();
        ch.Controls.Add(new LiteralControl("Screenshot"));
        rh.Cells.Add(ch);
        ch = new TableHeaderCell();
        Table1.Rows.Add(rh);
        ********************************/
        foreach (FileInfo fi in rgFiles)
        {
            TableRow r;
            TableCell c;
            r = new TableRow();
            c = new TableCell();
            HyperLink h = new HyperLink();
            h.Text = fi.Name;
            h.NavigateUrl = "index/" + fi.Name;
            c.Controls.Add(h);
            r.Cells.Add(c);
            Table1.Rows.Add(r);
        }
    }
    protected FileInfo[] getFileInfo()
    {
        return di.GetFiles("*.*");
    }
    protected string[] getValidExtensions()
    {
        string[] extensions = {"vsd","doc","docx","ppt","pptx","mpp","txt","pdf","xls","xlsx","jpg","jpeg",
                                  "gif","png","bmp","rtf","csv"};

        return extensions;
    }
    /*protected void SqlConnect()
    {
        SqlConnection conn;
            SqlCommand cmd;
            SqlDataAdapter sda;
            DataTable dt;

            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=ajjpsqlserverdb.db.4338448.hostedresource.com;Initial Catalog=ajjpsqlserverdb;User Id=ajjpsqlserverdb;Password=Devry2010;";

            cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM documents";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            dt = new DataTable();
            sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dt);

            Console.WriteLine(dt.Rows);
            //GridView1.DataSource = MyTable.DefaultView;
            //GridView1.DataBind();

            sda.Dispose();
            cmd.Dispose();
            conn.Dispose();
    }*/
}