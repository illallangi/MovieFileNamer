namespace Illallangi.MovieFileNamer
{
    public interface IConfig
    {
        string Template { get; }
        string FromAddress { get; }
        string ToAddress { get; }
        string TheMovieDbApiKey { get; }
        string TheMovieDbApiUri { get; }
        string Directory { get; }
        string JsonPath { get; }
        string HtmlPath { get; }
        string Subject { get; }
        int Interval { get; }
    }
}