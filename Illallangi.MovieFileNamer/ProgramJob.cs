namespace Illallangi.MovieFileNamer
{
    using System;

    using Quartz;

    using log4net;
    using log4net.Config;

    using Ninject;

    public sealed class ProgramJob : IJob
    {
        private readonly string[] currentArguments;

        public ProgramJob()
            : this(null)
        {
        }

        public ProgramJob(string[] arguments)
        {
            this.currentArguments = arguments;
        }

        public void Execute(IJobExecutionContext cx)
        {
            var start = DateTime.Now;
            var logger = LogManager.GetLogger(typeof(Program));
            logger.InfoFormat("Started at {0}", start);
            try
            {
                var result = new StandardKernel(new ProgramModule<ProgramDriver>(this.Arguments)).Get<IDriver>().Execute();
                if (!result.IsCompleted)
                {
                    logger.ErrorFormat("Execute method did not complete.");
                }
            }
            catch (Exception e)
            {
                logger.FatalFormat("Unhandled Exception: {0}", e);
            }

            var finish = DateTime.Now;
            var span = finish.Subtract(start);
            logger.InfoFormat("Finished at {0}, took {1} seconds", start, span.TotalSeconds);
        }

        public string[] Arguments
        {
            get
            {
                return this.currentArguments;
            }
        }    
    }
}