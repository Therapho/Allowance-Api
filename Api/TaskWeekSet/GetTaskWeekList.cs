using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllowanceFunctions.Common;

namespace AllowanceFunctions.Api.TaskWeekSet
{
    public class GetTaskWeekList : Function
    {
        public GetTaskWeekList(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskWeekList")]
        public async Task<List<TaskWeek>> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "taskweekset")] HttpRequest req, ILogger log)
        {
            
            var dateStart = req.Query.GetValueOrDefault<DateTime>("startdate");
            Ensure.That(dateStart.HasValue).IsTrue();
            dateStart = dateStart.Value.FirstDayOfWeek();
            

            var dateEnd = req.Query.GetValueOrDefault<DateTime>("enddate");
            if (!dateEnd.HasValue) dateEnd = dateStart;
            dateEnd = dateEnd.Value.LastDayOfWeek();

            log.LogTrace($"GetTaskWeek triggered with Date from {dateStart} to {dateEnd}");
                    
            var query = from taskWeek in _context.TaskWeekSet
                        where taskWeek.WeekStartDate >= dateStart && taskWeek.WeekStartDate <= dateEnd
                        select taskWeek;

            return await query.ToListAsync();
        }
    }
}
