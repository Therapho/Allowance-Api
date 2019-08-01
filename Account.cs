
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AllowanceFunctions
{
    public class Account : Entity
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public double Balance { get; set; }
        public string Name { get; set; }
    }
    
    public class AccountRow : TableRow<Account>
    {
        public AccountRow() { }
        public AccountRow(Account account)
        {
            MapFromEntity(account);
        }
                     

        public double Balance { get; set; }
        public string Name { get; set; }

        
        protected override void MapFromEntity(Account account)
        {
            PartitionKey = account.Role;
            RowKey = account.Email;
            Balance = account.Balance;
            Name = account.Name;
            
        }

        protected override Account MapToEntity()
        {
            var account = new Account()
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

