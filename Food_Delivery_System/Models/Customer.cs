using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using ServiceStack.DataAnnotations;

using System;
using Food_Delivery_System.Models;

namespace Food_Delivery.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }


        [System.ComponentModel.DataAnnotations.StringLength(32, MinimumLength = 2)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the CustomerName Minimum length of 2 maximum length of 32")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(13, MinimumLength = 10)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the Correct Customer Number")]
        public string ContactNumber { get; set; }
        public string? Gender { get; set; }

        //[System.ComponentModel.DataAnnotations.StringLength(13, MinimumLength = 10)]
        //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the Email Minimum length of 2 maximum length of 32")]
        public string Email { get; set; }
        //[System.ComponentModel.DataAnnotations.StringLength(13, MinimumLength = 10)]
        //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the Address Minimum length of 2 maximum length of 32")]
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

    }
}
