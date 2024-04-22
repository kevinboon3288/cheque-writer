namespace ChequeWriter.Modules.MainModule;

public class MainModule : IModule
{
    private readonly IRegionManager _regionManager;

    public MainModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion("HeaderContentRegion", typeof(HeaderView));
        _regionManager.RegisterViewWithRegion("ModuleContentRegion", typeof(ModuleView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainView>();
        containerRegistry.RegisterForNavigation<HeaderView>();
        containerRegistry.RegisterForNavigation<ModuleView>();

        ViewModelLocationProvider.Register<MainView, MainViewModel>();
        ViewModelLocationProvider.Register<HeaderView, HeaderViewModel>();
        ViewModelLocationProvider.Register<ModuleView, ModuleViewModel>();
    }
}
