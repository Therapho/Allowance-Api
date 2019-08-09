using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllowanceFunctions.Entities
{
    [Table("transactionlogs")]
    public class TransactionLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public string User { get; set; }

    }
}
