
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AllowanceFunctions
{
    public class Account : Record
    {
        public double Balance { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
    public class AccountEntity : EntityAdapter<Account>
    {
      public AccountEntity() : base() {}

      public AccountEntity(Account Account) : base(Account){}

        protected override string BuildPartitionKey()
        {
            return "Account";
        }

        protected override string BuildRowKey()
        {
            return Value.Email;
        }
    }
}

