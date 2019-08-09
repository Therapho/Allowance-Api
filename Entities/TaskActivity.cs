using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllowanceFunctions.Entities
{
    [Table("taskactivities")]
    public class TaskActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TaskGroupId { get; set; }
        public bool Completed { get; set; }
        public bool Blocked { get; set; }
        public decimal Value { get; set; }

        public int AccountId { get; set; }
        public int TaskDayId { get; set; }
        public int Sequence { get; set; }
    }
}
