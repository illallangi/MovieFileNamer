using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckIfHasNoDirectories : ICheck
    {
        public bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (0 != Directory.GetDirectories(directory).Count())
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" has subdirectories;", Path.GetFileName(directory));
                return false;
            }
            return true;
        }
    }
}