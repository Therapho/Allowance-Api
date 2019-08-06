using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Services
{
    public abstract class Function
    {
        protected readonly DatabaseContext _context;

        public Function(DatabaseContext context)
        {
            _context = context;
        }

        protected void Initialize(ILogger log, string message)
        {
            log.LogInformation(message);
        }

    }
}
