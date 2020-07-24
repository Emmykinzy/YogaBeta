using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaBeta.Data
{
    public class CosmoDB
    {
        private static readonly string EndpointUri = "https://24cc2c0b-0ee0-4-231-b9ee.documents.azure.com:443/";
        private static readonly string PrimaryKey = "OUZQIpfKAas1aITVCsCaUrIaFNrBnZuia3q5ImMUjfFt116oI7yaYOgXxa0KHVRYx0RM1baVT3L1XRjpEm4zFQ==";

        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        private string databaseId = "TestDatabase";
        private string containerId = "TestContainer";

        public async Task GetStartedDemoAsync()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            await this.CreateDatabaseAsync();
            await this.CreateContainerAsync();
            await this.QueryItemsAsync();
        }

        private async Task CreateDatabaseAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        private async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/ProdId");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }

        private async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT  DISTINCT VALUE f.name FROM f IN TestContainer.Poses";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<string> queryResultSetIterator = this.container.GetItemQueryIterator<string>(queryDefinition);


            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<string> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (string c in currentResultSet)
                {
                    Console.WriteLine("" + c);
                    //foreach (string p in c)
                    //{
                    //    Console.WriteLine("" + p);
                    //}
                }
            }

        }
    }
}
