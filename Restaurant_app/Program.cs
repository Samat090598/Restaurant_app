using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Restaurant_app
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var datasource = @"HOME-PC\SQLEXPRESS";
            var database = "Restaurant";

             
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                                + database + ";Integrated Security=True;";

            SqlConnection conn = new SqlConnection(connString);

            //Инициализируем FileManager
            FileManager fileManager = new FileManager(conn);
            fileManager.CreateHtmlFile();
            
            await EmailService.SendEmailAsync();
        }
    }
}