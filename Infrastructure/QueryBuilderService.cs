using Infrastructure.CoinGeckoApi;

namespace Infrastructure
{
    public static class QueryBuilderService
    {
        public static Uri BuildQuery(Uri baseUri, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(baseUri.OriginalString))
                throw new ArgumentException("Base path cannot be null or empty", nameof(baseUri));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var queryString = string.Join("&", parameters.Select(param => 
            $"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(Convert.ToString(param.Value))}"));

            string? url = queryString.Length > 0 ? $"{baseUri}?{queryString}" : baseUri.OriginalString;

            return new Uri(CoinGeckoEndPoints.BaseUri, url);
        }
    }
}
