namespace Illallangi.MovieFileNamer
{
    using System.Threading.Tasks;

    public interface IDriver
    {
        ParallelLoopResult Execute();
    }
}