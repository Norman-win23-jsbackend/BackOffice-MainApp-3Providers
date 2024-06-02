namespace Norman.Common.Services.Interfaces
{
    public interface ICosmosDBInterface
    {
        Task<List<T>> GetAllAsync<T>(string databaseName, string containerName);
        Task<T> GetOneAsync<T>(string databaseName, string containerName, string id);
        //Task<T> CreateAsync<T>(string databaseName, string containerName, T entity);
        //Task<T> UpdateAsync<T>(string databaseName, string containerName, T entity);
        //Task DeleteAsync<T>(string databaseName, string containerName, T entity);
    }
}
