using System.IO;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfFoundInMovieDb : CheckBase
    {
        public override bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (null == movie)
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" not found in MovieDb", Path.GetFileName(directory));
                return false;
            }

            return true;
        }

        public override int Priority
        {
            get
            {
                return -1;
            }
        }
    }
}