namespace Food_Delivery.Models
{
    public class OrderRequest
    {

        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public List<FoodDetaile> Food { get; set; }

    }
    public class FoodDetaile
    {
        public int FoodId { get; set; }

        public int Quantity { get; set; }
    }

}
