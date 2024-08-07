﻿namespace ChequeWriter.Modules.ChequeModule
{
    public class ReportModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ReportModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ReportContentRegion", typeof(ReportView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ReportView>();

            ViewModelLocationProvider.Register<ReportView, ReportViewModel>();
        }
    }
}
