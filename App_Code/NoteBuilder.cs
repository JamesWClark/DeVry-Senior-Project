using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class NoteBuilder
{
	public NoteBuilder()
	{
	}
    public static void InsertNote(string note, string userName, string incidentId)
    {
        note = note.Replace("'", "''");
        string noteGuid = Guid.NewGuid().ToString();
        
        string[] noteInsert = new string[2];
        
        noteInsert[0] = "INSERT INTO ajjp_Notes(NoteId,Note,UserName,DateEntered) " 
            + "VALUES('" + noteGuid + "','" + note + "','" + userName + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "')";

        noteInsert[1] = "INSERT INTO ajjp_NotesInIncidents(NoteId,IncidentId) "
            + "VALUES('" + noteGuid + "','" + incidentId + "')";

        DataTypeHandler.ExecuteNonQuery(noteInsert[0]);
        DataTypeHandler.ExecuteNonQuery(noteInsert[1]);
    }
}