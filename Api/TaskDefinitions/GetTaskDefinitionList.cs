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
    public class GetTaskDefinitionList : Function
    {
        public GetTaskDefinitionList(DatabaseContext context) : base(context) { }

        [FunctionName("GetTaskDefinitionList")]
        public async Task<List<TaskDefinition>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "taskdefinitionset"), ] HttpRequest req, ILogger log)
        {
            Initialize(log,"GetTaskList function processed a request.");

            var query = from taskDefinition in _context.TaskDefinitions
                        orderby taskDefinition.Sequence
                        select taskDefinition;

            return query.ToList();
        }
    }
}
