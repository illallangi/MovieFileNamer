using System.Collections.Generic;
using System.Configuration;
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
                .ToMethod(x => MovieFileNamerConfiguration.GetConfig())
                .InSingletonScope();

            this.Bind<IResultSource>()
              .To<ResultSource>()
              .InSingletonScope();

            this.Bind<IHtmlResultSource>()
              .To<HtmlResultSource>()
              .InSingletonScope();

            this.Bind<IHttpClientConfig>()
                .To<MovieFileNamerConfiguration>()
                .InSingletonScope();

            this.Bind<ResultCollection>()
                .ToMethod(x => x.Kernel.Get<IResultSource>().Get())
                .InSingletonScope();

            this.Bind<IHttpClient>()
                .To<HttpClient>()
                .InSingletonScope();

            this.Bind<HtmlResultCollection>()
                .ToMethod(x => x.Kernel.Get<IHtmlResultSource>().Get())
                .InSingletonScope();

            this.Bind<ISmtpClient>()
                .To<SmtpClient>()
                .InSingletonScope();

            this.Bind<ISmtpClientConfig>()
                .ToMethod(x => x.Kernel.Get<IConfig>())
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