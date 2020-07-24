using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaBeta.Model;

namespace YogaBeta.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }



        public async Task<List<Chakra>> GetChakrasAsync()
        {
            string queryString = "SELECT * FROM c ORDER BY c.ChakraNum";
            FeedIterator<Chakra> query = this._container.GetItemQueryIterator<Chakra>(new QueryDefinition(queryString));
            List<Chakra> results = new List<Chakra>();
            while (query.HasMoreResults)
            {
                var response =  query.ReadNextAsync();
                response.Wait();
                FeedResponse<Chakra> currentResultSet = response.Result;
                results.AddRange(currentResultSet.ToList());
            }
            return results;
        }
    }
}
