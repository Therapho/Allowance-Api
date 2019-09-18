using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskDaySet
{
    public class PutTaskDay : Function
    {
        private TaskDayService _taskDayService;

        public PutTaskDay(AuthorizationService authorizationService, TaskDayService taskDayService)
            : base(authorizationService) { _taskDayService = taskDayService; }

        [FunctionName("PutTaskDay")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "put", Route = "taskdayset/{id?}")] HttpRequest req, ILogger log, CancellationToken ct, int? id )
        {
            log.LogTrace($"PutTaskDay function processed a request for id:{id}.");
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TaskDay>(requestBody);
            var userIdentifier = await GetTargetUserIdentifier(req);
         

            try
            {
                if (data.UserIdentifier != userIdentifier && ! await IsParent(req))
                {
                    throw new SecurityException("Invalid attempt to access a record by an invalid user");
                }
                if (id.HasValue) Ensure.That(data.Id = id.Value);
                await _taskDayService.Update(data);
            }
            catch (Exception exception)
            {

                return new BadRequestObjectResult($"Error trying to execute PutTaskDay.  {exception.Message}");
            }
            return new OkObjectResult( data.Id.Value);
        }

    }
}