using AllowanceFunctions.Common;
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


namespace AllowanceFunctions.Services
{
    public abstract class LookupService<TEntity> : Function where TEntity : Lookup
    {

        protected LookupService(DatabaseContext context) : base(context) { }

        [FunctionName("GetList")]
        protected async Task< List<TEntity>> RunInternal(ILogger log)
        {
            log.LogTrace("GetRoleList function processed a request.");

            var query = from entity in _context.Set<TEntity>()
                        select entity;

            return await query.ToListAsync();
        }

    }
}
