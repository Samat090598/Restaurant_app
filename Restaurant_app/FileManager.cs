using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Insight.Database;

namespace Restaurant_app
{
    public class FileManager
    {
        private readonly SqlConnection _connection;
        private const string _path = "index.html";

        public FileManager(SqlConnection connection)
        {
            _connection = connection;
        }

        public void CreateHtmlFile()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                new DataColumn("Size", typeof(string)),
                new DataColumn("FreeSize",typeof(string)) });
            try
            { 
                //open connection
                _connection.Open();
                foreach (var table in _connection.Query<Table>("GetTables"))
                {
                    dt.Rows.Add(table.Id, table.Size, table.FreeSize);
                }
                _connection.Close();

                StringBuilder sb = new StringBuilder();
                
                sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;" +
                          "text-align:center; margin: 50px auto 0 auto'>");
                
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
                }
                sb.Append("</tr>");
                
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                }
                
                sb.Append("</table>");
                System.IO.File.WriteAllText(_path, sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}