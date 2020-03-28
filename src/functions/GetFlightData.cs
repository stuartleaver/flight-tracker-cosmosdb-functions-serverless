using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace flighttracker.functions
{
    public static class GetFlightData
    {
        [FunctionName("GetFlightData")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getflightdata")] HttpRequest req,
            [CosmosDB(
                databaseName: "flighttracker",
                collectionName: "flights",
                SqlQuery = "SELECT * FROM c",
                ConnectionStringSetting = "CosmosDbConnectionString")] IEnumerable<Flight> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(documents);
        }
    }
}