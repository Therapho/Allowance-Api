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
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security;

namespace AllowanceFunctions.Api.TaskDaySet
{
    public class GetTaskDayList : Function
    {
        private TaskDayService _taskDayService;

        public GetTaskDayList(AuthorizationService authorizationService, TaskDayService taskDayService)
            : base(authorizationService) { _taskDayService = taskDayService; }

        [FunctionName("GetTaskDayList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "taskdayset")] HttpRequest req, ILogger log)
        {
            List<TaskDay> result = null;
            try
            {
                var callingUserIdentifier = req.GetUserIdentifier();

                var taskWeekId = req.Query.GetValueOrDefault<int>("taskweekid");

                log.LogTrace($"GetTaskDay triggered with taskWeekId={taskWeekId} by userIdentifier={callingUserIdentifier}");

                result = await _taskDayService.GetByTaskWeek(taskWeekId.Value);
                
                if (result != null && result.Count > 0 
                    && callingUserIdentifier != result[0].UserIdentifier 
                    && !await IsParent(req))
                    throw new SecurityException("Invalid attempt to access taskDayList.  User not permitted.");

            }
            catch (Exception exception)
            {

                return new BadRequestObjectResult($"Error trying to execute GetTaskDay.  {exception.Message}");
            }

            return new OkObjectResult(result);
        }
    }
}
