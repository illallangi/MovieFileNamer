using System.Collections.Generic;
using System.Linq;

using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    using System.IO;

    public abstract class BaseOutput<T, Tobj> : IOutput where T : IEnumerable<Tobj>
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IConfig currentConfig;
        private readonly T currentResults;

        #endregion

        #region Constructor

        protected BaseOutput(ILogger logger, IConfig config, T results)
        {
            this.currentLogger = logger;
            this.currentConfig = config;
            this.currentResults = results;
            this.Logger.Debug("Constructor Complete with {0} results", this.Results.Count());
        }

        #endregion

        #region Methods

        public abstract void Write();

        #endregion

        #region Properties

        protected ILogger Logger
        {
            get { return this.currentLogger; }
        }

        protected T Results
        {
            get { return this.currentResults; }
        }

        protected IConfig Config
        {
            get { return this.currentConfig; }
        }

        #endregion
    }
}