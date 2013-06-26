using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Logging;
using log4net;

namespace Illallangi.MovieFileNamer
{
    public sealed class Program : IDriver
    {
        #region Fields

        /// <summary>
        /// Holds the current value of the Logger property.
        /// </summary>
        private readonly ILogger currentLogger;

        /// <summary>
        /// Holds the current value of the Outputs property.
        /// </summary>
        private readonly IOutput[] currentOutputs;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the Program class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="outputs"></param>
        public Program(ILogger logger, IOutput[] outputs)
        {
            this.currentLogger = logger;
            this.currentOutputs = outputs;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        /// <param name="arguments">The command line arguments passed to the program.</param>
        public static void Main(string[] arguments)
        {
            var start = DateTime.Now;
            log4net.Config.XmlConfigurator.Configure();
            var logger = LogManager.GetLogger(typeof(Program));
            logger.InfoFormat("Started at {0}", start);
            try
            {
                new StandardKernel(new ProgramModule<Program>(arguments)).Get<IDriver>().Execute();     
            }
            catch (Exception e)
            {
                logger.FatalFormat("Unhandled Exception: {0}", e);
            }
            var finish = DateTime.Now;
            var span = finish.Subtract(start);
            logger.InfoFormat("Finished at {0}, took {1} seconds", start, span.TotalSeconds);
        }

        public void Execute()
        {
            this.Logger.Debug("Execute() Called");

            Parallel.ForEach(this.Outputs, output => output.Write());

            this.Logger.Debug("Execute() Complete");
        }

        #endregion

        #region Properties

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        private IEnumerable<IOutput> Outputs
        {
            get { return this.currentOutputs; }
        }

        #endregion
    }
}

