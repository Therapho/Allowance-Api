using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllowanceFunctions.Common;

namespace AllowanceFunctions.Api.TaskDaySet
{
    public class GetTaskDay : Function
    {
        public GetTaskDay(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskDay")]
        public async Task<List<TaskDay>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "taskdayset")] HttpRequest req, ILogger log)
        {
            var accountId = req.Query.GetValueOrDefault<int>("accountid");
            var taskWeekId = req.Query.GetValueOrDefault<int>("taskweekid");

            log.LogTrace($"GetTaskDayByTaskWeek triggered with taskWeekId={taskWeekId} && accountid={accountId}");
         
            var query = from taskday in _context.TaskDaySet
                        where taskday.TaskWeekId == taskWeekId && taskday.AccountId == accountId
                        orderby taskday.Date
                        select taskday;

            return await query.ToListAsync();
        }
    }
}
