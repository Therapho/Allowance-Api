using AllowanceFunctions.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Common
{
    public abstract class Function
    {
        protected readonly DatabaseContext _context;

        public Function(DatabaseContext context)
        {
            _context = context;
        }
  
        
    }
}
