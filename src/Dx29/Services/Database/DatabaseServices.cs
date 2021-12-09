using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace Dx29.Services
{
    public class DatabaseServices
    {
        public DatabaseServices()
        {
        }

        public CosmosClient Client { get; private set; }
        public Database Database { get; private set; }

        public async Task InitializeAsync()
        {
            Client = new CosmosClientBuilder(AppConfig.DatabaseConnectionString)
                .WithApplicationName("Dx29")
                .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                .Build();

            var response = await Client.CreateDatabaseIfNotExistsAsync(AppConfig.DatabaseName);
            Database = response.Database;
        }

        public IAsyncEnumerable<T> ExecuteQueryAsync<T>(string container, string query)
        {
            return ExecuteQueryAsync<T>(Database.GetContainer(container), query);
        }
        public async IAsyncEnumerable<T> ExecuteQueryAsync<T>(Container container, string query)
        {
            var queryDef = new QueryDefinition(query);
            var iterator = container.GetItemQueryIterator<T>(queryDef);
            while (iterator.HasMoreResults)
            {
                var current = await iterator.ReadNextAsync();
                foreach (var item in current)
                {
                    yield return item;
                }
            }
        }
    }
}
