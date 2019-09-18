using AllowanceFunctions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Services
{
    public class TransactionLogService : EntityService<TransactionLog>
    {
        public TransactionLogService(DatabaseContext databaseContext): base(databaseContext) { }
    }
}
