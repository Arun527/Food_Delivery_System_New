using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Delivery.Models
{
    public class OrderShipmentDetail
    {
        [Key]
        public int OrderShipmentDetailId { get; set; }

        public   int DeliveryPersonId { get; set; }

        [ForeignKey("DeliveryPersonId")]
        public DeliveryPerson? DeliveryPerson { get; set; }

        public virtual int OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        public OrderDetail? OrderDetail { get; set; }

        public string? TrackingStatus { get; set; } = "Out Of Delivery";

        public string? TrackingStatusDescription { get; set; } = "ThankYou For Your Valuable Order..";


    }
}
