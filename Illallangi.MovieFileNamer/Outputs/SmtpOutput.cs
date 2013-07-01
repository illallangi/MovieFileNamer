namespace Illallangi.MovieFileNamer.Outputs
{
    using Ninject.Extensions.Logging;

    using System.Collections.Generic;
    using System.IO;

    public sealed class SmtpOutput : BaseOutput<HtmlResultCollection, IHtmlResult>
    {
        #region Fields

        private readonly ISmtpClient currentSmtpClient;

        #endregion

        #region Constructor

        public SmtpOutput(ILogger logger, IConfig config, ISmtpClient smtpClient, HtmlResultCollection htmlResults)
            : base(logger, config, htmlResults)
        {
            this.currentSmtpClient = smtpClient;
        }

        #endregion

        #region Methods

        public override void Write()
        {
            foreach (var result in this.Results)
            {
                if (null != result.Html)
                {
                    this.SmtpClient.SendEmail(this.Config.FromAddress, this.Config.ToAddress, string.Format(this.Config.Subject, result.Name), result.Html);
                }
            }
        }

        #endregion

        #region Properties

        private ISmtpClient SmtpClient
        {
            get { return this.currentSmtpClient; }
        }

        #endregion
    }
}