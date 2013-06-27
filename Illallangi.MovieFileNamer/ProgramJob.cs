namespace Illallangi.MovieFileNamer
{
    using System;

    using Quartz;

    using log4net;
    using log4net.Config;

    using Ninject;

    [DisallowConcurrentExecution]
    public sealed class ProgramJob : IJob
    {
        public void Execute(IJobExecutionContext cx)
        {
            var start = DateTime.Now;
            var logger = LogManager.GetLogger(typeof(Program));
            logger.InfoFormat("Started at {0}", start);
            try
            {
                var result = new StandardKernel(new ProgramModule<ProgramDriver>()).Get<IDriver>().Execute();
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
    }
}