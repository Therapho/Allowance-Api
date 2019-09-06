using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskDefinitionSet
{
    public class GetTaskDefinitionList : Function
    {
        public GetTaskDefinitionList(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskDefinitionList")]
        public async Task<IActionResult> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "taskdefinitionset"), ] HttpRequest req, ILogger log)
        {
            log.LogTrace("GetTaskList function processed a request.");
            var query = from taskDefinition in _context.TaskDefinitionSet
                        orderby taskDefinition.Sequence
                        select taskDefinition;

            List<TaskDefinition> result = null;
            try
            {
                result = await query.ToListAsync();
            }
            catch (Exception exception)
            {
               
                return new BadRequestObjectResult($"Error trying to execute GetTaskDefinitionList.  {exception.Message}");
            }
            
            return new OkObjectResult(result);
        }
    }
}
