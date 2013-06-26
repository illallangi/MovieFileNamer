using System.IO;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;
using System.Web.Script.Serialization;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class JsonOutput : IOutput
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IConfig currentConfig;
        private readonly IResult currentResult;

        #endregion

        #region Constructor

        public JsonOutput(ILogger logger, IConfig config, IResult result)
        {
            this.currentLogger = logger;
            this.currentConfig = config;
            this.currentResult = result;
            this.Logger.Debug("Constructor Complete");
        }

        #endregion

        #region Methods

        public void Write()
        {
            File.WriteAllText(this.Config.JsonPath, this.JavaScriptSerializer.Serialize(this.Result));
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

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        private JavaScriptSerializer JavaScriptSerializer
        {
            get { return new JavaScriptSerializer(); }
        }

        #endregion
    }
}