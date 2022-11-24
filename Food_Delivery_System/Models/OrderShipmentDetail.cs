using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Food_Delivery.Models
{
    public class OrderShipmentDetail
    {
        [Key]
        public int OrderShipmentDetailId { get; set; }

        public int DeliveryPersonId { get; set; }

        [ForeignKey("DeliveryPersonId")]
        [JsonIgnore]
        public DeliveryPerson? DeliveryPerson { get; set; }

        public int OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        [JsonIgnore]
        public OrderDetail? OrderDetail { get; set; }

        public string? TrackingStatus { get; set; } = "Out Of Delivery";


    }
}
