using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfHasNoDirectories : CheckBase
    {
        public override bool Passes(MovieDbResult entry, MovieDirectory directory, Result result)
        {
            if (!directory.HasDirectories)
            {
                result.AddError(directory.GetFileName(), @"""{0}"" has subdirectories;", directory.GetFileName());
                return false;
            }

            return true;
        }
    }
}