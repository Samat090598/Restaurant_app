using System.Threading.Tasks;
using Quartz;

namespace Restaurant_app
{
    public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateHtmlFile();
            string pdf = fileManager.ConvertHtmlToPdf();
            await EmailService.SendEmailAsync(pdf);
        }
    }
}