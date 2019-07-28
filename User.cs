
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Allowance
{
    public class User
    {
        public double Balance { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
    public class UserEntity : EntityAdapter<User>
    {
      public UserEntity() : base() {}

      public UserEntity(User user) : base(user){}

        protected override string BuildPartitionKey()
        {
            return "User";
        }

        protected override string BuildRowKey()
        {
            return Value.Email;
        }
    }
}

