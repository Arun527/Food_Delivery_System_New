namespace Food_Delivery_System.Models
{
    public class TrackingDetail
    {

        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public string? OrderStatus { get; set; }
        public string? TrackingStatus { get; set; } = "Out Of Delivery";
        public string TrackingStatusDescription { get; set; } = "ThankYou For Your Valuable Order..";
    }
}
