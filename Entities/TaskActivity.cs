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

        public int TaskGroupId { get; set; }
        public int TaskDayId { get; set; }
        public int Sequence { get; set; }
        public int TaskWeekId { get; set; }
        public int StatusId { get; set; }
        public int TaskDefinitionId { get; set; }
        public int DaySequence { get; set; }
        public Guid UserIdentifier { get; set; }
    }
}
