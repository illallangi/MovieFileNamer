namespace Illallangi.MovieFileNamer.Checks
{
    using Illallangi.MovieFileNamer.Model;

    public abstract class CheckBase : ICheck
    {
        public abstract bool Passes(MovieDbResult entry, MovieDirectory directory, Result result);

        public virtual int Priority
        {
            get
            {
                return 100;
            }
        }
    }
}