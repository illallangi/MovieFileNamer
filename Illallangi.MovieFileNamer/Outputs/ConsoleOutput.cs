using System;
using System.Collections.Generic;
using System.Linq;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class ConsoleOutput : BaseOutput<ResultCollection, IResult>
    {
        #region Constructor

        public ConsoleOutput(ILogger logger, IConfig config, ResultCollection results)
            : base(logger, config, results)
        {
        }
        
        #endregion

        #region Methods

        public override void Write()
        {
            foreach (var result in this.Results)
            {
                Console.WriteLine("{0}: Of {1} movies, {2} had {3} errors.",
                                    result.Name,
                                    result.Movies.Count,
                                    result.FailedMovies.Count,
                                    result.Errors.Count());
            }
        }

        #endregion
    }
}