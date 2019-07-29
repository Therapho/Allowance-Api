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

namespace AllowanceFunctions
{
    public static class SetAccount
    {

        [FunctionName("SetAccount")]
        public static async Task Run(
                    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "account"),] HttpRequestMessage req,
                     [Table("Account")] CloudTable cloudTable,
                    ILogger log)
        {
            dynamic body = await req.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<Account>(body as string);
            var profileEntity = new AccountEntity(profile);

            log.LogInformation(message: $"SetAccount function processed a request with parameter '{profile.Email}'.");

            var updateOperation = TableOperation.InsertOrReplace(profileEntity);
            var result = await cloudTable.ExecuteAsync(updateOperation);

        }
    }
}
