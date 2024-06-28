namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequePreviewViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;

    public ChequePreviewViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
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
