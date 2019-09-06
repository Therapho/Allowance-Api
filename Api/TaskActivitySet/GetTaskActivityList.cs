using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AllowanceFunctions.Api.TaskActivitySet
{
    public class GetTaskActivityListByDay : Function    
    {
        public GetTaskActivityListByDay(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskActivityList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "taskactivityset"),] HttpRequest req, ILogger log)
        {
            var taskWeekId = req.Query.GetValue<int>("taskweekid");
            Ensure.That(taskWeekId).IsGt(0);

            log.LogTrace($"GetTaskActivityListByDay function processed a request for taskWeekId={taskWeekId}.");

            var query = from taskActivity in _context.TaskActivitySet
                        where taskActivity.TaskWeekId == taskWeekId
                        orderby taskActivity.Sequence
                        select taskActivity;

            List<TaskActivity> result = null;
            try
            {
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
               
                return new BadRequestObjectResult($"Error trying to execute GetActivityList with TaskWeekId:{taskWeekId}.  {exception.Message}");
            }
            
            return new OkObjectResult( result);
        }
    }
}
