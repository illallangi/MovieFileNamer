namespace Illallangi.MovieFileNamer
{
    using log4net.Config;

    using Topshelf;

    public sealed class Program
    {
        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        /// <param name="arguments">The command line arguments passed to the program.</param>
        public static void Main(string[] arguments)
        {
            var host = HostFactory.New(
                x =>
                    {
                        x.Service<ProgramService>(
                            s =>
                                {
                                    s.ConstructUsing(name => new ProgramService());
                                    s.WhenStarted(
                                        tc =>
                                            {
                                                XmlConfigurator.Configure();
                                                tc.Start();
                                            });
                                    s.WhenStopped(tc => tc.Stop());
                                    s.WhenPaused(tc => tc.Pause());
                                    s.WhenContinued(tc => tc.Continue());
                                });
                        x.RunAsLocalSystem();
                        x.SetServiceName("Illallangi.MovieFileNamer");
                        x.SetDisplayName("Illallangi Movie File Namer");
                        x.SetDescription("Illallangi Enterprises Movie File Namer Service");
                    });

            host.Run();
        }
    }
}

