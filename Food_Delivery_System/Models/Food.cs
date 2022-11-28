using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Food_Delivery.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(32, MinimumLength = 2)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the Food Name Minimum length of 2 maximum length of 32")]

        public string? FoodName { get; set; }

        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int Price { get; set; }

        public string? ImageId { get; set; }

        public  int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        [JsonIgnore]
        public Hotel? Hotel { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(32, MinimumLength = 2)]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter the Food Type Minimum length of 2 maximum length of 32")]

        public string? Type { get; set; }

        [NotMapped]
        public IFormFile? CoverPhoto { get; set; }

    }

  


}