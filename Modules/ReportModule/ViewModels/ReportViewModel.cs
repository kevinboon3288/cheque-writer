using ChequeWriter.Modules.CommonModule.Events;

namespace ChequeWriter.Modules.ReportModule.ViewModels
{
    public class ReportViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand NavigateBack { get; set; }

        public ReportViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NavigateBack = new DelegateCommand(OnReturn);
        }

        private void OnReturn()
        {
            IRegion region = _regionManager.Regions["UserContentRegion"];
            region.RequestNavigate("MainView");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<UIControlEvent>().Publish("Report");
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
