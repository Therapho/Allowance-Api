using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskActivitySet
{
    public class PutTaskActivityList : Function
    {
        public PutTaskActivityList(DatabaseContext context) : base(context) { }

        [FunctionName("PutTaskActivitySet")]
        public async Task Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "put", Route = "taskactivityset"),] HttpRequest req, ILogger log, CancellationToken ct)
        {
            log.LogTrace($"SetTaskActivityList function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!requestBody.Contains("[")) requestBody = $"[{requestBody}]";

            var data = JsonConvert.DeserializeObject<List<TaskActivity>>(requestBody);
            try
            {
                await _context.AddRangeAsync(data);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception exception)
            {

                log.LogError($"Exception {exception.Message} occurred");
                throw;
            }

        }
    }
}
