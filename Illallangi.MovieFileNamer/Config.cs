using System;
using System.Configuration;
using System.Data;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer
{
    public sealed class Config : IConfig, IHttpClientConfig, ISmtpClientConfig
    {
        #region Fields

        private readonly ILogger currentLogger;
        
        private string currentTemplate;
        private string currentFromAddress;
        private string currentToAddress;
        private string currentTheMovieDbApiKey;
        private string currentTheMovieDbApiUri;
        private string currentCachePath;
        private string currentDirectory;
        private string currentJsonPath;
        private string currentHtmlPath;
        private string currentSmtpServer;
        private string currentSmtpPort;

        private const string TEMPLATEKEY = @"Template";
        private const string FROMADDRESSKEY = @"FromAddress";
        private const string TOADDRESSKEY = @"ToAddress";
        private const string THEMOVIEDBAPIKEYKEY = @"TheMovieDbApiKey";
        private const string THEMOVIEDBAPIURIKEY = @"TheMovieDbApiUri";
        private const string CACHEPATHKEY = @"CachePath";
        private const string DIRECTORYKEY = @"Directory";
        private const string JSONPATHKEY = @"JsonPath";
        private const string HTMLPATHKEY = @"HtmlPath";
        private const string SMTPSERVERKEY = @"SmtpServer";
        private const string SMTPPORTKEY = @"SmtpPort";

        private const string TEMPLATEDEFAULT = @"Email.cshtml";
        private const string THEMOVIEDBAPIURIDEFAULT = @"https://api.themoviedb.org/3/search/movie?api_key={0}&query={1}&year={2}";
        private const string CACHEPATHDEFAULT = @"%temp%\Illallangi.MovieFileNamer";
        private const string JSONPATHDEFAULT = @"%temp%\Illallangi.MovieFileNamer.json";
        private const string HTMLPATHDEFAULT = @"%temp%\Illallangi.MovieFileNamer.html";
        private const string SMTPPORTDEFAULT = @"25";
        
        #endregion

        #region Constructor
        
        public Config(ILogger logger)
        {
            this.currentLogger = logger;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        private string GetConfigValue(string key, string defaultValue = null)
        {
            string result;
            if (!string.IsNullOrWhiteSpace(result = ConfigurationManager.AppSettings[key]))
            {
                this.Logger.Debug(@"Returning value from config for {0} of ""{1}""", key, result);
                return result;
            }

            if (!string.IsNullOrWhiteSpace(defaultValue))
            {
                this.Logger.Debug(@"Returning default value for {0} of ""{1}""", key, defaultValue);
                return defaultValue;
            }

            this.Logger.Error(@"No configured value found for {0}", key);
            throw new NoNullAllowedException(key);
        }

        #endregion

        #region Properties

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        public string Template
        {
            get { return this.currentTemplate ?? (this.currentTemplate = this.GetConfigValue(TEMPLATEKEY, TEMPLATEDEFAULT)); }
        }

        public string FromAddress
        {
            get { return this.currentFromAddress ?? (this.currentFromAddress = this.GetConfigValue(FROMADDRESSKEY)); }
        }

        public string ToAddress
        {
            get { return this.currentToAddress ?? (this.currentToAddress = this.GetConfigValue(TOADDRESSKEY)); }    
        }

        public string TheMovieDbApiKey
        {
            get { return this.currentTheMovieDbApiKey ?? (this.currentTheMovieDbApiKey = this.GetConfigValue(THEMOVIEDBAPIKEYKEY)); }
        }

        public string TheMovieDbApiUri
        {
            get { return this.currentTheMovieDbApiUri ?? (this.currentTheMovieDbApiUri = this.GetConfigValue(THEMOVIEDBAPIURIKEY, THEMOVIEDBAPIURIDEFAULT)); }
        }

        public string CachePath
        {
            get { return this.currentCachePath ?? (this.currentCachePath = Environment.ExpandEnvironmentVariables(this.GetConfigValue(CACHEPATHKEY, CACHEPATHDEFAULT)).CreatePath()); }
        }

        public string Directory
        {
            get
            {
                return this.currentDirectory ??
                       (this.currentDirectory =
                        this.GetConfigValue(DIRECTORYKEY, System.IO.Directory.GetCurrentDirectory()));
            }
        }

        public string JsonPath
        {
            get { return this.currentJsonPath ?? (this.currentJsonPath = Environment.ExpandEnvironmentVariables(this.GetConfigValue(JSONPATHKEY, JSONPATHDEFAULT))); }
        }

        public string HtmlPath
        {
            get { return this.currentHtmlPath ?? (this.currentHtmlPath = Environment.ExpandEnvironmentVariables(this.GetConfigValue(HTMLPATHKEY, HTMLPATHDEFAULT))); }
        }

        public string SmtpServer
        {
            get
            {
                return this.currentSmtpServer
                       ?? (this.currentSmtpServer =
                           this.GetConfigValue(SMTPSERVERKEY));
            }
        }

        public int SmtpPort
        {
            get
            {
                return
                    int.Parse(
                        this.currentSmtpPort
                        ?? (this.currentSmtpPort = this.GetConfigValue(SMTPPORTKEY, SMTPPORTDEFAULT)));
            }
        }

        #endregion
    }
}