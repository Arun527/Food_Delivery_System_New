using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery.Models
{
    public class FoodDto
    {
        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public string Type { get; set; }
        public string? ImageId { get; set; }
        public int Price { get; set; }

        public int HotelId { get; set; }

        public string HotelName { get; set; }

        

        public string Location { get; set; }

        public List<SelectListItem>? hotelname { get; set; }
    }
}
