using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace flighttracker.functions
{
    public static class GetOpenSkyFlightData
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("GetOpenSkyFlightData")]
        public static async Task RunAsync([TimerTrigger("*/5 * * * * *")]TimerInfo myTimer,
        [CosmosDB(
            databaseName: "flighttracker",
            collectionName: "flights",
            ConnectionStringSetting = "CosmosDbConnectionString")] IAsyncCollector<Flight> documents,
        ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            using (var response = await client.GetAsync("https://opensky-network.org/api/states/all?lamin=49.9599&lomin=-7.5721&lamax=58.6350&lomax=1.6815"))
            using (var content = response.Content)
            {
                var result = JsonConvert.DeserializeObject<FlightData>(await content.ReadAsStringAsync());

                foreach (var item in result.states)
                {
                    var flight = Flight.CreateFlight(item);

                    await documents.AddAsync(flight);
                }
            }
        }
    }
}