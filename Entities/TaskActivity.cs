using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Entities
{
    public class TaskActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TaskGroupId { get; set; }
        public bool Completed { get; set; }
        public bool Blocked { get; set; }
        public decimal Value { get; set; }

        public int ForAccountId { get; set; }
        public int TaskDayId { get; set; }
        public int Sequence { get; set; }
    }
}
