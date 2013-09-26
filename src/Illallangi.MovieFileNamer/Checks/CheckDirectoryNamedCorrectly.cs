using System;
using System.IO;
using Illallangi.MovieFileNamer.Model;

namespace Illallangi.MovieFileNamer.Checks
{
    public sealed class CheckDirectoryNamedCorrectly : CheckBase
    {
        public override bool Passes(MovieDbResult entry, MovieDirectory directory, Result result)
        {
            if (!entry.ToString().Equals(directory.GetFileName(), StringComparison.InvariantCultureIgnoreCase))
            {
                result.AddError(directory.GetFileName(), @"""{0}"" should be ""{1}""", directory.GetFileName(), entry);
                return false;
            }

            return true;
        }

        public override int Priority
        {
            get
            {
                return 10;
            }
        }
    }
}