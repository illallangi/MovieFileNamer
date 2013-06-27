namespace Illallangi.MovieFileNamer.Checks
{
    using Illallangi.MovieFileNamer.Model;

    public abstract class CheckBase : ICheck
    {
        public abstract bool Passes(MovieDbResult movie, string directory, Result result);

        public virtual int Priority
        {
            get
            {
                return 100;
            }
        }
    }
}