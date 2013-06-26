using System;
using System.Linq;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class ConsoleOutput : IOutput
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IResult currentResult;

        #endregion

        #region Constructor

        public ConsoleOutput(ILogger logger, IResult result)
        {
            this.currentLogger = logger;
            this.currentResult = result;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        public void Write()
        {
            Console.WriteLine("Of {0} movies, {1} had {2} errors.",
                                this.Result.Movies.Count,
                                this.Result.FailedMovies.Count,
                                this.Result.Errors.Count());
        }

        #endregion
        
        #region Properties

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        private IResult Result
        {
            get { return this.currentResult; }
        }

        #endregion
    }
}