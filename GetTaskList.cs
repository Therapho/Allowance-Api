using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using AllowanceFunctions.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AllowanceFunctions
{
    public static class GetTaskList
    {
        [FunctionName("GetTaskList")]
        public static async Task<List<TaskEntity>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "task"), ] HttpRequest req, [Table("Task")] CloudTable cloudTable, ILogger log)
        {
            log.LogInformation("GetTaskList function processed a request.");

            var query = new TableQuery<TaskRow>();
            var results = await cloudTable.ExecuteQuerySegmentedAsync(query, null);
            var taskList = new List<TaskEntity>();

            foreach (var row in results)
            {
                taskList.Add(row.Entity);
            }

            return taskList.OrderBy(e=>e.Sequence).ToList();
        }
    }
}
