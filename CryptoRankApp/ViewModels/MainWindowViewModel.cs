using CryptoRankApp.Events;
using CryptoRankApp.Views;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel;

namespace CryptoRankApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private bool _isThemeDark = false;
        private string _title = "Crypto Rank";
        private bool _scrollToTopTrigger;
        public bool ScrollToTopTrigger
        {
            get => _scrollToTopTrigger;
            set
            {
                _scrollToTopTrigger = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ScrollToTopTrigger)));
            }
        }
        public bool IsThemeDark
        {
            get { return _isThemeDark; }
            set { SetProperty(ref _isThemeDark, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand ChangeThemeCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
            ChangeThemeCommand = new DelegateCommand(ChangeTheme);

            eventAggregator.GetEvent<ScrollToTopEvent>().Subscribe(OnScrollToTop);
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

        private void ChangeTheme()
        {
            IsThemeDark = !IsThemeDark;
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            IBaseTheme baseTheme = IsThemeDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            paletteHelper.SetTheme(theme);
        }

        private async void OnScrollToTop()
        {
            if (ScrollToTopTrigger)
            {
                ScrollToTopTrigger = false;
            }

            ScrollToTopTrigger = true;
        }
    }
}
