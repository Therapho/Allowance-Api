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

namespace AllowanceFunctions.Api.TaskDefinitions
{
    public class GetTaskActivityListByDay : Function    
    {
        public GetTaskActivityListByDay(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskActivityListByDay")]
        public async Task<List<TaskActivity>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "taskactivityset/{taskdayid}"),] HttpRequest req, int taskdayid, ILogger log)
        {
            Initialize(log, $"GetTaskActivityListByDay function processed a request for taskdayid={taskdayid}.");

            var query = from taskActivity in _context.TaskActivitySet
                        where taskActivity.TaskDayId == taskdayid
                        orderby taskActivity.Sequence
                        select taskActivity;

            return query.ToList();
        }
    }
}
