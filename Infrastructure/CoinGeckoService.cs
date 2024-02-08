using Infrastructure.CoinGeckoApi;
using Infrastructure.Interfaces;
using Models;

namespace Infrastructure
{
    public class CoinGeckoService : ICoinService
    {
        public IApiService _apiService;

        public CoinGeckoService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IList<CoinMarketData>> GetCoinMarketDataAsync(string vsCurrency, string? order, int? perPage,
        int? page, bool? sparkline, string? priceChangePercentage)
        {
            return await _apiService.GetAsync<List<CoinMarketData>>(QueryBuilderService.BuildQuery(CoinGeckoEndPoints.CoinsMarkets,
            new Dictionary<string, object>
            {
                {"vs_currency", vsCurrency},
                {"order", order},
                {"per_page", perPage},
                {"page", page},
                {"sparkline", sparkline},
                {"price_change_percentage", priceChangePercentage},
            })).ConfigureAwait(false);
        }

        public async Task<IList<CoinShortData>> GetCoinListAsync()
        {
            return await _apiService.GetAsync<List<CoinShortData>>(CoinGeckoEndPoints.CoinsList).ConfigureAwait(false);
        }
    }
}
