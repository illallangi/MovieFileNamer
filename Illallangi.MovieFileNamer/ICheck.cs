using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer
{
    public interface ICheck
    {
        bool Passes(MovieDbResult movie, string directory, Result result);
    }
}