using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery.Models
{
    public class OrderRequest
    {

        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
        public int? OrderDetailId { get; set; }

        public string OrderStatus { get; set; } = "Order Placed";

        public List<SelectListItem>? CustomerList { get; set; }
        public List<FoodDetaile> Food { get; set; }

    }
    public class FoodDetaile
    {
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int FoodId { get; set; }
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int HotelId { get; set; }
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int Quantity { get; set; }
    }


}
