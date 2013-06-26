using System.Collections.Generic;

namespace Illallangi.MovieFileNamer.Model
{
    public class MovieDbResultCollection : JsonFileBackedObject<MovieDbResultCollection>
    {
        #region Constructor

        public MovieDbResultCollection()
        {
            this.Results = new List<MovieDbResult>();
        }

        #endregion

        #region Properties

        // InconsistentNaming suppressed for deserialization purposes
        // ReSharper disable InconsistentNaming
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
        public int Page { get; set; }
        public List<MovieDbResult> Results { get; set; }
        // ReSharper restore InconsistentNaming

        #endregion
    }
}