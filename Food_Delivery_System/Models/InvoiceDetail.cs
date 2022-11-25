using System.ComponentModel.DataAnnotations;

namespace Food_Delivery.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public DateTime Orderdate { get; set; }
        public string CustomerName { get; set; }
        public int FoodId { get; set; }
        public string FoodName{ get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }



    }
  
}