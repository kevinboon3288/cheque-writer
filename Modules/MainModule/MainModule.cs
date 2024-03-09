using ChequeWriter.Modules.MainModule.ViewModels;
using ChequeWriter.Modules.MainModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace ChequeWriter.Modules.MainModule
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("MainContentRegion", typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();

            ViewModelLocationProvider.Register<MainView, MainViewModel>();
        }
    }
}
