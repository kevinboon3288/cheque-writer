namespace ChequeWriter.Modules.MainModule.ViewModels;

public class MainViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        IRegion headerRegion = _regionManager.Regions["HeaderContentRegion"];
        IRegion moduleRegion = _regionManager.Regions["ModuleContentRegion"];

        headerRegion.RequestNavigate("HeaderView");
        moduleRegion.RequestNavigate("ModuleView");

        _eventAggregator.GetEvent<HeaderTitleUIControlEvent>().Publish("Main");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
