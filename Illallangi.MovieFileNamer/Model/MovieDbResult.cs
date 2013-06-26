using System.Web;

namespace Illallangi.MovieFileNamer.Model
{
    public class MovieDbResult
    {
        #region Methods

        public override string ToString()
        {
            return string.Format("{0} ({1})", HttpUtility.HtmlDecode(this.title)
                .Replace("L!f", "Lif")
                .Replace(@":", string.Empty)
                .Replace(@"?", string.Empty)
                .Replace(@"!", string.Empty)
                .Replace(@"³", " 3")
                .Replace(@"/", @" ")
                .Replace(@"é", @"e")
                .Replace(@"·", @"-"), this.Year);
        }

        #endregion

        #region Properties

        // InconsistentNaming suppressed for deserialization purposes
        // ReSharper disable InconsistentNaming
        public string adult { get; set; }
        public string backdrop_path { get; set; }
        public string id { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }
        public string popularity { get; set; }
        public string title { get; set; }
        public string vote_average { get; set; }
        public string vote_count { get; set; }
        // ReSharper restore InconsistentNaming

        public string Year
        {
            get { return this.release_date.Substring(0, 4); }
        }

        public string FileName
        {
            get
            {
                return this.ToString()
                           .ToLower()
                           .Replace(@" ", @".")
                           .Replace(@"(", string.Empty)
                           .Replace(@")", string.Empty)
                           .Replace(@",", string.Empty)
                           .Replace(@"'", string.Empty)
                           .Replace(@"-", @".")
                           .Replace(@"...", @".")
                           .Replace(@"..", @".")
                           .Replace(@"con.", @"con")
                           .Replace(@".3.d.", @".3d.");
            }
        }

        #endregion
    }
}