using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;

using System.Net.Http;

namespace Allowance
{
    public static class SetUser
    {

        [FunctionName("SetUser")]
        public static async Task Run(
                    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user"),] HttpRequestMessage req,
                     [Table("Data")] CloudTable cloudTable,
                    ILogger log)
        {
            dynamic body = await req.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<User>(body as string);
            var profileEntity = new UserEntity(profile);

            log.LogInformation(message: $"SetUser function processed a request with parameter '{profile.Email}'.");

            var updateOperation = TableOperation.InsertOrReplace(profileEntity);
            var result = await cloudTable.ExecuteAsync(updateOperation);

        }
    }
}
