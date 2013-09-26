namespace Illallangi.MovieFileNamer
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public sealed class ProgramInstaller : Installer
    {
        private ServiceInstaller currentServiceInstaller;

        private ServiceProcessInstaller currentServiceProcessInstaller;

        public ProgramInstaller()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.currentServiceInstaller = new ServiceInstaller();
            this.currentServiceProcessInstaller = new ServiceProcessInstaller { Account = ServiceAccount.LocalService };

            this.currentServiceInstaller.DisplayName = "Illallangi Movie File Namer";
            this.currentServiceInstaller.ServiceName = "Illallangi.MovieFileNamer";

            this.Installers.AddRange(new Installer[] { this.currentServiceProcessInstaller, this.currentServiceInstaller });
        }
    }
}