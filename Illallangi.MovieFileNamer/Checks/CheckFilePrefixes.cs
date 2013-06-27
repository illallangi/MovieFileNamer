using System;
using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckFilePrefixes : ICheck
    {
        public bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (null == movie)
            {
                return true;
            }

            var good = true;
            foreach (var file in Directory.GetFiles(directory).Where(file =>
                {
                    var fileName = Path.GetFileName(file);
                    return fileName != null && !fileName.StartsWith(movie.FileName, StringComparison.InvariantCultureIgnoreCase);
                }))
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" should start with ""{1}""", Path.GetFileName(file), movie.FileName);
                good = false;
            }
            return good;
        }
    }
}