using System;
using System.ServiceProcess;
using Common.Logging;
using Quartz;
using Quartz.Impl;

namespace paracode.Janitor
{
    public partial class JobService : ServiceBase
    {
        private ILog _log;
        private IScheduler _sched;

        static void Main(string[] args)
        {
            JobService service = new JobService();


            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Console.WriteLine(DateTime.Now+" Running Janitor... <Press any key to stop>\n\nLive Output\n-----------");
                Console.Read();
                service.OnStop();
            }
            else
            {
                System.ServiceProcess.ServiceBase[] ServicesToRun;
                ServicesToRun = new System.ServiceProcess.ServiceBase[] {service};
                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
        }

        public JobService()
        {
            InitializeComponent();
            InitializeScheduling();
        }

        private void InitializeScheduling()
        {
            _log = LogManager.GetLogger(typeof(JobService));
            ISchedulerFactory sf = new StdSchedulerFactory();
            _sched = sf.GetScheduler();
            _log.Info("** Initialized **");
        }


        protected override void OnStart(string[] args)
        {
            startScheduler();  //production
        }

        protected override void OnStop()
        {
            shutDownScheduler();
        }


        private void startScheduler()
        {
            _sched.Start();
            _log.Info("** Started Scheduler **");
        }

        private void shutDownScheduler()
        {
            _log.Info("** Shutting Down **");
            _sched.Shutdown(true);
            _log.Info("** Shutdown Complete **");
        }


    }
}
