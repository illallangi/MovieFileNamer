using System.IO;
using Illallangi.MovieFileNamer.Model;
using RazorEngine;

namespace Illallangi.MovieFileNamer
{
    public class HtmlResult : IHtmlResult
    {
        private string currentHtml;
        private readonly IConfig currentConfig;
        private readonly IResult currentResult;

        public HtmlResult(IConfig config, IResult result)
        {
            this.currentConfig = config;
            this.currentResult = result;
        }

        public string Html
        {
            get 
            {
                return this.currentHtml ?? (this.currentHtml = Razor.Parse(File.ReadAllText(this.Config.Template), this.Result));
            }
        }

        private IResult Result
        {
            get { return this.currentResult; }
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }
    }
}