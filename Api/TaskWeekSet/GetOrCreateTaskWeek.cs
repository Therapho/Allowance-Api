using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace AllowanceFunctions.Api.TaskWeekSet
{
    public class GetOrCreateTaskWeek
    {
        private TaskWeekService _taskWeekService;
        private TaskDefinitionService _taskDefinitonService;
        private TaskDayService _taskDayService;
        private TaskActivityService _taskActivityService;

        public GetOrCreateTaskWeek(TaskWeekService taskWeekService, TaskDefinitionService taskDefinitionService,
            TaskDayService taskDayService, TaskActivityService taskActivityService)
        {
            _taskWeekService = taskWeekService;
            _taskDefinitonService = taskDefinitionService;
            _taskDayService = taskDayService;
            _taskActivityService = taskActivityService;
        }
        [FunctionName("GetOrCreateTaskWeek")]
        public async Task<IActionResult> Run(
             [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "getorcreatetaskweek"),] HttpRequest req, ILogger log)
        {
            Ensure.That(req.Query.ContainsKey("startdate")).IsTrue();
            Ensure.That(req.Query.ContainsKey("accountid")).IsTrue();


            var startDate = req.Query.GetValue<DateTime>("startdate").StartOfDay();

            var accountId = req.Query.GetValue<int>("accountid");

            log.LogTrace($"GetTaskActivityListByDay function processed a request for taskWeekId={accountId}, startDate={startDate}.");

            TaskWeek taskWeek = null;

            try

            {
                taskWeek = await _taskWeekService.GetOrCreate(accountId, startDate);
                await _taskDayService.GetOrCreateList(accountId, taskWeek);
            }

            catch (Exception exception)
            {
                return new BadRequestObjectResult(new BadRequestErrorMessageResult(exception.Message));
            }
            return new OkObjectResult(taskWeek);
        }
    }
}
