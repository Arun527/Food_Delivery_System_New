namespace Food_Delivery.Models
{
    public class OrderRequest
    {

        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public string OrderStatus { get; set; } = "Order Placed";
        public List<FoodDetaile> Food { get; set; }

    }
    public class FoodDetaile
    {
        public int FoodId { get; set; }

        public int HotelId { get; set; }
        public int Quantity { get; set; }
    }

}
