using System.IO;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class HtmlOutput : IOutput
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IConfig currentConfig;
        private readonly IHtmlResult currentHtmlResult;

        #endregion

        #region Constructor

        public HtmlOutput(ILogger logger, IConfig config, IHtmlResult htmlResult)
        {
            this.currentLogger = logger;
            this.currentConfig = config;
            this.currentHtmlResult = htmlResult;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        public void Write()
        {
            File.WriteAllText(this.Config.HtmlPath, this.HtmlResult.Html);
        }

        #endregion

        #region Properties

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        private IHtmlResult HtmlResult
        {
            get { return this.currentHtmlResult; }
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        #endregion
    }
}