namespace Infrastructure.Interfaces
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(Uri uri);
    }
}
