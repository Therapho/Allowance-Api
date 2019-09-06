using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.Lookups
{
    public class GetRoleList : LookupService<Role>
    {

        public GetRoleList(DatabaseContext context) : base(context) { }

        [FunctionName("GetRoleList")]
        public async Task<List<Role>> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", "options", Route = "lookups/roleset"), ] HttpRequest req, ILogger log)
        {
            return await RunInternal(log);
        }
    }
}
