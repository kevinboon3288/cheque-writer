
using ChequeWriter.Modules.CommonModule.Events;

namespace ChequeWriter.Modules.ChequeModule.ViewModels
{
    public class ChequeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IChequeManager _chequeManager;
        private readonly IEventAggregator _eventAggregator;

        public DelegateCommand NavigateBack { get; set; }

        public ChequeViewModel(IRegionManager regionManager, IChequeManager chequeManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _chequeManager = chequeManager;
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
            _eventAggregator.GetEvent<UIControlEvent>().Publish("Cheque");
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
