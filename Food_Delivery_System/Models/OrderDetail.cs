using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;
using System.Text.Json.Serialization;

namespace Food_Delivery.Models
{
    public class OrderDetail
    {

        [Key]
        
        public int OrderDetailId { get; set; }
        public  int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public Customer? customer { get; set; }
        public  int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Orders? Orders { get; set; }
        public  int? FoodId { get; set; }

        [ForeignKey("FoodId")]
        [JsonIgnore]
        public Food? Food { get; set; }

        public int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        [JsonIgnore]
        public Hotel? Hotel { get; set; }
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int Quantity { get; set; }

        public string? OrderStatus { get; set; }


    }
}
