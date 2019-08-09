using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.Lookups
{
    public class GetTaskGroupList : LookupService<TaskGroup>
    {
        public GetTaskGroupList(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskGroupList")]
        public async Task<List<TaskGroup>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "lookups/taskgroupset"), ] HttpRequest req, ILogger log)
        {
            return await RunInternal(log);
        }
    }
}
