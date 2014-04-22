using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class DataTypeHandler
{
	public DataTypeHandler()
	{

	}
    public static void ExecuteNonQuery(string query)
    {
        SqlConnection conn; SqlCommand cmd;

        //create and open a connection to the database
        conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["AjjpSqlServerDB"].ConnectionString;
        conn.Open();

        //create a new command with the column name and data type query above
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();

        cmd.Dispose(); conn.Dispose();
    }
    public static DataTable GetDataTable(string query)
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
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            dt = new DataTable();
            sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dt);

            sda.Dispose(); cmd.Dispose(); conn.Dispose();
                        
            return dt;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static void AddTypeValidators(Table t, DataTable dt)
    {
        int numRows = t.Rows.Count;
        for (int i = 0; i < numRows; i++)
        {
            string dataType = (string)dt.Rows[i+1].ItemArray[1];
            switch (dataType)
            {
                case "float":
                {
                    RegularExpressionValidator rev = new RegularExpressionValidator();
                    rev.ValidationExpression = "^[+-]?[\\d,]*(\\.\\d*)?$";
                    rev.ErrorMessage = "This field requires a valid number.";
                    TextBox tb = (TextBox)t.Rows[i].Cells[1].Controls[0];
                    rev.ControlToValidate = tb.ID;
                    t.Rows[i].Cells[3].Controls.Add(rev);
                    break;
                }
                case "int":
                {
                    RegularExpressionValidator rev = new RegularExpressionValidator();
                    rev.ValidationExpression = "^\\d+$";
                    rev.ErrorMessage = "This field is limited to integers.";
                    TextBox tb = (TextBox)t.Rows[i].Cells[1].Controls[0];
                    rev.ControlToValidate = tb.ID;
                    t.Rows[i].Cells[3].Controls.Add(rev);
                    break;
                }
                case "money":
                {
                    RegularExpressionValidator rev = new RegularExpressionValidator();
                    rev.ValidationExpression = "^\\$?[+-]?[\\d,]*(\\.\\d*)?$";
                    rev.ErrorMessage = "This field requires a valid currency value.";
                    TextBox tb = (TextBox)t.Rows[i].Cells[1].Controls[0];
                    rev.ControlToValidate = tb.ID;
                    t.Rows[i].Cells[3].Controls.Add(rev);
                    break;
                }
                case "datetime":
                {
                    RegularExpressionValidator rev = new RegularExpressionValidator();
                    rev.ValidationExpression = "^((0?[13578]|10|12)(-|\\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\\/)((19)([2-9])(\\d{1})|(20)([01])(\\d{1})|([8901])(\\d{1}))|(0?[2469]|11)(-|\\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\\/)((19)([2-9])(\\d{1})|(20)([01])(\\d{1})|([8901])(\\d{1})))$";
                    rev.ErrorMessage = "This field requires a date in the MM/DD/YYYY format.";
                    TextBox tb = (TextBox)t.Rows[i].Cells[1].Controls[0];
                    rev.ControlToValidate = tb.ID;
                    t.Rows[i].Cells[3].Controls.Add(rev);
                    break;
                }
                case "varchar":
                {
                    break;
                }
            }
        }
    }
}