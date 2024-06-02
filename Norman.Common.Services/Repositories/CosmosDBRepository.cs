using Microsoft.Azure.Cosmos;
using Norman.Common.Services.Interfaces;

namespace Nour.Common.Services.Repositories
{
    public class CosmosDBRepository : ICosmosDBInterface
    {
        private readonly CosmosClient cosmosClient;

        public CosmosDBRepository(CosmosClient cosmosClient)
        {
            this.cosmosClient = cosmosClient;
        }

        //public async Task<T> CreateAsync<T>(string databaseName, string containerName, T entity)
        //{
        //    var container = cosmosClient.GetContainer(databaseName, containerName);
        //    var response = await container.CreateItemAsync(entity, new PartitionKey(containerName));
        //    return response.Resource;
        //}

        //public async Task DeleteAsync<T>(string databaseName, string containerName, T entity)
        //{
        //    var container = cosmosClient.GetContainer(databaseName, containerName);
        //    return await container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
        //}

        public async Task<List<T>> GetAllAsync<T>(string databaseName, string containerName)
        {
            var container = cosmosClient.GetContainer(databaseName, containerName);
            var query = container.GetItemQueryIterator<T>(new QueryDefinition("SELECT * FROM " + containerName));
            var result = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }
            return result;
        }

        public async Task<T> GetOneAsync<T>(string databaseName, string containerName, string id)
        {
            var container = cosmosClient.GetContainer(databaseName, containerName);
            var query = container.GetItemQueryIterator<T>(new QueryDefinition($"SELECT * FROM '{containerName}' WHERE id = '{id}'"));
            if (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                if (response.Any())
                {
                    return response.First();
                }
                else
                {
                    throw new KeyNotFoundException($"Item with ID '{id}' not found in table '{containerName}'.");
                }
            }
            else
            {
                throw new Exception("No results found.");
            }
        }

        //public async Task<T> UpdateAsync<T>(string databaseName, string containerName, string id, T entity)
        //{
        //    var container = cosmosClient.GetContainer(databaseName, containerName);
        //    var response = await container.ReplaceItemAsync<T>(entity, id, new PartitionKey(containerName));
        //    return response.Resource;
        //}
    }
}
