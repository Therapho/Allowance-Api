using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading;
using System.Linq;


namespace Allowance
{
    public static class GetUser
    {
        [FunctionName("GetUser")]
        public static async Task<User> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{email}"),] HttpRequest req, string email,
             [Table("Data")] CloudTable cloudTable,
            ILogger log, CancellationToken ct)
        {
            //string name = req.Query["name"];
            log.LogInformation($"GetUser function processed a request with parameter '{email}'.");

            var rangeQuery = new TableQuery<UserEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "User"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email)));

            // Execute the query and loop through the results
            var results = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            User user = null;
            var userEntity = results.FirstOrDefault();
            if(userEntity != null) user = userEntity.Value;

            return user;
        }


    }
}
