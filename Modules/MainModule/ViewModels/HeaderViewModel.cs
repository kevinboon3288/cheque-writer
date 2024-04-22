

namespace ChequeWriter.Modules.MainModule.ViewModels;

public class HeaderViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private string? _title;

    public string? Title 
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }

    public DelegateCommand NavigateCommand { get; private set; }

    public HeaderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        NavigateCommand = new DelegateCommand(OnNavigateToUserManagement);

        _eventAggregator.GetEvent<UIControlEvent>().Subscribe(OnTitleChanged);
    }

    private void OnNavigateToUserManagement() 
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("UserManagementView");
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        // Entry Point when navigate to HeaderView
    }

    private void OnTitleChanged(string title) 
    {
        if (!string.IsNullOrEmpty(title)) 
        {
            Title = title;
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
