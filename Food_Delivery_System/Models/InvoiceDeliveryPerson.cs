namespace Food_Delivery_System.Models
{
    public class InvoiceDeliveryPerson
    {
        public int OrderShipmentDetailId { get; set; }

        public int DeliveryPersonId { get; set; }

        public string DeliveryPersonName { get; set; }
        public string Contactnumber { get; set; }
        public DateTime OrderShipmentdateTime { get; set; }

        public string CustomerName { get; set; }
        public string Address { get; set; }

        public int Quantity { get; set; }


       
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
    }
}
