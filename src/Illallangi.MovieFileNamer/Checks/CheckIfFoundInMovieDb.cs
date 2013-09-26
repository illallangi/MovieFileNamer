using System.IO;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfFoundInMovieDb : CheckBase
    {
        public override bool Passes(MovieDbResult entry, MovieDirectory directory, Result result)
        {
            if (null == entry)
            {
                result.AddError(directory.GetFileName(), @"""{0}"" not found in MovieDb", directory.GetFileName());
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