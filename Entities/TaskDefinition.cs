using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceFunctions.Entities
{
    [Table("taskdefinitions")]
    public class TaskDefinition
    {
        public int Id { get; set; }
        public int TaskGroupId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Sequence { get; set; }
        public bool Weekly { get; set; }
        public int Maximum { get; set; }

    }

}