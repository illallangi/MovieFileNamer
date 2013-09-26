using System.Collections.Generic;
using System.IO;
using Ninject.Extensions.Logging;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class HtmlOutput : BaseOutput<HtmlResultCollection, IHtmlResult>
    {
        #region Constructor

        public HtmlOutput(ILogger logger, IConfig config, HtmlResultCollection htmlResults)
            : base(logger, config, htmlResults)
        {
        }

        #endregion

        #region Methods

        public override void Write()
        {
            foreach (var result in this.Results)
            {
                if (null != result.Html)
                {
                    File.WriteAllText(string.Format(this.Config.HtmlPath, result.Name), result.Html);
                }
            }
        }

        #endregion
    }
}