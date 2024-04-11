using ChequeWriter.Modules.ChequeModule.Core;

namespace ChequeWriter.Modules.ChequeModule.ViewModels
{
    public class ChequeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IDataService _dataService;

        public DelegateCommand NavigateBack { get; set; }

        public ChequeViewModel(IRegionManager regionManager, IDataService dataService)
        {
            _regionManager = regionManager;
            _dataService = dataService;

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

            var userLevels = _dataService.GetUserLevels();
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
