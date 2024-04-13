namespace ChequeWriter.Modules.ChequeModule.ViewModels
{
    public class ChequeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IChequeManager _chequeManager;

        public DelegateCommand NavigateBack { get; set; }

        public ChequeViewModel(IRegionManager regionManager, IChequeManager chequeManager)
        {
            _regionManager = regionManager;
            _chequeManager = chequeManager;

            NavigateBack = new DelegateCommand(OnReturn);
        }

        private void OnReturn()
        {
            IRegion region = _regionManager.Regions["MainContentRegion"];
            region.RequestNavigate("MainView");
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
}
