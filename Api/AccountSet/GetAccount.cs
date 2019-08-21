using AllowanceFunctions.Common;
using AllowanceFunctions.Entities;
using AllowanceFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace AllowanceFunctions.Api.AccountSet
{
    public class GetAccount : Function
    {
        public GetAccount(DatabaseContext context) : base(context) { }

        [FunctionName("GetAccountByEmail")]
        public async Task<Account> Run(
            [HttpTrigger(Constants.AUTHORIZATION_LEVEL, "get", Route = "accountset/{email}"),] HttpRequest req, string email,
            ILogger log, CancellationToken ct)
        {
            log.LogTrace($"GetAccount function processed a request with parameter '{email}'.");

            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch (System.Exception)
            {

                throw new ArgumentException($"Email {email} is not a valid email address");
            }
                       
            var query = from account in _context.AccountSet where account.Username == email select account;
            
            return await query.FirstOrDefaultAsync();
        }


    }
}
