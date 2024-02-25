using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using ChequeWriter.Modules.ChequeModule.Views;
using ChequeWriter.Modules.ChequeModule.ViewModels;

namespace ChequeWriter.Modules.ChequeModule
{
    public class ChequeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ChequeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ChequeContentRegion", typeof(ChequeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ChequeView>();

            ViewModelLocationProvider.Register<ChequeView, ChequeViewModel>();
        }
    }
}
