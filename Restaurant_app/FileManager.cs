using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Insight.Database;
using Word = Microsoft.Office.Interop.Word;

namespace Restaurant_app
{
    public class FileManager
    {
        private const string _html = "Index.html";
        private const string _pdf = "Index.PDF";

        public async Task CreateHtmlFile()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                new DataColumn("Size", typeof(string)),
                new DataColumn("FreeSize",typeof(string)) });

            var datasource = @"HOME-PC\SQLEXPRESS";
            var database = "Restaurant";

             
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                                + database + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();   
                    foreach (var table in connection.Query<Table>("GetTables"))
                    {
                        dt.Rows.Add(table.Id, table.Size, table.FreeSize);
                    }
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
                    using (StreamWriter sw = new StreamWriter(_html, false, Encoding.Default))
                    {
                        sw.WriteLine(sb);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            await ConvertHtmlToPdf();
        }

        private async Task ConvertHtmlToPdf()
        {
            object readOnly = true;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;
            object fileName = Directory.GetCurrentDirectory() + "\\" + _html;
            
            //Открываем приложение
            Word.Application ap = new Word.Application();

            //открывем файл на ms word
            Word.Document document = ap.Documents.Open(ref fileName, ref missing, 
                ref readOnly, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing, ref missing);
            
            // сохраняем файл в формате pdf
            document.SaveAs(Directory.GetCurrentDirectory() + "\\" + _pdf, 
                Word.WdExportFormat.wdExportFormatPDF);

            // закрываем ms word
             document.Close(false);
             document = null;
             ap.Quit(false);
             Marshal.ReleaseComObject(ap);
             ap = null;

             // Удалил html файл
             if (File.Exists(_html))
            {
                File.Delete(_html);
            }
            await EmailService.SendEmailAsync(_pdf);
        }
    }
}