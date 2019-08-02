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
using AllowanceFunctions.Entities;

namespace AllowanceFunctions
{
    public static class GetAccount
    {
        [FunctionName("GetAccount")]
        public static async Task<AccountEntity> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{email}"),] HttpRequest req, string email,
             [Table("Account")] CloudTable cloudTable,
            ILogger log, CancellationToken ct)
        {
            
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (System.Exception)
            {

                throw new ArgumentException($"Email {email} is not a valid email address");
            }
            

            
            log.LogInformation($"GetAccount function processed a request with parameter '{email}'.");

            var rangeQuery = new TableQuery<AccountRow>().Where(                  
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, email)
            );

            // Execute the query and loop through the results
            var results = await cloudTable.ExecuteQuerySegmentedAsync(rangeQuery, null);
            AccountEntity account = null;
            var accountRow = results.FirstOrDefault();
            if(accountRow != null) account = accountRow.Entity;

            return account;
        }


    }
}
