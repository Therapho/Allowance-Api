using System.ComponentModel.DataAnnotations.Schema;

namespace AllowanceFunctions.Entities
{
    [Table("accounts")]
    public class Account 
    {
        public int? Id { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
    }
    
    
}

