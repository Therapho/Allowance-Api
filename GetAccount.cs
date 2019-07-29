using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading;
using System.Linq;
using System.Net.Mail;
using System;

namespace AllowanceFunctions
{
    public static class GetAccount
    {
        [FunctionName("GetAccount")]
        public static async Task<Account> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{email}"),] HttpRequest req, string email,
             [Table("Account")] CloudTable cloudTable,
            ILogger log, CancellationToken ct)
        {
            string partitionKey = null;

            try
            {
                var mailAddress = new MailAddress(email);
                partitionKey = mailAddress.Host;
            }
            catch (System.Exception)
            {

                throw new ArgumentException($"Email {email} is not a valid email address");
            }
            

            
            log.LogInformation($"GetAccount function processed a request with parameter '{email}'.");

            var rangeQuery = new TableQuery<AccountEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email)));

            // Execute the query and loop through the results
            var results = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            Account account = null;
            var AccountEntity = results.FirstOrDefault();
            if(AccountEntity != null) account = AccountEntity.Value;

            return account;
        }


    }
}
