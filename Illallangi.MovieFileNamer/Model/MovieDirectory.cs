namespace Illallangi.MovieFileNamer.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class MovieDirectory
    {
        private readonly string currentPath;

        public MovieDirectory(string path)
        {
            this.currentPath = path;
        }

        public bool HasFiles
        {
            get
            {
                return this.GetFiles().Any();
            }
        }

        public string Path
        {
            get
            {
                return this.currentPath;
            }
        }

        public bool HasDirectories
        {
            get
            {
                return Directory.GetDirectories(this.Path).Any();
            }
        }

        public string GetFileName()
        {
            return System.IO.Path.GetFileName(this.Path);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(this.Path).Where(f => !this.ExcludeFiles.Contains(System.IO.Path.GetFileName(f), StringComparer.InvariantCultureIgnoreCase));
        }

        protected IEnumerable<string> ExcludeFiles
        {
            get
            {
                yield return "Thumbs.db";
            }
        }
    }
}