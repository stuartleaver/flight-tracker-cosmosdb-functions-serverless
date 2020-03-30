using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace flighttracker.functions
{
    public static class FlightDataCosmosDBChangeFeed
    {
        [FunctionName("FlightDataCosmosDBChangeFeed")]
        public static async Task RunAsync([CosmosDBTrigger(
            databaseName: "flighttracker",
            collectionName: "flights",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents,
            [SignalR(HubName = "flightdata")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.LogInformation("Documents modified " + documents.Count);

                foreach (var document in documents)
                {
                    await signalRMessages.AddAsync(
                        new SignalRMessage
                        {
                            Target = "newFlightData",
                            Arguments = new[] { document }
                        });
                }
            }
        }
    }
}
