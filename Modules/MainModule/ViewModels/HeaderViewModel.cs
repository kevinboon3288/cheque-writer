

namespace ChequeWriter.Modules.MainModule.ViewModels;

public class HeaderViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;



    public HeaderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        _eventAggregator.GetEvent<UIControlEvent>().Subscribe(OnTitleChanged);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        // Entry Point when navigate to HeaderView
    }

    private void OnTitleChanged(string title) 
    {
        
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
