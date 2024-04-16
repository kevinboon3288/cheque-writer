using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ChequeWriter.Modules.MainModule.ViewModels
{
    public class MainViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateToCheque { get; set; }
        public DelegateCommand NavigateToReport { get; set; }

        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateToCheque = new DelegateCommand(OnNavigateToCheque);
            NavigateToReport = new DelegateCommand(OnNavigateToReport);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Entry Point when navigate to MainView
        }

        private void OnNavigateToCheque()
        {
            IRegion region = _regionManager.Regions["UserContentRegion"];
            region.RequestNavigate("ChequeView");
        }

        private void OnNavigateToReport()
        {
            IRegion region = _regionManager.Regions["UserContentRegion"];
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
}
