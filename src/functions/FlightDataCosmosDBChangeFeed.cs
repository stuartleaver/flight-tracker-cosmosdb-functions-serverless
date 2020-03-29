using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class FlightDataCosmosDBChangeFeed
    {
        [FunctionName("FlightDataCosmosDBChangeFeed")]
        public static void Run([CosmosDBTrigger(
            databaseName: "flighttracker",
            collectionName: "flights",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents,
            ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified " + documents.Count);
                log.LogInformation("First document Id " + documents[0].Id);
            }
        }
    }
}
