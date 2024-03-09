using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ChequeWriter.Modules.ReportModule.ViewModels
{
    public class ReportViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateBack { get; set; }

        public ReportViewModel(IRegionManager regionManager)
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
            // Entry Point when navigate to ReportView
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
