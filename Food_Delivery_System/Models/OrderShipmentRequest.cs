using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery_System.Models
{
    public class OrderShipmentRequest
    {
        
        public int OrderShipmentDetailId { get; set; }

    
        public int DeliveryPersonId { get; set; }
        public List<OrderShipmentList>? ShipmentRequest { get; set; }

    }

    public class OrderShipmentList
    {
        
        public int OrderDetailId { get; set; }
    }

}
