using ChequeWriter.Modules.ChequeModule.Core;
using ChequeWriter.Modules.UserModule.Core;
using DataServices;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Configuration;
using System.IO;
using System.Runtime.Versioning;
using System.Windows;
using Unity;

namespace ChequeWriter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ChequeWriterMainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureOptions(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            containerRegistry.RegisterInstance(serviceProvider.GetService<IOptions<ChequeWriterOption>>());
            containerRegistry.Register<IDesignTimeDbContextFactory<ChequeWriterDbContext>, ChequeWriterDbContextFactory>();
            containerRegistry.Register<IChequeManager, ChequeManager>();
            containerRegistry.Register<IUserManager, UserManager>();
            containerRegistry.Register<IDataService, DataService>();

            IEventAggregator eventAggregator = containerRegistry.GetContainer().Resolve<IEventAggregator>();
            IRegionManager _regionManager = containerRegistry.GetContainer().Resolve<IRegionManager>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Modules.MainModule.MainModule>();
            moduleCatalog.AddModule<Modules.ChequeModule.ChequeModule > ();
            moduleCatalog.AddModule<Modules.ChequeModule.ReportModule>();
            moduleCatalog.AddModule<Modules.UserModule.UserModule>();
            moduleCatalog.AddModule<Modules.NotificationModule.NotificationModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private static void ConfigureOptions(IServiceCollection services)
        {
            string? connectionString = ConfigurationManager.ConnectionStrings["cheque-writer-ui"].ConnectionString;

            ChequeWriterOption customOptions = new ChequeWriterOption
            {
                ConnectionString = new Dictionary<string, string>() { { "cheque-writer-ui", connectionString } }
            };

            services.Configure<ChequeWriterOption>(options =>
            {
                options.ConnectionString = customOptions.ConnectionString;
            });
        }
    }
}
