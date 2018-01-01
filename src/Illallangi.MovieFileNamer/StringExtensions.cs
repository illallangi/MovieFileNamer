using System;

namespace Illallangi.MovieFileNamer
{
    public static class StringExtensions
    {
        public static string GetTitle(this string path)
        {
            return path.Contains("(")
                       ? path.Substring(0, path.IndexOf("(", StringComparison.InvariantCulture)).Replace("_", "").Trim()
                       : path.Replace("_", "").Trim();
        }

        public static string GetYear(this string path)
        {
            return path.Contains("(")
                       ? path.Substring(path.IndexOf("(", StringComparison.InvariantCulture)).Trim().Trim("()".ToCharArray())
                       : string.Empty;
        }
    }
}