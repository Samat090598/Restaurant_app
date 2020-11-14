using System.Threading.Tasks;
using Quartz;

namespace Restaurant_app
{
    public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            FileManager fileManager = new FileManager();
            await fileManager.CreateHtmlFile();
        }
    }
}