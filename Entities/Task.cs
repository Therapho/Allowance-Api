using AllowanceFunctions.Common;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Entities
{
    public class TaskEntity : Entity
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public Double Value { get; set; }
        public int Sequence { get; set; }
        public string Frequency { get; set; }
        public int Maximum { get; set; }
        
    }

    public class TaskRow : TableRow<TaskEntity>
    {   
        public Double Value { get; set; }
        public int Sequence { get; set; }
        public string Frequency { get; set; }
        public int Maximum { get; set; }
        public string Description { get; set; }

        protected override void MapFromEntity(TaskEntity entity)
        {
            PartitionKey = entity.Group;
            RowKey = entity.Id.ToString();
            Description = entity.Description;
            Value = entity.Value;
            Sequence = entity.Sequence;
            Frequency = entity.Frequency;
            Maximum = entity.Maximum;
            

        }

        protected override TaskEntity MapToEntity()
        {
            var entity = new TaskEntity()
            {
                Group = PartitionKey,
                Id = new Guid(RowKey),
                Description = Description,
                Value = Value,
                Sequence = Sequence,
                Frequency = Frequency,
                Maximum = Maximum
            };
            return entity;
        }
    }
}
