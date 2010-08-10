using System;
using Common.Logging;
using Quartz;

namespace Jobs
{
    public class HelloWorldJob : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            ILog logger = LogManager.GetLogger(typeof(HelloWorldJob));
            logger.Debug("hello world");
            Console.WriteLine("hello");
        }
    }
}
