using System;
using System.IO;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckDirectoryNamedCorrectly : ICheck
    {
        public bool Passes(MovieDbResult movie, string directory, Result result)
        {
            if (!movie.ToString().Equals(Path.GetFileName(directory), StringComparison.InvariantCultureIgnoreCase))
            {
                result.AddError(Path.GetFileName(directory), @"""{0}"" should be ""{1}""", Path.GetFileName(directory), movie);
                return false;
            }
            return true;
        }
    }
}