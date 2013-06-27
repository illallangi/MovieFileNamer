using Illallangi.MovieFileNamer.Model;
using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace Illallangi.MovieFileNamer
{
    public sealed class ProgramModule<T> : NinjectModule where T : IDriver
    {
        #region Methods

        public override void Load()
        {
            this.Bind<IConfig>()
                .To<Config>()
                .InSingletonScope();

            this.Bind<IResultSource>()
              .To<ResultSource>()
              .InSingletonScope();

            this.Bind<IHttpClientConfig>()
                .To<Config>()
                .InSingletonScope();

            this.Bind<IHtmlResult>()
                .To<HtmlResult>()
                .InSingletonScope();

            this.Bind<IHttpClient>()
                .To<HttpClient>()
                .InSingletonScope();

            this.Bind<IResult>()
                .ToMethod(x => x.Kernel.Get<IResultSource>().Get())
                .InSingletonScope();

            this.Bind<ISmtpClient>()
                .To<SmtpClient>()
                .InSingletonScope();

            this.Bind<ISmtpClientConfig>()
                .To<Config>()
                .InSingletonScope();

            this.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFromAny(typeof(ICheck))
                .BindAllInterfaces()
                .Configure(f => f.InSingletonScope()));

            this.Bind(x => x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFromAny(typeof(IOutput))
                .BindAllInterfaces()
                .Configure(f => f.InSingletonScope()));

            this.Bind<IDriver>()
                .To<T>()
                .InSingletonScope();
        }

        #endregion
    }
}