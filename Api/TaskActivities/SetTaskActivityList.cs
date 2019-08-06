using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskActivities
{
    public class SetTaskActivityList : Function
    {
        public SetTaskActivityList(DatabaseContext context) : base(context) { }

        [FunctionName("SetTaskActivityList")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "taskactivityset"),] HttpRequest req, ILogger log)
        {
            Initialize(log, $"SetTaskActivityList function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<List<TaskActivity>>(requestBody);

        }
    }
}
