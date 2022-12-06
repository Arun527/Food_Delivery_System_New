
using ServiceStack.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = System.ComponentModel.DataAnnotations.StringLengthAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Delivery.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required(ErrorMessage = "Please enter the Hotel name")]
   
        public string HotelName { get; set; }

        [Required]

        public string Email { get; set; }

        public string? ImageId { get; set; }


        public string? Type { get; set; }

        public string Address { get; set; }


        [Unique]
        [StringLength(13, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter the Correct Customer Number")]
        
        public string ContactNumber { get; set; }

     
        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; }= DateTime.Now;


        [NotMapped]
        public IFormFile? CoverPhoto { get; set; }

    }
}
