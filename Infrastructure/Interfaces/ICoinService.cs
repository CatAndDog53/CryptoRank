using Models;

namespace Infrastructure.Interfaces
{
    public interface ICoinService
    {
        Task<IList<CoinMarketData>> GetCoinMarketDataAsync(string vsCurrency, string? order, int? perPage,
        int? page, bool? sparkline, string? priceChangePercentage);
    }
}
