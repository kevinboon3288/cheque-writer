namespace ChequeWriter.Modules.ChequeModule.ViewModels
{
    public class ChequeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateBack { get; set; }

        public ChequeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateBack = new DelegateCommand(OnReturn);
        }

        private void OnReturn()
        {
            IRegion region = _regionManager.Regions["MainContentRegion"];
            region.RequestNavigate("MainView");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Entry Point when navigate to ChequeView
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
