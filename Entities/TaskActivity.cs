using AllowanceFunctions.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllowanceFunctions.Entities
{
    [Table("taskactivities")]
    public class TaskActivity : Entity
    {
        public string Description { get; set; }
        public int TaskGroupId { get; set; }
        public decimal Value { get; set; }

        public int AccountId { get; set; }
        public int TaskDayId { get; set; }
        public int Sequence { get; set; }
        public int TaskWeekId { get; set; }
        public int StatusId { get; set; }
    }
}
