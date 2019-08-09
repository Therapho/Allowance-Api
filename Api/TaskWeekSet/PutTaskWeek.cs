using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using EnsureThat;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.TaskWeekSet
{
    public class PutTaskWeek : Function
    {
        public PutTaskWeek(DatabaseContext context) : base(context) { }

        [FunctionName("PutTaskWeek")]
        public async Task<int> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "taskweekset/{id?}")] HttpRequest req, ILogger log, CancellationToken ct, int? id )
        {
            log.LogTrace($"PutTaskWeek function processed a request for id:{id}.");
           
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TaskWeek>(requestBody);
            //data.WeekStartDate = data.WeekStartDate.FirstDayOfWeek();

            if(id.HasValue) Ensure.That(data.Id = id.Value);

            try
            {
                await _context.AddAsync(data);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception exception)
            {

                log.LogError($"Exception {exception.Message} occurred");
            }
            return data.Id.Value;
        }

    }
}