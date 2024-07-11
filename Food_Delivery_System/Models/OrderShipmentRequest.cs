namespace Food_Delivery_System.Models
{
    public class OrderShipmentRequest
    {
        
        public int OrderShipmentDetailId { get; set; }

        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int DeliveryPersonId { get; set; }
        public List<OrderShipmentList>? ShipmentRequest { get; set; }    
    }

    public class OrderShipmentList
    {
        [System.ComponentModel.DataAnnotations.Range(1, 2147483647)]
        public int OrderDetailId { get; set; }
    }

}
