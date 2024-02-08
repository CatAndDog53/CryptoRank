using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Models;
using Infrastructure.Interfaces;
using Prism.Events;
using CryptoRankApp.Events;
using Prism.Services.Dialogs;
using CryptoRankApp.Views.Components;
using CryptoRankApp.Views;

namespace CryptoRankApp.ViewModels
{
    public class TopCurrenciesViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ICoinService _coinService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        private IList<CoinMarketData> _currentPageCoinMarketData;
        private IList<CoinShortData> _allCoinsShortData;
        private IDictionary<int, IList<CoinMarketData>> _previousPagesData = new Dictionary<int, IList<CoinMarketData>>();
        private int _currentPage = 1;
        private bool _isPreviousPageAvailable;
        private bool _wasDataUpdatedSuccessfully;

        public IList<CoinShortData> AllCoinsShortData
        {
            get { return _allCoinsShortData; }
            set { SetProperty(ref _allCoinsShortData, value); }
        }

        public bool WasDataUpdatedSuccessfully
        {
            get => _wasDataUpdatedSuccessfully;
            set
            {
                SetProperty(ref _wasDataUpdatedSuccessfully, value);
                if (_wasDataUpdatedSuccessfully == false)
                {
                    var parameters = new DialogParameters
                    {
                        { "message", "Too many requests. Please wait a few minutes and try again" }
                    };

                    _dialogService.ShowDialog(nameof(ErrorDialog), parameters, null);
                }
            }
        }

        public IList<CoinMarketData> CurrentPageCoinMarketData
        {
            get { return _currentPageCoinMarketData; }
            set { SetProperty(ref _currentPageCoinMarketData, value); }
        }

        public IDictionary<int, IList<CoinMarketData>> PreviousPagesData
        {
            get { return _previousPagesData; }
            set { SetProperty(ref _previousPagesData, value); }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set { if (value > 0) SetProperty(ref _currentPage, value);
                RaisePropertyChanged(nameof(IsPreviousPageAvailable)); }
        }

        public bool IsPreviousPageAvailable
        {
            get { if (CurrentPage > 1) return true; else return false; }
        }

        public DelegateCommand<string> NavigateToDetailsCommand { get; private set; }
        public DelegateCommand LoadCoinsCommand { get; private set; }
        public DelegateCommand<string> ChangePageCommand { get; private set; }


        public TopCurrenciesViewModel(IRegionManager regionManager, ICoinService coinService, IEventAggregator eventAggregator,
            IDialogService dialogService)
        {
            _regionManager = regionManager;
            _coinService = coinService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            NavigateToDetailsCommand = new DelegateCommand<string>(NavigateToDetails);
            LoadCoinsCommand = new DelegateCommand(async () => await LoadCoinsAsync());
            ChangePageCommand = new DelegateCommand<string>(async (numberChange) => await ChangePage(numberChange));

            Task.Run(LoadCoinsAsync);
            Task.Run(LoadAllCoinsShortDataAsync);
        }

        private void NavigateToDetails(string coinId)
        {
            var parameters = new NavigationParameters
            {
                { "coinId", coinId }
            };
            _regionManager.RequestNavigate(RegionNames.CurrenciesContentRegion, nameof(DetailsView), parameters);
        }

        private async Task<bool> LoadCoinsAsync()
        {
            try
            {
                CurrentPageCoinMarketData = await _coinService.GetCoinMarketDataAsync("usd", "market_cap_desc", 30, CurrentPage, true, "24h,7d");
                WasDataUpdatedSuccessfully = true;
                return true;
            }
            catch
            {
                WasDataUpdatedSuccessfully = false;
                return false;
            }
        }

        private async Task ChangePage(string numberChange)
        {
            SaveCurrentPageData();
            int.TryParse(numberChange, out int parsedNumberChange);
            CurrentPage += parsedNumberChange;
            if (PreviousPagesData.ContainsKey(CurrentPage))
            {
                CurrentPageCoinMarketData = PreviousPagesData[CurrentPage];
            }
            else
            {
                await LoadCoinsAsync();
            }
            _eventAggregator.GetEvent<ScrollToTopEvent>().Publish();
        }

        private void SaveCurrentPageData()
        {
            if (PreviousPagesData.ContainsKey(CurrentPage))
            {
                PreviousPagesData[CurrentPage] = CurrentPageCoinMarketData;
            }
            else
            {
                PreviousPagesData.Add(CurrentPage, CurrentPageCoinMarketData);
            }
        }

        private async Task LoadAllCoinsShortDataAsync()
        {
            AllCoinsShortData = await _coinService.GetCoinListAsync();
        }
    }
}
