using System.IO;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfFoundInMovieDb : ICheck
    {
        public bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (null == movie)
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" not found in MovieDb", Path.GetFileName(directory));
                return false;
            }
            return true;
        }
    }
}