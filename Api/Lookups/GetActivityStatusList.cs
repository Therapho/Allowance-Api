﻿using AllowanceFunctions.Common;
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

namespace AllowanceFunctions.Api.Lookups
{
    public class GetActivityStatusList : LookupService<ActivityStatus>
    {
        public GetActivityStatusList(DatabaseContext context) : base(context) { }

        [FunctionName("GetActivityStatusList")]
        public async Task<List<ActivityStatus>> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "lookups/activitystatusset"),] HttpRequest req, ILogger log)
        {
            return await RunInternal(log);
        }
    }
}
