using Food_Delivery.Models;
using System.Text.Json.Serialization;

namespace Food_Delivery_System.Models
{
    public class FoodList
    {
        public string? HotelName { get; set; }
        public int FoodId { get; set; }
        public string? ImageId { get; set; }
        public string? FoodName { get; set; }
        public int Price { get; set; }

      
        public string? Type { get; set; }
    }
}
