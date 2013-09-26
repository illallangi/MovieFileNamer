namespace Illallangi.MovieFileNamer
{
    using System.Collections.Generic;

    public interface IConfig : ISmtpClientConfig, IHttpClientConfig
    {
        string Template { get; }
        string FromAddress { get; }
        string ToAddress { get; }
        string TheMovieDbApiKey { get; }
        string TheMovieDbApiUri { get; }
        string JsonPath { get; }
        string HtmlPath { get; }
        string Subject { get; }
        int Interval { get; }
        IEnumerable<IDirectory> Directories { get; }
    }
}