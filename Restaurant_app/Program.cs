using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Restaurant_app
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<MyJob>()
                .WithIdentity("job1", "group1")
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(2).RepeatForever())
                .Build();
 
            await scheduler.ScheduleJob(job, trigger);

            Console.WriteLine("Нажмите любую кнопку чтобы прекратить работу");
            Console.ReadKey();
        }
    }
}