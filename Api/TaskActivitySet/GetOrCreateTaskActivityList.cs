using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;


namespace AllowanceFunctions.Api.TaskActivitySet
{
    public class GetOrCreateTaskActivityList     
    {
        private TaskWeekService _taskWeekService;
        private TaskDefinitionService _taskDefinitonService;
        private TaskDayService _taskDayService;
        private TaskActivityService _taskActivityService;

        public GetOrCreateTaskActivityList(TaskWeekService taskWeekService, TaskDefinitionService taskDefinitionService, 
            TaskDayService taskDayService, TaskActivityService taskActivityService)
        {
            _taskWeekService = taskWeekService;
            _taskDefinitonService = taskDefinitionService;
            _taskDayService = taskDayService;
            _taskActivityService = taskActivityService;
        }

        [FunctionName("GetOrCreateTaskActivityList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "getorcreatetaskactivitylist"),] HttpRequest req, ILogger log)
        {
            var startDate = req.Query.GetValue<DateTime>("weekdstartdate").StartOfDay();

            var accountId = req.Query.GetValue<int>("accountid");
            var taskWeekId = req.Query.GetValue<int>("taskweekid");

            log.LogTrace($"GetTaskActivityListByDay function processed a request for taskWeekId={accountId}, startDate={startDate}.");

            List<TaskActivity> taskActivityList = null;

            try

            {
                TaskWeek taskWeek = null;

                if (taskWeekId > 0)
                    taskWeek= await _taskWeekService.Get(taskWeekId);
                else
                    taskWeek=await _taskWeekService.GetOrCreate(accountId, startDate);

                taskActivityList = await  _taskActivityService.GetList(accountId, taskWeek.Id.Value);

                if (taskActivityList == null) taskActivityList = new List<TaskActivity>();

                if (taskActivityList.Count() == 0)
                {
                   
                    var taskDayList = await _taskDayService.GetOrCreateList(accountId, taskWeek);
                    var taskDefinitionList = await _taskDefinitonService.GetList();
                    taskActivityList = await _taskActivityService.CreateList(accountId, taskDayList, taskDefinitionList);
                }
            }
            catch(Exception exception)
            {
                return new BadRequestObjectResult(new BadRequestErrorMessageResult(exception.Message));
            }
            return new OkObjectResult(taskActivityList);
        }




    }
}
