using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfHasFiles : CheckBase
    {
        public override bool Passes(MovieDbResult entry, MovieDirectory directory, Result result)
        {
            if (directory.HasFiles)
            {
                result.AddError(directory.GetFileName(), @"""{0}"" has no files;", directory.GetFileName());
                return false;
            }

            return true;
        }
    }
}