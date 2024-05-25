namespace SavaDev.Cache
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);

        Task<IEnumerable<T>> Get<T>(IEnumerable<string> keys);

        Task Set<T>(string key, T value);

        Task Set<T>(Dictionary<string, T> dict);

        Task Delete(string key);
    }
}
