using System.Linq;
using System.Collections.Generic;

namespace Illallangi.MovieFileNamer.Model
{
    public sealed class Result : IResult
    {
        #region Fields

        private IDictionary<string, IList<string>> currentMovies;

        #endregion

        #region Properties

        public string Directory { get; set; }

        public string Name { get; set; }

        public IDictionary<string, IList<string>> Movies
        {
            get { return this.currentMovies ?? (this.currentMovies = new Dictionary<string, IList<string>>()); }
        }

        public IDictionary<string, IList<string>> FailedMovies
        {
            get { return this.Movies.Where(m => 0 != m.Value.Count).ToDictionary(m => m.Key, m => m.Value); }
        }

        public IDictionary<string, IList<string>> PassedMovies
        {
            get { return this.Movies.Where(m => 0 != m.Value.Count).ToDictionary(m => m.Key, m => m.Value); }
        }

        public IEnumerable<string> Errors
        {
            get { return this.Movies.SelectMany(movie => movie.Value); }
        }

        public bool HasErrors
        {
            get { return this.Errors.Any(); }
        }

        #endregion

        #region Methods

        public void AddMovie(string movie)
        {
            if (!this.Movies.ContainsKey(movie))
            {
                this.Movies.Add(movie, new List<string>());
            }
        }

        public void AddError(string movie, string error, params object[] args)
        {
            this.AddMovie(movie);
            this.Movies[movie].Add(string.Format(error, args));
        }

        #endregion
    }
}