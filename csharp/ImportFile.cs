using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ImportFile
/// </summary>
public class ImportFile
{
    
    #region Constructor
    public ImportFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #endregion

    #region ImportCSV
    /// <summary>
    /// Reads in a .csv file.
    /// </summary>
    /// <param name="fileName">File path.</param>
    /// <param name="encoding">Encoding in which the file is written.</param>
    /// <param name="separator">The separator used to separate the fields</param>
    /// <returns></returns>
    public static DataTable ImportCSV(string fileName, Stream str, char separator)
    {
        DataTable table = null;

        if (fileName != null && !fileName.Equals(string.Empty))
        {
            try
            {
                // If required, you can collect some useful info from the file
                FileInfo info = new FileInfo(fileName);
                string tableName = info.Name;

                // Prepare for the data to be processed into a DataTable
                // We don't know how many records there are in the .csv, so we
                // use a List<string> to store the records in it temporarily.
                // We also prepare a DataTable;
                List<string> rows = new List<string>();

                // Then we read in the raw data
                StreamReader reader = new StreamReader(str,System.Text.UTF8Encoding.Default);
                string record = reader.ReadLine();
                while (record != null)
                {
                    rows.Add(record);
                    record = reader.ReadLine();
                }

                // And we split it into chuncks.
                // Note that we keep track of the number of columns
                // in case the file has been tampered with, or has been made
                // in a weird kind of way (believe me: this does happen)

                // Here we will store the converted rows
                List<string[]> rowObjects = new List<string[]>();

                int maxColsCount = 0;
                foreach (string s in rows)
                {
                    string[] convertedRow = s.Split(new char[] { separator });
                    if (convertedRow.Length > maxColsCount)
                        maxColsCount = convertedRow.Length;
                    rowObjects.Add(convertedRow);
                }

                // Then we build the table
                table = new DataTable(tableName);
                for (int i = 0; i < maxColsCount; i++)
                {
                    // Change this if you want other datatypes
                    // make sure you also convert the string[] to
                    // the corect datataype
                    table.Columns.Add(new DataColumn(rowObjects[0][i]));
                }

                rowObjects.RemoveAt(0);

                foreach (string[] rowArray in rowObjects)
                {
                    table.Rows.Add(rowArray);
                }
                table.AcceptChanges();
            }
            catch
            {
                throw new Exception("Error in ReadFromCsv: IO error.");
            }
        }
        else
        {
            throw new FileNotFoundException("Error in ReadFromCsv: the file path could not be found.");
        }
        return table;
    }
    #endregion

    #region ExportToDatabase
    public static void ExportToDatabase(DataTable _dtImported, string Tablename,string histoID)
    {
        Database db;
        DbCommand dbCommand;
        db = DatabaseFactory.CreateDatabase("MySql");

        //------Insert query-----------------------------///
        string query = "";
        string strInsert = "insert into "+Tablename+" (";

        for (int i = 0; i <= _dtImported.Columns.Count - 1; i++)
        {
            strInsert += _dtImported.Columns[i].ColumnName + " , ";
        }
        strInsert = strInsert.Remove(strInsert.Length - 2);
        strInsert += ",Import_id) values ";

        //---- Generates insert values----------------///
        string _values;
        foreach (DataRow item in _dtImported.Rows)
        {
            _values = " ( ";
            foreach (string str in item.ItemArray)
            {
                if (str != string.Empty)
                    _values += "'" + str.Replace("'", "''") + "',";
                else
                    _values += "' ',";
            }
            _values = _values.Remove(_values.Length - 1);
            _values += "," + histoID + ")";
            query += _values + " ,";
        }

        query = query.Remove(query.Length - 2, 2);
        query += " ;";
        query = strInsert + query;

        dbCommand = db.GetStoredProcCommand("spExecuteQuery");
        db.AddInParameter(dbCommand, "p_Query", DbType.String, query);
        db.ExecuteNonQuery(dbCommand);
        //---- Executes the query-------------------------------
    }
    #endregion

}