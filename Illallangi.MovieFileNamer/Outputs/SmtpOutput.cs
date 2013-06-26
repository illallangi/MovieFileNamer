using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class SmtpOutput : IOutput
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IConfig currentConfig;
        private readonly IHtmlResult currentHtmlResult;
        private readonly ISmtpClient currentSmtpClient;

        #endregion

        #region Constructor

        public SmtpOutput(ILogger logger, IConfig config, ISmtpClient smtpClient, IHtmlResult htmlResult)
        {
            this.currentLogger = logger;
            this.currentConfig = config;
            this.currentSmtpClient = smtpClient;
            this.currentHtmlResult = htmlResult;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        public void Write()
        {
            this.SmtpClient.SendEmail(this.Config.FromAddress, this.Config.ToAddress, "Welcome to our site!", this.HtmlResult.Html);
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

        private ISmtpClient SmtpClient
        {
            get { return this.currentSmtpClient; }
        }

        #endregion
    }
}