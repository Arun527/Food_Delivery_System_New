using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using ServiceStack.DataAnnotations;

using System;

namespace Food_Delivery.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
      
     
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; }
      
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

    }
}
