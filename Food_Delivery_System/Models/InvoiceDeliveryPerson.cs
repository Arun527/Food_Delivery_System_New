namespace Food_Delivery_System.Models
{
    public class InvoiceDeliveryPerson
    {
        public int OrderShipmentDetailId { get; set; }

        public int DeliveryPersonId { get; set; }

        public int DeliveryPersonName { get; set; }

        public string CustomerName { get; set; }

        public string Contactnumber { get; set; }
        public int price { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }
        public int OrderDetailId { get; set; }

    }
}
