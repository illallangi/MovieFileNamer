using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer
{
    public sealed class ResultSource : IResultSource
    {
        #region Fields

        private readonly IConfig currentConfig;
        private readonly ILogger currentLogger;
        private readonly IHttpClient currentHttpClient;
        private readonly ICheck[] currentChecks;

        #endregion

        public ResultSource(ILogger logger, IConfig config, IHttpClient httpClient, ICheck[] checks)
        {
            this.currentConfig = config;
            this.currentHttpClient = httpClient;
            this.currentLogger = logger;
            this.currentChecks = checks;
            this.Logger.Debug("Constructor Complete");

            foreach (var check in this.Checks)
            {
                this.Logger.Debug("Added check {0}", check.GetType());
            }
        }

        public IResult Get()
        {
            var result = new Result
                {
                    Directory = this.Config.Directory,
                };
    
            foreach (var directory in Directory.EnumerateDirectories(this.Config.Directory))
            {
                this.Logger.Debug("Checking {0}", directory);
                result.AddMovie(Path.GetFileName(directory));
                
                MovieDbResultCollection results;
                var uri = string.Format(this.Config.TheMovieDbApiUri,
                                        this.Config.TheMovieDbApiKey,
                                        HttpUtility.UrlEncode(Path.GetFileName(directory).GetTitle()),
                                        HttpUtility.UrlEncode(Path.GetFileName(directory).GetYear()));
                var xml = string.Empty;

                try
                {
                    xml = this.HttpClient.HttpGet(uri, @"application/json");
                    results = MovieDbResultCollection.FromString(xml);
                }
                catch (Exception e)
                {
                    result.AddError(Path.GetFileName(directory),
                                    string.Format(@"""{0}"" encountered error ""{1}"" parsing {2}\r\n{3}",
                                      Path.GetFileName(directory),
                                      e.Message,
                                      uri,
                                      xml));
                    continue;
                }

                var movie = results.Results.FirstOrDefault(f => f.title.Equals(Path.GetFileName(directory).GetTitle()))
                            ?? results.Results.FirstOrDefault();

                foreach (var priority in this.Checks.Select(c => c.Priority).Distinct().OrderBy(i => i))
                {
                    var pass = true;
                    this.Logger.Debug("Executing level {0} checks", priority);
                    foreach (var check in this.Checks.Where(check => priority == check.Priority))
                    {
                        this.Logger.Debug("Checking {0} with {1}", Path.GetFileName(directory), check.GetType());
                        if (!check.Passes(movie, directory, result))
                        {
                            pass = false;
                        }
                    }

                    if (!pass)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        #region Properties

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        private IHttpClient HttpClient
        {
            get { return this.currentHttpClient; }
        }

        private IEnumerable<ICheck> Checks
        {
            get { return this.currentChecks; }
        }

        #endregion
    }
}