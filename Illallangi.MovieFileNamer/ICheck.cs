using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer
{
    public interface ICheck
    {
        bool Passes(MovieDbResult entry, MovieDirectory directory, Result result);
        int Priority { get; }
    }
}