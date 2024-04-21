namespace ChequeWriter.Modules.MainModule.ViewModels;

public class ModuleViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand NavigateToCheque { get; set; }
    public DelegateCommand NavigateToReport { get; set; }

    public ModuleViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        NavigateToCheque = new DelegateCommand(OnNavigateToCheque);
        NavigateToReport = new DelegateCommand(OnNavigateToReport);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
    }

    private void OnNavigateToCheque()
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("ChequeView");
    }

    private void OnNavigateToReport()
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("ReportView");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
