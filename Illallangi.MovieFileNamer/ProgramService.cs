namespace Illallangi.MovieFileNamer
{
    using Quartz;
    using Quartz.Collection;
    using Quartz.Impl;
    using Quartz.Impl.Triggers;

    public sealed class ProgramService
    {
        private ISchedulerFactory currentSchedulerFactory;

        private IScheduler currentScheduler;

        private ITrigger currentTrigger;

        private IJobDetail currentJobDetail;

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
                                              .WithIntervalInMinutes(1)
                                              .WithMisfireHandlingInstructionIgnoreMisfires())
                                         .Build());
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