namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequeViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand NavigateBack { get; private set; }
    public DelegateCommand NavigateToOverview { get; private set; }

    public ChequeViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        NavigateBack = new DelegateCommand(OnReturn);
        NavigateToOverview = new DelegateCommand(OnNavigateToOverview);
    }

    private void OnReturn()
    {
        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("MainView");
    }

    private void OnNavigateToOverview() 
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("ChequeManagementView");
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _eventAggregator.GetEvent<HeaderTitleUIControlEvent>().Publish("Cheque");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
