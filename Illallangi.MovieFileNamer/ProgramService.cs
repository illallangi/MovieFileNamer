namespace Illallangi.MovieFileNamer
{
    using Ninject;

    using Quartz;
    using Quartz.Impl;

    public sealed class ProgramService
    {
        private ISchedulerFactory currentSchedulerFactory;

        private IScheduler currentScheduler;

        private ITrigger currentTrigger;

        private IJobDetail currentJobDetail;

        private IConfig currentConfig;

        public void Start()
        {
            this.Scheduler.Start();
            this.Scheduler.ScheduleJob(this.JobDetail, this.Trigger);
        }

        public void Pause()
        {
            this.Scheduler.PauseAll();
        }

        public void Continue()
        {
            this.Scheduler.ResumeAll();
        }

        public void Stop()
        {
            this.Scheduler.Shutdown(true);
        }

        public IJobDetail JobDetail
        {
            get
            {
                return this.currentJobDetail
                       ?? (this.currentJobDetail =
                           JobBuilder.Create<ProgramJob>().WithIdentity("job1", "group1").Build());
            }
        }

        public ITrigger Trigger
        {
            get
            {
                return this.currentTrigger
                       ?? (this.currentTrigger =
                           TriggerBuilder.Create()
                                         .WithIdentity("trigger1", "group1")
                                         .StartNow()
                                         .WithSimpleSchedule(
                                             x =>
                                             x.RepeatForever()
                                              .WithIntervalInMinutes(this.Config.Interval)
                                              .WithMisfireHandlingInstructionIgnoreMisfires())
                                         .Build());
            }
        }

        public IConfig Config
        {
            get
            {
                return this.currentConfig
                       ?? (this.currentConfig =
                           new StandardKernel(new ProgramModule<ProgramDriver>()).Get<IConfig>());
            }
        }
        public ISchedulerFactory SchedulerFactory
        {
            get
            {
                return this.currentSchedulerFactory ?? (this.currentSchedulerFactory = new StdSchedulerFactory());
            }
        }

        public IScheduler Scheduler
        {
            get
            {
                return this.currentScheduler ?? (this.currentScheduler = this.SchedulerFactory.GetScheduler());
            }
        }
    }
}