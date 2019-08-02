
using System;
using System.Collections.Generic;
using AllowanceFunctions.Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AllowanceFunctions.Entities
{
    public class AccountEntity : Entity
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
    }
    
    public class AccountRow : TableRow<AccountEntity>
    {
        public AccountRow() { }
        public AccountRow(AccountEntity account) : base(account) { }
                     

        public double Balance { get; set; }
        public string Name { get; set; }

        
        protected override void MapFromEntity(AccountEntity account)
        {
            PartitionKey = account.Role;
            RowKey = account.Email;
            Balance = account.Balance;
            Name = account.Name;
            
        }

        protected override AccountEntity MapToEntity()
        {
            var account = new AccountEntity()
            {
                Role = PartitionKey,
                Email = RowKey,
                Balance = Balance,
                Name = Name,
                LastModifiedOn = Timestamp
            };

            return account;
        }
    }
}

