namespace Infrastructure.CoinGeckoApi
{
    public static class CoinGeckoEndPoints
    {
        public static readonly Uri BaseUri = new Uri("https://api.coingecko.com/api/v3/");
        public static readonly Uri CoinsList = new Uri(BaseUri + "coins/list");
        public static readonly Uri CoinsMarkets = new Uri(BaseUri + "coins/markets");
        public static Uri AllDataByCoinId(string id) => new Uri(BaseUri + id);
    }
}
