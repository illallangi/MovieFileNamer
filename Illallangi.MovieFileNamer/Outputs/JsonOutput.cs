using System.IO;
using System.Collections.Generic;
using Illallangi.MovieFileNamer.Model;
using Ninject.Extensions.Logging;
using System.Web.Script.Serialization;

namespace Illallangi.MovieFileNamer.Outputs
{
    public sealed class JsonOutput : BaseOutput<ResultCollection, IResult>
    {
        #region Constructor

        public JsonOutput(ILogger logger, IConfig config, ResultCollection results)
            : base(logger, config, results)
        {
        }

        #endregion

        #region Methods

        public override void Write()
        {
            foreach (var result in this.Results)
            {
                File.WriteAllText(string.Format(this.Config.JsonPath, result.Name), this.JavaScriptSerializer.Serialize(result));
            }
        }

        #endregion

        #region Properties

        private JavaScriptSerializer JavaScriptSerializer
        {
            get { return new JavaScriptSerializer(); }
        }

        #endregion
    }
}