using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfHasFiles : CheckBase
    {
        public override bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (!Directory.GetFiles(directory).Any())
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" has no files;", Path.GetFileName(directory));
                return false;
            }

            return true;
        }
    }
}