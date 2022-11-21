using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Food_Delivery.Models
{
    public class OrderDetail
    {

        [Key]
        public int OrderDetailId { get; set; }
        public  int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? customer { get; set; }
        public  int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Orders? Orders { get; set; }

      
        public  int? FoodId { get; set; }

        [ForeignKey("FoodId")]
        public Food? Food { get; set; }


        public int? HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }

        

        public int Quantity { get; set; }

        public string OrderStatus { get; set; }


    }
}
