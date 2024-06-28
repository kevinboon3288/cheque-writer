
namespace ChequeWriter.Modules.ChequeModule;

public class ChequeModule : IModule
{
    private readonly IRegionManager _regionManager;

    public ChequeModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion("ChequeFormContentRegion", typeof(ChequeFormView));
        _regionManager.RegisterViewWithRegion("ChequePreviewContentRegion", typeof(ChequePreviewView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<ChequeView>();
        containerRegistry.RegisterForNavigation<ChequeManagementView>();
        containerRegistry.RegisterForNavigation<ChequeFormView>();
        containerRegistry.RegisterForNavigation<ChequePreviewView>();

        ViewModelLocationProvider.Register<ChequeView, ChequeViewModel>();
        ViewModelLocationProvider.Register<ChequeManagementView, ChequeManagementViewModel>();
        ViewModelLocationProvider.Register<ChequePreviewView, ChequePreviewViewModel>();
    }
}
