using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.Lookups
{
    public class GetTransactionCategoryList : LookupService<TransactionCategory>
    {
        public GetTransactionCategoryList(DatabaseContext context) : base(context) { }

        [FunctionName("GetTransactionCategoryList")]
        public async Task<List<TransactionCategory>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lookups/transactioncategoryset"),] HttpRequest req, ILogger log)
        {
            return RunInternal(log);
        }
    }
}
