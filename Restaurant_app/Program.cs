using System.Data.SqlClient;

namespace Restaurant_app
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var datasource = @"HOME-PC\SQLEXPRESS";
            var database = "Restaurant";

             
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                                + database + ";Integrated Security=True;";

            
            SqlConnection conn = new SqlConnection(connString);

            FileManager fileManager = new FileManager(conn);
            fileManager.CreateHtmlFile();
        }
    }
}