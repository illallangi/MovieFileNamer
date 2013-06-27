namespace Illallangi.MovieFileNamer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Ninject.Extensions.Logging;

    public sealed class ProgramDriver : IDriver
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
        public ProgramDriver(ILogger logger, IOutput[] outputs)
        {
            this.currentLogger = logger;
            this.currentOutputs = outputs;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        public ParallelLoopResult Execute()
        {
            this.Logger.Debug("Execute() Called");

            var result = Parallel.ForEach(this.Outputs, output => output.Write());

            this.Logger.Debug("Execute() Complete");

            return result;
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