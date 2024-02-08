using Infrastructure.Interfaces;
using System.Text.Json;

namespace Infrastructure
{
    public class ApiService : IApiService
    {
        public async Task<T> GetAsync<T>(Uri uri)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonSerializer.Deserialize<T>(responseContent);
            }
            catch (Exception e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }
}
