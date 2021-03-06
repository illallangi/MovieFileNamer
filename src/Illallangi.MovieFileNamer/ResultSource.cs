﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer
{
    public sealed class ResultCollection : List<IResult>
    {
    }

    public sealed class HtmlResultCollection : List<IHtmlResult>
    {
    }

    public sealed class HtmlResultSource : IHtmlResultSource
    {
        private readonly ILogger currentLogger;
        private readonly IConfig currentConfig;
        private readonly ResultCollection currentResultCollection;

        public HtmlResultSource(ILogger logger, IConfig config, ResultCollection resultCollection)
        {
            this.currentLogger = logger;
            this.currentConfig = config;
            this.currentResultCollection = resultCollection;
        }

        public HtmlResultCollection Get()
        {
            var resultCollection = new HtmlResultCollection();
            foreach (var result in this.ResultCollection)
            {
                resultCollection.Add(new HtmlResult(this.Config, result));
            }
            return resultCollection;
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        private ResultCollection ResultCollection
        {
            get { return this.currentResultCollection; }
        }
    }

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

        public ResultCollection Get()
        {
            var resultCollection = new ResultCollection();

            foreach (var directoryElement in this.Config.Directories)
            {
                var result = new Result
                    {
                        Directory = directoryElement.Path,
                        Name = directoryElement.Name,
                    };

                foreach (var path in Directory.EnumerateDirectories(directoryElement.Path))
                {
                    var directory = new MovieDirectory(path);
                    this.Logger.Debug("Checking {0}", directory);
                    result.AddMovie(directory.GetFileName());

                    MovieDbResultCollection results;
                    var uri = string.Format(this.Config.TheMovieDbApiUri,
                                            this.Config.TheMovieDbApiKey,
                                            HttpUtility.UrlEncode(directory.GetFileName().GetTitle()),
                                            HttpUtility.UrlEncode(directory.GetFileName().GetYear()));
                    var xml = string.Empty;

                    try
                    {
                        xml = this.HttpClient.HttpGet(uri, @"application/json");
                        results = MovieDbResultCollection.FromString(xml);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Info(@"""{0}"" encountered error ""{1}"" parsing {2}\r\n{3}",
                                          directory.GetFileName(),
                                          e.Message,
                                          uri,
                                          xml);

                        result.AddError(directory.GetFileName(),
                                        string.Format(@"""{0}"" encountered error ""{1}"" parsing {2}\r\n{3}",
                                          directory.GetFileName(),
                                          e.Message,
                                          uri,
                                          xml));
                        continue;
                    }

                    var movie = results.Results.FirstOrDefault(f => f.title.Equals(directory.GetFileName().GetTitle()))
                                ?? results.Results.FirstOrDefault();

                    foreach (var priority in this.Checks.Select(c => c.Priority).Distinct().OrderBy(i => i))
                    {
                        var pass = true;
                        this.Logger.Debug("Executing level {0} checks", priority);
                        foreach (var check in this.Checks.Where(check => priority == check.Priority))
                        {
                            this.Logger.Debug("Checking {0} with {1}", directory.GetFileName(), check.GetType());
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

                resultCollection.Add(result);
            }
            return resultCollection;
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