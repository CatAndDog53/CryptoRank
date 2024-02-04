using CryptoRankApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CryptoRankApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "Crypto Rank";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath == null)
                return;

            var currentCurrenciesRegionView = _regionManager.Regions[RegionNames.CurrenciesContentRegion].NavigationService.Journal.CurrentEntry;
            var currentMainContentRegionView = _regionManager.Regions[RegionNames.MainContentRegion].NavigationService.Journal.CurrentEntry;
            bool forceNavigateCurrenciesContentRegionToTopCurrencirsView =
                currentCurrenciesRegionView != null
                && currentMainContentRegionView != null
                && currentCurrenciesRegionView.Uri.ToString() == nameof(DetailsView)
                && currentMainContentRegionView.Uri.ToString() == nameof(CurrenciesAreaView)
                && navigatePath == nameof(CurrenciesAreaView);

            if (forceNavigateCurrenciesContentRegionToTopCurrencirsView)
            {
                _regionManager.RequestNavigate(RegionNames.CurrenciesContentRegion, nameof(TopCurrenciesView));
                return;
            }

            _regionManager.RequestNavigate(RegionNames.MainContentRegion, navigatePath);

        }
    }
}
