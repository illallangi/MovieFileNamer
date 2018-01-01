using System;
using System.IO;
using System.Linq;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckFilePrefixes : CheckBase
    {
        public override bool Passes(MovieDbResult entry, MovieDirectory directory, Result result)
        {
            var good = true;
            foreach (var file in directory.GetFiles().Where(file =>
                {
                    var fileName = Path.GetFileName(file);
                    return fileName != null && !fileName.StartsWith(entry.FileName.Replace("_",""), StringComparison.InvariantCultureIgnoreCase);
                }))
            {
                result.AddError(directory.GetFileName(), @"""{0}"" should start with ""{1}""", Path.GetFileName(file), entry.FileName);
                good = false;
            }
            return good;
        }
    }
}